// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CleanCommand.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoApp.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoLib.Models.Enums;
    using ZipatoApp.Models;
    using ZipatoLib.Models;

    #endregion

    /// <summary>
    /// Application command cleaning selected Zipatile data on the Zipatile.
    /// </summary>
    /// <summary>
    /// Application command "value".
    /// </summary>
    [Command(Name = "clean",
             FullName = "Zipato Clean Command",
             Description = "Cleaning of selected Zipato data entities.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class CleanCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionX || OptionSD);
        }

        #endregion

        #region Public Properties

        [Option("-sd|--schedules", "Clean unused schedules from the Zipatile.", CommandOptionType.NoValue)]
        public bool OptionSD { get; set; }

        [Option("-x|--execute", "Executes the actual cleaning.", CommandOptionType.NoValue)]
        public bool OptionX { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public CleanCommand(IZipato zipato,
                           ILogger<CleanCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("CleanCommand()");

            // Setting the Zipato instance.
            _zipato = zipato;
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            if (CheckOptions(app))
            {
                try
                {
                    // Overriding Zipato options.
                    _zipato.BaseAddress = Parent.BaseAddress;
                    _zipato.Timeout = Parent.Timeout;
                    _zipato.User = Parent.User;
                    _zipato.Password = Parent.Password;
                    _zipato.IsLocal = Parent.IsLocal;
                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    if (OptionSD)
                    {
                        Console.WriteLine($"Cleaning Zipatile Schedules:");
                        var (result1, status1) = await _zipato.DataReadRulesFullAsync();

                        if (status1.IsGood)
                        {
                            IEnumerable<Guid?> schedulesInRules = result1
                                .Where(r => r.Type == RuleTypes.SCHEDULER)
                                .Select(r => r.Code.WhenClause.Properties.ScheduleUuid);

                            var (result2, status2) = _zipato.DataReadSchedulesAsync().Result;

                            if (status2.IsGood)
                            {
                                IEnumerable<Guid?> schedules = result2
                                    .Select(s => s.Uuid);

                                foreach (var schedule in schedules)
                                {
                                    if (!schedulesInRules.Contains(schedule))
                                    {
                                        if (OptionX)
                                        {
                                            var status = await _zipato.DeleteScheduleAsync(schedule.Value);

                                            if (status.IsGood)
                                            {
                                                Console.WriteLine($"Zipatile Schedule '{schedule}' deleted.");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Zipatile Schedule '{schedule}' NOT deleted.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Zipatile Schedule '{schedule}' not used.");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No schedule infos found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No rules found.");
                        }

                        if (!OptionX)
                        {
                            Console.WriteLine($"Specify -x to clean unused schedules.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception CleanCommand.");
                    return -1;
                }
                finally
                {
                    _zipato.EndSession();
                }
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
                app.ShowHelp();
                return false;
            }

            return true;
        }

        #endregion
    }
}
