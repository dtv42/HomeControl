// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RtuMasterData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusLib.Models
{
    /// <summary>
    /// Helper class holding Modbus RTU communcation data.
    /// </summary>
    public class RtuMasterData
    {
        #region Public Properties

        public string SerialPort { get; set; } = string.Empty;
        public int Baudrate { get; set; } = 9600;
        public System.IO.Ports.Parity Parity { get; set; } = System.IO.Ports.Parity.None;
        public int DataBits { get; set; } = 8;
        public System.IO.Ports.StopBits StopBits { get; set; } = System.IO.Ports.StopBits.One;

        public int ReadTimeout { get; set; } = -1;
        public int WriteTimeout { get; set; } = -1;
        public int Retries { get; set; }
        public int WaitToRetryMilliseconds { get; set; }
        public bool SlaveBusyUsesRetryCount { get; set; }

        #endregion
    }
}
