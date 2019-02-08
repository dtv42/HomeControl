// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClusterEndpointInfo.cs" company="DTV-Online">
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

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Entities;
    using ZipatoLib.Models.Enums;

    #endregion

    public class ClusterEndpointInfo : UuidEntity
    {
        public List<string> Actions { get; set; } = new List<string> { };
        public List<AttributeData> Attributes { get; set; } = new List<AttributeData> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public string ClusterClass { get; set; }
        public ClusterEndpointConfig Config { get; set; }
        public List<EventEntity> ConsumeEvents { get; set; } = new List<EventEntity> { };
        public List<EventEntity> CreateEvents { get; set; } = new List<EventEntity> { };
        public UuidEntity Device { get; set; }
        public EndpointEntity Endpoint { get; set; }
        public UuidEntity Network { get; set; }
        public RoomData Room { get; set; }
        public int? RoomId { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
    }
}
