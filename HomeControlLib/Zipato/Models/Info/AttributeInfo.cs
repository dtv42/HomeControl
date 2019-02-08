// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeInfo.cs" company="DTV-Online">
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

    using Newtonsoft.Json.Linq;

    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Dtos;
    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class AttributeInfo : AttributeEntity
    {
        public List<JObject> Children { get; set; } = new List<JObject> { };
        public EndpointEntity ClusterEndpoint { get; set; }
        public AttributeConfig Config { get; set; }
        public AttributeDefinition Definition { get; set; }
        public DeviceEntity Device { get; set; }
        public EndpointEntity Endpoint { get; set; }
        public string Name { get; set; }
        public UuidEntity Network { get; set; }
        public UuidEntity Parent { get; set; }
        public IdEntity Room { get; set; }
        public int? RoomId { get; set; }
        public bool? ShowIcon { get; set; }
        public NameEntity UiType { get; set; }
        public AttributeValueDto Value { get; set; }
    }
}
