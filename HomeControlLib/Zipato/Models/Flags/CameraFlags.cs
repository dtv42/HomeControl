// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraFlags.cs" company="DTV-Online">
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
    public enum CameraFlags
    {
        NONE = 0,
        LOCAL = 1,
        NETWORK = 2,
        DEVICE = 4,
        CLUSTERENDPOINTS = 8,
        CONFIG = 16,
        ICONS = 32,
        DESCRIPTOR = 64,
        ROOM = 128,
        FULL = 256,
        ATTRIBUTES = 512,
        ALL = LOCAL | NETWORK | DEVICE | CLUSTERENDPOINTS | CONFIG |
              ICONS | DESCRIPTOR | ROOM | FULL | ATTRIBUTES
    }
}
