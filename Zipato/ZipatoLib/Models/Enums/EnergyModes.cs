// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnergyModes.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Enums
{
    /// <summary>
    /// The supported device energy modes.
    /// </summary>
    public enum EnergyModes
    {
        BATTERY,
        MAINS,
        BATTERY_MAINS,
        PASSIVE,
        UNKNOWN
    }
}
