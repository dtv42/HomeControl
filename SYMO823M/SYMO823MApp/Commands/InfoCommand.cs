// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using SYMO823MLib;
    using SYMO823MApp.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "SYMO823M Info Command",
             Description = "Reading data values from Fronius Symo 8.2-3-M inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly ISYMO823M _symo823m;

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

        [Option("-a|--all", "Gets all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-d|--device", "Gets the device common model data.", CommandOptionType.NoValue)]
        public bool OptionD { get; set; }

        [Option("-i|--inverter", "Gets the inverter model data.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-n|--nameplate", "Gets the nameplate model data.", CommandOptionType.NoValue)]
        public bool OptionN { get; set; }

        [Option("-s|--settings", "Gets the basic settings model data.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        [Option("-x|--extended", "Gets the extended measurements & status model data.", CommandOptionType.NoValue)]
        public bool OptionX { get; set; }

        [Option("-c|--control", "Gets the immediate control model data.", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-m|--multiple", "Gets the multiple MPPT inverter extension model data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-f|--fronius", "Gets the Fronius register data.", CommandOptionType.NoValue)]
        public bool OptionF { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(ISYMO823M symo823m,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

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
            try
            {
                if (CheckOptions(app))
                {
                    // Overriding SYMO823M options.
                    _symo823m.Slave.Address = Parent.Address;
                    _symo823m.Slave.Port = Parent.Port;
                    _symo823m.Slave.ID = Parent.SlaveID;

                    await _symo823m.ReadBlockAsync();

                    if (OptionA)
                    {
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(_symo823m.Data, Formatting.Indented)}");
                    }

                    if (OptionD)
                    {
                        Console.WriteLine($"C001: {JsonConvert.SerializeObject(_symo823m.CommonModel, Formatting.Indented)}");
                    }

                    if (OptionI)
                    {
                        Console.WriteLine($"I113: {JsonConvert.SerializeObject(_symo823m.InverterModel, Formatting.Indented)}");
                    }

                    if (OptionN)
                    {
                        Console.WriteLine($"IC120: {JsonConvert.SerializeObject(_symo823m.NameplateModel, Formatting.Indented)}");
                    }

                    if (OptionS)
                    {
                        Console.WriteLine($"IC121: {JsonConvert.SerializeObject(_symo823m.SettingsModel, Formatting.Indented)}");
                    }

                    if (OptionX)
                    {
                        Console.WriteLine($"IC122: {JsonConvert.SerializeObject(_symo823m.ExtendedModel, Formatting.Indented)}");
                    }

                    if (OptionC)
                    {
                        Console.WriteLine($"IC123: {JsonConvert.SerializeObject(_symo823m.ControlModel, Formatting.Indented)}");
                    }

                    if (OptionM)
                    {
                        Console.WriteLine($"I160: {JsonConvert.SerializeObject(_symo823m.MultipleModel, Formatting.Indented)}");
                    }

                    if (OptionF)
                    {
                        Console.WriteLine($"Fronius: {JsonConvert.SerializeObject(_symo823m.FroniusRegister, Formatting.Indented)}");
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
