// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDevices.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoTest
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using DataValueLib;

    using ZipatoLib.Models;
    using ZipatoLib.Models.Dtos;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Flags;
    using ZipatoLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestDevices : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestDevices(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestDevicesData()
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Devices.Status.IsGood);
            Assert.NotEmpty(_zipato.Devices.Switches);
            Assert.NotEmpty(_zipato.Devices.OnOffSwitches);
            Assert.NotEmpty(_zipato.Devices.Dimmers);
            Assert.NotEmpty(_zipato.Devices.Wallplugs);
            Assert.NotEmpty(_zipato.Devices.RGBControls);
        }

        [Theory]
        [InlineData("a51aa1e1-fcd2-40d1-b5d8-3ecb8a29a62d")]
        [InlineData("40b471ad-3a53-4a1c-a61e-adbfa90ef39d")]
        [InlineData("09ee5034-b287-4474-81d3-1e60a7ae964c")]
        [InlineData("f504abdb-c48c-4219-ade6-1e6422b15858")]
        [InlineData("fe9809a1-b6fd-462f-96bb-ec5bfb456d00")]
        public async Task TestSwitches(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Devices.Status.IsGood);
            Assert.NotEmpty(_zipato.Devices.Switches);
            Assert.NotNull(_zipato.Devices.Switches.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("6410df3e-fcb6-427d-9517-75a9239def60")]
        [InlineData("3c4881b4-a242-41b6-a614-639a6ed52145")]
        [InlineData("f77fce13-0339-40bd-a553-338e0740efda")]
        [InlineData("06f45224-c7be-4673-bfc7-1c9754d90a2e")]
        [InlineData("7b34607e-e7c0-4856-8404-f0f77eff43ca")]
        [InlineData("5f640952-f3ab-457c-88a2-a71ae6bcd9c0")]
        [InlineData("baf3b194-afc9-44da-8e78-0e5f924a0f67")]
        [InlineData("d964b98a-8921-4895-b22e-ce7d0d81a91c")]
        [InlineData("47f9b7e7-0f11-4ac1-a690-adc2eca711fc")]
        [InlineData("29b5177d-a811-44ec-9b30-54d2736394c3")]
        [InlineData("5b376290-75a8-494a-b1f9-a5073376cfe8")]
        [InlineData("1938c671-ad9a-4987-bd39-9dd8e4bd11a7")]
        [InlineData("a202d439-7bb1-405a-931c-84bfb9b9853a")]
        [InlineData("df407634-311a-4e2d-8c33-bd56df7b5b51")]
        [InlineData("e5de95dc-4868-4026-b6c7-433ff0eb5781")]
        [InlineData("45685519-b061-4730-acb2-85fe7870fde5")]
        [InlineData("09a9ecc8-07ff-420a-851a-b04b283444cd")]
        public async Task TestOnOffSwitches(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Devices.Status.IsGood);
            Assert.NotEmpty(_zipato.Devices.OnOffSwitches);
            Assert.NotNull(_zipato.Devices.OnOffSwitches.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("73c92279-d7ee-4da3-a42c-de9ba4656746")]
        public async Task TestDimmers(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Devices.Status.IsGood);
            Assert.NotEmpty(_zipato.Devices.Dimmers);
            Assert.NotNull(_zipato.Devices.Dimmers.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("e9e1422c-11b7-4f7e-bfc4-438977fc1e4c")]
        [InlineData("673c052e-b2f1-4b65-bc6d-ce1225b4ca4b")]
        [InlineData("e5bf7dda-e7a7-4461-bb33-a084627ea3a8")]
        [InlineData("e78d765b-8b7d-44db-9270-01f23d21020a")]
        [InlineData("5a94efa7-a0cc-48ca-94c3-8d88bb0fe3b6")]
        [InlineData("90a2d9f5-5c68-496f-8ab6-ef0e6db25a75")]
        [InlineData("830059e3-dade-42ec-a46e-ce39d6fb6086")]
        [InlineData("60d93107-0e35-4d46-b800-f530e4f57ac8")]
        public async Task TestWallPlugs(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Devices.Status.IsGood);
            Assert.NotEmpty(_zipato.Devices.Wallplugs);
            Assert.NotNull(_zipato.Devices.Wallplugs.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("480cc173-0a19-4a9a-a677-2aed55ae99cd")]
        [InlineData("75536a93-6d24-4ece-a227-454b5f51e9d4")]
        [InlineData("a64a8821-2fa9-4934-b4c9-e0b46e819736")]
        [InlineData("0894d6a7-18f7-4943-afcd-ceca72dd16c0")]
        [InlineData("3a6b0649-b03c-4c9a-9c55-a531cbfbbc04")]
        public async Task TestRGBControls(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Devices.Status.IsGood);
            Assert.NotEmpty(_zipato.Devices.RGBControls);
            Assert.NotNull(_zipato.Devices.RGBControls.FirstOrDefault(d => d.Uuid == uuid));
        }

        #endregion Test Methods
    }
}
