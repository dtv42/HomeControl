// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeFlags.cs" company="DTV-Online">
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
    public enum AttributeFlags
    {
        NONE = 0,
        NETWORK = 1,
        DEVICE = 2,
        ENDPOINT = 4,
        CLUSTERENDPOINT = 8,
        DEFINITION = 16,
        CONFIG = 32,
        ROOM = 64,
        ICONS = 128,
        VALUE = 256,
        PARENT = 512,
        CHILDREN = 1024,
        FULL = 2048,
        TYPE = 4096,
        ALL = NETWORK | DEVICE | ENDPOINT | CLUSTERENDPOINT | DEFINITION |
              CONFIG | ROOM | ICONS | VALUE | PARENT | CHILDREN | FULL | TYPE
    }
}
