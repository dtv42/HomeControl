// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIntValueData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace FroniusLib.Models
{
    public class UIntValueData
    {
        public uint Value { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
