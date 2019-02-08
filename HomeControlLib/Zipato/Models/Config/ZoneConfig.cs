// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZoneConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Config
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using HomeControlLib.Zipato.Models.Enums;

    /// <summary>
    ///
    /// </summary>
    public class ZoneConfig : ZipaboxConfig
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ArmModes? ArmMode { get; set; }
        public string Attribute { get; set; }
        public string AttributeUuid { get; set; }
        public bool? Bypassable { get; set; }
        public string ClusterClass { get; set; }
        public string CrossZoningGroup { get; set; }
        //    "crossZoningTime": null,
        public string Endpoint { get; set; }
        public bool? Entry { get; set; }
        //    "exit": null,
        public bool? Follower { get; set; }
        //    "inactivityDelay": null,
        //    "latchInterval": null,
        //    "maxAge": null,
        public string Name { get; set; }
        public int? Number { get; set; }
        public bool? RemoveBypass { get; set; }
        //    "supervision": null,
        [JsonConverter(typeof(StringEnumConverter))]
        public ZoneTypes? Type { get; set; }
        public string Uuid { get; set; }
    }
}
