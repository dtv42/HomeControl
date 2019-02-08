// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoTest
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
    using NetatmoLib;
    using NetatmoLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Netatmo Test Collection")]
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
            _logger = loggerFactory.CreateLogger<Netatmo>();
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8002")
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
            var response = await _client.GetAsync("api/Netatmo/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NetatmoData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetMainData()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/main");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<StationDeviceData>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestGetOutdoorData()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/outdoor");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OutdoorModuleData>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestGetIndoor1Data()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/indoor1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IndoorModuleData>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestGetIndoor2Data()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/indoor2");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IndoorModuleData>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestGetIndoor3Data()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/indoor3");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IndoorModuleData>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestGetRainData()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/rain");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<RainGaugeData>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestGetWindData()
        {
            // Act
            var response = await _client.GetAsync("api/Netatmo/wind");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<WindGaugeData>(responseString);
            Assert.NotNull(data);
        }

        [Theory]
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
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/Netatmo/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        #endregion
    }
}
