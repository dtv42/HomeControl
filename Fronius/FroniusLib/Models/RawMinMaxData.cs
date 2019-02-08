// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinMaxData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawMinMaxData
    {
        public DoubleValueData DAY_UDCMAX { get; set; } = new DoubleValueData();
        public DoubleValueData DAY_UACMAX { get; set; } = new DoubleValueData();
        public DoubleValueData DAY_UACMIN { get; set; } = new DoubleValueData();
        public DoubleValueData YEAR_UDCMAX { get; set; } = new DoubleValueData();
        public DoubleValueData YEAR_UACMAX { get; set; } = new DoubleValueData();
        public DoubleValueData YEAR_UACMIN { get; set; } = new DoubleValueData();
        public DoubleValueData TOTAL_UDCMAX { get; set; } = new DoubleValueData();
        public DoubleValueData TOTAL_UACMAX { get; set; } = new DoubleValueData();
        public DoubleValueData TOTAL_UACMIN { get; set; } = new DoubleValueData();
        public UIntValueData DAY_PMAX { get; set; } = new UIntValueData();
        public UIntValueData YEAR_PMAX { get; set; } = new UIntValueData();
        public UIntValueData TOTAL_PMAX { get; set; } = new UIntValueData();
    }
}
