// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlApp.Commands
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
    using BControlLib;
    using BControlApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "BControl Monitor Command",
             Description = "Monitoring data values from TQ energy meter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IBControl _bcontrol;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionI || OptionE || OptionP || OptionS); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-i|--internal", "Monitors the internal data.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-e|--energy", "Monitors the energy data.", CommandOptionType.NoValue)]
        public bool OptionE { get; set; }

        [Option("-p|--pnp", "Monitors the pnp data.", CommandOptionType.NoValue)]
        public bool OptionP { get; set; }

        [Option("-s|--sunspec", "Monitors the SunSpec data.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        [Option("--monitor", "The remote monitor url.", CommandOptionType.SingleValue)]
        public string Monitor { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="bcontrol">The BControl instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IBControl bcontrol,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the BControl instance.
            _bcontrol = bcontrol;
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
                    // Overriding BControl options.
                    _bcontrol.TcpSlave.Address = Parent.Address;
                    _bcontrol.TcpSlave.Port = Parent.Port;
                    _bcontrol.TcpSlave.ID = Parent.SlaveID;

                    _bcontrol.Data.PropertyChanged += OnDataPropertyChanged;
                    _bcontrol.InternalData.PropertyChanged += OnInternalDataPropertyChanged;
                    _bcontrol.EnergyData.PropertyChanged += OnEnergyDataPropertyChanged;
                    _bcontrol.PnPData.PropertyChanged += OnPnPDataPropertyChanged;
                    _bcontrol.SunSpecData.PropertyChanged += OnSunSpecDataPropertyChanged;

                    Console.WriteLine("Monitoring BControl has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            await _bcontrol?.ReadAllAsync();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("BControlMonitor: Update data...");
                                    var status = await _bcontrol?.ReadAllAsync();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "BControlMonitor: Exception");
                                }

                                // Wait if necessary to reach 60 seconds.
                                var time = DateTime.Now;
                                delay = 60.0 - time.Second - time.Millisecond / 1000.0;
                                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
                            }

                        }, cancellationToken);

                        Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
                        {
                            Console.WriteLine($"Monitoring BControl cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring BControl cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring BControl cancelled.");
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
                Console.WriteLine($"BControlData: {JsonConvert.SerializeObject(_bcontrol.Data, Formatting.Indented)}");
            }
        }

        private void OnInternalDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionI)
            {
                Console.WriteLine($"InternalData: {JsonConvert.SerializeObject(_bcontrol.InternalData, Formatting.Indented)}");
            }
        }

        private void OnEnergyDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionE)
            {
                Console.WriteLine($"EnergyData: {JsonConvert.SerializeObject(_bcontrol.EnergyData, Formatting.Indented)}");
            }
        }

        private void OnPnPDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionP)
            {
                Console.WriteLine($"PnPData: {JsonConvert.SerializeObject(_bcontrol.PnPData, Formatting.Indented)}");
            }
        }

        private void OnSunSpecDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionS)
            {
                Console.WriteLine($"SunSpecData: {JsonConvert.SerializeObject(_bcontrol.SunSpecData, Formatting.Indented)}");
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

            if (OptionA && (OptionI || OptionE || OptionP || OptionS))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
