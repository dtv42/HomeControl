// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDataRead.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using DataValueLib;
    using ZipatoLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestDataRead : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestDataRead(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        #endregion

        [Fact]
        public async Task TestDataReadAlarm()
        {
            var (result, status) = await _zipato.DataReadAlarmAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.Equal("alarm", result.Link);
        }

        [Fact]
        public async Task TestDataReadAnnouncements()
        {
            var (result, status) = await _zipato.DataReadAnnouncementsAsync();
            Assert.Equal(DataValue.Good, status);
            //Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadAttributes()
        {
            var (result, status) = await _zipato.DataReadAttributesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadValues()
        {
            var (result, status) = await _zipato.DataReadValuesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
            _logger?.LogInformation($"Values: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
        }

        [Fact]
        public async Task TestDataReadBindings()
        {
            var (result, status) = await _zipato.DataReadBindingsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadBrands()
        {
            var (result, status) = await _zipato.DataReadBrandsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadCameras()
        {
            var (result, status) = await _zipato.DataReadCamerasAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadClusters()
        {
            var (result, status) = await _zipato.DataReadClustersAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.Empty(result); // No clusters!
        }

        [Fact]
        public async Task TestDataReadClusterEndpoints()
        {
            var (result, status) = await _zipato.DataReadClusterEndpointsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadContacts()
        {
            var (result, status) = await _zipato.DataReadContactsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadDevices()
        {
            var (result, status) = await _zipato.DataReadDevicesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadEndpoints()
        {
            var (result, status) = await _zipato.DataReadEndpointsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadGroups()
        {
            var (result, status) = await _zipato.DataReadGroupsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadNetworks()
        {
            var (result, status) = await _zipato.DataReadNetworksAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadNetworkTrees()
        {
            var (result, status) = await _zipato.DataReadNetworkTreesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadRooms()
        {
            var (result, status) = await _zipato.DataReadRoomsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadRules()
        {
            var (result, status) = await _zipato.DataReadBrandsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadScenes()
        {
            var (result, status) = await _zipato.DataReadScenesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadSchedules()
        {
            var (result, status) = await _zipato.DataReadSchedulesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestThermostats()
        {
            var (result, status) = await _zipato.DataReadThermostatsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadVirtualEndpoints()
        {
            var (result, status) = await _zipato.DataReadVirtualEndpointsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestDataReadBox()
        {
            var (result, status) = await _zipato.DataReadBoxAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.Equal("ZT7E104099D0D75BCC", result.Serial);
        }

        [Theory]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c")]
        public async Task TestDataReadAttributeAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadAttributeAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("a45e74e2-8a61-4d39-9fa0-786d915039cc")]
        public async Task TestDataReadBindingAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadBindingAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("Zwave")]
        public async Task TestDataReadBrandAsync(string name)
        {
            var (result, status) = await _zipato.DataReadBrandAsync(name);
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("eed3c4e2-62c3-4011-8756-9c294de702c5")]
        public async Task TestDataReadCameraAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadCameraAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData(0)]
        public async Task TestDataReadClusterAsync(int id)
        {
            var (result, status) = await _zipato.DataReadClusterAsync(id);
            Assert.Equal(DataStatus.BadNotSupported, status.Code); // No cluster.
        }

        [Theory]
        [InlineData("d029ff97-8817-49c1-9c3f-c841919f89db")]
        public async Task TestDataReadClusterEndpointAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadClusterEndpointAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData(27896)]
        [InlineData(27932)]
        public async Task TestDataReadContactAsync(int id)
        {
            var (result, status) = await _zipato.DataReadContactAsync(id);
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestDataReadDeviceAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadDeviceAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("7596cb26-3cdc-4f8b-b665-eaf5cc26ff66")]
        public async Task TestDataReadEndpointAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadEndpointAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("a78751d3-7315-4b43-8887-a42ffd644c76")]
        public async Task TestDataReadGroupAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadGroupAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("946b1557-9fb1-48f1-b86c-94f3a4cbeb6b")]
        //[InlineData("b1e74a45-eb2e-4ba1-831a-03292192fcf4")]
        //[InlineData("3582e8be-73ec-4f06-bf5f-88337a1eca6a")]
        //[InlineData("5d46a38a-9cca-4ae1-be10-640f89d6484c")]
        public async Task TestDataReadNetworkAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadNetworkAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("946b1557-9fb1-48f1-b86c-94f3a4cbeb6b")]
        [InlineData("b1e74a45-eb2e-4ba1-831a-03292192fcf4")]
        [InlineData("3582e8be-73ec-4f06-bf5f-88337a1eca6a")]
        [InlineData("5d46a38a-9cca-4ae1-be10-640f89d6484c")]
        public async Task TestDataReadNetworkTreeAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadNetworkTreeAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData(194646)]
        public async Task TestDataReadRuleAsync(int id)
        {
            var (result, status) = await _zipato.DataReadRuleAsync(id);
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("498163d5-f2af-4f74-a946-f555774f9c9d")]
        public async Task TestDataReadSceneAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadSceneAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("b6a99ab4-89ce-4c85-b01b-6cabd69742a2")]
        public async Task TestDataReadScheduleAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadScheduleAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814")]
        public async Task TestDataReadThermostatAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadThermostatAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        [Theory]
        [InlineData("29b5177d-a811-44ec-9b30-54d2736394c3")]
        public async Task TestDataReadVirtualEndpointAsync(string uuid)
        {
            var (result, status) = await _zipato.DataReadVirtualEndpointAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }
    }
}
