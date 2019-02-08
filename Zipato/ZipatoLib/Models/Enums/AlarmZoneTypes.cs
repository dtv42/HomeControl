// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmZoneTypes.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Enums
{
    /// <summary>
    ///
    /// </summary>
    public enum AlarmZoneTypes
    {
        UNKNOWN,
        INTRUDER,
        INTERIOR,
        PERIMETER,
        MOTION,
        BREAK,
        DOOR_WINDOW,
        GLASS_BREAK,
        TAMPER,
        FIRE,
        GAS,
        SMOKE,
        FLOOD,
        DURESS,
        HEALTH,
        PANIC,
        EMERGENCY,
        SAFETY,
        PROGRAM_RULE,
        DEVICE_OFFLINE,
        NETWORK_OFFLINE,
        BATTERY_WARNING,
        MAINS_FAULT,
        DEVICE_TROUBLE
    }
}
