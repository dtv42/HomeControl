// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualEndpointConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Config
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Entities;
    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class VirtualEndpointConfig : UuidEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public bool? Hidden { get; set; }
        public bool? HiddenRules { get; set; }
        public string Order { get; set; }
        public int? Room { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public string Type { get; set; }
        public string UiData { get; set; }
    }
}
