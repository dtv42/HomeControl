// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusApp.Commands
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
    using DataValueLib;
    using FroniusLib;
    using FroniusLib.Models;
    using FroniusApp.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "Fronius Read Command",
             Description = "Reading data values from Fronius Symo 8.2-3-M solar inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IFronius _fronius;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionC || OptionI || OptionL || OptionM || OptionP); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Reads all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-c|--common", "Reads the inverter common data.", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-i|--inverter", "Reads the inverter info.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-l|--logger", "Reads the data logger info.", CommandOptionType.NoValue)]
        public bool OptionL { get; set; }

        [Option("-m|--minmax", "Reads the inverter minmax data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-p|--phase", "Reads the inverter phase data.", CommandOptionType.NoValue)]
        public bool OptionP { get; set; }

        [Argument(0, "Reads the named property.")]
        public string Property { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IFronius fronius,
                           ILogger<ReadCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

            // Setting the Fronius instance.
            _fronius = fronius;
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
                    // Overriding Fronius options.
                    _fronius.BaseAddress = Parent.BaseAddress;
                    _fronius.Timeout = Parent.Timeout;
                    _fronius.DeviceID = Parent.DeviceID;

                    if (Property.Length > 0)
                    {
                        Console.WriteLine($"Reading property '{Property}' from Fronius solar inverter.");
                        var status = await _fronius.ReadAllAsync();

                        if (status != DataValue.Good)
                        {
                            Console.WriteLine($"Error reading property '{Property}' from Fronius solar inverter.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                        else
                        {
                            Console.WriteLine($"Value of property '{Property}' = {JsonConvert.SerializeObject(_fronius.GetPropertyValue(Property), Formatting.Indented)}");
                        }
                    }
                    else
                    {
                        if (OptionA)
                        {
                            var status = await _fronius.ReadAllAsync();
                            Console.WriteLine($"FroniusData: {JsonConvert.SerializeObject(_fronius.Data, Formatting.Indented)}");
                        }
                        else
                        {
                            if (OptionC)
                            {
                                var status = await _fronius.ReadCommonDataAsync();
                                Console.WriteLine($"CommonData: {JsonConvert.SerializeObject(_fronius.Data.CommonData, Formatting.Indented)}");
                            }

                            if (OptionI)
                            {
                                var status = await _fronius.ReadInverterInfoAsync();
                                Console.WriteLine($"InverterInfo: {JsonConvert.SerializeObject(_fronius.Data.InverterInfo, Formatting.Indented)}");
                            }

                            if (OptionL)
                            {
                                var status = await _fronius.ReadLoggerInfoAsync();
                                Console.WriteLine($"LoggerInfo: {JsonConvert.SerializeObject(_fronius.LoggerInfo, Formatting.Indented)}");
                            }

                            if (OptionM)
                            {
                                var status = await _fronius.ReadMinMaxDataAsync();
                                Console.WriteLine($"MinMaxData: {JsonConvert.SerializeObject(_fronius.Data.MinMaxData, Formatting.Indented)}");
                            }

                            if (OptionP)
                            {
                                var status = await _fronius.ReadPhaseDataAsync();
                                Console.WriteLine($"PhaseData: {JsonConvert.SerializeObject(_fronius.Data.PhaseData, Formatting.Indented)}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception ReadCommand.");
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
            if (NoOptions && Property.Length == 0)
            {
                Console.WriteLine($"Select a data option or specify a property.");
                return false;
            }

            if (OptionA && (OptionC || OptionI || OptionL || OptionM || OptionP))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            if (Property.Length > 0)
            {
                if (!NoOptions)
                {
                    Console.WriteLine($"Reading the property '{Property}' - options are ignored.");
                }

                if (!Fronius.IsProperty(Property))
                {
                    _logger?.LogError($"The property '{Property}' has not been found.");
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
