// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoilerData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.ETAPU11.Models
{
    #region Using Directives

    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using DataValueLib;
    using static HomeControlLib.ETAPU11.Models.ETAPU11Data;

    #endregion

    public class BoilerData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The ETAPU11 property subset.
        /// </summary>
        public TimeSpan FullLoadHours { get; set; }
        public double TotalConsumed { get; set; }
        public double ConsumptionSinceDeAsh { get; set; }
        public double ConsumptionSinceAshBoxEmptied { get; set; }
        public double ConsumptionSinceMaintainence { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StartValues HopperFillUpPelletBin { get; set; }
        public double HopperPelletBinContents { get; set; }
        public TimeSpan HopperFillUpTime { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BoilerStates BoilerState { get; set; }
        public double BoilerPressure { get; set; }
        public double BoilerTemperature { get; set; }
        public double BoilerTarget { get; set; }
        public double BoilerBottom { get; set; }
        public double FlueGasTemperature { get; set; }
        public double DraughtFanSpeed { get; set; }
        public double ResidualO2 { get; set; }

        #endregion
    }
}
