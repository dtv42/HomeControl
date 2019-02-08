// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraConfig.cs" company="DTV-Online">
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
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Enums;

    #endregion

    public class CameraConfig : ZipaboxConfig
    {
        public string AdminUrl { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public string FtpDir { get; set; }
        public string FtpPassword { get; set; }
        public string FtpServer { get; set; }
        public string FtpUsername { get; set; }
        public bool? Hidden { get; set; }
        public string HiQualityStream { get; set; }
        public string LowQualityStream { get; set; }
        public string MidQualityStream { get; set; }
        public string MjpegStreamEx { get; set; }
        public string MJpegUrl { get; set; }
        public string Name { get; set; }
        public int? OnlineStatusPollTime { get; set; }
        public int? OnlineStatusPollTimeDelay { get; set; }
        public int? Order { get; set; }
        public string Password { get; set; }
        public int? Port { get; set; }
        public string PrepareRecording { get; set; }
        public bool? PrepareSnapshot { get; set; }
        public string PtzClassName { get; set; }
        public string Recording { get; set; }
        public string Snapshot { get; set; }
        public string SnapshotEx { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public string Type { get; set; }
        public string Username { get; set; }
        public Guid? Uuid { get; set; }
        public int? VideoThumbDelay { get; set; }
    }
}
