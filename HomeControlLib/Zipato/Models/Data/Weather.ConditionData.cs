// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.ConditionData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data.Weather
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    public class ConditionData
    {
        public string Weather { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        //"cloudcover": null,
        public double? PrecipMM { get; set; }
        //"weatherCode": null,
        public string WeatherDesc { get; set; }
        public string WindDir16Point { get; set; }
        public double? WindDirDegree { get; set; }
        public double? WindspeedKmph { get; set; }
        [JsonProperty(PropertyName = "display_location")]
        LocationData DisplayLocation { get; set; } = new LocationData();
        [JsonProperty(PropertyName = "observation_location")]
        LocationData ObservationLocation { get; set; } = new LocationData();
        [JsonProperty(PropertyName = "observation_epoch")]
        public string ObservationEpoch { get; set; }
        [JsonProperty(PropertyName = "temp_c")]
        public double? TemperatureC { get; set; }
        [JsonProperty(PropertyName = "temp_f")]
        public double? TemperatureF { get; set; }
        [JsonProperty(PropertyName = "relative_humidity")]
        public string RelativeHumidity { get; set; }
        [JsonProperty(PropertyName = "wind_dir")]
        public string WindDir { get; set; }
        [JsonProperty(PropertyName = "wind_degrees")]
        public double? WindDegrees { get; set; }
        [JsonProperty(PropertyName = "wind_kph")]
        public double? WindKph { get; set; }
        [JsonProperty(PropertyName = "wind_mph")]
        public double? WindMph { get; set; }
        [JsonProperty(PropertyName = "pressure_mb")]
        public string PressureMb { get; set; }
        [JsonProperty(PropertyName = "pressure_in")]
        public string PressureIn { get; set; }
        [JsonProperty(PropertyName = "pressure_trend")]
        public string PressureTrend { get; set; }
        [JsonProperty(PropertyName = "feelslike_c")]
        public double? FeelsLikeC { get; set; }
        [JsonProperty(PropertyName = "feelslike_f")]
        public double? FeelsLikeF { get; set; }
        public string UV { get; set; }
        [JsonProperty(PropertyName = "precip_1hr_metric")]
        public double? Precip1hrMetric { get; set; }
        [JsonProperty(PropertyName = "precip_1hr_in")]
        public double? Precip1hrIn { get; set; }
        [JsonProperty(PropertyName = "precip_today_metric")]
        public double? PrecipTodayMetric { get; set; }
        [JsonProperty(PropertyName = "precip_today_in")]
        public double? PrecipTodayIn { get; set; }
        public string Icon { get; set; }
    }
}
