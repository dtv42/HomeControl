// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoxInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using HomeControlLib.Zipato.Extensions;
    using HomeControlLib.Zipato.Models.Config;

    #endregion

    public class BoxInfo
    {
        public BoxConfig Config { get; set; }
        public bool? Exclusive { get; set; }
        public bool? FirmwareUpgradeAvailable { get; set; }
        public bool? FirmwareUpgradeRequired { get; set; }
        public string FirmwareVersion { get; set; }
        public int? GmtOffset { get; set; }
        public string LatestFirmwareVersion { get; set; }
        public string LocalIp { get; set; }
        public string Name { get; set; }
        public bool? NeedSync { get; set; }
        public bool? Online { get; set; }
        public List<string> PackageTags { get; set; } = new List<string> { };
        public string RemoteIp { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? SaveDate { get; set; }
        public string Serial { get; set; }
        public bool? SetupComplete { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? SyncDate { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public string TimeZone { get; set; }
    }
}
