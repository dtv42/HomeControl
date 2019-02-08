// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceFlags.cs" company="DTV-Online">
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
    public enum DeviceFlags
    {
        NONE = 0,
        NETWORK = 1,
        ENDPOINTS = 2,
        TYPE = 4,
        CONFIG = 8,
        STATE = 16,
        ICONS = 32,
        INFO = 64,
        DESCRIPTOR = 128,
        ROOM = 256,
        UNSUPPORTED = 512,
        FULL = 1024,
        ALL = NETWORK | ENDPOINTS | TYPE | CONFIG | STATE | ICONS |
              INFO | DESCRIPTOR | ROOM | UNSUPPORTED | FULL
    }
}
