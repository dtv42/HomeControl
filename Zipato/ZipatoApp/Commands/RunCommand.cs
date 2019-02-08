// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunCommand.cs" company="DTV-Online">
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
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoApp.Models;

    #endregion

    /// <summary>
    /// Application command "value".
    /// </summary>
    [Command(Name = "run",
             FullName = "Zipato Run Command",
             Description = "Running scenes at Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class RunCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        #endregion

        #region Public Properties

        [Required]
        [Argument(0, "scene id (uuid or name)")]
        public string Scene { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RunCommand(IZipato zipato,
                           ILogger<RunCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RunCommand()");

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

                    Console.WriteLine($"Running scene '{Scene}'.");

                    if (Guid.TryParse(Scene, out Guid uuid))
                    {
                        var (runresult, runstatus) = await _zipato.ReadSceneRunAsync(uuid);

                        if (runstatus.IsGood)
                        {
                            Console.WriteLine($"Running scene with UUID '{Scene}' successful.");
                        }
                        else
                        {
                            Console.WriteLine($"Running scene with UUID '{Scene}' not successful: {runresult}");
                        }
                    }
                    else
                    {
                        var (scenes, status) = await _zipato.DataReadScenesAsync();

                        if (status.IsGood)
                        {
                            var scene = scenes.First(s => s.Name == Scene);

                            if ((scene != null) && scene.Uuid.HasValue)
                            {
                                var (runresult, runstatus) = await _zipato.ReadSceneRunAsync(scene.Uuid.Value);

                                if (runstatus.IsGood)
                                {
                                    Console.WriteLine($"Running scene with UUID '{scene.Uuid}' successful.");
                                }
                                else
                                {
                                    Console.WriteLine($"Running scene with UUID '{scene.Uuid}' not successful: {runresult}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Could not find scene with name '{Scene}'.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Could not read scenes.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception RunCommand.");
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
            return true;
        }

        #endregion
    }
}
