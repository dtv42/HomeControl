// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Test
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

    using DataValueLib;
    using ETAPU11Lib;
    using ETAPU11Lib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("ETAPU11 Test Collection")]
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
            _logger = loggerFactory.CreateLogger<ETAPU11>();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8004");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestGetAllData()
        {
            // Act
            var response = await _client.GetAsync("api/ETAPU11/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ETAPU11Data>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetOverviewData()
        {
            // Act
            var response = await _client.GetAsync("api/ETAPU11/boiler");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BoilerData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetVoltageData()
        {
            // Act
            var response = await _client.GetAsync("api/ETAPU11/hotwater");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<HotwaterData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetCurrentData()
        {
            // Act
            var response = await _client.GetAsync("api/ETAPU11/heating");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<HeatingData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetPowerData()
        {
            // Act
            var response = await _client.GetAsync("api/ETAPU11/storage");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<StorageData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetFactorData()
        {
            // Act
            var response = await _client.GetAsync("api/ETAPU11/system");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SystemData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Theory]
        [InlineData("FullLoadHours")]
        [InlineData("TotalConsumed")]
        [InlineData("BoilerState")]
        [InlineData("BoilerPressure")]
        [InlineData("BoilerTemperature")]
        [InlineData("BoilerTarget")]
        [InlineData("BoilerBottom")]
        [InlineData("FlowControlState")]
        [InlineData("DiverterValveState")]
        [InlineData("DiverterValveDemand")]
        [InlineData("DemandedOutput")]
        [InlineData("FlowMixValveTarget")]
        [InlineData("FlowMixValveState")]
        [InlineData("FlowMixValveCurrTemp")]
        [InlineData("FlowMixValvePosition")]
        [InlineData("BoilerPumpOutput")]
        [InlineData("BoilerPumpDemand")]
        [InlineData("FlueGasTemperature")]
        [InlineData("DraughtFanSpeed")]
        [InlineData("ResidualO2")]
        [InlineData("StokerScrewDemand")]
        [InlineData("StokerScrewClockRate")]
        [InlineData("StokerScrewState")]
        [InlineData("StokerScrewMotorCurr")]
        [InlineData("AshRemovalState")]
        [InlineData("AshRemovalStartIdleTime")]
        [InlineData("AshRemovalDurationIdleTime")]
        [InlineData("ConsumptionSinceDeAsh")]
        [InlineData("ConsumptionSinceAshBoxEmptied")]
        [InlineData("EmptyAshBoxAfter")]
        [InlineData("ConsumptionSinceMaintainence")]
        [InlineData("HopperState")]
        [InlineData("HopperFillUpPelletBin")]
        [InlineData("HopperPelletBinContents")]
        [InlineData("HopperFillUpTime")]
        [InlineData("HopperVacuumState")]
        [InlineData("HopperVacuumDemand")]
        [InlineData("OnOffButton")]
        [InlineData("DeAshButton")]
        [InlineData("HotwaterTankState")]
        [InlineData("ChargingTimesState")]
        [InlineData("ChargingTimesSwitchStatus")]
        [InlineData("ChargingTimesTemperature")]
        [InlineData("HotwaterSwitchonDiff")]
        [InlineData("HotwaterTarget")]
        [InlineData("HotwaterTemperature")]
        [InlineData("ChargeButton")]
        [InlineData("RoomSensor")]
        [InlineData("HeatingCircuitState")]
        [InlineData("RunningState")]
        [InlineData("HeatingTimes")]
        [InlineData("HeatingSwitchStatus")]
        [InlineData("HeatingTemperature")]
        [InlineData("RoomTemperature")]
        [InlineData("RoomTarget")]
        [InlineData("Flow")]
        [InlineData("HeatingCurve")]
        [InlineData("FlowAtMinus10")]
        [InlineData("FlowAtPlus10")]
        [InlineData("FlowSetBack")]
        [InlineData("OutsideTemperatureDelayed")]
        [InlineData("DayHeatingThreshold")]
        [InlineData("NightHeatingThreshold")]
        [InlineData("HeatingDayButton")]
        [InlineData("HeatingAutoButton")]
        [InlineData("HeatingNightButton")]
        [InlineData("HeatingOnOffButton")]
        [InlineData("HeatingHomeButton")]
        [InlineData("HeatingAwayButton")]
        [InlineData("HeatingHolidayStart")]
        [InlineData("HeatingHolidayEnd")]
        [InlineData("DischargeScrewDemand")]
        [InlineData("DischargeScrewClockRate")]
        [InlineData("DischargeScrewState")]
        [InlineData("DischargeScrewMotorCurr")]
        [InlineData("ConveyingSystem")]
        [InlineData("Stock")]
        [InlineData("StockWarningLimit")]
        [InlineData("OutsideTemperature")]
        [InlineData("FirebedState")]
        [InlineData("SupplyDemand")]
        [InlineData("IgnitionDemand")]
        [InlineData("FlowMixValveTemperature")]
        [InlineData("AirValveSetPosition")]
        [InlineData("AirValveCurrPosition")]
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/ETAPU11/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        #endregion
    }
}
