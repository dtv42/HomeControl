// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotwaterData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Lib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using DataValueLib;
    using static ETAPU11Lib.Models.ETAPU11Data;

    #endregion

    public class HotwaterData : DataValue, IPropertyHelper
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

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in BoilerData.
        /// </summary>
        /// <param name="data">The ETAPU11 data.</param>
        public void Refresh(ETAPU11Data data)
        {
            if (data != null)
            {
                HotwaterTankState = data.HotwaterTankState;
                ChargingTimesState = data.ChargingTimesState;
                ChargingTimesSwitchStatus = data.ChargingTimesSwitchStatus;
                ChargingTimesTemperature = data.ChargingTimesTemperature;
                HotwaterSwitchonDiff = data.HotwaterSwitchonDiff;
                HotwaterTarget = data.HotwaterTarget;
                HotwaterTemperature = data.HotwaterTemperature;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the OverviewData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(BoilerData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the BoilerData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(BoilerData), property) != null) ? true : false;

        /// <summary>
        /// Returns the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property) => PropertyValue.GetPropertyValue(this, property);

        /// <summary>
        /// Sets the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        public void SetPropertyValue(string property, object value) => PropertyValue.SetPropertyValue(this, property, value);

        #endregion
    }
}