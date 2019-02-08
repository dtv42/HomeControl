// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
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
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using ZipatoLib;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestWeb
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HttpClient _client;
        private readonly bool _islocal;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWeb"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWeb(ITestOutputHelper output)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<Zipato>();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8007");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _islocal = JsonConvert.DeserializeObject<bool>(_client.GetAsync("api/zipato/islocal").Result.Content.ReadAsStringAsync().Result);
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task GetValues()
        {
            // Act
            var response = await _client.GetAsync("api/zipato/values");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            Assert.NotEmpty(responseString);
            var data = JsonConvert.DeserializeObject<List<ValueData>>(responseString);
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("ed8e041b-06b4-4920-afe8-258411ec1712")]
        [InlineData("7c4aecf1-5036-4963-a3a6-10c839e19cbb")]
        public async Task GetValue(string uuid)
        {
            // Act
            var response = await _client.GetAsync($"api/zipato/values/{uuid}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            Assert.NotEmpty(responseString);
            var data = JsonConvert.DeserializeObject<ValueData>(responseString);
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("5802bf1a-ed76-406b-ba48-d5a618a8d0a9", "true")]
        public async Task PutZipatoValue(string uuid, string value)
        {
            // Act
            var response = await _client.PutAsync($"api/zipato/values/{uuid}?value={value}", new StringContent(""));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("5802bf1a-ed76-406b-ba48-d5a618a8d0a9")]
        public async Task GetState(string uuid)
        {
            // Act
            var response = await _client.GetAsync($"api/zipato/states/{uuid}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            var data = JsonConvert.DeserializeObject<bool>(responseString);
        }

        [Theory]
        [InlineData("5802bf1a-ed76-406b-ba48-d5a618a8d0a9", true)]
        public async Task PutZipatoState(string uuid, bool value)
        {
            // Act
            var response = await _client.PutAsync($"api/zipato/states/{uuid}?value={value.ToString()}", new StringContent(""));

            // Assert
            response.EnsureSuccessStatusCode();
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
        public async Task PutColorsRGB(string uuid, string value)
        {
            // Act
            var response = await _client.PutAsync($"api/zipato/colors/rgb/{uuid}?value={value}", new StringContent(""));

            // Assert
            response.EnsureSuccessStatusCode();
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
        public async Task PutColorsRGBW(string uuid, string value)
        {
            // Act
            var response = await _client.PutAsync($"api/zipato/colors/rgbw/{uuid}?value={value}", new StringContent(""));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("bbd91ea8-a35e-438a-9775-68424c81e2da")]
        [InlineData("982493e6-aefa-4eb7-8464-c4520d9c5311")]
        public async Task RunZipatoScene(string uuid)
        {
            // Act
            var response = await _client.GetAsync($"api/zipato/scenes/{uuid}/run");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("Zipato")]
        [InlineData("Zipato.Data")]
        [InlineData("Zipato.Data.Alarm")]
        [InlineData("Zipato.Data.Announcements")]
        [InlineData("Zipato.Data.Attributes")]
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
        [InlineData("Zipato.Data.Values")]

        [InlineData("Data")]
        [InlineData("Data.Alarm")]
        [InlineData("Data.Announcements")]
        [InlineData("Data.Attributes")]
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
        [InlineData("Data.Values")]

        [InlineData("Alarm")]
        [InlineData("Announcements")]
        [InlineData("Attributes")]
        [InlineData("Bindings")]
        [InlineData("Brands")]
        [InlineData("Cameras")]
        [InlineData("Clusters")]
        [InlineData("ClusterEndpoints")]
        [InlineData("Contacts")]
        [InlineData("Devices")]
        [InlineData("Endpoints")]
        [InlineData("Groups")]
        [InlineData("Networks")]
        [InlineData("NetworkTrees")]
        [InlineData("Rooms")]
        [InlineData("Rules")]
        [InlineData("Scenes")]
        [InlineData("Schedules")]
        [InlineData("Thermostats")]
        [InlineData("VirtualEndpoints")]
        [InlineData("Box")]
        [InlineData("Values")]

      //[InlineData("Announcements[0]")]
        [InlineData("Attributes[0]")]
      //[InlineData("Bindings[0]")]
        [InlineData("Brands[0]")]
        [InlineData("Cameras[0]")]
      //[InlineData("Clusters[0]")]
        [InlineData("ClusterEndpoints[0]")]
        [InlineData("Contacts[0]")]
        [InlineData("Devices[0]")]
        [InlineData("Endpoints[0]")]
        [InlineData("Groups[0]")]
        [InlineData("Networks[0]")]
        [InlineData("NetworkTrees[0]")]
        [InlineData("Rooms[0]")]
      //[InlineData("Rules[0]")]
        [InlineData("Scenes[0]")]
        [InlineData("Schedules[0]")]
        [InlineData("Thermostats[0]")]
      //[InlineData("VirtualEndpoints[0]")]
        [InlineData("Values[0]")]
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/zipato/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            Assert.NotEmpty(responseString);
        }

        [Fact]
        public async Task GetAlarmData()
        {
            // Act
            var response = await _client.GetAsync("api/info/alarm");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            Assert.NotEmpty(responseString);
            var data = JsonConvert.DeserializeObject<AlarmInfo>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetAnnouncementData()
        {
            // Act
            var response = await _client.GetAsync("api/info/announcements");

            // Assert
            if (!_islocal)
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                Assert.NotNull(responseString);
                Assert.NotEmpty(responseString);
                var data = JsonConvert.DeserializeObject<List<AnnouncementInfo>>(responseString);
                Assert.NotNull(data);
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetAttributeData()
        {
            // Act
            var response = await _client.GetAsync("api/info/attributes");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<AttributeInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetBindingData()
        {
            // Act
            var response = await _client.GetAsync("api/info/bindings");

            // Assert
            if (!_islocal)
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<BindingInfo>>(responseString);
                Assert.NotNull(data);
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetBrandData()
        {
            // Act
            var response = await _client.GetAsync("api/info/brands");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<BrandInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetCameraData()
        {
            // Act
            var response = await _client.GetAsync("api/info/cameras");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<CameraInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetClusterData()
        {
            // Act
            var response = await _client.GetAsync("api/info/clusters");

            // Assert
            if (!_islocal)
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ClusterInfo>>(responseString);
                Assert.NotNull(data);
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetClusterEndpointData()
        {
            // Act
            var response = await _client.GetAsync("api/info/clusterendpoints");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ClusterEndpointInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetContactData()
        {
            // Act
            var response = await _client.GetAsync("api/info/contacts");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ContactInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetDeviceData()
        {
            // Act
            var response = await _client.GetAsync("api/info/devices");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<DeviceInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetEndpointData()
        {
            // Act
            var response = await _client.GetAsync("api/info/endpoints");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<EndpointInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetGroupData()
        {
            // Act
            var response = await _client.GetAsync("api/info/groups");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<GroupInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetNetworksData()
        {
            // Act
            var response = await _client.GetAsync("api/info/networks");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<NetworkInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetNetworkTreesData()
        {
            // Act
            var response = await _client.GetAsync("api/info/networktrees");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<NetworkTree>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetRoomData()
        {
            // Act
            var response = await _client.GetAsync("api/info/rooms");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<RoomData>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetRuleData()
        {
            // Act
            var response = await _client.GetAsync("api/info/rules");

            // Assert
            if (!_islocal)
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<RuleInfo>>(responseString);
                Assert.NotNull(data);
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetSceneData()
        {
            // Act
            var response = await _client.GetAsync("api/info/scenes");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<SceneInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetScheduleData()
        {
            // Act
            var response = await _client.GetAsync("api/info/schedules");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ScheduleInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetThermostatData()
        {
            // Act
            var response = await _client.GetAsync("api/info/thermostats");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ThermostatInfo>>(responseString);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetVirtualEndpointData()
        {
            // Act
            var response = await _client.GetAsync("api/info/virtualendpoints");

            // Assert
            if (!_islocal)
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<VirtualEndpointInfo>>(responseString);
                Assert.NotNull(data);
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetBoxData()
        {
            // Act
            var response = await _client.GetAsync("api/info/box");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BoxInfo>(responseString);
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("e9e1422c-11b7-4f7e-bfc4-438977fc1e4c")]
        [InlineData("673c052e-b2f1-4b65-bc6d-ce1225b4ca4b")]
        public async Task GetEndpoint(string uuid)
        {
            // Act
            var response = await _client.GetAsync($"api/info/endpoints/{uuid}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<EndpointInfo>(responseString);
            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("946b1557-9fb1-48f1-b86c-94f3a4cbeb6b")]
        [InlineData("b1e74a45-eb2e-4ba1-831a-03292192fcf4")]
        [InlineData("3582e8be-73ec-4f06-bf5f-88337a1eca6a")]
        [InlineData("5d46a38a-9cca-4ae1-be10-640f89d6484c")]
        public async Task GetNetworkData(string uuid)
        {
            // Act
            var response = await _client.GetAsync($"api/info/networks/{new Guid(uuid)}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NetworkInfo>(responseString);
            Assert.NotNull(data);
        }

        #endregion
    }
}
