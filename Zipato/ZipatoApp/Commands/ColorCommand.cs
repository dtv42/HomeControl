// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorCommand.cs" company="DTV-Online">
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

    using CommandLine.Core.Hosting.Abstractions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoLib.Models.Data.Color;
    using ZipatoApp.Models;

    #endregion

    /// <summary>
    /// Application command setting RGB and RGBW colors on the Zipatile.
    /// </summary>
    [Command(Name = "color",
             FullName = "Zipato Color Command",
             Description = "Setting LED color of selected RGB and RGBW entities.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ColorCommand : BaseCommand<AppSettings>
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
        /// Command options.
        /// </summary>
        private bool OptionE { get; set; }
        private bool OptionI { get; set; }
        private bool OptionR { get; set; }
        private bool OptionG { get; set; }
        private bool OptionB { get; set; }
        private bool OptionW { get; set; }
        private bool OptionCW { get; set; }
        private bool OptionWW { get; set; }
        private bool OptionRGB { get; set; }
        private bool OptionRGBW { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions
        {
            get => !(OptionI ||
                     OptionR ||
                     OptionG ||
                     OptionB ||
                     OptionW ||
                     OptionCW ||
                     OptionWW ||
                     OptionRGB ||
                     OptionRGBW);
        }

        #endregion

        #region Public Properties

        [Uuid]
        [Required]
        [Argument(0, "endpoint (UUID)")]
        public string Endpoint { get; set; }

        [Option("-i|--intensity <value>", "LED intensity value.", CommandOptionType.SingleValue)]
        public byte Intensity { get; set; }

        [Option("-r|--red <value>", "Red color value (0-255).", CommandOptionType.SingleValue)]
        public byte Red { get; set; }

        [Option("-g|--green <value>", "Green color value (0-255).", CommandOptionType.SingleValue)]
        public byte Green { get; set; }

        [Option("-b|--blue <value>", "Blue color value (0-255).", CommandOptionType.SingleValue)]
        public byte Blue { get; set; }

        [Option("-w|--white <value>", "White color value (0-255).", CommandOptionType.SingleValue)]
        public byte White { get; set; }

        [Option("-cw|--coldwhite <value>", "Cold-White color value (0-255).", CommandOptionType.SingleValue)]
        public byte ColdWhite { get; set; }

        [Option("-ww|--warmwhite <value>", "Warm-White color value (0-255).", CommandOptionType.SingleValue)]
        public byte WarmWhite { get; set; }

        [Option("-rgb|--rgb <value>", "RGB color value (hex).", CommandOptionType.SingleValue)]
        public string RGB { get; set; } = string.Empty;

        [Option("-rgbw|--rgbw <value>", "RGBW color value (hex).", CommandOptionType.SingleValue)]
        public string RGBW { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ColorCommand(IZipato zipato,
                           ILogger<ColorCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ColorCommand()");

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

                    var (endpoints, status1) = await _zipato.ReadEndpointsAsync();
                    var (attributes, status2) = await _zipato.ReadAttributesAsync();

                    if ((status1.IsGood) && (status2.IsGood))
                    {
                        if (OptionRGB)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "rgb");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetColorAsync(attribute.Uuid.Value, RGB);
                            }
                            else
                            {
                                Console.WriteLine($"Attribute 'rgb' from endpoint '{Endpoint}' not found.");
                            }
                        }
                        else if (OptionRGBW)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "rgbw");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetColorAsync(attribute.Uuid.Value, RGBW);
                            }
                            else
                            {
                                Console.WriteLine($"Attribute 'rgbw' from endpoint '{Endpoint}' not found.");
                            }
                        }
                        else if (OptionR && OptionG && OptionB && OptionW)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "rgbw");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetColorAsync(attribute.Uuid.Value,
                                                                    new RGBW()
                                                                    {
                                                                        R = Red,
                                                                        G = Green,
                                                                        B = Blue,
                                                                        W = White
                                                                    });
                            }
                            else
                            {
                                Console.WriteLine($"RGBW attribute from endpoint '{Endpoint}' not found.");
                            }
                        }
                        else if (OptionR && OptionG && OptionB)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "rgb");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetColorAsync(attribute.Uuid.Value,
                                                                    new RGB()
                                                                    {
                                                                        R = Red,
                                                                        G = Green,
                                                                        B = Blue
                                                                    });
                            }
                            else
                            {
                                Console.WriteLine($"RGB attribute from endpoint '{Endpoint}' not found.");
                            }
                        }

                        if (OptionR)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "red");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetValueAsync(attribute.Uuid.Value, Red.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"RED attribute from endpoint '{Endpoint}' not found.");
                            }
                        }

                        if (OptionG)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "green");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetValueAsync(attribute.Uuid.Value, Green.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"GREEN attribute from endpoint '{Endpoint}' not found.");
                            }
                        }

                        if (OptionB)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "blue");

                            if (attribute != null)
                            {
                                var ok = _zipato.SetValueAsync(attribute.Uuid.Value, Blue.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"BLUE attribute from endpoint '{Endpoint}' not found.");
                            }
                        }

                        if (OptionI)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "intensity");

                            if ((attribute != null) && attribute.Uuid.HasValue)

                                if (attribute != null)
                            {
                                var ok = await _zipato.SetValueAsync(attribute.Uuid.Value, Intensity.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"INTENSITY attribute from endpoint '{Endpoint}' not found.");
                            }
                        }

                        if (OptionCW)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "coldWhite");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetValueAsync(attribute.Uuid.Value, ColdWhite.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"COLDWHITE attribute from endpoint '{Endpoint}' not found.");
                            }
                        }

                        if (OptionWW)
                        {
                            var attribute = _zipato.GetAttributeByName(new Guid(Endpoint), "warmWhite");

                            if ((attribute != null) && attribute.Uuid.HasValue)
                            {
                                var ok = await _zipato.SetValueAsync(attribute.Uuid.Value, WarmWhite.ToString());
                            }
                            else
                            {
                                Console.WriteLine($"WARMWHITE attribute from endpoint '{Endpoint}' not found.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No attribute extended data found.");
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception ColorCommand.");
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
                if (option.Inherited == false)
                {
                    switch (option.ShortName)
                    {
                        case "i": OptionI = option.HasValue(); break;
                        case "r": OptionR = option.HasValue(); break;
                        case "g": OptionG = option.HasValue(); break;
                        case "b": OptionB = option.HasValue(); break;
                        case "w": OptionW = option.HasValue(); break;
                        case "cw": OptionCW = option.HasValue(); break;
                        case "ww": OptionWW = option.HasValue(); break;
                        case "rgw": OptionRGB = option.HasValue(); break;
                        case "rgbw": OptionRGBW = option.HasValue(); break;
                    }
                }
            }

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
