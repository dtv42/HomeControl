// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoCommand.cs" company="DTV-Online">
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
    /// Application command "info".
    /// </summary>
    [Command(Name = "info",
             FullName = "EM300LR Info Command",
             Description = "Reading data values from b-Control EM300LR energy manager.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class InfoCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IEM300LR _em300lr;

        #endregion Private Data Members

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionT || Option1 || Option2 || Option3); }

        #endregion Private Properties

        #region Public Properties

        [Option("-t|--total", "Get the total data.", CommandOptionType.NoValue)]
        public bool OptionT { get; set; }

        [Option("-1|--phase1", "Get the phase 1 data.", CommandOptionType.NoValue)]
        public bool Option1 { get; set; }

        [Option("-2|--phase2", "Get the phase 2 data.", CommandOptionType.NoValue)]
        public bool Option2 { get; set; }

        [Option("-3|--phase3", "Get the phase 3 data.", CommandOptionType.NoValue)]
        public bool Option3 { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoCommand"/> class.
        /// </summary>
        /// <param name="em300lr">The EM300LR instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public InfoCommand(IEM300LR em300lr,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("InfoCommand()");

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

                    var status = await _em300lr.ReadAllAsync();

                    if (status.IsGood)
                    {
                        if (OptionT)
                        {
                            Console.WriteLine($"TotalData: {JsonConvert.SerializeObject(_em300lr.TotalData, Formatting.Indented)}");
                        }

                        if (Option1)
                        {
                            Console.WriteLine($"Phase1Data: {JsonConvert.SerializeObject(_em300lr.Phase1Data, Formatting.Indented)}");
                        }

                        if (Option2)
                        {
                            Console.WriteLine($"Phase2Data: {JsonConvert.SerializeObject(_em300lr.Phase2Data, Formatting.Indented)}");
                        }

                        if (Option3)
                        {
                            Console.WriteLine($"Phase3Data: {JsonConvert.SerializeObject(_em300lr.Phase3Data, Formatting.Indented)}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error reading data from EM300LR energy manager.");
                        Console.WriteLine($"Reason: {status.Explanation}.");
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

        #endregion Public Methods

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

        #endregion Private Methods
    }
}