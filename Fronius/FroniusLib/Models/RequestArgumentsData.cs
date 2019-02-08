// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestArgumentsData.cs" company="DTV-Online">
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
    public class RequestArgumentsData
    {
        public string DataCollection { get; set; } = string.Empty;
        public string DeviceClass { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
    }
}
