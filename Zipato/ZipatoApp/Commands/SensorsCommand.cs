// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SensorsCommand.cs" company="DTV-Online">
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
    using System.Linq;
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
    /// Application command "sensors".
    /// </summary>
    [Command(Name = "sensors",
             FullName = "Zipato Sensors Command",
             Description = "Accessing sensors (and meters) from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    [Subcommand("meter", typeof(MeterCommand))]
    [Subcommand("virtual", typeof(VirtualCommand))]
    [Subcommand("temperature", typeof(TemperatureCommand))]
    [Subcommand("humidity", typeof(HumidityCommand))]
    [Subcommand("luminance", typeof(LuminanceCommand))]
    public class SensorsCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionIndex { get; set; }
        private bool OptionName { get; set; }
        private bool OptionUuid { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionIndex || OptionName || OptionUuid); }

        #endregion

        #region Public Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        public RootCommand Parent { get; set; }

        [Option("--index <number>", "Using sensor index.", CommandOptionType.SingleValue, Inherited = true)]
        public int Index { get; set; }

        [Option("--name <string>", "Using sensor name.", CommandOptionType.SingleValue, Inherited = true)]
        public string Name { get; set; }

        [Uuid]
        [Option("--uuid <uuid>", "Using sensor uuid.", CommandOptionType.SingleValue, Inherited = true)]
        public string Uuid { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorsCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public SensorsCommand(IZipato zipato,
                              ILogger<SensorsCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("SensorsCommand()");

            // Setting the Zipato instance.
            _zipato = zipato;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecute(CommandLineApplication app)
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
                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    await _zipato.ReadAllDataAsync();

                    if (NoOptions)
                    {
                        Console.WriteLine($"Zipato Sensors: {JsonConvert.SerializeObject(_zipato.Sensors, Formatting.Indented)}");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception SensorsCommand.");
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

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                switch (option.LongName)
                {
                    case "index": OptionIndex = option.HasValue(); break;
                    case "name": OptionName = option.HasValue(); break;
                    case "uuid": OptionUuid = option.HasValue(); break;
                }
            }

            if (!NoOptions)
            {
                Console.WriteLine("Select a command: 'meter', 'virtual', 'temperature', 'humidity', or 'luminance'.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
