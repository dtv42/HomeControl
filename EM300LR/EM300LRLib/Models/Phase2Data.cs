// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Phase2Data.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib.Models
{
    #region Using Directives

    using System.Linq;
    using System.Reflection;
    using DataValueLib;

    #endregion Using Directives

    /// <summary>
    /// Class holding all data from the b-Control EM300LR energy manager.
    /// </summary>
    public class Phase2Data : DataValue, IPropertyHelper
    {
        #region Public Properties

        public double ActivePowerPlus { get; set; }
        public double ActiveEnergyPlus { get; set; }
        public double ActivePowerMinus { get; set; }
        public double ActiveEnergyMinus { get; set; }
        public double ReactivePowerPlus { get; set; }
        public double ReactiveEnergyPlus { get; set; }
        public double ReactivePowerMinus { get; set; }
        public double ReactiveEnergyMinus { get; set; }
        public double ApparentPowerPlus { get; set; }
        public double ApparentEnergyPlus { get; set; }
        public double ApparentPowerMinus { get; set; }
        public double ApparentEnergyMinus { get; set; }
        public double PowerFactor { get; set; }
        public double Current { get; set; }
        public double Voltage { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in EM300LR data.
        /// </summary>
        /// <param name="data">The EM300LR data.</param>
        public void Refresh(EM300LRData data)
        {
            if (data != null)
            {
                ActivePowerPlus = data.ActivePowerPlusL2;
                ActiveEnergyPlus = data.ActiveEnergyPlusL2;
                ActivePowerMinus = data.ActivePowerMinusL2;
                ActiveEnergyMinus = data.ActiveEnergyMinusL2;
                ReactivePowerPlus = data.ReactivePowerPlusL2;
                ReactiveEnergyPlus = data.ReactiveEnergyPlusL2;
                ReactivePowerMinus = data.ReactivePowerMinusL2;
                ReactiveEnergyMinus = data.ReactiveEnergyMinusL2;
                ApparentPowerPlus = data.ApparentPowerPlusL2;
                ApparentEnergyPlus = data.ApparentEnergyPlusL2;
                ApparentPowerMinus = data.ApparentPowerMinusL2;
                ApparentEnergyMinus = data.ApparentEnergyMinusL2;
                PowerFactor = data.PowerFactorL2;
                Current = data.CurrentL2;
                Voltage = data.VoltageL2;
            }

            Status = data?.Status ?? Uncertain;
        }

        #endregion Public Methods

        #region Public Property Helper

        /// <summary>
        /// Gets the property list for the ZipatoData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(Phase2Data).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the ETAPU11Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(Phase2Data), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(Phase2Data), property);

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
