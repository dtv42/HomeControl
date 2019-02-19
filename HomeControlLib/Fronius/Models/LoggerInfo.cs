// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding selected data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class LoggerInfo : DataValue
    {
        #region Public Properties

        public string UniqueID { get; set; }
        public string ProductID { get; set; }
        public string PlatformID { get; set; }
        public string HWVersion { get; set; }
        public string SWVersion { get; set; }
        public string TimezoneLocation { get; set; }
        public string TimezoneName { get; set; }
        public int UTCOffset { get; set; }
        public string DefaultLanguage { get; set; }
        public double CashFactor { get; set; }
        public string CashCurrency { get; set; }
        public double CO2Factor { get; set; }
        public string CO2Unit { get; set; }

        #endregion
    }
}
