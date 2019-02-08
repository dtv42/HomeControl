// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestReadWrite.cs" company="DTV-Online">
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
    using System.Globalization;
    using System.Threading.Tasks;

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
    public class TestReadWrite : IClassFixture<KWLEC200Fixture>
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
        public TestReadWrite(KWLEC200Fixture fixture, ITestOutputHelper outputHelper)
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
        public void TestKWLEC200ReadWrite()
        {
            _kwlec200.WriteAll();
            Assert.True(_kwlec200.Data.Status.IsGood);
            _kwlec200.ReadAll();
            Assert.True(_kwlec200.Data.Status.IsGood);
        }

        [Fact]
        public void TestKWLEC200ReadWriteItemDescription()
        {
            var status = _kwlec200.WriteData("ItemDescription", "KWL EC 200                     ");
            Assert.True(status.IsGood);
            _kwlec200.Data.ItemDescription = "";
            status = _kwlec200.ReadData("ItemDescription");
            Assert.True(status.IsGood);
            Assert.Equal("KWL EC 200                     ", _kwlec200.Data.ItemDescription);
        }

        [Fact]
        public void TestKWLEC200ReadWriteOrderNumber()
        {
            var status = _kwlec200.WriteData("OrderNumber", "1234567890123456");
            Assert.True(status.IsGood);
            _kwlec200.Data.OrderNumber = "";
            status = _kwlec200.ReadData("OrderNumber");
            Assert.True(status.IsGood);
            Assert.Equal("1234567890123456", _kwlec200.Data.OrderNumber);
        }

        // MacAddress is not writable.
        [Fact]
        public void TestKWLEC200ReadWriteLanguage()
        {
            var status = _kwlec200.WriteData("Language", "en");
            Assert.True(status.IsGood);
            _kwlec200.Data.Language = "";
            status = _kwlec200.ReadData("Language");
            Assert.True(status.IsGood);
            Assert.Equal("en", _kwlec200.Data.Language);
        }

        [Fact]
        public void TestKWLEC200ReadWriteDate()
        {
            var status = _kwlec200.WriteData("Date", "29.07.2018");
            Assert.True(status.IsGood);
            _kwlec200.Data.Date = new DateTime();
            status = _kwlec200.ReadData("Date");
            Assert.True(status.IsGood);
            Assert.Equal(new DateTime(2018, 7, 29), _kwlec200.Data.Date);
        }

        [Fact]
        public void TestKWLEC200ReadWriteTime()
        {
            var status = _kwlec200.WriteData("Time", "08:39:15");
            Assert.True(status.IsGood);
            _kwlec200.Data.Time = new TimeSpan();
            status = _kwlec200.ReadData("Time");
            Assert.True(status.IsGood);
            Assert.Equal(new TimeSpan(8, 39, 15), _kwlec200.Data.Time);
        }

        [Fact]
        public void TestKWLEC200ReadWriteDayLightSaving()
        {
            var status = _kwlec200.WriteData("DayLightSaving", "Winter");
            Assert.True(status.IsGood);
            _kwlec200.Data.DayLightSaving = 0;
            status = _kwlec200.ReadData("DayLightSaving");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.DaylightSaving)0, _kwlec200.Data.DayLightSaving);
        }

        [Fact]
        public void TestKWLEC200ReadWriteAutoUpdateEnabled()
        {
            var status = _kwlec200.WriteData("AutoUpdateEnabled", "Disabled");
            Assert.True(status.IsGood);
            _kwlec200.Data.AutoUpdateEnabled = 0;
            status = _kwlec200.ReadData("AutoUpdateEnabled");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.AutoSoftwareUpdates)0, _kwlec200.Data.AutoUpdateEnabled);
        }

        [Fact]
        public void TestKWLEC200ReadWritePortalAccessEnabled()
        {
            var status = _kwlec200.WriteData("PortalAccessEnabled", "Disabled");
            Assert.True(status.IsGood);
            _kwlec200.Data.PortalAccessEnabled = 0;
            status = _kwlec200.ReadData("PortalAccessEnabled");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.HeliosPortalAccess)0, _kwlec200.Data.PortalAccessEnabled);
        }

        [Fact]
        public void TestKWLEC200ReadWriteExhaustVentilatorVoltageLevel1()
        {
            var status = _kwlec200.WriteData("ExhaustVentilatorVoltageLevel1", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.ExhaustVentilatorVoltageLevel1 = 0;
            status = _kwlec200.ReadData("ExhaustVentilatorVoltageLevel1");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.ExhaustVentilatorVoltageLevel1);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSupplyVentilatorVoltageLevel1()
        {
            var status = _kwlec200.WriteData("SupplyVentilatorVoltageLevel1", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.SupplyVentilatorVoltageLevel1 = 0;
            status = _kwlec200.ReadData("SupplyVentilatorVoltageLevel1");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.SupplyVentilatorVoltageLevel1);
        }

        [Fact]
        public void TestKWLEC200ReadWriteExhaustVentilatorVoltageLevel2()
        {
            var status = _kwlec200.WriteData("ExhaustVentilatorVoltageLevel2", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.ExhaustVentilatorVoltageLevel2 = 0;
            status = _kwlec200.ReadData("ExhaustVentilatorVoltageLevel2");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.ExhaustVentilatorVoltageLevel2);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSupplyVentilatorVoltageLevel2()
        {
            var status = _kwlec200.WriteData("SupplyVentilatorVoltageLevel2", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.SupplyVentilatorVoltageLevel2 = 0;
            status = _kwlec200.ReadData("SupplyVentilatorVoltageLevel2");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.SupplyVentilatorVoltageLevel2);
        }

        [Fact]
        public void TestKWLEC200ReadWriteExhaustVentilatorVoltageLevel3()
        {
            var status = _kwlec200.WriteData("ExhaustVentilatorVoltageLevel3", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.ExhaustVentilatorVoltageLevel3 = 0;
            status = _kwlec200.ReadData("ExhaustVentilatorVoltageLevel3");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.ExhaustVentilatorVoltageLevel3);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSupplyVentilatorVoltageLevel3()
        {
            var status = _kwlec200.WriteData("SupplyVentilatorVoltageLevel3", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.SupplyVentilatorVoltageLevel3 = 0;
            status = _kwlec200.ReadData("SupplyVentilatorVoltageLevel3");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.SupplyVentilatorVoltageLevel3);
        }

        [Fact]
        public void TestKWLEC200ReadWriteExhaustVentilatorVoltageLevel4()
        {
            var status = _kwlec200.WriteData("ExhaustVentilatorVoltageLevel4", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.ExhaustVentilatorVoltageLevel4 = 0;
            status = _kwlec200.ReadData("ExhaustVentilatorVoltageLevel4");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.ExhaustVentilatorVoltageLevel4);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSupplyVentilatorVoltageLevel4()
        {
            var status = _kwlec200.WriteData("SupplyVentilatorVoltageLevel4", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.SupplyVentilatorVoltageLevel4 = 0;
            status = _kwlec200.ReadData("SupplyVentilatorVoltageLevel4");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.SupplyVentilatorVoltageLevel4);
        }

        [Fact]
        public void TestKWLEC200ReadWriteMinimumVentilationLevel()
        {
            var status = _kwlec200.WriteData("MinimumVentilationLevel", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.MinimumVentilationLevel = 0;
            status = _kwlec200.ReadData("MinimumVentilationLevel");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.MinimumFanLevels)0, _kwlec200.Data.MinimumVentilationLevel);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlBeEnabled()
        {
            var status = _kwlec200.WriteData("KwlBeEnabled", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlBeEnabled = 0;
            status = _kwlec200.ReadData("KwlBeEnabled");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.StatusTypes)0, _kwlec200.Data.KwlBeEnabled);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlBecEnabled()
        {
            var status = _kwlec200.WriteData("KwlBecEnabled", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlBecEnabled = 0;
            status = _kwlec200.ReadData("KwlBecEnabled");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.StatusTypes)0, _kwlec200.Data.KwlBecEnabled);
        }

        [Fact]
        public void TestKWLEC200ReadWriteDeviceConfiguration()
        {
            var status = _kwlec200.WriteData("DeviceConfiguration", "DiBt");
            Assert.True(status.IsGood);
            _kwlec200.Data.DeviceConfiguration = 0;
            status = _kwlec200.ReadData("DeviceConfiguration");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.ConfigOptions)1, _kwlec200.Data.DeviceConfiguration);
        }

        [Fact]
        public void TestKWLEC200ReadWritePreheaterStatus()
        {
            var status = _kwlec200.WriteData("PreheaterStatus", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.PreheaterStatus = 0;
            status = _kwlec200.ReadData("PreheaterStatus");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.StatusTypes)0, _kwlec200.Data.PreheaterStatus);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig0()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig0", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig0 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig0");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig0);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig1()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig1", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig1 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig1");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig1);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig2()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig2", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig2 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig2");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig2);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig3()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig3", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig3 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig3");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig3);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig4()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig4", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig4 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig4");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig4);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig5()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig5", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig5 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig5");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig5);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig6()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig6", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig6 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig6");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig6);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFTFConfig7()
        {
            var status = _kwlec200.WriteData("KwlFTFConfig7", "RF");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFTFConfig7 = 0;
            status = _kwlec200.ReadData("KwlFTFConfig7");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.KwlFTFConfig)1, _kwlec200.Data.KwlFTFConfig7);
        }

        [Fact]
        public void TestKWLEC200ReadWriteHumidityControlStatus()
        {
            var status = _kwlec200.WriteData("HumidityControlStatus", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.HumidityControlStatus = 0;
            status = _kwlec200.ReadData("HumidityControlStatus");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.SensorStatus)0, _kwlec200.Data.HumidityControlStatus);
        }

        [Fact]
        public void TestKWLEC200ReadWriteHumidityControlTarget()
        {
            var status = _kwlec200.WriteData("HumidityControlTarget", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.HumidityControlTarget = 0;
            status = _kwlec200.ReadData("HumidityControlTarget");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.HumidityControlTarget);
        }

        [Fact]
        public void TestKWLEC200ReadWriteHumidityControlStep()
        {
            var status = _kwlec200.WriteData("HumidityControlStep", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.HumidityControlStep = 0;
            status = _kwlec200.ReadData("HumidityControlStep");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.HumidityControlStep);
        }

        [Fact]
        public void TestKWLEC200ReadWriteHumidityControlStop()
        {
            var status = _kwlec200.WriteData("HumidityControlStop", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.HumidityControlStop = 0;
            status = _kwlec200.ReadData("HumidityControlStop");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.HumidityControlStop);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2ControlStatus()
        {
            var status = _kwlec200.WriteData("CO2ControlStatus", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2ControlStatus = 0;
            status = _kwlec200.ReadData("CO2ControlStatus");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.SensorStatus)0, _kwlec200.Data.CO2ControlStatus);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2ControlTarget()
        {
            var status = _kwlec200.WriteData("CO2ControlTarget", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2ControlTarget = 0;
            status = _kwlec200.ReadData("CO2ControlTarget");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.CO2ControlTarget);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2ControlStep()
        {
            var status = _kwlec200.WriteData("CO2ControlStep", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2ControlStep = 0;
            status = _kwlec200.ReadData("CO2ControlStep");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.CO2ControlStep);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCControlStatus()
        {
            var status = _kwlec200.WriteData("VOCControlStatus", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCControlStatus = 0;
            status = _kwlec200.ReadData("VOCControlStatus");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.SensorStatus)0, _kwlec200.Data.VOCControlStatus);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCControlTarget()
        {
            var status = _kwlec200.WriteData("VOCControlTarget", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCControlTarget = 0;
            status = _kwlec200.ReadData("VOCControlTarget");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.VOCControlTarget);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCControlStep()
        {
            var status = _kwlec200.WriteData("VOCControlStep", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCControlStep = 0;
            status = _kwlec200.ReadData("VOCControlStep");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.VOCControlStep);
        }

        [Fact]
        public void TestKWLEC200ReadWriteThermalComfortTemperature()
        {
            var status = _kwlec200.WriteData("ThermalComfortTemperature", "0.0");
            Assert.True(status.IsGood);
            _kwlec200.Data.ThermalComfortTemperature = 0;
            status = _kwlec200.ReadData("ThermalComfortTemperature");
            Assert.True(status.IsGood);
            Assert.Equal(0.0, _kwlec200.Data.ThermalComfortTemperature);
        }

        [Fact]
        public void TestKWLEC200ReadWriteTimeZoneOffset()
        {
            var status = _kwlec200.WriteData("TimeZoneOffset", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.TimeZoneOffset = 0;
            status = _kwlec200.ReadData("TimeZoneOffset");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.TimeZoneOffset);
        }

        [Fact]
        public void TestKWLEC200ReadWriteDateFormat()
        {
            var status = _kwlec200.WriteData("DateFormat", "DDMMYY");
            Assert.True(status.IsGood);
            _kwlec200.Data.DateFormat = 0;
            status = _kwlec200.ReadData("DateFormat");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.DateFormats)0, _kwlec200.Data.DateFormat);
        }

        [Fact]
        public void TestKWLEC200ReadWriteHeatExchangerType()
        {
            var status = _kwlec200.WriteData("HeatExchangerType", "Plastic");
            Assert.True(status.IsGood);
            _kwlec200.Data.HeatExchangerType = 0;
            status = _kwlec200.ReadData("HeatExchangerType");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.HeatExchangerTypes)1, _kwlec200.Data.HeatExchangerType);
        }

        [Fact]
        public void TestKWLEC200ReadWritePartyOperationDuration()
        {
            var status = _kwlec200.WriteData("PartyOperationDuration", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.PartyOperationDuration = 0;
            status = _kwlec200.ReadData("PartyOperationDuration");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.PartyOperationDuration);
        }

        [Fact]
        public void TestKWLEC200ReadWritePartyVentilationLevel()
        {
            var status = _kwlec200.WriteData("PartyVentilationLevel", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.PartyVentilationLevel = 0;
            status = _kwlec200.ReadData("PartyVentilationLevel");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.PartyVentilationLevel);
        }

        // PartyOperationRemaining is not writable.

        [Fact]
        public void TestKWLEC200ReadWritePartyOperationActivate()
        {
            var status = _kwlec200.WriteData("PartyOperationActivate", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.PartyOperationActivate = 0;
            status = _kwlec200.ReadData("PartyOperationActivate");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.StatusTypes)0, _kwlec200.Data.PartyOperationActivate);
        }

        [Fact]
        public void TestKWLEC200ReadWriteStandbyOperationDuration()
        {
            var status = _kwlec200.WriteData("StandbyOperationDuration", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.StandbyOperationDuration = 0;
            status = _kwlec200.ReadData("StandbyOperationDuration");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.StandbyOperationDuration);
        }

        [Fact]
        public void TestKWLEC200ReadWriteStandbyVentilationLevel()
        {
            var status = _kwlec200.WriteData("StandbyVentilationLevel", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.StandbyVentilationLevel = 0;
            status = _kwlec200.ReadData("StandbyVentilationLevel");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.StandbyVentilationLevel);
        }

        // StandbyOperationRemaining is not writable.

        [Fact]
        public void TestKWLEC200ReadWriteStandbyOperationActivate()
        {
            var status = _kwlec200.WriteData("StandbyOperationActivate", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.StandbyOperationActivate = 0;
            status = _kwlec200.ReadData("StandbyOperationActivate");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.StatusTypes)0, _kwlec200.Data.StandbyOperationActivate);
        }

        [Fact]
        public void TestKWLEC200ReadWriteOperationMode()
        {
            var status = _kwlec200.WriteData("OperationMode", "Manual");
            Assert.True(status.IsGood);
            _kwlec200.Data.OperationMode = KWLEC200Data.OperationModes.Manual;
            status = _kwlec200.ReadData("OperationMode");
            Assert.True(status.IsGood);
            Assert.Equal(KWLEC200Data.OperationModes.Manual, _kwlec200.Data.OperationMode);
            Task.Delay(1000);
            status = _kwlec200.WriteData("OperationMode", "Automatic");
            Assert.True(status.IsGood);
            _kwlec200.Data.OperationMode = KWLEC200Data.OperationModes.Manual;
            status = _kwlec200.ReadData("OperationMode");
            Assert.True(status.IsGood);
            Assert.Equal(KWLEC200Data.OperationModes.Automatic, _kwlec200.Data.OperationMode);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVentilationLevel()
        {
            var status = _kwlec200.ReadData("VentilationLevel");
            Assert.True(status.IsGood);
            KWLEC200Data.FanLevels level = _kwlec200.Data.VentilationLevel;
            status = _kwlec200.WriteData("OperationMode", "Manual");
            Assert.True(status.IsGood);
            status = _kwlec200.WriteData("VentilationLevel", "Level4");
            Assert.True(status.IsGood);
            _kwlec200.Data.VentilationLevel = KWLEC200Data.FanLevels.Level0;
            Task.Delay(1000);
            status = _kwlec200.ReadData("VentilationLevel");
            Assert.True(status.IsGood);
            Assert.Equal(KWLEC200Data.FanLevels.Level4, _kwlec200.Data.VentilationLevel);
            status = _kwlec200.WriteData("OperationMode", "Automatic");
            Assert.True(status.IsGood);
            status = _kwlec200.ReadData("VentilationLevel");
            Assert.True(status.IsGood);
        }

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

        [Fact]
        public void TestKWLEC200ReadWriteWeeklyProfile()
        {
            var status = _kwlec200.WriteData("WeeklyProfile", "Standard1");
            Assert.True(status.IsGood);
            _kwlec200.Data.WeeklyProfile = 0;
            status = _kwlec200.ReadData("WeeklyProfile");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.WeeklyProfiles)0, _kwlec200.Data.WeeklyProfile);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSerialNumber()
        {
            var status = _kwlec200.WriteData("SerialNumber", "1234567890123456");
            Assert.True(status.IsGood);
            _kwlec200.Data.SerialNumber = "";
            status = _kwlec200.ReadData("SerialNumber");
            Assert.True(status.IsGood);
            Assert.Equal("1234567890123456", _kwlec200.Data.SerialNumber);
        }

        [Fact]
        public void TestKWLEC200ReadWriteProductionCode()
        {
            var status = _kwlec200.WriteData("ProductionCode", "xxxxxxxxxxxxx");
            Assert.True(status.IsGood);
            _kwlec200.Data.ProductionCode = "";
            status = _kwlec200.ReadData("ProductionCode");
            Assert.True(status.IsGood);
            Assert.Equal("xxxxxxxxxxxxx", _kwlec200.Data.ProductionCode);
        }

        // SupplyFanSpeed is not writable.
        // ExhaustFanSpeed is not writable.

        // Logout is not readable

        [Fact]
        public void TestKWLEC200ReadWriteVacationOperation()
        {
            var status = _kwlec200.WriteData("VacationOperation", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.VacationOperation = 0;
            status = _kwlec200.ReadData("VacationOperation");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.VacationOperations)0, _kwlec200.Data.VacationOperation);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVacationVentilationLevel()
        {
            var status = _kwlec200.WriteData("VacationVentilationLevel", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.VacationVentilationLevel = 0;
            status = _kwlec200.ReadData("VacationVentilationLevel");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.VacationVentilationLevel);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVacationStartDate()
        {
            var status = _kwlec200.WriteData("VacationStartDate", "26.07.2018");
            Assert.True(status.IsGood);
            _kwlec200.Data.VacationStartDate = new DateTime();
            status = _kwlec200.ReadData("VacationStartDate");
            Assert.True(status.IsGood);
            Assert.Equal(new DateTime(2018, 7, 26), _kwlec200.Data.VacationStartDate);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVacationEndDate()
        {
            var status = _kwlec200.WriteData("VacationEndDate", "02.08.2018");
            Assert.True(status.IsGood);
            _kwlec200.Data.VacationEndDate = new DateTime();
            status = _kwlec200.ReadData("VacationEndDate");
            Assert.True(status.IsGood);
            Assert.Equal(new DateTime(2018, 8, 2), _kwlec200.Data.VacationEndDate);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVacationInterval()
        {
            var status = _kwlec200.WriteData("VacationInterval", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.VacationInterval = 0;
            status = _kwlec200.ReadData("VacationInterval");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.VacationInterval);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVacationDuration()
        {
            var status = _kwlec200.WriteData("VacationDuration", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.VacationDuration = 0;
            status = _kwlec200.ReadData("VacationDuration");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.VacationDuration);
        }

        [Fact]
        public void TestKWLEC200ReadWritePreheaterType()
        {
            var status = _kwlec200.WriteData("PreheaterType", "Basis");
            Assert.True(status.IsGood);
            _kwlec200.Data.PreheaterType = 0;
            status = _kwlec200.ReadData("PreheaterType");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.PreheaterTypes)1, _kwlec200.Data.PreheaterType);
        }

        [Fact]
        public void TestKWLEC200ReadWriteKwlFunctionType()
        {
            var status = _kwlec200.WriteData("KwlFunctionType", "Function1");
            Assert.True(status.IsGood);
            _kwlec200.Data.KwlFunctionType = 0;
            status = _kwlec200.ReadData("KwlFunctionType");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FunctionTypes)1, _kwlec200.Data.KwlFunctionType);
        }

        [Fact]
        public void TestKWLEC200ReadWriteHeaterAfterRunTime()
        {
            var status = _kwlec200.WriteData("HeaterAfterRunTime", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.HeaterAfterRunTime = 0;
            status = _kwlec200.ReadData("HeaterAfterRunTime");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.HeaterAfterRunTime);
        }

        [Fact]
        public void TestKWLEC200ReadWriteExternalContact()
        {
            var status = _kwlec200.WriteData("ExternalContact", "Function1");
            Assert.True(status.IsGood);
            _kwlec200.Data.ExternalContact = 0;
            status = _kwlec200.ReadData("ExternalContact");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.ContactTypes)1, _kwlec200.Data.ExternalContact);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFaultTypeOutput()
        {
            var status = _kwlec200.WriteData("FaultTypeOutput", "MultipleFaults");
            Assert.True(status.IsGood);
            _kwlec200.Data.FaultTypeOutput = 0;
            status = _kwlec200.ReadData("FaultTypeOutput");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FaultTypes)1, _kwlec200.Data.FaultTypeOutput);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFilterChange()
        {
            var status = _kwlec200.WriteData("FilterChange", "Off");
            Assert.True(status.IsGood);
            _kwlec200.Data.FilterChange = 0;
            status = _kwlec200.ReadData("FilterChange");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.StatusTypes)0, _kwlec200.Data.FilterChange);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFilterChangeInterval()
        {
            var status = _kwlec200.WriteData("FilterChangeInterval", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.FilterChangeInterval = 0;
            status = _kwlec200.ReadData("FilterChangeInterval");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.FilterChangeInterval);
        }

        // FilterChangeRemaining is not writable.

        [Fact]
        public void TestKWLEC200ReadWriteBypassRoomTemperature()
        {
            var status = _kwlec200.WriteData("BypassRoomTemperature", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.BypassRoomTemperature = 0;
            status = _kwlec200.ReadData("BypassRoomTemperature");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.BypassRoomTemperature);
        }

        [Fact]
        public void TestKWLEC200ReadWriteBypassOutdoorTemperature()
        {
            var status = _kwlec200.WriteData("BypassOutdoorTemperature", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.BypassOutdoorTemperature = 0;
            status = _kwlec200.ReadData("BypassOutdoorTemperature");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.BypassOutdoorTemperature);
        }

        [Fact]
        public void TestKWLEC200ReadWriteBypassOutdoorTemperature2()
        {
            var status = _kwlec200.WriteData("BypassOutdoorTemperature2", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.BypassOutdoorTemperature2 = 0;
            status = _kwlec200.ReadData("BypassOutdoorTemperature2");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.BypassOutdoorTemperature2);
        }

        // StartReset is not readable.

        // FactoryReset is not readable.

        [Fact]
        public void TestKWLEC200ReadWriteSupplyLevel()
        {
            var status = _kwlec200.WriteData("SupplyLevel", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.SupplyLevel = 0;
            status = _kwlec200.ReadData("SupplyLevel");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.SupplyLevel);
        }

        [Fact]
        public void TestKWLEC200ReadWriteExhaustLevel()
        {
            var status = _kwlec200.WriteData("ExhaustLevel", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.ExhaustLevel = 0;
            status = _kwlec200.ReadData("ExhaustLevel");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.ExhaustLevel);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFanLevelRegion02()
        {
            var status = _kwlec200.WriteData("FanLevelRegion02", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.FanLevelRegion02 = 0;
            status = _kwlec200.ReadData("FanLevelRegion02");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.FanLevelRegion02);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFanLevelRegion24()
        {
            var status = _kwlec200.WriteData("FanLevelRegion24", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.FanLevelRegion24 = 0;
            status = _kwlec200.ReadData("FanLevelRegion24");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.FanLevelRegion24);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFanLevelRegion46()
        {
            var status = _kwlec200.WriteData("FanLevelRegion46", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.FanLevelRegion46 = 0;
            status = _kwlec200.ReadData("FanLevelRegion46");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.FanLevelRegion46);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFanLevelRegion68()
        {
            var status = _kwlec200.WriteData("FanLevelRegion68", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.FanLevelRegion68 = 0;
            status = _kwlec200.ReadData("FanLevelRegion68");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.FanLevelRegion68);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFanLevelRegion80()
        {
            var status = _kwlec200.WriteData("FanLevelRegion80", "Level0");
            Assert.True(status.IsGood);
            _kwlec200.Data.FanLevelRegion80 = 0;
            status = _kwlec200.ReadData("FanLevelRegion80");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevels)0, _kwlec200.Data.FanLevelRegion80);
        }

        [Fact]
        public void TestKWLEC200ReadWriteOffsetExhaust()
        {
            var status = _kwlec200.WriteData("OffsetExhaust", "0");
            Assert.True(status.IsGood);
            _kwlec200.Data.OffsetExhaust = 0;
            status = _kwlec200.ReadData("OffsetExhaust");
            Assert.True(status.IsGood);
            Assert.Equal(0, _kwlec200.Data.OffsetExhaust);
        }

        [Fact]
        public void TestKWLEC200ReadWriteFanLevelConfiguration()
        {
            var status = _kwlec200.WriteData("FanLevelConfiguration", "Continuous");
            Assert.True(status.IsGood);
            _kwlec200.Data.FanLevelConfiguration = 0;
            status = _kwlec200.ReadData("FanLevelConfiguration");
            Assert.True(status.IsGood);
            Assert.Equal((KWLEC200Data.FanLevelConfig)0, _kwlec200.Data.FanLevelConfiguration);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName1()
        {
            var status = _kwlec200.WriteData("SensorName1", "KWL%20FTF%20AD1");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName1 = "";
            status = _kwlec200.ReadData("SensorName1");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD1", _kwlec200.Data.SensorName1);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName2()
        {
            var status = _kwlec200.WriteData("SensorName2", "KWL%20FTF%20AD2");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName2 = "";
            status = _kwlec200.ReadData("SensorName2");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD2", _kwlec200.Data.SensorName2);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName3()
        {
            var status = _kwlec200.WriteData("SensorName3", "KWL%20FTF%20AD3");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName3 = "";
            status = _kwlec200.ReadData("SensorName3");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD3", _kwlec200.Data.SensorName3);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName4()
        {
            var status = _kwlec200.WriteData("SensorName4", "KWL%20FTF%20AD4");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName4 = "";
            status = _kwlec200.ReadData("SensorName4");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD4", _kwlec200.Data.SensorName4);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName5()
        {
            var status = _kwlec200.WriteData("SensorName5", "KWL%20FTF%20AD5");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName5 = "";
            status = _kwlec200.ReadData("SensorName5");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD5", _kwlec200.Data.SensorName5);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName6()
        {
            var status = _kwlec200.WriteData("SensorName6", "KWL%20FTF%20AD6");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName6 = "";
            status = _kwlec200.ReadData("SensorName6");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD6", _kwlec200.Data.SensorName6);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName7()
        {
            var status = _kwlec200.WriteData("SensorName7", "KWL%20FTF%20AD7");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName7 = "";
            status = _kwlec200.ReadData("SensorName7");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD7", _kwlec200.Data.SensorName7);
        }

        [Fact]
        public void TestKWLEC200ReadWriteSensorName8()
        {
            var status = _kwlec200.WriteData("SensorName8", "KWL%20FTF%20AD8");
            Assert.True(status.IsGood);
            _kwlec200.Data.SensorName8 = "";
            status = _kwlec200.ReadData("SensorName8");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20FTF%20AD8", _kwlec200.Data.SensorName8);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName1()
        {
            var status = _kwlec200.WriteData("CO2SensorName1", "KWL%20CO2%20AD1");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName1 = "";
            status = _kwlec200.ReadData("CO2SensorName1");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD1", _kwlec200.Data.CO2SensorName1);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName2()
        {
            var status = _kwlec200.WriteData("CO2SensorName2", "KWL%20CO2%20AD2");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName2 = "";
            status = _kwlec200.ReadData("CO2SensorName2");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD2", _kwlec200.Data.CO2SensorName2);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName3()
        {
            var status = _kwlec200.WriteData("CO2SensorName3", "KWL%20CO2%20AD3");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName3 = "";
            status = _kwlec200.ReadData("CO2SensorName3");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD3", _kwlec200.Data.CO2SensorName3);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName4()
        {
            var status = _kwlec200.WriteData("CO2SensorName4", "KWL%20CO2%20AD4");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName4 = "";
            status = _kwlec200.ReadData("CO2SensorName4");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD4", _kwlec200.Data.CO2SensorName4);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName5()
        {
            var status = _kwlec200.WriteData("CO2SensorName5", "KWL%20CO2%20AD5");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName5 = "";
            status = _kwlec200.ReadData("CO2SensorName5");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD5", _kwlec200.Data.CO2SensorName5);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName6()
        {
            var status = _kwlec200.WriteData("CO2SensorName6", "KWL%20CO2%20AD6");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName6 = "";
            status = _kwlec200.ReadData("CO2SensorName6");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD6", _kwlec200.Data.CO2SensorName6);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName7()
        {
            var status = _kwlec200.WriteData("CO2SensorName7", "KWL%20CO2%20AD7");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName7 = "";
            status = _kwlec200.ReadData("CO2SensorName7");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD7", _kwlec200.Data.CO2SensorName7);
        }

        [Fact]
        public void TestKWLEC200ReadWriteCO2SensorName8()
        {
            var status = _kwlec200.WriteData("CO2SensorName8", "KWL%20CO2%20AD8");
            Assert.True(status.IsGood);
            _kwlec200.Data.CO2SensorName8 = "";
            status = _kwlec200.ReadData("CO2SensorName8");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20CO2%20AD8", _kwlec200.Data.CO2SensorName8);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName1()
        {
            var status = _kwlec200.WriteData("VOCSensorName1", "KWL%20VOC%20AD1");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName1 = "";
            status = _kwlec200.ReadData("VOCSensorName1");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD1", _kwlec200.Data.VOCSensorName1);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName2()
        {
            var status = _kwlec200.WriteData("VOCSensorName2", "KWL%20VOC%20AD2");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName2 = "";
            status = _kwlec200.ReadData("VOCSensorName2");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD2", _kwlec200.Data.VOCSensorName2);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName3()
        {
            var status = _kwlec200.WriteData("VOCSensorName3", "KWL%20VOC%20AD3");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName3 = "";
            status = _kwlec200.ReadData("VOCSensorName3");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD3", _kwlec200.Data.VOCSensorName3);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName4()
        {
            var status = _kwlec200.WriteData("VOCSensorName4", "KWL%20VOC%20AD4");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName4 = "";
            status = _kwlec200.ReadData("VOCSensorName4");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD4", _kwlec200.Data.VOCSensorName4);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName5()
        {
            var status = _kwlec200.WriteData("VOCSensorName5", "KWL%20VOC%20AD5");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName5 = "";
            status = _kwlec200.ReadData("VOCSensorName5");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD5", _kwlec200.Data.VOCSensorName5);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName6()
        {
            var status = _kwlec200.WriteData("VOCSensorName6", "KWL%20VOC%20AD6");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName6 = "";
            status = _kwlec200.ReadData("VOCSensorName6");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD6", _kwlec200.Data.VOCSensorName6);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName7()
        {
            var status = _kwlec200.WriteData("VOCSensorName7", "KWL%20VOC%20AD7");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName7 = "";
            status = _kwlec200.ReadData("VOCSensorName7");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD7", _kwlec200.Data.VOCSensorName7);
        }

        [Fact]
        public void TestKWLEC200ReadWriteVOCSensorName8()
        {
            var status = _kwlec200.WriteData("VOCSensorName8", "KWL%20VOC%20AD8");
            Assert.True(status.IsGood);
            _kwlec200.Data.VOCSensorName8 = "";
            status = _kwlec200.ReadData("VOCSensorName8");
            Assert.True(status.IsGood);
            Assert.Equal("KWL%20VOC%20AD8", _kwlec200.Data.VOCSensorName8);
        }

        // SoftwareVersion is not writable.
        // OperationMinutesSupply is not writable.
        // OperationMinutesExhaust is not writable.
        // OperationMinutesPreheater is not writable.
        // OperationMinutesAfterheater is not writable.
        // PowerPreheater is not writable.
        // PowerAfterheater is not writable.

        // ResetFlag returns all zero.

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
    }
}
