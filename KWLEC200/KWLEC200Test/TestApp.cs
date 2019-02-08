// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Test
{
    #region Using Directives

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using KWLEC200Lib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("KWLEC200 Test Collection")]
    public class TestApp
    {
        #region Private Data Members

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestApp(ITestOutputHelper output)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<KWLEC200>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Modbus TCP client found", 0)]
        [InlineData("-?", "Usage: KWLEC200App [options] [command]", 0)]
        [InlineData("--help", "Usage: KWLEC200App [options] [command]", 0)]
        [InlineData("--address 127.0.0.1 --port 504 --slaveid 180", "Modbus TCP client found", 0)]
        public void TestRootCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("info", "Select an info option", 0)]
        [InlineData("info -?", "Usage: KWLEC200App info [options]", 0)]
        [InlineData("info --help", "Usage: KWLEC200App info [options]", 0)]
        [InlineData("info -a", "Data:", 0)]
        [InlineData("info --all", "Data:", 0)]
        [InlineData("info -o", "Overview:", 0)]
        [InlineData("info --overview", "Overview:", 0)]
        public void TestInfoCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("read", "KWL EC 200W L", 0)]
        [InlineData("read -?", "Usage: KWLEC200App read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: KWLEC200App read [arguments] [options]", 0)]
        [InlineData("read ItemDescription", "Value of property 'ItemDescription' =", 0)]
        [InlineData("read OrderNumber", "Value of property 'OrderNumber' =", 0)]
        [InlineData("read MacAddress", "Value of property 'MacAddress' =", 0)]
        [InlineData("read Language", "Value of property 'Language' =", 0)]
        [InlineData("read Date", "Value of property 'Date' =", 0)]
        [InlineData("read Time", "Value of property 'Time' =", 0)]
        [InlineData("read DayLightSaving", "Value of property 'DayLightSaving' =", 0)]
        [InlineData("read AutoUpdateEnabled", "Value of property 'AutoUpdateEnabled' =", 0)]
        [InlineData("read PortalAccessEnabled", "Value of property 'PortalAccessEnabled' =", 0)]
        [InlineData("read ExhaustVentilatorVoltageLevel1", "Value of property 'ExhaustVentilatorVoltageLevel1' =", 0)]
        [InlineData("read SupplyVentilatorVoltageLevel1", "Value of property 'SupplyVentilatorVoltageLevel1' =", 0)]
        [InlineData("read ExhaustVentilatorVoltageLevel2", "Value of property 'ExhaustVentilatorVoltageLevel2' =", 0)]
        [InlineData("read SupplyVentilatorVoltageLevel2", "Value of property 'SupplyVentilatorVoltageLevel2' =", 0)]
        [InlineData("read ExhaustVentilatorVoltageLevel3", "Value of property 'ExhaustVentilatorVoltageLevel3' =", 0)]
        [InlineData("read SupplyVentilatorVoltageLevel3", "Value of property 'SupplyVentilatorVoltageLevel3' =", 0)]
        [InlineData("read ExhaustVentilatorVoltageLevel4", "Value of property 'ExhaustVentilatorVoltageLevel4' =", 0)]
        [InlineData("read SupplyVentilatorVoltageLevel4", "Value of property 'SupplyVentilatorVoltageLevel4' =", 0)]
        [InlineData("read MinimumVentilationLevel", "Value of property 'MinimumVentilationLevel' =", 0)]
        [InlineData("read KwlBeEnabled", "Value of property 'KwlBeEnabled' =", 0)]
        [InlineData("read KwlBecEnabled", "Value of property 'KwlBecEnabled' =", 0)]
        [InlineData("read DeviceConfiguration", "Value of property 'DeviceConfiguration' =", 0)]
        [InlineData("read PreheaterStatus", "Value of property 'PreheaterStatus' =", 0)]
        [InlineData("read KwlFTFConfig0", "Value of property 'KwlFTFConfig0' =", 0)]
        [InlineData("read KwlFTFConfig1", "Value of property 'KwlFTFConfig1' =", 0)]
        [InlineData("read KwlFTFConfig2", "Value of property 'KwlFTFConfig2' =", 0)]
        [InlineData("read KwlFTFConfig3", "Value of property 'KwlFTFConfig3' =", 0)]
        [InlineData("read KwlFTFConfig4", "Value of property 'KwlFTFConfig4' =", 0)]
        [InlineData("read KwlFTFConfig5", "Value of property 'KwlFTFConfig5' =", 0)]
        [InlineData("read KwlFTFConfig6", "Value of property 'KwlFTFConfig6' =", 0)]
        [InlineData("read KwlFTFConfig7", "Value of property 'KwlFTFConfig7' =", 0)]
        [InlineData("read HumidityControlStatus", "Value of property 'HumidityControlStatus' =", 0)]
        [InlineData("read HumidityControlTarget", "Value of property 'HumidityControlTarget' =", 0)]
        [InlineData("read HumidityControlStep", "Value of property 'HumidityControlStep' =", 0)]
        [InlineData("read HumidityControlStop", "Value of property 'HumidityControlStop' =", 0)]
        [InlineData("read CO2ControlStatus", "Value of property 'CO2ControlStatus' =", 0)]
        [InlineData("read CO2ControlTarget", "Value of property 'CO2ControlTarget' =", 0)]
        [InlineData("read CO2ControlStep", "Value of property 'CO2ControlStep' =", 0)]
        [InlineData("read VOCControlStatus", "Value of property 'VOCControlStatus' =", 0)]
        [InlineData("read VOCControlTarget", "Value of property 'VOCControlTarget' =", 0)]
        [InlineData("read VOCControlStep", "Value of property 'VOCControlStep' =", 0)]
        [InlineData("read ThermalComfortTemperature", "Value of property 'ThermalComfortTemperature' =", 0)]
        [InlineData("read TimeZoneOffset", "Value of property 'TimeZoneOffset' =", 0)]
        [InlineData("read DateFormat", "Value of property 'DateFormat' =", 0)]
        [InlineData("read HeatExchangerType", "Value of property 'HeatExchangerType' =", 0)]
        [InlineData("read PartyOperationDuration", "Value of property 'PartyOperationDuration' =", 0)]
        [InlineData("read PartyVentilationLevel", "Value of property 'PartyVentilationLevel' =", 0)]
        [InlineData("read PartyOperationRemaining", "Value of property 'PartyOperationRemaining' =", 0)]
        [InlineData("read PartyOperationActivate", "Value of property 'PartyOperationActivate' =", 0)]
        [InlineData("read StandbyOperationDuration", "Value of property 'StandbyOperationDuration' =", 0)]
        [InlineData("read StandbyVentilationLevel", "Value of property 'StandbyVentilationLevel' =", 0)]
        [InlineData("read StandbyOperationRemaining", "Value of property 'StandbyOperationRemaining' =", 0)]
        [InlineData("read StandbyOperationActivate", "Value of property 'StandbyOperationActivate' =", 0)]
        [InlineData("read OperationMode", "Value of property 'OperationMode' =", 0)]
        [InlineData("read VentilationLevel", "Value of property 'VentilationLevel' =", 0)]
        [InlineData("read VentilationPercentage", "Value of property 'VentilationPercentage' =", 0)]
        [InlineData("read TemperatureOutdoor", "Value of property 'TemperatureOutdoor' =", 0)]
        [InlineData("read TemperatureSupply", "Value of property 'TemperatureSupply' =", 0)]
        [InlineData("read TemperatureExhaust", "Value of property 'TemperatureExhaust' =", 0)]
        [InlineData("read TemperatureExtract", "Value of property 'TemperatureExtract' =", 0)]
        [InlineData("read TemperaturePreHeater", "Value of property 'TemperaturePreHeater' =", 0)]
        [InlineData("read TemperaturePostHeater", "Value of property 'TemperaturePostHeater' =", 0)]
        [InlineData("read ExternalHumiditySensor1", "Value of property 'ExternalHumiditySensor1' =", 0)]
        [InlineData("read ExternalHumiditySensor2", "Value of property 'ExternalHumiditySensor2' =", 0)]
        [InlineData("read ExternalHumiditySensor3", "Value of property 'ExternalHumiditySensor3' =", 0)]
        [InlineData("read ExternalHumiditySensor4", "Value of property 'ExternalHumiditySensor4' =", 0)]
        [InlineData("read ExternalHumiditySensor5", "Value of property 'ExternalHumiditySensor5' =", 0)]
        [InlineData("read ExternalHumiditySensor6", "Value of property 'ExternalHumiditySensor6' =", 0)]
        [InlineData("read ExternalHumiditySensor7", "Value of property 'ExternalHumiditySensor7' =", 0)]
        [InlineData("read ExternalHumiditySensor8", "Value of property 'ExternalHumiditySensor8' =", 0)]
        [InlineData("read ExternalHumidityTemperature1", "Value of property 'ExternalHumidityTemperature1' =", 0)]
        [InlineData("read ExternalHumidityTemperature2", "Value of property 'ExternalHumidityTemperature2' =", 0)]
        [InlineData("read ExternalHumidityTemperature3", "Value of property 'ExternalHumidityTemperature3' =", 0)]
        [InlineData("read ExternalHumidityTemperature4", "Value of property 'ExternalHumidityTemperature4' =", 0)]
        [InlineData("read ExternalHumidityTemperature5", "Value of property 'ExternalHumidityTemperature5' =", 0)]
        [InlineData("read ExternalHumidityTemperature6", "Value of property 'ExternalHumidityTemperature6' =", 0)]
        [InlineData("read ExternalHumidityTemperature7", "Value of property 'ExternalHumidityTemperature7' =", 0)]
        [InlineData("read ExternalHumidityTemperature8", "Value of property 'ExternalHumidityTemperature8' =", 0)]
        [InlineData("read ExternalCO2Sensor1", "Value of property 'ExternalCO2Sensor1' =", 0)]
        [InlineData("read ExternalCO2Sensor2", "Value of property 'ExternalCO2Sensor2' =", 0)]
        [InlineData("read ExternalCO2Sensor3", "Value of property 'ExternalCO2Sensor3' =", 0)]
        [InlineData("read ExternalCO2Sensor4", "Value of property 'ExternalCO2Sensor4' =", 0)]
        [InlineData("read ExternalCO2Sensor5", "Value of property 'ExternalCO2Sensor5' =", 0)]
        [InlineData("read ExternalCO2Sensor6", "Value of property 'ExternalCO2Sensor6' =", 0)]
        [InlineData("read ExternalCO2Sensor7", "Value of property 'ExternalCO2Sensor7' =", 0)]
        [InlineData("read ExternalCO2Sensor8", "Value of property 'ExternalCO2Sensor8' =", 0)]
        [InlineData("read ExternalVOCSensor1", "Value of property 'ExternalVOCSensor1' =", 0)]
        [InlineData("read ExternalVOCSensor2", "Value of property 'ExternalVOCSensor2' =", 0)]
        [InlineData("read ExternalVOCSensor3", "Value of property 'ExternalVOCSensor3' =", 0)]
        [InlineData("read ExternalVOCSensor4", "Value of property 'ExternalVOCSensor4' =", 0)]
        [InlineData("read ExternalVOCSensor5", "Value of property 'ExternalVOCSensor5' =", 0)]
        [InlineData("read ExternalVOCSensor6", "Value of property 'ExternalVOCSensor6' =", 0)]
        [InlineData("read ExternalVOCSensor7", "Value of property 'ExternalVOCSensor7' =", 0)]
        [InlineData("read ExternalVOCSensor8", "Value of property 'ExternalVOCSensor8' =", 0)]
        [InlineData("read TemperatureChannel", "Value of property 'TemperatureChannel' =", 0)]
        [InlineData("read WeeklyProfile", "Value of property 'WeeklyProfile' =", 0)]
        [InlineData("read SerialNumber", "Value of property 'SerialNumber' =", 0)]
        [InlineData("read ProductionCode", "Value of property 'ProductionCode' =", 0)]
        [InlineData("read SupplyFanSpeed", "Value of property 'SupplyFanSpeed' =", 0)]
        [InlineData("read ExhaustFanSpeed", "Value of property 'ExhaustFanSpeed' =", 0)]
        [InlineData("read VacationOperation", "Value of property 'VacationOperation' =", 0)]
        [InlineData("read VacationVentilationLevel", "Value of property 'VacationVentilationLevel' =", 0)]
        [InlineData("read VacationStartDate", "Value of property 'VacationStartDate' =", 0)]
        [InlineData("read VacationEndDate", "Value of property 'VacationEndDate' =", 0)]
        [InlineData("read VacationInterval", "Value of property 'VacationInterval' =", 0)]
        [InlineData("read VacationDuration", "Value of property 'VacationDuration' =", 0)]
        [InlineData("read PreheaterType", "Value of property 'PreheaterType' =", 0)]
        [InlineData("read KwlFunctionType", "Value of property 'KwlFunctionType' =", 0)]
        [InlineData("read HeaterAfterRunTime", "Value of property 'HeaterAfterRunTime' =", 0)]
        [InlineData("read ExternalContact", "Value of property 'ExternalContact' =", 0)]
        [InlineData("read FaultTypeOutput", "Value of property 'FaultTypeOutput' =", 0)]
        [InlineData("read FilterChange", "Value of property 'FilterChange' =", 0)]
        [InlineData("read FilterChangeInterval", "Value of property 'FilterChangeInterval' =", 0)]
        [InlineData("read FilterChangeRemaining", "Value of property 'FilterChangeRemaining' =", 0)]
        [InlineData("read BypassRoomTemperature", "Value of property 'BypassRoomTemperature' =", 0)]
        [InlineData("read BypassOutdoorTemperature", "Value of property 'BypassOutdoorTemperature' =", 0)]
        [InlineData("read BypassOutdoorTemperature2", "Value of property 'BypassOutdoorTemperature2' =", 0)]
        [InlineData("read SupplyLevel", "Value of property 'SupplyLevel' =", 0)]
        [InlineData("read ExhaustLevel", "Value of property 'ExhaustLevel' =", 0)]
        [InlineData("read FanLevelRegion02", "Value of property 'FanLevelRegion02' =", 0)]
        [InlineData("read FanLevelRegion24", "Value of property 'FanLevelRegion24' =", 0)]
        [InlineData("read FanLevelRegion46", "Value of property 'FanLevelRegion46' =", 0)]
        [InlineData("read FanLevelRegion68", "Value of property 'FanLevelRegion68' =", 0)]
        [InlineData("read FanLevelRegion80", "Value of property 'FanLevelRegion80' =", 0)]
        [InlineData("read OffsetExhaust", "Value of property 'OffsetExhaust' =", 0)]
        [InlineData("read FanLevelConfiguration", "Value of property 'FanLevelConfiguration' =", 0)]
        [InlineData("read SensorName1", "Value of property 'SensorName1' =", 0)]
        [InlineData("read SensorName2", "Value of property 'SensorName2' =", 0)]
        [InlineData("read SensorName3", "Value of property 'SensorName3' =", 0)]
        [InlineData("read SensorName4", "Value of property 'SensorName4' =", 0)]
        [InlineData("read SensorName5", "Value of property 'SensorName5' =", 0)]
        [InlineData("read SensorName6", "Value of property 'SensorName6' =", 0)]
        [InlineData("read SensorName7", "Value of property 'SensorName7' =", 0)]
        [InlineData("read SensorName8", "Value of property 'SensorName8' =", 0)]
        [InlineData("read CO2SensorName1", "Value of property 'CO2SensorName1' =", 0)]
        [InlineData("read CO2SensorName2", "Value of property 'CO2SensorName2' =", 0)]
        [InlineData("read CO2SensorName3", "Value of property 'CO2SensorName3' =", 0)]
        [InlineData("read CO2SensorName4", "Value of property 'CO2SensorName4' =", 0)]
        [InlineData("read CO2SensorName5", "Value of property 'CO2SensorName5' =", 0)]
        [InlineData("read CO2SensorName6", "Value of property 'CO2SensorName6' =", 0)]
        [InlineData("read CO2SensorName7", "Value of property 'CO2SensorName7' =", 0)]
        [InlineData("read CO2SensorName8", "Value of property 'CO2SensorName8' =", 0)]
        [InlineData("read VOCSensorName1", "Value of property 'VOCSensorName1' =", 0)]
        [InlineData("read VOCSensorName2", "Value of property 'VOCSensorName2' =", 0)]
        [InlineData("read VOCSensorName3", "Value of property 'VOCSensorName3' =", 0)]
        [InlineData("read VOCSensorName4", "Value of property 'VOCSensorName4' =", 0)]
        [InlineData("read VOCSensorName5", "Value of property 'VOCSensorName5' =", 0)]
        [InlineData("read VOCSensorName6", "Value of property 'VOCSensorName6' =", 0)]
        [InlineData("read VOCSensorName7", "Value of property 'VOCSensorName7' =", 0)]
        [InlineData("read VOCSensorName8", "Value of property 'VOCSensorName8' =", 0)]
        [InlineData("read SoftwareVersion", "Value of property 'SoftwareVersion' =", 0)]
        [InlineData("read OperationMinutesSupply", "Value of property 'OperationMinutesSupply' =", 0)]
        [InlineData("read OperationMinutesExhaust", "Value of property 'OperationMinutesExhaust' =", 0)]
        [InlineData("read OperationMinutesPreheater", "Value of property 'OperationMinutesPreheater' =", 0)]
        [InlineData("read OperationMinutesAfterheater", "Value of property 'OperationMinutesAfterheater' =", 0)]
        [InlineData("read PowerPreheater", "Value of property 'PowerPreheater' =", 0)]
        [InlineData("read PowerAfterheater", "Value of property 'PowerAfterheater' =", 0)]
        // ResetFlag returns all zeros.
        [InlineData("read ErrorCode", "Value of property 'ErrorCode' =", 0)]
        [InlineData("read WarningCode", "Value of property 'WarningCode' =", 0)]
        [InlineData("read InfoCode", "Value of property 'InfoCode' =", 0)]
        [InlineData("read NumberOfErrors", "Value of property 'NumberOfErrors' =", 0)]
        [InlineData("read NumberOfWarnings", "Value of property 'NumberOfWarnings' =", 0)]
        [InlineData("read NumberOfInfos", "Value of property 'NumberOfInfos' =", 0)]
        [InlineData("read Errors", "Value of property 'Errors' =", 0)]
        [InlineData("read Warnings", "Value of property 'Warnings' =", 0)]
        [InlineData("read Infos", "Value of property 'Infos' =", 0)]
        [InlineData("read StatusFlags", "Value of property 'StatusFlags' =", 0)]
        // GlobalUpdate returns all zeros.
        // LastError returns all zeros.
        [InlineData("read SensorConfig1", "Value of property 'SensorConfig1' =", 0)]
        [InlineData("read SensorConfig2", "Value of property 'SensorConfig2' =", 0)]
        [InlineData("read SensorConfig3", "Value of property 'SensorConfig3' =", 0)]
        [InlineData("read SensorConfig4", "Value of property 'SensorConfig4' =", 0)]
        [InlineData("read SensorConfig5", "Value of property 'SensorConfig5' =", 0)]
        [InlineData("read SensorConfig6", "Value of property 'SensorConfig6' =", 0)]
        [InlineData("read SensorConfig7", "Value of property 'SensorConfig7' =", 0)]
        [InlineData("read SensorConfig8", "Value of property 'SensorConfig8' =", 0)]
        public void TestReadCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("--address 127.0.0.1 --port 504 write", "The property name field is required.", 1)]
        [InlineData("--address 127.0.0.1 --port 504 write -?", "Usage: KWLEC200App write [arguments] [options]", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write --help", "Usage: KWLEC200App write [arguments] [options]", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ItemDescription \"KWL EC 200 L\"", "Value of property 'ItemDescription' = KWL EC 200 L", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write OrderNumber 1234567890123456", "Value of property 'OrderNumber' = 1234567890123456", 0)]
        // MacAddress is not writable.
        [InlineData("--address 127.0.0.1 --port 504 write Language de", "Value of property 'Language' = de", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write Date 29.07.2018", "Value of property 'Date' = 29-Jul-18", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write Time 11:42:58", "Value of property 'Time' = 11:42:58", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write DayLightSaving Summer", "Value of property 'DayLightSaving' = Summer", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write AutoUpdateEnabled Enabled", "Value of property 'AutoUpdateEnabled' = Enabled", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write PortalAccessEnabled Enabled", "Value of property 'PortalAccessEnabled' = Enabled", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ExhaustVentilatorVoltageLevel1 0.0", "Value of property 'ExhaustVentilatorVoltageLevel1' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SupplyVentilatorVoltageLevel1 0.0", "Value of property 'SupplyVentilatorVoltageLevel1' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ExhaustVentilatorVoltageLevel2 0.0", "Value of property 'ExhaustVentilatorVoltageLevel2' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SupplyVentilatorVoltageLevel2 0.0", "Value of property 'SupplyVentilatorVoltageLevel2' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ExhaustVentilatorVoltageLevel3 0.0", "Value of property 'ExhaustVentilatorVoltageLevel3' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SupplyVentilatorVoltageLevel3 0.0", "Value of property 'SupplyVentilatorVoltageLevel3' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ExhaustVentilatorVoltageLevel4 0.0", "Value of property 'ExhaustVentilatorVoltageLevel4' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SupplyVentilatorVoltageLevel4 0.0", "Value of property 'SupplyVentilatorVoltageLevel4' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write MinimumVentilationLevel Level0", "Value of property 'MinimumVentilationLevel' = Level0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlBeEnabled Off", "Value of property 'KwlBeEnabled' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlBecEnabled Off", "Value of property 'KwlBecEnabled' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write DeviceConfiguration DiBt", "Value of property 'DeviceConfiguration' = DiBt", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write PreheaterStatus Off", "Value of property 'PreheaterStatus' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig0 RF", "Value of property 'KwlFTFConfig0' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig1 RF", "Value of property 'KwlFTFConfig1' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig2 RF", "Value of property 'KwlFTFConfig2' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig3 RF", "Value of property 'KwlFTFConfig3' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig4 RF", "Value of property 'KwlFTFConfig4' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig5 RF", "Value of property 'KwlFTFConfig5' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig6 RF", "Value of property 'KwlFTFConfig6' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFTFConfig7 RF", "Value of property 'KwlFTFConfig7' = RF", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write HumidityControlStatus Off", "Value of property 'HumidityControlStatus' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write HumidityControlTarget 0", "Value of property 'HumidityControlTarget' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write HumidityControlStep 0", "Value of property 'HumidityControlStep' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write HumidityControlStop 0", "Value of property 'HumidityControlStop' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2ControlStatus Off", "Value of property 'CO2ControlStatus' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2ControlTarget 0", "Value of property 'CO2ControlTarget' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2ControlStep 0", "Value of property 'CO2ControlStep' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCControlStatus Off", "Value of property 'VOCControlStatus' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCControlTarget 0", "Value of property 'VOCControlTarget' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCControlStep 0", "Value of property 'VOCControlStep' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ThermalComfortTemperature 0.0", "Value of property 'ThermalComfortTemperature' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write TimeZoneOffset 0", "Value of property 'TimeZoneOffset' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write DateFormat DDMMYY", "Value of property 'DateFormat' = DDMMYY", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write HeatExchangerType Plastic", "Value of property 'HeatExchangerType' = Plastic", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write PartyOperationDuration 0", "Value of property 'PartyOperationDuration' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write PartyVentilationLevel Level4", "Value of property 'PartyVentilationLevel' = Level4", 0)]
        // PartyOperationRemaining is not writable.
        [InlineData("--address 127.0.0.1 --port 504 write PartyOperationActivate Off", "Value of property 'PartyOperationActivate' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write StandbyOperationDuration 0", "Value of property 'StandbyOperationDuration' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write StandbyVentilationLevel Level0", "Value of property 'StandbyVentilationLevel' = Level0", 0)]
        // StandbyOperationRemaining is not writable.
        [InlineData("--address 127.0.0.1 --port 504 write StandbyOperationActivate Off", "Value of property 'StandbyOperationActivate' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write OperationMode Manual", "Value of property 'OperationMode' = Manual", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VentilationLevel Level0", "Value of property 'VentilationLevel' = Level0", 0)]
        // VentilationPercentage is not writable.
        // TemperatureOutdoor is not writable.
        // TemperatureSupply is not writable.
        // TemperatureExhaust is not writable.
        // TemperatureExtract is not writable.
        // TemperaturePreHeater is not writable.
        // TemperaturePostHeater is not writable.
        // ExternalHumiditySensor1 is not writable.
        // ExternalHumiditySensor2 is not writable.
        // ExternalHumiditySensor3 is not writable.
        // ExternalHumiditySensor4 is not writable.
        // ExternalHumiditySensor5 is not writable.
        // ExternalHumiditySensor6 is not writable.
        // ExternalHumiditySensor7 is not writable.
        // ExternalHumiditySensor8 is not writable.
        // ExternalHumidityTemperature1 is not writable.
        // ExternalHumidityTemperature2 is not writable.
        // ExternalHumidityTemperature3 is not writable.
        // ExternalHumidityTemperature4 is not writable.
        // ExternalHumidityTemperature5 is not writable.
        // ExternalHumidityTemperature6 is not writable.
        // ExternalHumidityTemperature7 is not writable.
        // ExternalHumidityTemperature8 is not writable.
        // ExternalCO2Sensor1 is not writable.
        // ExternalCO2Sensor2 is not writable.
        // ExternalCO2Sensor3 is not writable.
        // ExternalCO2Sensor4 is not writable.
        // ExternalCO2Sensor5 is not writable.
        // ExternalCO2Sensor6 is not writable.
        // ExternalCO2Sensor7 is not writable.
        // ExternalCO2Sensor8 is not writable.
        // ExternalVOCSensor1 is not writable.
        // ExternalVOCSensor2 is not writable.
        // ExternalVOCSensor3 is not writable.
        // ExternalVOCSensor4 is not writable.
        // ExternalVOCSensor5 is not writable.
        // ExternalVOCSensor6 is not writable.
        // ExternalVOCSensor7 is not writable.
        // ExternalVOCSensor8 is not writable.
        // TemperatureChannel is not writable.
        [InlineData("--address 127.0.0.1 --port 504 write WeeklyProfile Standard1", "Value of property 'WeeklyProfile' = Standard1", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SerialNumber 1234567890123456", "Value of property 'SerialNumber' = 1234567890123456", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ProductionCode xxxxxxxxxxxxx", "Value of property 'ProductionCode' = xxxxxxxxxxxxx", 0)]
        // SupplyFanSpeed is not writable.
        // ExhaustFanSpeed is not writable.
        // Logout is not readable.
        [InlineData("--address 127.0.0.1 --port 504 write VacationOperation Off", "Value of property 'VacationOperation' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VacationVentilationLevel Level0", "Value of property 'VacationVentilationLevel' = Level0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VacationStartDate 26.07.2018", "Value of property 'VacationStartDate' = 26-Jul-18", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VacationEndDate 02.08.2018", "Value of property 'VacationEndDate' = 02-Aug-18", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VacationInterval 0", "Value of property 'VacationInterval' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VacationDuration 0", "Value of property 'VacationDuration' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write PreheaterType Basis", "Value of property 'PreheaterType' = Basis", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write KwlFunctionType Function1", "Value of property 'KwlFunctionType' = Function1", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write HeaterAfterRunTime 0", "Value of property 'HeaterAfterRunTime' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ExternalContact Function1", "Value of property 'ExternalContact' = Function1", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FaultTypeOutput SingleFault", "Value of property 'FaultTypeOutput' = SingleFault", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FilterChange Off", "Value of property 'FilterChange' = Off", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FilterChangeInterval 0", "Value of property 'FilterChangeInterval' = 0", 0)]
        // FilterChangeRemaining is not writable.
        [InlineData("--address 127.0.0.1 --port 504 write BypassRoomTemperature 0", "Value of property 'BypassRoomTemperature' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write BypassOutdoorTemperature 0", "Value of property 'BypassOutdoorTemperature' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write BypassOutdoorTemperature2 0", "Value of property 'BypassOutdoorTemperature2' = 0", 0)]
        // StartReset is not readable.
        // FactoryReset is not readable.
        [InlineData("--address 127.0.0.1 --port 504 write SupplyLevel Level2", "Value of property 'SupplyLevel' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write ExhaustLevel Level2", "Value of property 'ExhaustLevel' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FanLevelRegion02 Level2", "Value of property 'FanLevelRegion02' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FanLevelRegion24 Level2", "Value of property 'FanLevelRegion24' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FanLevelRegion46 Level2", "Value of property 'FanLevelRegion46' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FanLevelRegion68 Level2", "Value of property 'FanLevelRegion68' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FanLevelRegion80 Level2", "Value of property 'FanLevelRegion80' = Level2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write OffsetExhaust 0", "Value of property 'OffsetExhaust' = 0", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write FanLevelConfiguration Continuous", "Value of property 'FanLevelConfiguration' = Continuous", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName1 KWL%20FTF%20AD1", "Value of property 'SensorName1' = KWL%20FTF%20AD1", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName2 KWL%20FTF%20AD2", "Value of property 'SensorName2' = KWL%20FTF%20AD2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName3 KWL%20FTF%20AD3", "Value of property 'SensorName3' = KWL%20FTF%20AD3", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName4 KWL%20FTF%20AD4", "Value of property 'SensorName4' = KWL%20FTF%20AD4", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName5 KWL%20FTF%20AD5", "Value of property 'SensorName5' = KWL%20FTF%20AD5", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName6 KWL%20FTF%20AD6", "Value of property 'SensorName6' = KWL%20FTF%20AD6", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName7 KWL%20FTF%20AD7", "Value of property 'SensorName7' = KWL%20FTF%20AD7", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write SensorName8 KWL%20FTF%20AD8", "Value of property 'SensorName8' = KWL%20FTF%20AD8", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName1 KWL%20CO2%20AD1", "Value of property 'CO2SensorName1' = KWL%20CO2%20AD1", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName2 KWL%20CO2%20AD2", "Value of property 'CO2SensorName2' = KWL%20CO2%20AD2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName3 KWL%20CO2%20AD3", "Value of property 'CO2SensorName3' = KWL%20CO2%20AD3", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName4 KWL%20CO2%20AD4", "Value of property 'CO2SensorName4' = KWL%20CO2%20AD4", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName5 KWL%20CO2%20AD5", "Value of property 'CO2SensorName5' = KWL%20CO2%20AD5", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName6 KWL%20CO2%20AD6", "Value of property 'CO2SensorName6' = KWL%20CO2%20AD6", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName7 KWL%20CO2%20AD7", "Value of property 'CO2SensorName7' = KWL%20CO2%20AD7", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write CO2SensorName8 KWL%20CO2%20AD8", "Value of property 'CO2SensorName8' = KWL%20CO2%20AD8", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName1 KWL%20VOC%20AD1", "Value of property 'VOCSensorName1' = KWL%20VOC%20AD1", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName2 KWL%20VOC%20AD2", "Value of property 'VOCSensorName2' = KWL%20VOC%20AD2", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName3 KWL%20VOC%20AD3", "Value of property 'VOCSensorName3' = KWL%20VOC%20AD3", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName4 KWL%20VOC%20AD4", "Value of property 'VOCSensorName4' = KWL%20VOC%20AD4", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName5 KWL%20VOC%20AD5", "Value of property 'VOCSensorName5' = KWL%20VOC%20AD5", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName6 KWL%20VOC%20AD6", "Value of property 'VOCSensorName6' = KWL%20VOC%20AD6", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName7 KWL%20VOC%20AD7", "Value of property 'VOCSensorName7' = KWL%20VOC%20AD7", 0)]
        [InlineData("--address 127.0.0.1 --port 504 write VOCSensorName8 KWL%20VOC%20AD8", "Value of property 'VOCSensorName8' = KWL%20VOC%20AD8", 0)]
        // SoftwareVersion is not writable.
        // OperationMinutesSupply is not writable.
        // OperationMinutesExhaust is not writable.
        // OperationMinutesPreheater is not writable.
        // OperationMinutesAfterheater is not writable.
        // PowerPreheater is not writable.
        // PowerAfterheater is not writable.
        // ResetFlag is not readable.
        // ErrorCode is not writable.
        // WarningCode is not writable.
        // InfoCode is not writable.
        // NumberOfErrors is not writable.
        // NumberOfWarnings is not writable.
        // NumberOfInfos is not writable.
        // Errors is not writable.
        // Warnings is not writable.
        // Infos is not writable.
        // StatusFlags is not writable.
        // GlobalUpdate returns all zero.
        // LastError is not writable.
        // ClearError is not readable.
        // SensorConfig1 is not writable.
        // SensorConfig2 is not writable.
        // SensorConfig3 is not writable.
        // SensorConfig4 is not writable.
        // SensorConfig5 is not writable.
        // SensorConfig6 is not writable.
        // SensorConfig7 is not writable.
        // SensorConfig8 is not writable.
        public void TestWriteCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("monitor", "Select a data option.", 0)]
        [InlineData("monitor -?", "KWLEC200App monitor [options]", 0)]
        [InlineData("monitor --help", "KWLEC200App monitor [options]", 0)]
        public void TestMonitorCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Starts the console application. Specify empty string to run with no arguments.
        /// </summary>
        /// <param name="arguments">The arguments for console application.</param>
        /// <returns>The exit code.</returns>
        private int StartConsoleApplication(string arguments)
        {
            // Initialize process here
            Process proc = new Process();
            proc.StartInfo.FileName = @"dotnet";

            // add arguments as whole string
            proc.StartInfo.Arguments = "run -- " + arguments;

            // use it to start from testing environment
            proc.StartInfo.UseShellExecute = false;

            // redirect outputs to have it in testing console
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            // set working directory
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\KWLEC200\KWLEC200App";

            // start and wait for exit
            proc.Start();
            proc.WaitForExit(120000);

            // get output to testing console.
            System.Console.WriteLine(proc.StandardOutput.ReadToEnd());
            System.Console.Write(proc.StandardError.ReadToEnd());

            // return exit code
            return proc.ExitCode;
        }

        #endregion
    }
}
