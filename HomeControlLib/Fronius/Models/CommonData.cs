// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonData.cs" company="DTV-Online">
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
    public class CommonData : DataValue
    {
        #region Public Properties

        public double Frequency { get; set; }
        public double CurrentDC { get; set; }
        public double CurrentAC { get; set; }
        public double VoltageDC { get; set; }
        public double VoltageAC { get; set; }
        public uint PowerAC { get; set; }
        public uint DailyEnergy { get; set; }
        public uint YearlyEnergy { get; set; }
        public uint TotalEnergy { get; set; }
        public StatusCodes StatusCode { get; set; } = new StatusCodes();

        #endregion
    }
}
