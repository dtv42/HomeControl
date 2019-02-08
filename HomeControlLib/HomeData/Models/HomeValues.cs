// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeValues.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.HomeData.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding all home data values.
    /// </summary>
    public class HomeValues : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The actual load in kW.
        /// </summary>
        public double Load { get; set; }
        public double LoadL1 { get; set; }
        public double LoadL2 { get; set; }
        public double LoadL3 { get; set; }

        /// <summary>
        /// The actual demand in kW.
        /// </summary>
        public double Demand { get; set; }
        public double DemandL1 { get; set; }
        public double DemandL2 { get; set; }
        public double DemandL3 { get; set; }

        /// <summary>
        /// The actual generation in kW.
        /// </summary>
        public double Generation { get; set; }
        public double GenerationL1 { get; set; }
        public double GenerationL2 { get; set; }
        public double GenerationL3 { get; set; }

        /// <summary>
        /// The actual surplus in kW.
        /// </summary>
        public double Surplus { get; set; }
        public double SurplusL1 { get; set; }
        public double SurplusL2 { get; set; }
        public double SurplusL3 { get; set; }

        /// <summary>
        /// The actual line frequency in Hz.
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// The actual powerfactor (meter1).
        /// </summary>
        public double PowerFactor { get; set; }
        public double PowerFactorL1 { get; set; }
        public double PowerFactorL2 { get; set; }
        public double PowerFactorL3 { get; set; }

        #endregion
    }
}
