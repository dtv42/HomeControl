// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding all data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class FroniusData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The CommonData property holds all Fronius common data.
        /// </summary>
        public CommonDeviceData CommonData { get; set; } = new CommonDeviceData();

        /// <summary>
        /// The InverterInfo holds all Fronius inverter data.
        /// </summary>
        public InverterDeviceData InverterInfo { get; set; } = new InverterDeviceData();

        /// <summary>
        /// The LoggerInfo property holds all Fronius logger info data.
        /// </summary>
        public LoggerDeviceData LoggerInfo { get; set; } = new LoggerDeviceData();

        /// <summary>
        /// The PhaseData property holds all Fronius phase data.
        /// </summary>
        public PhaseDeviceData PhaseData { get; set; } = new PhaseDeviceData();

        /// <summary>
        /// The MinMaxData property holds all Fronius minmax data.
        /// </summary>
        public MinMaxDeviceData MinMaxData { get; set; } = new MinMaxDeviceData();

        #endregion Public Properties
    }
}
