// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Dtos
{
    #region Using Directives

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class LocationDto
    {
        public string Name { get; set; }
        public string Key { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LocationTypes TamperAlarmMode { get; set; }
        public int? Id { get; set; }
    }
}
