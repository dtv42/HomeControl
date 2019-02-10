// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
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
    using System.Reflection;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoApp.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "ZipatoApp",
             FullName = "Zipato Application",
             Description = "A .NET core 2.2 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    [Subcommand("find", typeof(FindCommand))]
    [Subcommand("info", typeof(InfoCommand))]
    [Subcommand("read", typeof(ReadCommand))]
    [Subcommand("value", typeof(ValueCommand))]
    [Subcommand("write", typeof(WriteCommand))]
    [Subcommand("color", typeof(ColorCommand))]
    [Subcommand("clean", typeof(CleanCommand))]
    [Subcommand("delete", typeof(DeleteCommand))]
    [Subcommand("monitor", typeof(MonitorCommand))]
    [Subcommand("others", typeof(OthersCommand))]
    [Subcommand("devices", typeof(DevicesCommand))]
    [Subcommand("sensors", typeof(SensorsCommand))]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionA { get; set; }
        private bool OptionT { get; set; }
        private bool OptionU { get; set; }
        private bool OptionP { get; set; }
        private bool OptionL { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion

        #region Public Properties

        [Option("--address <string>", "Sets the Zipato base address.", CommandOptionType.SingleValue, Inherited = true)]
        public string BaseAddress { get; set; } = string.Empty;

        [Option("--timeout <number>", "Sets the web service request time out in seconds.", CommandOptionType.SingleValue, Inherited = true)]
        public int Timeout { get; set; } = 10;

        [Option("--user <string>", "Sets the Zipato user name.", CommandOptionType.SingleValue, Inherited = true)]
        public string User { get; set; } = string.Empty;

        [Option("--password <string>", "Sets the Zipato user password.", CommandOptionType.SingleValue, Inherited = true)]
        public string Password { get; set; } = string.Empty;

        [Option("--islocal", "Sets the Zipato access type.", CommandOptionType.NoValue, Inherited = true)]
        public bool IsLocal { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(IZipato zipato,
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
            IsLocal = _settings.IsLocal;

            // Setting the Zipato instance.
            _zipato = zipato;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when the root command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public int OnExecuteAsync(CommandLineApplication app)
        {
            Console.WriteLine($"Settings: {JsonConvert.SerializeObject(this, Formatting.Indented)}");

            try
            {
                if (CheckOptions(app))
                {
                    // Overriding Zipato data options.
                    _zipato.BaseAddress = BaseAddress;
                    _zipato.Timeout = Timeout;
                    _zipato.User = User;
                    _zipato.Password = Password;
                    _zipato.IsLocal = IsLocal;

                    if (_zipato.CheckUserSession())
                    {
                        Console.WriteLine($"Zipato web service found at {BaseAddress}.");
                    }
                    else
                    {
                        Console.WriteLine($"Zipato web service not found at {BaseAddress}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to Zipato web service at {BaseAddress}.");
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
                    case "islocal": OptionL = option.HasValue(); break;
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
