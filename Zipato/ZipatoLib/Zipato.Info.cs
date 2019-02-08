// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.Info.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using DataValueLib;

    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Flags;
    using ZipatoLib.Models;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Read Data Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Read Info Methods

        /// <summary>
        /// Reads Alarm data.
        /// </summary>
        /// <returns></returns>
        public async Task<(AlarmInfo Data, DataStatus Status)> DataReadAlarmAsync()
        {
            var (alarm, status) = await ReadAlarmFullAsync();

            if (status.IsGood)
            {
                Data.Alarm = alarm;
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading alarm: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Alarm, Data.Status);
        }

        /// <summary>
        /// Read all announcements.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<AnnouncementInfo> Data, DataStatus Status)> DataReadAnnouncementsAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading announcement data not supported with local web service.");
                return (new List<AnnouncementInfo> { }, DataValue.BadNotSupported);
            }

            var (announcements, status) = await ReadAnnouncementsAsync();

            if (status.IsGood)
            {
                Data.Announcements = announcements;
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading announcements: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Announcements, Data.Status);
        }

        /// <summary>
        /// Read all attributes. Note this only updates name, id, and room id.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<AttributeData> Data, DataStatus Status)> DataReadAttributesAsync()
        {
            var (attributes, status) = await ReadAttributesAsync();

            if (status.IsGood)
            {
                foreach (var attribute in attributes)
                {
                    Data.UpdateAttributes(attribute);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading attributes: {status.Explanation}.");
            }

            Data.Status = status;
            return (attributes, Data.Status);
        }

        /// <summary>
        /// Read all attributes.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<AttributeInfo> Data, DataStatus Status)> DataReadAttributesFullAsync()
        {
            var (attributes, status) = await ReadAttributesFullAsync(AttributeFlags.ALL);

            if (status.IsGood)
            {
                foreach (var attribute in attributes)
                {
                    Data.UpdateAttributes(attribute);
                }

                Data.CleanAttributes(attributes.Select(a => a.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading attributes: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Attributes, Data.Status);
        }

        /// <summary>
        /// Read all attributes and their endpoints (including definitions and values).
        /// </summary>
        /// <returns></returns>
        public async Task<(List<AttributeInfo> Data, DataStatus Status)> DataReadAttributesEndpointsAsync()
        {
            var (attributes, status) = await ReadAttributesFullAsync(AttributeFlags.ENDPOINT | AttributeFlags.DEFINITION | AttributeFlags.VALUE);

            if (status.IsGood)
            {
                foreach (var attribute in attributes)
                {
                    Data.UpdateAttributesSelected(attribute);
                }

                Data.CleanAttributes(attributes.Select(a => a.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading attributes: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Attributes, Data.Status);
        }

        /// <summary>
        /// Read all endpoints and their attributes.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<EndpointInfo> Data, DataStatus Status)> DataReadEndpointsAttributesAsync()
        {
            var (endpoints, status) = await ReadEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    var (fullendpoint, fullstatus) = await ReadEndpointAsync(endpoint.Uuid.Value, EndpointFlags.ATTRIBUTES);

                    if (status.IsGood)
                    {
                        Data.UpdateEndpointsSelected(fullendpoint);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading endpoints: {fullstatus.Explanation}.");
                        return (Data.Endpoints, Data.Status);
                    }
                }

                Data.CleanEndpoints(endpoints.Select(e => e.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Endpoints, Data.Status);
        }

        /// <summary>
        /// Read all attribute values (using ReadAttributeValuesAsync()).
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ValueData> Data, DataStatus Status)> DataReadValuesAsync()
        {
            var (values, status) = await ReadAttributeValuesAsync(false);

            if (status.IsGood)
            {
                foreach (var value in values)
                {
                    Data.UpdateValues(value);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading values: {status.Explanation}.");
            }

            Data.Status = status;
            return (values, status);
        }

        /// <summary>
        /// Read all attribute values (using ReadAttributesFullAsync()).
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ValueData> Data, DataStatus Status)> DataReadAttributeValuesAsync()
        {
            var (attributes, status) = await ReadAttributesFullAsync(AttributeFlags.VALUE);

            if (status.IsGood)
            {
                foreach (var attribute in attributes)
                {
                    Data.UpdateValues(new ValueData() { Uuid = attribute.Uuid, Value = attribute.Value });
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading values: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Attributes.Select(a => new ValueData() { Uuid = a.Uuid, Value = a.Value }).ToList(), status);
        }

        /// <summary>
        /// Reads all bindings. Note this only updates name, and link.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<BindingData> Data, DataStatus Status)> DataReadBindingsAsync()
        {
            var (bindings, status) = await ReadBindingsAsync();

            if (status.IsGood)
            {
                foreach (var binding in bindings)
                {
                    Data.UpdateBindings(binding);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading bindings: {status.Explanation}.");
            }

            Data.Status = status;
            return (bindings, Data.Status);
        }

        /// <summary>
        /// Reads all bindings. Note this deletes missing bindings.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<BindingInfo> Data, DataStatus Status)> DataReadBindingsFullAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading bindings (full) not supported with local web service.");
                return (new List<BindingInfo> { }, DataValue.BadNotSupported);
            }

            var (bindings, status) = await ReadBindingsAsync();

            if (status.IsGood)
            {
                foreach (var binding in bindings)
                {
                    var (fullbinding, fullstatus) = await ReadBindingAsync(binding.Uuid.Value, BindingFlags.ALL);

                    if (fullstatus.IsGood)
                    {
                        Data.UpdateBindings(fullbinding);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading bindings: {fullstatus.Explanation}.");
                        return (Data.Bindings, Data.Status);
                    }
                }

                Data.CleanAttributes(bindings.Select(a => a.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading bindings: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Bindings, Data.Status);
        }

        /// <summary>
        /// Reads all available brands. Note this only updates link, description, order and role.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<BrandData> Data, DataStatus Status)> DataReadBrandsAsync()
        {
            var (brands, status) = await ReadBrandsAsync();

            if (status.IsGood)
            {
                foreach (var brand in brands)
                {
                    Data.UpdateBrands(brand);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading brands: {status.Explanation}.");
            }

            Data.Status = status;
            return (brands, Data.Status);
        }

        /// <summary>
        /// Reads all brands. Note this deletes missing brands.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<BrandInfo> Data, DataStatus Status)> DataReadBrandsFullAsync()
        {
            var (brands, status) = await ReadBrandsAsync();

            if (status.IsGood)
            {
                foreach (var brand in brands)
                {
                    var (fullbrand, fullstatus) = await ReadBrandAsync(brand.Name);

                    if (fullstatus.IsGood)
                    {
                        Data.UpdateBrands(fullbrand);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading brands: {fullstatus.Explanation}.");
                        return (Data.Brands, Data.Status);
                    }

                    Data.CleanBrands(brands.Select(b => b.Name).ToList());
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading brands: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Brands, Data.Status);
        }

        /// <summary>
        /// Reads all used brands. Note this only updates link, description, order and role.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<BrandInfo> Data, DataStatus Status)> DataReadBrandsUsedAsync()
        {
            var (usedbrands, status) = await ReadBrandsUsedAsync();

            if (status.IsGood)
            {
                foreach (var brand in usedbrands.BrandList)
                {
                    Data.UpdateBrands(brand);
                }

                foreach (var brand in usedbrands.DeviceBrands)
                {
                    Data.UpdateBrands(brand);
                }

                foreach (var brand in usedbrands.NetworkBrands)
                {
                    Data.UpdateBrands(brand);
                }

                var list = usedbrands.BrandList.Select(b => b.Name)
                                .Concat(usedbrands.DeviceBrands.Select(b => b.Name))
                                .Concat(usedbrands.NetworkBrands.Select(b => b.Name))
                                .ToList();

                Data.CleanBrands(list);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading brands: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Brands, Data.Status);
        }

        /// <summary>
        /// Reads all cameras. Note this only updates link, name and room id.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<CameraData> Data, DataStatus Status)> DataReadCamerasAsync()
        {
            var (cameras, status) = await ReadCamerasAsync();

            if (status.IsGood)
            {
                foreach (var camera in cameras)
                {
                    Data.UpdateCameras(camera);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading cameras: {status.Explanation}.");
            }

            Data.Status = status;
            return (cameras, Data.Status);
        }

        /// <summary>
        /// Reads all cameras. Note this deletes missing cameras.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<CameraInfo> Data, DataStatus Status)> DataReadCamerasFullAsync()
        {
            var (cameras, status) = await ReadCamerasAsync();

            if (status.IsGood)
            {
                foreach (var camera in cameras)
                {
                    var (fullcamera, fullstatus) = await ReadCameraAsync(camera.Uuid.Value, CameraFlags.ALL);

                    if (fullstatus.IsGood)
                    {
                        Data.UpdateCameras(fullcamera);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading cameras: {fullstatus.Explanation}.");
                        return (Data.Cameras, Data.Status);
                    }

                    Data.CleanCameras(cameras.Select(a => a.Uuid.Value).ToList());
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading cameras: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Cameras, Data.Status);
        }

        /// <summary>
        /// Reads all accessible clusters.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ClusterData> Data, DataStatus Status)> DataReadClustersAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading clusters not supported with local web service.");
                return (new List<ClusterData> { }, DataValue.BadNotSupported);
            }

            var (clusters, status) = await ReadClustersAsync();

            if (status.IsGood)
            {
                foreach (var cluster in clusters)
                {
                    Data.UpdateClusters(cluster);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading clusters: {status.Explanation}.");
            }

            Data.Status = status;
            return (clusters, Data.Status);
        }

        /// <summary>
        /// Reads all clusters. Note this deletes missing clusters.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ClusterInfo> Data, DataStatus Status)> DataReadClustersFullAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading clusters (full) not supported with local web service.");
                return (new List<ClusterInfo> { }, DataValue.BadNotSupported);
            }

            var (clusters, status) = await ReadClustersAsync();

            if (status.IsGood)
            {
                foreach (var cluster in clusters)
                {
                    var (fullcluster, fullstatus) = await ReadClusterAsync(cluster.Id.Value);

                    if (fullstatus.IsGood)
                    {
                        Data.UpdateClusters(fullcluster);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading clusters: {fullstatus.Explanation}.");
                        return (Data.Clusters, Data.Status);
                    }

                    Data.CleanClusters(clusters.Select(c => c.Id.Value).ToList());
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading clusters: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Clusters, Data.Status);
        }

        /// <summary>
        /// Reads all cluster endpoints. Note this only updates link, name, tags and room id.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ClusterEndpointData> Data, DataStatus Status)> DataReadClusterEndpointsAsync()
        {
            var (endpoints, status) = await ReadClusterEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    Data.UpdateClusterEndpoints(endpoint);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading cluster endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (endpoints, Data.Status);
        }

        /// <summary>
        /// Reads all cluster endpoints. Note this deletes missing cluster endpoints.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ClusterEndpointInfo> Data, DataStatus Status)> DataReadClusterEndpointsFullAsync()
        {
            var (endpoints, status) = await ReadClusterEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    var (fullendpoint, fullstatus) = await ReadClusterEndpointAsync(endpoint.Uuid.Value, ClusterEndpointFlags.ALL);

                    if (status.IsGood)
                    {
                        Data.UpdateClusterEndpoints(fullendpoint);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading cluster endpoints: {fullstatus.Explanation}.");
                        return (Data.ClusterEndpoints, Data.Status);
                    }
                }

                Data.CleanClusterEndpoints(endpoints.Select(e => e.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading cluster endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.ClusterEndpoints, Data.Status);
        }

        /// <summary>
        /// Reads all contacts for current user.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ContactInfo> Data, DataStatus Status)> DataReadContactsAsync()
        {
            var (contacts, status) = await ReadContactsAsync();

            if (status.IsGood)
            {
                foreach (var contact in contacts)
                {
                    Data.UpdateContacts(contact);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading contacts: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Contacts, Data.Status);
        }

        /// <summary>
        /// Reads all devices. Note this only updates link, name, tags, and room id.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<DeviceData> Data, DataStatus Status)> DataReadDevicesAsync()
        {
            var (devices, status) = await ReadDevicesAsync();

            if (status.IsGood)
            {
                foreach (var device in devices)
                {
                    Data.UpdateDevices(device);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading devices: {status.Explanation}.");
            }

            Data.Status = status;
            return (devices, Data.Status);
        }

        /// <summary>
        /// Reads all devices. Note this deletes missing devices.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<DeviceInfo> Data, DataStatus Status)> DataReadDevicesFullAsync()
        {
            var (devices, status) = await ReadDevicesAsync();

            if (status.IsGood)
            {
                foreach (var device in devices)
                {
                    var (fulldevice, fullstatus) = await ReadDeviceAsync(device.Uuid.Value, DeviceFlags.ALL);

                    if (status.IsGood)
                    {
                        Data.UpdateDevices(fulldevice);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading devices: {fullstatus.Explanation}.");
                        return (Data.Devices, Data.Status);
                    }
                }

                Data.CleanDevices(devices.Select(d => d.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading devices: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Devices, Data.Status);
        }

        /// <summary>
        /// Reads all endpoints. Note this only updates link, name, tags, and room id.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<EndpointData> Data, DataStatus Status)> DataReadEndpointsAsync()
        {
            var (endpoints, status) = await ReadEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    Data.UpdateEndpoints(endpoint);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (endpoints, Data.Status);
        }

        /// <summary>
        /// Reads all endpoints. Note this deletes missing endpoints.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<EndpointInfo> Data, DataStatus Status)> DataReadEndpointsFullAsync()
        {
            var (endpoints, status) = await ReadEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    var (fullendpoint, fullstatus) = await ReadEndpointAsync(endpoint.Uuid.Value, EndpointFlags.ALL);

                    if (status.IsGood)
                    {
                        Data.UpdateEndpoints(fullendpoint);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading endpoints: {fullstatus.Explanation}.");
                        return (Data.Endpoints, Data.Status);
                    }
                }

                Data.CleanEndpoints(endpoints.Select(e => e.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Endpoints, Data.Status);
        }

        /// <summary>
        /// Reads all groups. Note this only updates link and name.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<GroupData> Data, DataStatus Status)> DataReadGroupsAsync()
        {
            var (groups, status) = await ReadGroupsAsync();

            if (status.IsGood)
            {
                foreach (var group in groups)
                {
                    Data.UpdateGroups(group);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading groups: {status.Explanation}.");
            }

            Data.Status = status;
            return (groups, Data.Status);
        }

        /// <summary>
        /// Reads all groups. Note this deletes missing bindings.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<GroupInfo> Data, DataStatus Status)> DataReadGroupsFullAsync()
        {
            var (groups, status) = await ReadGroupsAsync();

            if (status.IsGood)
            {
                foreach (var group in groups)
                {
                    var (fullgroup, fullstatus) = await ReadGroupAsync(group.Uuid.Value, GroupFlags.ALL);

                    if (status.IsGood)
                    {
                        Data.UpdateGroups(fullgroup);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading groups: {fullstatus.Explanation}.");
                        return (Data.Groups, Data.Status);
                    }
                }

                Data.CleanGroups(groups.Select(g => g.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading groups: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Groups, Data.Status);
        }

        /// <summary>
        /// Reads all networks. Note this only updates link and name.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<NetworkData> Data, DataStatus Status)> DataReadNetworksAsync()
        {
            var (networks, status) = await ReadNetworksAsync();

            if (status.IsGood)
            {
                foreach (var network in networks)
                {
                    Data.UpdateNetworks(network);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading networks: {status.Explanation}.");
            }

            Data.Status = status;
            return (networks, Data.Status);
        }

        /// <summary>
        /// Reads all networks.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<NetworkInfo> Data, DataStatus Status)> DataReadNetworksFullAsync()
        {
            var (networks, status) = await ReadNetworksAsync();

            if (status.IsGood)
            {
                foreach (var network in networks)
                {
                    var (fullnetwork, fullstatus) = await ReadNetworkAsync(network.Uuid.Value, NetworkFlags.ALL);

                    if (status.IsGood)
                    {
                        Data.UpdateNetworks(fullnetwork);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading networks: {fullstatus.Explanation}.");
                        return (Data.Networks, Data.Status);
                    }
                }

                Data.CleanNetworks(networks.Select(n => n.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading networks: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Networks, Data.Status);
        }

        /// <summary>
        /// Reads all network tree data.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<NetworkTree> Data, DataStatus Status)> DataReadNetworkTreesAsync()
        {
            var (trees, status) = await ReadNetworksTreesAsync();

            if (status.IsGood)
            {
                foreach (var tree in trees)
                {
                    Data.UpdateNetworkTrees(tree);
                }

                Data.CleanNetworkTrees(trees.Select(t => t.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading network trees: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.NetworkTrees, Data.Status);
        }

        /// <summary>
        /// Reads all rooms.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<RoomData> Data, DataStatus Status)> DataReadRoomsAsync()
        {
            var (rooms, status) = await ReadRoomsAsync();

            if (status.IsGood)
            {
                foreach (var room in rooms)
                {
                    Data.UpdateRooms(room);
                }

                Data.CleanRooms(rooms.Select(r => r.Id.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading rooms: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Rooms, Data.Status);
        }

        /// <summary>
        /// Reads all rules.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<RuleData> Data, DataStatus Status)> DataReadRulesAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading rules not supported with local web service.");
                return (new List<RuleData> { }, DataValue.BadNotSupported);
            }

            var (rules, status) = await ReadRulesAsync();

            if (status.IsGood)
            {
                foreach (var rule in rules)
                {
                    Data.UpdateRules(rule);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading rules: {status.Explanation}.");
            }

            Data.Status = status;
            return (rules, Data.Status);
        }

        /// <summary>
        /// Reads all rules code. Note this deletes missing rules.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<RuleInfo> Data, DataStatus Status)> DataReadRulesFullAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading rules (full) not supported with local web service.");
                return (new List<RuleInfo> { }, DataValue.BadNotSupported);
            }

            var (rules, status) = await ReadRulesAsync();

            if (status.IsGood)
            {
                foreach (var rule in rules)
                {
                    var (fullrule, fullstatus) = await ReadRuleAsync(rule.Id.Value);

                    if (status.IsGood)
                    {
                        Data.UpdateRules(fullrule);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading rules: {fullstatus.Explanation}.");
                        return (Data.Rules, Data.Status);
                    }
                }

                Data.CleanRules(rules.Select(r => r.Id.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading rules: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Rules, Data.Status);
        }

        /// <summary>
        /// Reads all rules code. Note this only updates the code.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<RuleInfo> Data, DataStatus Status)> DataReadRulesCodeAsync()
        {
            var (rules, status) = await ReadRulesAsync();

            if (status.IsGood)
            {
                foreach (var rule in rules)
                {
                    var (code, codestatus) = await ReadRuleCodeAsync(rule.Id.Value);

                    if (status.IsGood)
                    {
                        Data.UpdateRules(rule.Id.Value, code);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {codestatus.Code} in reading rules code: {codestatus.Explanation}.");
                        return (Data.Rules, Data.Status);
                    }
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading rules: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Rules, Data.Status);
        }

        /// <summary>
        /// Reads all scenes. Note this only updates link, name, and tags.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<SceneData> Data, DataStatus Status)> DataReadScenesAsync()
        {
            var (scenes, status) = await ReadScenesAsync();

            if (status.IsGood)
            {
                foreach (var scene in scenes)
                {
                    Data.UpdateScenes(scene);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading scenes: {status.Explanation}.");
            }

            Data.Status = status;
            return (scenes, Data.Status);
        }

        /// <summary>
        /// Reads all scenes. Note this deletes missing scenes.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<SceneInfo> Data, DataStatus Status)> DataReadScenesFullAsync()
        {
            var (scenes, status) = await ReadScenesAsync();

            if (status.IsGood)
            {
                foreach (var scene in scenes)
                {
                    var (fullscene, fullstatus) = await ReadSceneAsync(scene.Uuid.Value);

                    if (status.IsGood)
                    {
                        Data.UpdateScenes(scene);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading scenes: {fullstatus.Explanation}.");
                        return (Data.Scenes, Data.Status);
                    }
                }

                Data.CleanScenes(scenes.Select(s => s.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading scenes: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Scenes, Data.Status);
        }

        /// <summary>
        /// Reads all schedules. Note this only updates link and name.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ScheduleData> Data, DataStatus Status)> DataReadSchedulesAsync()
        {
            var (schedules, status) = await ReadSchedulesAsync();

            if (status.IsGood)
            {
                foreach (var schedule in schedules)
                {
                    Data.UpdateSchedules(schedule);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading schedules: {status.Explanation}.");
            }

            Data.Status = status;
            return (schedules, Data.Status);
        }

        /// <summary>
        /// Reads all schedules. Note this deletes missing schedules.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ScheduleInfo> Data, DataStatus Status)> DataReadSchedulesFullAsync()
        {
            var (schedules, status) = await ReadSchedulesAsync();

            if (status.IsGood)
            {
                foreach (var schedule in schedules)
                {
                    var (fullschedule, fullstatus) = await ReadScheduleAsync(schedule.Uuid.Value, ScheduleFlags.ALL);

                    if (status.IsGood)
                    {
                        Data.UpdateSchedules(schedule);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading schedules: {fullstatus.Explanation}.");
                        return (Data.Schedules, Data.Status);
                    }

                    Data.CleanSchedules(schedules.Select(s => s.Uuid.Value).ToList());
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading schedules: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Schedules, Data.Status);
        }

        /// <summary>
        /// Reads all thermostats. Note this only updates link, name, and tags.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ThermostatData> Data, DataStatus Status)> DataReadThermostatsAsync()
        {
            var (thermostats, status) = await ReadThermostatsAsync();

            if (status.IsGood)
            {
                foreach (var thermostat in thermostats)
                {
                    Data.UpdateThermostats(thermostat);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading thermostats: {status.Explanation}.");
            }

            Data.Status = status;
            return (thermostats, Data.Status);
        }

        /// <summary>
        /// Reads all thermostats. Note this deletes missing thermostats.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<ThermostatInfo> Data, DataStatus Status)> DataReadThermostatsFullAsync()
        {
            var (thermostats, status) = await ReadThermostatsAsync();

            if (status.IsGood)
            {
                foreach (var thermostat in thermostats)
                {
                    var (fullthermostat, fullstatus) = await ReadThermostatAsync(thermostat.Uuid.Value, ThermostatFlags.ALL);

                    if (fullstatus.IsGood)
                    {
                        Data.UpdateThermostats(fullthermostat);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading thermostats: {fullstatus.Explanation}.");
                        return (Data.Thermostats, Data.Status);
                    }
                }

                Data.CleanThermostats(thermostats.Select(a => a.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading thermostats: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Thermostats, Data.Status);
        }

        /// <summary>
        /// Reads all virtual endpoints.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<VirtualEndpointData> Data, DataStatus Status)> DataReadVirtualEndpointsAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading virtual endpoints not supported with local web service.");
                return (new List<VirtualEndpointData> { }, DataValue.BadNotSupported);
            }

            var (endpoints, status) = await ReadVirtualEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    Data.UpdateVirtualEndpoints(endpoint);
                }
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading virtual endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (endpoints, Data.Status);
        }

        /// <summary>
        /// Reads all virtual endpoints. Note this deletes missing virtual endpoints.
        /// </summary>
        /// <returns></returns>
        public async Task<(List<VirtualEndpointInfo> Data, DataStatus Status)> DataReadVirtualEndpointsFullAsync()
        {
            if (IsLocal)
            {
                _logger?.LogWarning($"Reading virtual endpoints (full) not supported with local web service.");
                return (new List<VirtualEndpointInfo> { }, DataValue.BadNotSupported);
            }

            var (endpoints, status) = await ReadVirtualEndpointsAsync();

            if (status.IsGood)
            {
                foreach (var endpoint in endpoints)
                {
                    var (fullendpoint, fullstatus) = await ReadVirtualEndpointAsync(endpoint.Uuid.Value, VirtualEndpointFlags.ALL);

                    if (fullstatus.IsGood)
                    {
                        Data.UpdateVirtualEndpoints(fullendpoint);
                    }
                    else
                    {
                        _logger?.LogError($"Error code {fullstatus.Code} in reading virtual endpoints: {fullstatus.Explanation}.");
                        return (Data.VirtualEndpoints, Data.Status);
                    }
                }

                Data.CleanVirtualEndpoints(endpoints.Select(e => e.Uuid.Value).ToList());
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading virtual endpoints: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.VirtualEndpoints, Data.Status);
        }

        /// <summary>
        /// Reads the Zipato box data.
        /// </summary>
        /// <returns></returns>
        public async Task<(BoxInfo Data, DataStatus Status)> DataReadBoxAsync()
        {
            var (box, status) = await ReadBoxAsync();

            if (status.IsGood)
            {
                Data.Box = box;
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading box: {status.Explanation}.");
            }

            Data.Status = status;
            return (Data.Box, Data.Status);
        }

        #endregion
    }
}
