// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhaseData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawPhaseData
    {
        public DoubleValueData IAC_L1 { get; set; } = new DoubleValueData();
        public DoubleValueData IAC_L2 { get; set; } = new DoubleValueData();
        public DoubleValueData IAC_L3 { get; set; } = new DoubleValueData();
        public DoubleValueData UAC_L1 { get; set; } = new DoubleValueData();
        public DoubleValueData UAC_L2 { get; set; } = new DoubleValueData();
        public DoubleValueData UAC_L3 { get; set; } = new DoubleValueData();
    }
}
