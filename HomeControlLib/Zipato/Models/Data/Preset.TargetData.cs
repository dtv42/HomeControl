// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Preset.TargetData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data.Preset
{
    public class PresetMap
    {
        public TargetData Heating { get; set; }
        public TargetData Cooling { get; set; }
        public TargetData Humidification { get; set; }
        public TargetData Dehumidification { get; set; }
        public TargetData Ventilation { get; set; }
    }
}
