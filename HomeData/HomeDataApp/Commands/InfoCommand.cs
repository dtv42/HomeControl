// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataApp.Commands
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
    using HomeDataLib;
    using HomeDataApp.Models;

    #endregion

    /// <summary>
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "HomeData Info Command",
             Description = "Reading data values from home control system.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IHomeData _homedata;

        #endregion Private Data Members

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        #endregion

        #region Public Properties

        [Argument(0, "Shows the named property.")]
        public string Property { get; set; } = string.Empty;

        [Option("-u|--update", "Use updates reading the data.", CommandOptionType.NoValue)]
        public bool OptionU { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="homedata">The HomeData instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IHomeData homedata,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

            // Setting the HomeData instance.
            _homedata = homedata;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecute(CommandLineApplication app)
        {
            try
            {
                if (CheckOptions(app))
                {
                    // Overriding EM300LR options.
                    _homedata.Meter1Address = Parent.Meter1Address;
                    _homedata.Meter2Address = Parent.Meter2Address;

                    await _homedata.ReadAllAsync(OptionU);

                    if (_homedata.IsInitialized)
                    {
                        if (_homedata.Data.IsGood)
                        {
                            if (Property.Length > 0)
                            {
                                Console.WriteLine($"Reading property '{Property}' from home control system.");
                                Console.WriteLine($"Value of property '{Property}' = {JsonConvert.SerializeObject(_homedata.GetPropertyValue(Property), Formatting.Indented)}");
                            }
                            else
                            {
                                Console.WriteLine($"HomeData: {JsonConvert.SerializeObject(_homedata.Data, Formatting.Indented)}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error getting updated data from home control system.");
                            Console.WriteLine($"Reason: {_homedata.Data.Status.Explanation}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error initializing data from home control system.");
                        Console.WriteLine($"Reason: {_homedata.Data.Status.Explanation}.");
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
                if (!HomeData.IsProperty(Property))
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
