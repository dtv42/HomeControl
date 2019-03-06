// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusApp.Commands
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using FroniusLib;
    using FroniusApp.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "FroniusApp",
             FullName = "Fronius Application",
             Description = "A .NET core 2.2 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    [Subcommand("info", typeof(InfoCommand))]
    [Subcommand("read", typeof(ReadCommand))]
    [Subcommand("monitor", typeof(MonitorCommand))]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IFronius _fronius;

        #endregion

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionA { get; set; }
        private bool OptionT { get; set; }
        private bool OptionD { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion

        #region Public Properties

        [Option("--address <string>", "Sets the Fronius base address.", CommandOptionType.SingleValue, Inherited = true)]
        public string BaseAddress { get; set; } = string.Empty;

        [Option("--timeout <number>", "Sets the web service request time out in seconds.", CommandOptionType.SingleValue, Inherited = true)]
        public int Timeout { get; set; } = 10;

        [Option("--device <string>", "Sets the Fronius device id.", CommandOptionType.SingleValue, Inherited = true)]
        public string DeviceID { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(IFronius fronius,
                           ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            // Setting default options from appsettings.json file.
            BaseAddress = _settings.BaseAddress;
            Timeout = _settings.Timeout;
            DeviceID = _settings.DeviceID;

            // Setting the Fronius instance.
            _fronius = fronius;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when the root command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public int OnExecute(CommandLineApplication app)
        {
            Console.WriteLine($"Settings: {JsonConvert.SerializeObject(this, Formatting.Indented)}");

            try
            {
                if (CheckOptions(app))
                {
                    // Overriding Fronius data options.
                    _fronius.BaseAddress = BaseAddress;
                    _fronius.Timeout = Timeout;
                    _fronius.DeviceID = DeviceID;

                    if (_fronius.CheckAccess())
                    {
                        Console.WriteLine($"Fronius web service with version '{_fronius.VersionInfo.APIVersion}' found at {BaseAddress}.");
                    }
                    else
                    {
                        Console.WriteLine($"Fronius web service not found at {BaseAddress}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to Fronius web service at {BaseAddress}.");
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
            // Getting additional option flags.
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                switch (option.LongName)
                {
                    case "address": OptionA = option.HasValue(); break;
                    case "timeout": OptionT = option.HasValue(); break;
                    case "deviceid": OptionD = option.HasValue(); break;
                }
            }

            if (OptionA)
            {
                if (!Uri.TryCreate(BaseAddress, UriKind.Absolute, out Uri result))
                {
                    Console.WriteLine($"Invalid base address (--address) specified.");
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
