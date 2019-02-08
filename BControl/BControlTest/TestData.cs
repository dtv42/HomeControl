namespace BControlTest
{
    #region Using Directives

    using System.Globalization;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using BControlLib;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("BControl Test Collection")]
    public class TestData : IClassFixture<BControlFixture>
    {
        #region Private Data Members

        private readonly ILogger<BControl> _logger;
        private readonly IBControl _bcontrol;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestData(BControlFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<BControl>();

            _bcontrol = fixture.BControl;
        }

        #endregion

        [Fact]
        public void TestBControlData()
        {
            Assert.True(_bcontrol.Data.IsGood);
            Assert.True(_bcontrol.IsInitialized);
            Assert.Equal(21043, _bcontrol.PnPData.ManufacturerID);
            Assert.Equal(18498, _bcontrol.PnPData.ProductID);
            Assert.Equal("Energy Manager 300", _bcontrol.PnPData.ProductName);
            Assert.Equal(1024, _bcontrol.PnPData.ProductVersion);
            Assert.Equal(516, _bcontrol.PnPData.FirmwareVersion);
            Assert.Equal("72130509", _bcontrol.PnPData.SerialNumber);
            Assert.Equal("B-control", _bcontrol.PnPData.VendorName);
        }

        [Fact]
        public async Task TestSettings()
        {
            _bcontrol.TcpSlave.Address = "10.0.1.10";
            await _bcontrol.ReadAllAsync();

            Assert.True(_bcontrol.Data.IsGood);
            Assert.True(_bcontrol.IsInitialized);
            Assert.Equal(21043, _bcontrol.PnPData.ManufacturerID);
            Assert.Equal(18498, _bcontrol.PnPData.ProductID);
            Assert.Equal("Energy Manager 300", _bcontrol.PnPData.ProductName);
            Assert.Equal(1024, _bcontrol.PnPData.ProductVersion);
            Assert.Equal(516, _bcontrol.PnPData.FirmwareVersion);
            Assert.Equal("72130511", _bcontrol.PnPData.SerialNumber);
            Assert.Equal("B-control", _bcontrol.PnPData.VendorName);
        }

        [Theory]
        // Internal immediate registers
        [InlineData("ActivePowerPositive")]
        [InlineData("ActivePowerNegative")]
        [InlineData("ReactivePowerPositive")]
        [InlineData("ReactivePowerNegative")]
        [InlineData("ApparentPowerPositive")]
        [InlineData("ApparentPowerNegative")]
        [InlineData("PowerFactor")]
        [InlineData("LineFrequency")]
        [InlineData("ActivePowerPositiveL1")]
        [InlineData("ActivePowerNegativeL1")]
        [InlineData("ReactivePowerPositiveL1")]
        [InlineData("ReactivePowerNegativeL1")]
        [InlineData("ApparentPowerPositiveL1")]
        [InlineData("ApparentPowerNegativeL1")]
        [InlineData("CurrentL1")]
        [InlineData("VoltageL1")]
        [InlineData("PowerFactorL1")]
        [InlineData("ActivePowerPositiveL2")]
        [InlineData("ActivePowerNegativeL2")]
        [InlineData("ReactivePowerPositiveL2")]
        [InlineData("ReactivePowerNegativeL2")]
        [InlineData("ApparentPowerPositiveL2")]
        [InlineData("ApparentPowerNegativeL2")]
        [InlineData("CurrentL2")]
        [InlineData("VoltageL2")]
        [InlineData("PowerFactorL2")]
        [InlineData("ActivePowerPositiveL3")]
        [InlineData("ActivePowerNegativeL3")]
        [InlineData("ReactivePowerPositiveL3")]
        [InlineData("ReactivePowerNegativeL3")]
        [InlineData("ApparentPowerPositiveL3")]
        [InlineData("ApparentPowerNegativeL3")]
        [InlineData("CurrentL3")]
        [InlineData("VoltageL3")]
        [InlineData("PowerFactorL3")]

        //Internal energy registers(meters)
        [InlineData("ActiveEnergyPositive")]
        [InlineData("ActiveEnergyNegative")]
        [InlineData("ReactiveEnergyPositive")]
        [InlineData("ReactiveEnergyNegative")]
        [InlineData("ApparentEnergyPositive")]
        [InlineData("ApparentEnergyNegative")]
        [InlineData("ActiveEnergyPositiveL1")]
        [InlineData("ActiveEnergyNegativeL1")]
        [InlineData("ReactiveEnergyPositiveL1")]
        [InlineData("ReactiveEnergyNegativeL1")]
        [InlineData("ApparentEnergyPositiveL1")]
        [InlineData("ApparentEnergyNegativeL1")]
        [InlineData("ActiveEnergyPositiveL2")]
        [InlineData("ActiveEnergyNegativeL2")]
        [InlineData("ReactiveEnergyPositiveL2")]
        [InlineData("ReactiveEnergyNegativeL2")]
        [InlineData("ApparentEnergyPositiveL2")]
        [InlineData("ApparentEnergyNegativeL2")]
        [InlineData("ActiveEnergyPositiveL3")]
        [InlineData("ActiveEnergyNegativeL3")]
        [InlineData("ReactiveEnergyPositiveL3")]
        [InlineData("ReactiveEnergyNegativeL3")]
        [InlineData("ApparentEnergyPositiveL3")]
        [InlineData("ApparentEnergyNegativeL3")]

        // PnP registers
        [InlineData("ManufacturerID")]
        [InlineData("ProductID")]
        [InlineData("ProductVersion")]
        [InlineData("FirmwareVersion")]
        [InlineData("VendorName")]
        [InlineData("ProductName")]
        [InlineData("SerialNumber")]

        // SunSpec registers
        [InlineData("C_SunSpec_ID")]
        [InlineData("C_SunSpec_DID1")]
        [InlineData("C_SunSpec_Length1")]
        [InlineData("C_Manufacturer")]
        [InlineData("C_Model")]
        [InlineData("C_Options")]
        [InlineData("C_Version")]
        [InlineData("C_SerialNumber")]
        [InlineData("C_DeviceAddress")]
        [InlineData("C_SunSpec_DID2")]
        [InlineData("C_SunSpec_Length2")]
        [InlineData("M_AC_Current")]
        [InlineData("M_AC_Current_A")]
        [InlineData("M_AC_Current_B")]
        [InlineData("M_AC_Current_C")]
        [InlineData("M_AC_Current_SF")]
        [InlineData("M_AC_Voltage_LN")]
        [InlineData("M_AC_Voltage_AN")]
        [InlineData("M_AC_Voltage_BN")]
        [InlineData("M_AC_Voltage_CN")]
        [InlineData("M_AC_Voltage_LL")]
        [InlineData("M_AC_Voltage_AB")]
        [InlineData("M_AC_Voltage_BC")]
        [InlineData("M_AC_Voltage_CA")]
        [InlineData("M_AC_Voltage_SF")]
        [InlineData("M_AC_Freq")]
        [InlineData("M_AC_Freq_SF")]
        [InlineData("M_AC_Power")]
        [InlineData("M_AC_Power_A")]
        [InlineData("M_AC_Power_B")]
        [InlineData("M_AC_Power_C")]
        [InlineData("M_AC_Power_SF")]
        [InlineData("M_AC_VA")]
        [InlineData("M_AC_VA_A")]
        [InlineData("M_AC_VA_B")]
        [InlineData("M_AC_VA_C")]
        [InlineData("M_AC_VA_SF")]
        [InlineData("M_AC_VAR")]
        [InlineData("M_AC_VAR_A")]
        [InlineData("M_AC_VAR_B")]
        [InlineData("M_AC_VAR_C")]
        [InlineData("M_AC_VAR_SF")]
        [InlineData("M_AC_PF")]
        [InlineData("M_AC_PF_A")]
        [InlineData("M_AC_PF_B")]
        [InlineData("M_AC_PF_C")]
        [InlineData("M_AC_PF_SF")]
        [InlineData("M_Exported")]
        [InlineData("M_Exported_A")]
        [InlineData("M_Exported_B")]
        [InlineData("M_Exported_C")]
        [InlineData("M_Imported")]
        [InlineData("M_Imported_A")]
        [InlineData("M_Imported_B")]
        [InlineData("M_Imported_C")]
        [InlineData("M_Energy_W_SF")]
        [InlineData("M_Exported_VA")]
        [InlineData("M_Exported_VA_A")]
        [InlineData("M_Exported_VA_B")]
        [InlineData("M_Exported_VA_C")]
        [InlineData("M_Imported_VA")]
        [InlineData("M_Imported_VA_A")]
        [InlineData("M_Imported_VA_B")]
        [InlineData("M_Imported_VA_C")]
        [InlineData("M_Energy_VA_SF")]
        [InlineData("M_Import_VARh_Q1")]
        [InlineData("M_Import_VARh_Q1A")]
        [InlineData("M_Import_VARh_Q1B")]
        [InlineData("M_Import_VARh_Q1C")]
        [InlineData("M_Import_VARh_Q2")]
        [InlineData("M_Import_VARh_Q2A")]
        [InlineData("M_Import_VARh_Q2B")]
        [InlineData("M_Import_VARh_Q2C")]
        [InlineData("M_Import_VARh_Q3")]
        [InlineData("M_Import_VARh_Q3A")]
        [InlineData("M_Import_VARh_Q3B")]
        [InlineData("M_Import_VARh_Q3C")]
        [InlineData("M_Import_VARh_Q4")]
        [InlineData("M_Import_VARh_Q4A")]
        [InlineData("M_Import_VARh_Q4B")]
        [InlineData("M_Import_VARh_Q4C")]
        [InlineData("M_Energy_VAR_SF")]
        [InlineData("M_Events")]
        [InlineData("C_SunSpec_DID3")]
        [InlineData("C_SunSpec_Length3")]
        public void TestProperty(string property)
        {
            Assert.True(BControl.IsProperty(property));
            Assert.NotNull(_bcontrol.GetPropertyValue(property));
        }
    }
}
