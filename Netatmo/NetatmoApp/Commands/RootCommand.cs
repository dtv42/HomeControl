// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoApp.Commands
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
    using NetatmoLib;
    using NetatmoApp.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "NetatmoApp",
             FullName = "Netatmo Application",
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

        private readonly INetatmo _netatmo;

        #endregion

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionA { get; set; }
        private bool OptionT { get; set; }
        private bool OptionU { get; set; }
        private bool OptionP { get; set; }
        private bool OptionC { get; set; }
        private bool OptionS { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion

        #region Public Properties

        [Option("--address <string>", "Sets the Netatmo base address.", CommandOptionType.SingleValue, Inherited = true)]
        public string BaseAddress { get; set; } = string.Empty;

        [Option("--timeout <number>", "Sets the web service request time out in seconds.", CommandOptionType.SingleValue, Inherited = true)]
        public int Timeout { get; set; } = 10;

        [Option("--user <string>", "Sets the Netatmo user name.", CommandOptionType.SingleValue, Inherited = true)]
        public string User { get; set; } = string.Empty;

        [Option("--password <string>", "Sets the Netatmo user password.", CommandOptionType.SingleValue, Inherited = true)]
        public string Password { get; set; } = string.Empty;

        [Option("--client <string>", "Sets the Netatmo client id.", CommandOptionType.SingleValue, Inherited = true)]
        public string ClientID { get; set; } = string.Empty;

        [Option("--secret <string>", "Sets the Netatmo client secret.", CommandOptionType.SingleValue, Inherited = true)]
        public string ClientSecret { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(INetatmo netatmo,
                           ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            // Setting default options from appsettings.json file.
            BaseAddress = _settings.BaseAddress;
            Timeout = _settings.Timeout;
            User = _settings.User;
            Password = _settings.Password;
            ClientID = _settings.ClientID;
            ClientSecret = _settings.ClientSecret;

            // Setting the Netatmo instance.
            _netatmo = netatmo;
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
                    // Overriding Netatmo data options.
                    _netatmo.BaseAddress = BaseAddress;
                    _netatmo.Timeout = Timeout;
                    _netatmo.User = User;
                    _netatmo.Password = Password;
                    _netatmo.ClientID = ClientID;
                    _netatmo.ClientSecret = ClientSecret;

                    if (_netatmo.CheckAccess())
                    {
                        Console.WriteLine($"Netatmo web service found at {BaseAddress}.");
                    }
                    else
                    {
                        Console.WriteLine($"Netatmo web service not found at {BaseAddress}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to Netatmo web service at {BaseAddress}.");
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
                    case "user": OptionU = option.HasValue(); break;
                    case "password": OptionP = option.HasValue(); break;
                    case "client": OptionC = option.HasValue(); break;
                    case "secret": OptionS = option.HasValue(); break;
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
