// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmPartitionFlags.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Flags
{
    using System;

    [Flags]
    public enum AlarmPartitionFlags
    {
        NONE = 0,
        ALARM = 1,
        ZONES = 2,
        CONTROL = 4,
        ATTRIBUTES = 8,
        CONFIG = 16,
        STATE = 32,
        FULL = 64,
        ALL = ALARM | ZONES | CONTROL | ATTRIBUTES | CONFIG | STATE | FULL
    }
}
