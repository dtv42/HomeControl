// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlApp.Commands
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
    using BControlLib;
    using BControlLib.Models;
    using BControlApp.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "BControl Read Command",
             Description = "Reading data values from TQ energy meter.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IBControl _bcontrol;

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
        /// <param name="bcontrol">The BControl instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IBControl bcontrol,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

            // Setting the BControl instance.
            _bcontrol = bcontrol;
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
                    // Overriding BControl options.
                    _bcontrol.TcpSlave.Address = Parent.Address;
                    _bcontrol.TcpSlave.Port = Parent.Port;
                    _bcontrol.TcpSlave.ID = Parent.SlaveID;

                    if (Property.Length == 0)
                    {
                        Console.WriteLine($"Reading all data from BControl energy meter.");
                        DataStatus status;

                        if (OptionB)
                        {
                            status = await _bcontrol.ReadBlockAsync();
                        }
                        else
                        {
                            status = await _bcontrol.ReadAllAsync();
                        }

                        if (status.IsGood)
                        {
                            Console.WriteLine($"BControl: {JsonConvert.SerializeObject(_bcontrol, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading data from BControl energy meter.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                    }
                    else if (Property.Length > 0)
                    {
                        Console.WriteLine($"Reading property '{Property}' from BControl energy meter:");
                        await _bcontrol.ReadDataAsync(Property);

                        if (_bcontrol.Data.Status.IsGood)
                        {
                            Console.WriteLine($"Value of property '{Property}' = {_bcontrol.Data.GetPropertyValue(Property)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading property '{Property}' from BControl energy meter.");
                            Console.WriteLine($"Reason: {_bcontrol.Data.Status.Explanation}.");
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
                if (!BControlData.IsProperty(Property))
                {
                    _logger?.LogError($"The property '{Property}' has not been found.");
                    return false;
                }

                if (!BControlData.IsReadable(Property))
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
