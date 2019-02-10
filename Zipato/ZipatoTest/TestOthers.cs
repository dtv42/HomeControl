// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestOthers.cs" company="DTV-Online">
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
    public class TestOthers : IClassFixture<ZipatoFixture>
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
        public TestOthers(ZipatoFixture fixture, ITestOutputHelper outputHelper)
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
        public async Task TestOthersData()
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Others.Status.IsGood);
            Assert.NotEmpty(_zipato.Others.Cameras);
            Assert.NotEmpty(_zipato.Others.Scenes);
        }

        [Theory]
        [InlineData("eed3c4e2-62c3-4011-8756-9c294de702c5")]
        public async Task TestCameras(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Others.Status.IsGood);
            Assert.NotEmpty(_zipato.Others.Cameras);
            Assert.NotNull(_zipato.Others.Cameras.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("498163d5-f2af-4f74-a946-f555774f9c9d")]
        [InlineData("b554649d-2e88-494b-98ad-fb4ef3ac7641")]
        [InlineData("9ca8888a-5857-4a02-a6a5-c4412378c8a2")]
        [InlineData("cd7c2f03-16d5-4083-a4b7-97c060d73d7d")]
        [InlineData("bbd91ea8-a35e-438a-9775-68424c81e2da")]
        [InlineData("094c721d-4d1b-4ba1-b948-42c1963d410f")]
        [InlineData("982493e6-aefa-4eb7-8464-c4520d9c5311")]
        [InlineData("42401f4b-6218-4ba1-8a45-8cf5dee7c4a7")]
        [InlineData("debf4c8c-6759-4c20-8e1d-1b3b9966daea")]
        [InlineData("76ee8664-91f5-417d-95c4-f5ac4ee85dee")]
        [InlineData("bbd3ee62-bf94-472f-b9de-fe449f6ff9c0")]
        public async Task TestScenes(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Others.Status.IsGood);
            Assert.NotEmpty(_zipato.Others.Scenes);
            Assert.NotNull(_zipato.Others.Scenes.FirstOrDefault(d => d.Uuid == uuid));
        }

        #endregion Test Methods
    }
}
