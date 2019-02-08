// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageData.cs" company="DTV-Online">
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

    public class StorageData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The ETAPU11 property subset.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DemandValuesEx DischargeScrewDemand { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ScrewStates DischargeScrewState { get; set; }
        public double DischargeScrewMotorCurr { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ConveyingSystemStates ConveyingSystem { get; set; }
        public double Stock { get; set; }
        public double StockWarningLimit { get; set; }

        #endregion
    }
}
