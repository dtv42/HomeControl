// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MTest
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
    using SYMO823MLib;
    using SYMO823MLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("SYMO823M Test Collection")]
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
            _logger = loggerFactory.CreateLogger<SYMO823M>();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8010");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestGetAllData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SYMO823MData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetCommonModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/common");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<CommonModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetInverterModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/inverter");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<InverterModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetNameplateModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/nameplate");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NameplateModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetSettingsModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/settings");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SettingsModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetExtendedModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/extended");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ExtendedModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetControlModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/control");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ControlModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetMultipleModelData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/multiple");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MultipleModelData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetFroniusRegisterData()
        {
            // Act
            var response = await _client.GetAsync("api/SYMO823M/fronius");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<FroniusRegisterData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

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
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/SYMO823M/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        #endregion
    }
}
