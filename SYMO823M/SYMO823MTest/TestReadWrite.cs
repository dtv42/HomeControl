// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestReadWrite.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MTest
{
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using SYMO823MLib;
    using SYMO823MLib.Models;
    using SunSpecLib;

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("SYMO823M Test Collection")]
    public class TestReadWrite : IClassFixture<SYMO823MFixture>
    {
        private readonly ILogger<SYMO823M> _logger;
        private readonly ISYMO823M _symo823m;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWrite"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestReadWrite(SYMO823MFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<SYMO823M>();

            _symo823m = fixture.SYMO823M;
        }

        [Fact]
        public async Task TestSYMO823MReadWrite()
        {
            await _symo823m.WriteAllAsync();
            Assert.True(_symo823m.Data.IsGood);
            await _symo823m.ReadAllAsync();
            Assert.True(_symo823m.Data.IsGood);
        }

        [Theory]
        [InlineData("VRefOfs", 0)]
        [InlineData("OutPFSet", 0)]
        [InlineData("VArMaxPct", 0)]
        public async Task TestSYMO823MReadWriteSunSpecInt16(string property, short data)
        {
            var status = await _symo823m.WriteDataAsync(property, data.ToString());
            Assert.True(status.IsGood);
            _symo823m.Data = new SYMO823MData();
            status = await _symo823m.ReadDataAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal(data, (short)(int16)_symo823m.Data.GetPropertyValue(property));
        }

        [Theory]
        [InlineData("DeleteData", 0)]
        [InlineData("StoreData", 0)]
        [InlineData("ResetAllEventFlags", 0)]
        [InlineData("ModelType", 0)]
        public async Task TestSYMO823MReadWriteUShort(string property, ushort data)
        {
            var status = await _symo823m.WriteDataAsync(property, data.ToString());
            Assert.True(status.IsGood);
            _symo823m.Data = new SYMO823MData();
            status = await _symo823m.ReadDataAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal(data, (ushort)_symo823m.Data.GetPropertyValue(property));
        }

        [Theory]
        [InlineData("VRef", 0)]

        [InlineData("ConnWinTms", 0)]
        [InlineData("ConnRvrtTms", 0)]
        [InlineData("Conn", 0)]
        [InlineData("WMaxLimPct", 0)]
        [InlineData("WMaxLimPctWinTms", 0)]
        [InlineData("WMaxLimPctRvrtTms", 0)]
        [InlineData("WMaxLimEna", 0)]
        [InlineData("OutPFSetWinTms", 0)]
        [InlineData("OutPFSetRvrtTms", 0)]
        [InlineData("OutPFSetEna", 0)]
        [InlineData("VArPctWinTms", 0)]
        [InlineData("VArPctRmpTms", 0)]
        [InlineData("VArPctEna", 0)]
        public async Task TestSYMO823MReadWriteSunSpecUInt16(string property, ushort data)
        {
            var status = await _symo823m.WriteDataAsync(property, data.ToString());
            Assert.True(status.IsGood);
            _symo823m.Data = new SYMO823MData();
            status = await _symo823m.ReadDataAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal(data, (ushort)(uint16)_symo823m.Data.GetPropertyValue(property));
        }
    }
}
