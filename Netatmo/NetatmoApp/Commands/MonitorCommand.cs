// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoApp.Commands
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;
    using McMaster.Extensions.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using NetatmoLib;
    using NetatmoApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "Netatmo Monitor Command",
             Description = "Monitoring data values from Netatmo weather station.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly INetatmo _netatmo;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionM || OptionO || Option1 || Option2 || Option3 || OptionR || OptionW); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-m|--main", "Get the main module data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-o|--outdoor", "Get the outdoor module data.", CommandOptionType.NoValue)]
        public bool OptionO { get; set; }

        [Option("-1|--indoor1", "Get the indoor module 1 data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--indoor2", "Get the indoor module 2 data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--indoor3", "Get the indoor module 3 data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        [Option("-r|--rain", "Get the rain gauge data.", CommandOptionType.NoValue)]
        public bool OptionR { get; set; }

        [Option("-w|--wind", "Get the wind gauge data.", CommandOptionType.NoValue)]
        public bool OptionW { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(INetatmo netatmo,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the Netatmo instance.
            _netatmo = netatmo;
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            if (CheckOptions(app))
            {
                try
                {
                    // Overriding Netatmo options.
                    _netatmo.BaseAddress = Parent.BaseAddress;
                    _netatmo.Timeout = Parent.Timeout;

                    _netatmo.Station.PropertyChanged += OnStationPropertyChanged;

                    Console.WriteLine("Monitoring Netatmo has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            await _netatmo?.ReadAllAsync();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("NetatmoMonitor: Update data...");
                                    await _netatmo?.ReadAllAsync();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "NetatmoMonitor: Exception");
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
                            Console.WriteLine($"Monitoring Netatmo cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring Netatmo cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring Netatmo cancelled.");
                    }

                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception MonitorCommand.");
                    return -1;
                }
            }

            return 0;
        }

        #endregion

        #region Private Methods

        private void OnStationPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionA)
            {
                Console.WriteLine($"Netatmo Data: {JsonConvert.SerializeObject(_netatmo.Station, Formatting.Indented)}");
            }

            if (OptionM)
            {
                Console.WriteLine($"Main Module: {JsonConvert.SerializeObject(_netatmo.Station.Device.DashboardData, Formatting.Indented)}");
            }

            if (OptionO)
            {
                Console.WriteLine($"Outdoor Module: {JsonConvert.SerializeObject(_netatmo.Station.Device.OutdoorModule.DashboardData, Formatting.Indented)}");
            }

            if (Option1)
            {
                Console.WriteLine($"Indoor Module 1: {JsonConvert.SerializeObject(_netatmo.Station.Device.IndoorModule1.DashboardData, Formatting.Indented)}");
            }

            if (Option2)
            {
                Console.WriteLine($"Indoor Module 2: {JsonConvert.SerializeObject(_netatmo.Station.Device.IndoorModule2.DashboardData, Formatting.Indented)}");
            }

            if (Option3)
            {
                Console.WriteLine($"Indoor Module 3: {JsonConvert.SerializeObject(_netatmo.Station.Device.IndoorModule3.DashboardData, Formatting.Indented)}");
            }

            if (OptionR)
            {
                Console.WriteLine($"Rain Gauge: {JsonConvert.SerializeObject(_netatmo.Station.Device.RainGauge.DashboardData, Formatting.Indented)}");
            }

            if (OptionW)
            {
                Console.WriteLine($"Wind Gauge: {JsonConvert.SerializeObject(_netatmo.Station.Device.WindGauge.DashboardData, Formatting.Indented)}");
            }
        }

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            if (NoOptions)
            {
                Console.WriteLine($"Select an data option.");
                return false;
            }

            if (OptionA && (OptionM || OptionW || OptionO || Option1 || Option2 || Option3 || OptionR || OptionW))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
