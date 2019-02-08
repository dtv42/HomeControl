// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InverterInfo.cs" company="DTV-Online">
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
    public class InverterInfo : DataValue, IPropertyHelper
    {
        #region Public Properties

        public string Index { get; set; } = string.Empty;
        public int DeviceType { get; set; }
        public int PVPower { get; set; }
        public string CustomName { get; set; } = string.Empty;
        public bool Show { get; set; }
        public string UniqueID { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public StatusCodes StatusCode { get; set; } = new StatusCodes();

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in InverterInfo.
        /// </summary>
        /// <param name="data">The Fronius data.</param>
        public void Refresh(FroniusData data)
        {
            if (data != null)
            {
                Index = data.InverterInfo.Inverter.Index;
                DeviceType = data.InverterInfo.Inverter.DeviceType;
                PVPower = data.InverterInfo.Inverter.PVPower;
                CustomName = data.InverterInfo.Inverter.CustomName;
                Show = data.InverterInfo.Inverter.Show;
                UniqueID = data.InverterInfo.Inverter.UniqueID;
                ErrorCode = data.InverterInfo.Inverter.ErrorCode;
                StatusCode = data.InverterInfo.Inverter.StatusCode;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the InverterInfo class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(InverterInfo).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the InverterInfo class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(InverterInfo), property) != null) ? true : false;

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
