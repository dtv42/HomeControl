// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoilerData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Lib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using DataValueLib;
    using static ETAPU11Lib.Models.ETAPU11Data;

    #endregion

    public class BoilerData : DataValue, IPropertyHelper
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

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in BoilerData.
        /// </summary>
        /// <param name="data">The ETAPU11 data.</param>
        public void Refresh(ETAPU11Data data)
        {
            if (data != null)
            {
                FullLoadHours = data.FullLoadHours;
                TotalConsumed = data.TotalConsumed;
                ConsumptionSinceDeAsh = data.ConsumptionSinceDeAsh;
                ConsumptionSinceAshBoxEmptied = data.ConsumptionSinceAshBoxEmptied;
                ConsumptionSinceMaintainence = data.ConsumptionSinceMaintainence;
                HopperFillUpPelletBin = data.HopperFillUpPelletBin;
                HopperPelletBinContents = data.HopperPelletBinContents;
                HopperFillUpTime = data.HopperFillUpTime;
                BoilerState = data.BoilerState;
                BoilerPressure = data.BoilerPressure;
                BoilerTemperature = data.BoilerTemperature;
                BoilerTarget = data.BoilerTarget;
                BoilerBottom = data.BoilerBottom;
                FlueGasTemperature = data.FlueGasTemperature;
                DraughtFanSpeed = data.DraughtFanSpeed;
                ResidualO2 = data.ResidualO2;
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