// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MApp.Commands
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
    using SYMO823MLib;
    using SYMO823MApp.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "SYMO823MApp",
             FullName = "SYMO823M Application",
             Description = "A .NET core 2.1 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    [Subcommand("info", typeof(InfoCommand))]
    [Subcommand("read", typeof(ReadCommand))]
    [Subcommand("write", typeof(WriteCommand))]
    [Subcommand("monitor", typeof(MonitorCommand))]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly ISYMO823M _symo823m;

        #endregion

        #region Private Properties

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionA { get; set; }
        private bool OptionP { get; set; }
        private bool OptionI { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion

        #region Public Properties

        [Option("--address <string>", "Sets the Modbus host IP address.", CommandOptionType.SingleValue, Inherited = true)]
        public string Address { get; set; } = string.Empty;

        [Option("--port <number>", "Sets the Modbus port number.", CommandOptionType.SingleValue, Inherited = true)]
        public int Port { get; set; }

        [Option("--slaveid", "Sets the Modbus slave id.", CommandOptionType.SingleValue, Inherited = true)]
        public byte SlaveID { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(ISYMO823M symo823m,
                           ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            // Setting default options from appsettings.json file.
            Address = _settings.TcpSlave.Address;
            Port = _settings.TcpSlave.Port;
            SlaveID = _settings.TcpSlave.ID;

            // Setting the SYMO823M instance.
            _symo823m = symo823m;
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
                    // Overriding SYMO823M options.
                    _symo823m.Slave.Address = Address;
                    _symo823m.Slave.Port = Port;
                    _symo823m.Slave.ID = SlaveID;

                    if (_symo823m.Connect())
                    {
                        Console.WriteLine($"Modbus TCP client found at {Address}:{Port}.");
                    }
                    else
                    {
                        Console.WriteLine($"Modbus TCP client not found at {Address}:{Port}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error connecting to Modbus TCP client at {Address}:{Port}.");
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
                    case "port": OptionP = option.HasValue(); break;
                    case "slaveid": OptionI = option.HasValue(); break;
                }
            }

            return true;
        }

        #endregion
    }
}
