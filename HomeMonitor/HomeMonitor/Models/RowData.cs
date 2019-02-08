// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMonitor.Models
{
    /// <summary>
    /// 
    /// </summary>
    internal class RowData
    {
        public string Caption { get; set; } = string.Empty;
        public GaugeData Gauge1 { get; set; } = new GaugeData();
        public GaugeData Gauge2 { get; set; } = new GaugeData();
        public GaugeData Gauge3 { get; set; } = new GaugeData();
    }
}
