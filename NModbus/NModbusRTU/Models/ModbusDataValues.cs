// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusDataValues.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusRTU.Models
{
    /// <summary>
    /// Helper class to define Modbus data values.
    /// </summary>
    public class ModbusDataValues<T> where T : new()
    {
        /// <summary>
        /// The array of Modbus data values.
        /// </summary>
        public T[] Values { get; set; } = new T[] { };
    }
}
