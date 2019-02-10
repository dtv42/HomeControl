// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.Data.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using DataValueLib;

    using ZipatoLib.Models.Dtos;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Flags;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Read Data Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Read Single Data Methods

        /// <summary>
        /// Reads single attribute.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(AttributeInfo Data, DataStatus Status)>DataReadAttributeAsync(Guid uuid)
        {
            var (attribute, status) = await ReadAttributeAsync(uuid, AttributeFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateAttributes(attribute);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading attribute: {status.Explanation}.");
            }

            Data.Status = status;
            return (attribute, Data.Status);
        }

        /// <summary>
        /// Reads single binding.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(BindingInfo Data, DataStatus Status)> DataReadBindingAsync(Guid uuid)
        {
            var (binding, status) = await ReadBindingAsync(uuid, BindingFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateBindings(binding);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading binding: {status.Explanation}.");
            }

            Data.Status = status;
            return (binding, Data.Status);
        }

        /// <summary>
        /// Reads single brand.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<(BrandInfo Data, DataStatus Status)> DataReadBrandAsync(string name)
        {
            var (brand, status) = await ReadBrandAsync(name);

            if (status.IsGood)
            {
                Data.UpdateBrands(brand);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading brand: {status.Explanation}.");
            }

            Data.Status = status;
            return (brand, Data.Status);
        }

        /// <summary>
        /// Reads single camera.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(CameraInfo Data, DataStatus Status)> DataReadCameraAsync(Guid uuid)
        {
            var (camera, status) = await ReadCameraAsync(uuid, CameraFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateCameras(camera);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading camera: {status.Explanation}.");
            }

            Data.Status = status;
            return (camera, Data.Status);
        }

        /// <summary>
        /// Reads single cluster.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(ClusterInfo Data, DataStatus Status)> DataReadClusterAsync(int id)
        {
            var (cluster, status) = await ReadClusterAsync(id);

            if (status.IsGood)
            {
                Data.UpdateClusters(cluster);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading cluster: {status.Explanation}.");
            }

            Data.Status = status;
            return (cluster, Data.Status);
        }

        /// <summary>
        /// Reads single cluster endpoint.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(ClusterEndpointInfo Data, DataStatus Status)> DataReadClusterEndpointAsync(Guid uuid)
        {
            var (endpoint, status) = await ReadClusterEndpointAsync(uuid, ClusterEndpointFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateClusterEndpoints(endpoint);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading cluster endpoint: {status.Explanation}.");
            }

            Data.Status = status;
            return (endpoint, Data.Status);
        }

        /// <summary>
        /// Reads single contact.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(ContactInfo Data, DataStatus Status)> DataReadContactAsync(int id)
        {
            var (contact, status) = await ReadContactAsync(id);

            if (status.IsGood)
            {
                Data.UpdateContacts(contact);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading contact: {status.Explanation}.");
            }

            Data.Status = status;
            return (contact, Data.Status);
        }

        /// <summary>
        /// Reads single device.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(DeviceInfo Data, DataStatus Status)> DataReadDeviceAsync(Guid uuid)
        {
            var (device, status) = await ReadDeviceAsync(uuid, DeviceFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateDevices(device);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading device: {status.Explanation}.");
            }

            Data.Status = status;
            return (device, Data.Status);
        }

        /// <summary>
        /// Reads single endpoint.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(EndpointInfo Data, DataStatus Status)> DataReadEndpointAsync(Guid uuid)
        {
            var (endpoint, status) = await ReadEndpointAsync(uuid, EndpointFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateEndpoints(endpoint);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading endpoint: {status.Explanation}.");
            }

            Data.Status = status;
            return (endpoint, Data.Status);
        }

        /// <summary>
        /// Reads single group.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(GroupInfo Data, DataStatus Status)> DataReadGroupAsync(Guid uuid)
        {
            var (group, status) = await ReadGroupAsync(uuid, GroupFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateGroups(group);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading group: {status.Explanation}.");
            }

            Data.Status = status;
            return (group, Data.Status);
        }

        /// <summary>
        /// Reads single network.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(NetworkInfo Data, DataStatus Status)> DataReadNetworkAsync(Guid uuid)
        {
            var (network, status) = await ReadNetworkAsync(uuid, NetworkFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateNetworks(network);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading network: {status.Explanation}.");
            }

            Data.Status = status;
            return (network, Data.Status);
        }

        /// <summary>
        /// Reads single network tree.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(NetworkTree Data, DataStatus Status)> DataReadNetworkTreeAsync(Guid uuid)
        {
            var (tree, status) = await ReadNetworkTreeAsync(uuid);

            if (status.IsGood)
            {
                Data.UpdateNetworkTrees(tree);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading network tree: {status.Explanation}.");
            }

            Data.Status = status;
            return (tree, Data.Status);
        }

        /// <summary>
        /// Reads single rule.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(RuleInfo Data, DataStatus Status)> DataReadRuleAsync(int id)
        {
            var (rule, status) = await ReadRuleAsync(id);

            if (status.IsGood)
            {
                Data.UpdateRules(rule);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading rule: {status.Explanation}.");
            }

            Data.Status = status;
            return (rule, Data.Status);
        }

        /// <summary>
        /// Reads single scene.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(SceneInfo Data, DataStatus Status)> DataReadSceneAsync(Guid uuid)
        {
            var (scene, status) = await ReadSceneAsync(uuid);

            if (status.IsGood)
            {
                Data.UpdateScenes(scene);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading scene: {status.Explanation}.");
            }

            Data.Status = status;
            return (scene, Data.Status);
        }

        /// <summary>
        /// Reads single schedule.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(ScheduleInfo Data, DataStatus Status)> DataReadScheduleAsync(Guid uuid)
        {
            var (schedule, status) = await ReadScheduleAsync(uuid, ScheduleFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateSchedules(schedule);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading schedule: {status.Explanation}.");
            }

            Data.Status = status;
            return (schedule, Data.Status);
        }

        /// <summary>
        /// Reads single saved file.
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(FileData Data, DataStatus Status)> DataReadSavedFileAsync(Guid uuid, string id)
        {
            var (file, status) = await ReadSavedFileAsync(id);

            if (status.IsGood)
            {
                Data.UpdateSavedFiles(uuid, file);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading file data: {status.Explanation}.");
            }

            Data.Status = status;
            return (file, Data.Status);
        }

        /// <summary>
        /// Reads single thermostats.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(ThermostatInfo Data, DataStatus Status)> DataReadThermostatAsync(Guid uuid)
        {
            var (thermostat, status) = await ReadThermostatAsync(uuid, ThermostatFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateThermostats(thermostat);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading thermostat: {status.Explanation}.");
            }

            Data.Status = status;
            return (thermostat, Data.Status);
        }

        /// <summary>
        /// Reads single virtual endpoint.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(VirtualEndpointInfo Data, DataStatus Status)> DataReadVirtualEndpointAsync(Guid uuid)
        {
            var (endpoint, status) = await ReadVirtualEndpointAsync(uuid, VirtualEndpointFlags.ALL);

            if (status.IsGood)
            {
                Data.UpdateVirtualEndpoints(endpoint);
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading virtual endpoint: {status.Explanation}.");
            }

            Data.Status = status;
            return (endpoint, Data.Status);
        }

        /// <summary>
        /// Returns the Attribute value reading the new value.
        /// Note that the parameter is an attribute UUID.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<(AttributeValueDto Data, DataStatus Status)> DataReadValueAsync(Guid uuid)
        {
            var (value, status) = await ReadAttributeValueAsync(uuid);

            if (status.IsGood)
            {
                Data.UpdateValues(new ValueData() { Uuid = uuid, Value = value });
            }
            else
            {
                _logger?.LogError($"Error code {status.Code} in reading value: {status.Explanation}.");
            }

            Data.Status = status;
            return (value, Data.Status);
        }

        /// <summary>
        /// Returns the Attribute value for an endpoint reading the new value.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<(AttributeValueDto Data, DataStatus Status)> DataReadValueAsync(Guid endpoint, string name)
        {
            var attribute = GetAttributeByName(endpoint, name);

            if ((attribute != null) && attribute.Uuid.HasValue)
            {
                return await DataReadValueAsync(attribute.Uuid.Value);
            }
            else
            {
                _logger?.LogError($"Attribute with name '{name}' of endpoint '{endpoint}' not found.");
            }

            return (null, DataValue.BadNotFound);
        }

        #endregion Read Data Methods
    }
}
