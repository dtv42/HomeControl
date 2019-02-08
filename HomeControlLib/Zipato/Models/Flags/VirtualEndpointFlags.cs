// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualEndpointFlags.cs" company="DTV-Online">
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
    public enum VirtualEndpointFlags
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
        ROOM = 512,
        INFO = 1024,
        FULL = 2048,
        ATTRIBUTES = 4096,
        ALL = NETWORK | DEVICE | CLUSTERENDPOINTS | CONFIG | ICONS | TYPE |
              BINDINGS | DESCRIPTOR | ROOM | INFO | FULL | ATTRIBUTES
    }
}
