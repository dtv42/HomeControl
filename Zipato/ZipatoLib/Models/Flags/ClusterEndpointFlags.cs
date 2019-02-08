// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClusterEndpointFlags.cs" company="DTV-Online">
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
    public enum ClusterEndpointFlags
    {
        NONE = 0,
        CONFIG = 1,
        NETWORK = 2,
        DEVICE = 4,
        ENDPOINT = 8,
        ATTRIBUTES = 16,
        ROOM = 32,
        ICONS = 64,
        DESCRIPTOR = 128,
        INFO = 256,
        ACTIONS = 512,
        EVENTS = 1024,
        FULL = 2048,
        ALL = CONFIG | NETWORK | DEVICE | ENDPOINT | ATTRIBUTES | ROOM |
              ICONS | DESCRIPTOR | INFO | ACTIONS | EVENTS | FULL
    }
}
