// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enums.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    /// <summary>
    /// Wifi signal status.
    /// </summary>
    public enum WifiSignal
    {
        Unknown,
        Bad,
        Average,
        Good,
    }

    /// <summary>
    /// Battery level.
    /// </summary>
    public enum BatteryLevel
    {
        Unknown,
        Max,
        Full,
        High,
        Medium,
        Low,
        VeryLow
    }

    /// <summary>
    /// Radio status for module.
    /// </summary>
    public enum RFSignal
    {
        Unknown,
        Low,
        Medium,
        High,
        Full
    }
}
