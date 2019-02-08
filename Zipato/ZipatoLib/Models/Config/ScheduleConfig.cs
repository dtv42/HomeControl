// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleConfig.cs" company="DTV-Online">
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

    using ZipatoLib.Extensions;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class ScheduleConfig : ZipaboxConfig
    {
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? ActiveFrom { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? ActiveUntil { get; set; }
        public string Astro { get; set; }
        public int? AstroOffset { get; set; }
        public string AstroWeather { get; set; }
        public string CronExpression { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public int? RepeatInterval { get; set; }
        public string RepeatUnit { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public Guid? Uuid { get; set; }
    }
}
