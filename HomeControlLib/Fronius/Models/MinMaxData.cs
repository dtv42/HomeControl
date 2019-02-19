// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinMaxData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding selected data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class MinMaxData : DataValue
    {
        #region Public Properties

        public double DailyMaxVoltageDC { get; set; }
        public double DailyMaxVoltageAC { get; set; }
        public double DailyMinVoltageAC { get; set; }
        public double YearlyMaxVoltageDC { get; set; }
        public double YearlyMaxVoltageAC { get; set; }
        public double YearlyMinVoltageAC { get; set; }
        public double TotalMaxVoltageDC { get; set; }
        public double TotalMaxVoltageAC { get; set; }
        public double TotalMinVoltageAC { get; set; }
        public double DailyMaxPower { get ; set; }
        public double YearlyMaxPower { get; set; }
        public double TotalMaxPower { get; set; }

        #endregion
    }
}
