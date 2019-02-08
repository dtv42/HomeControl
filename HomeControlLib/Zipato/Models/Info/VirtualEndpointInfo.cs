// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualEndpointInfo.cs" company="DTV-Online">
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

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;
    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    public class VirtualEndpointInfo : IconEntity
    {
        public List<string> ApiKeys { get; set; } = new List<string> { };
        public List<AttributeUrlEntity> AttributeUrls { get; set; } = new List<AttributeUrlEntity> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public List<EndpointEntity> ClusterEndpoints { get; set; } = new List<EndpointEntity> { };
        public VirtualEndpointConfig Config { get; set; }
        public DescriptorEntity Descriptor { get; set; }
        public UuidEntity Device { get; set; }
        public UuidEntity Network { get; set; }
        public RoomData Room { get; set; }
        public int? RoomId { get; set; }
        public List<UuidEntity> SourceBindings { get; set; } = new List<UuidEntity> { };
        public bool? Supported { get; set; }
        public List<UuidEntity> TargetBindings { get; set; } = new List<UuidEntity> { };
    }
}
