// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwitchCommand.cs" company="DTV-Online">
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
    using ZipatoLib.Models.Devices;

    #endregion

    /// <summary>
    /// Application command "switch".
    /// </summary>
    [Command(Name = "switch",
             FullName = "Zipato Switch Command",
             Description = "Switch device access from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class SwitchCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="DevicesCommand"/>.
        /// </summary>
        private DevicesCommand Parent { get; set; }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnOffCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public SwitchCommand(IZipato zipato,
                             ILogger<SwitchCommand> logger,
                             IOptions<AppSettings> options,
                             IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("SwitchCommand()");

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

                    Switch device = null;

                    if (NoParentOptions)
                    {
                        Console.WriteLine($"Switch devices: {JsonConvert.SerializeObject(_zipato.Devices.Switches, Formatting.Indented)}");
                        return 0;
                    }
                    else if (OptionIndex)
                    {
                        var index = Parent.Index;

                        if ((index >= 0) && (index < _zipato.Devices.Wallplugs.Count))
                        {
                            device = _zipato.Devices.Switches[index];

                            if (device == null)
                            {
                                Console.WriteLine($"Switch device with index '{index}' not found.");
                                return -1;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Switch device index '{index}' out of bounds (0 - {_zipato.Devices.Switches.Count - 1}).");
                            return -1;
                        }
                    }
                    else if (OptionName)
                    {
                        var name = Parent.Name;
                        device = _zipato.Devices.Switches.FirstOrDefault(d => d.Name == name);

                        if (device == null)
                        {
                            Console.WriteLine($"Switch device with name '{name}' not found.");
                            return -1;
                        }
                    }
                    else if (OptionUuid)
                    {
                        var uuid = new Guid(Parent.Uuid);
                        device = _zipato.Devices.Switches.FirstOrDefault(d => d.Uuid == uuid);

                        if (device == null)
                        {
                            Console.WriteLine($"Switch device with UUID '{uuid}' not found.");
                            return -1;
                        }
                    }

                    Console.WriteLine($"Switch device: {JsonConvert.SerializeObject(device, Formatting.Indented)}");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception SwitchCommand.");
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
