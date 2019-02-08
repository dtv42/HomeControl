// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MApp.Commands
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
    using SYMO823MLib;
    using SYMO823MApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "SYMO823M Monitor Command",
             Description = "Monitoring data values from Fronius Symo 8.2-3-M inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private ISYMO823M _symo823m;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionA || OptionD || OptionI || OptionN ||
                     OptionS || OptionX || OptionC || OptionM || OptionF);
        }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-d|--device", "Monitors the device common model data.", CommandOptionType.NoValue)]
        public bool OptionD { get; set; }

        [Option("-i|--inverter", "Monitors the inverter model data.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-n|--nameplate", "Monitors the nameplate model data.", CommandOptionType.NoValue)]
        public bool OptionN { get; set; }

        [Option("-s|--settings", "Monitors the basic settings model data.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        [Option("-x|--extended", "Monitors the extended measurements & status model data.", CommandOptionType.NoValue)]
        public bool OptionX { get; set; }

        [Option("-c|--control", "Monitors the immediate control model data.", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-m|--multiple", "Monitors the multiple MPPT inverter extension model data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-f|--fronius", "Monitors the Fronius register data.", CommandOptionType.NoValue)]
        public bool OptionF { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(ISYMO823M symo823m,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the SYMO823M instance.
            _symo823m = symo823m;
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
                    // Overriding SYMO823M options.
                    _symo823m.Slave.Address = Parent.Address;
                    _symo823m.Slave.Port = Parent.Port;
                    _symo823m.Slave.ID = Parent.SlaveID;

                    _symo823m.Data.PropertyChanged += OnDataPropertyChanged;
                    _symo823m.CommonModel.PropertyChanged += OnCommonModelPropertyChanged;
                    _symo823m.InverterModel.PropertyChanged += OnInverterModelPropertyChanged;
                    _symo823m.NameplateModel.PropertyChanged += OnNameplateModelPropertyChanged;
                    _symo823m.SettingsModel.PropertyChanged += OnSettingsModelPropertyChanged;
                    _symo823m.ExtendedModel.PropertyChanged += OnExtendedModelPropertyChanged;
                    _symo823m.ControlModel.PropertyChanged += OnControlModelPropertyChanged;
                    _symo823m.MultipleModel.PropertyChanged += OnMultipleModelPropertyChanged;
                    _symo823m.FroniusRegister.PropertyChanged += OnFroniusRegisterPropertyChanged;

                    Console.WriteLine("Monitoring SYMO823M has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            await _symo823m?.ReadBlockAsync();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("SYMO823MMonitor: Update data...");
                                    var status = await _symo823m?.ReadBlockAsync();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "SYMO823MMonitor: Exception");
                                }

                                // Wait if necessary to reach 60 seconds.
                                var time = DateTime.Now;
                                delay = 60.0 - time.Second - time.Millisecond / 1000.0;
                                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
                            }

                        }, cancellationToken);

                        Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
                        {
                            Console.WriteLine($"Monitoring SYMO823M cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring SYMO823M cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring SYMO823M cancelled.");
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

        private void OnDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionA)
            {
                Console.WriteLine($"SYMO823MData: {JsonConvert.SerializeObject(_symo823m.Data, Formatting.Indented)}");
            }
        }

        private void OnCommonModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionD)
            {
                Console.WriteLine($"CommonModel: {JsonConvert.SerializeObject(_symo823m.CommonModel, Formatting.Indented)}");
            }
        }

        private void OnInverterModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionI)
            {
                Console.WriteLine($"InverterModel: {JsonConvert.SerializeObject(_symo823m.InverterModel, Formatting.Indented)}");
            }
        }

        private void OnNameplateModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionN)
            {
                Console.WriteLine($"NameplateModel: {JsonConvert.SerializeObject(_symo823m.NameplateModel, Formatting.Indented)}");
            }
        }

        private void OnSettingsModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionS)
            {
                Console.WriteLine($"SettingsModel: {JsonConvert.SerializeObject(_symo823m.SettingsModel, Formatting.Indented)}");
            }
        }

        private void OnExtendedModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionX)
            {
                Console.WriteLine($"ExtendedModel: {JsonConvert.SerializeObject(_symo823m.ExtendedModel, Formatting.Indented)}");
            }
        }

        private void OnControlModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionC)
            {
                Console.WriteLine($"ControlModel: {JsonConvert.SerializeObject(_symo823m.ControlModel, Formatting.Indented)}");
            }
        }
        private void OnMultipleModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionM)
            {
                Console.WriteLine($"MultipleModel: {JsonConvert.SerializeObject(_symo823m.MultipleModel, Formatting.Indented)}");
            }
        }

        private void OnFroniusRegisterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionF)
            {
                Console.WriteLine($"FroniusRegister: {JsonConvert.SerializeObject(_symo823m.FroniusRegister, Formatting.Indented)}");
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

            if (OptionA && (OptionD || OptionI || OptionN || OptionS ||
                            OptionX || OptionC || OptionM || OptionF))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
