// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalData.cs" company="DTV-Online">
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

    public class InternalData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public UInt32 ActivePowerPlus { get; set; }
        public UInt32 ActivePowerMinus { get; set; }
        public UInt32 ReactivePowerPlus { get; set; }
        public UInt32 ReactivePowerMinus { get; set; }
        public UInt32 ApparentPowerPlus { get; set; }
        public UInt32 ApparentPowerMinus { get; set; }
        public Int32 PowerFactor { get; set; }
        public UInt32 LineFrequency { get; set; }
        public UInt32 ActivePowerPlusL1 { get; set; }
        public UInt32 ActivePowerMinusL1 { get; set; }
        public UInt32 ReactivePowerPlusL1 { get; set; }
        public UInt32 ReactivePowerMinusL1 { get; set; }
        public UInt32 ApparentPowerPlusL1 { get; set; }
        public UInt32 ApparentPowerMinusL1 { get; set; }
        public UInt32 CurrentL1 { get; set; }
        public UInt32 VoltageL1 { get; set; }
        public Int32 PowerFactorL1 { get; set; }
        public UInt32 ActivePowerPlusL2 { get; set; }
        public UInt32 ActivePowerMinusL2 { get; set; }
        public UInt32 ReactivePowerPlusL2 { get; set; }
        public UInt32 ReactivePowerMinusL2 { get; set; }
        public UInt32 ApparentPowerPlusL2 { get; set; }
        public UInt32 ApparentPowerMinusL2 { get; set; }
        public UInt32 CurrentL2 { get; set; }
        public UInt32 VoltageL2 { get; set; }
        public Int32 PowerFactorL2 { get; set; }
        public UInt32 ActivePowerPlusL3 { get; set; }
        public UInt32 ActivePowerMinusL3 { get; set; }
        public UInt32 ReactivePowerPlusL3 { get; set; }
        public UInt32 ReactivePowerMinusL3 { get; set; }
        public UInt32 ApparentPowerPlusL3 { get; set; }
        public UInt32 ApparentPowerMinusL3 { get; set; }
        public UInt32 CurrentL3 { get; set; }
        public UInt32 VoltageL3 { get; set; }
        public Int32 PowerFactorL3 { get; set; }

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
                ActivePowerPlus = data.ActivePowerPlus;
                ActivePowerMinus = data.ActivePowerMinus;
                ReactivePowerPlus = data.ReactivePowerPlus;
                ReactivePowerMinus = data.ReactivePowerMinus;
                ApparentPowerPlus = data.ApparentPowerPlus;
                ApparentPowerMinus = data.ApparentPowerMinus;
                PowerFactor = data.PowerFactor;
                LineFrequency = data.LineFrequency;
                ActivePowerPlusL1 = data.ActivePowerPlusL1;
                ActivePowerMinusL1 = data.ActivePowerMinusL1;
                ReactivePowerPlusL1 = data.ReactivePowerPlusL1;
                ReactivePowerMinusL1 = data.ReactivePowerMinusL1;
                ApparentPowerPlusL1 = data.ApparentPowerPlusL1;
                ApparentPowerMinusL1 = data.ApparentPowerMinusL1;
                CurrentL1 = data.CurrentL1;
                VoltageL1 = data.VoltageL1;
                PowerFactorL1 = data.PowerFactorL1;
                ActivePowerPlusL2 = data.ActivePowerPlusL2;
                ActivePowerMinusL2 = data.ActivePowerMinusL2;
                ReactivePowerPlusL2 = data.ReactivePowerPlusL2;
                ReactivePowerMinusL2 = data.ReactivePowerMinusL2;
                ApparentPowerPlusL2 = data.ApparentPowerPlusL2;
                ApparentPowerMinusL2 = data.ApparentPowerMinusL2;
                CurrentL2 = data.CurrentL2;
                VoltageL2 = data.VoltageL2;
                PowerFactorL2 = data.PowerFactorL2;
                ActivePowerPlusL3 = data.ActivePowerPlusL3;
                ActivePowerMinusL3 = data.ActivePowerMinusL3;
                ReactivePowerPlusL3 = data.ReactivePowerPlusL3;
                ReactivePowerMinusL3 = data.ReactivePowerMinusL3;
                ApparentPowerPlusL3 = data.ApparentPowerPlusL3;
                ApparentPowerMinusL3 = data.ApparentPowerMinusL3;
                CurrentL3 = data.CurrentL3;
                VoltageL3 = data.VoltageL3;
                PowerFactorL3 = data.PowerFactorL3;
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
