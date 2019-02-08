// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.Delete.cs" company="DTV-Online">
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

    using DataValueLib;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Enums;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Delete Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Public Delete Methods

        // Implements DELETE /alarm/monitors/{monitor} - delete a monitor.
        public async Task<DataStatus>
            DeleteAlarmMonitorAsync(Guid monitor) =>
            await DeleteDataAsync($"alarm/monitors/{monitor}");

        // Implements DELETE /alarm/partitions/{partition} - delete a partition.
        public async Task<DataStatus>
            DeleteAlarmPartitionAsync(Guid partition) =>
            await DeleteDataAsync($"alarm/partitions/{partition}");

        // Implements DELETE /alarm/partitions/{partition}/zones/{zone} - delete a zone.
        public async Task<DataStatus>
            DeleteAlarmPartitionZoneAsync(Guid partition, string zone) =>
            await DeleteDataAsync($"alarm/partitions/{partition}/zones/{zone}");

        // Implements DELETE /attributes/{uuid}/parent - delete the parent attribute of an attribute.
        public async Task<DataStatus>
            DeleteAttributeParentAsync(Guid uuid) =>
            await DeleteDataAsync($"attributes/{uuid}/parent");

        // Implements DELETE /bindings/{uuid} - delete a binding.
        public async Task<DataStatus>
            DeleteBindingAsync(Guid uuid) =>
            await DeleteDataAsync($"bindings/{uuid}");

        // Implements DELETE /cameras/{uuid} - delete a camera.
        public async Task<DataStatus>
            DeleteCameraAsync(Guid uuid) =>
            await DeleteDataAsync($"cameras/{uuid}");

        // Implements DELETE /cluster/{cluster}/members/{member} - leave cluster.

        // Implements DELETE /clusterEndpoints/{uuid} - delete a clusterEndpoint.
        public async Task<DataStatus>
            DeleteClusterEndpointAsync(Guid uuid) =>
            await DeleteDataAsync($"clusterEndpoints/{uuid}");

        // Implements DELETE /contacts/{id} - delete a contact.
        public async Task<DataStatus>
            DeleteContactAsync(int id) =>
            await DeleteDataAsync($"contacts/{id}");

        // Implements DELETE /dealer/{serial} - remove dealer of a box.
        public async Task<DataStatus>
            DeleteDealerAsync(string serial) =>
            await DeleteDataAsync($"dealer/{serial}");

        // Implements DELETE /devices/{uuid} - delete a device.
        public async Task<DataStatus>
            DeleteDeviceAsync(Guid uuid) =>
            await DeleteDataAsync($"devices/{uuid}");

        // Implements DELETE /endpoints/{uuid} - delete a endpoint.
        public async Task<DataStatus>
            DeleteEndpointAsync(Guid uuid) =>
            await DeleteDataAsync($"endpoints/{uuid}");

        // Implements DELETE /groups/{uuid} - delete a group.
        public async Task<DataStatus>
            DeleteGroupAsync(Guid uuid) =>
            await DeleteDataAsync($"groups/{uuid}");

        // Implements DELETE /meteo/{uuid}
        public async Task<DataStatus>
            DeleteMeteoAsync(Guid uuid) =>
            await DeleteDataAsync($"meteo/{uuid}");

        // Implements DELETE /networks/{uuid} - delete a network.
        public async Task<DataStatus>
            DeleteNetworkAsync(Guid uuid) =>
            await DeleteDataAsync($"networks/{uuid}");

        // Implements DELETE /networks/{uuid}/discovery/{discovery} - stop device discovery.
        // Implements DELETE /networks/{uuid}/flush - delete all unused devices from a network.

        // Implements DELETE /rooms/{room} - delete the room.
        public async Task<DataStatus>
            DeleteRoomAsync(int room) =>
            await DeleteDataAsync($"rooms/{room}");

        // Implements DELETE /rules/{id} - delete a schedule.
        public async Task<DataStatus>
            DeleteRuleAsync(int id) =>
            await DeleteDataAsync($"rules/{id}");

        // Implements DELETE /scenes/{uuid} - delete a scene.
        public async Task<DataStatus>
            DeleteSceneAsync(Guid uuid) =>
            await DeleteDataAsync($"scenes/{uuid}");

        // Implements DELETE /schedules/{uuid} - delete a schedule.
        public async Task<DataStatus>
            DeleteScheduleAsync(Guid uuid) =>
            await DeleteDataAsync($"schedules/{uuid}");

        // Implements DELETE /sip/contacts - removes SIP contact from SIP Server.
        // Implements DELETE /sip/devices - removes SIP device from SIP Server.
        // Implements DELETE /sip/server - removes SIP Server.
        // Implements DELETE /sip/server/additional - removes additional SIP Server.
        // Implements DELETE /sip/server/additional/contacts - removes additional SIP Server Contact.

        // Implements DELETE /snapshot/{serial}/{uuid} - delete the snapshot.
        public async Task<DataStatus>
            DeleteSnapshotAsync(string serial, Guid uuid) =>
            await DeleteDataAsync($"snapshot/{serial}/{uuid}");

        // Implements DELETE /subscriptions/{name} - delete the subscription.
        // Implements DELETE /subscriptions/{name}/attributes - delete attributes from the subscription.

        // Implements DELETE /sv/{id} - delete a file.
        public async Task<DataStatus>
            DeleteSavedFileAsync(string id) =>
            await DeleteDataAsync($"sv/{id}");

        // Implements DELETE /thermostats/{uuid} - delete a thermostat.
        public async Task<DataStatus>
            DeleteThermostatAsync(Guid uuid) =>
            await DeleteDataAsync($"thermostats/{uuid}");

        // Implements DELETE /thermostats/{uuid}/config/{operation}/inputs/{endpoint} - remove an input from the thermostat.
        public async Task<DataStatus>
            DeleteThermostatInputAsync(Guid uuid, OperationTypes operation, Guid endpoint) =>
            await DeleteDataAsync($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}/inputs/{endpoint}");

        // Implements DELETE /thermostats/{uuid}/config/{operation}/meters/{endpoint} - remove a thermometer from the thermostat.
        public async Task<DataStatus>
            DeleteThermostatMeterAsync(Guid uuid, OperationTypes operation, Guid endpoint) =>
            await DeleteDataAsync($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}/meters/{endpoint}");

        // Implements DELETE /thermostats/{uuid}/config/{operation}/outputs/{endpoint} - remove an actuator from the thermostat.
        public async Task<DataStatus>
            DeleteThermostatOutputAsync(Guid uuid, OperationTypes operation, Guid endpoint) =>
            await DeleteDataAsync($"thermostats/{uuid}/config/{operation.ToString().ToUpper()}/outputs/{endpoint}");

        // Implements DELETE /users/{id} - delete user.
        public async Task<DataStatus>
            DeleteUserAsync(int id) =>
            await DeleteDataAsync($"users/{id}");

        // Implements DELETE /virtualEndpoints/{uuid} - delete a virtual endpoint.
        public async Task<DataStatus>
            DeleteVirtualEndpointAsync(Guid uuid) =>
            await DeleteDataAsync($"virtualEndpoints/{uuid}");

        #endregion Public Delete Methods
    }
}
