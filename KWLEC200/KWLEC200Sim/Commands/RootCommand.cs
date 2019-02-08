// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Sim.Commands
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
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
    using KWLEC200Sim.Models;
    using KWLEC200Lib.Models;

    #endregion

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "KWLEC200Sim",
             FullName = "KWLEC200 Simulation",
             Description = "A .NET core 2.1 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-?|--help")]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private const ushort OFFSET = 1;

        private readonly Dictionary<string, string> _commands = new Dictionary<string, string>
        {
            { "v00000", "ItemDescription" },
            { "v00001", "OrderNumber" },
            { "v00002", "MacAddress" },
            { "v00003", "Language" },
            { "v00004", "Date" },
            { "v00005", "Time" },
            { "v00006", "DayLightSaving" },
            { "v00007", "AutoUpdateEnabled" },
            { "v00008", "PortalAccessEnabled" },
            { "v00012", "ExhaustVentilatorVoltageLevel1" },
            { "v00013", "SupplyVentilatorVoltageLevel1" },
            { "v00014", "ExhaustVentilatorVoltageLevel2" },
            { "v00015", "SupplyVentilatorVoltageLevel2" },
            { "v00016", "ExhaustVentilatorVoltageLevel3" },
            { "v00017", "SupplyVentilatorVoltageLevel3" },
            { "v00018", "ExhaustVentilatorVoltageLevel4" },
            { "v00019", "SupplyVentilatorVoltageLevel4" },
            { "v00020", "MinimumVentilationLevel" },
            { "v00021", "KwlBeEnabled" },
            { "v00022", "KwlBecEnabled" },
            { "v00023", "DeviceConfiguration" },
            { "v00024", "PreheaterStatus" },
            { "v00025", "KwlFTFConfig0" },
            { "v00026", "KwlFTFConfig1" },
            { "v00027", "KwlFTFConfig2" },
            { "v00028", "KwlFTFConfig3" },
            { "v00029", "KwlFTFConfig4" },
            { "v00030", "KwlFTFConfig5" },
            { "v00031", "KwlFTFConfig6" },
            { "v00032", "KwlFTFConfig7" },
            { "v00033", "HumidityControlStatus" },
            { "v00034", "HumidityControlTarget" },
            { "v00035", "HumidityControlStep" },
            { "v00036", "HumidityControlStop" },
            { "v00037", "CO2ControlStatus" },
            { "v00038", "CO2ControlTarget" },
            { "v00039", "CO2ControlStep" },
            { "v00040", "VOCControlStatus" },
            { "v00041", "VOCControlTarget" },
            { "v00042", "VOCControlStep" },
            { "v00043", "ThermalComfortTemperature" },
            { "v00051", "TimeZoneOffset" },
            { "v00052", "DateFormat" },
            { "v00053", "HeatExchangerType" },
            { "v00091", "PartyOperationDuration" },
            { "v00092", "PartyVentilationLevel" },
            { "v00093", "PartyOperationRemaining" },
            { "v00094", "PartyOperationActivate" },
            { "v00096", "StandbyOperationDuration" },
            { "v00097", "StandbyVentilationLevel" },
            { "v00098", "StandbyOperationRemaining" },
            { "v00099", "StandbyOperationActivate" },
            { "v00101", "OperationMode" },
            { "v00102", "VentilationLevel" },
            { "v00103", "VentilationPercentage" },
            { "v00104", "TemperatureOutdoor" },
            { "v00105", "TemperatureSupply" },
            { "v00106", "TemperatureExhaust" },
            { "v00107", "TemperatureExtract" },
            { "v00108", "TemperaturePreHeater" },
            { "v00110", "TemperaturePostHeater" },
            { "v00111", "ExternalHumiditySensor1" },
            { "v00112", "ExternalHumiditySensor2" },
            { "v00113", "ExternalHumiditySensor3" },
            { "v00114", "ExternalHumiditySensor4" },
            { "v00115", "ExternalHumiditySensor5" },
            { "v00116", "ExternalHumiditySensor6" },
            { "v00117", "ExternalHumiditySensor7" },
            { "v00118", "ExternalHumiditySensor8" },
            { "v00119", "ExternalHumidityTemperature1" },
            { "v00120", "ExternalHumidityTemperature2" },
            { "v00121", "ExternalHumidityTemperature3" },
            { "v00122", "ExternalHumidityTemperature4" },
            { "v00123", "ExternalHumidityTemperature5" },
            { "v00124", "ExternalHumidityTemperature6" },
            { "v00125", "ExternalHumidityTemperature7" },
            { "v00126", "ExternalHumidityTemperature8" },
            { "v00128", "ExternalCO2Sensor1" },
            { "v00129", "ExternalCO2Sensor2" },
            { "v00130", "ExternalCO2Sensor3" },
            { "v00131", "ExternalCO2Sensor4" },
            { "v00132", "ExternalCO2Sensor5" },
            { "v00133", "ExternalCO2Sensor6" },
            { "v00134", "ExternalCO2Sensor7" },
            { "v00135", "ExternalCO2Sensor8" },
            { "v00136", "ExternalVOCSensor1" },
            { "v00137", "ExternalVOCSensor2" },
            { "v00138", "ExternalVOCSensor3" },
            { "v00139", "ExternalVOCSensor4" },
            { "v00140", "ExternalVOCSensor5" },
            { "v00141", "ExternalVOCSensor6" },
            { "v00142", "ExternalVOCSensor7" },
            { "v00143", "ExternalVOCSensor8" },
            { "v00146", "TemperatureChannel" },
            { "v00201", "WeeklyProfile" },
            { "v00303", "SerialNumber" },
            { "v00304", "ProductionCode" },
            { "v00348", "SupplyFanSpeed" },
            { "v00349", "ExhaustFanSpeed" },
            { "v00403", "Logout" },
            { "v00601", "VacationOperation" },
            { "v00602", "VacationVentilationLevel" },
            { "v00603", "VacationStartDate" },
            { "v00604", "VacationEndDate" },
            { "v00605", "VacationInterval" },
            { "v00606", "VacationDuration" },
            { "v01010", "PreheaterType" },
            { "v01017", "KwlFunctionType" },
            { "v01019", "HeaterAfterRunTime" },
            { "v01020", "ExternalContact" },
            { "v01021", "FaultTypeOutput" },
            { "v01031", "FilterChange" },
            { "v01032", "FilterChangeInterval" },
            { "v01033", "FilterChangeRemaining" },
            { "v01035", "BypassRoomTemperature" },
            { "v01036", "BypassOutdoorTemperature" },
            { "v01037", "BypassOutdoorTemperature2" },
            { "v01041", "StartReset" },
            { "v01042", "FactoryReset" },
            { "v01050", "SupplyLevel" },
            { "v01051", "ExhaustLevel" },
            { "v01061", "FanLevelRegion02" },
            { "v01062", "FanLevelRegion24" },
            { "v01063", "FanLevelRegion46" },
            { "v01064", "FanLevelRegion68" },
            { "v01065", "FanLevelRegion80" },
            { "v01066", "OffsetExhaust" },
            { "v01068", "FanLevelConfiguration" },
            { "v01071", "SensorName1" },
            { "v01072", "SensorName2" },
            { "v01073", "SensorName3" },
            { "v01074", "SensorName4" },
            { "v01075", "SensorName5" },
            { "v01076", "SensorName6" },
            { "v01077", "SensorName7" },
            { "v01078", "SensorName8" },
            { "v01081", "CO2SensorName1" },
            { "v01082", "CO2SensorName2" },
            { "v01083", "CO2SensorName3" },
            { "v01084", "CO2SensorName4" },
            { "v01085", "CO2SensorName5" },
            { "v01086", "CO2SensorName6" },
            { "v01087", "CO2SensorName7" },
            { "v01088", "CO2SensorName8" },
            { "v01091", "VOCSensorName1" },
            { "v01092", "VOCSensorName2" },
            { "v01093", "VOCSensorName3" },
            { "v01094", "VOCSensorName4" },
            { "v01095", "VOCSensorName5" },
            { "v01096", "VOCSensorName6" },
            { "v01097", "VOCSensorName7" },
            { "v01098", "VOCSensorName8" },
            { "v01101", "SoftwareVersion" },
            { "v01103", "OperationMinutesSupply" },
            { "v01104", "OperationMinutesExhaust" },
            { "v01105", "OperationMinutesPreheater" },
            { "v01106", "OperationMinutesAfterheater" },
            { "v01108", "PowerPreheater" },
            { "v01109", "PowerAfterheater" },
            { "v01120", "ResetFlag" },
            { "v01123", "ErrorCode" },
            { "v01124", "WarningCode" },
            { "v01125", "InfoCode" },
            { "v01300", "NumberOfErrors" },
            { "v01301", "NumberOfWarnings" },
            { "v01302", "NumberOfInfos" },
            { "v01303", "Errors" },
            { "v01304", "Warnings" },
            { "v01305", "Infos" },
            { "v01306", "StatusFlags" },
            //{ "v02013", "GlobalUpdate" },
            //{ "v02014", "LastError" },
            { "v02015", "ClearError" },
            { "v02020", "SensorConfig1" },
            { "v02021", "SensorConfig2" },
            { "v02022", "SensorConfig3" },
            { "v02023", "SensorConfig4" },
            { "v02024", "SensorConfig5" },
            { "v02025", "SensorConfig6" },
            { "v02026", "SensorConfig7" },
            { "v02027", "SensorConfig8" }
        };

        private SlaveStorage _storage = new SlaveStorage();

        private string _property;

        private bool _pending;

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

        [Option("--slave", "Sets the Modbus slave id.", CommandOptionType.SingleValue, Inherited = true)]
        public byte Slave { get; set; }

        public KWLEC200Data Data { get; set; } = new KWLEC200Data();

        public Helios Helios { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="helioslogger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(ILogger<RootCommand> logger,
                           ILogger<Helios> helioslogger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            Address = _settings.Address;
            Port = _settings.Port;
            Slave = _settings.Slave;
            Data = _settings.Data;
            Helios = new Helios(helioslogger);
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

        #endregion

        #region Private Methods

        /// <summary>
        ///     Simple Modbus TCP slave example.
        /// </summary>
        private void StartModbusTcpSlave()
        {
            int port = 504;
            byte slaveid = 180;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            Console.WriteLine($"Listening on '{address}:{port}' at slave ID={slaveid}.");

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(address, port);
            slaveTcpListener.Start();

            IModbusFactory factory = new ModbusFactory();
            IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

            _storage.HoldingRegisters.StorageOperationOccurred += HoldingRegistersStorageOperationOccurred;

            IModbusSlave slave = factory.CreateSlave(slaveid, _storage);
            network.AddSlave(slave);
            network.ListenAsync().GetAwaiter().GetResult();

            // prevent the main thread from exiting
            Thread.Sleep(Timeout.Infinite);
        }

        private void HoldingRegistersStorageOperationOccurred(object sender, StorageEventArgs<ushort> e)
        {
            string command = e.Points.ToASCII();
            _logger?.LogDebug($"KWLEC command: '{command}', operation: {e.Operation}");

            if (e.Operation == PointOperation.Write)
            {
                if (!_pending)
                {
                    // Check for write command.
                    if (command.Contains('='))
                    {
                        var commandparts = command.Split('=');

                        if (commandparts.Length == 2)
                        {
                            string property = _commands[commandparts[0]];
                            UpdateData(property, command);
                        }
                    }
                    // Read command received (variable name).
                    else
                    {
                        // Get property name using the variable name.
                        _property = _commands[command];
                        ReturnData(_property);
                    }
                }
                else
                {
                    _pending = false;
                }
            }
        }

        /// <summary>
        /// Helper method to write the property value to the storage.
        /// </summary>
        /// <param name="property">The property name.</param>
        private void ReturnData(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                if (KWLEC200Data.IsProperty(property))
                {
                    if (KWLEC200Data.IsReadable(property))
                    {
                        object value = Data.GetPropertyValue(property);
                        string name = KWLEC200Data.GetName(property);
                        ushort size = KWLEC200Data.GetSize(property);
                        ushort count = KWLEC200Data.GetCount(property);
                        string text = string.Empty;

                        _logger?.LogDebug($"Property '{property}' => Type: {value.GetType()}, Name: {name}, Size: {size}, Count: {count}.");

                        if (value is bool)
                        {
                            text = Helios.GetBoolData(name, size, count, (bool)value);
                        }
                        else if (value is int)
                        {
                            text = Helios.GetIntegerData(name, size, count, (int)value);
                        }
                        else if (value is double)
                        {
                            text = Helios.GetDoubleData(name, size, count, (double)value);
                        }
                        else if (value is DateTime)
                        {
                            text = Helios.GetDateData(name, size, count, (DateTime)value);
                        }
                        else if (value is TimeSpan)
                        {
                            text = Helios.GetTimeData(name, size, count, (TimeSpan)value);
                        }
                        else if (((dynamic)value).GetType().IsEnum)
                        {
                            text = Helios.GetIntegerData(name, size, count, (int)value);
                        }
                        else if (value is string)
                        {
                            text = Helios.GetStringData(name, size, count, (string)value);
                        }

                        if (!string.IsNullOrEmpty(text))
                        {
                            _pending = true;
                            _storage.HoldingRegisters.WritePoints(OFFSET, text.ToRegisters());
                            _logger?.LogDebug($"Property '{property}' => '{text}'.");
                        }
                    }
                    else
                    {
                        _logger?.LogDebug($"ReturnData property '{property}' not readable.");
                    }
                }
                else
                {
                    _logger?.LogWarning($"ReturnData property '{property}' not found.");
                }
            }
            else
            {
                _logger?.LogError($"ReturnData property invalid.");
            }
        }

        /// <summary>
        /// Helper method to read the property value from the storage.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="command">The raw command string.</param>
        public void UpdateData(string property, string command)
        {
            if (!string.IsNullOrEmpty(property))
            {
                if (KWLEC200Data.IsProperty(property))
                {
                    if (KWLEC200Data.IsWritable(property))
                    {
                        object value = Data.GetPropertyValue(property);
                        string name = KWLEC200Data.GetName(property);
                        ushort size = KWLEC200Data.GetSize(property);
                        ushort count = KWLEC200Data.GetCount(property);

                        _logger?.LogDebug($"Property '{property}' => Type: {value.GetType()}, Name: {name}, Size: {size}, Count: {count}.");

                        if (value is bool)
                        {
                            var (data, status) = Helios.ParseBoolData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                        else if (value is int)
                        {
                            var (data, status) = Helios.ParseIntegerData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                        else if (value is double)
                        {
                            var (data, status) = Helios.ParseDoubleData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                        else if (value is DateTime)
                        {
                            var (data, status) = Helios.ParseDateData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                        else if (value is TimeSpan)
                        {
                            var (data, status) = Helios.ParseTimeData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                        else if (value.GetType().IsEnum)
                        {
                            var (data, status) = Helios.ParseIntegerData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                        else if (value is string)
                        {
                            var (data, status) = Helios.ParseStringData(name, size, count, command);

                            if (status.IsGood)
                            {
                                Data.SetPropertyValue(property, data);
                            }
                        }
                    }
                    else
                    {
                        _logger?.LogDebug($"UpdateData property '{property}' not writable.");
                    }
                }
                else
                {
                    _logger?.LogWarning($"UpdateData property '{property}' not found.");
                }
            }
            else
            {
                _logger?.LogError($"UpdateData property invalid.");
            }
        }

        #endregion
    }
}
