// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DimmerCommand.cs" company="DTV-Online">
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
    using ZipatoLib.Models.Others;

    #endregion

    /// <summary>
    /// Application command "camera".
    /// </summary>
    [Command(Name = "camera",
             FullName = "Zipato Camera Command",
             Description = "Camera access from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class CameraCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="OthersCommand"/>.
        /// </summary>
        private OthersCommand Parent { get; set; }

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionIndex { get; set; }
        private bool OptionName { get; set; }
        private bool OptionUuid { get; set; }

        /// <summary>
        /// Returns true if no parent option is selected.
        /// </summary>
        private bool NoParentOptions { get => !(OptionIndex || OptionName || OptionUuid); }

        #endregion

        #region Public Properties

        [Option("-s|--snapshot", "take a snapshot.", CommandOptionType.NoValue)]
        public bool OptionS { get; set; }

        [Option("-r|--recording", "take a recording.", CommandOptionType.NoValue)]
        public bool OptionR { get; set; }

        [Option("-f|--files", "show saved files.", CommandOptionType.NoValue)]
        public bool OptionF { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public CameraCommand(IZipato zipato,
                             ILogger<DimmerCommand> logger,
                             IOptions<AppSettings> options,
                             IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("CameraCommand()");

            // Setting the Zipato instance.
            _zipato = zipato;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecute(CommandLineApplication app)
        {
            if (CheckOptions(app))
            {
                try
                {
                    // Overriding Zipato options.
                    _zipato.BaseAddress = Parent.Parent.BaseAddress;
                    _zipato.Timeout = Parent.Parent.Timeout;
                    _zipato.User = Parent.Parent.User;
                    _zipato.Password = Parent.Parent.Password;
                    _zipato.IsLocal = Parent.Parent.IsLocal;
                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    await _zipato.ReadAllDataAsync();

                    Camera camera = null;

                    if (NoParentOptions)
                    {
                        Console.WriteLine($"Cameras: {JsonConvert.SerializeObject(_zipato.Others.Cameras, Formatting.Indented)}");
                        return 0;
                    }
                    else if (OptionIndex)
                    {
                        var index = Parent.Index;

                        if ((index >= 0) && (index < _zipato.Others.Cameras.Count))
                        {
                            camera = _zipato.Others.Cameras[index];

                            if (camera == null)
                            {
                                Console.WriteLine($"Camera with index '{index}' not found.");
                                return -1;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Camera index '{index}' out of bounds (0 - {_zipato.Others.Scenes.Count - 1}).");
                            return -1;
                        }
                    }
                    else if (OptionName)
                    {
                        var name = Parent.Name;
                        camera = _zipato.Others.Cameras.FirstOrDefault(d => d.Name == name);

                        if (camera == null)
                        {
                            Console.WriteLine($"Camera with name '{name}' not found.");
                            return -1;
                        }
                    }
                    else if (OptionUuid)
                    {
                        var uuid = new Guid(Parent.Uuid);
                        camera = _zipato.Others.Cameras.FirstOrDefault(d => d.Uuid == uuid);

                        if (camera == null)
                        {
                            Console.WriteLine($"Camera with UUID '{uuid}' not found.");
                            return -1;
                        }
                    }

                    if (OptionS)
                    {
                        Console.WriteLine($"Snapshot {(camera.TakeSnapshot() ? "OK" : "not OK")}");
                    }

                    if (OptionR)
                    {
                        Console.WriteLine($"Recording {(camera.TakeRecording() ? "OK" : "not OK")}");
                    }

                    Console.WriteLine($"Camera: {JsonConvert.SerializeObject(camera, Formatting.Indented)}");

                    if (OptionF)
                    {
                        Console.WriteLine($"Saved Files: {JsonConvert.SerializeObject(camera.Files, Formatting.Indented)}");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception CameraCommand.");
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
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                switch (option.LongName)
                {
                    case "index": OptionIndex = option.HasValue(); break;
                    case "name": OptionName = option.HasValue(); break;
                    case "uuid": OptionUuid = option.HasValue(); break;
                }
            }

            if ((OptionIndex && (OptionName || OptionUuid)) ||
                (OptionName && (OptionUuid || OptionIndex)) ||
                (OptionUuid && (OptionIndex || OptionName)))
            {
                Console.WriteLine("Select only a single option from '--index', '--name', and '--uuid'.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
