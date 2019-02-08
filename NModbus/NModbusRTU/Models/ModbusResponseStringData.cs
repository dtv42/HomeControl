// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusResponseStringData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusRTU.Models
{
    /// <summary>
    /// Helper class to hold Modbus string response data.
    /// </summary>
    public class ModbusResponseStringData
    {
        /// <summary>
        /// The Modbus request data.
        /// </summary>
        public ModbusRequestData Request { get; } = new ModbusRequestData();

        /// <summary>
        /// The Modbus data values.
        /// </summary>
        public string Value { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModbusResponseStringData"/> class.
        /// </summary>
        public ModbusResponseStringData() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModbusResponseStringData"/> class.
        /// </summary>
        /// <param name="request">The Modbus request data.</param>
        /// <param name="value">The data value.</param>
        public ModbusResponseStringData(ModbusRequestData request, string value)
        {
            this.Request = request;
            this.Value = value;
        }
    }
}
