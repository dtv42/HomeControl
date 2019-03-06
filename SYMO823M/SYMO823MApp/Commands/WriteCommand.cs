// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteCommand.cs" company="DTV-Online">
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
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using SYMO823MLib;
    using SYMO823MLib.Models;
    using SYMO823MApp.Models;

    #endregion

    /// <summary>
    /// Application command "write".
    /// </summary>
    [Command(Name = "write",
             FullName = "SYMO823M Write Command",
             Description = "Writing data values to Fronius Symo 8.2-3-M inverter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class WriteCommand : BaseCommand<AppSettings>
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

        [Required]
        [Argument(0, "property name")]
        public string Property { get; set; } = string.Empty;

        [Required]
        [Argument(1, "property value")]
        public string Value { get; set; } = string.Empty;

        [Option("-b|--block", "Using block mode read.", CommandOptionType.NoValue)]
        public bool OptionB { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteCommand"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public WriteCommand(ISYMO823M symo823m,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("WriteCommand()");

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
                // Overriding SYMO823M options.
                _symo823m.Slave.Address = Parent.Address;
                _symo823m.Slave.Port = Parent.Port;
                _symo823m.Slave.ID = Parent.SlaveID;

                if (CheckOptions(app))
                {
                    Console.WriteLine($"Writing value '{Value}' to property '{Property}' at SYMO823M symo");
                    var status = await _symo823m.WritePropertyAsync(Property, Value);

                    if (status.IsGood)
                    {
                        Console.WriteLine($"Reading properties from SYMO823M inverter.");

                        if (OptionB)
                        {
                            await _symo823m.ReadBlockAllAsync();
                        }
                        else
                        {
                            await _symo823m.ReadAllAsync();
                        }

                        if (_symo823m.Data.Status.IsGood)
                        {
                            Console.WriteLine($"Value of property '{Property}' = {_symo823m.Data.GetPropertyValue(Property)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading property '{Property}' from SYMO823M inverter.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error writing property '{Property}' from SYMO823M inverter.");
                        Console.WriteLine($"Reason: {status.Explanation}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception WriteCommand.");
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

                if (!SYMO823MData.IsWritable(Property))
                {
                    _logger?.LogError($"The property '{Property}' is not writable.");
                    return false;
                }

                if (string.IsNullOrEmpty(Value))
                {
                    _logger?.LogError($"The value '{Value}' for the property '{Property}' is invalid.");
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
