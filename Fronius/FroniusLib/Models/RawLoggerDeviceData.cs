// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawLoggerDeviceData
    {
        /// <summary>
        /// Unique ID of the logging Logger.
        /// </summary>
        public string UniqueID { get; set; } = string.Empty;

        /// <summary>
        /// String identifying the exact product type.
        /// </summary>
        public string ProductID { get; set; } = string.Empty;

        /// <summary>
        /// String identifying the exact hardware platform.
        /// </summary>
        public string PlatformID { get; set; } = string.Empty;

        /// <summary>
        /// Hardware version of the logging Logger.
        /// </summary>
        public string HWVersion { get; set; } = string.Empty;

        /// <summary>
        /// Software version of the logging Logger.
        /// </summary>
        public string SWVersion { get; set; } = string.Empty;

        /// <summary>
        /// Name of city/country which the user selected as time zone.
        /// </summary>
        public string TimezoneLocation { get; set; } = string.Empty;

        /// <summary>
        /// Name of the selected time zone. May be empty if information not available.
        /// </summary>
        public string TimezoneName { get; set; } = string.Empty;

        /// <summary>
        /// UTC offset in seconds east of UTC, including adjustments for daylight saving.
        /// </summary>
        public int UTCOffset { get; set; }

        /// <summary>
        /// Default language set on the logging device as a two letter abbreviation (e.g. "en").
        /// </summary>
        public string DefaultLanguage { get; set; } = string.Empty;

        /// <summary>
        /// The cash factor set on the logging device, NOT the factor set on the inverters.
        /// </summary>
        public double CashFactor { get; set; }

        /// <summary>
        /// Currency of cash factor set on the logging device, NOT the currency set on the inverters.
        /// </summary>
        public string CashCurrency { get; set; } = string.Empty;

        /// <summary>
        /// The CO2 factor set on the logging device, NOT the factor set on the inverters.
        /// </summary>
        public double CO2Factor { get; set; }

        /// <summary>
        /// Unit of CO2 factor set on the logging device, NOT the unit set on the inverters.
        /// </summary>
        public string CO2Unit { get; set; } = string.Empty;
    }
}
