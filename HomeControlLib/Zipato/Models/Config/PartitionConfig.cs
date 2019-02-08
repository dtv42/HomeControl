// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartitionConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Config
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    using HomeControlLib.Zipato.Models.Enums;
    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class PartitionConfig : ZipaboxConfig
    {
        public Dictionary<string, dynamic> ActionMap { get; set; } = new Dictionary<string, dynamic>();
        public bool? AllowSyncWhenArmed { get; set; }
        public bool? AlwaysArmed { get; set; }
        public int? AwayEntryDelay { get; set; }
        public int? AwayExitDelay { get; set; }
        public int? BatteryThreshold { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public bool? CrossZoning { get; set; }
        public bool? DefaultAlertBattery { get; set; }
        public bool? DefaultAlertOffline { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public Dictionary<string, dynamic> DevUserMap { get; set; } = new Dictionary<string, dynamic>();
        public List<ReportEntity> EmailReports { get; set; } = new List<ReportEntity> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public ArmModes? FirstStartMode { get; set; }
        public bool? Hidden { get; set; }
        public int? HomeEntryDelay { get; set; }
        public int? HomeExitDelay { get; set; }
        public bool? Mobility { get; set; }
        public bool? MobilityResetOnStateChange { get; set; }
        public int? MobilityTime { get; set; }
        public bool? MobilityTripOnAway { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Order { get; set; }
        public List<ReportEntity> PushReports { get; set; } = new List<ReportEntity> { };
        public bool? QuickArm { get; set; }
        public List<ReportEntity> Reports { get; set; } = new List<ReportEntity> { };
        public Dictionary<string, dynamic> Schedule { get; set; } = new Dictionary<string, dynamic>();
        public bool? SendConnectivityReports { get; set; }
        public bool? Silent { get; set; }
        public int? SirenDelay { get; set; }
        public List<string> Sirens { get; set; } = new List<string> { };
        public int? SirenTime { get; set; }
        public List<JObject> SlotList { get; set; } = new List<JObject> { };
        public List<ReportEntity> SmsReports { get; set; } = new List<ReportEntity> { };
        public List<string> Squawks { get; set; } = new List<string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public string Type { get; set; }
        public Dictionary<string, dynamic> UserRights { get; set; } = new Dictionary<string, dynamic>();
        public Guid? Uuid { get; set; }
        public List<ReportEntity> VoiceReports { get; set; } = new List<ReportEntity> { };
    }
}
