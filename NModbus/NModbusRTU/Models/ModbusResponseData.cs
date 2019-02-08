// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusResponseData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusRTU.Models
{
    /// <summary>
    /// Helper class to hold all Modbus response data.
    /// </summary>
    public class ModbusResponseData<T> where T : new()
    {
        /// <summary>
        /// The Modbus request data.
        /// </summary>
        public ModbusRequestData Request { get; } = new ModbusRequestData();

        /// <summary>
        /// The Modbus data values.
        /// </summary>
        public T Value { get; } = new T();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModbusResponseData&lt;T&gt;"/> class.
        /// </summary>
        public ModbusResponseData() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModbusResponseData&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="request">The Modbus request data.</param>
        /// <param name="value">The data value.</param>
        public ModbusResponseData(ModbusRequestData request, T value)
        {
            Request = request;
            Value = value;
        }
    }

}
