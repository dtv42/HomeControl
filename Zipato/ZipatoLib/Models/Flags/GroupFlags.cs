// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupFlags.cs" company="DTV-Online">
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
    public enum GroupFlags
    {
        NONE = 0,
        ENDPOINTS = 1,
        TYPE = 2,
        CONFIG = 4,
        ICONS = 8,
        INFO = 16,
        FULL = 32,
        ALL = ENDPOINTS | TYPE | CONFIG | ICONS | INFO | FULL
    }
}
