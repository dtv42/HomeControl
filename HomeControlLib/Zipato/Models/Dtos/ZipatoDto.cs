// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Dtos
{
    #region Using Directives

    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class ZipatoDto
    {
        public DateTime? AllowRegistrationDate { get; set; }
        public DateTime? AppBuildDate { get; set; }
        public bool? Cluster { get; set; }
        public string Data { get; set; }
        public DateTime? FirstEntryDate { get; set; }
        public string HardwareVersion { get; set; }
        public int? Id { get; set; }
        public string JarVersion { get; set; }
        public string KernelVersion { get; set; }
        public DateTime? LastBootDate { get; set; }
        public DateTime? LastEntryDate { get; set; }
        public string LastEntryIp { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ZipatoDto ParentZipato { get; set; }
        public string RedirectUrl { get; set; }
        public string Serial { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ZipatoStatusTypes? Status { get; set; }
        public string SystemVersion { get; set; }
        public int? TimeZone { get; set; }
        public string TimeZoneName { get; set; }
        public int? TimeZoneOffset { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public UserTypes? Type { get; set; }
        public string UbootVersion { get; set; }
        public string UiData { get; set; }
        public string UpdaterVersion { get; set; }
    }
}
