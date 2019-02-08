
// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace ZipatoIoT
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Polly;
    using Polly.Extensions.Http;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Background;
    using Windows.Storage;
    using Windows.System.Threading;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Serilog;
    using Serilog.AspNetCore;

    using ZipatoIoT.Models;
    using ILogger = Microsoft.Extensions.Logging.ILogger;

    #endregion

    public sealed class StartupTask : IBackgroundTask
    {
        #region Private Data Members

        private BackgroundTaskDeferral _deferral = null;
        private AppSettings _settings = new AppSettings();
        private ThreadPoolTimer _timer;
        private ILogger _logger;

        /// <summary>
        /// Data values written to Zipato virtual meters.
        /// </summary>
        private NetatmoValues _netatmoValues;
        private ETAPU11Values _etapu11Values;
        private EM300LRValues _em300lrValues;
        private FroniusValues _froniusValues;
        private KWLEC200Values _kwlec200Values;
        private HomeDataValues _homeDataValues;
        private WallboxValues _wallboxValues;

        #endregion

        #region Public Methods

        /// <summary>
        /// Main background task.
        /// </summary>
        /// <param name="taskInstance"></param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstanceCanceled;

            try
            {
                // Copy the appsetting.json file if necessary.
                if (CopyAppSettings().Result)
                {
                    // Set the default culture.
                    CultureInfo.CurrentCulture = new CultureInfo("en-US");

                    // Read the application settings file containing the Serilog configuration.
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(ApplicationData.Current.LocalFolder.Path)
                        .AddJsonFile("appsettings.json", false, false)
                        .AddEnvironmentVariables()
                        .Build();

                    configuration.GetSection("AppSettings")?.Bind(_settings);

                    // Setting up the static Serilog logger.
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .Enrich.FromLogContext()
                        .CreateLogger();

                    // Setting up the application logger.
                    var services = new ServiceCollection()
                        .AddLogging(builder =>
                        {
                            builder.AddSerilog();
                        });

                    // Add services.
                    services.AddSingleton<ILoggerFactory>(logger => new SerilogLoggerFactory(null, true));
                    services.Configure<SettingsData>(configuration.GetSection("AppSettings"));
                    services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
                    services.AddSingleton<ISettingsData, SettingsData>();

                    // Setting up the Zipato HTTP client.
                    services.AddHttpClient<IZipatoClient, ZipatoClient>(client =>
                    {
                        client.BaseAddress = new Uri(_settings.Servers.Zipato);
                        client.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
                    })
                    .ConfigureHttpMessageHandlerBuilder(config => new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
                    })
                    .AddPolicyHandler(HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                            .WaitAndRetryAsync(_settings.Retries,
                                               attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));

                    // Setup the data values.
                    services.AddSingleton<NetatmoValues>();
                    services.AddSingleton<ETAPU11Values>();
                    services.AddSingleton<EM300LRValues>();
                    services.AddSingleton<FroniusValues>();
                    services.AddSingleton<KWLEC200Values>();
                    services.AddSingleton<HomeDataValues>();
                    services.AddSingleton<WallboxValues>();

                    // Build the service provider and add the logger.
                    var serviceProvider = services.BuildServiceProvider();
                    _logger = serviceProvider.GetService<ILogger<StartupTask>>();

                    // Get the data values instances.
                    _netatmoValues = ActivatorUtilities.CreateInstance<NetatmoValues>(serviceProvider);
                    _etapu11Values = ActivatorUtilities.CreateInstance<ETAPU11Values>(serviceProvider);
                    _em300lrValues = ActivatorUtilities.CreateInstance<EM300LRValues>(serviceProvider);
                    _froniusValues = ActivatorUtilities.CreateInstance<FroniusValues>(serviceProvider);
                    _kwlec200Values = ActivatorUtilities.CreateInstance<KWLEC200Values>(serviceProvider);
                    _homeDataValues = ActivatorUtilities.CreateInstance<HomeDataValues>(serviceProvider);
                    _wallboxValues = ActivatorUtilities.CreateInstance<WallboxValues>(serviceProvider);

                    // Update immediately.
                    ThreadPoolTimer.CreateTimer(async (timer) => await SendValuesAsync(), TimeSpan.Zero);

                    // Setup periodic timer (updating every 60 seconds).
                    _timer = ThreadPoolTimer.CreatePeriodicTimer(async (timer) => await SendValuesAsync(), TimeSpan.FromMinutes(1));
                }
                else
                {
                    throw new ApplicationException("StartupTask application settings not found.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception StartupTask.");
                _deferral.Complete();
            }

            _logger?.LogInformation($"StartupTask done.");
        }

        private void TaskInstanceCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _logger?.LogInformation($"StartupTask cancelled: {reason}");
            _timer.Cancel();
            _deferral.Complete();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to send updated data values to the Zipato home control.
        /// </summary>
        private async Task SendValuesAsync()
        {
            _logger?.LogDebug($"Netatmo send data {(await _netatmoValues.UpdateValuesAsync() ? "" : "not")} successful.");
            _logger?.LogDebug($"ETAPU11 send data {(await _etapu11Values.UpdateValuesAsync() ? "" : "not")} successful.");
            _logger?.LogDebug($"EM300LR send data {(await _em300lrValues.UpdateValuesAsync() ? "" : "not")} successful.");
            _logger?.LogDebug($"Fronius send data {(await _froniusValues.UpdateValuesAsync() ? "" : "not")} successful.");
            _logger?.LogDebug($"KWLEC200 send data {(await _kwlec200Values.UpdateValuesAsync() ? "" : "not")} successful.");
            _logger?.LogDebug($"HomeData send data {(await _homeDataValues.UpdateValuesAsync() ? "" : "not")} successful.");
            _logger?.LogDebug($"Wallbox send data {(await _wallboxValues.UpdateValuesAsync() ? "" : "not")} successful.");
        }

        /// <summary>
        /// Helper method to copy the file from the install folder to the local folder.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CopyAppSettings()
        {
            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var source = await Package.Current.InstalledLocation.GetFileAsync("appsettings.json");
                var target = await localFolder.TryGetItemAsync("appsettings.json");

                if (source != null)
                {
                    if (target == null)
                    {
                        await source.CopyAsync(localFolder, "appsettings.json");
                    }
                    else
                    {
                        var targetproperties = await target.GetBasicPropertiesAsync();
                        var sourceproperties = await source.GetBasicPropertiesAsync();

                        if (targetproperties.DateModified < sourceproperties.DateModified)
                        {
                            await source.CopyAsync(localFolder, "appsettings.json", NameCollisionOption.ReplaceExisting);
                        }
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        #endregion
    }
}
