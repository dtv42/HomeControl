// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxApp.Commands
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
    using WallboxLib;
    using WallboxApp.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "WallboxApp",
             FullName = "Wallbox Application",
             Description = "A .NET core 2.2 console application.",
             ExtendedHelpText = "\nCopyright (c) 2019 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    [Subcommand("info", typeof(InfoCommand))]
    [Subcommand("read", typeof(ReadCommand))]
    [Subcommand("monitor", typeof(MonitorCommand))]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IWallbox _wallbox;

        #endregion

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionH { get; set; }
        private bool OptionP { get; set; }
        private bool OptionT { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion

        #region Public Properties

        [Option("--host <string>", "Sets the Wallbox host name.", CommandOptionType.SingleValue, Inherited = true)]
        public string HostName { get; set; } = string.Empty;

        [Option("--port <number>", "Sets the Wallbox host name.", CommandOptionType.SingleValue, Inherited = true)]
        public int Port { get; set; }

        [Option("--timeout <number>", "Sets the Wallbox receive timeout.", CommandOptionType.SingleValue, Inherited = true)]
        public double Timeout { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(IWallbox wallbox,
                           ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            // Setting default options from appsettings.json file.
            HostName = _settings.HostName;
            Port = _settings.Port;
            Timeout = _settings.Timeout;

            // Setting the Wallbox instance.
            _wallbox = wallbox;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when the root command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            Console.WriteLine($"Settings: {JsonConvert.SerializeObject(this, Formatting.Indented)}");

            try
            {
                if (CheckOptions(app))
                {
                    // Overriding Wallbox data options.
                    _wallbox.HostName = HostName;
                    _wallbox.Port = Port;
                    _wallbox.Timeout = Timeout;

                    if (await _wallbox.CheckAccess())
                    {
                        Console.WriteLine($"Wallbox UDP service with firmware '{_wallbox.Info.Firmware}' found at {HostName}:{Port}.");
                    }
                    else
                    {
                        Console.WriteLine($"Wallbox UDP service not found at {HostName}:{Port}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to Wallbox UDP service at {HostName}:{Port}.");
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
                    case "host": OptionH = option.HasValue(); break;
                    case "port": OptionP = option.HasValue(); break;
                    case "timeout": OptionT = option.HasValue(); break;
                }
            }

            return true;
        }

        #endregion
    }
}
