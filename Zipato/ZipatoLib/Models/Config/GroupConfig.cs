// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Config
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class GroupConfig : ZipaboxConfig
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public bool? Hidden { get; set; }
        public string Name { get; set; }
        //"nativeAt": null,
        public string Order { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceTypes? Type { get; set; }
        public List<string> Endpoints { get; set; } = new List<string> { };
        public int? Room { get; set; }
        public Guid? Uuid { get; set; }
    }
}
