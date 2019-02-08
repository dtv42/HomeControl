// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report3Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Wallbox.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    public class Report3Data : DataValue
    {
        #region Public Properties

        /// <summary>
        /// ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// Measured voltage value on phase 1 in V
        /// </summary>
        public double VoltageL1N { get; set; }

        /// <summary>
        /// Measured voltage value on phase 2 in V
        /// </summary>
        public double VoltageL2N { get; set; }

        /// <summary>
        /// Measured voltage value on phase 3 in V
        /// </summary>
        public double VoltageL3N { get; set; }

        /// <summary>
        /// Measured current value on phase 1 in A
        /// </summary>
        public double CurrentL1 { get; set; }

        /// <summary>
        /// Measured current value on phase 2 in A
        /// </summary>
        public double CurrentL2 { get; set; }

        /// <summary>
        /// Measured current value on phase 3 in A
        /// </summary>
        public double CurrentL3 { get; set; }

        /// <summary>
        /// Power in W (effective power).
        /// </summary>
        public double Power { get; set; }

        /// <summary>
        /// Current power factor(cosphi).
        /// </summary>
        public double PowerFactor { get; set; }

        /// <summary>
        /// Energy transferred in the current charging session in Wh.
        /// </summary>
        public double EnergyCharging { get; set; }

        /// <summary>
        /// Total energy consumption(persistent, device related) in Wh.
        /// </summary>
        public double EnergyTotal { get; set; }

        /// <summary>
        /// Serial number of the device.
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// Current state of the system clock in seconds from the last startup of the device.
        /// </summary>
        public uint Seconds { get; set; }

        #endregion
    }
}
