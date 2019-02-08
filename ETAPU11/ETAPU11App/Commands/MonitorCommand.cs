// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11App.Commands
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using ETAPU11Lib;
    using ETAPU11App.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "ETAPU11 Monitor Command",
             Description = "Monitoring data values from ETA PU 11 pellet boiler.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IETAPU11 _etapu11;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionB || OptionW || OptionH || OptionS || OptionY); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-b|--boiler", "Monitors the boiler data.", CommandOptionType.NoValue)]
        public bool OptionB { get; set; }

        [Option("-w|--hotwater", "Monitors the hot water data.", CommandOptionType.NoValue)]
        public bool OptionW { get; set; }

        [Option("-h|--heating", "Monitors the heating data.", CommandOptionType.NoValue)]
        public bool OptionH { get; set; }

        [Option("-s|--storage", "Monitors the storage data.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        [Option("-y|--system", "Monitors the system data.", CommandOptionType.NoValue)]
        public bool OptionY { get; set; }

        [Option("--monitor", "The remote monitor url.", CommandOptionType.SingleValue)]
        public string Monitor { get; set; } = string.Empty;

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="etapu11">The ETAPU11 instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IETAPU11 etapu11,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the ETAPU11 instance.
            _etapu11 = etapu11;
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
                    // Overriding ETAPU11 options.
                    _etapu11.TcpSlave.Address = Parent.Address;
                    _etapu11.TcpSlave.Port = Parent.Port;
                    _etapu11.TcpSlave.ID = Parent.SlaveID;

                    _etapu11.Data.PropertyChanged += OnDataPropertyChanged;
                    _etapu11.BoilerData.PropertyChanged += OnBoilerDataPropertyChanged;
                    _etapu11.HotwaterData.PropertyChanged += OnHotwaterDataPropertyChanged;
                    _etapu11.HeatingData.PropertyChanged += OnHeatingDataPropertyChanged;
                    _etapu11.StorageData.PropertyChanged += OnStorageDataPropertyChanged;
                    _etapu11.SystemData.PropertyChanged += OnSystemDataPropertyChanged;

                    Console.WriteLine("Monitoring ETAPU11 has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            await _etapu11?.ReadAllAsync();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("ETAPU11Monitor: Update data...");
                                    var status = await _etapu11?.ReadAllAsync();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "ETAPU11Monitor: Exception");
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
                            Console.WriteLine($"Monitoring ETAPU11 cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring ETAPU11 cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring ETAPU11 cancelled.");
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

        private void OnDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionA)
            {
                Console.WriteLine($"ETAPU11Data: {JsonConvert.SerializeObject(_etapu11.Data, Formatting.Indented)}");
            }
        }

        private void OnBoilerDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionB)
            {
                Console.WriteLine($"BoilerData: {JsonConvert.SerializeObject(_etapu11.BoilerData, Formatting.Indented)}");
            }
        }

        private void OnHotwaterDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionW)
            {
                Console.WriteLine($"HotwaterData: {JsonConvert.SerializeObject(_etapu11.HotwaterData, Formatting.Indented)}");
            }
        }

        private void OnHeatingDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionH)
            {
                Console.WriteLine($"HeatingData: {JsonConvert.SerializeObject(_etapu11.HeatingData, Formatting.Indented)}");
            }
        }

        private void OnStorageDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionS)
            {
                Console.WriteLine($"StorageData: {JsonConvert.SerializeObject(_etapu11.StorageData, Formatting.Indented)}");
            }
        }

        private void OnSystemDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionY)
            {
                Console.WriteLine($"SystemData: {JsonConvert.SerializeObject(_etapu11.SystemData, Formatting.Indented)}");
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

            if (OptionA && (OptionB || OptionW || OptionH || OptionS || OptionY))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
