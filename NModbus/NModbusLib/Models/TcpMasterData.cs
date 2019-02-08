// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpMasterData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusLib.Models
{
    /// <summary>
    /// Helper class holding Modbus TCP communcation data.
    /// </summary>
    public class TcpMasterData
    {
        #region Public Properties

        public bool ExclusiveAddressUse { get; set; } = true;
        public int ReceiveTimeout { get; set; } = 1000;
        public int SendTimeout { get; set; } = 1000;

        #endregion
    }
}
