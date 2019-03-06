// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRead.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Test
{
    #region Using Directives

    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using ETAPU11Lib;
    using ETAPU11Lib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("ETAPU11 Test Collection")]
    public class TestRead : IClassFixture<ETAPU11Fixture>
    {
        #region Private Data Members

        private readonly ILogger<ETAPU11> _logger;
        private readonly IETAPU11 _etapu11;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestRead(ETAPU11Fixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<ETAPU11>();

            _etapu11 = fixture.ETAPU11;
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestETAPU11Read()
        {
            await _etapu11.ReadAllAsync();
            Assert.True(_etapu11.Data.IsGood);
            Assert.True(_etapu11.BoilerData.IsGood);
            Assert.True(_etapu11.HotwaterData.IsGood);
            Assert.True(_etapu11.HeatingData.IsGood);
            Assert.True(_etapu11.StorageData.IsGood);
            Assert.True(_etapu11.SystemData.IsGood);
            _logger?.LogInformation($"ETAPU11: {JsonConvert.SerializeObject(_etapu11, Formatting.Indented)}");
        }

        [Fact]
        public async Task TestETAPU11BlockRead()
        {
            var status = await _etapu11.ReadBlockAllAsync();
            Assert.True(status.IsGood);
            Assert.True(_etapu11.Data.IsGood);
            Assert.True(_etapu11.BoilerData.IsGood);
            Assert.True(_etapu11.HotwaterData.IsGood);
            Assert.True(_etapu11.HeatingData.IsGood);
            Assert.True(_etapu11.StorageData.IsGood);
            Assert.True(_etapu11.SystemData.IsGood);
        }

        [Fact]
        public async Task TestReadData()
        {
            var status = await _etapu11.ReadBoilerDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_etapu11.BoilerData.IsGood);
            status = await _etapu11.ReadHotwaterDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_etapu11.HotwaterData.IsGood);
            status = await _etapu11.ReadHeatingDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_etapu11.HeatingData.IsGood);
            status = await _etapu11.ReadStorageDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_etapu11.StorageData.IsGood);
            status = await _etapu11.ReadSystemDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_etapu11.SystemData.IsGood);
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
        public async Task TestETAPU11ReadProperty(string property)
        {
            Assert.True(ETAPU11Data.IsProperty(property));
            Assert.True(ETAPU11Data.IsReadable(property));
            var status = await _etapu11.ReadPropertyAsync(property);
            Assert.True(status.IsGood);
        }

        #endregion
    }
}
