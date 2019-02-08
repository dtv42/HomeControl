// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotWaterData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.ETAPU11.Models
{
    #region Using Directives

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using DataValueLib;
    using static HomeControlLib.ETAPU11.Models.ETAPU11Data;

    #endregion

    public class HotwaterData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The ETAPU11 property subset.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HWTankStates HotwaterTankState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates ChargingTimesState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates ChargingTimesSwitchStatus { get; set; }
        public double ChargingTimesTemperature { get; set; }
        public double HotwaterSwitchonDiff { get; set; }
        public double HotwaterTarget { get; set; }
        public double HotwaterTemperature { get; set; }

        #endregion
    }
}
