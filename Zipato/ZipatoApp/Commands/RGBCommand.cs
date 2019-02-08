// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RGBCommand.cs" company="DTV-Online">
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
    /// Application command "rgb".
    /// </summary>
    [Command(Name = "rgb",
             FullName = "Zipato RGB Command",
             Description = "RGB device access from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class RGBCommand : BaseCommand<AppSettings>
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

        private bool OptionV { get; set; }
        private bool OptionR { get; set; }
        private bool OptionG { get; set; }
        private bool OptionB { get; set; }
        private bool OptionCW { get; set; }
        private bool OptionWW { get; set; }
        private bool OptionRGB { get; set; }
        private bool OptionRGBW { get; set; }

        /// <summary>
        /// Returns true if no parent option is selected.
        /// </summary>
        private bool NoParentOptions { get => !(OptionIndex || OptionName || OptionUuid); }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionV || OptionCW || OptionWW || OptionRGB || OptionRGBW); }

        #endregion

        #region Public Properties

        [Option("-v|--value <number>", "The intensity value (0-100).", CommandOptionType.SingleValue)]
        public int Intensity { get; set; }

        [Option("-r|--red <number>", "The red value (0-255).", CommandOptionType.SingleValue)]
        public int Red { get; set; }

        [Option("-g|--green <number>", "The green value (0-255).", CommandOptionType.SingleValue)]
        public int Green { get; set; }

        [Option("-b|--blue <number>", "The blue value (0-255).", CommandOptionType.SingleValue)]
        public int Blue { get; set; }

        [Option("-cw|--coldwhite <value>", "Cold-White color value (0-255).", CommandOptionType.SingleValue)]
        public int ColdWhite { get; set; }

        [Option("-ww|--warmwhite <value>", "Warm-White color value (0-255).", CommandOptionType.SingleValue)]
        public int WarmWhite { get; set; }

        [Option("-rgb|--rgb <value>", "RGB color value (hex).", CommandOptionType.SingleValue)]
        public string RGBValue { get; set; } = string.Empty;

        [Option("-rgbw|--rgbw <value>", "RGBW color value (hex).", CommandOptionType.SingleValue)]
        public string RGBWValue { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RGBCommand(IZipato zipato,
                          ILogger<RGBCommand> logger,
                          IOptions<AppSettings> options,
                          IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RGBCommand()");

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

                    RGBControl device = null;

                    if (NoParentOptions)
                    {
                        Console.WriteLine($"RGB devices: {JsonConvert.SerializeObject(_zipato.Devices.RGBControls, Formatting.Indented)}");
                        return 0;
                    }
                    else if (OptionIndex)
                    {
                        var index = Parent.Index;

                        if ((index >= 0) && (index < _zipato.Devices.RGBControls.Count))
                        {
                            device = _zipato.Devices.RGBControls[index];

                            if (device == null)
                            {
                                Console.WriteLine($"RGB device with index '{index}' not found.");
                                return -1;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"RGB device index '{index}' out of bounds (0 - {_zipato.Devices.RGBControls.Count - 1}).");
                            return -1;
                        }
                    }
                    else if (OptionName)
                    {
                        var name = Parent.Name;
                        device = _zipato.Devices.RGBControls.FirstOrDefault(d => d.Name == name);

                        if (device == null)
                        {
                            Console.WriteLine($"RGB device with name '{name}' not found.");
                            return -1;
                        }
                    }
                    else if (OptionUuid)
                    {
                        var uuid = new Guid(Parent.Uuid);
                        device = _zipato.Devices.RGBControls.FirstOrDefault(d => d.Uuid == uuid);

                        if (device == null)
                        {
                            Console.WriteLine($"RGB device with UUID '{uuid}' not found.");
                            return -1;
                        }
                    }

                    if (OptionV)
                    {
                        Console.WriteLine($"RGB device set intensity {(device.SetIntensity(Intensity) ? "OK" : "not OK")}");
                    }

                    if (OptionR)
                    {
                        Console.WriteLine($"RGB device set red {(device.SetRed(Red) ? "OK" : "not OK")}");
                    }

                    if (OptionG)
                    {
                        Console.WriteLine($"RGB device set green {(device.SetGreen(Green) ? "OK" : "not OK")}");
                    }

                    if (OptionB)
                    {
                        Console.WriteLine($"RGB device set blue {(device.SetBlue(Blue) ? "OK" : "not OK")}");
                    }

                    if (OptionCW)
                    {
                        Console.WriteLine($"RGB device set cold white {(device.SetColdWhite(ColdWhite) ? "OK" : "not OK")}");
                    }

                    if (OptionWW)
                    {
                        Console.WriteLine($"RGB device set warm white {(device.SetWarmWhite(WarmWhite) ? "OK" : "not OK")}");
                    }

                    if (OptionRGB)
                    {
                        Console.WriteLine($"RGB device set RGB {(device.SetRGB(RGBValue) ? "OK" : "not OK")}");
                    }

                    if (OptionRGBW)
                    {
                        Console.WriteLine($"RGB device set RGBW {(device.SetRGBW(RGBWValue) ? "OK" : "not OK")}");
                    }

                    Console.WriteLine($"RGB device: {JsonConvert.SerializeObject(device, Formatting.Indented)}");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception RGBCommand.");
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

                    case "value": OptionV = option.HasValue(); break;
                    case "red": OptionR = option.HasValue(); break;
                    case "green": OptionG = option.HasValue(); break;
                    case "blue": OptionB = option.HasValue(); break;
                    case "coldwhite": OptionCW = option.HasValue(); break;
                    case "warmwhite": OptionWW = option.HasValue(); break;
                    case "rgb": OptionRGB = option.HasValue(); break;
                    case "rgbw": OptionRGBW = option.HasValue(); break;
                }
            }

            if ((OptionIndex && (OptionName || OptionUuid)) ||
                (OptionName && (OptionUuid || OptionIndex)) ||
                (OptionUuid && (OptionIndex || OptionName)))
            {
                Console.WriteLine("Select only a single option from '--index', '--name', and '--uuid'.");
                return false;
            }

            if (OptionRGB && (OptionR || OptionG || OptionB))
            {
                Console.WriteLine("RGB option overwrites other options.");
                OptionR = false;
                OptionG = false;
                OptionB = false;
            }

            if (OptionRGBW && (OptionR || OptionG || OptionB))
            {
                Console.WriteLine("RGBW option overwrites other options.");
                OptionR = false;
                OptionG = false;
                OptionB = false;
            }

            return true;
        }

        #endregion
    }
}
