// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpCommand.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusApp.Commands
{
    #region Using Directives

    using System;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;
    using Newtonsoft.Json;

    using BaseClassLib;
    using NModbusLib;

    #endregion

    /// <summary>
    /// This class implements the TCP command.
    /// </summary>
    [Command(Name = "tcp", Description = "Supporting standard Modbus TCP operations.")]
    [HelpOption("-?|--help")]
    [Subcommand("read", typeof(TcpReadCommand))]
    [Subcommand("write", typeof(TcpWriteCommand))]
    [Subcommand("monitor", typeof(TcpMonitorCommand))]
    public class TcpCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly ITcpClient _client;

        #endregion

        #region Public Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        public RootCommand Parent { get; set; }

        [Option("--address <string>", "Sets the Modbus slave IP address.", CommandOptionType.SingleValue, Inherited = true)]
        public string Address { get; set; } = string.Empty;

        [Option("--port <number>", "Sets the Modbus slave port number.", CommandOptionType.SingleValue, Inherited = true)]
        public int Port { get; set; }

        [Option("--slaveid", "Sets the Modbus slave ID.", CommandOptionType.SingleValue, Inherited = true)]
        public byte SlaveID { get; set; }

        public int ReceiveTimeout { get; set; }

        public int SendTimeout { get; set; }

        public bool OptionSettings { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommand"/> class.
        /// Selected properties are initialized with data from the AppSettings instance.
        /// </summary>
        /// <param name="client">The Modbus client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public TcpCommand(ITcpClient client,
                          ILogger<TcpCommand> logger,
                          IOptions<AppSettings> options,
                          IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger.LogDebug("TcpCommand()");

            ReceiveTimeout = _settings.TcpMaster.ReceiveTimeout;
            SendTimeout = _settings.TcpMaster.SendTimeout;
            Address = _settings.TcpSlave.Address;
            Port = _settings.TcpSlave.Port;
            SlaveID = _settings.TcpSlave.ID;

            // Setting the TCP client instance.
            _client = client;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is called processing the tcp command.
        /// </summary>
        /// <returns></returns>
        public int OnExecute()
        {
            _logger.LogDebug("OnExecute()");

            // Setting default options from appsettings.json file.
            OptionSettings = Parent.OptionSettings;

            if (OptionSettings)
            {
                Console.WriteLine($"TcpMaster: {JsonConvert.SerializeObject(_settings.TcpMaster, Formatting.Indented)}");
                Console.WriteLine($"TcpSlave: {JsonConvert.SerializeObject(_settings.TcpSlave, Formatting.Indented)}");
            }

            try
            {
                // Overriding TCP client options.
                _client.TcpMaster.ReceiveTimeout = ReceiveTimeout;
                _client.TcpMaster.SendTimeout = SendTimeout;
                _client.TcpSlave.Address = Address;
                _client.TcpSlave.Port = Port;
                _client.TcpSlave.ID = SlaveID;

                if (_client.Connect())
                {
                    Console.WriteLine($"Modbus TCP slave found at {Address}:{Port}.");
                    return 0;
                }
                else
                {
                    Console.WriteLine($"Modbus TCP slave not found at {Address}:{Port}.");
                    return 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"TcpCommand - {ex.Message}");
                return -1;
            }
            finally
            {
                if (_client.Connected)
                {
                    _client.Disconnect();
                }
            }
        }

        #endregion
    }
}
