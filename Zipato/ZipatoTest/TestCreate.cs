// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCreate.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoTest
{
    #region Using Directives

    using System.Globalization;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using ZipatoLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestCreate : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCreate"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestCreate(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        // Testing POST /alarm/monitors - create a meter monitor.

        // Testing POST /alarm/partitions - create a partition.
        // Testing POST /alarm/partitions/{partition}/events/{event} - acknowledge an alarm event with message in text/plain
        // Testing POST /alarm/partitions/{partition}/refresh - refresh the state of the partition.
        // Testing POST /alarm/partitions/{partition}/setMode - arm or disarm a partition.
        // Testing POST /alarm/partitions/{partition}/zones - create a zone from clusterEndpoint
        // Testing POST /alarm/partitions/{partition}/zones - create a zone from attribute

        // Testing POST /bindings/ - create a binding.

        // Testing POST /box/register - register a Zipato.
        // Testing POST /box/unregister - unregister a Zipato.
        // Testing POST /box/wipeAndUnregister - reset to factory defaults and unregister a Zipato.

        // Testing POST /cameras - add a camera on discovered IP device.
        // Testing POST /cameras - add a camera with static IP address.
        // Testing POST /cameras/{uuid}/configure -

        // Testing POST /cluster - create a cluster.

        // Testing POST /clusterEndpoints/{uuid}/actions/{action} - execute action on cluster endpoint.
        // Testing POST /clusterEndpoints/{uuid}/applyAttributes - (re)apply attributes.
        // Testing POST /clusterEndpoints/{uuid}/icon - set icons.

        // Testing POST /contacts - create a new contact.

        // Testing POST /devices/{uuid}/ - create an endpoint.
        // Testing POST /devices/{uuid}/actions/{action} - execute action on device.
        // Testing POST /devices/{uuid}/icon - set icons.
        // Testing POST /devices/{uuid}/reapply - (re)apply device descroptors.
        // Testing POST /devices/{uuid}/rename - rename device and its children by expressions from descriptors.

        // Testing POST /endpoints/{uuid}/ - create a clusterEndpoint.
        // Testing POST /endpoints/{uuid}/actions/{action} - execute action on an endpoint.
        // Testing POST /endpoints/{uuid}/icon - set icons.

        // Testing POST /firmware/upgrade - upgrade
        // Testing POST /firmware/upgrade/{serial} - upgrade.

        // Testing POST /groups/ - create a group.
        // Testing POST /groups/flush - delete all groups that have no endpoints in them.

        // Testing POST /meteo

        // Testing POST /networks/ - create a network.
        // Testing POST /networks/{uuid} - create a device.
        // Testing POST /networks/{uuid}/actions/{action} - execute action on a network.
        // Testing POST /networks/{uuid}/discovery/ - start device discovery.

        // Testing POST /rooms/ - create a room.
        // Testing POST /rooms/flush - delete all rooms that have no devices or endpoints in them.

        // Testing POST /rules/ - create a rule.

        // Testing POST /scenes/ - create a scene.
        // Testing POST /scenes/flush - delete all scenes with empty settings.

        // Testing POST /schedules/ - create a schedule.

        // Testing POST /sip/contacts - creates new SIP contact on SIP Server.
        // Testing POST /sip/devices - creates new SIP device on SIP Server.
        // Testing POST /sip/server - creates new SIP Server.
        // Testing POST /sip/server/additional - creates additional SIP Server for connection to other Zipato SIP Servers.
        // Testing POST /sip/server/additional/contacts - creates SIP contact which connects two SIP Servers on Zipato network.

        // Testing POST /snapshot/factoryDefault/restore - restore to factory default.
        // Testing POST /snapshot/{serial} - create the snapshot for the Zipato.
        // Testing POST /snapshot/{serial}/{uuid}/restore - restore the snapshot.

        // Testing POST /subscriptions - create a subscription.
        // Testing POST /subscriptions/{name}/test

        // Testing POST /sv/deleteBatch - delete multiple files.

        // Testing POST /thermostats/ - create a thermostat.
        // Testing POST /thermostats/wrap/{device} - create a thermostat from a device based on tags on endpoints of the device.
        // Testing POST /thermostats/{uuid}/wrap/{device} - add a device to an existing thermostat based on tags on endpoints of the device.

        // Testing POST /user/changePassword - change password for the user logged in.
        // Testing POST /user/register- register a new user.
        // Testing POST /user/setPassword - change password for the user logged in.
        // Testing POST /users/ - create user.

        // Testing POST /virtualEndpoints - create a virtual endpoint.

        // Testing POST /wallet/transfer/box - transfer credits to controller's wallet.
        // Testing POST /wallet/transfer/user - transfer credits to another user's wallet.

        // Testing POST /wizard/tx/{transaction}/next - next stetp, post form fields if present.

    }
}
