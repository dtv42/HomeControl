// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
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
    using DataValueLib;
    using SYMO823MLib;
    using SYMO823MLib.Models;
    using SYMO823MApp.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "SYMO823M Read Command",
             Description = "Reading data values from Fronius Symo 8.2-3-M inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly ISYMO823M _symo823m;

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

        [Option("-b|--block", "Using block mode read.", CommandOptionType.NoValue)]
        public bool OptionB { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(ISYMO823M symo823m,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

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

                    if (Property.Length == 0)
                    {
                        Console.WriteLine($"Reading all data from SYMO823M inverter.");

                        if (OptionB)
                        {
                            await _symo823m.ReadBlockAllAsync();
                        }
                        else
                        {
                            await _symo823m.ReadAllAsync();
                        }

                        Console.WriteLine($"SYMO823M: {JsonConvert.SerializeObject(_symo823m, Formatting.Indented)}");
                    }

                    if (Property.Length > 0)
                    {
                        Console.WriteLine($"Reading property '{Property}' from SYMO823M inverter");
                        _symo823m.ReadPropertyAsync(Property).Wait();
                        Console.WriteLine($"Value of property '{Property}' = {_symo823m.Data.GetPropertyValue(Property)}");
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
                if (!SYMO823MData.IsProperty(Property))
                {
                    _logger?.LogError($"The property '{Property}' has not been found.");
                    return false;
                }

                if (!SYMO823MData.IsReadable(Property))
                {
                    _logger?.LogError($"The property '{Property}' is not readable.");
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
