// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestData.cs" company="DTV-Online">
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
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using DataValueLib;
    using ZipatoLib;
    using ZipatoLib.Models.Data.Color;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestData : IClassFixture<ZipatoFixture>
    {
        #region Private Data Members

        private readonly ILogger<Zipato> _logger;
        private readonly IZipato _zipato;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestData(ZipatoFixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<Zipato>();

            _zipato = fixture.Zipato;
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestReadData()
        {
            await _zipato.ReadAllAsync();

            Assert.Equal(DataValue.Good, _zipato.Data.Status);
            Assert.Equal("alarm", _zipato.Data.Alarm.Link);
            Assert.NotEmpty(_zipato.Data.Attributes);
            Assert.NotEmpty(_zipato.Data.Brands);
            Assert.NotEmpty(_zipato.Data.Cameras);
            Assert.NotEmpty(_zipato.Data.ClusterEndpoints);
            Assert.NotEmpty(_zipato.Data.Contacts);
            Assert.NotEmpty(_zipato.Data.Devices);
            Assert.NotEmpty(_zipato.Data.Endpoints);
            Assert.NotEmpty(_zipato.Data.Groups);
            Assert.NotEmpty(_zipato.Data.Networks);
            Assert.NotEmpty(_zipato.Data.NetworkTrees);
            Assert.NotEmpty(_zipato.Data.Rooms);
            Assert.NotEmpty(_zipato.Data.Scenes);
            Assert.NotEmpty(_zipato.Data.Schedules);
            Assert.NotEmpty(_zipato.Data.Thermostats);
            Assert.NotNull(_zipato.Data.Box);
            Assert.Equal("ZT7E104099D0D75BCC", _zipato.Data.Box.Serial);
            Assert.NotEmpty(_zipato.Data.Values);

            if (_zipato.IsLocal)
            {
                Assert.Empty(_zipato.Data.Announcements);
                Assert.Empty(_zipato.Data.Bindings);
                Assert.Empty(_zipato.Data.Clusters);
                Assert.Empty(_zipato.Data.Rules);
                Assert.Empty(_zipato.Data.VirtualEndpoints);
            }
            else
            {
                Assert.NotEmpty(_zipato.Data.Announcements);
                Assert.NotEmpty(_zipato.Data.Bindings);
                Assert.Empty(_zipato.Data.Clusters); // No clusters...
                Assert.NotEmpty(_zipato.Data.Rules);
                Assert.NotEmpty(_zipato.Data.VirtualEndpoints);
            }
        }

        [Fact]
        public async Task TestReadValues()
        {
            await _zipato.ReadAllValuesAsync();

            Assert.Equal(DataValue.Good, _zipato.Data.Status);
            Assert.NotEmpty(_zipato.Data.Values);
        }

        [Theory]
        [InlineData("7596cb26-3cdc-4f8b-b665-eaf5cc26ff66")]
        public async Task GetEndpointData(string endpoint)
        {
            await _zipato.DataReadEndpointsFullAsync();

            var data = _zipato.GetEndpoint(new Guid(endpoint));
            Assert.NotNull(data);
            Assert.Equal(endpoint, data.Uuid.ToString());
        }

        [Theory]
        [InlineData("673c052e-b2f1-4b65-bc6d-ce1225b4ca4b", "state")]
        [InlineData("673c052e-b2f1-4b65-bc6d-ce1225b4ca4b", "STATE")]
        [InlineData("6b958e64-1618-400d-ad76-24b8901e3725", "temperature")]
        [InlineData("6b958e64-1618-400d-ad76-24b8901e3725", "TEMPERATURE")]
        public async Task GetAttributeData(string endpoint, string attribute)
        {
            await _zipato.DataReadAttributesFullAsync();

            var data = _zipato.GetAttributeByName(new Guid(endpoint), attribute);
            Assert.NotNull(data);
            Assert.Equal(attribute, data.Name, ignoreCase: true);
        }

        [Theory]
        [InlineData("ed8e041b-06b4-4920-afe8-258411ec1712", "[0-9]+\\.[0-9]+")]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c", "(true|false)")]
        public async Task GetAttributeValuePattern(string uuid, string pattern)
        {
            await _zipato.DataReadValuesAsync();

            var data = _zipato.GetValue(new Guid(uuid));
            Assert.NotNull(data);
            Assert.Matches(new Regex(pattern, RegexOptions.IgnoreCase), data.Value.Value);
        }

        [Theory]
        [InlineData("673c052e-b2f1-4b65-bc6d-ce1225b4ca4b", "state", "(true|false)")]
        public async Task GetAttributeValueState(string endpoint, string attribute, string pattern)
        {
            await _zipato.DataReadAttributesFullAsync();

            var data = _zipato.GetValueByName(new Guid(endpoint), attribute);
            Assert.NotNull(data);
            Assert.Matches(new Regex(pattern, RegexOptions.IgnoreCase), data.Value.Value);
        }

        [Theory]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c")]
        [InlineData("ed8e041b-06b4-4920-afe8-258411ec1712")]
        public async Task GetValue(string uuid)
        {
            await _zipato.DataReadValuesAsync();

            var value = _zipato.GetValue(new Guid(uuid));
            Assert.NotNull(value);
        }

        [Theory]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c")]
        public async Task GetState(string uuid)
        {
            await _zipato.DataReadAttributesFullAsync();

            var value = _zipato.GetState(new Guid(uuid));
            Assert.NotNull(value);
        }

        [Theory]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c", "true")]
        public async Task SetValue(string uuid, string value)
        {
            var result = await _zipato.SetValueAsync(new Guid(uuid), value);
            Assert.True(result);
        }

        [Theory]
        [InlineData("932570ca-1061-4b92-8049-e81e3456f15c", true)]
        public async Task SetState(string uuid, bool value)
        {
            var result = await _zipato.SetStateAsync(new Guid(uuid), value);
            Assert.True(result);
        }

        [Theory]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "000000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "003F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "007F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00BF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00FF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00003F")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00007F")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "0000BF")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "0000FF")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "3F0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "7F0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "BF0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FF0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FF3F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FF7F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFBF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFF3F")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFF7F")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFFBF")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFFFF")]
        public async Task SetColorHexRGB(string uuid, string hex)
        {
            bool result = await _zipato.SetColorAsync(new Guid(uuid), hex);
            Assert.True(result);
        }

        [Theory]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00000000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "003F0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "007F0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00BF0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00FF0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00003F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "00007F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "0000BF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "0000FF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "3F000000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "7F000000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "BF000000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FF000000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FF3F0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FF7F0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFBF0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFF0000")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFF3F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFF7F00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFFBF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "FFFFFF00")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "0000003F")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "0000007F")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "000000BF")]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", "000000FF")]
        public async Task SetColorHexRGBW(string uuid, string hex)
        {
            bool result = await _zipato.SetColorAsync(new Guid(uuid), hex);
            Assert.True(result);
        }

        [Theory]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 0, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 63, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 127, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 191, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 255, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 0, 63)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 0, 127)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 0, 191)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 0, 0, 255)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 63, 0, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 127, 0, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 191, 0, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 0, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 63, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 127, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 191, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 255, 0)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 255, 63)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 255, 127)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 255, 191)]
        [InlineData("c59805ff-e7e9-4563-a43d-3eaf77716f18", 255, 255, 255)]
        public async Task SetColorRGB(string uuid, byte red, byte green, byte blue)
        {
            bool result = await _zipato.SetColorAsync(new Guid(uuid), new RGB() { R = red, G = green, B = blue });
            Assert.True(result);
        }

        [Theory]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 63, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 127, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 191, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 255, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 63, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 127, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 191, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 255, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 63, 0, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 127, 0, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 191, 0, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 0, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 63, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 127, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 191, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 255, 0, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 255, 63, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 255, 127, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 255, 191, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 255, 255, 255, 0)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 0, 63)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 0, 127)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 0, 191)]
        [InlineData("0fe978f9-2cec-482f-b819-eb916dbf963d", 0, 0, 0, 255)]
        public async Task SetColorRGBW(string uuid, byte red, byte green, byte blue, byte white)
        {
            bool result = await _zipato.SetColorAsync(new Guid(uuid), new RGBW() { R = red, G = green, B = blue, W = white });
            Assert.True(result);
        }

        [Theory]
        [InlineData("Zipato")]
        [InlineData("Zipato.Data")]
        [InlineData("Zipato.Data.Alarm")]
        [InlineData("Zipato.Data.Attributes")]
        [InlineData("Zipato.Data.Values")]
        [InlineData("Zipato.Data.Bindings")]
        [InlineData("Zipato.Data.Brands")]
        [InlineData("Zipato.Data.Cameras")]
        [InlineData("Zipato.Data.Clusters")]
        [InlineData("Zipato.Data.ClusterEndpoints")]
        [InlineData("Zipato.Data.Contacts")]
        [InlineData("Zipato.Data.Devices")]
        [InlineData("Zipato.Data.Endpoints")]
        [InlineData("Zipato.Data.Groups")]
        [InlineData("Zipato.Data.Networks")]
        [InlineData("Zipato.Data.NetworkTrees")]
        [InlineData("Zipato.Data.Rooms")]
        [InlineData("Zipato.Data.Rules")]
        [InlineData("Zipato.Data.Scenes")]
        [InlineData("Zipato.Data.Schedules")]
        [InlineData("Zipato.Data.Thermostats")]
        [InlineData("Zipato.Data.VirtualEndpoints")]
        [InlineData("Zipato.Data.Box")]
        [InlineData("Data")]
        [InlineData("Data.Alarm")]
        [InlineData("Data.Announcements")]
        [InlineData("Data.Attributes")]
        [InlineData("Data.Values")]
        [InlineData("Data.Bindings")]
        [InlineData("Data.Brands")]
        [InlineData("Data.Cameras")]
        [InlineData("Data.Clusters")]
        [InlineData("Data.ClusterEndpoints")]
        [InlineData("Data.Contacts")]
        [InlineData("Data.Devices")]
        [InlineData("Data.Endpoints")]
        [InlineData("Data.Groups")]
        [InlineData("Data.Networks")]
        [InlineData("Data.NetworkTrees")]
        [InlineData("Data.Rooms")]
        [InlineData("Data.Rules")]
        [InlineData("Data.Scenes")]
        [InlineData("Data.Schedules")]
        [InlineData("Data.Thermostats")]
        [InlineData("Data.VirtualEndpoints")]
        [InlineData("Data.Box")]
        public void TestProperty(string property)
        {
            Assert.True(Zipato.IsProperty(property));
            Assert.NotNull(_zipato.GetPropertyValue(property));
        }

        #endregion
    }
}
