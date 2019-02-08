// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestSensors.cs" company="DTV-Online">
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
    public class TestSensors : IClassFixture<ZipatoFixture>
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
        public TestSensors(ZipatoFixture fixture, ITestOutputHelper outputHelper)
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
        public async Task TestSensorsData()
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Sensors.Status.IsGood);
            Assert.NotEmpty(_zipato.Sensors.VirtualMeters);
            Assert.NotEmpty(_zipato.Sensors.ConsumptionMeters);
            Assert.NotEmpty(_zipato.Sensors.TemperatureSensors);
            Assert.NotEmpty(_zipato.Sensors.HumiditySensors);
            Assert.NotEmpty(_zipato.Sensors.LuminanceSensors);
        }

        [Theory]
        [InlineData("d6573e27-0692-4b71-afb5-a7a88c4ddaeb")]
        [InlineData("e7b664f5-0027-4fa2-881b-d7b8cf5498b5")]
        [InlineData("af7c0c1a-537b-4aca-9693-2813ff85d845")]
        [InlineData("49867b36-2030-4d50-aff6-425ae6dac604")]
        [InlineData("8bba3e09-68a9-4a36-a582-aebd4fe67965")]
        [InlineData("fa7e088d-2b9d-4931-a338-7bc84b8d1473")]
        [InlineData("4e4e19ba-e01d-475b-9661-ef989c8f5d9c")]
        public async Task TestVirtualMeters(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Sensors.Status.IsGood);
            Assert.NotEmpty(_zipato.Sensors.VirtualMeters);
            Assert.NotNull(_zipato.Sensors.VirtualMeters.FirstOrDefault(d => d.Uuid == uuid));
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
        [InlineData("3c4881b4-a242-41b6-a614-639a6ed52145")]
        [InlineData("480cc173-0a19-4a9a-a677-2aed55ae99cd")]
        [InlineData("75536a93-6d24-4ece-a227-454b5f51e9d4")]
        [InlineData("a64a8821-2fa9-4934-b4c9-e0b46e819736")]
        public async Task TestConsumptionMeters(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Sensors.Status.IsGood);
            Assert.NotEmpty(_zipato.Sensors.ConsumptionMeters);
            Assert.NotNull(_zipato.Sensors.ConsumptionMeters.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("6b958e64-1618-400d-ad76-24b8901e3725")]
        [InlineData("2c997619-de39-458d-b891-1dff30d2837c")]
        [InlineData("19f57b0b-c140-4e9c-b2b9-198e7f0d9733")]
        [InlineData("c847e2ea-0009-41f9-a6ed-7367bb91b4d0")]
        [InlineData("7a4f2e22-53a5-4ae1-9246-b96bcf92ebc6")]
        [InlineData("8a8bf7c1-4e8b-46d2-9b30-a3a03d8ef1b5")]
        [InlineData("20808472-42e9-4b7c-bb19-8dab711d3608")]
        [InlineData("473e3cfb-5e8c-4df9-97df-208676d3c553")]
        [InlineData("3924712b-3efe-4533-8bfc-4fc7fe025079")]
        public async Task TestTemperatureSensors(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Sensors.Status.IsGood);
            Assert.NotEmpty(_zipato.Sensors.TemperatureSensors);
            Assert.NotNull(_zipato.Sensors.TemperatureSensors.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("07b386c2-b66e-4e47-96d1-7d2722efccbf")]
        public async Task TestHumiditySensors(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Sensors.Status.IsGood);
            Assert.NotEmpty(_zipato.Sensors.HumiditySensors);
            Assert.NotNull(_zipato.Sensors.HumiditySensors.FirstOrDefault(d => d.Uuid == uuid));
        }

        [Theory]
        [InlineData("00ad3342-9bfc-44a2-90cc-b5c02cc3872e")]
        [InlineData("2c997619-de39-458d-b891-1dff30d2837c")]
        public async Task TestLuminanceSensors(Guid uuid)
        {
            await _zipato.ReadAllDataAsync();
            Assert.True(_zipato.Sensors.Status.IsGood);
            Assert.NotEmpty(_zipato.Sensors.LuminanceSensors);
            Assert.NotNull(_zipato.Sensors.LuminanceSensors.FirstOrDefault(d => d.Uuid == uuid));
        }

        #endregion Test Methods
    }
}
