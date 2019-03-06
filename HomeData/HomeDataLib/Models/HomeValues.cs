// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataLib.Models
{
    #region Using Directives

    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding all home data values.
    /// </summary>
    public class HomeValues : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// The actual load in kW.
        /// </summary>
        public double Load { get; set; }
        public double LoadL1 { get; set; }
        public double LoadL2 { get; set; }
        public double LoadL3 { get; set; }

        /// <summary>
        /// The actual demand in kW.
        /// </summary>
        public double Demand { get; set; }
        public double DemandL1 { get; set; }
        public double DemandL2 { get; set; }
        public double DemandL3 { get; set; }

        /// <summary>
        /// The actual generation in kW.
        /// </summary>
        public double Generation { get; set; }
        public double GenerationL1 { get; set; }
        public double GenerationL2 { get; set; }
        public double GenerationL3 { get; set; }

        /// <summary>
        /// The actual surplus in kW.
        /// </summary>
        public double Surplus { get; set; }
        public double SurplusL1 { get; set; }
        public double SurplusL2 { get; set; }
        public double SurplusL3 { get; set; }

        /// <summary>
        /// The actual line frequency in Hz.
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// The actual powerfactor (meter1).
        /// </summary>
        public double PowerFactor { get; set; }
        public double PowerFactorL1 { get; set; }
        public double PowerFactorL2 { get; set; }
        public double PowerFactorL3 { get; set; }

        #endregion

        /// <summary>
        /// Calculates the load, demand, and surplus values.
        /// </summary>
        /// <remarks>
        /// The meter1 contains data from the total consumption energy manager.
        /// The meter2 contains data from the solar generation energy manager.
        /// </remarks>
        /// <param name="meter1"></param>
        /// <param name="meter2"></param>
        public void Refresh(MeterData meter1, MeterData meter2)
        {
            Demand = meter1.Total.ActivePowerPlus / 1000.0;
            DemandL1 = meter1.Phase1.ActivePowerPlus / 1000.0;
            DemandL2 = meter1.Phase2.ActivePowerPlus / 1000.0;
            DemandL3 = meter1.Phase3.ActivePowerPlus / 1000.0;

            Surplus = meter1.Total.ActivePowerMinus / 1000.0;
            SurplusL1 = meter1.Phase1.ActivePowerMinus / 1000.0;
            SurplusL2 = meter1.Phase2.ActivePowerMinus / 1000.0;
            SurplusL3 = meter1.Phase3.ActivePowerMinus / 1000.0;

            Generation = meter2.Total.ActivePowerPlus / 1000.0;
            GenerationL1 = meter2.Phase1.ActivePowerPlus / 1000.0;
            GenerationL2 = meter2.Phase2.ActivePowerPlus / 1000.0;
            GenerationL3 = meter2.Phase3.ActivePowerPlus / 1000.0;

            Load = Demand + Generation - Surplus;
            LoadL1 = DemandL1 + GenerationL1 - SurplusL1;
            LoadL2 = DemandL2 + GenerationL2 - SurplusL2;
            LoadL3 = DemandL3 + GenerationL3 - SurplusL3;

            Frequency = meter1.Total.SupplyFrequency;
            PowerFactor = meter1.Total.PowerFactor;
            PowerFactorL1 = meter1.Phase1.PowerFactor;
            PowerFactorL2 = meter1.Phase2.PowerFactor;
            PowerFactorL3 = meter1.Phase3.PowerFactor;
        }

        #region Public Property Helper

        /// <summary>
        /// Gets the property list for the HomeValues class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(HomeValues).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the ETAPU11Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(HomeValues), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(HomeValues), property);

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
