// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
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
    using DataValueLib;
    using ETAPU11Lib;
    using ETAPU11Lib.Models;
    using ETAPU11App.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "ETAPU11 Read Command",
             Description = "Reading data values from ETA PU 11 pellet boiler.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IETAPU11 _etapu11;

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
        /// <param name="etapu11">The ETAPU11 instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IETAPU11 etapu11,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

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

                    if (Property.Length == 0)
                    {
                        Console.WriteLine($"Reading all data from ETAPU11 boiler.");
                        DataStatus status;

                        if (OptionB)
                        {
                            status = await _etapu11.ReadBlockAsync();
                        }
                        else
                        {
                            status = await _etapu11.ReadAllAsync();
                        }

                        if (status.IsGood)
                        {
                            Console.WriteLine($"ETAPU11: {JsonConvert.SerializeObject(_etapu11, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading data from ETAPU11 boiler.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                    }
                    else if (Property.Length > 0)
                    {
                        Console.WriteLine($"Reading property '{Property}' from ETAPU11 boiler");
                        _etapu11.ReadDataAsync(Property).Wait();

                        if (_etapu11.Data.Status.IsGood)
                        {
                            Console.WriteLine($"Value of property '{Property}' = {_etapu11.Data.GetPropertyValue(Property)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading property '{Property}' from KWLEC200 hvac.");
                            Console.WriteLine($"Reason: {_etapu11.Data.Status.Explanation}.");
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
                if (!ETAPU11Data.IsProperty(Property))
                {
                    _logger?.LogError($"The property '{Property}' has not been found.");
                    return false;
                }

                if (!ETAPU11Data.IsReadable(Property))
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
