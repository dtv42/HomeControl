// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.LocationData.cs" company="DTV-Online">
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

    public class LocationData
    {
        public string Full { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Elevation { get; set; }
        [JsonProperty(PropertyName = "state_name")]
        public string StateName { get; set; }
        [JsonProperty(PropertyName = "country_iso3166")]
        public string CountryISO3166 { get; set; }
    }
}
