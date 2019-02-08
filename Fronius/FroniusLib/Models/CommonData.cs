// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;
    using Extensions;

    #endregion

    /// <summary>
    /// Class holding selected data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class CommonData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public double Frequency { get; set; }
        public double CurrentDC { get; set; }
        public double CurrentAC { get; set; }
        public double VoltageDC { get; set; }
        public double VoltageAC { get; set; }
        public uint PowerAC { get; set; }
        public uint DailyEnergy { get; set; }
        public uint YearlyEnergy { get; set; }
        public uint TotalEnergy { get; set; }
        public StatusCodes StatusCode { get; set; } = new StatusCodes();

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in CommonData.
        /// </summary>
        /// <param name="data">The Fronius data.</param>
        public void Refresh(FroniusData data)
        {
            if (data != null)
            {
                Frequency = data.CommonData.Inverter.Frequency.Value;
                CurrentDC = data.CommonData.Inverter.CurrentDC.Value;
                CurrentAC = data.CommonData.Inverter.CurrentAC.Value;
                VoltageDC = data.CommonData.Inverter.VoltageDC.Value;
                VoltageAC = data.CommonData.Inverter.VoltageAC.Value;
                PowerAC = data.CommonData.Inverter.PowerAC.Value;
                DailyEnergy = data.CommonData.Inverter.DailyEnergy.Value;
                YearlyEnergy = data.CommonData.Inverter.YearlyEnergy.Value;
                TotalEnergy = data.CommonData.Inverter.TotalEnergy.Value;
                StatusCode = data.CommonData.Inverter.DeviceStatus.StatusCode.ToStatusCode();
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the CommonData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(CommonData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the CommonData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(CommonData), property) != null) ? true : false;

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
