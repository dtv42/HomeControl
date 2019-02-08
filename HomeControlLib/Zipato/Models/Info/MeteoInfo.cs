// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeteoInfo.cs" company="DTV-Online">
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
    using HomeControlLib.Zipato.Models.Entities;
    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    public class MeteoInfo : IconEntity
    {
        public List<AttributeData> Attributes { get; set; } = new List<AttributeData> { };
        public string ClusterClass { get; set; }
        public bool? Supported { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes Category { get; set; }
    }
}
