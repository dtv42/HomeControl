// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmPartitionZoneFlags.cs" company="DTV-Online">
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
    public enum AlarmPartitionZoneFlags
    {
        NONE = 0,
        NETWORK = 1,
        DEVICE = 2,
        ENDPOINT = 4,
        CLUSTERENDPOINT = 8,
        ATTRIBUTE = 16,
        CONFIG = 32,
        VALUE = 64,
        STATE = 128,
        FULL = 256,
        ALL = NETWORK | DEVICE | ENDPOINT | CLUSTERENDPOINT | 
              ATTRIBUTE | CONFIG | VALUE | STATE | FULL
    }
}
