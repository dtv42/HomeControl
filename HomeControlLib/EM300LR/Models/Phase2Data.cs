// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Phase2Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.EM300LR.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion Using Directives

    /// <summary>
    /// Class holding all data from the b-Control EM300LR energy manager.
    /// </summary>
    public class Phase2Data : DataValue
    {
        #region Public Properties

        public double ActivePowerPlus { get; set; }
        public double ActiveEnergyPlus { get; set; }
        public double ActivePowerMinus { get; set; }
        public double ActiveEnergyMinus { get; set; }
        public double ReactivePowerPlus { get; set; }
        public double ReactiveEnergyPlus { get; set; }
        public double ReactivePowerMinus { get; set; }
        public double ReactiveEnergyMinus { get; set; }
        public double ApparentPowerPlus { get; set; }
        public double ApparentEnergyPlus { get; set; }
        public double ApparentPowerMinus { get; set; }
        public double ApparentEnergyMinus { get; set; }
        public double PowerFactor { get; set; }
        public double Current { get; set; }
        public double Voltage { get; set; }

        #endregion Public Properties
    }
}
