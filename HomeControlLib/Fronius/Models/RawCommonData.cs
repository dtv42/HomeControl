// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RawCommonData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    public class RawCommonData
    {
        public DoubleValueData FAC { get; set; } = new DoubleValueData();
        public DoubleValueData IDC { get; set; } = new DoubleValueData();
        public DoubleValueData IAC { get; set; } = new DoubleValueData();
        public DoubleValueData UDC { get; set; } = new DoubleValueData();
        public DoubleValueData UAC { get; set; } = new DoubleValueData();
        public UIntValueData PAC { get; set; } = new UIntValueData();
        public UIntValueData DAY_ENERGY { get; set; } = new UIntValueData();
        public UIntValueData YEAR_ENERGY { get; set; } = new UIntValueData();
        public UIntValueData TOTAL_ENERGY { get; set; } = new UIntValueData();
        public DeviceStatusData DeviceStatus { get; set; } = new DeviceStatusData();
    }
}
