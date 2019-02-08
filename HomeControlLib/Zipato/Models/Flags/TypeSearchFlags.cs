// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeSearchFlags.cs" company="DTV-Online">
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
    public enum TypeSearchFlags
    {
        NONE = 0,
        TS = 1,
        HIDDEN = 2
    }
}
