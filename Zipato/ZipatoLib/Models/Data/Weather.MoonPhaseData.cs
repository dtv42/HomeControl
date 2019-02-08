// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.MoonPhaseData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data.Weather
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    public class MoonPhaseData
    {
        public double? PercentIlluminated { get; set; }
        public double? AgeOfMoon { get; set; }
        [JsonProperty(PropertyName = "current_time")]
        public TimeData CurrentTime { get; set; }
        public string PhaseOfMoon { get; set; }
    }
}
