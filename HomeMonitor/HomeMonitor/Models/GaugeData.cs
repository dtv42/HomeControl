// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaugeData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Models
{
    /// <summary>
    /// 
    /// </summary>
    internal class GaugeData
    {
        public string Title { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string Header { get; set; } = string.Empty;
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public double Value { get; set; }
    }
}
