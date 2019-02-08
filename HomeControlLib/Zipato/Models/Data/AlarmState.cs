// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmState.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    public class AlarmState
    {
        public DateTime? BoxTimestamp { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ArmModes? TamperAlarmMode { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? Tripped { get; set; }
    }
}
