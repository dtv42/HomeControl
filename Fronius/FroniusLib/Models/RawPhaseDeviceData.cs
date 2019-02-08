// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhaseDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawPhaseDeviceData
    {
        public DoubleValueData CurrentL1 { get; set; } = new DoubleValueData();
        public DoubleValueData CurrentL2 { get; set; } = new DoubleValueData();
        public DoubleValueData CurrentL3 { get; set; } = new DoubleValueData();
        public DoubleValueData VoltageL1N { get; set; } = new DoubleValueData();
        public DoubleValueData VoltageL2N { get; set; } = new DoubleValueData();
        public DoubleValueData VoltageL3N { get; set; } = new DoubleValueData();
    }
}
