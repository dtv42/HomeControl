// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmConfig.cs" company="DTV-Online">
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

    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    public class AlarmConfig : ZipaboxConfig
    {
        public string BatteryPartitionTag { get; set; }
        public bool? CatchAllTamper { get; set; }
        public string Description { get; set; }
        public string DescriptorFlags { get; set; }
        public string Firmware { get; set; }
        public bool? Hidden { get; set; }
        public string LocationId { get; set; }
        public string ManufacturerId { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AlarmModes PanicAlarmMode { get; set; }
        public bool? PeriodicallyWakeUp { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        [JsonConverter(typeof(StringEnumConverter))]
        public AlarmModes TamperAlarmMode { get; set; }
        public string User { get; set; }
        public Guid? Uuid { get; set; }
        public string WakeUpInterval { get; set; }
    }
}
