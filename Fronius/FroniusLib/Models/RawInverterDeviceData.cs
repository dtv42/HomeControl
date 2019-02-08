// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InverterDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawInverterDeviceData
    {
        /// <summary>
        /// The index of the inverter.
        /// </summary>
        public string Index { get; set; } = string.Empty;

        /// <summary>
        /// Device type of the inverter.
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// PV power connected to this inverter (in watts). If none defined , default power for this DT is used.
        /// </summary>
        public int PVPower { get; set; }

        /// <summary>
        /// Custom name of the inverter, assigned by the customer.
        /// Note: This property will not be present or empty if device does not support custom names.
        /// </summary>
        public string CustomName { get; set; } = string.Empty;

        /// <summary>
        /// Whether the device shall be displayed in visualizations according to customer settings.
        /// Note: This property may not be present if device does not support visualization settings.
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// Unique ID of the inverter (e.g. serial number).
        /// </summary>
        public string UniqueID { get; set; } = string.Empty;

        /// <summary>
        /// Error code that is currently present on inverter. A value of -1 means that there is no valid error code.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Status code reflecting the operational state of the inverter.
        /// </summary>
        public StatusCodes StatusCode { get; set; } = new StatusCodes();
    }
}
