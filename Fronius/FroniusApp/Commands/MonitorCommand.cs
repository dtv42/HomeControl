// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusApp.Commands
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
    using FroniusLib;
    using FroniusApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "Fronius Monitor Command",
             Description = "Monitoring data values from Fronius Symo 8.2-3-M solar inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IFronius _fronius;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionC || OptionI || OptionL || OptionM || OptionP); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-c|--common", "Monitors the common data.", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-i|--inverter", "Monitors the inverter data.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-l|--logger", "Monitors the logger data.", CommandOptionType.NoValue)]
        public bool OptionL { get; set; }

        [Option("-m|--minmax", "Monitors the minmax data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-p|--phase", "Monitors the phase data.", CommandOptionType.NoValue)]
        public bool OptionP { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IFronius fronius,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the Fronius instance.
            _fronius = fronius;
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
                    // Overriding Fronius options.
                    _fronius.BaseAddress = Parent.BaseAddress;
                    _fronius.Timeout = Parent.Timeout;
                    _fronius.DeviceID = Parent.DeviceID;

                    _fronius.Data.PropertyChanged += OnDataPropertyChanged;
                    _fronius.CommonData.PropertyChanged += OnCommonDataPropertyChanged;
                    _fronius.InverterInfo.PropertyChanged += OnInverterInfoPropertyChanged;
                    _fronius.LoggerInfo.PropertyChanged += OnLoggerInfoPropertyChanged;
                    _fronius.MinMaxData.PropertyChanged += OnMinMaxDataPropertyChanged;
                    _fronius.PhaseData.PropertyChanged += OnPhaseDataPropertyChanged;

                    Console.WriteLine("Monitoring Fronius has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            if (OptionA) await _fronius?.ReadAllAsync();
                            if (OptionC) await _fronius?.ReadCommonDataAsync();
                            if (OptionI) await _fronius?.ReadInverterInfoAsync();
                            if (OptionL) await _fronius?.ReadLoggerInfoAsync();
                            if (OptionM) await _fronius?.ReadMinMaxDataAsync();
                            if (OptionP) await _fronius?.ReadPhaseDataAsync();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("FroniusMonitor: Update data...");
                                    if (OptionA) await _fronius?.ReadAllAsync();
                                    if (OptionC) await _fronius?.ReadCommonDataAsync();
                                    if (OptionI) await _fronius?.ReadInverterInfoAsync();
                                    if (OptionL) await _fronius?.ReadLoggerInfoAsync();
                                    if (OptionM) await _fronius?.ReadMinMaxDataAsync();
                                    if (OptionP) await _fronius?.ReadPhaseDataAsync();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "FroniusMonitor: Exception");
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
                            Console.WriteLine($"Monitoring Fronius cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring Fronius cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring Fronius cancelled.");
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

        private void OnDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionA)
            {
                Console.WriteLine($"Fronius Data: {JsonConvert.SerializeObject(_fronius.Data, Formatting.Indented)}");
            }
        }

        private void OnCommonDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionC)
            {
                Console.WriteLine($"Common Data: {JsonConvert.SerializeObject(_fronius.CommonData, Formatting.Indented)}");
            }
        }

        private void OnInverterInfoPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionI)
            {
                Console.WriteLine($"Inverter Info: {JsonConvert.SerializeObject(_fronius.InverterInfo, Formatting.Indented)}");
            }
        }

        private void OnLoggerInfoPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionL)
            {
                Console.WriteLine($"Logger Info: {JsonConvert.SerializeObject(_fronius.LoggerInfo, Formatting.Indented)}");
            }
        }

        private void OnMinMaxDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionM)
            {
                Console.WriteLine($"MinMax Data: {JsonConvert.SerializeObject(_fronius.MinMaxData, Formatting.Indented)}");
            }
        }

        private void OnPhaseDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionP)
            {
                Console.WriteLine($"Phase Data: {JsonConvert.SerializeObject(_fronius.PhaseData, Formatting.Indented)}");
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
                Console.WriteLine($"Select a data option.");
                return false;
            }

            if (OptionA && (OptionC || OptionI || OptionL || OptionM || OptionP))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
