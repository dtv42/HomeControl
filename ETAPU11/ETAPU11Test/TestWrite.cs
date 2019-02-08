// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWrite.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Test
{
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using ETAPU11Lib;
    using ETAPU11Lib.Models;

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("ETAPU11 Test Collection")]
    public class TestWrite : IClassFixture<ETAPU11Fixture>
    {
        #region Private Data Members

        private readonly ILogger<ETAPU11> _logger;
        private readonly IETAPU11 _etapu11;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWrite"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWrite(ETAPU11Fixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<ETAPU11>();

            _etapu11 = fixture.ETAPU11;

            // Default to localhost for write tests.
            _etapu11.TcpSlave.Address = "127.0.0.1";
        }

        [Fact]
        public async Task TestETAPU11Write()
        {
            await _etapu11.WriteAllAsync();
            Assert.True(_etapu11.Data.IsGood);
        }

        [Theory]
        [InlineData("AshRemovalStartIdleTime", "21:00:00")]
        [InlineData("AshRemovalDurationIdleTime", "10:00:00")]
        [InlineData("HeatingHolidayStart", "2018-07-26T00:00:00")]
        [InlineData("HeatingHolidayEnd", "2018-08-02T23:59:00")]
        [InlineData("EmptyAshBoxAfter", "1000.0")]
        [InlineData("HopperPelletBinContents", "30.0")]
        [InlineData("HopperFillUpTime", "19:00:00")]
        [InlineData("HotwaterSwitchonDiff", "15.0")]
        [InlineData("HeatingTemperature", "20.0")]
        [InlineData("FlowAtMinus10", "55.0")]
        [InlineData("FlowAtPlus10", "35.0")]
        [InlineData("FlowSetBack", "15.0")]
        [InlineData("DayHeatingThreshold", "16.0")]
        [InlineData("NightHeatingThreshold", "2.0")]
        [InlineData("Stock", "2000.0")]
        [InlineData("StockWarningLimit", "800.0")]
        [InlineData("OutsideTemperature", "22.0")]
        [InlineData("HopperFillUpPelletBin", "Yes")]
        [InlineData("OnOffButton", "On")]
        [InlineData("DeAshButton", "On")]
        [InlineData("ChargeButton", "On")]
        [InlineData("HeatingDayButton", "On")]
        [InlineData("HeatingAutoButton", "On")]
        [InlineData("HeatingNightButton", "On")]
        [InlineData("HeatingOnOffButton", "On")]
        [InlineData("HeatingHomeButton", "On")]
        [InlineData("HeatingAwayButton", "On")]
        public async Task TestETAPU11WriteProperty(string property, string data)
        {
            Assert.True(ETAPU11Data.IsProperty(property));
            Assert.True(ETAPU11Data.IsWritable(property));
            var status = await _etapu11.WriteDataAsync(property, data);
            Assert.True(status.IsGood);
        }
    }
}
