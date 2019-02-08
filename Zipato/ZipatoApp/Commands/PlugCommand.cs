// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlugCommand.cs" company="DTV-Online">
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
    /// Application command "plug".
    /// </summary>
    [Command(Name = "plug",
             FullName = "Zipato Plug Command",
             Description = "Plug device access from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class PlugCommand : BaseCommand<AppSettings>
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

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionOn || OptionOff); }

        #endregion

        #region Public Properties

        [Option("-on|--turnon", "Turn the device on.", CommandOptionType.NoValue)]
        public bool OptionOn { get; set; }

        [Option("-off|--turnoff", "Turn the device off.", CommandOptionType.NoValue)]
        public bool OptionOff { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlugCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public PlugCommand(IZipato zipato,
                           ILogger<PlugCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("PlugCommand()");

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

                    Plug device = null;

                    if (NoParentOptions)
                    {
                        Console.WriteLine($"Wallplugs: {JsonConvert.SerializeObject(_zipato.Devices.Wallplugs, Formatting.Indented)}");
                        return 0;
                    }
                    else if (OptionIndex)
                    {
                        var index = Parent.Index;

                        if ((index >= 0) && (index < _zipato.Devices.Wallplugs.Count))
                        {
                            device = _zipato.Devices.Wallplugs[index];

                            if (device == null)
                            {
                                Console.WriteLine($"Wallplug with index '{index}' not found.");
                                return -1;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Wallplug index '{index}' out of bounds (0 - {_zipato.Devices.Wallplugs.Count - 1}).");
                            return -1;
                        }
                    }
                    else if (OptionName)
                    {
                        var name = Parent.Name;
                        device = _zipato.Devices.Wallplugs.FirstOrDefault(d => d.Name == name);

                        if (device == null)
                        {
                            Console.WriteLine($"Wallplug with name '{name}' not found.");
                            return -1;
                        }
                    }
                    else if (OptionUuid)
                    {
                        var uuid = new Guid(Parent.Uuid);
                        device = _zipato.Devices.Wallplugs.FirstOrDefault(d => d.Uuid == uuid);

                        if (device == null)
                        {
                            Console.WriteLine($"Wallplug with UUID '{uuid}' not found.");
                            return -1;
                        }
                    }

                    if (OptionOn)
                    {
                        Console.WriteLine($"Wallplug turn on {(device.TurnOn() ? "OK" : "not OK")}");
                    }

                    if (OptionOff)
                    {
                        Console.WriteLine($"Wallplug turn off {(device.TurnOff() ? "OK" : "not OK")}");
                    }

                    Console.WriteLine($"Wallplug: {JsonConvert.SerializeObject(device, Formatting.Indented)}");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception PlugCommand.");
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

            if (OptionOn && OptionOff)
            {
                Console.WriteLine("Select on or off option.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
