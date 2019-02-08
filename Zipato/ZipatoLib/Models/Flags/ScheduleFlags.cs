// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleFlags.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Flags
{
    using System;

    [Flags]
    public enum ScheduleFlags
    {
        NONE = 0,
        CONFIG = 1,
        DEVICES = 2,
        STATE = 4,
        FULL = 8,
        ALL = CONFIG | DEVICES | STATE | FULL
    }
}
