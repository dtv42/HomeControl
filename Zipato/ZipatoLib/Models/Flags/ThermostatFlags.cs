// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThermostatFlags.cs" company="DTV-Online">
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
    public enum ThermostatFlags
    {
        NONE = 0,
        NETWORK = 1,
        ENDPOINTS = 2,
        CONFIG = 4,
        ICONS = 8,
        CLUSTERENDPOINTS = 16,
        OPERATIONS = 32,
        TYPE = 64,
        BINDINGS = 128,
        FULL = 512,
        ATTRIBUTES = 1024,
        ALL = NETWORK | ENDPOINTS | CONFIG | ICONS | CLUSTERENDPOINTS |
              OPERATIONS | TYPE | BINDINGS | FULL | ATTRIBUTES
    }
}
