// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RtuCommand.cs" company="DTV-Online">
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
    using System.IO.Ports;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;
    using Newtonsoft.Json;

    using BaseClassLib;
    using NModbusLib;

    #endregion

    /// <summary>
    /// This class implements the RTU command.
    /// </summary>
    [Command(Name = "rtu", Description = "Supporting standard Modbus RTU operations.")]
    [HelpOption("-?|--help")]
    [Subcommand("read", typeof(RtuReadCommand))]
    [Subcommand("write", typeof(RtuWriteCommand))]
    [Subcommand("monitor", typeof(RtuMonitorCommand))]
    public class RtuCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IRtuClient _client;

        #endregion

        #region Public Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        public RootCommand Parent { get; set; }

        [Option("--com <string>", "Sets the Modbus master COM port.", CommandOptionType.SingleValue, Inherited = true)]
        public string SerialPort { get; set; } = string.Empty;

        [Option("--baudrate <number>", "Sets the Modbus COM port baud rate.", CommandOptionType.SingleValue, Inherited = true)]
        public int Baudrate { get; set; } = 9600;

        [Option("--parity <string>", "Sets the Modbus COM port parity.", CommandOptionType.SingleValue, Inherited = true)]
        public Parity Parity { get; set; } = Parity.None;

        [Option("--databits <number>", "Sets the Modbus COM port data bits.", CommandOptionType.SingleValue, Inherited = true)]
        public int DataBits { get; set; } = 8;

        [Option("--stopbits <string>", "Sets the Modbus COM port stop bits.", CommandOptionType.SingleValue, Inherited = true)]
        public StopBits StopBits { get; set; } = StopBits.One;

        [Option("--slaveid <number>", "Sets the Modbus slave ID.", CommandOptionType.SingleValue, Inherited = true)]
        public byte SlaveID { get; set; }

        public int ReadTimeout { get; set; }

        public int WriteTimeout { get; set; }

        public bool OptionSettings { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RtuCommand"/> class.
        /// Selected properties are initialized with data from the AppSettings instance.
        /// </summary>
        /// <param name="client">The Modbus client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RtuCommand(IRtuClient client,
                          ILogger<RtuCommand> logger,
                          IOptions<AppSettings> options,
                          IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger.LogDebug("RtuCommand()");

            SerialPort = _settings.RtuMaster.SerialPort;
            Baudrate = _settings.RtuMaster.Baudrate;
            Parity = _settings.RtuMaster.Parity;
            DataBits = _settings.RtuMaster.DataBits;
            StopBits = _settings.RtuMaster.StopBits;
            ReadTimeout = _settings.RtuMaster.ReadTimeout;
            WriteTimeout = _settings.RtuMaster.WriteTimeout;
            SlaveID = _settings.RtuSlave.ID;

            // Setting the RTU client instance.
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
                Console.WriteLine($"RtuMaster: {JsonConvert.SerializeObject(_settings.RtuMaster, Formatting.Indented)}");
                Console.WriteLine($"RtuSlave: {JsonConvert.SerializeObject(_settings.RtuSlave, Formatting.Indented)}");
            }

            try
            {
                // Overriding RTU client options.
                _client.RtuMaster.SerialPort = SerialPort;
                _client.RtuMaster.Baudrate = Baudrate;
                _client.RtuMaster.Parity = Parity;
                _client.RtuMaster.DataBits = DataBits;
                _client.RtuMaster.StopBits = StopBits;
                _client.RtuMaster.ReadTimeout = ReadTimeout;
                _client.RtuMaster.WriteTimeout = WriteTimeout;
                _client.RtuSlave.ID = SlaveID;

                if (_client.Connect())
                {
                    Console.WriteLine($"RTU serial port found at {SerialPort}.");
                    return 0;
                }
                else
                {
                    Console.WriteLine($"RTU serial port not found at {SerialPort}.");
                    return 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RtuCommand - {ex.Message}");
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
