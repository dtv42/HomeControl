// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultipleModelData.cs" company="DTV-Online">
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

    public class MultipleModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint16 I160ID { get; set; } = 160;
        public uint16 I160Length { get; set; } = 48;
        public sunssf ScaleFactorCurrent { get; set; }
        public sunssf ScaleFactorVoltage { get; set; }
        public sunssf ScaleFactorPower { get; set; }
        public sunssf ScaleFactorEnergy { get; set; }
        public uint32 GlobalEvents { get; set; }
        public uint16 NumberOfModules { get; set; }
        public uint16 TimestampPeriod { get; set; }
        public uint16 InputID1 { get; set; }
        public string InputIDString1 { get; set; }
        public uint16 CurrentDC1 { get; set; }
        public uint16 VoltageDC1 { get; set; }
        public uint16 PowerDC1 { get; set; }
        public uint32 LifetimeEnergy1 { get; set; }
        public uint32 Timestamp1 { get; set; }
        public uint16 Temperature1 { get; set; }
        public OperatingState OperatingState1 { get; set; }
        public uint32 ModuleEvents1 { get; set; }
        public uint16 InputID2 { get; set; }
        public string InputIDString2 { get; set; }
        public uint16 CurrentDC2 { get; set; }
        public uint16 VoltageDC2 { get; set; }
        public uint16 PowerDC2 { get; set; }
        public uint32 LifetimeEnergy2 { get; set; }
        public uint32 Timestamp2 { get; set; }
        public uint16 Temperature2 { get; set; }
        public OperatingState OperatingState2 { get; set; }
        public uint32 ModuleEvents2 { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in CommonData.
        /// </summary>
        /// <param name="data">The ETAPU11 data.</param>
        public void Refresh(SYMO823MData data)
        {
            if (data != null)
            {
                // Multiple MPPT Inverter Extension Model (I160)
                I160ID = data.I160ID;
                I160Length = data.I160Length;
                ScaleFactorCurrent = data.ScaleFactorCurrent;
                ScaleFactorVoltage = data.ScaleFactorVoltage;
                ScaleFactorPower = data.ScaleFactorPower;
                ScaleFactorEnergy = data.ScaleFactorEnergy;
                GlobalEvents = data.GlobalEvents;
                NumberOfModules = data.NumberOfModules;
                TimestampPeriod = data.TimestampPeriod;
                InputID1 = data.InputID1;
                InputIDString1 = data.InputIDString1;
                CurrentDC1 = data.CurrentDC1;
                VoltageDC1 = data.VoltageDC1;
                PowerDC1 = data.PowerDC1;
                LifetimeEnergy1 = data.LifetimeEnergy1;
                Timestamp1 = data.Timestamp1;
                Temperature1 = data.Temperature1;
                OperatingState1 = data.OperatingState1;
                ModuleEvents1 = data.ModuleEvents1;
                InputID2 = data.InputID2;
                InputIDString2 = data.InputIDString2;
                CurrentDC2 = data.CurrentDC2;
                VoltageDC2 = data.VoltageDC2;
                PowerDC2 = data.PowerDC2;
                LifetimeEnergy2 = data.LifetimeEnergy2;
                Timestamp2 = data.Timestamp2;
                Temperature2 = data.Temperature2;
                OperatingState2 = data.OperatingState2;
                ModuleEvents2 = data.ModuleEvents2;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the OverviewData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(CommonModelData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the BoilerData class.
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