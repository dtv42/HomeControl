// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnergyData.cs" company="DTV-Online">
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

    public class EnergyData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public UInt64 ActiveEnergyPlus { get; set; }
        public UInt64 ActiveEnergyMinus { get; set; }
        public UInt64 ReactiveEnergyPlus { get; set; }
        public UInt64 ReactiveEnergyMinus { get; set; }
        public UInt64 ApparentEnergyPlus { get; set; }
        public UInt64 ApparentEnergyMinus { get; set; }
        public UInt64 ActiveEnergyPlusL1 { get; set; }
        public UInt64 ActiveEnergyMinusL1 { get; set; }
        public UInt64 ReactiveEnergyPlusL1 { get; set; }
        public UInt64 ReactiveEnergyMinusL1 { get; set; }
        public UInt64 ApparentEnergyPlusL1 { get; set; }
        public UInt64 ApparentEnergyMinusL1 { get; set; }
        public UInt64 ActiveEnergyPlusL2 { get; set; }
        public UInt64 ActiveEnergyMinusL2 { get; set; }
        public UInt64 ReactiveEnergyPlusL2 { get; set; }
        public UInt64 ReactiveEnergyMinusL2 { get; set; }
        public UInt64 ApparentEnergyPlusL2 { get; set; }
        public UInt64 ApparentEnergyMinusL2 { get; set; }
        public UInt64 ActiveEnergyPlusL3 { get; set; }
        public UInt64 ActiveEnergyMinusL3 { get; set; }
        public UInt64 ReactiveEnergyPlusL3 { get; set; }
        public UInt64 ReactiveEnergyMinusL3 { get; set; }
        public UInt64 ApparentEnergyPlusL3 { get; set; }
        public UInt64 ApparentEnergyMinusL3 { get; set; }

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
                ActiveEnergyPlus = data.ActiveEnergyPlus;
                ActiveEnergyMinus = data.ActiveEnergyMinus;
                ReactiveEnergyPlus = data.ReactiveEnergyPlus;
                ReactiveEnergyMinus = data.ReactiveEnergyMinus;
                ApparentEnergyPlus = data.ApparentEnergyPlus;
                ApparentEnergyMinus = data.ApparentEnergyMinus;
                ActiveEnergyPlusL1 = data.ActiveEnergyPlusL1;
                ActiveEnergyMinusL1 = data.ActiveEnergyMinusL1;
                ReactiveEnergyPlusL1 = data.ReactiveEnergyPlusL1;
                ReactiveEnergyMinusL1 = data.ReactiveEnergyMinusL1;
                ApparentEnergyPlusL1 = data.ApparentEnergyPlusL1;
                ApparentEnergyMinusL1 = data.ApparentEnergyMinusL1;
                ActiveEnergyPlusL2 = data.ActiveEnergyPlusL2;
                ActiveEnergyMinusL2 = data.ActiveEnergyMinusL2;
                ReactiveEnergyPlusL2 = data.ReactiveEnergyPlusL2;
                ReactiveEnergyMinusL2 = data.ReactiveEnergyMinusL2;
                ApparentEnergyPlusL2 = data.ApparentEnergyPlusL2;
                ApparentEnergyMinusL2 = data.ApparentEnergyMinusL2;
                ActiveEnergyPlusL3 = data.ActiveEnergyPlusL3;
                ActiveEnergyMinusL3 = data.ActiveEnergyMinusL3;
                ReactiveEnergyPlusL3 = data.ReactiveEnergyPlusL3;
                ReactiveEnergyMinusL3 = data.ReactiveEnergyMinusL3;
                ApparentEnergyPlusL3 = data.ApparentEnergyPlusL3;
                ApparentEnergyMinusL3 = data.ApparentEnergyMinusL3;
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
