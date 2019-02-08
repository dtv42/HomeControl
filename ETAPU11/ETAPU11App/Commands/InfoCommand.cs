// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using ETAPU11Lib;
    using ETAPU11App.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "ETAPU11 Info Command",
             Description = "Reading data values from ETA PU 11 pellet boiler.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

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

        [Option("-a|--all", "Gets all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-b|--boiler", "Gets the boiler data.", CommandOptionType.NoValue)]
        public bool OptionB { get; set; }

        [Option("-w|--hotwater", "Gets the hot water data.", CommandOptionType.NoValue)]
        public bool OptionW { get; set; }

        [Option("-h|--heating", "Gets the heating circuit data.", CommandOptionType.NoValue)]
        public bool OptionH { get; set; }

        [Option("-s|--storage", "Gets the storage data.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        [Option("-y|--system", "Gets the system data.", CommandOptionType.NoValue)]
        public bool OptionY { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="etapu11">The ETAPU11 instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IETAPU11 etapu11,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

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

                    await _etapu11.ReadBlockAsync();

                    if (OptionA)
                    {
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(_etapu11.Data, Formatting.Indented)}");
                    }

                    if (OptionB)
                    {
                        Console.WriteLine($"Boiler: {JsonConvert.SerializeObject(_etapu11.BoilerData, Formatting.Indented)}");
                    }

                    if (OptionW)
                    {
                        Console.WriteLine($"Hotwater: {JsonConvert.SerializeObject(_etapu11.HotwaterData, Formatting.Indented)}");
                    }

                    if (OptionH)
                    {
                        Console.WriteLine($"Heating: {JsonConvert.SerializeObject(_etapu11.HeatingData, Formatting.Indented)}");
                    }

                    if (OptionS)
                    {
                        Console.WriteLine($"Storage: {JsonConvert.SerializeObject(_etapu11.StorageData, Formatting.Indented)}");
                    }

                    if (OptionY)
                    {
                        Console.WriteLine($"System: {JsonConvert.SerializeObject(_etapu11.SystemData, Formatting.Indented)}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception InfoCommand.");
                return -1;
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
            if (NoOptions)
            {
                Console.WriteLine($"Select an info option.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
