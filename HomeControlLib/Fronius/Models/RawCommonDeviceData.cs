// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RawCommonDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    public class RawCommonDeviceData
    {
        public DoubleValueData Frequency { get; set; } = new DoubleValueData();
        public DoubleValueData CurrentDC { get; set; } = new DoubleValueData();
        public DoubleValueData CurrentAC { get; set; } = new DoubleValueData();
        public DoubleValueData VoltageDC { get; set; } = new DoubleValueData();
        public DoubleValueData VoltageAC { get; set; } = new DoubleValueData();
        public UIntValueData PowerAC { get; set; } = new UIntValueData();
        public UIntValueData DailyEnergy { get; set; } = new UIntValueData();
        public UIntValueData YearlyEnergy { get; set; } = new UIntValueData();
        public UIntValueData TotalEnergy { get; set; } = new UIntValueData();
        public DeviceStatusData DeviceStatus { get; set; } = new DeviceStatusData();
    }
}
