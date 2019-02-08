// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWrite.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Test
{
    #region Using Directives

    using System.Globalization;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using KWLEC200Lib;
    using KWLEC200Lib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("KWLEC200 Test Collection")]
    public class TestWrite : IClassFixture<KWLEC200Fixture>
    {
        #region Private Data Members

        private readonly ILogger<KWLEC200> _logger;
        private readonly IKWLEC200 _kwlec200;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWrite(KWLEC200Fixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<KWLEC200>();

            _kwlec200 = fixture.KWLEC200;
        }

        #endregion

        [Fact]
        public void TestKWLEC200Write()
        {
            var status = _kwlec200.WriteAll();
            Assert.True(status.IsGood);
            Assert.True(_kwlec200.Data.Status.IsGood);
        }

        [Theory]
        [InlineData("ItemDescription", "KWL EC 200                     ")]
        [InlineData("OrderNumber", "1234567890123456")]
        // MacAddress is not writable.
        [InlineData("Language", "de")]
        [InlineData("Date", "29.07.2018")]
        [InlineData("Time", "11:42:58")]
        [InlineData("DayLightSaving", "Summer")]
        [InlineData("AutoUpdateEnabled", "Enabled")]
        [InlineData("PortalAccessEnabled", "Enabled")]
        [InlineData("ExhaustVentilatorVoltageLevel1", "0.0")]
        [InlineData("SupplyVentilatorVoltageLevel1", "0.0")]
        [InlineData("ExhaustVentilatorVoltageLevel2", "0.0")]
        [InlineData("SupplyVentilatorVoltageLevel2", "0.0")]
        [InlineData("ExhaustVentilatorVoltageLevel3", "0.0")]
        [InlineData("SupplyVentilatorVoltageLevel3", "0.0")]
        [InlineData("ExhaustVentilatorVoltageLevel4", "0.0")]
        [InlineData("SupplyVentilatorVoltageLevel4", "0.0")]
        [InlineData("MinimumVentilationLevel", "Level0")]
        [InlineData("KwlBeEnabled", "Off")]
        [InlineData("KwlBecEnabled", "Off")]
        [InlineData("DeviceConfiguration", "DiBt")]
        [InlineData("PreheaterStatus", "Off")]
        [InlineData("KwlFTFConfig0", "RF")]
        [InlineData("KwlFTFConfig1", "RF")]
        [InlineData("KwlFTFConfig2", "RF")]
        [InlineData("KwlFTFConfig3", "RF")]
        [InlineData("KwlFTFConfig4", "RF")]
        [InlineData("KwlFTFConfig5", "RF")]
        [InlineData("KwlFTFConfig6", "RF")]
        [InlineData("KwlFTFConfig7", "RF")]
        [InlineData("HumidityControlStatus", "Off")]
        [InlineData("HumidityControlTarget", "0")]
        [InlineData("HumidityControlStep", "0")]
        [InlineData("HumidityControlStop", "0")]
        [InlineData("CO2ControlStatus", "Off")]
        [InlineData("CO2ControlTarget", "0")]
        [InlineData("CO2ControlStep", "0")]
        [InlineData("VOCControlStatus", "Off")]
        [InlineData("VOCControlTarget", "0")]
        [InlineData("VOCControlStep", "0")]
        [InlineData("ThermalComfortTemperature", "0.0")]
        [InlineData("TimeZoneOffset", "0")]
        [InlineData("DateFormat", "DDMMYY")]
        [InlineData("HeatExchangerType", "Plastic")]
        [InlineData("PartyOperationDuration", "0")]
        [InlineData("PartyVentilationLevel", "Level4")]
        // PartyOperationRemaining is not writable.
        [InlineData("PartyOperationActivate", "Off")]
        [InlineData("StandbyOperationDuration", "0")]
        [InlineData("StandbyVentilationLevel", "Level0")]
        // StandbyOperationRemaining is not writable.
        [InlineData("StandbyOperationActivate", "Off")]
        [InlineData("OperationMode", "Manual")]
        [InlineData("VentilationLevel", "Level0")]
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
        [InlineData("WeeklyProfile", "Standard1")]
        [InlineData("SerialNumber", "1234567890123456")]
        [InlineData("ProductionCode", "xxxxxxxxxxxxx")]
        // SupplyFanSpeed is not writable.
        // ExhaustFanSpeed is not writable.
        [InlineData("Logout", "true")]
        [InlineData("VacationOperation", "Off")]
        [InlineData("VacationVentilationLevel", "Level0")]
        [InlineData("VacationStartDate", "26.07.2018")]
        [InlineData("VacationEndDate", "02.08.2018")]
        [InlineData("VacationInterval", "0")]
        [InlineData("VacationDuration", "0")]
        [InlineData("PreheaterType", "Basis")]
        [InlineData("KwlFunctionType", "Function1")]
        [InlineData("HeaterAfterRunTime", "0")]
        [InlineData("ExternalContact", "Function1")]
        [InlineData("FaultTypeOutput", "SingleFault")]
        [InlineData("FilterChange", "Off")]
        [InlineData("FilterChangeInterval", "0")]
        // FilterChangeRemaining is not writable.
        [InlineData("BypassRoomTemperature", "0")]
        [InlineData("BypassOutdoorTemperature", "0")]
        [InlineData("BypassOutdoorTemperature2", "0")]
        [InlineData("StartReset", "false")]
        [InlineData("FactoryReset", "false")]
        [InlineData("SupplyLevel", "Level2")]
        [InlineData("ExhaustLevel", "Level2")]
        [InlineData("FanLevelRegion02", "Level2")]
        [InlineData("FanLevelRegion24", "Level2")]
        [InlineData("FanLevelRegion46", "Level2")]
        [InlineData("FanLevelRegion68", "Level2")]
        [InlineData("FanLevelRegion80", "Level2")]
        [InlineData("OffsetExhaust", "0")]
        [InlineData("FanLevelConfiguration", "Continuous")]
        [InlineData("SensorName1", "KWL%20FTF%20AD1")]
        [InlineData("SensorName2", "KWL%20FTF%20AD2")]
        [InlineData("SensorName3", "KWL%20FTF%20AD3")]
        [InlineData("SensorName4", "KWL%20FTF%20AD4")]
        [InlineData("SensorName5", "KWL%20FTF%20AD5")]
        [InlineData("SensorName6", "KWL%20FTF%20AD6")]
        [InlineData("SensorName7", "KWL%20FTF%20AD7")]
        [InlineData("SensorName8", "KWL%20FTF%20AD8")]
        [InlineData("CO2SensorName1", "KWL%20CO2%20AD1")]
        [InlineData("CO2SensorName2", "KWL%20CO2%20AD2")]
        [InlineData("CO2SensorName3", "KWL%20CO2%20AD3")]
        [InlineData("CO2SensorName4", "KWL%20CO2%20AD4")]
        [InlineData("CO2SensorName5", "KWL%20CO2%20AD5")]
        [InlineData("CO2SensorName6", "KWL%20CO2%20AD6")]
        [InlineData("CO2SensorName7", "KWL%20CO2%20AD7")]
        [InlineData("CO2SensorName8", "KWL%20CO2%20AD8")]
        [InlineData("VOCSensorName1", "KWL%20VOC%20AD1")]
        [InlineData("VOCSensorName2", "KWL%20VOC%20AD2")]
        [InlineData("VOCSensorName3", "KWL%20VOC%20AD3")]
        [InlineData("VOCSensorName4", "KWL%20VOC%20AD4")]
        [InlineData("VOCSensorName5", "KWL%20VOC%20AD5")]
        [InlineData("VOCSensorName6", "KWL%20VOC%20AD6")]
        [InlineData("VOCSensorName7", "KWL%20VOC%20AD7")]
        [InlineData("VOCSensorName8", "KWL%20VOC%20AD8")]
        // SoftwareVersion is not writable.
        // OperationMinutesSupply is not writable.
        // OperationMinutesExhaust is not writable.
        // OperationMinutesPreheater is not writable.
        // OperationMinutesAfterheater is not writable.
        // PowerPreheater is not writable.
        // PowerAfterheater is not writable.
        [InlineData("ResetFlag", "false")]
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
        //[InlineData("GlobalUpdate", "")]
        // LastError is not writable.
        [InlineData("ClearError", "false")]
        // SensorConfig1 is not writable.
        // SensorConfig2 is not writable.
        // SensorConfig3 is not writable.
        // SensorConfig4 is not writable.
        // SensorConfig5 is not writable.
        // SensorConfig6 is not writable.
        // SensorConfig7 is not writable.
        // SensorConfig8 is not writable.
        public void TestKWLEC200WriteProperty(string property, string data)
        {
            Assert.True(KWLEC200Data.IsProperty(property));
            Assert.True(KWLEC200Data.IsWritable(property));
            var status = _kwlec200.WriteData(property, data);
            Assert.True(status.IsGood);
        }
    }
}
