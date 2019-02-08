// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhaseData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
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
    public class PhaseData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public double CurrentL1 { get; set; }
        public double CurrentL2 { get; set; }
        public double CurrentL3 { get; set; }
        public double VoltageL1N { get; set; }
        public double VoltageL2N { get; set; }
        public double VoltageL3N { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in PhaseData.
        /// </summary>
        /// <param name="data">The Fornius data.</param>
        public void Refresh(FroniusData data)
        {
            if (data != null)
            {
                CurrentL1 = data.PhaseData.Inverter.CurrentL1.Value;
                CurrentL2 = data.PhaseData.Inverter.CurrentL2.Value;
                CurrentL3 = data.PhaseData.Inverter.CurrentL3.Value;
                VoltageL1N = data.PhaseData.Inverter.VoltageL1N.Value;
                VoltageL2N = data.PhaseData.Inverter.VoltageL2N.Value;
                VoltageL3N = data.PhaseData.Inverter.VoltageL3N.Value;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the PhaseData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(PhaseData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the PhaseData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(PhaseData), property) != null) ? true : false;

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
