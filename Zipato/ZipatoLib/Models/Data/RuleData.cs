// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Enums;

    #endregion

    public class RuleData
    {
        public DateTime? Created { get; set; }
        public bool? Deleted { get; set; }
        public string Description { get; set; }
        public bool? Disabled { get; set; }
        public int? Id { get; set; }
        public bool? Invalid { get; set; }
        public DateTime? Modified { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public RuleTypes? Type { get; set; }
    }
}
