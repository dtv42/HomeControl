// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeDefinition.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class AttributeDefinition
    {
        public string Attribute { get; set; }
        public string AttributeType { get; set; }
        public string Cluster { get; set; }
        public int? Id { get; set; }
        public bool? Readable { get; set; }
        [JsonProperty("reportble")]
        public bool? Reportable { get; set; }
        [JsonProperty("writable")]
        public bool? Writeable { get; set; }
    }
}
