// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverviewData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Lib.Models
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds a subset of the Helios KWL EC 200 data properties.
    /// </summary>
    public class OverviewData : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// The KWLEC200 property subset.
        /// </summary>
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double TemperatureChannel { get; set; }
        public double TemperatureExhaust { get; set; }
        public double TemperatureExtract { get; set; }
        public double TemperatureOutdoor { get; set; }
        public double TemperaturePostHeater { get; set; }
        public double TemperaturePreHeater { get; set; }
        public double TemperatureSupply { get; set; }
        public KWLEC200Data.OperationModes OperationMode { get; set; }
        public KWLEC200Data.FanLevels VentilationLevel { get; set; }
        public KWLEC200Data.FanLevels SupplyLevel { get; set; }
        public KWLEC200Data.FanLevels ExhaustLevel { get; set; }
        public int VentilationPercentage { get; set; }
        public int SupplyFanSpeed { get; set; }
        public int ExhaustFanSpeed { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in OverviewData.
        /// </summary>
        /// <param name="data">The KWLEC200 data.</param>
        public void Refresh(KWLEC200Data data)
        {
            if (data != null)
            {
                Date = data.Date;
                Time = data.Time;
                TemperatureChannel = data.TemperatureChannel;
                TemperatureExhaust = data.TemperatureExhaust;
                TemperatureExtract = data.TemperatureExtract;
                TemperatureOutdoor = data.TemperatureOutdoor;
                TemperaturePostHeater = data.TemperaturePostHeater;
                TemperaturePreHeater = data.TemperaturePreHeater;
                TemperatureSupply = data.TemperatureSupply;
                OperationMode = data.OperationMode;
                VentilationLevel = data.VentilationLevel;
                SupplyLevel = data.SupplyLevel;
                ExhaustLevel = data.ExhaustLevel;
                VentilationPercentage = data.VentilationPercentage;
                SupplyFanSpeed = data.SupplyFanSpeed;
                ExhaustFanSpeed = data.ExhaustFanSpeed;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the OverviewData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(OverviewData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the OverviewData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(OverviewData), property) != null) ? true : false;

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