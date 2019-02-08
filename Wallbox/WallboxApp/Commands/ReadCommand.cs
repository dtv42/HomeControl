// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using DataValueLib;
    using WallboxLib;
    using WallboxLib.Models;
    using WallboxApp.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "Wallbox Read Command",
             Description = "Reading data values from BMW Wallbox charging station.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

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

        [Option("-a|--all", "Reads all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-1|--report1", "Reads the report 1 data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--report2", "Reads the report 2 data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--report3", "Reads the report 3 data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        [Option("-l|--last", "Reads the last charging report data.", CommandOptionType.NoValue)]
        public bool OptionL { get; set; }

        [Option("-r|--reports", "Reads the charging reports data.", CommandOptionType.NoValue)]
        public bool OptionR { get; set; }

        [Argument(0, "Reads the named property.")]
        public string Property { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IWallbox wallbox,
                           ILogger<ReadCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

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
            try
            {
                if (CheckOptions(app))
                {
                    // Overriding Wallbox options.
                    _wallbox.HostName = Parent.HostName;
                    _wallbox.Port = Parent.Port;

                    if (Property.Length > 0)
                    {
                        Console.WriteLine($"Reading property '{Property}' from Wallbox charging station.");
                        var status = await _wallbox.ReadAllAsync();

                        if (status != DataValue.Good)
                        {
                            Console.WriteLine($"Error reading property '{Property}' from Wallbox charging station.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                        else
                        {
                            Console.WriteLine($"Value of property '{Property}' = {JsonConvert.SerializeObject(_wallbox.GetPropertyValue(Property), Formatting.Indented)}");
                        }
                    }
                    else
                    {
                        if (OptionA)
                        {
                            await _wallbox.ReadAllAsync();
                            Console.WriteLine($"WallboxData: {JsonConvert.SerializeObject(_wallbox.Data, Formatting.Indented)}");
                        }
                        else
                        {
                            if (Option1)
                            {
                                await _wallbox.ReadReport1Async();
                                Console.WriteLine($"Report1: {JsonConvert.SerializeObject(_wallbox.Data.Report1, Formatting.Indented)}");
                            }

                            if (Option2)
                            {
                                await _wallbox.ReadReport2Async();
                                Console.WriteLine($"Report2: {JsonConvert.SerializeObject(_wallbox.Data.Report2, Formatting.Indented)}");
                            }

                            if (Option3)
                            {
                                await _wallbox.ReadReport3Async();
                                Console.WriteLine($"Report3: {JsonConvert.SerializeObject(_wallbox.Data.Report3, Formatting.Indented)}");
                            }

                            if (OptionL)
                            {
                                await _wallbox.ReadReport100Async();
                                Console.WriteLine($"Report100: {JsonConvert.SerializeObject(_wallbox.Data.Report100, Formatting.Indented)}");
                            }

                            if (OptionR)
                            {
                                await _wallbox.ReadReportsAsync();
                                Console.WriteLine($"Reports: {JsonConvert.SerializeObject(_wallbox.Data.Reports, Formatting.Indented)}");
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

            if (OptionA && (Option1 || Option2 || Option3 || OptionL || OptionR))
            {
                Console.WriteLine($"Option -a overrides all other options.");
            }

            if (Property.Length > 0)
            {
                if (!NoOptions)
                {
                    Console.WriteLine($"Reading the property '{Property}' - options are ignored.");
                }

                if (!Wallbox.IsProperty(Property))
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
