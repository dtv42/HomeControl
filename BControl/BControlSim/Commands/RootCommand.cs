// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlSim.Commands
{
    #region Using Directives

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Threading;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;

    using NModbus;
    using NModbus.Extensions;

    using BaseClassLib;
    using BControlSim.Models;
    using BControlLib.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "BControlSim",
             FullName = "B-Control Simulation",
             Description = "A .NET core 2.1 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly SlaveStorage _storage = new SlaveStorage();
        public readonly BControlData _data = new BControlData();
        private Timer _timer;

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
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            Address = _settings.Slave.Address;
            Port = _settings.Slave.Port;
            SlaveID = _settings.Slave.ID;

            _data.Refresh(_settings.Data);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when the root command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public int OnExecuteAsync(CommandLineApplication app)
        {
            try
            {
                StartModbusTcpSlave();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception RootCommand Run()");
                Console.WriteLine($"Error starting the Modbus TCP client at {Address}:{Port}.");
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// The timer callback method.
        /// </summary>
        /// <param name="state">The timer callback state object.</param>
        public void OnTimerElapsed(object state)
        {
            _logger?.LogDebug($"TimedService timer elapsed.");
            UpdateDataStoreInputRegisters();
            UpdateDataStoreHoldingRegisters();
            _timer.Change(GetDueTime(), Timeout.Infinite);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Simple Modbus TCP slave example.
        /// </summary>
        private void StartModbusTcpSlave()
        {
            IPAddress ipaddress = IPAddress.Parse(Address);

            Console.WriteLine($"Listening on '{Address}:{Port}' at slave ID={SlaveID}.");

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(ipaddress, Port);
            slaveTcpListener.Start();

            IModbusFactory factory = new ModbusFactory();
            IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

            // Set the storage operation event handlers.
            _storage.InputRegisters.StorageOperationOccurred += InputRegistersStorageOperationOccurred;
            _storage.HoldingRegisters.StorageOperationOccurred += HoldingRegistersStorageOperationOccurred;

            // Start the update timer.
            _timer = new Timer(OnTimerElapsed, null, 0, -1);

            // create and start the Modbus slave
            IModbusSlave slave = factory.CreateSlave(SlaveID, _storage);
            network.AddSlave(slave);
            network.ListenAsync().GetAwaiter().GetResult();

            // prevent the main thread from exiting
            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// Handler to update the input register storage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputRegistersStorageOperationOccurred(object sender, StorageEventArgs<ushort> e)
        {
            _logger.LogDebug($"Input registers: {e.Operation} starting at {e.StartingAddress} for {e.Points.Length} points.");

            if (e.Operation == PointOperation.Read)
            {
                // NoOp - timer updates only.
            }
            else if (e.Operation == PointOperation.Write)
            {
                // NoOp - timer updates only.
            }
        }

        /// <summary>
        /// Handler to update the holding register storage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoldingRegistersStorageOperationOccurred(object sender, StorageEventArgs<ushort> e)
        {
            _logger.LogDebug($"Holding registers: {e.Operation} starting at {e.StartingAddress} for {e.Points.Length} points.");

            if (e.Operation == PointOperation.Read)
            {
                // NoOp - timer updates only.
            }
            else if (e.Operation == PointOperation.Write)
            {
                // NoOp - timer updates only.
            }
        }

        /// <summary>
        /// Updates the input register data store.
        /// </summary>
        private void UpdateDataStoreInputRegisters()
        {
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("InternalDataBlock1"), GetPoints(_data.InternalDataBlock1));
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("InternalDataBlock2"), GetPoints(_data.InternalDataBlock2));
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("EnergyDataBlock1"), GetPoints(_data.EnergyDataBlock1));
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("EnergyDataBlock2"), GetPoints(_data.EnergyDataBlock2));
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("EnergyDataBlock3"), GetPoints(_data.EnergyDataBlock3));
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("PnPDataBlock1"), _data.PnPDataBlock1);
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("SunSpecDataBlock1"), _data.SunSpecDataBlock1);
            _storage.InputRegisters.WritePoints(BControlData.GetOffset("SunSpecDataBlock2"), _data.SunSpecDataBlock2);
        }

        /// <summary>
        /// Updates the holding register data store.
        /// </summary>
        private void UpdateDataStoreHoldingRegisters()
        {
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("InternalDataBlock1"), GetPoints(_data.InternalDataBlock1));
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("InternalDataBlock2"), GetPoints(_data.InternalDataBlock2));
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("EnergyDataBlock1"), GetPoints(_data.EnergyDataBlock1));
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("EnergyDataBlock2"), GetPoints(_data.EnergyDataBlock2));
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("EnergyDataBlock3"), GetPoints(_data.EnergyDataBlock3));
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("PnPDataBlock1"), _data.PnPDataBlock1);
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("SunSpecDataBlock1"), _data.SunSpecDataBlock1);
            _storage.HoldingRegisters.WritePoints(BControlData.GetOffset("SunSpecDataBlock2"), _data.SunSpecDataBlock2);
        }

        /// <summary>
        /// Convert uint array to array of registers (points).
        /// </summary>
        /// <returns>An array of points (ushort).</returns>
        private ushort[] GetPoints(uint[] data)
        {
            ushort[] registers = new ushort[2 * data.Length];
            int index = 0;

            for (int i = 0; i < data.Length; i++)
            {
                ushort[] array = data[i].ToRegisters();
                registers[index++] = array[0];
                registers[index++] = array[1];
            }

            return registers;
        }

        /// <summary>
        /// Convert uint array to array of registers (points).
        /// </summary>
        /// <returns>An array of points (ushort).</returns>
        private ushort[] GetPoints(ulong[] data)
        {
            ushort[] registers = new ushort[4 * data.Length];
            int index = 0;

            for (int i = 0; i < data.Length; i++)
            {
                ushort[] array = data[i].ToRegisters();
                registers[index++] = array[0];
                registers[index++] = array[1];
                registers[index++] = array[2];
                registers[index++] = array[3];
            }

            return registers;
        }

        /// <summary>
        /// Returns the milliseconds to the next 10 seconds.
        /// </summary>
        /// <returns>The milliseconds to the next 10 seconds.</returns>
        private int GetDueTime()
        {
            DateTime now = DateTime.Now;
            var seconds = 10 * (int)Math.Ceiling((now.Second + now.Millisecond / 1000.0) / 10.0);
            if (seconds == 0) seconds = 10;
            return (seconds - now.Second) * 1000 - now.Millisecond;
        }

        #endregion
    }
}
