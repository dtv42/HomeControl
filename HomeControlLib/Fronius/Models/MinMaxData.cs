// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinMaxData.cs" company="DTV-Online">
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
    public class MinMaxData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public double DailyMaxVoltageDC { get; set; }
        public double DailyMaxVoltageAC { get; set; }
        public double DailyMinVoltageAC { get; set; }
        public double YearlyMaxVoltageDC { get; set; }
        public double YearlyMaxVoltageAC { get; set; }
        public double YearlyMinVoltageAC { get; set; }
        public double TotalMaxVoltageDC { get; set; }
        public double TotalMaxVoltageAC { get; set; }
        public double TotalMinVoltageAC { get; set; }
        public double DailyMaxPower { get ; set; }
        public double YearlyMaxPower { get; set; }
        public double TotalMaxPower { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in MinMaxData.
        /// </summary>
        /// <param name="data">The Fronius data.</param>
        public void Refresh(FroniusData data)
        {
            if (data != null)
            {
                DailyMaxVoltageDC = data.MinMaxData.Inverter.DailyMaxVoltageDC.Value;
                DailyMaxVoltageAC = data.MinMaxData.Inverter.DailyMaxVoltageAC.Value;
                DailyMinVoltageAC = data.MinMaxData.Inverter.DailyMinVoltageAC.Value;
                YearlyMaxVoltageDC = data.MinMaxData.Inverter.YearlyMaxVoltageDC.Value;
                YearlyMaxVoltageAC = data.MinMaxData.Inverter.YearlyMaxVoltageAC.Value;
                YearlyMinVoltageAC = data.MinMaxData.Inverter.YearlyMinVoltageAC.Value;
                TotalMaxVoltageDC = data.MinMaxData.Inverter.TotalMaxVoltageDC.Value;
                TotalMaxVoltageAC = data.MinMaxData.Inverter.TotalMaxVoltageAC.Value;
                TotalMinVoltageAC = data.MinMaxData.Inverter.TotalMinVoltageAC.Value;
                DailyMaxPower = data.MinMaxData.Inverter.DailyMaxPower.Value;
                YearlyMaxPower = data.MinMaxData.Inverter.YearlyMaxPower.Value;
                TotalMaxPower = data.MinMaxData.Inverter.TotalMaxPower.Value;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the MinMaxData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(MinMaxData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the MinMaxData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(MinMaxData), property) != null) ? true : false;

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
