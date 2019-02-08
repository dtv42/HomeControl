// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
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
    using DataValueLib;
    using NetatmoLib;
    using NetatmoLib.Models;
    using NetatmoApp.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "Netatmo Read Command",
             Description = "Reading data values from Netatmo weather station.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly INetatmo _netatmo;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        #endregion

        #region Public Properties

        [Argument(0, "Reads the named property.")]
        public string Property { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(INetatmo netatmo,
                           ILogger<ReadCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

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

                    if (Property.Length == 0)
                    {
                        Console.WriteLine($"Reading all data from Netatmo weather station.");
                        var status = await _netatmo.ReadAllAsync();

                        if (status != DataValue.Good)
                        {
                            Console.WriteLine($"Error reading property '{Property}' from Netatmo weather station.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                        else
                        {
                            Console.WriteLine($"Netatmo: {JsonConvert.SerializeObject(_netatmo, Formatting.Indented)}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Reading property '{Property}' from Netatmo weather station.");
                        var status = await _netatmo.ReadAllAsync();

                        if (status != DataValue.Good)
                        {
                            Console.WriteLine($"Error reading property '{Property}' from Netatmo weather station.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                        else
                        {
                            Console.WriteLine($"Value of property '{Property}' = {JsonConvert.SerializeObject(_netatmo.GetPropertyValue(Property), Formatting.Indented)}");
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
            if (Property.Length > 0)
            {
                if (!Netatmo.IsProperty(Property))
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
