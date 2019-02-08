// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWrite.cs" company="DTV-Online">
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

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("SYMO823M Test Collection")]
    public class TestWrite : IClassFixture<SYMO823MFixture>
    {
        private readonly ILogger<SYMO823M> _logger;
        private readonly ISYMO823M _symo823m;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWrite"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWrite(SYMO823MFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<SYMO823M>();

            _symo823m = fixture.SYMO823M;
        }

        [Fact]
        public async Task TestSYMO823MWrite()
        {
            await _symo823m.WriteAllAsync();
            Assert.True(_symo823m.Data.IsGood);
        }

        [Theory]
        [InlineData("VRef", "0")]
        [InlineData("VRefOfs", "0")]

        [InlineData("ConnWinTms", "0")]
        [InlineData("ConnRvrtTms", "0")]
        [InlineData("Conn", "0")]
        [InlineData("WMaxLimPct", "0")]
        [InlineData("WMaxLimPctWinTms", "0")]
        [InlineData("WMaxLimPctRvrtTms", "0")]
        [InlineData("WMaxLimEna", "0")]
        [InlineData("OutPFSet", "0")]
        [InlineData("OutPFSetWinTms", "0")]
        [InlineData("OutPFSetRvrtTms", "0")]
        [InlineData("OutPFSetEna", "0")]
        [InlineData("VArMaxPct", "0")]
        [InlineData("VArPctWinTms", "0")]
        [InlineData("VArPctRmpTms", "0")]
        [InlineData("VArPctEna", "0")]

        [InlineData("DeleteData", "0")]
        [InlineData("StoreData", "0")]
        [InlineData("ResetAllEventFlags", "0")]
        [InlineData("ModelType", "0")]
        public async Task TestSYMO823MWriteProperty(string property, string data)
        {
            Assert.True(SYMO823MData.IsProperty(property));
            Assert.True(SYMO823MData.IsWritable(property));
            var status = await _symo823m.WriteDataAsync(property, data);
            Assert.True(status.IsGood);
        }
    }
}
