// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonModelData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;
    using SunSpecLib;

    #endregion

    public class CommonModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint32 SunSpecID { get; set; } = 0x53756e53;
        public uint16 C001ID { get; set; } = 1;
        public uint16 C001Length { get; set; } = 65;
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Options { get; set; }
        public string Version { get; set; }
        public string SerialNumber { get; set; }
        public uint16 DeviceAddress { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in CommonData.
        /// </summary>
        /// <param name="data">The SYMO823M data.</param>
        public void Refresh(SYMO823MData data)
        {
            if (data != null)
            {
                // Common Model (C001)
                SunSpecID = data.SunSpecID;
                C001ID = data.C001ID;
                C001Length = data.C001Length;
                Manufacturer = data.Manufacturer;
                Model = data.Model;
                Options = data.Options;
                Version = data.Version;
                SerialNumber = data.SerialNumber;
                DeviceAddress = data.DeviceAddress;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the CommonModelData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(CommonModelData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the CommonModelData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(CommonModelData), property) != null) ? true : false;

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