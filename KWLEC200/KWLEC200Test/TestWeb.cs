// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
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
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
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
    public class TestWeb
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWeb"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWeb(ITestOutputHelper output)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<KWLEC200>();
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8003")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestGetAllData()
        {
            // Act
            var response = await _client.GetAsync("api/KWLEC200/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<KWLEC200Data>(responseString);
            Assert.NotNull(data);
            Assert.True(data.Status.IsGood);
        }

        [Fact]
        public async Task TestGetOverviewData()
        {
            // Act
            var response = await _client.GetAsync("api/KWLEC200/overview");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OverviewData>(responseString);
            Assert.NotNull(data);
            Assert.True(data.Status.IsGood);
        }

        [Theory]
        [InlineData("ItemDescription")]
        [InlineData("OrderNumber")]
        [InlineData("MacAddress")]
        [InlineData("Language")]
        [InlineData("Date")]
        [InlineData("Time")]
        [InlineData("DayLightSaving")]
        [InlineData("AutoUpdateEnabled")]
        [InlineData("PortalAccessEnabled")]
        [InlineData("ExhaustVentilatorVoltageLevel1")]
        [InlineData("SupplyVentilatorVoltageLevel1")]
        [InlineData("ExhaustVentilatorVoltageLevel2")]
        [InlineData("SupplyVentilatorVoltageLevel2")]
        [InlineData("ExhaustVentilatorVoltageLevel3")]
        [InlineData("SupplyVentilatorVoltageLevel3")]
        [InlineData("ExhaustVentilatorVoltageLevel4")]
        [InlineData("SupplyVentilatorVoltageLevel4")]
        [InlineData("MinimumVentilationLevel")]
        [InlineData("KwlBeEnabled")]
        [InlineData("KwlBecEnabled")]
        [InlineData("DeviceConfiguration")]
        [InlineData("PreheaterStatus")]
        [InlineData("KwlFTFConfig0")]
        [InlineData("KwlFTFConfig1")]
        [InlineData("KwlFTFConfig2")]
        [InlineData("KwlFTFConfig3")]
        [InlineData("KwlFTFConfig4")]
        [InlineData("KwlFTFConfig5")]
        [InlineData("KwlFTFConfig6")]
        [InlineData("KwlFTFConfig7")]
        [InlineData("HumidityControlStatus")]
        [InlineData("HumidityControlTarget")]
        [InlineData("HumidityControlStep")]
        [InlineData("HumidityControlStop")]
        [InlineData("CO2ControlStatus")]
        [InlineData("CO2ControlTarget")]
        [InlineData("CO2ControlStep")]
        [InlineData("VOCControlStatus")]
        [InlineData("VOCControlTarget")]
        [InlineData("VOCControlStep")]
        [InlineData("ThermalComfortTemperature")]
        [InlineData("TimeZoneOffset")]
        [InlineData("DateFormat")]
        [InlineData("HeatExchangerType")]
        [InlineData("PartyOperationDuration")]
        [InlineData("PartyVentilationLevel")]
        [InlineData("PartyOperationRemaining")]
        [InlineData("PartyOperationActivate")]
        [InlineData("StandbyOperationDuration")]
        [InlineData("StandbyVentilationLevel")]
        [InlineData("StandbyOperationRemaining")]
        [InlineData("StandbyOperationActivate")]
        [InlineData("OperationMode")]
        [InlineData("VentilationLevel")]
        [InlineData("VentilationPercentage")]
        [InlineData("TemperatureOutdoor")]
        [InlineData("TemperatureSupply")]
        [InlineData("TemperatureExhaust")]
        [InlineData("TemperatureExtract")]
        [InlineData("TemperaturePreHeater")]
        [InlineData("TemperaturePostHeater")]
        [InlineData("ExternalHumiditySensor1")]
        [InlineData("ExternalHumiditySensor2")]
        [InlineData("ExternalHumiditySensor3")]
        [InlineData("ExternalHumiditySensor4")]
        [InlineData("ExternalHumiditySensor5")]
        [InlineData("ExternalHumiditySensor6")]
        [InlineData("ExternalHumiditySensor7")]
        [InlineData("ExternalHumiditySensor8")]
        [InlineData("ExternalHumidityTemperature1")]
        [InlineData("ExternalHumidityTemperature2")]
        [InlineData("ExternalHumidityTemperature3")]
        [InlineData("ExternalHumidityTemperature4")]
        [InlineData("ExternalHumidityTemperature5")]
        [InlineData("ExternalHumidityTemperature6")]
        [InlineData("ExternalHumidityTemperature7")]
        [InlineData("ExternalHumidityTemperature8")]
        [InlineData("ExternalCO2Sensor1")]
        [InlineData("ExternalCO2Sensor2")]
        [InlineData("ExternalCO2Sensor3")]
        [InlineData("ExternalCO2Sensor4")]
        [InlineData("ExternalCO2Sensor5")]
        [InlineData("ExternalCO2Sensor6")]
        [InlineData("ExternalCO2Sensor7")]
        [InlineData("ExternalCO2Sensor8")]
        [InlineData("ExternalVOCSensor1")]
        [InlineData("ExternalVOCSensor2")]
        [InlineData("ExternalVOCSensor3")]
        [InlineData("ExternalVOCSensor4")]
        [InlineData("ExternalVOCSensor5")]
        [InlineData("ExternalVOCSensor6")]
        [InlineData("ExternalVOCSensor7")]
        [InlineData("ExternalVOCSensor8")]
        [InlineData("TemperatureChannel")]
        [InlineData("WeeklyProfile")]
        [InlineData("SerialNumber")]
        [InlineData("ProductionCode")]
        [InlineData("SupplyFanSpeed")]
        [InlineData("ExhaustFanSpeed")]
        [InlineData("VacationOperation")]
        [InlineData("VacationVentilationLevel")]
        [InlineData("VacationStartDate")]
        [InlineData("VacationEndDate")]
        [InlineData("VacationInterval")]
        [InlineData("VacationDuration")]
        [InlineData("PreheaterType")]
        [InlineData("KwlFunctionType")]
        [InlineData("HeaterAfterRunTime")]
        [InlineData("ExternalContact")]
        [InlineData("FaultTypeOutput")]
        [InlineData("FilterChange")]
        [InlineData("FilterChangeInterval")]
        [InlineData("FilterChangeRemaining")]
        [InlineData("BypassRoomTemperature")]
        [InlineData("BypassOutdoorTemperature")]
        [InlineData("BypassOutdoorTemperature2")]
        [InlineData("SupplyLevel")]
        [InlineData("ExhaustLevel")]
        [InlineData("FanLevelRegion02")]
        [InlineData("FanLevelRegion24")]
        [InlineData("FanLevelRegion46")]
        [InlineData("FanLevelRegion68")]
        [InlineData("FanLevelRegion80")]
        [InlineData("OffsetExhaust")]
        [InlineData("FanLevelConfiguration")]
        [InlineData("SensorName1")]
        [InlineData("SensorName2")]
        [InlineData("SensorName3")]
        [InlineData("SensorName4")]
        [InlineData("SensorName5")]
        [InlineData("SensorName6")]
        [InlineData("SensorName7")]
        [InlineData("SensorName8")]
        [InlineData("CO2SensorName1")]
        [InlineData("CO2SensorName2")]
        [InlineData("CO2SensorName3")]
        [InlineData("CO2SensorName4")]
        [InlineData("CO2SensorName5")]
        [InlineData("CO2SensorName6")]
        [InlineData("CO2SensorName7")]
        [InlineData("CO2SensorName8")]
        [InlineData("VOCSensorName1")]
        [InlineData("VOCSensorName2")]
        [InlineData("VOCSensorName3")]
        [InlineData("VOCSensorName4")]
        [InlineData("VOCSensorName5")]
        [InlineData("VOCSensorName6")]
        [InlineData("VOCSensorName7")]
        [InlineData("VOCSensorName8")]
        [InlineData("SoftwareVersion")]
        [InlineData("OperationMinutesSupply")]
        [InlineData("OperationMinutesExhaust")]
        [InlineData("OperationMinutesPreheater")]
        [InlineData("OperationMinutesAfterheater")]
        [InlineData("PowerPreheater")]
        [InlineData("PowerAfterheater")]
        // ResetFlag returns all zeros.
        [InlineData("ErrorCode")]
        [InlineData("WarningCode")]
        [InlineData("InfoCode")]
        [InlineData("NumberOfErrors")]
        [InlineData("NumberOfWarnings")]
        [InlineData("NumberOfInfos")]
        [InlineData("Errors")]
        [InlineData("Warnings")]
        [InlineData("Infos")]
        [InlineData("StatusFlags")]
        // GlobalUpdate returns all zeros.
        // LastError returns all zeros.
        [InlineData("SensorConfig1")]
        [InlineData("SensorConfig2")]
        [InlineData("SensorConfig3")]
        [InlineData("SensorConfig4")]
        [InlineData("SensorConfig5")]
        [InlineData("SensorConfig6")]
        [InlineData("SensorConfig7")]
        [InlineData("SensorConfig8")]
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/KWLEC200/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        #endregion
    }
}
