// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    public class DeviceInfo : UuidEntity
    {
        public DeviceConfig Config { get; set; }
        public DescriptorEntity Descriptor { get; set; }
        public DescriptorInfo DeviceDescriptorInfo { get; set; }
        public List<EndpointData> Endpoints { get; set; } = new List<EndpointData> { };
        public InfoData Info { get; set; }
        public UuidEntity Network { get; set; }
        public RoomData Room { get; set; }
        public int? RoomId { get; set; }
        public Dictionary<string, dynamic> State { get; set; } = new Dictionary<string, dynamic> { };
        public List<string> Tags { get; set; } = new List<string> { };
    }
}
