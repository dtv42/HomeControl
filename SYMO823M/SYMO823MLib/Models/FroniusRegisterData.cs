// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusRegisterData.cs" company="DTV-Online">
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

    #endregion

    public class FroniusRegisterData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public ushort DeleteData { get; set; }
        public ushort StoreData { get; set; }
        public ushort ActiveStateCode { get; set; }
        public ushort ResetAllEventFlags { get; set; }
        public ushort ModelType { get; set; }
        public uint SitePower { get; set; }
        public ulong SiteEnergyDay { get; set; }
        public ulong SiteEnergyYear { get; set; }
        public ulong SiteEnergyTotal { get; set; }

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
                // Fronius registers
                DeleteData = data.DeleteData;
                StoreData = data.StoreData;
                ActiveStateCode = data.ActiveStateCode;
                ResetAllEventFlags = data.ResetAllEventFlags;
                ModelType = data.ModelType;
                SitePower = data.SitePower;
                SiteEnergyDay = data.SiteEnergyDay;
                SiteEnergyYear = data.SiteEnergyYear;
                SiteEnergyTotal = data.SiteEnergyTotal;
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