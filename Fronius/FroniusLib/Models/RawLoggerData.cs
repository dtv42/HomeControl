// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawLoggerData
    {
        public string UniqueID { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public string PlatformID { get; set; } = string.Empty;
        public string HWVersion { get; set; } = string.Empty;
        public string SWVersion { get; set; } = string.Empty;
        public string TimezoneLocation { get; set; } = string.Empty;
        public string TimezoneName { get; set; } = string.Empty;
        public int UTCOffset { get; set; }
        public string DefaultLanguage { get; set; } = string.Empty;
        public double CashFactor { get; set; }
        public double DeliveryFactor { get; set; }
        public string CashCurrency { get; set; } = string.Empty;
        public double CO2Factor { get; set; }
        public string CO2Unit { get; set; } = string.Empty;
    }
}
