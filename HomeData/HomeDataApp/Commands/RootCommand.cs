// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataApp.Commands
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
    using HomeDataLib;
    using HomeDataApp.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "HomeDataApp",
             FullName = "HomeData Application",
             Description = "A .NET core 2.2 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    [Subcommand("info", typeof(InfoCommand))]
    [Subcommand("monitor", typeof(MonitorCommand))]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IHomeData _homedata;

        #endregion Private Data Members

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool Option1 { get; set; }
        private bool Option2 { get; set; }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion

        #region Public Properties

        [Option("--timeout <number>", "Sets the web service request time out in seconds.", CommandOptionType.SingleValue, Inherited = true)]
        public int Timeout { get; set; } = 10;

        [Option("--meter1 <string>", "Sets the first base address (consumption).", CommandOptionType.SingleValue, Inherited = true)]
        public string Meter1Address { get; set; } = string.Empty;

        [Option("--meter2 <string>", "Sets the second base address (generation).", CommandOptionType.SingleValue, Inherited = true)]
        public string Meter2Address { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="homedata">The HomeData instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(IHomeData homedata,
                           ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            // Setting default options from appsettings.json file.
            Meter1Address = _settings.Meter1Address;
            Meter2Address = _settings.Meter2Address;

            // Setting the HomeData instance.
            _homedata = homedata;
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
                    // Overriding HomeData data options.
                    _homedata.Timeout = Timeout;
                    _homedata.Meter1Address = Meter1Address;
                    _homedata.Meter2Address = Meter2Address;

                    if (await _homedata.CheckAccess())
                    {
                        Console.WriteLine($"EM300LR web services found.");
                    }
                    else
                    {
                        Console.WriteLine($"EM300LR web services not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to EM300LR web services.");
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
                    case "meter1": Option1 = option.HasValue(); break;
                    case "meter2": Option2 = option.HasValue(); break;
                }
            }

            if (Option1)
            {
                if (!Uri.TryCreate(Meter1Address, UriKind.Absolute, out Uri result))
                {
                    Console.WriteLine($"Invalid first meter base address (--meter1) specified.");
                    return false;
                }
            }

            if (Option2)
            {
                if (!Uri.TryCreate(Meter2Address, UriKind.Absolute, out Uri result))
                {
                    Console.WriteLine($"Invalid second meter base address (--meter2) specified.");
                    return false;
                }
            }

            return true;
        }

        #endregion Private Methods
    }
}
