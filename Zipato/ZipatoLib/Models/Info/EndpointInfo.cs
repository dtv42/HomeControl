// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointInfo.cs" company="DTV-Online">
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

    public class EndpointInfo : UuidEntity
    {
        public List<AttributeData> Attributes { get; set; } = new List<AttributeData> { };
        public List<EndpointEntity> ClusterEndpoints { get; set; } = new List<EndpointEntity> { };
        public EndpointConfig Config { get; set; } = new EndpointConfig();
        public List<EventEntity> ConsumeEvents { get; set; } = new List<EventEntity> { };
        public List<EventEntity> CreateEvents { get; set; } = new List<EventEntity> { };
        public DescriptorEntity Descriptor { get; set; } = new DescriptorEntity();
        public UuidEntity Device { get; set; } = new UuidEntity();
        public UuidEntity Network { get; set; } = new UuidEntity();
        public RoomData Room { get; set; } = new RoomData();
        public int? RoomId { get; set; }
        public List<UuidEntity> SourceBindings { get; set; } = new List<UuidEntity> { };
        public bool? Supported { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public List<UuidEntity> TargetBindings { get; set; } = new List<UuidEntity> { };
    }
}
