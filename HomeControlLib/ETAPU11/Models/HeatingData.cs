// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeatingData.cs" company="DTV-Online">
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

    public class HeatingData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The ETAPU11 property subset.
        /// </summary>
        public double RoomSensor { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HeatingCircuitStates HeatingCircuitState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HWRunningStates RunningState { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingTimes { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OnOffStates HeatingSwitchStatus { get; set; }
        public double HeatingTemperature { get; set; }
        public double RoomTemperature { get; set; }
        public double RoomTarget { get; set; }
        public double Flow { get; set; }
        public double DayHeatingThreshold { get; set; }
        public double NightHeatingThreshold { get; set; }

        #endregion
    }
}
