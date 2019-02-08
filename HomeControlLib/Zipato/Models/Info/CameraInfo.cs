// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    public class CameraInfo : UuidEntity
    {
        public string AdminUrl { get; set; }
        public List<EndpointEntity> ClusterEndpoints { get; set; } = new List<EndpointEntity> { };
        public CameraConfig Config { get; set; }
        public DescriptorEntity Descriptor { get; set; }
        public UuidEntity Device { get; set; }
        public string HiQualityStream { get; set; }
        public NameEntity Icon { get; set; }
        public string IpAddress { get; set; }
        public string LowQualityStream { get; set; }
        public UuidEntity Network { get; set; }
        public RoomData Room { get; set; }
        public int? RoomId { get; set; }
        public bool? ShowIcon { get; set; }
        public string Snapshot { get; set; }
        public bool? Supported { get; set; }
        public string TemplateId { get; set; }
        public NameEntity UserIcon { get; set; }
    }
}
