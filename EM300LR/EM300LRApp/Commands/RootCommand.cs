// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRApp.Commands
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
    using EM300LRLib;
    using EM300LRApp.Models;

    #endregion Using Directives

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "EM300LRApp",
             FullName = "EM300LR Application",
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

        private readonly IEM300LR _em300lr;

        #endregion Private Data Members

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionA { get; set; }
        private bool OptionT { get; set; }
        private bool OptionP { get; set; }
        private bool OptionS { get; set; }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion Private Methods

        #region Public Properties

        [Option("--address <string>", "Sets the EM300LR base address.", CommandOptionType.SingleValue, Inherited = true)]
        public string BaseAddress { get; set; } = string.Empty;

        [Option("--timeout <number>", "Sets the test web service request time out in seconds.", CommandOptionType.SingleValue, Inherited = true)]
        public int Timeout { get; set; } = 10;

        [Option("--password <string>", "Sets the EM300LR password.", CommandOptionType.SingleValue, Inherited = true)]
        public string Password { get; set; } = string.Empty;

        [Option("--serialnumber <string>", "Sets the EM300LR serial number.", CommandOptionType.SingleValue, Inherited = true)]
        public string SerialNumber { get; set; } = string.Empty;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="em300lr">The EM300LR instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(IEM300LR em300lr,
                           ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            // Setting default options from appsettings.json file.
            BaseAddress = _settings.BaseAddress;
            Timeout = _settings.Timeout;
            Password = _settings.Password;
            SerialNumber = _settings.SerialNumber;

            // Setting the EM300LR instance.
            _em300lr = em300lr;
        }

        #endregion Constructors

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
                    // Overriding EM300LR data options.
                    _em300lr.BaseAddress = BaseAddress;
                    _em300lr.Timeout = Timeout;
                    _em300lr.Password = Password;
                    _em300lr.SerialNumber = SerialNumber;

                    if (await _em300lr.CheckAccess())
                    {
                        Console.WriteLine($"EM300LR web service with serial number '{_em300lr.SerialNumber}' found at {BaseAddress}.");
                    }
                    else
                    {
                        Console.WriteLine($"EM300LR web service with serial number '{_em300lr.SerialNumber}' not found at {BaseAddress}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to EM300LR web service at {BaseAddress}.");
                return -1;
            }

            return 0;
        }

        #endregion Public Methods

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
                    case "password": OptionP = option.HasValue(); break;
                    case "serialnumber": OptionS = option.HasValue(); break;
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

        #endregion Private Methods
    }
}