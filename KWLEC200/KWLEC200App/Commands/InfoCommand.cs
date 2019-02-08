// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200App.Commands
{
    #region Using Directives

    using System;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using KWLEC200Lib;
    using KWLEC200App.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "KWLEC200 Info Command",
             Description = "Reading data values from Helios KWL 200 EC ventilation unit.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IKWLEC200 _kwlec200;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionO); }

        #endregion

        #region Public Properties

        [Option("-a|--all", "Gets all data.", CommandOptionType.NoValue)]
        public bool OptionA { get; set; }

        [Option("-o|--overview", "Gets the overview data.", CommandOptionType.NoValue)]
        public bool OptionO { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="kwlec200">The KWLEC200 instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IKWLEC200 kwlec200,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

            // Setting the KWLEC200 instance.
            _kwlec200 = kwlec200;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public int OnExecute(CommandLineApplication app)
        {
            try
            {
                if (CheckOptions(app))
                {
                    // Overriding KWLEC200 options.
                    _kwlec200.Slave.Address = Parent.Address;
                    _kwlec200.Slave.Port = Parent.Port;
                    _kwlec200.Slave.ID = Parent.SlaveID;

                    if (OptionA)
                    {
                        _kwlec200.ReadAll();
                        Console.WriteLine($"Data: {JsonConvert.SerializeObject(_kwlec200.Data, Formatting.Indented)}");
                    }

                    if (OptionO)
                    {
                        _kwlec200.ReadOverviewData();
                        Console.WriteLine($"Overview: {JsonConvert.SerializeObject(_kwlec200.OverviewData, Formatting.Indented)}");
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