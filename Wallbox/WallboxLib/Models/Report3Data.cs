// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report3Data.cs" company="DTV-Online">
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

    public class Report3Data : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// Measured voltage value on phase 1 in V
        /// </summary>
        public double VoltageL1N { get; set; }

        /// <summary>
        /// Measured voltage value on phase 2 in V
        /// </summary>
        public double VoltageL2N { get; set; }

        /// <summary>
        /// Measured voltage value on phase 3 in V
        /// </summary>
        public double VoltageL3N { get; set; }

        /// <summary>
        /// Measured current value on phase 1 in A
        /// </summary>
        public double CurrentL1 { get; set; }

        /// <summary>
        /// Measured current value on phase 2 in A
        /// </summary>
        public double CurrentL2 { get; set; }

        /// <summary>
        /// Measured current value on phase 3 in A
        /// </summary>
        public double CurrentL3 { get; set; }

        /// <summary>
        /// Power in W (effective power).
        /// </summary>
        public double Power { get; set; }

        /// <summary>
        /// Current power factor(cosphi).
        /// </summary>
        public double PowerFactor { get; set; }

        /// <summary>
        /// Energy transferred in the current charging session in Wh.
        /// </summary>
        public double EnergyCharging { get; set; }

        /// <summary>
        /// Total energy consumption(persistent, device related) in Wh.
        /// </summary>
        public double EnergyTotal { get; set; }

        /// <summary>
        /// Serial number of the device.
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// Current state of the system clock in seconds from the last startup of the device.
        /// </summary>
        public uint Seconds { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in Report3Data.
        /// </summary>
        /// <param name="data">The Wallbox data.</param>
        public void Refresh(WallboxData data)
        {
            if (data != null)
            {
                ID = data.Report3.ID;
                VoltageL1N = data.Report3.U1;
                VoltageL2N = data.Report3.U2;
                VoltageL3N = data.Report3.U3;
                CurrentL1 = data.Report3.I1 / 1000.0;
                CurrentL2 = data.Report3.I2 / 1000.0;
                CurrentL3 = data.Report3.I3 / 1000.0;
                Power = data.Report3.P / 1000000.0;
                PowerFactor = data.Report3.PF / 10.0;
                EnergyCharging = data.Report3.Epres / 10000.0;
                EnergyTotal = data.Report3.Etotal / 10000.0;
                Serial = data.Report3.Serial;
                Seconds = data.Report3.Sec;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the Report3Data class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(Report3Data).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the Report3Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(Report3Data), property) != null) ? true : false;

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
