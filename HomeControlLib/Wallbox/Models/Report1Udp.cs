// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report1Udp.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Wallbox.Models
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// {
    ///   "ID": 1,
    ///   "Product": "BMW-10-EC2405B2-E1R",
    ///   "Serial": "18711747",
    ///   "Firmware": "P30 v 3.9.17 (180601-115841)",
    ///   "COM-module": 1,
    ///   "Backend": 1,
    ///   "timeQ": 3,
    ///   "DIP-Sw": "0x2600",
    ///   "Sec": 934129
    /// }
    /// </summary>
    public class Report1Udp
    {
        #region Public Properties

        /// <summary>
        /// 1 - ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// String(32 chars) - Product name as defined by the manufacturer
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// String (8 chars) - Serial number of the device
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// String (32 chars) - Firmware version of the device
        /// </summary>
        public string Firmware { get; set; } = string.Empty;

        /// <summary>
        /// 0 - No communication module is present.
        /// 1 - Communication module is present.
        /// </summary>
        [JsonProperty("COM-module")]
        public ushort ComModule { get; set; }

        /// <summary>
        /// 0 No backend communication is present.
        /// 1 Backend communication is present.
        /// </summary>
        public ushort Backend { get; set; }

        /// <summary>
        /// 0 - Not synced time.
        /// X - Strong synced time.
        /// 2 - Weak synced time.
        /// </summary>
        [JsonProperty("timeQ")]
        public ushort TimeQ { get; set; }

        /// <summary>
        /// Typically a HEX number e.g. "0x2600" (undocumented).
        /// </summary>
        [JsonProperty("DIP-Sw")]
        public string DipSW { get; set; } = string.Empty;

        /// <summary>
        /// uint32 - Current state of the system clock in seconds
        ///          from the last startup of the device.
        /// </summary>
        public uint Sec { get; set; }

        #endregion
    }
}
