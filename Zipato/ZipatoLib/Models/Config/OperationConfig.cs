// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Config
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ZipatoLib.Models.Enums;
    using ZipatoLib.Models.Data;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class OperationConfig : ZipaboxConfig
    {
        public Dictionary<string, string> AutoPresetTimes { get; set; } = new Dictionary<string, string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes Category { get; set; }
        //"checkInterval": null,
        public int? Cooldown { get; set; }
        //"configs": null,
        //"defaultConfig": null,
        public string DefaultManualMode { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public string Firmware { get; set; }
        public bool? Hidden { get; set; }
        public bool? HiddenRules { get; set; }
        public double? Hysteresis { get; set; }
        public bool? InitiallyDisabled { get; set; }
        public bool? Invert { get; set; }
        public int? LocationId { get; set; }
        public int? ManufacturerId { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        //"nextHoursEnd": null,
        //"nextHoursStart": null,
        //"nextHoursTimes": null,
        //"offValue": null,
        //"offset": null,
        public string OperationMode { get; set; }
        public string Order { get; set; }
        public bool? PeriodicallyWakeUp { get; set; }
        public List<PresetData> Presets { get; set; } = new List<PresetData> { };
        public int? Room { get; set; }
        public bool? SchedulerEnabled { get; set; }
        //"sendOffValue": null,
        public double? Separation { get; set; }
        public string Serial { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceTypes Type { get; set; }
        public string User { get; set; }
        //"vacationEnd": null,
        //"vacationStart": null,
        public string VacationPresetName { get; set; }
        public int? WakeUpInterval { get; set; }
    }
}
