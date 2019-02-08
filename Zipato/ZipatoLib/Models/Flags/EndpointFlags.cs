// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointFlags.cs" company="DTV-Online">
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
    public enum EndpointFlags
    {
        NONE = 0,
        NETWORK = 1,
        DEVICE = 2,
        CLUSTERENDPOINTS = 4,
        CONFIG = 8,
        ICONS = 16,
        TYPE = 32,
        BINDINGS = 64,
        DESCRIPTOR = 128,
        ROOM = 256,
        INFO = 512,
        ACTIONS = 1024,
        UNSUPPORTED = 2048,
        FULL = 4096,
        ATTRIBUTES = 8192,
        EVENTS = 16384,
        ALL = NETWORK | DEVICE | CLUSTERENDPOINTS | CONFIG | ICONS | TYPE | BINDINGS |
              DESCRIPTOR | ROOM | INFO | ACTIONS | UNSUPPORTED | FULL | ATTRIBUTES | EVENTS
    }
}
