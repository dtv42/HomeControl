// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRApp.Commands
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
    using EM300LRLib;
    using EM300LRApp.Models;

    #endregion Using Directives

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "EM300LR Read Command",
             Description = "Reading data values from b-Control EM300LR energy manager.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IEM300LR _em300lr;

        #endregion Private Data Members

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        #endregion Private Properties

        #region Public Properties

        [Argument(0, "Reads the named property.")]
        public string Property { get; set; } = string.Empty;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="em300lr">The EM300LR instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IEM300LR em300lr,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

            // Setting the EM300LR instance.
            _em300lr = em300lr;
        }

        #endregion Constructors

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
                    // Overriding EM300LR options.
                    _em300lr.BaseAddress = Parent.BaseAddress;
                    _em300lr.Timeout = Parent.Timeout;
                    _em300lr.Password = Parent.Password;
                    _em300lr.SerialNumber = Parent.SerialNumber;

                    if (Property.Length > 0)
                    {
                        Console.WriteLine($"Reading property '{Property}' from EM300LR energy manager.");
                        var status = await _em300lr.ReadAllAsync();

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Value of property '{Property}' = {JsonConvert.SerializeObject(_em300lr.GetPropertyValue(Property), Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading property '{Property}' from EM300LR energy manager.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                    }
                    else
                    {
                        var status = await _em300lr.ReadAllAsync();

                        if (status.IsGood)
                        {
                            Console.WriteLine($"EM300LRData: {JsonConvert.SerializeObject(_em300lr.Data, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading data from EM300LR energy manager.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
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

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            return true;
        }

        #endregion Private Methods
    }
}