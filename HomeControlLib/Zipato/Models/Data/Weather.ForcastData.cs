// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.ForcastData.cs" company="DTV-Online">
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

    using Newtonsoft.Json;

    #endregion

    public class ForecastData
    {
        public DateTime? Date { get; set; }
        public int? Period { get; set; }
        public TemperatureData High { get; set; }
        public TemperatureData Low { get; set; }
        public string Conditions { get; set; }
        public string Icon { get; set; }
        public double? Pop { get; set; }
        public double? PrecipMM { get; set; }
        //"weatherCode": null,
        public string WeatherDesc { get; set; }
        public string WindDir16Point { get; set; }
        public double? WindDirDegree { get; set; }
        public double? WindspeedKmph { get; set; }
        public double? TempMaxC { get; set; }
        public double? TempMinC { get; set; }
        public string WindDirection { get; set; }
        public string SkyIcon { get; set; }

        [JsonProperty(PropertyName = "qpf_allday")]
        public QpfData QpfAllday { get; set; }
        [JsonProperty(PropertyName = "qpf_day")]
        public QpfData QpfDay { get; set; }
        [JsonProperty(PropertyName = "qpf_night")]
        public QpfData QpfNight { get; set; }
        [JsonProperty(PropertyName = "snow_allday")]
        public SnowData SnowAllday { get; set; }
        [JsonProperty(PropertyName = "snow_day")]
        public SnowData SnowDay { get; set; }
        [JsonProperty(PropertyName = "snow_night")]
        public SnowData SnowNight { get; set; }
        public WindData MaxWind { get; set; }
        public WindData AveWind { get; set; }
        public double? AveHumidity { get; set; }
        public double? MaxHumidity { get; set; }
        public double? MinHumidity { get; set; }
    }
}
