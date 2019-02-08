// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceEndpointFlags.cs" company="DTV-Online">
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
    public enum DeviceEndpointFlags
    {
        NONE = 0,
        UNSUPPORTED = 1
    }
}
