// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRead.cs" company="DTV-Online">
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

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models.Flags;
    using ZipatoLib.Models.Enums;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestRead : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRead"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestRead(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        #endregion

        // Testing GET /alarm/full - get full alarm dump.
        [Fact]
        public async Task TestReadAlarmFull()
        {
            var (result, status) = await _zipato.ReadAlarmFullAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.Equal("alarm", result.Link);
        }

        // Testing GET /alarm/config - get alarm configuration.
        [Fact]
        public async Task TestReadAlarmConfig()
        {
            var (result, status) = await _zipato.ReadAlarmConfigAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /alarm/monitors - list monitors.
        [Fact]
        public async Task TestReadAlarmMonitors()
        {
            var (result, status) = await _zipato.ReadAlarmMonitorsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.Empty(result);
        }

        // Testing GET /alarm/monitors/{monitor} - get monitor.
        //[Theory]
        //[InlineData("")]
        //public async Task TestReadAlarmMonitor(string uuid)
        //{
        //    var (result, status) = await _zipato.ReadAlarmMonitorAsync(new Guid(uuid));
        //    Assert.Equal(DataValue.Good, status);
        //    Assert.NotNull(result);
        //}

        // Testing GET /alarm/monitors/{monitor}/config - get monitor configuration.
        //[Theory]
        //[InlineData("")]
        //public async Task TestReadAlarmMonitorConfig(string uuid)
        //{
        //    var (result, status) = await _zipato.ReadAlarmMonitorConfigAsync(new Guid(uuid));
        //    Assert.Equal(DataValue.Good, status);
        //    Assert.NotNull(result);
        //}

        // Testing GET /alarm/partitions - list partitions.
        [Fact]
        public async Task TestReadAlarmPartitions()
        {
            var (result, status) = await _zipato.ReadAlarmPartitionsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /alarm/partitions/{partition} - get partition information.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29")]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410")]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6")]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c")]
        public async Task TestReadAlarmPartition(string uuid)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /alarm/partitions/{partition}/attributes - get all attributes of the partition.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29", AlarmPartitionAttributeFlags.ALL)]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410", AlarmPartitionAttributeFlags.ALL)]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6", AlarmPartitionAttributeFlags.ALL)]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c", AlarmPartitionAttributeFlags.ALL)]
        public async Task TestReadAlarmPartitionAttributes(string uuid, AlarmPartitionAttributeFlags flag)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionAttributesAsync(new Guid(uuid), flag);
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /alarm/partitions/{partition}/config - get partition configuration.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29")]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410")]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6")]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c")]
        public async Task TestReadAlarmPartitionConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /alarm/partitions/{partition}/events - list event log for partition
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29", AlarmPartitionEventFlags.NONE)]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410", AlarmPartitionEventFlags.NONE)]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6", AlarmPartitionEventFlags.NONE)]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c", AlarmPartitionEventFlags.NONE)]
        public async Task TestReadAlarmPartitionEvents(string uuid, AlarmPartitionEventFlags flag)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionEventsAsync(new Guid(uuid), flag);
            Assert.Equal(DataValue.Good, status);
            //Assert.NotEmpty(result);
        }

        // Testing GET /alarm/partitions/{partition}/eventsByTime - list event log for partition.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29")]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410")]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6")]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c")]
        public async Task TestReadAlarmPartitionEventsByTime(string uuid)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionEventsByTimeAsync(new Guid(uuid), DateTime.Now.Subtract(TimeSpan.FromDays(1)));
            Assert.Equal(DataValue.Good, status);
            //Assert.NotEmpty(result);
        }

        // Testing GET /alarm/partitions/{partition}/zones - list zones in partition.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29")]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410")]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6")]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c")]
        public async Task TestReadAlarmPartitionZones(string uuid)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionZonesAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /alarm/partitions/{partition}/zones/statuses - get statuses of all zones.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29")]
        [InlineData("0af081a1-e636-4d76-88c1-bcc9c6462410")]
        [InlineData("5bb93be8-0a85-4a3b-99fc-792bb30810d6")]
        [InlineData("5991c239-fcef-4165-956a-46676b62cf9c")]
        public async Task TestReadAlarmPartitionZoneStates(string uuid)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionZoneStatesAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /alarm/partitions/{partition}/zones/{zone} - get zone information.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29", "7a18e436-fb01-441d-9fa0-372a041cf61a", AlarmPartitionZoneFlags.ALL)]
        public async Task TestReadAlarmPartitionZone(string uuid, string zone, AlarmPartitionZoneFlags flag)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionZoneAsync(new Guid(uuid), zone, flag);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /alarm/partitions/{partition}/zones/{zone}/config - get zone config.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29", "7a18e436-fb01-441d-9fa0-372a041cf61a")]
        public async Task TestReadAlarmPartitionZoneConfig(string uuid, string zone)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionZoneConfigAsync(new Guid(uuid), zone);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /alarm/partitions/{partition}/zones/{zone}/status - get zone status.
        [Theory]
        [InlineData("b075179c-29a1-429a-aa6a-5f3a2dc21a29", "7a18e436-fb01-441d-9fa0-372a041cf61a")]
        public async Task TestReadAlarmPartitionZoneStatus(string uuid, string zone)
        {
            var (result, status) = await _zipato.ReadAlarmPartitionZoneStatusAsync(new Guid(uuid), zone);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /announcements Get - announcements for currently loged in user and box.
        [Fact]
        public async Task TestReadAnnouncements()
        {
            var (result, status) = await _zipato.ReadAnnouncementsAsync();
            Assert.Equal(DataValue.Good, status);
            //Assert.NotEmpty(result);
        }

        // Testing GET /attributes - get all attributes.
        [Fact]
        public async Task TestReadAttributes()
        {
            var (result, status) = await _zipato.ReadAttributesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /attributes/values - get last value of all attributes.
        [Fact]
        public async Task TestReadAttributeValues()
        {
            var (result, status) = await _zipato.ReadAttributeValuesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /attributes/full - get all attributes full.
        [Fact]
        public async Task TestReadAttributesFull()
        {
            var (result, status) = await _zipato.ReadAttributesFullAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /attributes/{uuid} - get an attribute details.
        [Theory]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c")]
        public async Task TestReadAttribute(string uuid)
        {
            var (result, status) = await _zipato.ReadAttributeAsync(new Guid(uuid), AttributeFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /attributes/{uuid}/children - get child attributes of an attribute.
        [Theory]
        [InlineData("4194dc6e-8d47-463d-bf4a-ad633164a0b8")]
        public async Task TestReadAttributeChildren(string uuid)
        {
            var (result, status) = await _zipato.ReadAttributeChildrenAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /attributes/{uuid}/config - get the configuration of the attribute.
        [Theory]
        [InlineData("4194dc6e-8d47-463d-bf4a-ad633164a0b8")]
        public async Task TestReadAttributeConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadAttributeConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /attributes/{uuid}/definition - get the definition of the attribute.
        [Theory]
        [InlineData("4194dc6e-8d47-463d-bf4a-ad633164a0b8")]
        public async Task TestReadAttributeDefinition(string uuid)
        {
            var (result, status) = await _zipato.ReadAttributeDefinitionAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /attributes/{uuid}/parent get the parent attribute of an attribute.
        [Theory]
        [InlineData("4194dc6e-8d47-463d-bf4a-ad633164a0b8")]
        public async Task TestReadAttributeParent(string uuid)
        {
            var (result, status) = await _zipato.ReadAttributeParentAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        // Testing GET /attributes/{uuid}/value - get the value of the attribute.
        [Theory]
        [InlineData("4194dc6e-8d47-463d-bf4a-ad633164a0b8")]
        public async Task TestReadAttributeValue(string uuid)
        {
            var (result, status) = await _zipato.ReadAttributeValueAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /bindings - get all bindings.
        [Fact]
        public async Task TestReadBindings()
        {
            var (result, status) = await _zipato.DataReadBindingsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /bindings/check - check what can the endpoint bind to.
        // Testing GET /bindings/{uuid} - get a binding.
        [Theory]
        [InlineData("a45e74e2-8a61-4d39-9fa0-786d915039cc")]
        public async Task TestReadBinding(string uuid)
        {
            var (result, status) = await _zipato.ReadBindingAsync(new Guid(uuid), BindingFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /bindings/{uuid}/config - get a binding config.
        [Theory]
        [InlineData("a45e74e2-8a61-4d39-9fa0-786d915039cc")]
        public async Task TestReadBindingConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadBindingConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /box - get the status of the Zipato.
        [Fact]
        public async Task TestReadBox()
        {
            var (result, status) = await _zipato.ReadBoxAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
            Assert.Equal("ZT7E104099D0D75BCC", result.Serial);
        }

        // Testing GET /box/config - get config of the current Zipato.
        [Fact]
        public async Task TestReadBoxConfig()
        {
            var (result, status) = await _zipato.ReadBoxConfigAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /box/config/{serial} - get config of another Zipato.
        [Theory]
        [InlineData("ZT7E104099D0D75BCC")]
        public async Task TestReadBoxConfigSerial(string serial)
        {
            var (result, status) = await _zipato.ReadBoxConfigAsync(serial);
            Assert.Equal(DataStatus.BadNotSupported, status.Code); // No clusters.
        }

        // Testing GET /box/current - get the status of the Zipato.
        [Fact]
        public async Task TestReadBoxInfoCurrent()
        {
            var (result, status) = await _zipato.ReadBoxInfoCurrentAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /box/list - list Zipatos available to the user.
        [Fact]
        public async Task TestReadBoxList()
        {
            var (result, status) = await _zipato.ReadBoxListAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /box/reboot - reboot the current Zipato.
        // Testing GET /box/reboot/{serial} - reboot Zipato wih serial.
        // Testing GET /box/replace/{serial} - replace the Zipato.
        // Testing GET /box/saveAll - send Zipato status to the server.
        // Testing GET /box/saveAndSynchronize - synchronize Zipato with the server.
        // Testing GET /box/select - select another Zipato.
        [Theory]
        [InlineData("ZT7E104099D0D75BCC")]
        public async Task TestReadBoxSelect(string serial)
        {
            var (result, status) = await _zipato.ReadBoxSelectAsync(serial);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /box/synchronize - synchronize Zipato with the server.
        // Testing GET /box/synchronizeRules - synchronize rules.

        // Testing GET /brands - get all brands.
        [Fact]
        public async Task TestReadBrands()
        {
            var (result, status) = await _zipato.ReadBrandsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /brands/used - list of currently used brands.
        [Fact]
        public async Task TestReadBrandsUsed()
        {
            var (result, status) = await _zipato.ReadBrandsUsedAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /brands/{name} - detailed information about brand, available networks and devices.
        [Theory]
        [InlineData("Zwave")]
        public async Task TestReadBrand(string name)
        {
            var (result, status) = await _zipato.ReadBrandAsync(name);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /cameras - get all cameras.
        [Fact]
        public async Task TestReadCameras()
        {
            var (result, status) = await _zipato.ReadCamerasAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /cameras/proxy - get http proxy url for ip device.
        //[Theory]
        //[InlineData("", 80)]
        //public async Task TestReadCamerasProxyData(string uuid, int port)
        //{
        //    var (result, status) = await _zipato.ReadCamerasProxyAsync(new Guid(uuid), port);
        //    Assert.Equal(DataValue.Good, status);
        //    Assert.NotEmpty(result);
        //}

        // Testing GET /cameras/{uuid} - get description of a specific camera.
        [Theory]
        [InlineData("eed3c4e2-62c3-4011-8756-9c294de702c5")]
        public async Task TestReadCamera(string uuid)
        {
            var (result, status) = await _zipato.ReadCameraAsync(new Guid(uuid), CameraFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /cameras/{uuid}/ptz/patrol/{patrol} - start Pan-Tilt-Zoom patrol program.
        // Testing GET /cameras/{uuid}/ptz/preset/{preset} - go to Pan-Tilt-Zoom preset.
        // Testing GET /cameras/{uuid}/ptz/{action} - perform a Pan-Tilt-Zoom action.
        // Testing GET /cameras/{uuid}/takeRecording - take 10 sec recording from the camera and put it to FTP server.
        [Theory]
        [InlineData("eed3c4e2-62c3-4011-8756-9c294de702c5")]
        public async Task TestReadCameraTakeRecording(string uuid)
        {
            var (result, status) = await _zipato.CameraTakeRecordingAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /cameras/{uuid}/takeSnapshot - take snapshot from the camera and put it to FTP server.
        [Theory]
        [InlineData("eed3c4e2-62c3-4011-8756-9c294de702c5")]
        public async Task TestReadCameraTakeSnapshot(string uuid)
        {
            var (result, status) = await _zipato.CameraTakeSnapshotAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /cluster - get all cluster.
        [Fact]
        public async Task TestReadClusters()
        {
            var (result, status) = await _zipato.ReadClustersAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.Empty(result);
        }

        // Testing GET /cluster/add/{serial} - add a box to the cluster.
        // Testing GET /cluster/join/{serial} - join a cluster.

        // Testing GET /cluster/{cluster} - show a cluster.
        [Theory]
        [InlineData(0)]
        public async Task TestReadCluster(int id)
        {
            var (result, status) = await _zipato.ReadClusterAsync(id);
            Assert.Equal(DataStatus.BadNotSupported, status.Code); // No clusters.
            Assert.NotNull(result);
        }

        // Testing GET /cluster/{cluster}/active - show active members of the cluster.
        // Testing GET /cluster/{cluster}/members - show members of the cluster.

        // Testing GET /clusterEndpoints - get all cluster endpoints.
        [Fact]
        public async Task TestReadClusterEndpoints()
        {
            var (result, status) = await _zipato.ReadClusterEndpointsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /clusterEndpoints/applyAll - (re)apply attributes.

        // Testing GET /clusterEndpoints/{uuid} - get a clusterEndpoint.
        [Theory]
        [InlineData("d029ff97-8817-49c1-9c3f-c841919f89db")]
        public async Task TestReadClusterEndpoint(string uuid)
        {
            var (result, status) = await _zipato.ReadClusterEndpointAsync(new Guid(uuid), ClusterEndpointFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /clusterEndpoints/{uuid}/actions - list available actions on cluster endpoint.
        [Theory]
        [InlineData("d029ff97-8817-49c1-9c3f-c841919f89db")]
        public async Task TestReadClusterEndpointActions(string uuid)
        {
            var (result, status) = await _zipato.ReadClusterEndpointActionsAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        // Testing GET /clusterEndpoints/{uuid}/config - get a clusterEndpoint config.
        [Theory]
        [InlineData("d029ff97-8817-49c1-9c3f-c841919f89db")]
        public async Task TestReadClusterEndpointConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadClusterEndpointConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /contacts - get all contacts.
        [Fact]
        public async Task TestReadContacts()
        {
            var (result, status) = await _zipato.ReadContactsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /contacts/self - Create or get a contact based on the current user.
        [Fact]
        public async Task TestReadContactSelf()
        {
            var (result, status) = await _zipato.ReadContactSelfAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /contacts/{id} - Get a contact.
        [Theory]
        [InlineData(27896)]
        [InlineData(27932)]
        public async Task TestReadContact(int id)
        {
            var (result, status) = await _zipato.ReadContactAsync(id);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /dealer/{serial} - get dealer of a box.
        [Theory]
        [InlineData("ZT7E104099D0D75BCC")]
        public async Task TestReadDealer(string serial)
        {
            var (result, status) = await _zipato.ReadDealerAsync(serial);
            Assert.Equal(DataValue.Good, status);
        }

        // Testing GET /devices - get all devices.
        [Fact]
        public async Task TestReadDevicesData()
        {
            var (result, status) = await _zipato.ReadDevicesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /devices/applyAll - (re)apply descriptors.
        // Testing GET /devices/applyMissing - apply all missing descriptors.
        // Testing GET /devices/find - get all devices.
        // Testing GET /devices/statuses - get device status.
        [Fact]
        public async Task TestReadDevicesStatuses()
        {
            var (result, status) = await _zipato.ReadDeviceStatusesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /devices/{uuid} - get a device.
        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestReadDevice(string uuid)
        {
            var (result, status) = await _zipato.ReadDeviceAsync(new Guid(uuid), DeviceFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /devices/{uuid}/config - get device config.
        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestReadDeviceConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadDeviceConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /devices/{uuid}/config/schema - get device config schema.
        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestReadDeviceSchema(string uuid)
        {
            var (result, status) = await _zipato.ReadDeviceSchemaAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /devices/{uuid}/endpoints - get endpoints of a device.
        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestReadDeviceEndpoints(string uuid)
        {
            var (result, status) = await _zipato.ReadDeviceEndpointsAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /devices/{uuid}/info - get device configuration info.
        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestReadDeviceInfo(string uuid)
        {
            var (result, status) = await _zipato.ReadDeviceInfoAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        // Testing GET /devices/{uuid}/status - get device status.
        [Theory]
        [InlineData("446f4a32-0b18-4893-b024-e090bff88aff")]
        public async Task TestReadDeviceStatus(string uuid)
        {
            var (result, status) = await _zipato.ReadDeviceStatusAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
        }

        // Testing GET /endpoints - get all endpoints.
        [Fact]
        public async Task TestReadEndpoints()
        {
            var (result, status) = await _zipato.ReadEndpointsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /endpoints/{uuid} - get a endpoint.
        [Theory]
        [InlineData("7596cb26-3cdc-4f8b-b665-eaf5cc26ff66")]
        public async Task TestReadEndpoint(string uuid)
        {
            var (result, status) = await _zipato.ReadEndpointAsync(new Guid(uuid), EndpointFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /endpoints/{uuid}/actions - list available actions on an endpoint.
        [Theory]
        [InlineData("7596cb26-3cdc-4f8b-b665-eaf5cc26ff66")]
        public async Task TestReadEndpointActions(string uuid)
        {
            var (result, status) = await _zipato.ReadEndpointActionsAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.Empty(result);
        }

        // Testing GET /endpoints/{uuid}/config - get a endpoint config.
        [Theory]
        [InlineData("7596cb26-3cdc-4f8b-b665-eaf5cc26ff66")]
        public async Task TestReadEndpointConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadEndpointConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /firmware - get all firmwares.
        // Testing GET /firmware/upgrade/beta - updagrade firmware to latest beta.
        // Testing GET /firmware/upgrade/old
        // Testing GET /firmware/upgrade/release - updagrade firmware to latest release.
        // Testing GET /firmware/upgrade/{serial}/beta - updagrade firmware to latest beta.
        // Testing GET /firmware/upgrade/{serial}/release - updagrade firmware to latest release.

        // Testing GET /groups - get all groups.
        [Fact]
        public async Task TestReadGroups()
        {
            var (result, status) = await _zipato.ReadGroupsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /groups/{uuid} - get a group.
        [Theory]
        [InlineData("a78751d3-7315-4b43-8887-a42ffd644c76")]
        public async Task TestReadGroup(string uuid)
        {
            var (result, status) = await _zipato.ReadGroupAsync(new Guid(uuid), GroupFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /groups/{uuid}/config get a group config.
        [Theory]
        [InlineData("a78751d3-7315-4b43-8887-a42ffd644c76")]
        public async Task TestReadGroupConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadGroupConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /log/attribute/{attribute} - get log of the attribute.
        [Theory]
        [InlineData("24042c03-0272-460e-8b81-7557ca994d6b", null, null, 10, SortOrderTypes.DESC)]
        public async Task TestReadLogAttribute(string uuid, DateTime? from, DateTime? until, int count, SortOrderTypes order)
        {
            var (result, status) = await _zipato.ReadLogAttributeAsync(new Guid(uuid), from, until, count, order);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /meteo
        [Fact]
        public async Task TestReadMeteo()
        {
            var (result, status) = await _zipato.ReadMeteoAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /meteo/attributes/values - get last value of all attributes.
        [Fact]
        public async Task TestReadMeteoValues()
        {
            var (result, status) = await _zipato.ReadMeteoValuesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /meteo/attributes/{uuid} - get attribute details.
        [Theory]
        [InlineData("fc4f6eb0-5872-4720-a14a-3caa6e486f0d", MeteoAttributeFlags.ALL)]
        public async Task TestReadMeteoAttribute(string uuid, MeteoAttributeFlags flag)
        {
            var (result, status) = await _zipato.ReadMeteoAttributeAsync(new Guid(uuid), flag);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /meteo/city
        [Theory]
        [InlineData("Linz, Austria")]
        public async Task TestReadMeteoCity(string location)
        {
            var (result, status) = await _zipato.ReadMeteoCityAsync(location);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /meteo/weather
        [Theory]
        [InlineData("Linz, Austria")]
        public async Task TestReadMeteoWeather(string location)
        {
            var (result, status) = await _zipato.ReadMeteoWeatherAsync(location);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /meteo/{uuid}/conditions
        [Theory]
        [InlineData("84eed82e-f61e-47a7-a58f-f43d369a4a76")]
        public async Task TestReadMeteoConditions(string uuid)
        {
            var (result, status) = await _zipato.ReadMeteoConditionsAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /meteo/{uuid}/forecast/{day}
        [Theory]
        [InlineData("84eed82e-f61e-47a7-a58f-f43d369a4a76", 1)]
        [InlineData("84eed82e-f61e-47a7-a58f-f43d369a4a76", 2)]
        [InlineData("84eed82e-f61e-47a7-a58f-f43d369a4a76", 3)]
        public async Task TestReadMeteoForecast(string uuid, int day)
        {
            var (result, status) = await _zipato.ReadMeteoForecastAsync(new Guid(uuid), day);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /multibox/list - list boxes owned the user.

        // Testing GET /networks - get all networks.
        [Fact]
        public async Task TestReadNetworks()
        {
            var (result, status) = await _zipato.ReadNetworksAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /networks/available - get list of available networks.
        [Fact]
        public async Task TestReadNetworksAvailable()
        {
            var (result, status) = await _zipato.ReadNetworksAvailableAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /networks/clearDuplicates - delete all duplicate networks.
        // Testing GET /networks/needFetch - check if the cloud if out-of-sync with the box.
        // Testing GET /networks/trees - get hierarchy under all networks.
        [Fact]
        public async Task TestReadNetworksTrees()
        {
            var (result, status) = await _zipato.ReadNetworksTreesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /networks/{uuid} - get a network.
        [Theory]
        [InlineData("946b1557-9fb1-48f1-b86c-94f3a4cbeb6b")]
        public async Task TestReadNetwork(string uuid)
        {
            var (result, status) = await _zipato.ReadNetworkAsync(new Guid(uuid), NetworkFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /networks/{uuid}/actions - list available actions on a network.
        [Theory]
        [InlineData("5d46a38a-9cca-4ae1-be10-640f89d6484c")]
        public async Task TestReadNetworkActions(string uuid)
        {
            var (result, status) = await _zipato.ReadNetworkActionsAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /networks/{uuid}/config - get a network config.
        [Theory]
        [InlineData("5d46a38a-9cca-4ae1-be10-640f89d6484c")]
        public async Task TestReadNetworkConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadNetworkConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /networks/{uuid}/discovery/{discovery} - get status of device discovery.
        // Testing GET /networks/{uuid}/discovery/{discovery}/devices - get discovered devices.
        // Testing GET /networks/{uuid}/tree - get hierarchy under a network.
        [Theory]
        [InlineData("946b1557-9fb1-48f1-b86c-94f3a4cbeb6b")]
        [InlineData("b1e74a45-eb2e-4ba1-831a-03292192fcf4")]
        [InlineData("3582e8be-73ec-4f06-bf5f-88337a1eca6a")]
        [InlineData("5d46a38a-9cca-4ae1-be10-640f89d6484c")]
        public async Task TestReadNetworkTree(string uuid)
        {
            var (result, status) = await _zipato.ReadNetworkTreeAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /rooms/ - list all rooms.
        [Fact]
        public async Task TestReadRooms()
        {
            var (result, status) = await _zipato.ReadRoomsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /rules - get all rules.
        [Fact]
        public async Task TestReadRules()
        {
            var (result, status) = await _zipato.ReadRulesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /rules/{id} - get a rule.
        [Theory]
        [InlineData(194646)]
        public async Task TestReadRule(int id)
        {
            var (result, status) = await _zipato.ReadRuleAsync(id);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /rules/{id}/code - get rule code.

        // Testing GET /scenes - get all scenes.
        [Fact]
        public async Task TestReadScenes()
        {
            var (result, status) = await _zipato.ReadScenesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /scenes/{uuid} - get a scene.
        [Theory]
        [InlineData("498163d5-f2af-4f74-a946-f555774f9c9d")]
        public async Task TestReadScene(string uuid)
        {
            var (result, status) = await _zipato.ReadSceneAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /scenes/{uuid}/run - run a scene.
        [Theory]
        [InlineData("bbd91ea8-a35e-438a-9775-68424c81e2da")]
        public async Task TestReadSceneRun(string uuid)
        {
            var (result, status) = await _zipato.ReadSceneRunAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /schedules - get all schedules.
        [Fact]
        public async Task TestReadSchedules()
        {
            var (result, status) = await _zipato.ReadSchedulesAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /schedules/{uuid} - get a schedule.
        [Theory]
        [InlineData("b6a99ab4-89ce-4c85-b01b-6cabd69742a2")]
        public async Task TestReadSchedule(string uuid)
        {
            var (result, status) = await _zipato.ReadScheduleAsync(new Guid(uuid), ScheduleFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /schedules/{uuid}/config - get a schedule config.
        [Theory]
        [InlineData("f17edb22-2726-459c-a4ae-79ea8806c367")]
        public async Task TestReadScheduleConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadScheduleConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /sip/contacts - returns all SIP contacts from SIP Server.
        // Testing GET /sip/contacts/{id} - returns SIP contact from SIP Server for input id.
        // Testing GET /sip/devices - returns all SIP devices and contacts from SIP Server.
        // Testing GET /sip/devices/{id} - returns SIP device from SIP Server from input id.
        // Testing GET /sip/devices/{id}/{serial} - returns SIP device from SIP Server from input id.
        // Testing GET /sip/server - returns SIP Server data.
        // Testing GET /sip/server/additional - returns additional SIP Server data.

        // Testing GET /snapshot/{serial} - list snapshots for the Zipato.
        [Theory]
        [InlineData("ZT7E104099D0D75BCC")]
        public async Task TestReadSnapshots(string serial)
        {
            var (result, status) = await _zipato.ReadSnapshotsAsync(serial);
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /snapshot/{serial}/{uuid} - get info about a snapshot.
        [Theory]
        [InlineData("ZT7E104099D0D75BCC", "b65db2fb-ad53-44ef-9d9a-a406e3d7921c")]
        public async Task TestReadSnapshot(string serial, string uuid)
        {
            var (result, status) = await _zipato.ReadSnapshotAsync(serial, new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /subscriptions get - subscription list for current Zipato.
        // Testing GET /subscriptions/{name} - get subscription by name.
        // Testing GET /subscriptions/{name}/attributes - get attributes for the subscription.

        // Testing GET /sv/camera/{uuid} - list files saved by the camera.
        [Theory]
        [InlineData("eed3c4e2-62c3-4011-8756-9c294de702c5", 0, 100, null, null, FileTypes.SNAPSHOT, false)]
        public async Task TestReadSavedFiles(string uuid, int start, int size, DateTime? from, DateTime? until, FileTypes type, bool refresh)
        {
            var (result, status) = await _zipato.ReadSavedFilesAsync(new Guid(uuid), start, size, from, until, type, refresh);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /sv/{id} - get saved file info.
        [Theory]
        [InlineData("vxg_933499_1538241854000_i")]
        public async Task TestReadSavedFile(string id)
        {
            var (result, status) = await _zipato.ReadSavedFileAsync(id);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /sv/{id}/dl - get a file.

        // Testing GET /thermostats - get all thermostats.
        [Fact]
        public async Task TestThermostats()
        {
            var (result, status) = await _zipato.ReadThermostatsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /thermostats/{uuid} - get a thermostat.
        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814")]
        public async Task TestReadThermostat(string uuid)
        {
            var (result, status) = await _zipato.ReadThermostatAsync(new Guid(uuid), ThermostatFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /thermostats/{uuid}/config - get thermostat configuration.
        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814")]
        public async Task TestReadThermostatConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadThermostatConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /thermostats/{uuid}/config/{operation} - get configuration of the thermostat actuator.
        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814", OperationTypes.TEMPERATURE)]
        public async Task TestReadThermostatConfigOperation(string uuid, OperationTypes operation)
        {
            var (result, status) = await _zipato.ReadThermostatConfigOperationAsync(new Guid(uuid), operation);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /thermostats/{uuid}/config/{operation}/inputs/ - list inputs assigned to the thermostat.
        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814", OperationTypes.HEATING)]
        public async Task TestReadThermostatInputs(string uuid, OperationTypes operation)
        {
            var (result, status) = await _zipato.ReadThermostatInputsAsync(new Guid(uuid), operation);
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /thermostats/{uuid}/config/{operation}/meters/ - list thermometers assigned to the thermostat.
        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814", OperationTypes.TEMPERATURE)]
        public async Task TestReadThermostatMeters(string uuid, OperationTypes operation)
        {
            var (result, status) = await _zipato.ReadThermostatMetersAsync(new Guid(uuid), operation);
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /thermostats/{uuid}/config/{operation}/outputs - list actuators assigned to the thermostat.
        [Theory]
        [InlineData("d1dcd6e2-4a7c-481c-be11-b3091839b814", OperationTypes.HEATING)]
        public async Task TestReadThermostatOutputs(string uuid, OperationTypes operation)
        {
            var (result, status) = await _zipato.ReadThermostatOutputsAsync(new Guid(uuid), operation);
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /types/search - search clusters and endpoints with given param type.
        // Testing GET /types/search/all - search all typed entities.
        // Testing GET /types/search/rooms/{room} - search typed entities within a room.
        // Testing GET /types/system/ - list all system icons.
        // Testing GET /types/system/{name} - find system icon by name.
        // Testing GET /types/user/ - list all user settable icons.
        // Testing GET /types/user/{name} - find user settable icon by name.

        // Testing GET /user/init - initialize nonce for login.
        // Testing GET /user/login - login.
        // Testing GET /user/logout - log the current user out.
        // Testing GET /user/nop - no operation.
        // Testing GET /user/restore - forgot password.
        // Testing GET /user/verify - submit user verification code.

        // Testing GET /users - show all users.
        [Fact]
        public async Task TestReadUsers()
        {
            var (result, status) = await _zipato.ReadUsersAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /users/current - show current user.
        [Fact]
        public async Task TestReadUsersCurrent()
        {
            var (result, status) = await _zipato.ReadUsersCurrentAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /users/roles/{role}/count - show status of counted roles.
        [Theory]
        [InlineData(2)]
        public async Task TestReadUsersRoleCount(int role)
        {
            var (result, status) = await _zipato.ReadUsersRoleCountAsync(role);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /users/{id}/boxUser - get boxUser for user or create one.
        [Theory]
        [InlineData(877)]
        public async Task TestReadUsersBoxUser(int id)
        {
            var (result, status) = await _zipato.ReadUsersBoxUserAsync(id);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /virtualEndpoints - get all virtual endpoints.
        [Fact]
        public async Task TestReadVirtualEndpoints()
        {
            var (result, status) = await _zipato.ReadVirtualEndpointsAsync();
            Assert.Equal(DataValue.Good, status);
            Assert.NotEmpty(result);
        }

        // Testing GET /virtualEndpoints/{uuid} - get a virtual endpoint.
        [Theory]
        [InlineData("29b5177d-a811-44ec-9b30-54d2736394c3")]
        public async Task TestReadVirtualEndpoint(string uuid)
        {
            var (result, status) = await _zipato.ReadVirtualEndpointAsync(new Guid(uuid), VirtualEndpointFlags.ALL);
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /virtualEndpoints/{uuid}/config - get virtual endpoint config.
        [Theory]
        [InlineData("29b5177d-a811-44ec-9b30-54d2736394c3")]
        public async Task TestReadVirtualEndpointConfig(string uuid)
        {
            var (result, status) = await _zipato.ReadVirtualEndpointConfigAsync(new Guid(uuid));
            Assert.Equal(DataValue.Good, status);
            Assert.NotNull(result);
        }

        // Testing GET /wallet - get the wallet of the current user.
        // Testing GET /wallet/box - get the wallet of the current controller.
        // Testing GET /wallet/box/{serial} - get the wallet of the another controller.
        // Testing GET /wallet/transactions - get the list of transactions.
        // Testing GET /wallet/transactions/box - get the list of transactions.
        // Testing GET /wallet/transactions/box/{serial} - get the list of transactions.
        // Testing GET /wallet/transactions/user - get the list of transactions.
        // Testing GET /wallet/transactions/user/{serial} - get the list of transactions.
        // Testing GET /wallet/user - get the wallet of the current user.
        // Testing GET /wallet/user/credit-warning - send warning e-mail message if credit balance is low.

        // Testing GET /wizard/create/{name} - start a wizard.
        // Testing GET /wizard/tx/{transaction}/cancel - cancel the wizard.
        // Testing GET /wizard/tx/{transaction}/next - next stetp, post form fields if present.
        // Testing GET /wizard/tx/{transaction}/poll - poll for update.
        // Testing GET /wizard/tx/{transaction}/repeat - repeat this step.

    }
}
