// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MTest
{
    #region Using Directives

    using System.Globalization;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using SYMO823MLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("SYMO823M Test Collection")]
    public class TestData : IClassFixture<SYMO823MFixture>
    {
        #region Private Data Members

        private ILogger<SYMO823M> _logger;
        private ISYMO823M _symo823m;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestData(SYMO823MFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<SYMO823M>();

            _symo823m = fixture.SYMO823M;
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("SunSpecID")]
        [InlineData("C001ID")]
        [InlineData("C001Length")]
        [InlineData("Manufacturer")]
        [InlineData("Model")]
        [InlineData("Options")]
        [InlineData("Version")]
        [InlineData("SerialNumber")]
        [InlineData("DeviceAddress")]

        [InlineData("I113ID")]
        [InlineData("I113Length")]
        [InlineData("TotalCurrentAC")]
        [InlineData("CurrentL1")]
        [InlineData("CurrentL2")]
        [InlineData("CurrentL3")]
        [InlineData("VoltageL1L2")]
        [InlineData("VoltageL2L3")]
        [InlineData("VoltageL3L1")]
        [InlineData("VoltageL1N")]
        [InlineData("VoltageL2N")]
        [InlineData("VoltageL3N")]
        [InlineData("PowerAC")]
        [InlineData("Frequency")]
        [InlineData("ApparentPower")]
        [InlineData("ReactivePower")]
        [InlineData("PowerFactor")]
        [InlineData("LifeTimeEnergy")]
        [InlineData("CurrentDC")]
        [InlineData("VoltageDC")]
        [InlineData("PowerDC")]
        [InlineData("CabinetTemperature")]
        [InlineData("HeatsinkTemperature")]
        [InlineData("TransformerTemperature")]
        [InlineData("OtherTemperature")]
        [InlineData("OperatingState")]
        [InlineData("VendorState")]
        [InlineData("Evt1")]
        [InlineData("Evt2")]
        [InlineData("EvtVnd1")]
        [InlineData("EvtVnd2")]
        [InlineData("EvtVnd3")]
        [InlineData("EvtVnd4")]

        [InlineData("IC120ID")]
        [InlineData("IC120Length")]
        [InlineData("DERType")]
        [InlineData("OutputW")]
        [InlineData("ScaleFactorOutputW")]
        [InlineData("OutputVA")]
        [InlineData("ScaleFactorOutputVA")]
        [InlineData("OutputVArQ1")]
        [InlineData("OutputVArQ2")]
        [InlineData("OutputVArQ3")]
        [InlineData("OutputVArQ4")]
        [InlineData("ScaleFactorOutputVAr")]
        [InlineData("MaxRMS")]
        [InlineData("ScaleFactorMaxRMS")]
        [InlineData("MinimumPFQ1")]
        [InlineData("MinimumPFQ2")]
        [InlineData("MinimumPFQ3")]
        [InlineData("MinimumPFQ4")]
        [InlineData("ScaleFactorMinimumPF")]
        [InlineData("EnergyRating")]
        [InlineData("ScaleFactorEnergyRating")]
        [InlineData("BatteryCapacity")]
        [InlineData("ScaleFactorBatteryCapacity")]
        [InlineData("MaxCharge")]
        [InlineData("ScaleFactorMaxCharge")]
        [InlineData("MaxDischarge")]
        [InlineData("ScaleFactorMaxDischarge")]
        [InlineData("Pad")]

        [InlineData("IC121ID")]
        [InlineData("IC121Length")]
        [InlineData("WMax")]
        [InlineData("VRef")]
        [InlineData("VRefOfs")]
        [InlineData("VMax")]
        [InlineData("VMin")]
        [InlineData("VAMax")]
        [InlineData("VARMaxQ1")]
        [InlineData("VARMaxQ2")]
        [InlineData("VARMaxQ3")]
        [InlineData("VARMaxQ4")]
        [InlineData("WGra")]
        [InlineData("VArAct")]
        [InlineData("ClcTotVA")]
        [InlineData("PFMinQ1")]
        [InlineData("PFMinQ2")]
        [InlineData("PFMinQ3")]
        [InlineData("PFMinQ4")]
        [InlineData("MaxRmpRte")]
        [InlineData("ECPNomHz")]
        [InlineData("ConnectedPhase")]
        [InlineData("ScaleFactorWMax")]
        [InlineData("ScaleFactorVRef")]
        [InlineData("ScaleFactorVRefOfs")]
        [InlineData("ScaleFactorVMinMax")]
        [InlineData("ScaleFactorVAMax")]
        [InlineData("ScaleFactorVARMax")]
        [InlineData("ScaleFactorWGra")]
        [InlineData("ScaleFactorPFMin")]
        [InlineData("ScaleFactorMaxRmpRte")]
        [InlineData("ScaleFactorECPNomHz")]

        [InlineData("IC122ID")]
        [InlineData("IC122Length")]
        [InlineData("PVConn")]
        [InlineData("StorConn")]
        [InlineData("ECPConn")]
        [InlineData("ActWh")]
        [InlineData("ActVAh")]
        [InlineData("ActVArhQ1")]
        [InlineData("ActVArhQ2")]
        [InlineData("ActVArhQ3")]
        [InlineData("ActVArhQ4")]
        [InlineData("AvailableVAr")]
        [InlineData("ScaleFactorAvailableVAr")]
        [InlineData("AvailableW")]
        [InlineData("ScaleFactorAvailableW")]
        [InlineData("StSetLimMsk")]
        [InlineData("StActCtl")]
        [InlineData("TmSrc")]
        [InlineData("Tms")]
        [InlineData("RtSt")]
        [InlineData("Riso")]
        [InlineData("ScaleFactorRiso")]

        [InlineData("IC123ID")]
        [InlineData("IC123Length")]
        [InlineData("ConnWinTms")]
        [InlineData("ConnRvrtTms")]
        [InlineData("Conn")]
        [InlineData("WMaxLimPct")]
        [InlineData("WMaxLimPctWinTms")]
        [InlineData("WMaxLimPctRvrtTms")]
        [InlineData("WMaxLimPctRmpTms")]
        [InlineData("WMaxLimEna")]
        [InlineData("OutPFSet")]
        [InlineData("OutPFSetWinTms")]
        [InlineData("OutPFSetRvrtTms")]
        [InlineData("OutPFSetRmpTms")]
        [InlineData("OutPFSetEna")]
        [InlineData("VArWMaxPct")]
        [InlineData("VArMaxPct")]
        [InlineData("VArAvalPct")]
        [InlineData("VArPctWinTms")]
        [InlineData("VArPctRvrtTms")]
        [InlineData("VArPctRmpTms")]
        [InlineData("VArPctMod")]
        [InlineData("VArPctEna")]
        [InlineData("ScaleFactorWMaxLimPct")]
        [InlineData("ScaleFactorOutPFSet")]
        [InlineData("ScaleFactorVArPct")]

        [InlineData("I160ID")]
        [InlineData("I160Length")]
        [InlineData("ScaleFactorCurrent")]
        [InlineData("ScaleFactorVoltage")]
        [InlineData("ScaleFactorPower")]
        [InlineData("ScaleFactorEnergy")]
        [InlineData("GlobalEvents")]
        [InlineData("NumberOfModules")]
        [InlineData("TimestampPeriod")]
        [InlineData("InputID1")]
        [InlineData("InputIDString1")]
        [InlineData("CurrentDC1")]
        [InlineData("VoltageDC1")]
        [InlineData("PowerDC1")]
        [InlineData("LifetimeEnergy1")]
        [InlineData("Timestamp1")]
        [InlineData("Temperature1")]
        [InlineData("OperatingState1")]
        [InlineData("ModuleEvents1")]
        [InlineData("InputID2")]
        [InlineData("InputIDString2")]
        [InlineData("CurrentDC2")]
        [InlineData("VoltageDC2")]
        [InlineData("PowerDC2")]
        [InlineData("LifetimeEnergy2")]
        [InlineData("Timestamp2")]
        [InlineData("Temperature2")]
        [InlineData("OperatingState2")]
        [InlineData("ModuleEvents2")]

        [InlineData("DeleteData")]
        [InlineData("StoreData")]
        [InlineData("ActiveStateCode")]
        [InlineData("ResetAllEventFlags")]
        [InlineData("ModelType")]
        [InlineData("SitePower")]
        [InlineData("SiteEnergyDay")]
        [InlineData("SiteEnergyYear")]
        [InlineData("SiteEnergyTotal")]
        public void TestProperty(string property)
        {
            Assert.True(SYMO823M.IsProperty(property));
            Assert.NotNull(_symo823m.GetPropertyValue(property));
        }

        #endregion
    }
}
