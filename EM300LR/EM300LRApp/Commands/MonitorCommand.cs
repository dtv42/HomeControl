// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRApp.Commands
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using EM300LRLib;
    using EM300LRApp.Models;

    #endregion Using Directives

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "EM300LR Monitor Command",
             Description = "Monitoring data values from b-Control EM300LR energy manager.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IEM300LR _em300lr;

        #endregion Private Data Members

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionT || Option1 || Option2 || Option3); }

        #endregion Private Properties

        #region Public Properties

        [Option("-a|--all", "Monitors all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-t|--total", "Monitors the common data.", CommandOptionType.NoValue)]
        public bool OptionT { get; set; }

        [Option("-1|--phase1", "Monitors the inverter data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--phase2", "Monitors the logger data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--phase3", "Monitors the minmax data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="em300lr">The EM300LR instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IEM300LR em300lr,
                              ILogger<MonitorCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the EM300LR instance.
            _em300lr = em300lr;
        }

        #endregion Constructors

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
                    // Overriding EM300LR options.
                    _em300lr.BaseAddress = Parent.BaseAddress;
                    _em300lr.Timeout = Parent.Timeout;
                    _em300lr.Password = Parent.Password;
                    _em300lr.SerialNumber = Parent.SerialNumber;

                    _em300lr.Data.PropertyChanged += OnDataPropertyChanged;
                    _em300lr.TotalData.PropertyChanged += OnTotalDataPropertyChanged;
                    _em300lr.Phase1Data.PropertyChanged += OnPhase1DataPropertyChanged;
                    _em300lr.Phase2Data.PropertyChanged += OnPhase2DataPropertyChanged;
                    _em300lr.Phase3Data.PropertyChanged += OnPhase3DataPropertyChanged;

                    Console.WriteLine("Monitoring EM300LR has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () =>
                        {
                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("EM300LRMonitor: Update data...");
                                    var status = await _em300lr?.ReadAllAsync();

                                    if (status.IsBad || status.IsUncertain)
                                    {
                                        Console.WriteLine($"Error reading data from EM300LR energy manager.");
                                        Console.WriteLine($"Reason: {status.Explanation}.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "EM300LRMonitor: Exception");
                                }

                                if (--Number <= 0)
                                {
                                    _closing.Set();
                                }

                                // Wait if necessary to reach 60 seconds.
                                var time = DateTime.Now;
                                var delay = 60.0 - time.Second - time.Millisecond / 1000.0;
                                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
                            }
                        }, cancellationToken);

                        Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
                        {
                            Console.WriteLine($"Monitoring EM300LR cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring EM300LR cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring EM300LR cancelled.");
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

        #endregion Public Methods

        #region Private Methods

        private void OnDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionA)
            {
                if (_em300lr.Data.IsGood)
                {
                    Console.WriteLine($"EM300LRData: {JsonConvert.SerializeObject(_em300lr.Data, Formatting.Indented)}");
                }
                else
                {
                    Console.WriteLine($"Error in EM300LR energy manager data.");
                    Console.WriteLine($"Reason: {_em300lr.Data.Status.Explanation}.");
                }
            }
        }

        private void OnTotalDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (OptionT)
            {
                if (_em300lr.Data.IsGood)
                {
                    Console.WriteLine($"TotalData: {JsonConvert.SerializeObject(_em300lr.TotalData, Formatting.Indented)}");
                }
                else
                {
                    Console.WriteLine($"Error in EM300LR energy manager data.");
                    Console.WriteLine($"Reason: {_em300lr.Data.Status.Explanation}.");
                }
            }
        }

        private void OnPhase1DataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Option1)
            {
                if (_em300lr.Data.IsGood)
                {
                    Console.WriteLine($"Phase1Data: {JsonConvert.SerializeObject(_em300lr.Phase1Data, Formatting.Indented)}");
                }
                else
                {
                    Console.WriteLine($"Error in EM300LR energy manager data.");
                    Console.WriteLine($"Reason: {_em300lr.Data.Status.Explanation}.");
                }
            }
        }

        private void OnPhase2DataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Option2)
            {
                if (_em300lr.Data.IsGood)
                {
                    Console.WriteLine($"Phase2Data: {JsonConvert.SerializeObject(_em300lr.Phase2Data, Formatting.Indented)}");
                }
                else
                {
                    Console.WriteLine($"Error in EM300LR energy manager data.");
                    Console.WriteLine($"Reason: {_em300lr.Data.Status.Explanation}.");
                }
            }
        }

        private void OnPhase3DataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Option3)
            {
                if (_em300lr.Data.IsGood)
                {
                    Console.WriteLine($"Phase3Data: {JsonConvert.SerializeObject(_em300lr.Phase3Data, Formatting.Indented)}");
                }
                else
                {
                    Console.WriteLine($"Error in EM300LR energy manager data.");
                    Console.WriteLine($"Reason: {_em300lr.Data.Status.Explanation}.");
                }
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

            if (OptionA && (OptionT || Option1 || Option2 || Option3))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion Private Methods
    }
}