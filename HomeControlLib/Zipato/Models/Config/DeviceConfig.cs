// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceConfig.cs" company="DTV-Online">
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
    using HomeControlLib.Zipato.Models.Data;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class DeviceConfig : ZipaboxConfig
    {
        public bool? AlternateVnodeAssoc { get; set; }
        public bool? AlwaysListening { get; set; }
        public int? AppSubVersion { get; set; }
        public int? AppVersion { get; set; }
        public JArray AssocGrpBlacklist { get; set; } = new JArray();
        public List<JObject> AssociationGroups { get; set; } = new List<JObject> { };
        public Dictionary<string, string> AutoPresetTimes { get; set; } = new Dictionary<string, string> { };
        public string BasicDevClass { get; set; }
        public string BatteryPartitionTag { get; set; }
        public bool? CatchAllTamper { get; set; }
        //public ConfigurationData Configuration { get; set; } = new JObject();
        public bool? Crc16Encap { get; set; }
        public string DefaultManualMode { get; set; }
        public int? DefaultWakeUpInterval { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public bool? Discovered { get; set; }
        public Dictionary<string, string> EventMap { get; set; } = new Dictionary<string, string> { };
        public string Firmware { get; set; }
        public string GenericDevClass { get; set; }
        public bool? Hidden { get; set; }
        public string IconType { get; set; }
        public bool? IgnoreMulticasts { get; set; }
        public bool? InitiallyDisabled { get; set; }
        public bool? Listening { get; set; }
        public int? LocationId { get; set; }
        public int? ManufacturerId { get; set; }
        public long? MaxWakeUpInterval { get; set; }
        public long? MinWakeUpInterval { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public List<string> NeighborNodes { get; set; } = new List<string> { };
        //"nextHoursEnd": null,
        //"nextHoursStart": null,
        //"nextHoursTimes": null,
        public bool? NoBatteryCheck { get; set; }
        public int? NodeId { get; set; }
        public string Order { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AlarmModes? PanicAlarmMode { get; set; }
        public bool? PeriodicallyWakeUp { get; set; }
        public List<PresetData> Presets { get; set; } = new List<PresetData> { };
        public int? ProductId { get; set; }
        public int? ProductTypeId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleTypes? RoleType { get; set; }
        public int? Room { get; set; }
        public bool? SchedulerEnabled { get; set; }
        public bool? SecurelyIncluded { get; set; }
        public bool? Sensor1000ms { get; set; }
        public bool? Sensor250ms { get; set; }
        public string Serial { get; set; }
        public bool? Sleeping { get; set; }
        public string SpecificDevClass { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public AlarmModes? TamperAlarmMode { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceTypes? Type { get; set; }
        public string User { get; set; }
        public bool? UsesStateChangeNotification { get; set; }
        public Guid? Uuid { get; set; }
        public string VacationPresetName { get; set; }
        //"vacationEnd": null,
        //"vacationStart": null,
        public long? WakeUpInterval { get; set; }
        public long? WakeUpIntervalStep { get; set; }
        public int? ZwManufacturerId { get; set; }
    }
}
