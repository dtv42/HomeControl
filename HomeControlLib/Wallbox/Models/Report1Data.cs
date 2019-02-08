// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report1Data.cs" company="DTV-Online">
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

    public class Report1Data : DataValue
    {
        #region Public Properties

        /// <summary>
        /// ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// Product name as defined by the manufacturer
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Serial number of the device
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// Firmware version of the device
        /// </summary>
        public string Firmware { get; set; } = string.Empty;

        /// <summary>
        /// Communication module.
        /// </summary>
        public ComModulePresent ComModule { get; set; }

        /// <summary>
        /// Backend communication status.
        /// </summary>
        public BackendPresent Backend { get; set; }

        /// <summary>
        /// Synced time.
        /// </summary>
        public ushort TimeQ { get; set; }

        /// <summary>
        /// "DIP-Sw": "0x2600" (undocumented).
        /// </summary>
        public DipSwitches DIPSwitch { get; set; }

        /// <summary>
        /// Current state of the system clock in seconds from the last startup of the device.
        /// </summary>
        public uint Seconds { get; set; }

        #endregion
    }
}
