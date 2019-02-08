// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDelete.cs" company="DTV-Online">
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
    public class TestDelete : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDelete"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestDelete(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        #endregion

        // Testing DELETE /alarm/monitors/{monitor} - delete a monitor.
        // Testing DELETE /alarm/partitions/{partition} - delete a partition.
        // Testing DELETE /alarm/partitions/{partition}/zones/{zone} - delete a zone.

        // Testing DELETE /attributes/{uuid}/parent - delete the parent attribute of an attribute.

        // Testing DELETE /cameras/{uuid} - delete a camera.

        // Testing DELETE /cluster/{cluster}/members/{member} - leave cluster.

        // Testing DELETE /clusterEndpoints/{uuid} - delete a clusterEndpoint.

        // Testing DELETE /contacts/{id} - delete a contact.

        // Testing DELETE /dealer/{serial} - remove dealer of a box.

        // Testing DELETE /devices/{uuid} - delete a device.

        // Testing DELETE /endpoints/{uuid} - delete a endpoint.

        // Testing DELETE /groups/{uuid} - delete a group.

        // Testing DELETE /meteo/{uuid}

        // Testing DELETE /networks/{uuid} - delete a network.
        // Testing DELETE /networks/{uuid}/discovery/{discovery} - stop device discovery.
        // Testing DELETE /networks/{uuid}/flush - delete all unused devices from a network.

        // Testing DELETE /rooms/{room} - delete the room.

        // Testing DELETE /rules/{id} - delete a schedule.

        // Testing DELETE /scenes/{uuid} - delete a scene.

        // Testing DELETE /schedules/{uuid} - delete a schedule.

        // Testing DELETE /sip/contacts - removes SIP contact from SIP Server.
        // Testing DELETE /sip/devices - removes SIP device from SIP Server.
        // Testing DELETE /sip/server - removes SIP Server.
        // Testing DELETE /sip/server/additional - removes additional SIP Server.
        // Testing DELETE /sip/server/additional/contacts - removes additional SIP Server Contact.

        // Testing DELETE /snapshot/{serial}/{uuid} - delete the snapshot.

        // Testing DELETE /subscriptions/{name} - delete the subscription.
        // Testing DELETE /subscriptions/{name}/attributes - delete attributes from the subscription.

        // Testing DELETE /sv/{id} - delete a file.

        // Testing DELETE /thermostats/{uuid} - delete a thermostat.
        // Testing DELETE /thermostats/{uuid}/config/{operation}/inputs/{endpoint} - remove an input from the thermostat.
        // Testing DELETE /thermostats/{uuid}/config/{operation}/meters/{endpoint} - remove a thermometer from the thermostat.
        // Testing DELETE /thermostats/{uuid}/config/{operation}/outputs/{endpoint} - remove an actuator from the thermostat.

        // Testing DELETE /users/{id} - delete user.

        // Testing DELETE /virtualEndpoints/{uuid} - delete a virtual endpoint.
    }
}
