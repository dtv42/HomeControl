// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoApp.Commands
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
    using ZipatoLib;
    using ZipatoApp.Models;

    #endregion

    /// <summary>
    /// Application command "monitor".
    /// </summary>
    [Command(Name = "monitor",
             FullName = "Zipato Monitor Command",
             Description = "Monitoring data values from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class MonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IZipato _zipato;

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
            get => !(OptionA || OptionV);
        }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Monitor Zipato data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-v|--values", "Monitor Zipato values.", CommandOptionType.NoValue)]
        public bool OptionV { get; set; }

        [Option("-n|--number <number>", "Sets the number of iterations (default: 1).", CommandOptionType.SingleValue)]
        public int Number { get; set; } = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public MonitorCommand(IZipato zipato,
                           ILogger<MonitorCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("MonitorCommand()");

            // Setting the Zipato instance.
            _zipato = zipato;
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
                    // Overriding Zipato options.
                    _zipato.BaseAddress = Parent.BaseAddress;
                    _zipato.Timeout = Parent.Timeout;
                    _zipato.User = Parent.User;
                    _zipato.Password = Parent.Password;
                    _zipato.IsLocal = Parent.IsLocal;

                    _zipato.Data.PropertyChanged += OnDataPropertyChanged;
                    _zipato.Devices.PropertyChanged += OnDevicesPropertyChanged;

                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    Console.WriteLine("Monitoring Zipato has started. Ctrl-C to end");

                    try
                    {
                        var source = new CancellationTokenSource();
                        var cancellationToken = source.Token;

                        await Task.Factory.StartNew(async () =>
                        {
                            if (OptionA) await _zipato?.ReadAllAsync();
                            if (OptionV) await _zipato?.ReadAllValuesAsync();

                            // Wait if necessary to reach 60 seconds.
                            var start = DateTime.UtcNow;
                            double delay = 60.0 - start.Second - start.Millisecond / 1000.0;
                            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    _logger?.LogDebug("ZipatoMonitor: Update data...");
                                    if (OptionA) await _zipato?.ReadAllAsync();
                                    if (OptionV) await _zipato?.ReadAllValuesAsync();
                                }
                                catch (Exception ex)
                                {
                                    _logger?.LogWarning(ex, "ZipatoMonitor: Exception");
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
                            Console.WriteLine($"Monitoring Zipato cancelled.");
                            _closing.Set();
                        });

                        _closing.WaitOne();
                    }
                    catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                    {
                        Console.WriteLine($"Monitoring Zipato cancelled.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Monitoring Zipato cancelled.");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception MonitorCommand.");
                    return -1;
                }
                finally
                {
                    _zipato.EndSession();
                }
            }

            return 0;
        }

        #endregion

        #region Private Methods

        private void OnDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    if (OptionA)
                    {
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(_zipato.Data, Formatting.Indented)}");
                    }
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDevicesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    if (OptionV)
                    {
                        Console.WriteLine($"Values: {JsonConvert.SerializeObject(_zipato.Devices, Formatting.Indented)}");
                    }
                    break;
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

            if (OptionA && OptionV)
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            return true;
        }

        #endregion
    }
}
