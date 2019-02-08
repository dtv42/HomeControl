// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
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
    using FroniusLib;
    using FroniusApp.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "Fronius Info Command",
             Description = "Reading data values from Fronius Symo 8.2-3-M solar inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
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
        private bool NoOptions { get => !(OptionC || OptionI || OptionL || OptionM || OptionP); }

        #endregion

        #region Public Properties

        [Option("-c|--common", "Gets the inverter common data.", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-i|--inverter", "Gets the inverter info.", CommandOptionType.NoValue)]
        public bool OptionI{ get; set; }

        [Option("-l|--logger", "Gets the data logger info.", CommandOptionType.NoValue)]
        public bool OptionL { get; set; }

        [Option("-m|--minmax", "Gets the inverter minmax data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-p|--phase", "Gets the inverter phase data.", CommandOptionType.NoValue)]
        public bool OptionP { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IFronius fronius,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

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

                    if (OptionC)
                    {
                        var status = await _fronius.ReadCommonDataAsync();
                        Console.WriteLine($"CommonData: {JsonConvert.SerializeObject(_fronius.CommonData, Formatting.Indented)}");
                    }

                    if (OptionI)
                    {
                        var status = await _fronius.ReadInverterInfoAsync();
                        Console.WriteLine($"InverterInfo: {JsonConvert.SerializeObject(_fronius.InverterInfo, Formatting.Indented)}");
                    }

                    if (OptionL)
                    {
                        var status = await _fronius.ReadLoggerInfoAsync();
                        Console.WriteLine($"LoggerInfo: {JsonConvert.SerializeObject(_fronius.LoggerInfo, Formatting.Indented)}");
                    }

                    if (OptionM)
                    {
                        var status = await _fronius.ReadMinMaxDataAsync();
                        Console.WriteLine($"MinMaxData: {JsonConvert.SerializeObject(_fronius.MinMaxData, Formatting.Indented)}");
                    }

                    if (OptionP)
                    {
                        var status = await _fronius.ReadPhaseDataAsync();
                        Console.WriteLine($"PhaseData: {JsonConvert.SerializeObject(_fronius.PhaseData, Formatting.Indented)}");
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
                _logger?.LogError($"Select an info option.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
