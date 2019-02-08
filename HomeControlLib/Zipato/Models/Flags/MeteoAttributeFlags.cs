// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeteoAttributeFlags.cs" company="DTV-Online">
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
    public enum MeteoAttributeFlags
    {
        NONE = 0,
        DEFINITION = 1,
        CONFIG = 2,
        VALUE = 4,
        ALL = DEFINITION | CONFIG | VALUE
    }
}
