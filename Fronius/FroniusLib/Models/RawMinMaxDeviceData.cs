// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinMaxDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawMinMaxDeviceData
    {
        public DoubleValueData DailyMaxVoltageDC { get; set; } = new DoubleValueData();
        public DoubleValueData DailyMaxVoltageAC { get; set; } = new DoubleValueData();
        public DoubleValueData DailyMinVoltageAC { get; set; } = new DoubleValueData();
        public DoubleValueData YearlyMaxVoltageDC { get; set; } = new DoubleValueData();
        public DoubleValueData YearlyMaxVoltageAC { get; set; } = new DoubleValueData();
        public DoubleValueData YearlyMinVoltageAC { get; set; } = new DoubleValueData();
        public DoubleValueData TotalMaxVoltageDC { get; set; } = new DoubleValueData();
        public DoubleValueData TotalMaxVoltageAC { get; set; } = new DoubleValueData();
        public DoubleValueData TotalMinVoltageAC { get; set; } = new DoubleValueData();
        public UIntValueData DailyMaxPower { get; set; } = new UIntValueData();
        public UIntValueData YearlyMaxPower { get; set; } = new UIntValueData();
        public UIntValueData TotalMaxPower { get; set; } = new UIntValueData();
    }
}
