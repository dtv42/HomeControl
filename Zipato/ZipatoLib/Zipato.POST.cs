// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.POST.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DataValueLib;
    using ZipatoLib.Models.Data;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Create Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Public Create Methods

        // Implements POST /alarm/monitors - create a meter monitor.
        public async Task<DataStatus>
            CreateAlarmMonitorAsync(MonitorData data) => await CreateDataAsync("alarm/monitors", data);

        // Implements POST /alarm/partitions - create a partition.
        // Implements POST /alarm/partitions/{partition}/events/{event} - acknowledge an alarm event with message in text/plain
        // Implements POST /alarm/partitions/{partition}/refresh - refresh the state of the partition.
        // Implements POST /alarm/partitions/{partition}/setMode - arm or disarm a partition.
        // Implements POST /alarm/partitions/{partition}/zones - create a zone from clusterEndpoint
        // Implements POST /alarm/partitions/{partition}/zones - create a zone from attribute

        // Implements POST /bindings/ - create a binding.

        // Implements POST /box/register - register a Zipato.
        // Implements POST /box/unregister - unregister a Zipato.
        // Implements POST /box/wipeAndUnregister - reset to factory defaults and unregister a Zipato.

        // Implements POST /cameras - add a camera on discovered IP device.
        // Implements POST /cameras - add a camera with static IP address.
        // Implements POST /cameras/{uuid}/configure -

        // Implements POST /cluster - create a cluster.

        // Implements POST /clusterEndpoints/{uuid}/actions/{action} - execute action on cluster endpoint.
        // Implements POST /clusterEndpoints/{uuid}/applyAttributes - (re)apply attributes.
        // Implements POST /clusterEndpoints/{uuid}/icon - set icons.

        // Implements POST /contacts - create a new contact.

        // Implements POST /devices/{uuid}/ - create an endpoint.
        // Implements POST /devices/{uuid}/actions/{action} - execute action on device.
        // Implements POST /devices/{uuid}/icon - set icons.
        // Implements POST /devices/{uuid}/reapply - (re)apply device descroptors.
        // Implements POST /devices/{uuid}/rename - rename device and its children by expressions from descriptors.

        // Implements POST /endpoints/{uuid}/ - create a clusterEndpoint.
        // Implements POST /endpoints/{uuid}/actions/{action} - execute action on an endpoint.
        // Implements POST /endpoints/{uuid}/icon - set icons.

        // Implements POST /firmware/upgrade - upgrade
        // Implements POST /firmware/upgrade/{serial} - upgrade.

        // Implements POST /groups/ - create a group.
        // Implements POST /groups/flush - delete all groups that have no endpoints in them.

        // Implements POST /meteo

        // Implements POST /networks/ - create a network.
        // Implements POST /networks/{uuid} - create a device.
        // Implements POST /networks/{uuid}/actions/{action} - execute action on a network.
        // Implements POST /networks/{uuid}/discovery/ - start device discovery.

        // Implements POST /rooms/ - create a room.
        // Implements POST /rooms/flush - delete all rooms that have no devices or endpoints in them.

        // Implements POST /rules/ - create a rule.

        // Implements POST /scenes/ - create a scene.
        // Implements POST /scenes/flush - delete all scenes with empty settings.

        // Implements POST /schedules/ - create a schedule.

        // Implements POST /sip/contacts - creates new SIP contact on SIP Server.
        // Implements POST /sip/devices - creates new SIP device on SIP Server.
        // Implements POST /sip/server - creates new SIP Server.
        // Implements POST /sip/server/additional - creates additional SIP Server for connection to other Zipato SIP Servers.
        // Implements POST /sip/server/additional/contacts - creates SIP contact which connects two SIP Servers on Zipato network.

        // Implements POST /snapshot/factoryDefault/restore - restore to factory default.
        // Implements POST /snapshot/{serial} - create the snapshot for the Zipato.
        // Implements POST /snapshot/{serial}/{uuid}/restore - restore the snapshot.

        // Implements POST /subscriptions - create a subscription.
        // Implements POST /subscriptions/{name}/test

        // Implements POST /sv/deleteBatch - delete multiple files.
        public async Task<DataStatus>
        DeleteFilesBatchAsync(List<string> data) => await CreateDataAsync("sv/deleteBatch", data);

        // Implements POST /thermostats/ - create a thermostat.
        // Implements POST /thermostats/wrap/{device} - create a thermostat from a device based on tags on endpoints of the device.
        // Implements POST /thermostats/{uuid}/wrap/{device} - add a device to an existing thermostat based on tags on endpoints of the device.

        // Implements POST /user/changePassword - change password for the user logged in.
        // Implements POST /user/register- register a new user.
        // Implements POST /user/setPassword - change password for the user logged in.
        // Implements POST /users/ - create user.

        // Implements POST /virtualEndpoints - create a virtual endpoint.

        // Implements POST /wallet/transfer/box - transfer credits to controller's wallet.
        // Implements POST /wallet/transfer/user - transfer credits to another user's wallet.

        // Implements POST /wizard/tx/{transaction}/next - next stetp, post form fields if present.

        #endregion Public Create Methods
    }
}
