// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeteoConfig.cs" company="DTV-Online">
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

    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class MeteoConfig : ZipaboxConfig
    {
        public string Name { get; set; }
        public bool? Master { get; set; }
        public bool? Hidden { get; set; }
        public bool? Reported { get; set; }
        public int? Scale { get; set; }
        public int? Precision { get; set; }
        public int? Room { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceTypes? Type { get; set; }
        public string Unit { get; set; }
        public List<string> EnumValues { get; set; } = new List<string> { };
        //"expire": null,
        //"compression": null,
    }
}
