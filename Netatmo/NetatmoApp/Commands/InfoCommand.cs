// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoApp.Commands
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
    using NetatmoLib;
    using NetatmoApp.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "Netatmo Info Command",
             Description = "Reading data values from Netatmo weather station.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly INetatmo _netatmo;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionM || OptionO || Option1 || Option2 || Option3 || OptionR || OptionW); }

        #endregion

        #region Public Properties

        [Option("-m|--main", "Get the main module data.", CommandOptionType.NoValue)]
        public bool OptionM { get; set; }

        [Option("-o|--outdoor", "Get the outdoor module data.", CommandOptionType.NoValue)]
        public bool OptionO { get; set; }

        [Option("-1|--indoor1", "Get the indoor module 1 data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--indoor2", "Get the indoor module 2 data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--indoor3", "Get the indoor module 3 data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        [Option("-r|--rain", "Get the rain gauge data.", CommandOptionType.NoValue)]
        public bool OptionR { get; set; }

        [Option("-w|--wind", "Get the wind gauge data.", CommandOptionType.NoValue)]
        public bool OptionW { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(INetatmo netatmo,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

            // Setting the Netatmo instance.
            _netatmo = netatmo;
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
                    // Overriding Netatmo options.
                    _netatmo.BaseAddress = Parent.BaseAddress;
                    _netatmo.Timeout = Parent.Timeout;
                    _netatmo.User = Parent.User;
                    _netatmo.Password = Parent.Password;
                    _netatmo.ClientID = Parent.ClientID;
                    _netatmo.ClientSecret = Parent.ClientSecret;

                    await _netatmo.ReadAllAsync();

                    if (OptionM)
                    {
                        Console.WriteLine($"Main Module: {JsonConvert.SerializeObject(_netatmo.Station.Device.DashboardData, Formatting.Indented)}");
                    }

                    if (OptionO)
                    {
                        Console.WriteLine($"Outdoor Module: {JsonConvert.SerializeObject(_netatmo.Station.Device.OutdoorModule.DashboardData, Formatting.Indented)}");
                    }

                    if (Option1)
                    {
                        Console.WriteLine($"Indoor Module 1: {JsonConvert.SerializeObject(_netatmo.Station.Device.IndoorModule1.DashboardData, Formatting.Indented)}");
                    }

                    if (Option2)
                    {
                        Console.WriteLine($"Indoor Module 2: {JsonConvert.SerializeObject(_netatmo.Station.Device.IndoorModule2.DashboardData, Formatting.Indented)}");
                    }

                    if (Option3)
                    {
                        Console.WriteLine($"Indoor Module 3: {JsonConvert.SerializeObject(_netatmo.Station.Device.IndoorModule3.DashboardData, Formatting.Indented)}");
                    }

                    if (OptionR)
                    {
                        Console.WriteLine($"Rain Gauge: {JsonConvert.SerializeObject(_netatmo.Station.Device.RainGauge.DashboardData, Formatting.Indented)}");
                    }

                    if (OptionW)
                    {
                        Console.WriteLine($"Wind Gauge: {JsonConvert.SerializeObject(_netatmo.Station.Device.WindGauge.DashboardData, Formatting.Indented)}");
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
