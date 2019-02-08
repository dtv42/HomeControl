// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverviewData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.KWLEC200.Models
{
    #region Using Directives

    using System;

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds a subset of the Helios KWL EC 200 data properties.
    /// </summary>
    public class OverviewData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The KWLEC200 property subset.
        /// </summary>
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double TemperatureChannel { get; set; }
        public double TemperatureExhaust { get; set; }
        public double TemperatureExtract { get; set; }
        public double TemperatureOutdoor { get; set; }
        public double TemperaturePostHeater { get; set; }
        public double TemperaturePreHeater { get; set; }
        public double TemperatureSupply { get; set; }
        public KWLEC200Data.OperationModes OperationMode { get; set; }
        public KWLEC200Data.FanLevels VentilationLevel { get; set; }
        public KWLEC200Data.FanLevels SupplyLevel { get; set; }
        public KWLEC200Data.FanLevels ExhaustLevel { get; set; }
        public int VentilationPercentage { get; set; }
        public int SupplyFanSpeed { get; set; }
        public int ExhaustFanSpeed { get; set; }

        #endregion
    }
}
