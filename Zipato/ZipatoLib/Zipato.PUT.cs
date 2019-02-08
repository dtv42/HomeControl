// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.PUT.cs" company="DTV-Online">
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
    using ZipatoLib.Models.Config;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Update Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Public Update Methods

        // Implements PUT /alarm/config - change alarm configuration.
        public async Task<DataStatus>
            UpdateAlarmConfigAsync(AlarmConfig data) => await UpdateDataAsync("alarm/config", data);

        // Implements PUT /alarm/monitors/{monitor}/config - update monitor configuration.
        // Implements PUT /alarm/partitions/{partition}/attributes/{attribute}/value - set attribute value.
        // Implements PUT /alarm/partitions/{partition}/config - update partition configuration.
        // Implements PUT /alarm/partitions/{partition}/zones/{zone}/config - modify zone.

        // Implements PUT /attributes/{uuid}/config - modify an attribute.
        // Implements PUT /attributes/{uuid}/icon - set icons.
        // Implements PUT /attributes/{uuid}/parent - modify an attribute.
        // Implements PUT /attributes/{uuid}/value - set attribute value.
        public async Task<DataStatus>
            UpdateAttributeValueAsync(Guid uuid, string data) => await UpdateDataAsync($"attributes/{uuid.ToString()}/value", data);

        // Implements PUT /bindings/{uuid}/config - modify a binding.

        // Implements PUT /box - change config of the current Zipato.
        // Implements PUT /box/config - change config of the current Zipato.
        // Implements PUT /box/config/{serial} - change config of another Zipato.

        // Implements PUT /cameras/{uuid}/config - modify a camera.

        // Implements PUT /cluster/{cluster}/members/{member} - update member of the cluster.

        // Implements PUT /clusterEndpoints/{uuid}/config - modify a clusterEndpoint.
        // Implements PUT /clusterEndpoints/{uuid}/icon - set icons.

        // Implements PUT /contacts/{id} - update the contact.

        // Implements PUT /dealer/{serial} - assign dealer to the box.

        // Implements PUT /devices/{uuid}/config - modify a device.
        // Implements PUT /devices/{uuid}/icon - set icons.

        // Implements PUT /endpoints/{uuid}/config - modify a endpoint.
        // Implements PUT /endpoints/{uuid}/icon - set icons.

        // Implements PUT /groups/{uuid}/attribute/{attributeUuid}/value - set attribute value.
        // Implements PUT /groups/{uuid}/attributeId/{attributeId}/value - set attribute value.
        // Implements PUT /groups/{uuid}/config modify a group.

        // Implements PUT /networks/{uuid}/config - modify a network.

        // Implements PUT /rooms/{room} - update the the room metadata.

        // Implements PUT /rules/{id} - modify rule.
        // Implements PUT /rules/{id}/code - modify code.

        // Implements PUT /scenes/{uuid} - modify a scene.

        // Implements PUT /schedules/{uuid}/config - modify a schedule.

        // Implements PUT /sip/contacts - updates SIP contact data on SIP Server.
        // Implements PUT /sip/contacts/share - updates SIP contact data with shared data on SIP Server.
        // Implements PUT /sip/devices - updates SIP device data on SIP Server.
        // Implements PUT /sip/devices/share - updates SIP device data with shared data on SIP Server.
        // Implements PUT /sip/server - updates SIP Server data.
        // Implements PUT /sip/server/additional - updates additional SIP Server data.
        // Implements PUT /sip/server/additional/contacts - updates additional SIP Server Contact.

        // Implements PUT /subscriptions/{name} - update subscription.
        // Implements PUT /subscriptions/{name}/attributes - add attributes to the subscription.

        // Implements PUT /thermostats/{uuid}/config - update thermostat configuration.
        // Implements PUT /thermostats/{uuid}/config/{operation} - update configuration of the thermostat actuator.
        // Implements PUT /thermostats/{uuid}/config/{operation}/inputs/{endpoint} - add an input to the thermostat.
        // Implements PUT /thermostats/{uuid}/config/{operation}/meters/{endpoint} - add a thermometer to the thermostat.
        // Implements PUT /thermostats/{uuid}/config/{operation}/outputs/{endpoint} - add an actuator to the thermostat.
        // Implements PUT /thermostats/{uuid}/endpoints/{endpoint}/values/{attribute} - set value.

        // Implements PUT /users/member - create member user.
        // Implements PUT /users/{id} - modify user.
        // Implements PUT /users/{id}/modify - enable or disable user.

        // Implements PUT /virtualEndpoints/{uuid}/config - modify a virtual endpoint.

        #endregion Public Update Methods
    }
}
