// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report1Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    public class Report1Data : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// Product name as defined by the manufacturer
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Serial number of the device
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// Firmware version of the device
        /// </summary>
        public string Firmware { get; set; } = string.Empty;

        /// <summary>
        /// Communication module.
        /// </summary>
        public ComModulePresent ComModule { get; set; }

        /// <summary>
        /// Backend communication status.
        /// </summary>
        public BackendPresent Backend { get; set; }

        /// <summary>
        /// Synced time.
        /// </summary>
        public ushort TimeQ { get; set; }

        /// <summary>
        /// "DIP-Sw": "0x2600" (undocumented).
        /// </summary>
        public DipSwitches DIPSwitch { get; set; }

        /// <summary>
        /// Current state of the system clock in seconds from the last startup of the device.
        /// </summary>
        public uint Seconds { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in Report1Data.
        /// </summary>
        /// <param name="data">The Wallbox data.</param>
        public void Refresh(WallboxData data)
        {
            if (data != null)
            {
                ID = data.Report1.ID;
                Product = data.Report1.Product;
                Serial = data.Report1.Serial;
                Firmware = data.Report1.Firmware;
                ComModule = (ComModulePresent)data.Report1.ComModule;
                Backend = (BackendPresent)data.Report1.Backend;
                TimeQ = data.Report1.TimeQ;
                DIPSwitch = (DipSwitches)Convert.ToInt64(data.Report1.DipSW, 16);
                Seconds = data.Report1.Sec;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the Report1Data class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(Report1Data).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the Report1Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(Report1Data), property) != null) ? true : false;

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
