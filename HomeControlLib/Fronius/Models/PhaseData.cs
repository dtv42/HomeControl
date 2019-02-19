// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhaseData.cs" company="DTV-Online">
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
    public class PhaseData : DataValue
    {
        #region Public Properties

        public double CurrentL1 { get; set; }
        public double CurrentL2 { get; set; }
        public double CurrentL3 { get; set; }
        public double VoltageL1N { get; set; }
        public double VoltageL2N { get; set; }
        public double VoltageL3N { get; set; }

        #endregion
    }
}
