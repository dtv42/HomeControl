// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report3Udp.cs" company="DTV-Online">
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
    ///  "ID": 3,
    ///  "U1": 0,
    ///  "U2": 0,
    ///  "U3": 0,
    ///  "I1": 0,
    ///  "I2": 0,
    ///  "I3": 0,
    ///  "P": 0,
    ///  "PF": 0,
    ///  "E pres": 0,
    ///  "E total": 0,
    ///  "Serial": "18711747",
    ///  "Sec": 935093
    ///}
    /// </summary>
    public class Report3Udp
    {
        #region Public Properties

        /// <summary>
        /// 3 - ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// int - (3 digits) Measured voltage value on phase 1 in V
        /// </summary>
        public int U1 { get; set; }

        /// <summary>
        /// int - (3 digits) Measured voltage value on phase 2 in V
        /// </summary>
        public int U2 { get; set; }

        /// <summary>
        /// int - (3 digits) Measured voltage value on phase 3 in V
        /// </summary>
        public int U3 { get; set; }

        /// <summary>
        /// int - (5 digits) Measured current value on phase 1 in mA
        /// </summary>
        public int I1 { get; set; }

        /// <summary>
        /// int - (5 digits) Measured current value on phase 2 in mA
        /// </summary>
        public int I2 { get; set; }

        /// <summary>
        /// int - (5 digits) Measured current value on phase 3 in mA
        /// </summary>
        public int I3 { get; set; }

        /// <summary>
        /// uint32 - (8 digits) Power in mW (effective power).
        /// </summary>
        public uint P { get; set; }

        /// <summary>
        /// int - (4 digits) Possible values: 0 - 1000
        ///       Current power factor(cosphi). The unit displayed is 0.1%.
        /// </summary>
        public int PF { get; set; }

        /// <summary>
        /// uint32 - Energy transferred in the current charging session in 0.1 Wh.
        ///          This value is reset at the beginning of a new charging session.
        ///          Possible values: 0 - 999999999
        /// </summary>
        [JsonProperty("E Pres")]
        public uint Epres { get; set; }

        /// <summary>
        /// uint32 - Total energy consumption(persistent, device related) in 0.1 Wh.
        ///          Possible values: 0 - 999999999
        /// </summary>
        [JsonProperty("E total")]
        public uint Etotal { get; set; }

        /// <summary>
        /// String (8 chars) - Serial number of the device
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// uint32 - Current state of the system clock in seconds
        ///          from the last startup of the device.
        /// </summary>
        public uint Sec { get; set; }

        #endregion
    }
}
