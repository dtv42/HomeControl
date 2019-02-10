// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.Get.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DataValueLib;

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Dtos;
    using ZipatoLib.Models.Enums;
    using ZipatoLib.Models.Entities;
    using ZipatoLib.Models.Flags;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Info.Rule;
    using ZipatoLib.Models;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Read Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Public Read Methods

        // Implements GET /alarm/full - get full alarm dump.
        public async Task<(AlarmInfo Data, DataStatus Status)>
            ReadAlarmFullAsync() =>
            await ReadDataAsync<AlarmInfo>("alarm/full");

        // Implements GET /alarm/config - get alarm configuration.
        public async Task<(AlarmConfig Data, DataStatus Status)>
            ReadAlarmConfigAsync() =>
            await ReadDataAsync<AlarmConfig>("alarm/config");

        // Implements GET /alarm/monitors - list monitors.
        public async Task<(List<MonitorInfo> Data, DataStatus Status)>
            ReadAlarmMonitorsAsync() =>
            await ReadListDataAsync<MonitorInfo>("alarm/monitors");

        // Implements GET /alarm/monitors/{monitor} - get monitor.
        public async Task<(MonitorInfo Data, DataStatus Status)>
            ReadAlarmMonitorAsync(Guid uuid) =>
            await ReadDataAsync<MonitorInfo>($"alarm/monitors/{uuid}");

        // Implements GET /alarm/monitors/{monitor}/config - get monitor configuration.
        public async Task<(MonitorConfig Data, DataStatus Status)>
            ReadAlarmMonitorConfigAsync(Guid uuid) =>
            await ReadDataAsync<MonitorConfig>($"alarm/monitors/{uuid}/config");

        // Implements GET /alarm/partitions - list partitions.
        public async Task<(List<PartitionInfo> Data, DataStatus Status)>
            ReadAlarmPartitionsAsync() =>
            await ReadListDataAsync<PartitionInfo>("alarm/partitions");

        // Implements GET /alarm/partitions/{partition} - get partition information.
        public async Task<(PartitionInfo Data, DataStatus Status)>
            ReadAlarmPartitionAsync(Guid uuid) =>
            await ReadDataAsync<PartitionInfo>($"alarm/partitions/{uuid}");

        // Implements GET /alarm/partitions/{partition}/attributes - get all attributes of the partition.
        public async Task<(List<AttributeData> Data, DataStatus Status)>
            ReadAlarmPartitionAttributesAsync(Guid uuid, AlarmPartitionAttributeFlags flag = AlarmPartitionAttributeFlags.ALL) =>
            await ReadListDataAsync<AttributeData>($"alarm/partitions/{uuid}/attributes{CreateQuery(flag)}");

        // Implements GET /alarm/partitions/{partition}/config - get partition configuration.
        public async Task<(PartitionConfig Data, DataStatus Status)>
            ReadAlarmPartitionConfigAsync(Guid uuid) =>
            await ReadDataAsync<PartitionConfig>($"alarm/partitions/{uuid}/config");

        // Implements GET /alarm/partitions/{partition}/events - list event log for partition
        public async Task<(List<AlarmLogDto> Data, DataStatus Status)>
            ReadAlarmPartitionEventsAsync(Guid uuid, AlarmPartitionEventFlags flag = AlarmPartitionEventFlags.NONE) =>
            await ReadListDataAsync<AlarmLogDto>($"alarm/partitions/{uuid}/events{CreateQuery(flag)}");

        // Implements GET /alarm/partitions/{partition}/eventsByTime - list event log for partition.
        public async Task<(List<AlarmLogDto> Data, DataStatus Status)>
            ReadAlarmPartitionEventsByTimeAsync(Guid uuid, DateTime timestamp, AlarmPartitionEventFlags flag = AlarmPartitionEventFlags.NONE) =>
            await ReadListDataAsync<AlarmLogDto>($"alarm/partitions/{uuid}/eventsByTime{CreateQuery(timestamp, flag)}");

        // Implements GET /alarm/partitions/{partition}/zones - list zones in partition.
        public async Task<(List<ZoneData> Data, DataStatus Status)>
            ReadAlarmPartitionZonesAsync(Guid uuid) =>
            await ReadListDataAsync<ZoneData>($"alarm/partitions/{uuid}/zones");

        // Implements GET /alarm/partitions/{partition}/zones/statuses - get statuses of all zones.
        public async Task<(List<ZoneState> Data, DataStatus Status)>
            ReadAlarmPartitionZoneStatesAsync(Guid uuid) =>
            await ReadListDataAsync<ZoneState>($"alarm/partitions/{uuid}/zones/statuses");

        // Implements GET /alarm/partitions/{partition}/zones/{zone} - get zone information.
        public async Task<(ZoneInfo Data, DataStatus Status)>
            ReadAlarmPartitionZoneAsync(Guid uuid, string zone, AlarmPartitionZoneFlags flag = AlarmPartitionZoneFlags.ALL) =>
            await ReadDataAsync<ZoneInfo>($"alarm/partitions/{uuid}/zones/{zone}{CreateQuery(flag)}");

        // Implements GET /alarm/partitions/{partition}/zones/{zone}/config - get zone config.
        public async Task<(ZoneConfig Data, DataStatus Status)>
            ReadAlarmPartitionZoneConfigAsync(Guid uuid, string zone) =>
            await ReadDataAsync<ZoneConfig>($"alarm/partitions/{uuid}/zones/{zone}/config");

        // Implements GET /alarm/partitions/{partition}/zones/{zone}/status - get zone status.
        public async Task<(ZoneFullState Data, DataStatus Status)>
            ReadAlarmPartitionZoneStatusAsync(Guid uuid, string zone) =>
            await ReadDataAsync<ZoneFullState>($"alarm/partitions/{uuid}/zones/{zone}/status");

        // Implements GET /announcements Get - announcments for currently loged in user and box.
        public async Task<(List<AnnouncementInfo> Data, DataStatus Status)>
            ReadAnnouncementsAsync() =>
            await ReadListDataAsync<AnnouncementInfo>("announcements");

        // Implements GET /attributes - get all attributes.
        public async Task<(List<AttributeData> Data, DataStatus Status)>
            ReadAttributesAsync() =>
            await ReadListDataAsync<AttributeData>("attributes");

        // Implements GET /attributes/values - get last value of all attributes.
        public async Task<(List<ValueData> Data, DataStatus Status)>
            ReadAttributeValuesAsync(bool update = false) =>
            await ReadListDataAsync<ValueData>($"attributes/values?update={update.ToString().ToLower()}");

        // Implements GET /attributes/full - get all attributes full.
        public async Task<(List<AttributeInfo> Data, DataStatus Status)>
            ReadAttributesFullAsync(AttributeFlags flags = AttributeFlags.ALL) =>
            await ReadListDataAsync<AttributeInfo>($"attributes/full{CreateQuery(flags)}");

        // Implements GET /attributes/{uuid} - get an attribute details.
        public async Task<(AttributeInfo Data, DataStatus Status)>
            ReadAttributeAsync(Guid uuid, AttributeFlags flag = AttributeFlags.ALL) =>
            await ReadDataAsync<AttributeInfo>($"attributes/{uuid}{CreateQuery(flag)}");

        // Implements GET /attributes/{uuid}/children - get child attributes of an attribute.
        public async Task<(List<AttributeData> Data, DataStatus Status)>
            ReadAttributeChildrenAsync(Guid uuid) =>
            await ReadListDataAsync<AttributeData>($"attributes/{uuid}/children");

        // Implements GET /attributes/{uuid}/config - get the configuration of the attribute.
        public async Task<(AttributeConfig Data, DataStatus Status)>
            ReadAttributeConfigAsync(Guid uuid) =>
            await ReadDataAsync<AttributeConfig>($"attributes/{uuid}/config");

        // Implements GET /attributes/{uuid}/definition - get the definition of the attribute.
        public async Task<(AttributeDefinition Data, DataStatus Status)>
            ReadAttributeDefinitionAsync(Guid uuid) =>
            await ReadDataAsync<AttributeDefinition>($"attributes/{uuid}/definition");

        // Implements GET /attributes/{uuid}/parent get the parent attribute of an attribute.
        public async Task<(AttributeData Data, DataStatus Status)>
            ReadAttributeParentAsync(Guid uuid) =>
            await ReadDataAsync<AttributeData>($"attributes/{uuid}/parent");

        // Implements GET /attributes/{uuid}/value - get the value of the attribute.
        public async Task<(AttributeValueDto Data, DataStatus Status)>
            ReadAttributeValueAsync(Guid uuid) =>
            await ReadDataAsync<AttributeValueDto>($"attributes/{uuid}/value");

        // Implements GET /bindings - get all bindings.
        public async Task<(List<BindingData> Data, DataStatus Status)>
            ReadBindingsAsync() =>
            await ReadListDataAsync<BindingData>("bindings");

        // Implements GET /bindings/check - check what can the endpoint bind to.

        // Implements GET /bindings/{uuid} - get a binding.
        public async Task<(BindingInfo Data, DataStatus Status)>
            ReadBindingAsync(Guid uuid, BindingFlags flag = BindingFlags.ALL) =>
            await ReadDataAsync<BindingInfo>($"bindings/{uuid}{CreateQuery(flag)}");

        // Implements GET /bindings/{uuid}/config - get a binding config.
        public async Task<(BindingConfig Data, DataStatus Status)>
            ReadBindingConfigAsync(Guid uuid) =>
            await ReadDataAsync<BindingConfig>($"bindings/{uuid}/config");

        // Implements GET /box - get the status of the Zipato.
        public async Task<(BoxInfo Data, DataStatus Status)>
            ReadBoxAsync() =>
            await ReadDataAsync<BoxInfo>("box");

        // Implements GET /box/config - get config of the current Zipato.
        public async Task<(BoxConfig Data, DataStatus Status)>
            ReadBoxConfigAsync() =>
            await ReadDataAsync<BoxConfig>("box/config");

        // Implements GET /box/config/{serial} - get config of the another Zipato.
        public async Task<(BoxConfig Data, DataStatus Status)>
            ReadBoxConfigAsync(string serial) =>
            await ReadDataAsync<BoxConfig>($"box/config/{serial}");

        // Implements GET /box/current - get the status of the Zipato.
        public async Task<(BoxInfo Data, DataStatus Status)>
            ReadBoxInfoCurrentAsync() =>
            await ReadDataAsync<BoxInfo>("box/current");

        // Implements GET /box/list - list Zipatos available to the user.
        public async Task<(List<BoxInfo> Data, DataStatus Status)>
            ReadBoxListAsync() =>
            await ReadListDataAsync<BoxInfo>("box/list");

        // Implements GET /box/reboot - reboot the current Zipato.
        // Implements GET /box/reboot/{serial} - reboot Zipato wih serial.
        // Implements GET /box/replace/{serial} - replace the Zipato.
        // Implements GET /box/saveAll - send Zipato status to the server.
        // Implements GET /box/saveAndSynchronize - synchronize Zipato with the server.
        // Implements GET /box/select - select another Zipato.
        public async Task<(BoxInfo Data, DataStatus Status)>
            ReadBoxSelectAsync(string serial) =>
            await ReadDataAsync<BoxInfo>($"box/select/?serial={serial}");

        // Implements GET /box/synchronize - synchronize Zipato with the server.
        // Implements GET /box/synchronizeRules - synchronize rules.

        // Implements GET /brands - get all brands.
        public async Task<(List<BrandData> Data, DataStatus Status)>
            ReadBrandsAsync() =>
            await ReadListDataAsync<BrandData>("brands");

        // Implements GET /brands/used - list of currently used brands.
        public async Task<(BrandsUsedData Data, DataStatus Status)>
            ReadBrandsUsedAsync() =>
            await ReadDataAsync<BrandsUsedData>("brands/used");

        // Implements GET /brands/{name} - detailed information about brand, available networks and devices.
        public async Task<(BrandInfo Data, DataStatus Status)>
            ReadBrandAsync(string name) =>
            await ReadDataAsync<BrandInfo>($"brands/{name}");

        // Implements GET /cameras - get all cameras.
        public async Task<(List<CameraData> Data, DataStatus Status)>
            ReadCamerasAsync() =>
            await ReadListDataAsync<CameraData>("cameras");

        // Implements GET /cameras/proxy - get http proxy url for ip device.
        public async Task<(string Data, DataStatus Status)>
            ReadCamerasProxyAsync(Guid uuid, int port = 80) =>
            await ReadDataAsync($"cameras/proxy?device={uuid}&port={port}");

        // Implements GET /cameras/{uuid} - get description of a specific camera.
        public async Task<(CameraInfo Data, DataStatus Status)>
            ReadCameraAsync(Guid uuid, CameraFlags flag = CameraFlags.ALL) =>
            await ReadDataAsync<CameraInfo>($"cameras/{uuid}{CreateQuery(flag)}");

        // Implements GET /cameras/{uuid}/ptz/patrol/{patrol} - start Pan-Tilt-Zoom patrol program.
        // Implements GET /cameras/{uuid}/ptz/preset/{preset} - go to Pan-Tilt-Zoom preset.
        // Implements GET /cameras/{uuid}/ptz/{action} - perform a Pan-Tilt-Zoom action.
        // Implements GET /cameras/{uuid}/takeRecording - take 10 sec recording from the camera and put it to FTP server.
        public async Task<(TransactionData Data, DataStatus Status)>
            CameraTakeRecordingAsync(Guid uuid) =>
            await ReadDataAsync<TransactionData>($"cameras/{uuid}/takeRecording");

        // Implements GET /cameras/{uuid}/takeSnapshot - take snapshot from the camera and put it to FTP server.
        public async Task<(TransactionData Data, DataStatus Status)>
            CameraTakeSnapshotAsync(Guid uuid) =>
            await ReadDataAsync<TransactionData>($"cameras/{uuid}/takeSnapshot");

        // Implements GET /cluster - get all cluster.
        public async Task<(List<ClusterData> Data, DataStatus Status)>
            ReadClustersAsync() =>
            await ReadListDataAsync<ClusterData>("cluster");

        // Implements GET /cluster/add/{serial} - add a box to the cluster.
        // Implements GET /cluster/join/{serial} - join a cluster.

        // Implements GET /cluster/{cluster} - show a cluster.
        public async Task<(ClusterInfo Data, DataStatus Status)>
            ReadClusterAsync(int id) =>
            await ReadDataAsync<ClusterInfo>($"cluster/{id}");

        // Implements GET /cluster/{cluster}/active - show active members of the cluster.
        // Implements GET /cluster/{cluster}/members - show members of the cluster.

        // Implements GET /clusterEndpoints - get all cluster endpoints.
        public async Task<(List<ClusterEndpointData> Data, DataStatus Status)>
            ReadClusterEndpointsAsync() =>
            await ReadListDataAsync<ClusterEndpointData>("clusterEndpoints");

        // Implements GET /clusterEndpoints/applyAll - (re)apply attributes.

        // Implements GET /clusterEndpoints/{uuid} - get a clusterEndpoint.
        public async Task<(ClusterEndpointInfo Data, DataStatus Status)>
            ReadClusterEndpointAsync(Guid uuid, ClusterEndpointFlags flag = ClusterEndpointFlags.ALL) =>
            await ReadDataAsync<ClusterEndpointInfo>($"clusterEndpoints/{uuid}{CreateQuery(flag)}");

        // Implements GET /clusterEndpoints/{uuid}/actions - list available actions on cluster endpoint.
        public async Task<(List<ActionData> Data, DataStatus Status)>
            ReadClusterEndpointActionsAsync(Guid uuid) =>
            await ReadListDataAsync<ActionData>($"clusterEndpoints/{uuid}/actions");

        // Implements GET /clusterEndpoints/{uuid}/config - get a clusterEndpoint config.
        public async Task<(ClusterEndpointConfig Data, DataStatus Status)>
            ReadClusterEndpointConfigAsync(Guid uuid) =>
            await ReadDataAsync<ClusterEndpointConfig>($"clusterEndpoints/{uuid}/config");

        // Implements GET /contacts - get all contacts.
        public async Task<(List<ContactInfo> Data, DataStatus Status)>
            ReadContactsAsync() =>
            await ReadListDataAsync<ContactInfo>("contacts");

        // Implements GET /contacts/self - Create or get a contact based on the current user.
        public async Task<(ContactInfo Data, DataStatus Status)>
            ReadContactSelfAsync() =>
            await ReadDataAsync<ContactInfo>("contacts/self");

        // Implements GET /contacts/{id} - Get a contact.
        public async Task<(ContactInfo Data, DataStatus Status)>
            ReadContactAsync(int id) =>
            await ReadDataAsync<ContactInfo>($"contacts/{id}");

        // Implements GET /dealer/{serial} - get dealer of a box.
        public async Task<(DealerData Data, DataStatus Status)>
            ReadDealerAsync(string serial) =>
            await ReadDataAsync<DealerData>($"dealer/{serial}");

        // Implements GET /devices - get all devices.
        public async Task<(List<DeviceData> Data, DataStatus Status)>
            ReadDevicesAsync() =>
            await ReadListDataAsync<DeviceData>("devices");

        // Implements GET /devices/applyAll - (re)apply descriptors.
        // Implements GET /devices/applyMissing - apply all missing descriptors.
        // Implements GET /devices/find - get all devices.
        // Implements GET /devices/statuses - get device status.
        public async Task<(List<DeviceStatus> Data, DataStatus Status)>
            ReadDeviceStatusesAsync() =>
            await ReadListDataAsync<DeviceStatus>("devices/statuses");

        // Implements GET /devices/{uuid} - get a device.
        public async Task<(DeviceInfo Data, DataStatus Status)>
            ReadDeviceAsync(Guid uuid, DeviceFlags flag = DeviceFlags.ALL) =>
            await ReadDataAsync<DeviceInfo>($"devices/{uuid}{CreateQuery(flag)}");

        // Implements GET /devices/{uuid}/config - get device config.
        public async Task<(DeviceConfig Data, DataStatus Status)>
            ReadDeviceConfigAsync(Guid uuid) =>
            await ReadDataAsync<DeviceConfig>($"devices/{uuid}/config");

        // Implements GET /devices/{uuid}/config/schema - get device config schema.
        public async Task<(DeviceSchema Data, DataStatus Status)>
            ReadDeviceSchemaAsync(Guid uuid) =>
            await ReadDataAsync<DeviceSchema>($"devices/{uuid}/config/schema");

        // Implements GET /devices/{uuid}/endpoints - get endpoints of a device.
        public async Task<(List<EndpointData> Data, DataStatus Status)>
            ReadDeviceEndpointsAsync(Guid uuid) =>
            await ReadListDataAsync<EndpointData>($"devices/{uuid}/endpoints");

        // Implements GET /devices/{uuid}/info - get device configuration info.
        public async Task<(DeviceInfo Data, DataStatus Status)>
            ReadDeviceInfoAsync(Guid uuid) =>
            await ReadDataAsync<DeviceInfo>($"devices/{uuid}/info");

        // Implements GET /devices/{uuid}/status - get device status.
        public async Task<(DeviceStatus Data, DataStatus Status)>
            ReadDeviceStatusAsync(Guid uuid) =>
            await ReadDataAsync<DeviceStatus>($"devices/{uuid}/status");

        // Implements GET /endpoints - get all endpoints.
        public async Task<(List<EndpointData> Data, DataStatus Status)>
            ReadEndpointsAsync() =>
            await ReadListDataAsync<EndpointData>("endpoints");

        // Implements GET /endpoints/{uuid} - get a endpoint.
        public async Task<(EndpointInfo Data, DataStatus Status)>
            ReadEndpointAsync(Guid uuid, EndpointFlags flag = EndpointFlags.ALL) =>
            await ReadDataAsync<EndpointInfo>($"endpoints/{uuid}{CreateQuery(flag)}");

        // Implements GET /endpoints/{uuid}/actions - list available actions on an endpoint.
        public async Task<(List<ActionData> Data, DataStatus Status)>
            ReadEndpointActionsAsync(Guid uuid) =>
            await ReadListDataAsync<ActionData>($"endpoints/{uuid}/actions");

        // Implements GET /endpoints/{uuid}/config - get a endpoint config.
        public async Task<(EndpointConfig Data, DataStatus Status)>
            ReadEndpointConfigAsync(Guid uuid) =>
            await ReadDataAsync<EndpointConfig>($"endpoints/{uuid}/config");

        // Implements GET /firmware - get all firmwares.
        // Implements GET /firmware/upgrade/beta - updagrade firmware to latest beta.
        // Implements GET /firmware/upgrade/old
        // Implements GET /firmware/upgrade/release - updagrade firmware to latest release.
        // Implements GET /firmware/upgrade/{serial}/beta - updagrade firmware to latest beta.
        // Implements GET /firmware/upgrade/{serial}/release - updagrade firmware to latest release.

        // Implements GET /groups - get all groups.
        public async Task<(List<GroupData> Data, DataStatus Status)>
            ReadGroupsAsync() =>
            await ReadListDataAsync<GroupData>("groups");

        // Implements GET /groups/{uuid} - get a group.
        public async Task<(GroupInfo Data, DataStatus Status)>
            ReadGroupAsync(Guid uuid, GroupFlags flag = GroupFlags.ALL) =>
            await ReadDataAsync<GroupInfo>($"groups/{uuid}{CreateQuery(flag)}");

        // Implements GET /groups/{uuid}/config get a group config.
        public async Task<(GroupConfig Data, DataStatus Status)>
            ReadGroupConfigAsync(Guid uuid) =>
            await ReadDataAsync<GroupConfig>($"groups/{uuid}/config");

        // Implements GET /log/attribute/{attribute} - get log of the attribute.
        public async Task<(ValueReport Data, DataStatus Status)>
            ReadLogAttributeAsync(Guid uuid, DateTime? from = null, DateTime? until = null, int count = 100, SortOrderTypes order = SortOrderTypes.DESC, DateTime? start = null, bool sticky = false) =>
            await ReadDataAsync<ValueReport>($"log/attribute/{uuid}{CreateQuery(from, until, count, order, start, sticky)}");

        // Implements GET /meteo
        public async Task<(List<MeteoData> Data, DataStatus Status)>
            ReadMeteoAsync() =>
            await ReadListDataAsync<MeteoData>("meteo");

        // Implements GET /meteo/attributes/values - get last value of all attributes.
        public async Task<(List<ValueData> Data, DataStatus Status)>
            ReadMeteoValuesAsync(bool update = false) =>
            await ReadListDataAsync<ValueData>($"meteo/attributes/values?update={update.ToString().ToLower()}");

        // Implements GET /meteo/attributes/{uuid} - get attribute details.
        public async Task<(AttributeData Data, DataStatus Status)>
            ReadMeteoAttributeAsync(Guid uuid, MeteoAttributeFlags flag = MeteoAttributeFlags.ALL) =>
            await ReadDataAsync<AttributeData>($"meteo/attributes/{uuid}{CreateQuery(flag)}");

        // Implements GET /meteo/city
        public async Task<(MeteoCityData Data, DataStatus Status)>
            ReadMeteoCityAsync(string location) =>
            await ReadDataAsync<MeteoCityData>($"meteo/city?location={location}");

        // Implements GET /meteo/weather
        public async Task<(WeatherData Data, DataStatus Status)>
            ReadMeteoWeatherAsync(string location) =>
            await ReadDataAsync<WeatherData>($"meteo/weather?location={location}");

        // Implements GET /meteo/{uuid}/conditions
        public async Task<(MeteoInfo Data, DataStatus Status)>
            ReadMeteoConditionsAsync(Guid uuid) =>
            await ReadDataAsync<MeteoInfo>($"meteo/{uuid}/conditions");

        // Implements GET /meteo/{uuid}/forecast/{day}
        public async Task<(MeteoInfo Data, DataStatus Status)>
            ReadMeteoForecastAsync(Guid uuid, int day) =>
            await ReadDataAsync<MeteoInfo>($"meteo/{uuid}/forecast/{day}");

        // Implements GET /multibox/list - list boxes owned the user.

        // Implements GET /networks - get all networks.
        public async Task<(List<NetworkData> Data, DataStatus Status)>
            ReadNetworksAsync() =>
            await ReadListDataAsync<NetworkData>("networks");

        // Implements GET /networks/available - get list of available networks.
        public async Task<(List<NetworkAvailableData> Data, DataStatus Status)>
            ReadNetworksAvailableAsync() =>
            await ReadListDataAsync<NetworkAvailableData>("networks/available");

        // Implements GET /networks/clearDuplicates - delete all duplicate networks.
        // Implements GET /networks/needFetch - check if the cloud if out-of-sync with the box.

        // Implements GET /networks/trees - get hierarchy under all networks.
        public async Task<(List<NetworkTree> Data, DataStatus Status)>
            ReadNetworksTreesAsync() =>
            await ReadListDataAsync<NetworkTree>("networks/trees");

        // Implements GET /networks/{uuid} - get a network.
        public async Task<(NetworkInfo Data, DataStatus Status)>
            ReadNetworkAsync(Guid uuid, NetworkFlags flag = NetworkFlags.ALL) =>
            await ReadDataAsync<NetworkInfo>($"networks/{uuid}{CreateQuery(flag)}");

        // Implements GET /networks/{uuid}/actions - list available actions on a network.
        public async Task<(List<ActionData> Data, DataStatus Status)>
            ReadNetworkActionsAsync(Guid uuid) =>
            await ReadListDataAsync<ActionData>($"networks/{uuid}/actions");

        // Implements GET /networks/{uuid}/config - get a network config.
        public async Task<(NetworkConfig Data, DataStatus Status)>
            ReadNetworkConfigAsync(Guid uuid) =>
            await ReadDataAsync<NetworkConfig>($"networks/{uuid}/config");

        // Implements GET /networks/{uuid}/discovery/{discovery} - get status of device discovery.
        // Implements GET /networks/{uuid}/discovery/{discovery}/devices - get discovered devices.

        // Implements GET /networks/{uuid}/tree - get hierarchy under a network.
        public async Task<(NetworkTree Data, DataStatus Status)>
            ReadNetworkTreeAsync(Guid uuid) =>
            await ReadDataAsync<NetworkTree>($"networks/{uuid}/tree");

        // Implements GET /rooms/ - list all rooms.
        public async Task<(List<RoomData> Data, DataStatus Status)>
            ReadRoomsAsync() =>
            await ReadListDataAsync<RoomData>("rooms/");

        // Implements GET /rules - get all rules.
        public async Task<(List<RuleData> Data, DataStatus Status)>
            ReadRulesAsync() =>
            await ReadListDataAsync<RuleData>("rules");

        // Implements GET /rules/{id} - get a rule.
        public async Task<(RuleInfo Data, DataStatus Status)>
            ReadRuleAsync(int id) =>
            await ReadDataAsync<RuleInfo>($"rules/{id}");

        // Implements GET /rules/{id}/code - get rule code.
        public async Task<(CodeData Data, DataStatus Status)>
            ReadRuleCodeAsync(int id) =>
            await ReadDataAsync<CodeData>($"rules/{id}/code");


        // Implements GET /scenes - get all scenes.
        public async Task<(List<SceneData> Data, DataStatus Status)>
            ReadScenesAsync() =>
            await ReadListDataAsync<SceneData>("scenes");

        // Implements GET /scenes/{uuid} - get a scene.
        public async Task<(SceneInfo Data, DataStatus Status)>
            ReadSceneAsync(Guid uuid) =>
            await ReadDataAsync<SceneInfo>($"scenes/{uuid}");

        // Implements GET /scenes/{uuid}/run - run a scene.
        public async Task<(string Data, DataStatus Status)>
            ReadSceneRunAsync(Guid uuid) =>
            await ReadDataAsync($"scenes/{uuid}/run");

        // Implements GET /schedules - get all schedules.
        public async Task<(List<ScheduleData> Data, DataStatus Status)>
            ReadSchedulesAsync() =>
            await ReadListDataAsync<ScheduleData>("schedules");

        // Implements GET /schedules/{uuid} - get a schedule.
        public async Task<(ScheduleInfo Data, DataStatus Status)>
            ReadScheduleAsync(Guid uuid, ScheduleFlags flag = ScheduleFlags.ALL) =>
            await ReadDataAsync<ScheduleInfo>($"schedules/{uuid}{CreateQuery(flag)}");

        // Implements GET /schedules/{uuid}/config - get a schedule config.
        public async Task<(ScheduleConfig Data, DataStatus Status)>
            ReadScheduleConfigAsync(Guid uuid) =>
            await ReadDataAsync<ScheduleConfig>($"schedules/{uuid}/config");

        // Implements GET /sip/contacts - returns all SIP contacts from SIP Server.
        // Implements GET /sip/contacts/{id} - returns SIP contact from SIP Server for input id.
        // Implements GET /sip/devices - returns all SIP devices and contacts from SIP Server.
        // Implements GET /sip/devices/{id} - returns SIP device from SIP Server from input id.
        // Implements GET /sip/devices/{id}/{serial} - returns SIP device from SIP Server from input id.
        // Implements GET /sip/server - returns SIP Server data.
        // Implements GET /sip/server/additional - returns additional SIP Server data.

        // Implements GET /snapshot/{serial} - list snapshots for the Zipato.
        public async Task<(List<SnapshotData> Data, DataStatus Status)>
            ReadSnapshotsAsync(string serial) =>
            await ReadListDataAsync<SnapshotData>($"snapshot/{serial}");

        // Implements GET /snapshot/{serial}/{uuid} - get info about a snapshot.
        public async Task<(SnapshotData Data, DataStatus Status)>
            ReadSnapshotAsync(string serial, Guid uuid) =>
            await ReadDataAsync<SnapshotData>($"snapshot/{serial}/{uuid}");

        // Implements GET /subscriptions get - subscription list for current Zipato.
        // Implements GET /subscriptions/{name} - get subscription by name.
        // Implements GET /subscriptions/{name}/attributes - get attributes for the subscription.

        // Implements GET /sv/camera/{uuid} - list files saved by the camera.
        public async Task<(List<FileData> Data, DataStatus Status)>
            ReadSavedFilesAsync(Guid uuid, int start, int size, DateTime? from = null, DateTime? until = null, FileTypes? type = null, bool refresh = false) =>
            await ReadListDataAsync<FileData>($"sv/camera/{uuid}{CreateQuery(start, size, from, until, type, refresh)}");

        // Implements GET /sv/{id} - get saved file info.
        public async Task<(FileData Data, DataStatus Status)>
            ReadSavedFileAsync(string id) =>
            await ReadDataAsync<FileData>($"sv/{id}");

        // Implements GET /sv/{id}/dl - get a file.

        // Implements GET /thermostats - get all thermostats.
        public async Task<(List<ThermostatData> Data, DataStatus Status)>
            ReadThermostatsAsync() =>
            await ReadListDataAsync<ThermostatData>("thermostats");

        // Implements GET /thermostats/{uuid} - get a thermostat.
        public async Task<(ThermostatInfo Data, DataStatus Status)>
            ReadThermostatAsync(Guid uuid, ThermostatFlags flag = ThermostatFlags.ALL) =>
            await ReadDataAsync<ThermostatInfo>($"thermostats/{uuid}{CreateQuery(flag)}");

        // Implements GET /thermostats/{uuid}/config - get thermostat configuration.
        public async Task<(ThermostatConfig Data, DataStatus Status)>
            ReadThermostatConfigAsync(Guid uuid) =>
            await ReadDataAsync<ThermostatConfig>($"thermostats/{uuid}/config");

        // Implements GET /thermostats/{uuid}/config/{operation} - get configuration of the thermostat actuator.
        public async Task<(ThermostatOperationConfig Data, DataStatus Status)>
            ReadThermostatConfigOperationAsync(Guid uuid, OperationTypes operation) =>
            await ReadDataAsync<ThermostatOperationConfig>($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}");

        // Implements GET /thermostats/{uuid}/config/{operation}/inputs/ - list inputs assigned to the thermostat.
        public async Task<(List<Entity> Data, DataStatus Status)>
            ReadThermostatInputsAsync(Guid uuid, OperationTypes operation) =>
            await ReadListDataAsync<Entity>($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}/inputs/");

        // Implements GET /thermostats/{uuid}/config/{operation}/meters/ - list thermometers assigned to the thermostat.
        public async Task<(List<Entity> Data, DataStatus Status)>
            ReadThermostatMetersAsync(Guid uuid, OperationTypes operation) =>
            await ReadListDataAsync<Entity>($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}/meters/");

        // Implements GET /thermostats/{uuid}/config/{operation}/outputs - list actuators assigned to the thermostat.
        public async Task<(List<Entity> Data, DataStatus Status)>
            ReadThermostatOutputsAsync(Guid uuid, OperationTypes operation) =>
            await ReadListDataAsync<Entity>($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}/outputs");

        // Implements GET /types/search - search clusters and endpoints with given param type.
        // Implements GET /types/search/all - search all typed entities.
        // Implements GET /types/search/rooms/{room} - search typed entities within a room.
        // Implements GET /types/system/ - list all system icons.
        // Implements GET /types/system/{name} - find system icon by name.
        // Implements GET /types/user/ - list all user settable icons.
        // Implements GET /types/user/{name} - find user settable icon by name.

        // Implements GET /user/init - initialize nonce for login.
        // Implements GET /user/login - login.
        // Implements GET /user/logout - log the current user out.
        // Implements GET /user/nop - no operation.
        // Implements GET /user/restore - forgot password.
        // Implements GET /user/verify - submit user verification code.

        // Implements GET /users - show all users.
        public async Task<(List<UserData> Data, DataStatus Status)>
            ReadUsersAsync() =>
            await ReadListDataAsync<UserData>("users");

        // Implements GET /users/current - show current user.
        public async Task<(UserData Data, DataStatus Status)>
            ReadUsersCurrentAsync() =>
            await ReadDataAsync<UserData>("users/current");

        // Implements GET /users/roles/{role}/count - show status of counted roles.
        public async Task<(UserData Data, DataStatus Status)>
            ReadUsersRoleCountAsync(int role) =>
            await ReadDataAsync<UserData>($"users/roles/{role}/count");

        // Implements GET /users/{id}/boxUser - get boxUser for user or create one.
        public async Task<(UserData Data, DataStatus Status)>
            ReadUsersBoxUserAsync(int id) =>
            await ReadDataAsync<UserData>($"users/{id}/boxUser");

        // Implements GET /virtualEndpoints - get all virtual endpoints.
        public async Task<(List<VirtualEndpointData> Data, DataStatus Status)>
            ReadVirtualEndpointsAsync() =>
            await ReadListDataAsync<VirtualEndpointData>("virtualEndpoints");

        // Implements GET /virtualEndpoints/{uuid} - get a virtual endpoint.
        public async Task<(VirtualEndpointInfo Data, DataStatus Status)>
            ReadVirtualEndpointAsync(Guid uuid, VirtualEndpointFlags flag = VirtualEndpointFlags.ALL) =>
            await ReadDataAsync<VirtualEndpointInfo>($"virtualEndpoints/{uuid}{CreateQuery(flag)}");

        // Implements GET /virtualEndpoints/{uuid}/config - get virtual endpoint config.
        public async Task<(VirtualEndpointConfig Data, DataStatus Status)>
            ReadVirtualEndpointConfigAsync(Guid uuid) =>
            await ReadDataAsync<VirtualEndpointConfig>($"virtualEndpoints/{uuid}/config");

        // Implements GET /wallet - get the wallet of the current user.
        // Implements GET /wallet/box - get the wallet of the current controller.
        // Implements GET /wallet/box/{serial} - get the wallet of the another controller.
        // Implements GET /wallet/transactions - get the list of transactions.
        // Implements GET /wallet/transactions/box - get the list of transactions.
        // Implements GET /wallet/transactions/box/{serial} - get the list of transactions.
        // Implements GET /wallet/transactions/user - get the list of transactions.
        // Implements GET /wallet/transactions/user/{serial} - get the list of transactions.
        // Implements GET /wallet/user - get the wallet of the current user.
        // Implements GET /wallet/user/credit-warning - send warning e-mail message if credit balance is low.

        // Implements GET /wizard/create/{name} - start a wizard.
        // Implements GET /wizard/tx/{transaction}/cancel - cancel the wizard.
        // Implements GET /wizard/tx/{transaction}/next - next stetp, post form fields if present.
        // Implements GET /wizard/tx/{transaction}/poll - poll for update.
        // Implements GET /wizard/tx/{transaction}/repeat - repeat this step.

        #endregion Public Read Methods
    }
}
