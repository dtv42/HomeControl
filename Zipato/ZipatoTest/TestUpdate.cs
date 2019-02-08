// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestUpdate.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoTest
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using DataValueLib;
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
    public class TestUpdate : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestUpdate"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestUpdate(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        #endregion

        // Testing PUT /alarm/config - change alarm configuration.

        // Testing PUT /alarm/monitors/{monitor}/config - update monitor configuration.
        // Testing PUT /alarm/partitions/{partition}/attributes/{attribute}/value - set attribute value.
        // Testing PUT /alarm/partitions/{partition}/config - update partition configuration.
        // Testing PUT /alarm/partitions/{partition}/zones/{zone}/config - modify zone.

        // Testing PUT /attributes/{uuid}/config - modify an attribute.
        // Testing PUT /attributes/{uuid}/icon - set icons.
        // Testing PUT /attributes/{uuid}/parent - modify an attribute.
        // Testing PUT /attributes/{uuid}/value - set attribute value.
        [Theory]
        [InlineData("5802bf1a-ed76-406b-ba48-d5a618a8d0a9", "true")]
        public async Task TestSetValue(string uuid, string value)
        {
            var status = await _zipato.UpdateAttributeValueAsync(new Guid(uuid), value);
            Assert.Equal(DataValue.Good, status);
        }

        // Testing PUT /bindings/{uuid}/config - modify a binding.

        // Testing PUT /box - change config of the current Zipato.
        // Testing PUT /box/config - change config of the current Zipato.
        // Testing PUT /box/config/{serial} - change config of another Zipato.

        // Testing PUT /cameras/{uuid}/config - modify a camera.

        // Testing PUT /cluster/{cluster}/members/{member} - update member of the cluster.

        // Testing PUT /clusterEndpoints/{uuid}/config - modify a clusterEndpoint.
        // Testing PUT /clusterEndpoints/{uuid}/icon - set icons.

        // Testing PUT /contacts/{id} - update the contact.

        // Testing PUT /dealer/{serial} - assign dealer to the box.

        // Testing PUT /devices/{uuid}/config - modify a device.
        // Testing PUT /devices/{uuid}/icon - set icons.

        // Testing PUT /endpoints/{uuid}/config - modify a endpoint.
        // Testing PUT /endpoints/{uuid}/icon - set icons.

        // Testing PUT /groups/{uuid}/attribute/{attributeUuid}/value - set attribute value.
        // Testing PUT /groups/{uuid}/attributeId/{attributeId}/value - set attribute value.
        // Testing PUT /groups/{uuid}/config modify a group.

        // Testing PUT /networks/{uuid}/config - modify a network.

        // Testing PUT /rooms/{room} - update the the room metadata.

        // Testing PUT /rules/{id} - modify rule.
        // Testing PUT /rules/{id}/code - modify code.

        // Testing PUT /scenes/{uuid} - modify a scene.

        // Testing PUT /schedules/{uuid}/config - modify a schedule.

        // Testing PUT /sip/contacts - updates SIP contact data on SIP Server.
        // Testing PUT /sip/contacts/share - updates SIP contact data with shared data on SIP Server.
        // Testing PUT /sip/devices - updates SIP device data on SIP Server.
        // Testing PUT /sip/devices/share - updates SIP device data with shared data on SIP Server.
        // Testing PUT /sip/server - updates SIP Server data.
        // Testing PUT /sip/server/additional - updates additional SIP Server data.
        // Testing PUT /sip/server/additional/contacts - updates additional SIP Server Contact.

        // Testing PUT /subscriptions/{name} - update subscription.
        // Testing PUT /subscriptions/{name}/attributes - add attributes to the subscription.

        // Testing PUT /thermostats/{uuid}/config - update thermostat configuration.
        // Testing PUT /thermostats/{uuid}/config/{operation} - update configuration of the thermostat actuator.
        // Testing PUT /thermostats/{uuid}/config/{operation}/inputs/{endpoint} - add an input to the thermostat.
        // Testing PUT /thermostats/{uuid}/config/{operation}/meters/{endpoint} - add a thermometer to the thermostat.
        // Testing PUT /thermostats/{uuid}/config/{operation}/outputs/{endpoint} - add an actuator to the thermostat.
        // Testing PUT /thermostats/{uuid}/endpoints/{endpoint}/values/{attribute} - set value.

        // Testing PUT /users/member - create member user.
        // Testing PUT /users/{id} - modify user.
        // Testing PUT /users/{id}/modify - enable or disable user.

        // Testing PUT /virtualEndpoints/{uuid}/config - modify a virtual endpoint.

    }
}
