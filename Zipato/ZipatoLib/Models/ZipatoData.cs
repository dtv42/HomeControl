// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    using ZipatoLib.Extensions;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Info.Rule;

    #endregion

    /// <summary>
    /// Class holding all Zipato data items.
    /// </summary>
    public class ZipatoData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public BoxInfo Box { get; set; } = new BoxInfo();
        public AlarmInfo Alarm { get; set; } = new AlarmInfo();
        public List<AnnouncementInfo> Announcements { get; set; } = new List<AnnouncementInfo> { };
        public List<AttributeInfo> Attributes { get; set; } = new List<AttributeInfo> { };
        public List<BindingInfo> Bindings { get; set; } = new List<BindingInfo> { };
        public List<BrandInfo> Brands { get; set; } = new List<BrandInfo> { };
        public List<CameraInfo> Cameras { get; set; } = new List<CameraInfo> { };
        public List<ClusterInfo> Clusters { get; set; } = new List<ClusterInfo> { };
        public List<ClusterEndpointInfo> ClusterEndpoints { get; set; } = new List<ClusterEndpointInfo> { };
        public List<ContactInfo> Contacts { get; set; } = new List<ContactInfo> { };
        public List<DeviceInfo> Devices { get; set; } = new List<DeviceInfo> { };
        public List<EndpointInfo> Endpoints { get; set; } = new List<EndpointInfo> { };
        public List<GroupInfo> Groups { get; set; } = new List<GroupInfo> { };
        public List<NetworkInfo> Networks { get; set; } = new List<NetworkInfo> { };
        public List<NetworkTree> NetworkTrees { get; set; } = new List<NetworkTree> { };
        public List<RoomData> Rooms { get; set; } = new List<RoomData> { };
        public List<RuleInfo> Rules { get; set; } = new List<RuleInfo> { };
        public List<SceneInfo> Scenes { get; set; } = new List<SceneInfo> { };
        public List<ScheduleInfo> Schedules { get; set; } = new List<ScheduleInfo> { };
        public Dictionary<Guid, List<FileData>> SavedFiles { get; set; } = new Dictionary<Guid, List<FileData>> { };
        public List<ThermostatInfo> Thermostats { get; set; } = new List<ThermostatInfo> { };
        public List<VirtualEndpointInfo> VirtualEndpoints { get; set; } = new List<VirtualEndpointInfo> { };
        public List<ValueData> Values { get => Attributes?.Where(a => a.Value != null).Select(a => a.ToValueData()).ToList(); }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateAttributes(AttributeData data)
        {
            int index = Attributes.FindIndex(a => a.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Attributes[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Attributes.Add(data.ToAttributeInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateAttributes(AttributeInfo info)
        {
            int index = Attributes.FindIndex(a => a.Uuid == info.Uuid);
            info.CopyData();

            // Update
            if (index > -1)
            {
                Attributes[index] = info;
            }
            // Insert
            else
            {
                Attributes.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with selected new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateAttributesSelected(AttributeInfo info)
        {
            int index = Attributes.FindIndex(a => a.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Attributes[index].Endpoint = info.Endpoint;
                Attributes[index].Definition = info.Definition;
                Attributes[index].Value = info.Value;
            }
            // Insert
            else
            {
                Attributes.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanAttributes(List<Guid> list)
        {
            var deleted = Attributes.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Attributes.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateBindings(BindingData data)
        {
            int index = Bindings.FindIndex(b => b.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Bindings[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Bindings.Add(data.ToBindingInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateBindings(BindingInfo info)
        {
            int index = Bindings.FindIndex(b => b.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Bindings[index] = info;
            }
            // Insert
            else
            {
                Bindings.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanBindings(List<Guid> list)
        {
            var deleted = Bindings.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Bindings.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateBrands(BrandData data)
        {
            int index = Brands.FindIndex(b => b.Name == data.Name);

            // Update
            if (index > -1)
            {
                Brands[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Brands.Add(data.ToBrandInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateBrands(BrandInfo info)
        {
            int index = Brands.FindIndex(b => b.Name == info.Name);
            info.CopyData();

            // Update
            if (index > -1)
            {
                Brands[index] = info;
            }
            // Insert
            else
            {
                Brands.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanBrands(List<string> list)
        {
            var deleted = Brands.Where(item => list.All(name => name != item.Name)).ToList();

            // Delete
            foreach (var item in deleted)
                Brands.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateCameras(CameraData data)
        {
            int index = Cameras.FindIndex(c => c.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Cameras[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Cameras.Add(data.ToCameraInfo());
                SavedFiles.Add(data.Uuid.Value, new List<FileData> { });
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateCameras(CameraInfo info)
        {
            int index = Cameras.FindIndex(a => a.Uuid == info.Uuid);
            info.CopyData();

            // Update
            if (index > -1)
            {
                Cameras[index] = info;
            }
            // Insert
            else
            {
                Cameras.Add(info);
                SavedFiles.Add(info.Uuid.Value, new List<FileData> { });
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanCameras(List<Guid> list)
        {
            var deleted = Cameras.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Cameras.Remove(item);

            foreach (var item in deleted)
                SavedFiles.Remove(item.Uuid.Value);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateClusters(ClusterData data)
        {
            int index = Clusters.FindIndex(c => c.Id == data.Id);

            // Update
            if (index > -1)
            {
                Clusters[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Clusters.Add(data.ToClusterInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateClusters(ClusterInfo info)
        {
            int index = Clusters.FindIndex(c => c.Id == info.Id);
            info.CopyData();

            // Update
            if (index > -1)
            {
                Clusters[index] = info;
            }
            // Insert
            else
            {
                Clusters.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanClusters(List<int> list)
        {
            var deleted = Clusters.Where(item => list.All(id => id != item.Id)).ToList();

            // Delete
            foreach (var item in deleted)
                Clusters.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateClusterEndpoints(ClusterEndpointData data)
        {
            int index = ClusterEndpoints.FindIndex(e => e.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                ClusterEndpoints[index].CopyFrom(data);
            }
            // Insert
            else
            {
                ClusterEndpoints.Add(data.ToClusterEndpointInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateClusterEndpoints(ClusterEndpointInfo info)
        {
            int index = ClusterEndpoints.FindIndex(e => e.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                ClusterEndpoints[index] = info;
            }
            // Insert
            else
            {
                ClusterEndpoints.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanClusterEndpoints(List<Guid> list)
        {
            var deleted = ClusterEndpoints.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                ClusterEndpoints.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateContacts(ContactInfo info)
        {
            int index = Contacts.FindIndex(c => c.Id == info.Id);

            // Update
            if (index > -1)
            {
                Contacts[index] = info;
            }
            // Insert
            else
            {
                Contacts.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateDevices(DeviceData data)
        {
            int index = Devices.FindIndex(a => a.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Devices[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Devices.Add(data.ToDeviceInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateDevices(DeviceInfo info)
        {
            int index = Devices.FindIndex(d => d.Uuid == info.Uuid);
            info.CopyData();

            // Update
            if (index > -1)
            {
                Devices[index] = info;
            }
            // Insert
            else
            {
                Devices.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanDevices(List<Guid> list)
        {
            var deleted = Devices.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Devices.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateEndpoints(EndpointData data)
        {
            int index = Endpoints.FindIndex(e => e.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Endpoints[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Endpoints.Add(data.ToEndpointInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateEndpoints(EndpointInfo info)
        {
            int index = Endpoints.FindIndex(e => e.Uuid == info.Uuid);
            info.CopyData();

            // Update
            if (index > -1)
            {
                Endpoints[index] = info;
            }
            // Insert
            else
            {
                Endpoints.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with selected new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateEndpointsSelected(EndpointInfo info)
        {
            int index = Endpoints.FindIndex(e => e.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Endpoints[index].Attributes = info.Attributes;
            }
            // Insert
            else
            {
                Endpoints.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanEndpoints(List<Guid> list)
        {
            var deleted = Endpoints.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Endpoints.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateGroups(GroupData data)
        {
            int index = Groups.FindIndex(g => g.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Groups[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Groups.Add(data.ToGroupInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateGroups(GroupInfo info)
        {
            int index = Groups.FindIndex(g => g.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Groups[index] = info;
            }
            // Insert
            else
            {
                Groups.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanGroups(List<Guid> list)
        {
            var deleted = Groups.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Groups.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateNetworks(NetworkData data)
        {
            int index = Networks.FindIndex(n => n.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Networks[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Networks.Add(data.ToNetworkInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateNetworks(NetworkInfo info)
        {
            int index = Networks.FindIndex(n => n.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Networks[index] = info;
            }
            // Insert
            else
            {
                Networks.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanNetworks(List<Guid> list)
        {
            var deleted = Networks.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Networks.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="tree"></param>
        public void UpdateNetworkTrees(NetworkTree tree)
        {
            int index = NetworkTrees.FindIndex(t => t.Uuid == tree.Uuid);

            // Update
            if (index > -1)
            {
                NetworkTrees[index] = tree;
            }
            // Insert
            else
            {
                NetworkTrees.Add(tree);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanNetworkTrees(List<Guid> list)
        {
            var deleted = NetworkTrees.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                NetworkTrees.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateRooms(RoomData data)
        {
            int index = Rooms.FindIndex(r => r.Id == data.Id);

            // Update
            if (index > -1)
            {
                Rooms[index] = data;
            }
            // Insert
            else
            {
                Rooms.Add(data);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanRooms(List<int> list)
        {
            var deleted = Rooms.Where(item => list.All(id => id != item.Id)).ToList();

            // Delete
            foreach (var item in deleted)
                Rooms.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateRules(RuleData data)
        {
            int index = Rules.FindIndex(r => r.Id == data.Id);

            // Update
            if (index > -1)
            {
                Rules[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Rules.Add(data.ToRuleInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateRules(RuleInfo info)
        {
            int index = Rules.FindIndex(r => r.Id == info.Id);

            // Update
            if (index > -1)
            {
                Rules[index] = info;
            }
            // Insert
            else
            {
                Rules.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        public void UpdateRules(int id, CodeData code)
        {
            int index = Rules.FindIndex(r => r.Id == id);

            // Update
            if (index > -1)
            {
                Rules[index].Code = code;
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanRules(List<int> list)
        {
            var deleted = Rules.Where(item => list.All(id => id != item.Id)).ToList();

            // Delete
            foreach (var item in deleted)
                Rules.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateScenes(SceneData data)
        {
            int index = Scenes.FindIndex(s => s.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Scenes[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Scenes.Add(data.ToSceneInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateScenes(SceneInfo info)
        {
            int index = Scenes.FindIndex(s => s.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Scenes[index] = info;
            }
            // Insert
            else
            {
                Scenes.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanScenes(List<Guid> list)
        {
            var deleted = Scenes.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Scenes.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateSchedules(ScheduleData data)
        {
            int index = Schedules.FindIndex(s => s.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Schedules[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Schedules.Add(data.ToScheduleInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateSchedules(ScheduleInfo info)
        {
            int index = Schedules.FindIndex(s => s.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Schedules[index] = info;
            }
            // Insert
            else
            {
                Schedules.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanSchedules(List<Guid> list)
        {
            var deleted = Schedules.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Schedules.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateThermostats(ThermostatData data)
        {
            int index = Thermostats.FindIndex(t => t.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                Thermostats[index].CopyFrom(data);
            }
            // Insert
            else
            {
                Thermostats.Add(data.ToThermostatInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateThermostats(ThermostatInfo info)
        {
            int index = Thermostats.FindIndex(t => t.Uuid == info.Uuid);

            // Update
            if (index > -1)
            {
                Thermostats[index] = info;
            }
            // Insert
            else
            {
                Thermostats.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanThermostats(List<Guid> list)
        {
            var deleted = Thermostats.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                Thermostats.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateVirtualEndpoints(VirtualEndpointData data)
        {
            int index = VirtualEndpoints.FindIndex(e => e.Uuid == data.Uuid);

            // Update
            if (index > -1)
            {
                VirtualEndpoints[index].CopyFrom(data);
            }
            // Insert
            else
            {
                VirtualEndpoints.Add(data.ToVirtualEndpointInfo());
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="info">The new data.</param>
        public void UpdateVirtualEndpoints(VirtualEndpointInfo info)
        {
            int index = VirtualEndpoints.FindIndex(t => t.Uuid == info.Uuid);
            info.CopyData();

            // Update
            if (index > -1)
            {
                VirtualEndpoints[index] = info;
            }
            // Insert
            else
            {
                VirtualEndpoints.Add(info);
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanVirtualEndpoints(List<Guid> list)
        {
            var deleted = VirtualEndpoints.Where(item => list.All(uuid => uuid != item.Uuid)).ToList();

            // Delete
            foreach (var item in deleted)
                VirtualEndpoints.Remove(item);
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="uuid">The camera UUID.</param>
        /// <param name="data">The new data.</param>
        public void UpdateSavedFiles(Guid uuid, FileData data)
        {
            // Are camera files available.
            if (SavedFiles.ContainsKey(uuid))
            {
                var files = SavedFiles[uuid];
                int index = files.FindIndex(f => f.Id == data.Id);

                // Update
                if (index > -1)
                {
                    files[index] = data;
                }
                // Insert
                else
                {
                    files.Add(data);
                }
            }
            // Create new entry in dictionary.
            else
            {
                SavedFiles.Add(uuid, new List<FileData> { data });
            }
        }

        /// <summary>
        /// Helper method to update the list (remove missing items).
        /// </summary>
        /// <param name="uuid">The camera UUID.</param>
        /// <param name="list">The list of data items identifiers.</param>
        public void CleanSavedFiles(Guid uuid, List<string> list)
        {
            // Are camera files available.
            if (SavedFiles.ContainsKey(uuid))
            {
                var files = SavedFiles[uuid];
                var deleted = files.Where(item => list.All(id => id != item.Id)).ToList();

                // Delete
                foreach (var item in deleted)
                    files.Remove(item);
            }
        }

        /// <summary>
        /// Helper method to update the data item in the list with new data.
        /// </summary>
        /// <param name="data">The new data.</param>
        public void UpdateValues(ValueData data)
        {
            if ((data != null) && data.Uuid.HasValue && (data.Value != null))
            {
                int index = Attributes.FindIndex(a => a.Uuid == data.Uuid);

                // Update
                if (index > -1)
                {
                    Attributes[index].Value = data.Value;
                }
                // Insert
                else
                {
                    Attributes.Add(data.ToAttributeInfo());
                }
            }
        }

        #endregion

        #region Public Property Helper

        /// <summary>
        /// Gets the property list for the ZipatoData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(ZipatoData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the ETAPU11Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(ZipatoData), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(ZipatoData), property);

        /// <summary>
        /// Returns the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property) => PropertyValue.GetPropertyValue(this, property);

        /// <summary>
        /// Sets the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        public void SetPropertyValue(string property, object value) => PropertyValue.SetPropertyValue(this, property, value);

        #endregion Public Property Helper
    }
}
