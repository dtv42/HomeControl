// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThermostatOperationConfig.cs" company="DTV-Online">
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

    /// <summary>
    ///
    /// </summary>
    public class ThermostatOperationConfig : ZipaboxConfig
    {
        public List<string> Tags { get; set; } = new List<string> { };
        public double? Hysteresis { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusTypes? Status { get; set; }
        public double? Separation { get; set; }
        public string DescriptorFlags { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceTypes? Type { get; set; }
        public double? CoolDown { get; set; }
        public bool? Invert { get; set; }
        public bool? HiddenRules { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryTypes? Category { get; set; }
        public string Order { get; set; }
        public bool? Hidden { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool? CheckInterval { get; set; }
        public Guid? Uuid { get; set; }
        public double? Offset { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationTypes? OperationMode { get; set; }
        public int? Room { get; set; }
        //"defaultConfig": null,
        //"configs": null,
        //"sendOffValue": null,
        //"offValue": null,
    }
}
