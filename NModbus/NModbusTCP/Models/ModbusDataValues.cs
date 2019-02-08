// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusDataValues.cs" company="DTV-Online">
//   Copyright(c) 2017 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusTCP.Models
{
    /// <summary>
    /// Helper class to define Modbus data values.
    /// </summary>
    public class ModbusDataValues<T> where T : new()
    {
        #region Public Properties

        /// <summary>
        /// The array of Modbus data values.
        /// </summary>
        public T[] Values { get; set; } = System.Array.Empty<T>();

        #endregion
    }
}
