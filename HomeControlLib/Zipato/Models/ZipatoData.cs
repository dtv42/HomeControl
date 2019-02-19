// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models
{
    #region Using Directives

    using System.Collections.Generic;

    using DataValueLib;
    using HomeControlLib.Zipato.Extensions;
    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Info;

    #endregion

    /// <summary>
    /// Class holding all Zipato data items.
    /// </summary>
    public class ZipatoData : DataValue
    {
        #region Public Properties

        public BoxInfo Box { get; set; } = new BoxInfo();
        public AlarmInfo Alarm { get; set; } = new AlarmInfo();
        public List<AnnouncementInfo> Announcements { get; set; } = new List<AnnouncementInfo> { };
        public List<AttributeInfo> Attributes { get; set; } = new List<AttributeInfo> { };
        public List<BindingInfo> Bindings { get; set; } = new List<BindingInfo> { };
        public List<BrandInfo> Brands { get; set; } = new List<BrandInfo> { };
        public List<CameraInfo> Cameras { get; set; } = new List<CameraInfo> { };
        public List<ClusterInfo> Clusters { get; set; } = new List<ClusterInfo> { };
        public List<ClusterEndpointInfo> ClusterEndpoints { get; set; } = new List<ClusterEndpointInfo> { };
        public List<ContactInfo> Contacts { get; set; } = new List<ContactInfo> { };
        public List<DeviceInfo> Devices { get; set; } = new List<DeviceInfo> { };
        public List<EndpointInfo> Endpoints { get; set; } = new List<EndpointInfo> { };
        public List<GroupInfo> Groups { get; set; } = new List<GroupInfo> { };
        public List<NetworkInfo> Networks { get; set; } = new List<NetworkInfo> { };
        public List<NetworkTree> NetworkTrees { get; set; } = new List<NetworkTree> { };
        public List<RoomData> Rooms { get; set; } = new List<RoomData> { };
        public List<RuleInfo> Rules { get; set; } = new List<RuleInfo> { };
        public List<SceneInfo> Scenes { get; set; } = new List<SceneInfo> { };
        public List<ScheduleInfo> Schedules { get; set; } = new List<ScheduleInfo> { };
        public List<ThermostatInfo> Thermostats { get; set; } = new List<ThermostatInfo> { };
        public List<VirtualEndpointInfo> VirtualEndpoints { get; set; } = new List<VirtualEndpointInfo> { };
        public List<ValueData> Values { get; set; } = new List<ValueData> { };

        #endregion
    }
}
