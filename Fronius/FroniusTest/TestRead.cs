// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRead.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusTest
{
    #region Using Directives

    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using FroniusLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Fronius Test Collection")]
    public class TestRead : IClassFixture<FroniusFixture>
    {
        #region Private Data Members

        private readonly ILogger<Fronius> _logger;
        private readonly IFronius _fronius;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestRead(FroniusFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Fronius>();

            _fronius = fixture.Fronius;
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestFroniusRead()
        {
            Assert.True(_fronius.IsInitialized);
            await _fronius.ReadAllAsync();
            Assert.True(_fronius.Data.Status.IsGood);
            Assert.True(_fronius.CommonData.Status.IsGood);
            Assert.True(_fronius.InverterInfo.Status.IsGood);
            Assert.True(_fronius.LoggerInfo.Status.IsGood);
            Assert.True(_fronius.MinMaxData.Status.IsGood);
            Assert.True(_fronius.PhaseData.Status.IsGood);
            _logger?.LogInformation($"Fronius: {JsonConvert.SerializeObject(_fronius, Formatting.Indented)}");
        }

        [Fact]
        public async Task TestReadData()
        {
            Assert.True(_fronius.IsInitialized);
            var status = await _fronius.ReadCommonDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_fronius.CommonData.Status.IsGood);
            status = await _fronius.ReadInverterInfoAsync();
            Assert.True(status.IsGood);
            Assert.True(_fronius.InverterInfo.Status.IsGood);
            status = await _fronius.ReadLoggerInfoAsync();
            Assert.True(status.IsGood);
            Assert.True(_fronius.LoggerInfo.Status.IsGood);
            status = await _fronius.ReadMinMaxDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_fronius.MinMaxData.Status.IsGood);
            status = await _fronius.ReadPhaseDataAsync();
            Assert.True(status.IsGood);
            Assert.True(_fronius.PhaseData.Status.IsGood);
        }

        [Theory]
        [InlineData("Fronius")]

        [InlineData("Fronius.CommonData")]
        [InlineData("Fronius.CommonData.Frequency")]
        [InlineData("Fronius.CommonData.CurrentDC")]
        [InlineData("Fronius.CommonData.CurrentAC")]
        [InlineData("Fronius.CommonData.VoltageDC")]
        [InlineData("Fronius.CommonData.VoltageAC")]
        [InlineData("Fronius.CommonData.PowerAC")]
        [InlineData("Fronius.CommonData.DailyEnergy")]
        [InlineData("Fronius.CommonData.YearlyEnergy")]
        [InlineData("Fronius.CommonData.TotalEnergy")]
        [InlineData("Fronius.CommonData.StatusCode")]

        [InlineData("Fronius.InverterInfo")]
        [InlineData("Fronius.InverterInfo.Index")]
        [InlineData("Fronius.InverterInfo.DeviceType")]
        [InlineData("Fronius.InverterInfo.PVPower")]
        [InlineData("Fronius.InverterInfo.CustomName")]
        [InlineData("Fronius.InverterInfo.Show")]
        [InlineData("Fronius.InverterInfo.UniqueID")]
        [InlineData("Fronius.InverterInfo.ErrorCode")]
        [InlineData("Fronius.InverterInfo.StatusCode")]

        [InlineData("Fronius.LoggerInfo")]
        [InlineData("Fronius.LoggerInfo.UniqueID")]
        [InlineData("Fronius.LoggerInfo.ProductID")]
        [InlineData("Fronius.LoggerInfo.PlatformID")]
        [InlineData("Fronius.LoggerInfo.HWVersion")]
        [InlineData("Fronius.LoggerInfo.SWVersion")]
        [InlineData("Fronius.LoggerInfo.TimezoneLocation")]
        [InlineData("Fronius.LoggerInfo.TimezoneName")]
        [InlineData("Fronius.LoggerInfo.UTCOffset")]
        [InlineData("Fronius.LoggerInfo.DefaultLanguage")]
        [InlineData("Fronius.LoggerInfo.CashFactor")]
        [InlineData("Fronius.LoggerInfo.CashCurrency")]
        [InlineData("Fronius.LoggerInfo.CO2Factor")]
        [InlineData("Fronius.LoggerInfo.CO2Unit")]

        [InlineData("Fronius.MinMaxData")]
        [InlineData("Fronius.MinMaxData.DailyMaxVoltageDC")]
        [InlineData("Fronius.MinMaxData.DailyMaxVoltageAC")]
        [InlineData("Fronius.MinMaxData.DailyMinVoltageAC")]
        [InlineData("Fronius.MinMaxData.YearlyMaxVoltageDC")]
        [InlineData("Fronius.MinMaxData.YearlyMaxVoltageAC")]
        [InlineData("Fronius.MinMaxData.YearlyMinVoltageAC")]
        [InlineData("Fronius.MinMaxData.TotalMaxVoltageDC")]
        [InlineData("Fronius.MinMaxData.TotalMaxVoltageAC")]
        [InlineData("Fronius.MinMaxData.TotalMinVoltageAC")]
        [InlineData("Fronius.MinMaxData.DailyMaxPower")]
        [InlineData("Fronius.MinMaxData.YearlyMaxPower")]
        [InlineData("Fronius.MinMaxData.TotalMaxPower")]

        [InlineData("Fronius.PhaseData")]
        [InlineData("Fronius.PhaseData.CurrentL1")]
        [InlineData("Fronius.PhaseData.CurrentL2")]
        [InlineData("Fronius.PhaseData.CurrentL3")]
        [InlineData("Fronius.PhaseData.VoltageL1N")]
        [InlineData("Fronius.PhaseData.VoltageL2N")]
        [InlineData("Fronius.PhaseData.VoltageL3N")]

        [InlineData("Common")]
        [InlineData("Common.Frequency")]
        [InlineData("Common.CurrentDC")]
        [InlineData("Common.CurrentAC")]
        [InlineData("Common.VoltageDC")]
        [InlineData("Common.VoltageAC")]
        [InlineData("Common.PowerAC")]
        [InlineData("Common.DailyEnergy")]
        [InlineData("Common.YearlyEnergy")]
        [InlineData("Common.TotalEnergy")]
        [InlineData("Common.StatusCode")]

        [InlineData("Inverter")]
        [InlineData("Inverter.Index")]
        [InlineData("Inverter.DeviceType")]
        [InlineData("Inverter.PVPower")]
        [InlineData("Inverter.CustomName")]
        [InlineData("Inverter.Show")]
        [InlineData("Inverter.UniqueID")]
        [InlineData("Inverter.ErrorCode")]
        [InlineData("Inverter.StatusCode")]

        [InlineData("Logger")]
        [InlineData("Logger.UniqueID")]
        [InlineData("Logger.ProductID")]
        [InlineData("Logger.PlatformID")]
        [InlineData("Logger.HWVersion")]
        [InlineData("Logger.SWVersion")]
        [InlineData("Logger.TimezoneLocation")]
        [InlineData("Logger.TimezoneName")]
        [InlineData("Logger.UTCOffset")]
        [InlineData("Logger.DefaultLanguage")]
        [InlineData("Logger.CashFactor")]
        [InlineData("Logger.CashCurrency")]
        [InlineData("Logger.CO2Factor")]
        [InlineData("Logger.CO2Unit")]

        [InlineData("MinMax")]
        [InlineData("MinMax.DailyMaxVoltageDC")]
        [InlineData("MinMax.DailyMaxVoltageAC")]
        [InlineData("MinMax.DailyMinVoltageAC")]
        [InlineData("MinMax.YearlyMaxVoltageDC")]
        [InlineData("MinMax.YearlyMaxVoltageAC")]
        [InlineData("MinMax.YearlyMinVoltageAC")]
        [InlineData("MinMax.TotalMaxVoltageDC")]
        [InlineData("MinMax.TotalMaxVoltageAC")]
        [InlineData("MinMax.TotalMinVoltageAC")]
        [InlineData("MinMax.DailyMaxPower")]
        [InlineData("MinMax.YearlyMaxPower")]
        [InlineData("MinMax.TotalMaxPower")]

        [InlineData("Phase")]
        [InlineData("Phase.CurrentL1")]
        [InlineData("Phase.CurrentL2")]
        [InlineData("Phase.CurrentL3")]
        [InlineData("Phase.VoltageL1N")]
        [InlineData("Phase.VoltageL2N")]
        [InlineData("Phase.VoltageL3N")]
        public async Task TestFroniusReadProperty(string property)
        {
            Assert.True(_fronius.IsInitialized);
            Assert.True(Fronius.IsProperty(property));
            var status = await _fronius.ReadPropertyAsync(property);
            Assert.True(status.IsGood);
        }

        #endregion
    }
}
