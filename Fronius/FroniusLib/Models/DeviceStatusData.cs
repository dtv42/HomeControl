// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceStatusData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class DeviceStatusData
    {
        public int StatusCode { get; set; }
        public int MgmtTimerRemainingTime { get; set; }
        public int ErrorCode { get; set; }
        public int LEDColor { get; set; }
        public int LEDState { get; set; }
        public bool StateToReset { get; set; }
    }
}
