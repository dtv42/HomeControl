// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PnPData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    public class PnPData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public UInt16 ManufacturerID { get; set; }
        public UInt16 ProductID { get; set; }
        public UInt16 ProductVersion { get; set; }
        public UInt16 FirmwareVersion { get; set; }
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public string SerialNumber { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in InternalData.
        /// </summary>
        /// <param name="data">The BControl data.</param>
        public void Refresh(BControlData data)
        {
            if (data != null)
            {
                ManufacturerID = data.ManufacturerID;
                ProductID = data.ProductID;
                ProductVersion = data.ProductVersion;
                FirmwareVersion = data.FirmwareVersion;
                VendorName = data.VendorName;
                ProductName = data.ProductName;
                SerialNumber = data.SerialNumber;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the OverviewData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(InternalData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the OverviewData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(InternalData), property) != null) ? true : false;

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
