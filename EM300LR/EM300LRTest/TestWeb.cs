// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRTest
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
    using EM300LRLib;
    using EM300LRLib.Models;

    #endregion Using Directives

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("EM300LR Test Collection")]
    public class TestWeb
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HttpClient _client;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWeb"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWeb(ITestOutputHelper output)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<EM300LR>();
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:8012")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion Constructors

        #region Test Methods

        [Fact]
        public async Task TestGetAllData()
        {
            // Act
            var response = await _client.GetAsync("api/em300lr/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<EM300LRData>(responseString);
            Assert.NotNull(data);
            Assert.True(data.IsGood);
        }

        [Fact]
        public async Task TestGetTotalData()
        {
            // Act
            var response = await _client.GetAsync("api/em300lr/total");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TotalData>(responseString);
            Assert.NotNull(data);
            Assert.True(data.IsGood);
        }

        [Fact]
        public async Task TestGetPhase1Data()
        {
            // Act
            var response = await _client.GetAsync("api/em300lr/phase1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Phase1Data>(responseString);
            Assert.NotNull(data);
            Assert.True(data.IsGood);
        }

        [Theory]
        [InlineData("EM300LR")]
        [InlineData("EM300LR.Data")]
        [InlineData("EM300LR.Data.ActivePowerPlus")]
        [InlineData("EM300LR.Data.ActiveEnergyPlus")]
        [InlineData("EM300LR.Data.ActivePowerMinus")]
        [InlineData("EM300LR.Data.ActiveEnergyMinus")]
        [InlineData("EM300LR.Data.ReactivePowerPlus")]
        [InlineData("EM300LR.Data.ReactiveEnergyPlus")]
        [InlineData("EM300LR.Data.ReactivePowerMinus")]
        [InlineData("EM300LR.Data.ReactiveEnergyMinus")]
        [InlineData("EM300LR.Data.ApparentPowerPlus")]
        [InlineData("EM300LR.Data.ApparentEnergyPlus")]
        [InlineData("EM300LR.Data.ApparentPowerMinus")]
        [InlineData("EM300LR.Data.ApparentEnergyMinus")]
        [InlineData("EM300LR.Data.PowerFactor")]
        [InlineData("EM300LR.Data.SupplyFrequency")]
        [InlineData("EM300LR.Data.ActivePowerPlusL1")]
        [InlineData("EM300LR.Data.ActiveEnergyPlusL1")]
        [InlineData("EM300LR.Data.ActivePowerMinusL1")]
        [InlineData("EM300LR.Data.ActiveEnergyMinusL1")]
        [InlineData("EM300LR.Data.ReactivePowerPlusL1")]
        [InlineData("EM300LR.Data.ReactiveEnergyPlusL1")]
        [InlineData("EM300LR.Data.ReactivePowerMinusL1")]
        [InlineData("EM300LR.Data.ReactiveEnergyMinusL1")]
        [InlineData("EM300LR.Data.ApparentPowerPlusL1")]
        [InlineData("EM300LR.Data.ApparentEnergyPlusL1")]
        [InlineData("EM300LR.Data.ApparentPowerMinusL1")]
        [InlineData("EM300LR.Data.ApparentEnergyMinusL1")]
        [InlineData("EM300LR.Data.CurrentL1")]
        [InlineData("EM300LR.Data.VoltageL1")]
        [InlineData("EM300LR.Data.PowerFactorL1")]
        [InlineData("EM300LR.Data.ActivePowerPlusL2")]
        [InlineData("EM300LR.Data.ActiveEnergyPlusL2")]
        [InlineData("EM300LR.Data.ActivePowerMinusL2")]
        [InlineData("EM300LR.Data.ActiveEnergyMinusL2")]
        [InlineData("EM300LR.Data.ReactivePowerPlusL2")]
        [InlineData("EM300LR.Data.ReactiveEnergyPlusL2")]
        [InlineData("EM300LR.Data.ReactivePowerMinusL2")]
        [InlineData("EM300LR.Data.ReactiveEnergyMinusL2")]
        [InlineData("EM300LR.Data.ApparentPowerPlusL2")]
        [InlineData("EM300LR.Data.ApparentEnergyPlusL2")]
        [InlineData("EM300LR.Data.ApparentPowerMinusL2")]
        [InlineData("EM300LR.Data.ApparentEnergyMinusL2")]
        [InlineData("EM300LR.Data.CurrentL2")]
        [InlineData("EM300LR.Data.VoltageL2")]
        [InlineData("EM300LR.Data.PowerFactorL2")]
        [InlineData("EM300LR.Data.ActivePowerPlusL3")]
        [InlineData("EM300LR.Data.ActiveEnergyPlusL3")]
        [InlineData("EM300LR.Data.ActivePowerMinusL3")]
        [InlineData("EM300LR.Data.ActiveEnergyMinusL3")]
        [InlineData("EM300LR.Data.ReactivePowerPlusL3")]
        [InlineData("EM300LR.Data.ReactiveEnergyPlusL3")]
        [InlineData("EM300LR.Data.ReactivePowerMinusL3")]
        [InlineData("EM300LR.Data.ReactiveEnergyMinusL3")]
        [InlineData("EM300LR.Data.ApparentPowerPlusL3")]
        [InlineData("EM300LR.Data.ApparentEnergyPlusL3")]
        [InlineData("EM300LR.Data.ApparentPowerMinusL3")]
        [InlineData("EM300LR.Data.ApparentEnergyMinusL3")]
        [InlineData("EM300LR.Data.CurrentL3")]
        [InlineData("EM300LR.Data.VoltageL3")]
        [InlineData("EM300LR.Data.PowerFactorL3")]
        [InlineData("EM300LR.TotalData")]
        [InlineData("EM300LR.TotalData.ActivePowerPlus")]
        [InlineData("EM300LR.TotalData.ActiveEnergyPlus")]
        [InlineData("EM300LR.TotalData.ActivePowerMinus")]
        [InlineData("EM300LR.TotalData.ActiveEnergyMinus")]
        [InlineData("EM300LR.TotalData.ReactivePowerPlus")]
        [InlineData("EM300LR.TotalData.ReactiveEnergyPlus")]
        [InlineData("EM300LR.TotalData.ReactivePowerMinus")]
        [InlineData("EM300LR.TotalData.ReactiveEnergyMinus")]
        [InlineData("EM300LR.TotalData.ApparentPowerPlus")]
        [InlineData("EM300LR.TotalData.ApparentEnergyPlus")]
        [InlineData("EM300LR.TotalData.ApparentPowerMinus")]
        [InlineData("EM300LR.TotalData.ApparentEnergyMinus")]
        [InlineData("EM300LR.TotalData.PowerFactor")]
        [InlineData("EM300LR.TotalData.SupplyFrequency")]
        [InlineData("EM300LR.Phase1Data")]
        [InlineData("EM300LR.Phase1Data.ActivePowerPlusL1")]
        [InlineData("EM300LR.Phase1Data.ActiveEnergyPlusL1")]
        [InlineData("EM300LR.Phase1Data.ActivePowerMinusL1")]
        [InlineData("EM300LR.Phase1Data.ActiveEnergyMinusL1")]
        [InlineData("EM300LR.Phase1Data.ReactivePowerPlusL1")]
        [InlineData("EM300LR.Phase1Data.ReactiveEnergyPlusL1")]
        [InlineData("EM300LR.Phase1Data.ReactivePowerMinusL1")]
        [InlineData("EM300LR.Phase1Data.ReactiveEnergyMinusL1")]
        [InlineData("EM300LR.Phase1Data.ApparentPowerPlusL1")]
        [InlineData("EM300LR.Phase1Data.ApparentEnergyPlusL1")]
        [InlineData("EM300LR.Phase1Data.ApparentPowerMinusL1")]
        [InlineData("EM300LR.Phase1Data.ApparentEnergyMinusL1")]
        [InlineData("EM300LR.Phase1Data.CurrentL1")]
        [InlineData("EM300LR.Phase1Data.VoltageL1")]
        [InlineData("EM300LR.Phase1Data.PowerFactorL1")]
        [InlineData("EM300LR.Phase2Data")]
        [InlineData("EM300LR.Phase2Data.ActivePowerPlusL2")]
        [InlineData("EM300LR.Phase2Data.ActiveEnergyPlusL2")]
        [InlineData("EM300LR.Phase2Data.ActivePowerMinusL2")]
        [InlineData("EM300LR.Phase2Data.ActiveEnergyMinusL2")]
        [InlineData("EM300LR.Phase2Data.ReactivePowerPlusL2")]
        [InlineData("EM300LR.Phase2Data.ReactiveEnergyPlusL2")]
        [InlineData("EM300LR.Phase2Data.ReactivePowerMinusL2")]
        [InlineData("EM300LR.Phase2Data.ReactiveEnergyMinusL2")]
        [InlineData("EM300LR.Phase2Data.ApparentPowerPlusL2")]
        [InlineData("EM300LR.Phase2Data.ApparentEnergyPlusL2")]
        [InlineData("EM300LR.Phase2Data.ApparentPowerMinusL2")]
        [InlineData("EM300LR.Phase2Data.ApparentEnergyMinusL2")]
        [InlineData("EM300LR.Phase2Data.CurrentL2")]
        [InlineData("EM300LR.Phase2Data.VoltageL2")]
        [InlineData("EM300LR.Phase2Data.PowerFactorL2")]
        [InlineData("EM300LR.Phase3Data")]
        [InlineData("EM300LR.Phase3Data.ActivePowerPlusL3")]
        [InlineData("EM300LR.Phase3Data.ActiveEnergyPlusL3")]
        [InlineData("EM300LR.Phase3Data.ActivePowerMinusL3")]
        [InlineData("EM300LR.Phase3Data.ActiveEnergyMinusL3")]
        [InlineData("EM300LR.Phase3Data.ReactivePowerPlusL3")]
        [InlineData("EM300LR.Phase3Data.ReactiveEnergyPlusL3")]
        [InlineData("EM300LR.Phase3Data.ReactivePowerMinusL3")]
        [InlineData("EM300LR.Phase3Data.ReactiveEnergyMinusL3")]
        [InlineData("EM300LR.Phase3Data.ApparentPowerPlusL3")]
        [InlineData("EM300LR.Phase3Data.ApparentEnergyPlusL3")]
        [InlineData("EM300LR.Phase3Data.ApparentPowerMinusL3")]
        [InlineData("EM300LR.Phase3Data.ApparentEnergyMinusL3")]
        [InlineData("EM300LR.Phase3Data.CurrentL3")]
        [InlineData("EM300LR.Phase3Data.VoltageL3")]
        [InlineData("EM300LR.Phase3Data.PowerFactorL3")]
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/em300lr/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        #endregion Test Methods
    }
}