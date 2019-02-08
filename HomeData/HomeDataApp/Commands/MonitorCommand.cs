// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataApp.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using DataValueLib;
    using HomeDataLib;
    using HomeDataApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "HomeData Monitor Command",
             Description = "Monitoring data values from home control system.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IHomeData _homedata;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        #endregion

        #region Public Properties

        [Option("-u|--update", "Use updates reading the data.", CommandOptionType.NoValue)]
        public bool OptionU { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="homedata">The HomeData instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IHomeData homedata,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the HomeData instance.
            _homedata = homedata;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                if (CheckOptions(app))
                {
                    try
                    {
                        // Overriding HomeData options.
                        _homedata.Timeout = Parent.Timeout;
                        _homedata.Meter1Address = Parent.Meter1Address;
                        _homedata.Meter2Address = Parent.Meter2Address;

                        _homedata.Data.PropertyChanged += OnDataPropertyChanged;

                        Console.WriteLine("Monitoring HomeData has started. Ctrl-C to end");

                        try
                        {
                            var source = new CancellationTokenSource();
                            var cancellationToken = source.Token;

                            await Task.Factory.StartNew(async () => {
                                await _homedata.ReadAllAsync(OptionU);

                                // Wait if necessary to reach 60 seconds.
                                var start = DateTime.UtcNow;
                                double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    try
                                    {
                                        _logger?.LogDebug("HomeDataMonitor: Update data...");
                                        await _homedata?.ReadAllAsync(OptionU);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger?.LogWarning(ex, "HomeDataMonitor: Exception");
                                    }

                                    if (--Number <= 0)
                                    {
                                        _closing.Set();
                                    }

                                    // Wait if necessary to reach 60 seconds.
                                    var time = DateTime.Now;
                                    delay = 60.0 - time.Second - time.Millisecond / 1000.0;
                                    await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
                                }

                            }, cancellationToken);

                            Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
                            {
                                Console.WriteLine($"Monitoring HomeData cancelled.");
                                _closing.Set();
                            });

                            _closing.WaitOne();
                        }
                        catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                        {
                            Console.WriteLine($"Monitoring HomeData cancelled.");
                        }
                        catch (OperationCanceledException)
                        {
                            Console.WriteLine($"Monitoring HomeData cancelled.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, $"Exception MonitorCommand.");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception MonitorCommand.");
                return -1;
            }

            return 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"Home Data: {JsonConvert.SerializeObject(_homedata.Data, Formatting.Indented)}");
        }

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            return true;
        }

        #endregion
    }
}
