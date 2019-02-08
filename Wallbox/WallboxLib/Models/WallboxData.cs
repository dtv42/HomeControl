// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds a the mapped Wallbox data.
    /// </summary>
    public class WallboxData : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// The Report1 property holds all Wallbox UDP data from report 1.
        /// </summary>
        public Report1Udp Report1 { get; set; } = new Report1Udp();

        /// <summary>
        /// The Report2 property holds all Wallbox UDP data from report 2.
        /// </summary>
        public Report2Udp Report2 { get; set; } = new Report2Udp();

        /// <summary>
        /// The Report3 property holds all Wallbox UDP data from report 3.
        /// </summary>
        public Report3Udp Report3 { get; set; } = new Report3Udp();

        /// <summary>
        /// The Reports property holds all Wallbox UDP data from report 100.
        /// </summary>
        public ReportsUdp Report100 { get; set; } = new ReportsUdp();

        /// <summary>
        /// The Reports property holds all Wallbox UDP data from report 101 - 130.
        /// </summary>
        public List<ReportsUdp> Reports { get; set; } = new List<ReportsUdp> { };

        #endregion

        #region Constructors

        public WallboxData()
        {
            for (int i = 0; i < Wallbox.MAX_REPORTS; ++i)
            {
                Reports.Add(new ReportsUdp());
            }
        }

        #endregion

        #region Public Property Helper

        /// <summary>
        /// Gets the property list for the WallboxData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(WallboxData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the WallboxData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(WallboxData), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(WallboxData), property);

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
