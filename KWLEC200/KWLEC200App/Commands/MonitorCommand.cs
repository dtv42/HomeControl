// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200App.Commands
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
    using KWLEC200Lib;
    using KWLEC200App.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "KWLEC200 Monitor Command",
             Description = "Monitoring data values from KWL 200 EC ventilation unit.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IKWLEC200 _kwlec200;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionO); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-o|--overview", "Monitors the overview data.", CommandOptionType.NoValue)]
        public bool OptionO { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="kwlec200">The KWLEC200 instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IKWLEC200 kwlec200,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the KWLEC200 instance.
            _kwlec200 = kwlec200;
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
                    // Overriding KWLEC200 options.
                    _kwlec200.Slave.Address = Parent.Address;
                    _kwlec200.Slave.Port = Parent.Port;
                    _kwlec200.Slave.ID = Parent.SlaveID;

                    _kwlec200.Data.PropertyChanged += OnDataPropertyChanged;
                    _kwlec200.OverviewData.PropertyChanged += OnOverviewDataPropertyChanged;

                    Console.WriteLine("Monitoring KWLEC200 has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            _kwlec200?.ReadAll();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("KWLEC200Monitor: Update data...");
                                    var status = _kwlec200?.ReadOverviewData();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "KWLEC200Monitor: Exception");
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
                            Console.WriteLine($"Monitoring KWLEC200 cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring KWLEC200 cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring KWLEC200 cancelled.");
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
                Console.WriteLine($"KWLEC200 Data: {JsonConvert.SerializeObject(_kwlec200.Data, Formatting.Indented)}");
            }
        }

        private void OnOverviewDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (OptionO)
            {
                Console.WriteLine($"Overview Data: {JsonConvert.SerializeObject(_kwlec200.OverviewData, Formatting.Indented)}");
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

            if (OptionA && OptionO)
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
