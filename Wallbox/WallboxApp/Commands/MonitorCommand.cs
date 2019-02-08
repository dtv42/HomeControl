// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxApp.Commands
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
    using WallboxLib;
    using WallboxApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "Wallbox Monitor Command",
             Description = "Monitoring data values from BMW Wallbox charging station.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IWallbox _wallbox;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || Option1 || Option2 || Option3 || OptionL || OptionR); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-1|--report1", "Monitors the report 1 data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--report2", "Monitors the report 2 data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--report3", "Monitors the report 3 data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        [Option("-l|--last", "Monitors the last charging report data.", CommandOptionType.NoValue)]
        public bool OptionL { get; set; }

        [Option("-r|--reports", "Monitors the charging reports data.", CommandOptionType.NoValue)]
        public bool OptionR { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IWallbox wallbox,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the Wallbox instance.
            _wallbox = wallbox;
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
                    // Overriding Wallbox options.
                    _wallbox.HostName = Parent.HostName;
                    _wallbox.Port = Parent.Port;

                    _wallbox.Data.PropertyChanged += OnDataPropertyChanged;
                    _wallbox.Report1.PropertyChanged += OnReport1PropertyChanged;
                    _wallbox.Report2.PropertyChanged += OnReport2PropertyChanged;
                    _wallbox.Report3.PropertyChanged += OnReport3PropertyChanged;
                    _wallbox.Report100.PropertyChanged += OnReport100PropertyChanged;

                    Console.WriteLine("Monitoring Wallbox has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () => {
                            if (OptionA) await _wallbox?.ReadAllAsync();
                            if (Option1) await _wallbox?.ReadReport1Async();
                            if (Option2) await _wallbox?.ReadReport2Async();
                            if (Option3) await _wallbox?.ReadReport3Async();
                            if (OptionL) await _wallbox?.ReadReport100Async();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("WallboxMonitor: Update data...");
                                    if (OptionA) await _wallbox?.ReadAllAsync();
                                    if (Option1) await _wallbox?.ReadReport1Async();
                                    if (Option2) await _wallbox?.ReadReport2Async();
                                    if (Option3) await _wallbox?.ReadReport3Async();
                                    if (OptionL) await _wallbox?.ReadReport100Async();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "WallboxMonitor: Exception");
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
                            Console.WriteLine($"Monitoring Wallbox cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring Wallbox cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring Wallbox cancelled.");
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
                Console.WriteLine($"Wallbox Data: {JsonConvert.SerializeObject(_wallbox.Data, Formatting.Indented)}");
            }
        }

        private void OnReport1PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Option1)
            {
                Console.WriteLine($"Report 1 Data: {JsonConvert.SerializeObject(_wallbox.Report1, Formatting.Indented)}");
            }
        }

        private void OnReport2PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Option2)
            {
                Console.WriteLine($"Report 2 Data: {JsonConvert.SerializeObject(_wallbox.Report2, Formatting.Indented)}");
            }
        }

        private void OnReport3PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Option3)
            {
                Console.WriteLine($"Report 3 Data: {JsonConvert.SerializeObject(_wallbox.Report3, Formatting.Indented)}");
            }
        }

        private void OnReport100PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionL)
            {
                Console.WriteLine($"Report 100 Data: {JsonConvert.SerializeObject(_wallbox.Report100, Formatting.Indented)}");
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

            if (OptionA && (Option1 || Option2 || Option3 || OptionL || OptionR))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
