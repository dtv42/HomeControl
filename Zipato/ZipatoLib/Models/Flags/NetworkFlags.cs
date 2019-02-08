// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkFlags.cs" company="DTV-Online">
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
    public enum NetworkFlags
    {
        NONE = 0,
        CONFIG = 1,
        DEVICES = 2,
        ACTIONS = 4,
        STATE = 8,
        FULL = 16,
        ALL = CONFIG | DEVICES | ACTIONS | STATE | FULL
    }
}
