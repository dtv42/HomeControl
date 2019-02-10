// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IZipato.cs" company="DTV-Online">
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
    using ZipatoLib.Models;
    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Data.Color;
    using ZipatoLib.Models.Dtos;
    using ZipatoLib.Models.Entities;
    using ZipatoLib.Models.Enums;
    using ZipatoLib.Models.Flags;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Info.Rule;

    #endregion

    public interface IZipato : ISettingsData
    {
        ZipatoData Data { get; }
        ZipatoOthers Others { get; }
        ZipatoDevices Devices { get; }
        ZipatoSensors Sensors { get; }
        bool IsInitialized { get; }
        bool IsSessionActive { get; }

        void StartSession();
        void EndSession();

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadAllValuesAsync();
        Task<DataStatus> ReadAllDataAsync();

        #region Read Data Methods
        Task<(AttributeInfo Data, DataStatus Status)> DataReadAttributeAsync(Guid uuid);
        Task<(BindingInfo Data, DataStatus Status)> DataReadBindingAsync(Guid uuid);
        Task<(BrandInfo Data, DataStatus Status)> DataReadBrandAsync(string name);
        Task<(CameraInfo Data, DataStatus Status)> DataReadCameraAsync(Guid uuid);
        Task<(ClusterInfo Data, DataStatus Status)> DataReadClusterAsync(int id);
        Task<(ClusterEndpointInfo Data, DataStatus Status)> DataReadClusterEndpointAsync(Guid uuid);
        Task<(ContactInfo Data, DataStatus Status)> DataReadContactAsync(int id);
        Task<(DeviceInfo Data, DataStatus Status)> DataReadDeviceAsync(Guid uuid);
        Task<(EndpointInfo Data, DataStatus Status)> DataReadEndpointAsync(Guid uuid);
        Task<(GroupInfo Data, DataStatus Status)> DataReadGroupAsync(Guid uuid);
        Task<(NetworkInfo Data, DataStatus Status)> DataReadNetworkAsync(Guid uuid);
        Task<(NetworkTree Data, DataStatus Status)> DataReadNetworkTreeAsync(Guid uuid);
        Task<(RuleInfo Data, DataStatus Status)> DataReadRuleAsync(int id);
        Task<(SceneInfo Data, DataStatus Status)> DataReadSceneAsync(Guid uuid);
        Task<(ScheduleInfo Data, DataStatus Status)> DataReadScheduleAsync(Guid uuid);
        Task<(FileData Data, DataStatus Status)> DataReadSavedFileAsync(Guid uuid, string id);
        Task<(ThermostatInfo Data, DataStatus Status)> DataReadThermostatAsync(Guid uuid);
        Task<(VirtualEndpointInfo Data, DataStatus Status)> DataReadVirtualEndpointAsync(Guid uuid);
        Task<(AttributeValueDto Data, DataStatus Status)> DataReadValueAsync(Guid uuid);
        Task<(AttributeValueDto Data, DataStatus Status)> DataReadValueAsync(Guid endpoint, string name);
        #endregion Read Data Methods

        #region Public Delete Methods
        Task<DataStatus> DeleteAlarmMonitorAsync(Guid monitor);
        Task<DataStatus> DeleteAlarmPartitionAsync(Guid partition);
        Task<DataStatus> DeleteAlarmPartitionZoneAsync(Guid partition, string zone);
        Task<DataStatus> DeleteAttributeParentAsync(Guid uuid);
        Task<DataStatus> DeleteBindingAsync(Guid uuid);
        Task<DataStatus> DeleteCameraAsync(Guid uuid);
        Task<DataStatus> DeleteClusterEndpointAsync(Guid uuid);
        Task<DataStatus> DeleteContactAsync(int id);
        Task<DataStatus> DeleteDealerAsync(string serial);
        Task<DataStatus> DeleteDeviceAsync(Guid uuid);
        Task<DataStatus> DeleteEndpointAsync(Guid uuid);
        Task<DataStatus> DeleteGroupAsync(Guid uuid);
        Task<DataStatus> DeleteMeteoAsync(Guid uuid);
        Task<DataStatus> DeleteNetworkAsync(Guid uuid);
        Task<DataStatus> DeleteRoomAsync(int room);
        Task<DataStatus> DeleteRuleAsync(int id);
        Task<DataStatus> DeleteSceneAsync(Guid uuid);
        Task<DataStatus> DeleteScheduleAsync(Guid uuid);
        Task<DataStatus> DeleteSnapshotAsync(string serial, Guid uuid);
        Task<DataStatus> DeleteSavedFileAsync(string id);
        Task<DataStatus> DeleteThermostatAsync(Guid uuid);
        Task<DataStatus> DeleteThermostatInputAsync(Guid uuid, OperationTypes operation, Guid endpoint);
        Task<DataStatus> DeleteThermostatMeterAsync(Guid uuid, OperationTypes operation, Guid endpoint);
        Task<DataStatus> DeleteThermostatOutputAsync(Guid uuid, OperationTypes operation, Guid endpoint);
        Task<DataStatus> DeleteUserAsync(int id);
        Task<DataStatus> DeleteVirtualEndpointAsync(Guid uuid);
        #endregion Public Delete Methods

        #region Public Read Methods
        Task<(AlarmInfo Data, DataStatus Status)> ReadAlarmFullAsync();
        Task<(AlarmConfig Data, DataStatus Status)> ReadAlarmConfigAsync();
        Task<(List<MonitorInfo> Data, DataStatus Status)> ReadAlarmMonitorsAsync();
        Task<(MonitorInfo Data, DataStatus Status)> ReadAlarmMonitorAsync(Guid uuid);
        Task<(MonitorConfig Data, DataStatus Status)> ReadAlarmMonitorConfigAsync(Guid uuid);
        Task<(List<PartitionInfo> Data, DataStatus Status)> ReadAlarmPartitionsAsync();
        Task<(PartitionInfo Data, DataStatus Status)> ReadAlarmPartitionAsync(Guid uuid);
        Task<(List<AttributeData> Data, DataStatus Status)> ReadAlarmPartitionAttributesAsync(Guid uuid, AlarmPartitionAttributeFlags flag = AlarmPartitionAttributeFlags.ALL);
        Task<(PartitionConfig Data, DataStatus Status)> ReadAlarmPartitionConfigAsync(Guid uuid);
        Task<(List<AlarmLogDto> Data, DataStatus Status)> ReadAlarmPartitionEventsAsync(Guid uuid, AlarmPartitionEventFlags flag = AlarmPartitionEventFlags.NONE);
        Task<(List<AlarmLogDto> Data, DataStatus Status)> ReadAlarmPartitionEventsByTimeAsync(Guid uuid, DateTime timestamp, AlarmPartitionEventFlags flag = AlarmPartitionEventFlags.NONE);
        Task<(List<ZoneData> Data, DataStatus Status)> ReadAlarmPartitionZonesAsync(Guid uuid);
        Task<(List<ZoneState> Data, DataStatus Status)> ReadAlarmPartitionZoneStatesAsync(Guid uuid);
        Task<(ZoneInfo Data, DataStatus Status)> ReadAlarmPartitionZoneAsync(Guid uuid, string zone, AlarmPartitionZoneFlags flag = AlarmPartitionZoneFlags.ALL);
        Task<(ZoneConfig Data, DataStatus Status)> ReadAlarmPartitionZoneConfigAsync(Guid uuid, string zone);
        Task<(ZoneFullState Data, DataStatus Status)> ReadAlarmPartitionZoneStatusAsync(Guid uuid, string zone);
        Task<(List<AnnouncementInfo> Data, DataStatus Status)> ReadAnnouncementsAsync();
        Task<(List<AttributeData> Data, DataStatus Status)> ReadAttributesAsync();
        Task<(List<ValueData> Data, DataStatus Status)> ReadAttributeValuesAsync(bool update = false);
        Task<(List<AttributeInfo> Data, DataStatus Status)> ReadAttributesFullAsync(AttributeFlags flags = AttributeFlags.ALL);
        Task<(AttributeInfo Data, DataStatus Status)> ReadAttributeAsync(Guid uuid, AttributeFlags flag = AttributeFlags.ALL);
        Task<(List<AttributeData> Data, DataStatus Status)> ReadAttributeChildrenAsync(Guid uuid);
        Task<(AttributeConfig Data, DataStatus Status)> ReadAttributeConfigAsync(Guid uuid);
        Task<(AttributeDefinition Data, DataStatus Status)> ReadAttributeDefinitionAsync(Guid uuid);
        Task<(AttributeData Data, DataStatus Status)> ReadAttributeParentAsync(Guid uuid);
        Task<(AttributeValueDto Data, DataStatus Status)> ReadAttributeValueAsync(Guid uuid);
        Task<(List<BindingData> Data, DataStatus Status)> ReadBindingsAsync();
        Task<(BindingInfo Data, DataStatus Status)> ReadBindingAsync(Guid uuid, BindingFlags flag = BindingFlags.ALL);
        Task<(BindingConfig Data, DataStatus Status)> ReadBindingConfigAsync(Guid uuid);
        Task<(BoxInfo Data, DataStatus Status)> ReadBoxAsync();
        Task<(BoxConfig Data, DataStatus Status)> ReadBoxConfigAsync();
        Task<(BoxConfig Data, DataStatus Status)> ReadBoxConfigAsync(string serial);
        Task<(BoxInfo Data, DataStatus Status)> ReadBoxInfoCurrentAsync();
        Task<(List<BoxInfo> Data, DataStatus Status)> ReadBoxListAsync();
        Task<(BoxInfo Data, DataStatus Status)> ReadBoxSelectAsync(string serial);
        Task<(List<BrandData> Data, DataStatus Status)> ReadBrandsAsync();
        Task<(BrandsUsedData Data, DataStatus Status)> ReadBrandsUsedAsync();
        Task<(BrandInfo Data, DataStatus Status)> ReadBrandAsync(string name);
        Task<(List<CameraData> Data, DataStatus Status)> ReadCamerasAsync();
        Task<(string Data, DataStatus Status)> ReadCamerasProxyAsync(Guid uuid, int port = 80);
        Task<(CameraInfo Data, DataStatus Status)> ReadCameraAsync(Guid uuid, CameraFlags flag = CameraFlags.ALL);
        Task<(TransactionData Data, DataStatus Status)> CameraTakeRecordingAsync(Guid uuid);
        Task<(TransactionData Data, DataStatus Status)> CameraTakeSnapshotAsync(Guid uuid);
        Task<(List<ClusterData> Data, DataStatus Status)> ReadClustersAsync();
        Task<(ClusterInfo Data, DataStatus Status)> ReadClusterAsync(int id);
        Task<(List<ClusterEndpointData> Data, DataStatus Status)> ReadClusterEndpointsAsync();
        Task<(ClusterEndpointInfo Data, DataStatus Status)> ReadClusterEndpointAsync(Guid uuid, ClusterEndpointFlags flag = ClusterEndpointFlags.ALL);
        Task<(List<ActionData> Data, DataStatus Status)> ReadClusterEndpointActionsAsync(Guid uuid);
        Task<(ClusterEndpointConfig Data, DataStatus Status)> ReadClusterEndpointConfigAsync(Guid uuid);
        Task<(List<ContactInfo> Data, DataStatus Status)> ReadContactsAsync();
        Task<(ContactInfo Data, DataStatus Status)> ReadContactSelfAsync();
        Task<(ContactInfo Data, DataStatus Status)> ReadContactAsync(int id);
        Task<(DealerData Data, DataStatus Status)> ReadDealerAsync(string serial);
        Task<(List<DeviceData> Data, DataStatus Status)> ReadDevicesAsync();
        Task<(List<DeviceStatus> Data, DataStatus Status)> ReadDeviceStatusesAsync();
        Task<(DeviceInfo Data, DataStatus Status)> ReadDeviceAsync(Guid uuid, DeviceFlags flag = DeviceFlags.ALL);
        Task<(DeviceConfig Data, DataStatus Status)> ReadDeviceConfigAsync(Guid uuid);
        Task<(DeviceSchema Data, DataStatus Status)> ReadDeviceSchemaAsync(Guid uuid);
        Task<(List<EndpointData> Data, DataStatus Status)> ReadDeviceEndpointsAsync(Guid uuid);
        Task<(DeviceInfo Data, DataStatus Status)> ReadDeviceInfoAsync(Guid uuid);
        Task<(DeviceStatus Data, DataStatus Status)> ReadDeviceStatusAsync(Guid uuid);
        Task<(List<EndpointData> Data, DataStatus Status)> ReadEndpointsAsync();
        Task<(EndpointInfo Data, DataStatus Status)> ReadEndpointAsync(Guid uuid, EndpointFlags flag = EndpointFlags.ALL);
        Task<(List<ActionData> Data, DataStatus Status)> ReadEndpointActionsAsync(Guid uuid);
        Task<(EndpointConfig Data, DataStatus Status)> ReadEndpointConfigAsync(Guid uuid);
        Task<(List<GroupData> Data, DataStatus Status)> ReadGroupsAsync();
        Task<(GroupInfo Data, DataStatus Status)> ReadGroupAsync(Guid uuid, GroupFlags flag = GroupFlags.ALL);
        Task<(GroupConfig Data, DataStatus Status)> ReadGroupConfigAsync(Guid uuid);
        Task<(ValueReport Data, DataStatus Status)> ReadLogAttributeAsync(Guid uuid, DateTime? from = null, DateTime? until = null, int count = 100, SortOrderTypes order = SortOrderTypes.DESC, DateTime? start = null, bool sticky = false);
        Task<(List<MeteoData> Data, DataStatus Status)> ReadMeteoAsync();
        Task<(List<ValueData> Data, DataStatus Status)> ReadMeteoValuesAsync(bool update = false);
        Task<(AttributeData Data, DataStatus Status)> ReadMeteoAttributeAsync(Guid uuid, MeteoAttributeFlags flag = MeteoAttributeFlags.ALL);
        Task<(MeteoCityData Data, DataStatus Status)> ReadMeteoCityAsync(string location);
        Task<(WeatherData Data, DataStatus Status)> ReadMeteoWeatherAsync(string location);
        Task<(MeteoInfo Data, DataStatus Status)> ReadMeteoConditionsAsync(Guid uuid);
        Task<(MeteoInfo Data, DataStatus Status)> ReadMeteoForecastAsync(Guid uuid, int day);
        Task<(List<NetworkData> Data, DataStatus Status)> ReadNetworksAsync();
        Task<(List<NetworkAvailableData> Data, DataStatus Status)> ReadNetworksAvailableAsync();
        Task<(List<NetworkTree> Data, DataStatus Status)> ReadNetworksTreesAsync();
        Task<(NetworkInfo Data, DataStatus Status)> ReadNetworkAsync(Guid uuid, NetworkFlags flag = NetworkFlags.ALL);
        Task<(List<ActionData> Data, DataStatus Status)> ReadNetworkActionsAsync(Guid uuid);
        Task<(NetworkConfig Data, DataStatus Status)> ReadNetworkConfigAsync(Guid uuid);
        Task<(NetworkTree Data, DataStatus Status)> ReadNetworkTreeAsync(Guid uuid);
        Task<(List<RoomData> Data, DataStatus Status)> ReadRoomsAsync();
        Task<(List<RuleData> Data, DataStatus Status)> ReadRulesAsync();
        Task<(RuleInfo Data, DataStatus Status)> ReadRuleAsync(int id);
        Task<(CodeData Data, DataStatus Status)> ReadRuleCodeAsync(int id);
        Task<(List<SceneData> Data, DataStatus Status)> ReadScenesAsync();
        Task<(SceneInfo Data, DataStatus Status)> ReadSceneAsync(Guid uuid);
        Task<(string Data, DataStatus Status)> ReadSceneRunAsync(Guid uuid);
        Task<(List<ScheduleData> Data, DataStatus Status)> ReadSchedulesAsync();
        Task<(ScheduleInfo Data, DataStatus Status)> ReadScheduleAsync(Guid uuid, ScheduleFlags flag = ScheduleFlags.ALL);
        Task<(ScheduleConfig Data, DataStatus Status)> ReadScheduleConfigAsync(Guid uuid);
        Task<(List<SnapshotData> Data, DataStatus Status)> ReadSnapshotsAsync(string serial);
        Task<(SnapshotData Data, DataStatus Status)> ReadSnapshotAsync(string serial, Guid uuid);
        Task<(Dictionary<Guid, List<FileData>> Data, DataStatus Status)> DataReadSavedFilesAsync();
        Task<(List<FileData> Data, DataStatus Status)> ReadSavedFilesAsync(Guid uuid, int start, int size, DateTime? from = null, DateTime? until = null, FileTypes? type = null, bool refresh = false);
        Task<(FileData Data, DataStatus Status)> ReadSavedFileAsync(string id);
        Task<(List<ThermostatData> Data, DataStatus Status)> ReadThermostatsAsync();
        Task<(ThermostatInfo Data, DataStatus Status)> ReadThermostatAsync(Guid uuid, ThermostatFlags flag = ThermostatFlags.ALL);
        Task<(ThermostatConfig Data, DataStatus Status)> ReadThermostatConfigAsync(Guid uuid);
        Task<(ThermostatOperationConfig Data, DataStatus Status)> ReadThermostatConfigOperationAsync(Guid uuid, OperationTypes operation);
        Task<(List<Entity> Data, DataStatus Status)> ReadThermostatInputsAsync(Guid uuid, OperationTypes operation);
        Task<(List<Entity> Data, DataStatus Status)> ReadThermostatMetersAsync(Guid uuid, OperationTypes operation);
        Task<(List<Entity> Data, DataStatus Status)> ReadThermostatOutputsAsync(Guid uuid, OperationTypes operation);
        Task<(List<UserData> Data, DataStatus Status)> ReadUsersAsync();
        Task<(UserData Data, DataStatus Status)> ReadUsersCurrentAsync();
        Task<(UserData Data, DataStatus Status)> ReadUsersRoleCountAsync(int role);
        Task<(UserData Data, DataStatus Status)> ReadUsersBoxUserAsync(int id);
        Task<(List<VirtualEndpointData> Data, DataStatus Status)> ReadVirtualEndpointsAsync();
        Task<(VirtualEndpointInfo Data, DataStatus Status)> ReadVirtualEndpointAsync(Guid uuid, VirtualEndpointFlags flag = VirtualEndpointFlags.ALL);
        Task<(VirtualEndpointConfig Data, DataStatus Status)> ReadVirtualEndpointConfigAsync(Guid uuid);
        #endregion Public Read Methods

        #region Read Info Methods
        Task<(AlarmInfo Data, DataStatus Status)> DataReadAlarmAsync();
        Task<(List<AnnouncementInfo> Data, DataStatus Status)> DataReadAnnouncementsAsync();
        Task<(List<AttributeData> Data, DataStatus Status)> DataReadAttributesAsync();
        Task<(List<AttributeInfo> Data, DataStatus Status)> DataReadAttributesFullAsync();
        Task<(List<AttributeInfo> Data, DataStatus Status)> DataReadAttributesEndpointsAsync();
        Task<(List<ValueData> Data, DataStatus Status)> DataReadValuesAsync();
        Task<(List<BindingData> Data, DataStatus Status)> DataReadBindingsAsync();
        Task<(List<BindingInfo> Data, DataStatus Status)> DataReadBindingsFullAsync();
        Task<(List<BrandData> Data, DataStatus Status)> DataReadBrandsAsync();
        Task<(List<BrandInfo> Data, DataStatus Status)> DataReadBrandsFullAsync();
        Task<(List<BrandInfo> Data, DataStatus Status)> DataReadBrandsUsedAsync();
        Task<(List<CameraData> Data, DataStatus Status)> DataReadCamerasAsync();
        Task<(List<CameraInfo> Data, DataStatus Status)> DataReadCamerasFullAsync();
        Task<(List<ClusterData> Data, DataStatus Status)> DataReadClustersAsync();
        Task<(List<ClusterInfo> Data, DataStatus Status)> DataReadClustersFullAsync();
        Task<(List<ClusterEndpointData> Data, DataStatus Status)> DataReadClusterEndpointsAsync();
        Task<(List<ClusterEndpointInfo> Data, DataStatus Status)> DataReadClusterEndpointsFullAsync();
        Task<(List<ContactInfo> Data, DataStatus Status)> DataReadContactsAsync();
        Task<(List<DeviceData> Data, DataStatus Status)> DataReadDevicesAsync();
        Task<(List<DeviceInfo> Data, DataStatus Status)> DataReadDevicesFullAsync();
        Task<(List<EndpointData> Data, DataStatus Status)> DataReadEndpointsAsync();
        Task<(List<EndpointInfo> Data, DataStatus Status)> DataReadEndpointsFullAsync();
        Task<(List<EndpointInfo> Data, DataStatus Status)> DataReadEndpointsAttributesAsync();
        Task<(List<GroupData> Data, DataStatus Status)> DataReadGroupsAsync();
        Task<(List<GroupInfo> Data, DataStatus Status)> DataReadGroupsFullAsync();
        Task<(List<NetworkData> Data, DataStatus Status)> DataReadNetworksAsync();
        Task<(List<NetworkInfo> Data, DataStatus Status)> DataReadNetworksFullAsync();
        Task<(List<NetworkTree> Data, DataStatus Status)> DataReadNetworkTreesAsync();
        Task<(List<RoomData> Data, DataStatus Status)> DataReadRoomsAsync();
        Task<(List<RuleData> Data, DataStatus Status)> DataReadRulesAsync();
        Task<(List<RuleInfo> Data, DataStatus Status)> DataReadRulesFullAsync();
        Task<(List<RuleInfo> Data, DataStatus Status)> DataReadRulesCodeAsync();
        Task<(List<SceneData> Data, DataStatus Status)> DataReadScenesAsync();
        Task<(List<SceneInfo> Data, DataStatus Status)> DataReadScenesFullAsync();
        Task<(List<ScheduleData> Data, DataStatus Status)> DataReadSchedulesAsync();
        Task<(List<ScheduleInfo> Data, DataStatus Status)> DataReadSchedulesFullAsync();
        Task<(List<ThermostatData> Data, DataStatus Status)> DataReadThermostatsAsync();
        Task<(List<ThermostatInfo> Data, DataStatus Status)> DataReadThermostatsFullAsync();
        Task<(List<VirtualEndpointData> Data, DataStatus Status)> DataReadVirtualEndpointsAsync();
        Task<(List<VirtualEndpointInfo> Data, DataStatus Status)> DataReadVirtualEndpointsFullAsync();
        Task<(BoxInfo Data, DataStatus Status)> DataReadBoxAsync();
        #endregion Read Info Methods

        #region Public Create Methods
        Task<DataStatus> CreateAlarmMonitorAsync(MonitorData data);
        Task<DataStatus> DeleteFilesBatchAsync(List<string> data);
        #endregion Public Create Methods

        #region Public Update Methods
        Task<DataStatus> UpdateAlarmConfigAsync(AlarmConfig data);
        Task<DataStatus> UpdateAttributeValueAsync(Guid uuid, string data);
        #endregion Public Update Methods

        #region Public Helper Methods
        AnnouncementInfo GetAnnouncement(int id);
        AttributeInfo GetAttribute(Guid uuid);
        ValueData GetValue(Guid uuid);
        ValueData GetAttributeValue(Guid uuid);
        BindingInfo GetBinding(Guid uuid);
        BrandInfo GetBrand(string name);
        CameraInfo GetCamera(Guid uuid);
        ClusterInfo GetCluster(int id);
        ClusterEndpointInfo GetClusterEndpoint(Guid uuid);
        ContactInfo GetContact(int id);
        DeviceInfo GetDevice(Guid uuid);
        EndpointInfo GetEndpoint(Guid uuid);
        GroupInfo GetGroup(Guid uuid);
        NetworkInfo GetNetwork(Guid uuid);
        NetworkInfo GetNetwork(string name);
        NetworkTree GetNetworkTree(Guid network);
        RoomData GetRoom(int id);
        RuleData GetRule(int id);
        SceneInfo GetScene(Guid uuid);
        SceneInfo GetScene(string name);
        ScheduleInfo GetSchedule(Guid uuid);
        ThermostatInfo GetThermostat(Guid uuid);
        VirtualEndpointInfo GetVirtualEndpoint(Guid uuid);

        List<AttributeInfo> GetAttributes(Guid endpoint);
        EndpointInfo GetEndpoint(string name);
        AttributeInfo GetAttributeByName(Guid endpoint, string name);
        AttributeInfo GetAttributeByDefinition(Guid endpoint, string name);
        AttributeInfo GetAttributeByIndex(Guid endpoint, int index);
        ValueData GetValueByName(Guid endpoint, string name);
        ValueData GetValueByDefinition(Guid endpoint, string name);
        ValueData GetValueByIndex(Guid endpoint, int index);
        bool? GetState(Guid uuid);
        bool? GetBoolean(Guid uuid);
        int? GetNumber(Guid uuid);
        double? GetDouble(Guid uuid);
        string GetString(Guid uuid);
        Task<bool> SetValueAsync(Guid uuid, string value);
        Task<bool> SetValueByNameAsync(Guid endpoint, string name, string value);
        Task<bool> SetValueByDefinitionAsync(Guid endpoint, string name, string value);
        Task<bool> SetValueByIndexAsync(Guid endpoint, int index, string value);
        Task<bool> SetStateAsync(Guid uuid, bool state);
        Task<bool> SetBooleanAsync(Guid uuid, bool value);
        Task<bool> SetNumberAsync(Guid uuid, int number);
        Task<bool> SetDoubleAsync(Guid uuid, double number);
        Task<bool> SetColorAsync(Guid uuid, RGB color);
        Task<bool> SetColorAsync(Guid uuid, RGBW color);
        Task<bool> SetColorAsync(Guid uuid, string hex);
        bool CheckUserSession();
        Task<DataStatus> ReadPropertyAsync(string property);
        object GetPropertyValue(string property);
        #endregion Public Helper Methods
    }
}
