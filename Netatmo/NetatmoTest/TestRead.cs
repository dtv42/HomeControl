// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRead.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoTest
{
    #region Using Directives

    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using NetatmoLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Netatmo Test Collection")]
    public class TestRead : IClassFixture<NetatmoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Netatmo> _logger;
        private readonly INetatmo _netatmo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestRead(NetatmoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Netatmo>();

            _netatmo = fixture.Netatmo;
        }

        #endregion

        #region Test Methods

        [Fact]
        public void TestNetatmoRead()
        {
            Assert.True(_netatmo.Station.Status.IsGood);
            _logger?.LogInformation($"Netatmo: {JsonConvert.SerializeObject(_netatmo, Formatting.Indented)}");
        }

        [Fact]
        public async Task TestReadAllData()
        {
            await _netatmo.ReadAllAsync();
            Assert.True(_netatmo.Station.Status.IsGood);
            Assert.Equal("ok", _netatmo.Station.Response.Status);
            Assert.Equal("peter.trimmel@live.com", _netatmo.Station.User.Mail);
            Assert.Equal("70:ee:50:03:cd:88", _netatmo.Station.Device.ID);
            Assert.Equal("02:00:00:03:d7:50", _netatmo.Station.Device.OutdoorModule.ID);
            Assert.Equal("03:00:00:01:b4:f6", _netatmo.Station.Device.IndoorModule1.ID);
            Assert.Equal("03:00:00:02:1a:4c", _netatmo.Station.Device.IndoorModule2.ID);
            Assert.Equal("03:00:00:05:89:6e", _netatmo.Station.Device.IndoorModule3.ID);
            Assert.Equal("05:00:00:00:ed:4a", _netatmo.Station.Device.RainGauge.ID);
            Assert.Equal("06:00:00:02:40:cc", _netatmo.Station.Device.WindGauge.ID);
        }

        [Theory]
        [InlineData("MainModule")]
        [InlineData("MainModule.TimeUTC")]
        [InlineData("MainModule.Temperature")]
        [InlineData("MainModule.TempTrend")]
        [InlineData("MainModule.Humidity")]
        [InlineData("MainModule.Noise")]
        [InlineData("MainModule.CO2")]
        [InlineData("MainModule.Pressure")]
        [InlineData("MainModule.PressureTrend")]
        [InlineData("MainModule.AbsolutePressure")]
        [InlineData("MainModule.DateMaxTemp")]
        [InlineData("MainModule.DateMinTemp")]
        [InlineData("MainModule.MinTemp")]
        [InlineData("MainModule.MaxTemp")]

        [InlineData("OutdoorModule")]
        [InlineData("OutdoorModule.TimeUTC")]
        [InlineData("OutdoorModule.Temperature")]
        [InlineData("OutdoorModule.TempTrend")]
        [InlineData("OutdoorModule.Humidity")]
        [InlineData("OutdoorModule.DateMaxTemp")]
        [InlineData("OutdoorModule.DateMinTemp")]
        [InlineData("OutdoorModule.MaxTemp")]
        [InlineData("OutdoorModule.MinTemp")]

        [InlineData("IndoorModule1")]
        [InlineData("IndoorModule1.TimeUTC")]
        [InlineData("IndoorModule1.Temperature")]
        [InlineData("IndoorModule1.TempTrend")]
        [InlineData("IndoorModule1.Humidity")]
        [InlineData("IndoorModule1.CO2")]
        [InlineData("IndoorModule1.DateMaxTemp")]
        [InlineData("IndoorModule1.DateMinTemp")]
        [InlineData("IndoorModule1.MaxTemp")]
        [InlineData("IndoorModule1.MinTemp")]

        [InlineData("IndoorModule2")]
        [InlineData("IndoorModule2.TimeUTC")]
        [InlineData("IndoorModule2.Temperature")]
        [InlineData("IndoorModule2.TempTrend")]
        [InlineData("IndoorModule2.Humidity")]
        [InlineData("IndoorModule2.CO2")]
        [InlineData("IndoorModule2.DateMaxTemp")]
        [InlineData("IndoorModule2.DateMinTemp")]
        [InlineData("IndoorModule2.MaxTemp")]
        [InlineData("IndoorModule2.MinTemp")]

        [InlineData("IndoorModule3")]
        [InlineData("IndoorModule3.TimeUTC")]
        [InlineData("IndoorModule3.Temperature")]
        [InlineData("IndoorModule3.TempTrend")]
        [InlineData("IndoorModule3.Humidity")]
        [InlineData("IndoorModule3.CO2")]
        [InlineData("IndoorModule3.DateMaxTemp")]
        [InlineData("IndoorModule3.DateMinTemp")]
        [InlineData("IndoorModule3.MaxTemp")]
        [InlineData("IndoorModule3.MinTemp")]

        [InlineData("RainGauge")]
        [InlineData("RainGauge.TimeUTC")]
        [InlineData("RainGauge.Rain")]
        [InlineData("RainGauge.SumRain1")]
        [InlineData("RainGauge.SumRain24")]

        [InlineData("WindGauge")]
        [InlineData("WindGauge.TimeUTC")]
        [InlineData("WindGauge.WindAngle")]
        [InlineData("WindGauge.WindStrength")]
        [InlineData("WindGauge.GustAngle")]
        [InlineData("WindGauge.GustStrength")]
        [InlineData("WindGauge.MaxWindAngle")]
        [InlineData("WindGauge.MaxWindStrength")]
        [InlineData("WindGauge.DateMaxWindStrength")]

        [InlineData("Station.Device")]
        [InlineData("Station.Device.ID")]
        [InlineData("Station.Device.CipherID")]
        [InlineData("Station.Device.StationName")]
        [InlineData("Station.Device.ModuleName")]
        [InlineData("Station.Device.Firmware")]
        [InlineData("Station.Device.WifiStatus")]
        [InlineData("Station.Device.CO2Calibrating")]
        [InlineData("Station.Device.Type")]
        [InlineData("Station.Device.DataType[0]")]

        [InlineData("Station.Device.Place")]
        [InlineData("Station.Device.Place.Altitude")]
        [InlineData("Station.Device.Place.City")]
        [InlineData("Station.Device.Place.Country")]
        [InlineData("Station.Device.Place.GeoIpCity")]
        [InlineData("Station.Device.Place.ImproveLocProposed")]
        [InlineData("Station.Device.Place.Location")]
        [InlineData("Station.Device.Place.Location.Latitude")]
        [InlineData("Station.Device.Place.Location.Longitude")]
        [InlineData("Station.Device.Place.Timezone")]

        [InlineData("Station.Device.DashboardData")]
        [InlineData("Station.Device.DashboardData.TimeUTC")]
        [InlineData("Station.Device.DashboardData.Temperature")]
        [InlineData("Station.Device.DashboardData.TempTrend")]
        [InlineData("Station.Device.DashboardData.Humidity")]
        [InlineData("Station.Device.DashboardData.Noise")]
        [InlineData("Station.Device.DashboardData.CO2")]
        [InlineData("Station.Device.DashboardData.Pressure")]
        [InlineData("Station.Device.DashboardData.PressureTrend")]
        [InlineData("Station.Device.DashboardData.AbsolutePressure")]
        [InlineData("Station.Device.DashboardData.DateMaxTemp")]
        [InlineData("Station.Device.DashboardData.DateMinTemp")]
        [InlineData("Station.Device.DashboardData.MinTemp")]
        [InlineData("Station.Device.DashboardData.MaxTemp")]

        [InlineData("Station.Device.LastStatusStore")]
        [InlineData("Station.Device.DateSetup")]
        [InlineData("Station.Device.LastSetup")]
        [InlineData("Station.Device.LastUpgrade")]

        [InlineData("Station.Device.OutdoorModule")]
        [InlineData("Station.Device.OutdoorModule.DashboardData")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.TimeUTC")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.Temperature")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.TempTrend")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.Humidity")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.DateMaxTemp")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.DateMinTemp")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.MaxTemp")]
        [InlineData("Station.Device.OutdoorModule.DashboardData.MinTemp")]

        [InlineData("Station.Device.OutdoorModule.ID")]
        [InlineData("Station.Device.OutdoorModule.Type")]
        [InlineData("Station.Device.OutdoorModule.DataType")]
        [InlineData("Station.Device.OutdoorModule.DataType[0]")]
        [InlineData("Station.Device.OutdoorModule.ModuleName")]
        [InlineData("Station.Device.OutdoorModule.LastMessage")]
        [InlineData("Station.Device.OutdoorModule.LastSeen")]
        [InlineData("Station.Device.OutdoorModule.LastSetup")]
        [InlineData("Station.Device.OutdoorModule.BatteryVP")]
        [InlineData("Station.Device.OutdoorModule.BatteryPercent")]
        [InlineData("Station.Device.OutdoorModule.RFStatus")]
        [InlineData("Station.Device.OutdoorModule.Firmware")]

        [InlineData("Station.Device.IndoorModule1")]

        [InlineData("Station.Device.IndoorModule1.DashboardData")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.TimeUTC")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.Temperature")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.TempTrend")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.Humidity")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.CO2")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.DateMaxTemp")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.DateMinTemp")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.MaxTemp")]
        [InlineData("Station.Device.IndoorModule1.DashboardData.MinTemp")]

        [InlineData("Station.Device.IndoorModule1.ID")]
        [InlineData("Station.Device.IndoorModule1.Type")]
        [InlineData("Station.Device.IndoorModule1.DataType")]
        [InlineData("Station.Device.IndoorModule1.DataType[0]")]
        [InlineData("Station.Device.IndoorModule1.ModuleName")]
        [InlineData("Station.Device.IndoorModule1.LastMessage")]
        [InlineData("Station.Device.IndoorModule1.LastSeen")]
        [InlineData("Station.Device.IndoorModule1.LastSetup")]
        [InlineData("Station.Device.IndoorModule1.BatteryVP")]
        [InlineData("Station.Device.IndoorModule1.BatteryPercent")]
        [InlineData("Station.Device.IndoorModule1.RFStatus")]
        [InlineData("Station.Device.IndoorModule1.Firmware")]

        [InlineData("Station.Device.IndoorModule2")]
        [InlineData("Station.Device.IndoorModule2.DashboardData")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.TimeUTC")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.Temperature")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.TempTrend")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.Humidity")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.CO2")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.DateMaxTemp")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.DateMinTemp")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.MaxTemp")]
        [InlineData("Station.Device.IndoorModule2.DashboardData.MinTemp")]

        [InlineData("Station.Device.IndoorModule2.ID")]
        [InlineData("Station.Device.IndoorModule2.Type")]
        [InlineData("Station.Device.IndoorModule2.DataType")]
        [InlineData("Station.Device.IndoorModule2.DataType[0]")]
        [InlineData("Station.Device.IndoorModule2.ModuleName")]
        [InlineData("Station.Device.IndoorModule2.LastMessage")]
        [InlineData("Station.Device.IndoorModule2.LastSeen")]
        [InlineData("Station.Device.IndoorModule2.LastSetup")]
        [InlineData("Station.Device.IndoorModule2.BatteryVP")]
        [InlineData("Station.Device.IndoorModule2.BatteryPercent")]
        [InlineData("Station.Device.IndoorModule2.RFStatus")]
        [InlineData("Station.Device.IndoorModule2.Firmware")]

        [InlineData("Station.Device.IndoorModule3")]
        [InlineData("Station.Device.IndoorModule3.DashboardData")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.TimeUTC")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.Temperature")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.TempTrend")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.Humidity")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.CO2")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.DateMaxTemp")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.DateMinTemp")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.MaxTemp")]
        [InlineData("Station.Device.IndoorModule3.DashboardData.MinTemp")]

        [InlineData("Station.Device.IndoorModule3.ID")]
        [InlineData("Station.Device.IndoorModule3.Type")]
        [InlineData("Station.Device.IndoorModule3.DataType")]
        [InlineData("Station.Device.IndoorModule3.DataType[0]")]
        [InlineData("Station.Device.IndoorModule3.ModuleName")]
        [InlineData("Station.Device.IndoorModule3.LastMessage")]
        [InlineData("Station.Device.IndoorModule3.LastSeen")]
        [InlineData("Station.Device.IndoorModule3.LastSetup")]
        [InlineData("Station.Device.IndoorModule3.BatteryVP")]
        [InlineData("Station.Device.IndoorModule3.BatteryPercent")]
        [InlineData("Station.Device.IndoorModule3.RFStatus")]
        [InlineData("Station.Device.IndoorModule3.Firmware")]

        [InlineData("Station.Device.RainGauge")]
        [InlineData("Station.Device.RainGauge.DashboardData")]
        [InlineData("Station.Device.RainGauge.DashboardData.TimeUTC")]
        [InlineData("Station.Device.RainGauge.DashboardData.Rain")]
        [InlineData("Station.Device.RainGauge.DashboardData.SumRain1")]
        [InlineData("Station.Device.RainGauge.DashboardData.SumRain24")]

        [InlineData("Station.Device.RainGauge.ID")]
        [InlineData("Station.Device.RainGauge.Type")]
        [InlineData("Station.Device.RainGauge.DataType")]
        [InlineData("Station.Device.RainGauge.DataType[0]")]
        [InlineData("Station.Device.RainGauge.ModuleName")]
        [InlineData("Station.Device.RainGauge.LastMessage")]
        [InlineData("Station.Device.RainGauge.LastSeen")]
        [InlineData("Station.Device.RainGauge.LastSetup")]
        [InlineData("Station.Device.RainGauge.BatteryVP")]
        [InlineData("Station.Device.RainGauge.BatteryPercent")]
        [InlineData("Station.Device.RainGauge.RFStatus")]
        [InlineData("Station.Device.RainGauge.Firmware")]

        [InlineData("Station.Device.WindGauge")]
        [InlineData("Station.Device.WindGauge.DashboardData")]
        [InlineData("Station.Device.WindGauge.DashboardData.TimeUTC")]
        [InlineData("Station.Device.WindGauge.DashboardData.WindAngle")]
        [InlineData("Station.Device.WindGauge.DashboardData.WindStrength")]
        [InlineData("Station.Device.WindGauge.DashboardData.GustAngle")]
        [InlineData("Station.Device.WindGauge.DashboardData.GustStrength")]
        [InlineData("Station.Device.WindGauge.DashboardData.MaxWindAngle")]
        [InlineData("Station.Device.WindGauge.DashboardData.MaxWindStrength")]
        [InlineData("Station.Device.WindGauge.DashboardData.DateMaxWindStrength")]

        [InlineData("Station.Device.WindGauge.ID")]
        [InlineData("Station.Device.WindGauge.Type")]
        [InlineData("Station.Device.WindGauge.DataType")]
        [InlineData("Station.Device.WindGauge.DataType[0]")]
        [InlineData("Station.Device.WindGauge.ModuleName")]
        [InlineData("Station.Device.WindGauge.LastMessage")]
        [InlineData("Station.Device.WindGauge.LastSeen")]
        [InlineData("Station.Device.WindGauge.LastSetup")]
        [InlineData("Station.Device.WindGauge.BatteryVP")]
        [InlineData("Station.Device.WindGauge.BatteryPercent")]
        [InlineData("Station.Device.WindGauge.RFStatus")]
        [InlineData("Station.Device.WindGauge.Firmware")]

        [InlineData("Station.User")]
        [InlineData("Station.User.Mail")]
        [InlineData("Station.User.Administrative")]
        [InlineData("Station.User.Administrative.Country")]
        [InlineData("Station.User.Administrative.FeelsLikeAlgorithm")]
        [InlineData("Station.User.Administrative.Language")]
        [InlineData("Station.User.Administrative.PressureUnit")]
        [InlineData("Station.User.Administrative.RegLocale")]
        [InlineData("Station.User.Administrative.Unit")]
        [InlineData("Station.User.Administrative.WindUnit")]

        [InlineData("Station.Response")]
        [InlineData("Station.Response.Status")]
        [InlineData("Station.Response.TimeExec")]
        [InlineData("Station.Response.TimeServer")]

        [InlineData("Netatmo.BaseAddress")]
        [InlineData("Netatmo.User")]
        [InlineData("Netatmo.Password")]
        [InlineData("Netatmo.ClientID")]
        [InlineData("Netatmo.ClientSecret")]
        [InlineData("Netatmo.Scope")]
        [InlineData("Netatmo.Token")]
        [InlineData("Netatmo.Expiration")]

        [InlineData("Netatmo.Station")]
        [InlineData("Netatmo.Station.Device")]
        [InlineData("Netatmo.Station.Device.ID")]
        [InlineData("Netatmo.Station.Device.CipherID")]
        [InlineData("Netatmo.Station.Device.StationName")]
        [InlineData("Netatmo.Station.Device.ModuleName")]
        [InlineData("Netatmo.Station.Device.Firmware")]
        [InlineData("Netatmo.Station.Device.WifiStatus")]
        [InlineData("Netatmo.Station.Device.CO2Calibrating")]
        [InlineData("Netatmo.Station.Device.Type")]
        [InlineData("Netatmo.Station.Device.DataType[0]")]

        [InlineData("Netatmo.Station.Device.Place")]
        [InlineData("Netatmo.Station.Device.Place.Altitude")]
        [InlineData("Netatmo.Station.Device.Place.City")]
        [InlineData("Netatmo.Station.Device.Place.Country")]
        [InlineData("Netatmo.Station.Device.Place.GeoIpCity")]
        [InlineData("Netatmo.Station.Device.Place.ImproveLocProposed")]
        [InlineData("Netatmo.Station.Device.Place.Location")]
        [InlineData("Netatmo.Station.Device.Place.Location.Latitude")]
        [InlineData("Netatmo.Station.Device.Place.Location.Longitude")]
        [InlineData("Netatmo.Station.Device.Place.Timezone")]

        [InlineData("Netatmo.Station.Device.DashboardData")]
        [InlineData("Netatmo.Station.Device.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.DashboardData.Temperature")]
        [InlineData("Netatmo.Station.Device.DashboardData.TempTrend")]
        [InlineData("Netatmo.Station.Device.DashboardData.Humidity")]
        [InlineData("Netatmo.Station.Device.DashboardData.Noise")]
        [InlineData("Netatmo.Station.Device.DashboardData.CO2")]
        [InlineData("Netatmo.Station.Device.DashboardData.Pressure")]
        [InlineData("Netatmo.Station.Device.DashboardData.PressureTrend")]
        [InlineData("Netatmo.Station.Device.DashboardData.AbsolutePressure")]
        [InlineData("Netatmo.Station.Device.DashboardData.DateMaxTemp")]
        [InlineData("Netatmo.Station.Device.DashboardData.DateMinTemp")]
        [InlineData("Netatmo.Station.Device.DashboardData.MinTemp")]
        [InlineData("Netatmo.Station.Device.DashboardData.MaxTemp")]

        [InlineData("Netatmo.Station.Device.LastStatusStore")]
        [InlineData("Netatmo.Station.Device.DateSetup")]
        [InlineData("Netatmo.Station.Device.LastSetup")]
        [InlineData("Netatmo.Station.Device.LastUpgrade")]

        [InlineData("Netatmo.Station.Device.OutdoorModule")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.Temperature")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.TempTrend")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.Humidity")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.DateMaxTemp")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.DateMinTemp")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.MaxTemp")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DashboardData.MinTemp")]

        [InlineData("Netatmo.Station.Device.OutdoorModule.ID")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.Type")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DataType")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.DataType[0]")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.ModuleName")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.LastMessage")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.LastSeen")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.LastSetup")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.BatteryVP")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.BatteryPercent")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.RFStatus")]
        [InlineData("Netatmo.Station.Device.OutdoorModule.Firmware")]

        [InlineData("Netatmo.Station.Device.IndoorModule1")]

        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.Temperature")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.TempTrend")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.Humidity")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.CO2")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.DateMaxTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.DateMinTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.MaxTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DashboardData.MinTemp")]

        [InlineData("Netatmo.Station.Device.IndoorModule1.ID")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.Type")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DataType")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.DataType[0]")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.ModuleName")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.LastMessage")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.LastSeen")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.LastSetup")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.BatteryVP")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.BatteryPercent")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.RFStatus")]
        [InlineData("Netatmo.Station.Device.IndoorModule1.Firmware")]

        [InlineData("Netatmo.Station.Device.IndoorModule2")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.Temperature")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.TempTrend")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.Humidity")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.CO2")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.DateMaxTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.DateMinTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.MaxTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DashboardData.MinTemp")]

        [InlineData("Netatmo.Station.Device.IndoorModule2.ID")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.Type")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DataType")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.DataType[0]")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.ModuleName")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.LastMessage")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.LastSeen")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.LastSetup")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.BatteryVP")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.BatteryPercent")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.RFStatus")]
        [InlineData("Netatmo.Station.Device.IndoorModule2.Firmware")]

        [InlineData("Netatmo.Station.Device.IndoorModule3")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.Temperature")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.TempTrend")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.Humidity")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.CO2")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.DateMaxTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.DateMinTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.MaxTemp")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DashboardData.MinTemp")]

        [InlineData("Netatmo.Station.Device.IndoorModule3.ID")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.Type")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DataType")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.DataType[0]")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.ModuleName")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.LastMessage")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.LastSeen")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.LastSetup")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.BatteryVP")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.BatteryPercent")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.RFStatus")]
        [InlineData("Netatmo.Station.Device.IndoorModule3.Firmware")]

        [InlineData("Netatmo.Station.Device.RainGauge")]
        [InlineData("Netatmo.Station.Device.RainGauge.DashboardData")]
        [InlineData("Netatmo.Station.Device.RainGauge.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.RainGauge.DashboardData.Rain")]
        [InlineData("Netatmo.Station.Device.RainGauge.DashboardData.SumRain1")]
        [InlineData("Netatmo.Station.Device.RainGauge.DashboardData.SumRain24")]

        [InlineData("Netatmo.Station.Device.RainGauge.ID")]
        [InlineData("Netatmo.Station.Device.RainGauge.Type")]
        [InlineData("Netatmo.Station.Device.RainGauge.DataType")]
        [InlineData("Netatmo.Station.Device.RainGauge.DataType[0]")]
        [InlineData("Netatmo.Station.Device.RainGauge.ModuleName")]
        [InlineData("Netatmo.Station.Device.RainGauge.LastMessage")]
        [InlineData("Netatmo.Station.Device.RainGauge.LastSeen")]
        [InlineData("Netatmo.Station.Device.RainGauge.LastSetup")]
        [InlineData("Netatmo.Station.Device.RainGauge.BatteryVP")]
        [InlineData("Netatmo.Station.Device.RainGauge.BatteryPercent")]
        [InlineData("Netatmo.Station.Device.RainGauge.RFStatus")]
        [InlineData("Netatmo.Station.Device.RainGauge.Firmware")]

        [InlineData("Netatmo.Station.Device.WindGauge")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.TimeUTC")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.WindAngle")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.WindStrength")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.GustAngle")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.GustStrength")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.MaxWindAngle")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.MaxWindStrength")]
        [InlineData("Netatmo.Station.Device.WindGauge.DashboardData.DateMaxWindStrength")]

        [InlineData("Netatmo.Station.Device.WindGauge.ID")]
        [InlineData("Netatmo.Station.Device.WindGauge.Type")]
        [InlineData("Netatmo.Station.Device.WindGauge.DataType")]
        [InlineData("Netatmo.Station.Device.WindGauge.DataType[0]")]
        [InlineData("Netatmo.Station.Device.WindGauge.ModuleName")]
        [InlineData("Netatmo.Station.Device.WindGauge.LastMessage")]
        [InlineData("Netatmo.Station.Device.WindGauge.LastSeen")]
        [InlineData("Netatmo.Station.Device.WindGauge.LastSetup")]
        [InlineData("Netatmo.Station.Device.WindGauge.BatteryVP")]
        [InlineData("Netatmo.Station.Device.WindGauge.BatteryPercent")]
        [InlineData("Netatmo.Station.Device.WindGauge.RFStatus")]
        [InlineData("Netatmo.Station.Device.WindGauge.Firmware")]

        [InlineData("Netatmo.Station.User")]
        [InlineData("Netatmo.Station.User.Mail")]
        [InlineData("Netatmo.Station.User.Administrative")]
        [InlineData("Netatmo.Station.User.Administrative.Country")]
        [InlineData("Netatmo.Station.User.Administrative.FeelsLikeAlgorithm")]
        [InlineData("Netatmo.Station.User.Administrative.Language")]
        [InlineData("Netatmo.Station.User.Administrative.PressureUnit")]
        [InlineData("Netatmo.Station.User.Administrative.RegLocale")]
        [InlineData("Netatmo.Station.User.Administrative.Unit")]
        [InlineData("Netatmo.Station.User.Administrative.WindUnit")]

        [InlineData("Netatmo.Station.Response")]
        [InlineData("Netatmo.Station.Response.Status")]
        [InlineData("Netatmo.Station.Response.TimeExec")]
        [InlineData("Netatmo.Station.Response.TimeServer")]
        public void TestNetatmoReadProperty(string property)
        {
            Assert.True(Netatmo.IsProperty(property));
            Assert.NotNull(_netatmo.GetPropertyValue(property));
        }

        #endregion
    }
}
