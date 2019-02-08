// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.AstronomyData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data.Weather
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion

    public class AstronomyData
    {
        public ResponseData Response { get; set; }
        public double? MoonPercent { get; set; }
        public double? MoonAge { get; set; }
        public string Sunset { get; set; }
        public string Sunrise { get; set; }
        public string MoonPhaseName { get; set; }
        [JsonProperty(PropertyName = "moon_phase")]
        public MoonPhaseData MoonPhase { get; set; }
        [JsonProperty(PropertyName = "sun_phase")]
        public SunPhaseData SunPhase { get; set; }
    }
}
