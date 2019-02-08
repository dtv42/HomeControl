// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScaleFactors.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SunSpecLib
{
    #region Using Directives

    using System;

    #endregion

    public enum ScaleFactors : ushort
    {
        NotImplemented = 0x8000,      // 0x8000
        Minus10 = 0xFFF6,             // -10
        Minus9 = 0xFFF7,              // -9
        Minus8 = 0xFFF8,              // -8
        Minus7 = 0xFFF9,              // -7
        Minus6 = 0xFFFA,              // -6
        Minus5 = 0xFFFB,              // -5
        Minus4 = 0xFFFC,              // -4
        Minus3 = 0xFFFD,              // -3
        Minus2 = 0xFFFE,              // -2
        Minus1 = 0xFFFF,              // -1
        Zero = 0x0000,                //  0
        Plus1 = 0x0001,               //  1
        Plus2 = 0x0002,               //  2
        Plus3 = 0x0003,               //  3
        Plus4 = 0x0004,               //  4
        Plus5 = 0x0005,               //  5
        Plus6 = 0x0006,               //  6
        Plus7 = 0x0007,               //  7
        Plus8 = 0x0008,               //  8
        Plus9 = 0x0009,               //  9
        Plus10 = 0x000A,              //  10
    }
}
