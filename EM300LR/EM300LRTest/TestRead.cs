// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRead.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRTest
{
    #region Using Directives

    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;
    using EM300LRLib;

    #endregion Using Directives

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("EM300LR Test Collection")]
    public class TestRead : IClassFixture<EM300LRFixture>
    {
        #region Private Data Members

        private readonly ILogger<EM300LR> _logger;
        private readonly IEM300LR _em300lr;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestRead(EM300LRFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<EM300LR>();

            _em300lr = fixture.EM300LR;
        }

        #endregion Constructors

        #region Test Methods

        [Fact]
        public async Task TestEM300LRRead()
        {
            await _em300lr.ReadAllAsync();
            Assert.True(_em300lr.Data.IsGood);
            Assert.True(_em300lr.TotalData.IsGood);
            Assert.True(_em300lr.Phase1Data.IsGood);
            Assert.True(_em300lr.Phase2Data.IsGood);
            Assert.True(_em300lr.Phase3Data.IsGood);
            _logger?.LogInformation($"EM300LR: {JsonConvert.SerializeObject(_em300lr, Formatting.Indented)}");
        }

        [Theory]
        [InlineData("ActivePowerPlus")]
        [InlineData("ActiveEnergyPlus")]
        [InlineData("ActivePowerMinus")]
        [InlineData("ActiveEnergyMinus")]
        [InlineData("ReactivePowerPlus")]
        [InlineData("ReactiveEnergyPlus")]
        [InlineData("ReactivePowerMinus")]
        [InlineData("ReactiveEnergyMinus")]
        [InlineData("ApparentPowerPlus")]
        [InlineData("ApparentEnergyPlus")]
        [InlineData("ApparentPowerMinus")]
        [InlineData("ApparentEnergyMinus")]
        [InlineData("PowerFactor")]
        [InlineData("SupplyFrequency")]
        [InlineData("ActivePowerPlusL1")]
        [InlineData("ActiveEnergyPlusL1")]
        [InlineData("ActivePowerMinusL1")]
        [InlineData("ActiveEnergyMinusL1")]
        [InlineData("ReactivePowerPlusL1")]
        [InlineData("ReactiveEnergyPlusL1")]
        [InlineData("ReactivePowerMinusL1")]
        [InlineData("ReactiveEnergyMinusL1")]
        [InlineData("ApparentPowerPlusL1")]
        [InlineData("ApparentEnergyPlusL1")]
        [InlineData("ApparentPowerMinusL1")]
        [InlineData("ApparentEnergyMinusL1")]
        [InlineData("CurrentL1")]
        [InlineData("VoltageL1")]
        [InlineData("PowerFactorL1")]
        [InlineData("ActivePowerPlusL2")]
        [InlineData("ActiveEnergyPlusL2")]
        [InlineData("ActivePowerMinusL2")]
        [InlineData("ActiveEnergyMinusL2")]
        [InlineData("ReactivePowerPlusL2")]
        [InlineData("ReactiveEnergyPlusL2")]
        [InlineData("ReactivePowerMinusL2")]
        [InlineData("ReactiveEnergyMinusL2")]
        [InlineData("ApparentPowerPlusL2")]
        [InlineData("ApparentEnergyPlusL2")]
        [InlineData("ApparentPowerMinusL2")]
        [InlineData("ApparentEnergyMinusL2")]
        [InlineData("CurrentL2")]
        [InlineData("VoltageL2")]
        [InlineData("PowerFactorL2")]
        [InlineData("ActivePowerPlusL3")]
        [InlineData("ActiveEnergyPlusL3")]
        [InlineData("ActivePowerMinusL3")]
        [InlineData("ActiveEnergyMinusL3")]
        [InlineData("ReactivePowerPlusL3")]
        [InlineData("ReactiveEnergyPlusL3")]
        [InlineData("ReactivePowerMinusL3")]
        [InlineData("ReactiveEnergyMinusL3")]
        [InlineData("ApparentPowerPlusL3")]
        [InlineData("ApparentEnergyPlusL3")]
        [InlineData("ApparentPowerMinusL3")]
        [InlineData("ApparentEnergyMinusL3")]
        [InlineData("CurrentL3")]
        [InlineData("VoltageL3")]
        [InlineData("PowerFactorL3")]
        [InlineData("Data")]
        [InlineData("Data.ActivePowerPlus")]
        [InlineData("Data.ActiveEnergyPlus")]
        [InlineData("Data.ActivePowerMinus")]
        [InlineData("Data.ActiveEnergyMinus")]
        [InlineData("Data.ReactivePowerPlus")]
        [InlineData("Data.ReactiveEnergyPlus")]
        [InlineData("Data.ReactivePowerMinus")]
        [InlineData("Data.ReactiveEnergyMinus")]
        [InlineData("Data.ApparentPowerPlus")]
        [InlineData("Data.ApparentEnergyPlus")]
        [InlineData("Data.ApparentPowerMinus")]
        [InlineData("Data.ApparentEnergyMinus")]
        [InlineData("Data.PowerFactor")]
        [InlineData("Data.SupplyFrequency")]
        [InlineData("Data.ActivePowerPlusL1")]
        [InlineData("Data.ActiveEnergyPlusL1")]
        [InlineData("Data.ActivePowerMinusL1")]
        [InlineData("Data.ActiveEnergyMinusL1")]
        [InlineData("Data.ReactivePowerPlusL1")]
        [InlineData("Data.ReactiveEnergyPlusL1")]
        [InlineData("Data.ReactivePowerMinusL1")]
        [InlineData("Data.ReactiveEnergyMinusL1")]
        [InlineData("Data.ApparentPowerPlusL1")]
        [InlineData("Data.ApparentEnergyPlusL1")]
        [InlineData("Data.ApparentPowerMinusL1")]
        [InlineData("Data.ApparentEnergyMinusL1")]
        [InlineData("Data.CurrentL1")]
        [InlineData("Data.VoltageL1")]
        [InlineData("Data.PowerFactorL1")]
        [InlineData("Data.ActivePowerPlusL2")]
        [InlineData("Data.ActiveEnergyPlusL2")]
        [InlineData("Data.ActivePowerMinusL2")]
        [InlineData("Data.ActiveEnergyMinusL2")]
        [InlineData("Data.ReactivePowerPlusL2")]
        [InlineData("Data.ReactiveEnergyPlusL2")]
        [InlineData("Data.ReactivePowerMinusL2")]
        [InlineData("Data.ReactiveEnergyMinusL2")]
        [InlineData("Data.ApparentPowerPlusL2")]
        [InlineData("Data.ApparentEnergyPlusL2")]
        [InlineData("Data.ApparentPowerMinusL2")]
        [InlineData("Data.ApparentEnergyMinusL2")]
        [InlineData("Data.CurrentL2")]
        [InlineData("Data.VoltageL2")]
        [InlineData("Data.PowerFactorL2")]
        [InlineData("Data.ActivePowerPlusL3")]
        [InlineData("Data.ActiveEnergyPlusL3")]
        [InlineData("Data.ActivePowerMinusL3")]
        [InlineData("Data.ActiveEnergyMinusL3")]
        [InlineData("Data.ReactivePowerPlusL3")]
        [InlineData("Data.ReactiveEnergyPlusL3")]
        [InlineData("Data.ReactivePowerMinusL3")]
        [InlineData("Data.ReactiveEnergyMinusL3")]
        [InlineData("Data.ApparentPowerPlusL3")]
        [InlineData("Data.ApparentEnergyPlusL3")]
        [InlineData("Data.ApparentPowerMinusL3")]
        [InlineData("Data.ApparentEnergyMinusL3")]
        [InlineData("Data.CurrentL3")]
        [InlineData("Data.VoltageL3")]
        [InlineData("Data.PowerFactorL3")]
        public void TestEM300LRReadProperty(string property)
        {
            Assert.True(EM300LR.IsProperty(property));
            Assert.NotNull(_em300lr.GetPropertyValue(property));
        }

        #endregion Test Methods
    }
}
