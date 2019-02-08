// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.Helper.cs" company="DTV-Online">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using DataValueLib;

    using ZipatoLib.Extensions;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Info;
    using ZipatoLib.Models.Data.Color;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control (Helper Methods).
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato
    {
        #region Public Helper Methods

        /// <summary>
        /// Returns the announcement data.
        /// </summary>
        /// <param name="id">The announcement ID.</param>
        /// <returns>The attribute data.</returns>
        public AnnouncementInfo GetAnnouncement(int id) =>
            Data.Announcements.FirstOrDefault(a => a.Id == id);

        /// <summary>
        /// Returns the attribute data.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <returns>The attribute data.</returns>
        public AttributeInfo GetAttribute(Guid uuid) =>
            Data.Attributes.FirstOrDefault(a => a.Uuid == uuid);

        /// <summary>
        /// Returns the attribute value data.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <returns>The attribute value.</returns>
        public ValueData GetValue(Guid uuid) =>
            Data.Values.FirstOrDefault(v => v.Uuid == uuid);

        /// <summary>
        /// Returns the attribute value data.
        /// </summary>
        /// <param name="uuid">The attribute UUID.</param>
        /// <returns>The attribute data.</returns>
        public ValueData GetAttributeValue(Guid uuid) =>
            Data.Attributes.FirstOrDefault(a => a.Uuid == uuid).ToValueData();

        /// <summary>
        /// Returns the binding data.
        /// </summary>
        /// <param name="uuid">The binding UUID.</param>
        /// <returns>The binding data.</returns>
        public BindingInfo GetBinding(Guid uuid) =>
            Data.Bindings.FirstOrDefault(b => b.Uuid == uuid);

        /// <summary>
        /// Returns the brand data.
        /// </summary>
        /// <param name="name">The brand name.</param>
        /// <returns>The brand data.</returns>
        public BrandInfo GetBrand(string name) =>
            Data.Brands.FirstOrDefault(b => b.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Returns the camera data.
        /// </summary>
        /// <param name="uuid">The camera UUID.</param>
        /// <returns>The camera data.</returns>
        public CameraInfo GetCamera(Guid uuid) =>
            Data.Cameras.FirstOrDefault(c => c.Uuid == uuid);

        /// <summary>
        /// Returns the cluster data.
        /// </summary>
        /// <param name="id">The cluster ID.</param>
        /// <returns>The cluster data.</returns>
        public ClusterInfo GetCluster(int id) =>
            Data.Clusters.FirstOrDefault(c => c.Id == id);

        /// <summary>
        /// Returns the cluster endpoint data.
        /// </summary>
        /// <param name="uuid">The cluster endpoint UUID.</param>
        /// <returns>The cluster endpoint data.</returns>
        public ClusterEndpointInfo GetClusterEndpoint(Guid uuid) =>
            Data.ClusterEndpoints.FirstOrDefault(e => e.Uuid == uuid);

        /// <summary>
        /// Returns the contact data.
        /// </summary>
        /// <param name="id">The contact ID.</param>
        /// <returns>The contact data.</returns>
        public ContactInfo GetContact(int id) =>
            Data.Contacts.FirstOrDefault(c => c.Id == id);

        /// <summary>
        /// Returns the device data.
        /// </summary>
        /// <param name="uuid">The device UUID.</param>
        /// <returns>The device data.</returns>
        public DeviceInfo GetDevice(Guid uuid) =>
            Data.Devices.FirstOrDefault(d => d.Uuid == uuid);

        /// <summary>
        /// Returns the endpoint data.
        /// </summary>
        /// <param name="uuid">The endpoint UUID.</param>
        /// <returns>The endpoint data.</returns>
        public EndpointInfo GetEndpoint(Guid uuid) =>
            Data.Endpoints.FirstOrDefault(e => e.Uuid == uuid);

        /// <summary>
        /// Returns the endpoint data.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EndpointInfo GetEndpoint(string name) =>
            Data.Endpoints.FirstOrDefault(e => e.Name.Equals(name, StringComparison.InvariantCulture));

        /// <summary>
        /// Returns the group data.
        /// </summary>
        /// <param name="uuid">The group UUID.</param>
        /// <returns>The group data.</returns>
        public GroupInfo GetGroup(Guid uuid) =>
            Data.Groups.FirstOrDefault(g => g.Uuid == uuid);

        /// <summary>
        /// Returns the network data.
        /// </summary>
        /// <param name="uuid">The network UUID.</param>
        /// <returns>The network data.</returns>
        public NetworkInfo GetNetwork(Guid uuid) =>
            Data.Networks.FirstOrDefault(n => n.Uuid == uuid);

        /// <summary>
        /// Returns the network data.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NetworkInfo GetNetwork(string name) =>
            Data.Networks.FirstOrDefault(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Returns the selected extended network data (tree).
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        public NetworkTree GetNetworkTree(Guid network) =>
            Data.NetworkTrees.FirstOrDefault(n => n.Uuid == network);

        /// <summary>
        /// Returns the room data.
        /// </summary>
        /// <param name="id">The room ID.</param>
        /// <returns>The room data.</returns>
        public RoomData GetRoom(int id) =>
            Data.Rooms.FirstOrDefault(r => r.Id == id);

        /// <summary>
        /// Returns the rule data.
        /// </summary>
        /// <param name="id">The rule ID.</param>
        /// <returns>The rule data.</returns>
        public RuleData GetRule(int id) =>
            Data.Rules.FirstOrDefault(r => r.Id == id);

        /// <summary>
        /// Returns the scene data.
        /// </summary>
        /// <param name="uuid">The scene UUID.</param>
        /// <returns>The scene data.</returns>
        public SceneInfo GetScene(Guid uuid) =>
            Data.Scenes.FirstOrDefault(s => s.Uuid == uuid);

        /// <summary>
        /// Returns the scene data.
        /// </summary>
        /// <param name="name">The scene name.</param>
        /// <returns>The scene data.</returns>
        public SceneInfo GetScene(string name) =>
            Data.Scenes.FirstOrDefault(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Returns the schedule data.
        /// </summary>
        /// <param name="uuid">The schedule UUID.</param>
        /// <returns>The schedule data.</returns>
        public ScheduleInfo GetSchedule(Guid uuid) =>
            Data.Schedules.FirstOrDefault(s => s.Uuid == uuid);

        /// <summary>
        /// Returns the thermostat data.
        /// </summary>
        /// <param name="uuid">The thermostat UUID.</param>
        /// <returns>The thermostat data.</returns>
        public ThermostatInfo GetThermostat(Guid uuid) =>
            Data.Thermostats.FirstOrDefault(t => t.Uuid == uuid);

        /// <summary>
        /// Returns the virtual endpoint data.
        /// </summary>
        /// <param name="uuid">The virtual endpoint UUID.</param>
        /// <returns>The virtual endpoint data.</returns>
        public VirtualEndpointInfo GetVirtualEndpoint(Guid uuid) =>
            Data.VirtualEndpoints.FirstOrDefault(v => v.Uuid == uuid);

        /// <summary>
        /// Returns all attributes for the selected endpoint.
        /// Note that all attribute infos have to include endpoint data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public List<AttributeInfo> GetAttributes(Guid endpoint) =>
            Data.Attributes
                .Where(a => a.Endpoint.Uuid == endpoint)
                .ToList();

        /// <summary>
        /// Returns the attribute of an endpoint using the attribute name.
        /// Note that all attribute infos have to include endpoint data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public AttributeInfo GetAttributeByName(Guid endpoint, string name) =>
            Data.Attributes
                .Where(a => a.Endpoint?.Uuid == endpoint)
                .FirstOrDefault(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Returns the attribute of an endpoint using the attribute name.
        /// Note that all attribute infos have to include endpoint and definition data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public AttributeInfo GetAttributeByDefinition(Guid endpoint, string name) =>
            Data.Attributes
                .Where(a => a.Endpoint?.Uuid == endpoint)
                .FirstOrDefault(a => a.Definition.Attribute.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Returns the attribute of an endpoint using the attribute index.
        /// Note that all attribute infos have to include endpoint data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public AttributeInfo GetAttributeByIndex(Guid endpoint, int index) =>
            Data.Attributes
                .Where(a => a.Endpoint?.Uuid == endpoint)
                .ElementAtOrDefault(index);

        /// <summary>
        /// Returns the attribute value of an endpoint using the attribute name.
        /// Note that all attribute infos have to include endpoint and value data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ValueData GetValueByName(Guid endpoint, string name) =>
            GetAttributeByName(endpoint, name).ToValueData();

        /// <summary>
        /// Returns the attribute value of an endpoint using the attribute definition name.
        /// Note that all attribute infos have to include endpoint, definition, and value data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ValueData GetValueByDefinition(Guid endpoint, string name) =>
            GetAttributeByDefinition(endpoint, name).ToValueData();

        /// <summary>
        /// Returns the attribute value of an endpoint using the attribute index.
        /// Note that all attribute infos have to include endpoint and value data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ValueData GetValueByIndex(Guid endpoint, int index) =>
            GetAttributeByIndex(endpoint, index).ToValueData();

        /// <summary>
        /// Gets a STATE (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <returns>The state value.</returns>
        public bool? GetState(Guid uuid)
            => GetAttribute(uuid)?.Value?.ToBool();

        /// <summary>
        /// Gets a BOOLEAN (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <returns>The boolean value.</returns>
        public bool? GetBoolean(Guid uuid)
            => GetAttribute(uuid)?.Value?.ToBool();

        /// <summary>
        /// Gets a NUMBER (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <returns>The number value.</returns>
        public int? GetNumber(Guid uuid)
            => GetAttribute(uuid)?.Value?.ToNumber();

        /// <summary>
        /// Gets a DOUBLE (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <returns>The number value.</returns>
        public double? GetDouble(Guid uuid)
            => GetAttribute(uuid)?.Value?.ToDouble();

        /// <summary>
        /// Gets a STRING (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <returns>The string value.</returns>
        public string GetString(Guid uuid)
            => GetAttribute(uuid)?.Value?.Value;

        /// <summary>
        /// Sets the new attribute value.
        /// Note that the parameter is an attribute UUID.
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetValueAsync(Guid uuid, string value)
        {
            var updatestatus = await UpdateAttributeValueAsync(uuid, value);

            if (updatestatus.IsGood)
            {
                var (data, readstatus) = await ReadAttributeValueAsync(uuid);

                if (readstatus.IsGood)
                {
                    // Update Attribute value.
                    int index = Data.Attributes.FindIndex(a => a.Uuid == uuid);

                    if (index > -1)
                    {
                        var attributedata = Data.Attributes[index].Value;

                        if (attributedata != null)
                        {
                            attributedata.Timestamp = DateTime.UtcNow;
                            attributedata.Value = value;
                        }
                    }

                    // Update value list.
                    index = Data.Values.FindIndex(v => v.Uuid == uuid);

                    if (index > -1)
                    {
                        var valuedata = Data.Values[index].Value;

                        if (valuedata != null)
                        {
                            valuedata.Timestamp = DateTime.UtcNow;
                            valuedata.Value = value;
                        }
                    }

                    Data.Status = readstatus;
                    return true;
                }

                Data.Status = readstatus;
                return false;
            }

            Data.Status = updatestatus;
            return false;
        }

        /// <summary>
        /// Sets the new attribute value of an endpoint using the attribute name.
        /// Note that all attribute infos have to include endpoint data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetValueByNameAsync(Guid endpoint, string name, string value)
        {
            var attribute = GetAttributeByName(endpoint, name);

            if ((attribute != null) && attribute.Uuid.HasValue)
            {
                return await SetValueAsync(attribute.Uuid.Value, value);
            }

            return false;
        }

        /// <summary>
        /// Sets the new attribute value of an endpoint using the attribute definition name.
        /// Note that all attribute infos have to include endpoint and definition data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetValueByDefinitionAsync(Guid endpoint, string name, string value)
        {
            var attribute = GetAttributeByDefinition(endpoint, name);

            if ((attribute != null) && attribute.Uuid.HasValue)
            {
                return await SetValueAsync(attribute.Uuid.Value, value);
            }

            return false;
        }

        /// <summary>
        /// Sets the new attribute value of an endpoint using the attribute index.
        /// Note that all attribute infos have to include endpoint data.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetValueByIndexAsync(Guid endpoint, int index, string value)
        {
            var attribute = GetAttributeByIndex(endpoint, index);

            if ((attribute != null) && attribute.Uuid.HasValue)
            {
                return await SetValueAsync(attribute.Uuid.Value, value);
            }

            return false;
        }

        /// <summary>
        /// Sets the STATE (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="state">The state value.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetStateAsync(Guid uuid, bool state)
            => await SetValueAsync(uuid, state.ToString().ToLower());

        /// <summary>
        /// Sets the BOOLEAN (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="number">The number value.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetBooleanAsync(Guid uuid, bool value)
            => await SetValueAsync(uuid, value.ToString().ToLower());

        /// <summary>
        /// Sets the NUMBER (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="number">The number value.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetNumberAsync(Guid uuid, int number)
            => await SetValueAsync(uuid, number.ToString());

        /// <summary>
        /// Sets the DOUBLE (attribute value).
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="number">The number value.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetDoubleAsync(Guid uuid, double number)
            => await SetValueAsync(uuid, number.ToString());

        /// <summary>
        /// Sets the color (attribute value) of a RGB LED endpoint using a RGBW value.
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="color">The color value.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetColorAsync(Guid uuid, RGB color) =>
            await SetColorAsync(uuid, ColorData.Rgb2Hex(color));

        /// <summary>
        /// Sets the color (attribute value) of a RGBW LED endpoint.
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="color">The color value (RGBW).</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetColorAsync(Guid uuid, RGBW color) =>
            await SetColorAsync(uuid, ColorData.Rgbw2Hex(color));

        /// <summary>
        /// Sets the color (attribute value) of a RGBW LED endpoint using a HEX value.
        /// </summary>
        /// <param name="uuid">The attribute uuid.</param>
        /// <param name="hex">The color value (HEX).</param>
        /// <returns></returns>
        public async Task<bool> SetColorAsync(Guid uuid, string hex)
        {
            switch (hex.Length)
            {
                case 6:
                    return await SetValueAsync(uuid, hex);
                case 8:
                    return await SetValueAsync(uuid, hex);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Check if a user session with the Zipato web service can be established.
        /// </summary>
        /// <returns></returns>
        public bool CheckUserSession()
        {
            var session = ZipatoSession.StartSession(_logger, _client, _settings);
            bool OK = session.IsActive;
            ZipatoSession.EndSession();
            return OK;
        }

        /// <summary>
        /// Async method to retrieve just the necessary data depending on the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        public async Task<DataStatus> ReadPropertyAsync(string property)
        {
            if (IsProperty(property))
            {
                switch (property)
                {
                    case "Zipato":
                    case "Zipato.Data":
                    case "Data":
                        return await ReadAllAsync();
                    case "Zipato.Data.Alarm":
                    case "Data.Alarm":
                    case "Alarm":
                        return (await DataReadAlarmAsync()).Status;
                    case "Zipato.Data.Announcements":
                    case "Data.Announcements":
                    case "Announcements":
                    case var text when new Regex(@"Zipato.Data.Announcements\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Announcements\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Announcements\[[0..9]+\]").IsMatch(text):
                        return (await DataReadAnnouncementsAsync()).Status;
                    case "Zipato.Data.Attributes":
                    case "Data.Attributes":
                    case "Attributes":
                    case var text when new Regex(@"Zipato.Data.Attributes\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Attributes\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Attributes\[[0..9]+\]").IsMatch(text):
                        return (await DataReadAttributesFullAsync()).Status;
                    case "Zipato.Data.Values":
                    case "Data.Values":
                    case "Values":
                    case var text when new Regex(@"Zipato.Data.Values\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Values\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Values\[[0..9]+\]").IsMatch(text):
                        return (await DataReadValuesAsync()).Status;
                    case "Zipato.Data.Bindings":
                    case "Data.Bindings":
                    case "Bindings":
                    case var text when new Regex(@"Zipato.Data.Bindings\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Bindings\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Bindings\[[0..9]+\]").IsMatch(text):
                        return (await DataReadBindingsFullAsync()).Status;
                    case "Zipato.Data.Brands":
                    case "Data.Brands":
                    case "Brands":
                    case var text when new Regex(@"Zipato.Data.Brands\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Brands\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Brands\[[0..9]+\]").IsMatch(text):
                        return (await DataReadBrandsFullAsync()).Status;
                    case "Zipato.Data.Cameras":
                    case "Data.Cameras":
                    case "Cameras":
                    case var text when new Regex(@"Zipato.Data.Cameras\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Zipato.Data.Cameras\[[0..9]+\]").IsMatch(text) ||
                    new Regex(@"Zipato.Data.Cameras\[[0..9]+\]").IsMatch(text):
                        return (await DataReadCamerasFullAsync()).Status;
                    case "Zipato.Data.Clusters":
                    case "Data.Clusters":
                    case "Clusters":
                    case var text when new Regex(@"Zipato.Data.Clusters\[[0..9]+\]").IsMatch(text) ||
                                           new Regex(@"Data.Clusters\[[0..9]+\]").IsMatch(text) ||
                                           new Regex(@"Clusters\[[0..9]+\]").IsMatch(text):
                        return (await DataReadClustersFullAsync()).Status;
                    case "Zipato.Data.ClusterEndpoints":
                    case "Data.ClusterEndpoints":
                    case "ClusterEndpoints":
                    case var text when new Regex(@"Zipato.Data.ClusterEndpoints\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.ClusterEndpoints\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"ClusterEndpoints\[[0..9]+\]").IsMatch(text):
                        return (await DataReadClusterEndpointsFullAsync()).Status;
                    case "Zipato.Data.Contacts":
                    case "Data.Contacts":
                    case "Contacts":
                    case var text when new Regex(@"Zipato.Data.Contacts\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Contacts\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Contacts\[[0..9]+\]").IsMatch(text):
                        return (await DataReadContactsAsync()).Status;
                    case "Zipato.Data.Devices":
                    case "Data.Devices":
                    case "Devices":
                    case var text when new Regex(@"Zipato.Data.Devices\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Devices\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Devices\[[0..9]+\]").IsMatch(text):
                        return (await DataReadDevicesFullAsync()).Status;
                    case "Zipato.Data.Endpoints":
                    case "Data.Endpoints":
                    case "Endpoints":
                    case var text when new Regex(@"Zipato.Data.Endpoints\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Endpoints\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Endpoints\[[0..9]+\]").IsMatch(text):
                        return (await DataReadEndpointsFullAsync()).Status;
                    case "Zipato.Data.Groups":
                    case "Data.Groups":
                    case "Groups":
                    case var text when new Regex(@"Zipato.Data.Groups\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Groups\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Groups\[[0..9]+\]").IsMatch(text):
                        return (await DataReadGroupsFullAsync()).Status;
                    case "Zipato.Data.Networks":
                    case "Data.Networks":
                    case "Networks":
                    case var text when new Regex(@"Zipato.Data.Networks\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Networks\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Networks\[[0..9]+\]").IsMatch(text):
                        return (await DataReadNetworksFullAsync()).Status;
                    case "Zipato.Data.NetworkTrees":
                    case "Data.NetworkTrees":
                    case "NetworkTrees":
                    case var text when new Regex(@"Zipato.Data.NetworkTrees\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.NetworkTrees\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"NetworkTrees\[[0..9]+\]").IsMatch(text):
                        return (await DataReadNetworkTreesAsync()).Status;
                    case "Zipato.Data.Rooms":
                    case "Data.Rooms":
                    case "Rooms":
                    case var text when new Regex(@"Zipato.Data.Rooms\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Rooms\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Rooms\[[0..9]+\]").IsMatch(text):
                        return (await DataReadRoomsAsync()).Status;
                    case "Zipato.Data.Rules":
                    case "Data.Rules":
                    case "Rules":
                    case var text when new Regex(@"Zipato.Data.Rules\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Rules\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Rules\[[0..9]+\]").IsMatch(text):
                        return (await DataReadRulesFullAsync()).Status;
                    case "Zipato.Data.Scenes":
                    case "Data.Scenes":
                    case "Scenes":
                    case var text when new Regex(@"Zipato.Data.Scenes\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Scenes\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Scenes\[[0..9]+\]").IsMatch(text):
                        return (await DataReadScenesFullAsync()).Status;
                    case "Zipato.Data.Schedules":
                    case "Data.Schedules":
                    case "Schedules":
                    case var text when new Regex(@"Zipato.Data.Schedules\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Schedules\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Schedules\[[0..9]+\]").IsMatch(text):
                        return (await DataReadSchedulesFullAsync()).Status;
                    case "Zipato.Data.Thermostats":
                    case "Data.Thermostats":
                    case "Thermostats":
                    case var text when new Regex(@"Zipato.Data.Thermostats\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Thermostats\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Thermostats\[[0..9]+\]").IsMatch(text):
                        return (await DataReadThermostatsFullAsync()).Status;
                    case "Zipato.Data.VirtualEndpoints":
                    case "Data.VirtualEndpoints":
                    case "VirtualEndpoints":
                    case var text when new Regex(@"Zipato.Data.Attributes\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Data.Attributes\[[0..9]+\]").IsMatch(text) ||
                                       new Regex(@"Attributes\[[0..9]+\]").IsMatch(text):
                        return (await DataReadVirtualEndpointsAsync()).Status;
                    case "Zipato.Data.Box":
                    case "Data.Box":
                    case "Box":
                        return (await DataReadBoxAsync()).Status;
                    default:
                        return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Method to determine if the property is supported. Switches to the proper section.
        /// Note this routine supports nested properties and simple arrays and generic List (IList).
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>True if the property is found.</returns>
        public static bool IsProperty(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new[] { '.' }, 2);

                switch (parts[0])
                {
                    case "Zipato":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Zipato), parts[1]) != null : true;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(ZipatoData), parts[1]) != null : true;
                    default:
                        return PropertyValue.GetPropertyInfo(typeof(ZipatoData), property) != null;
                }
            }

            return false;
        }

        /// <summary>
        /// Method to get the property value by name. Switches to the proper section.
        /// Note this routine supports nested properties and simple arrays and generic List (IList).
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new[] { '.' }, 2);

                switch (parts[0])
                {
                    case "Zipato":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : this.Data;
                    default:
                        return PropertyValue.GetPropertyValue(Data, property);
                }
            }

            return null;
        }

        #endregion Public Helper Methods
    }
}
