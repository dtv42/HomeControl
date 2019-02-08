// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RtuSlaveData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusLib.Models
{
    /// <summary>
    /// Helper class holding Modbus TCP slave data.
    /// </summary>
    public class RtuSlaveData
    {
        #region Public Properties

        public byte ID { get; set; } = 1;

        #endregion
    }
}
