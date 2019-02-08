// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding all data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class FroniusData : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// The CommonData property holds all Fronius common data.
        /// </summary>
        public CommonDeviceData CommonData { get; set; } = new CommonDeviceData();

        /// <summary>
        /// The InverterInfo holds all Fronius inverter data.
        /// </summary>
        public InverterDeviceData InverterInfo { get; set; } = new InverterDeviceData();

        /// <summary>
        /// The LoggerInfo property holds all Fronius logger info data.
        /// </summary>
        public LoggerDeviceData LoggerInfo { get; set; } = new LoggerDeviceData();

        /// <summary>
        /// The PhaseData property holds all Fronius phase data.
        /// </summary>
        public PhaseDeviceData PhaseData { get; set; } = new PhaseDeviceData();

        /// <summary>
        /// The MinMaxData property holds all Fronius minmax data.
        /// </summary>
        public MinMaxDeviceData MinMaxData { get; set; } = new MinMaxDeviceData();

        #endregion Public Properties

        #region Public Property Helper

        /// <summary>
        /// Gets the property list for the FroniusData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(FroniusData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the FroniusData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(FroniusData), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(FroniusData), property);

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

        #endregion Public Property Helper
    }
}
