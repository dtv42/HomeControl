// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InverterModelData.cs" company="DTV-Online">
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

    public class InverterModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint16 I113ID { get; set; } = 113;
        public uint16 I113Length { get; set; } = 60;
        public float TotalCurrentAC { get; set; }
        public float CurrentL1 { get; set; }
        public float CurrentL2 { get; set; }
        public float CurrentL3 { get; set; }
        public float VoltageL1L2 { get; set; }
        public float VoltageL2L3 { get; set; }
        public float VoltageL3L1 { get; set; }
        public float VoltageL1N { get; set; }
        public float VoltageL2N { get; set; }
        public float VoltageL3N { get; set; }
        public float PowerAC { get; set; }
        public float Frequency { get; set; }
        public float ApparentPower { get; set; }
        public float ReactivePower { get; set; }
        public float PowerFactor { get; set; }
        public float LifeTimeEnergy { get; set; }
        public float CurrentDC { get; set; }
        public float VoltageDC { get; set; }
        public float PowerDC { get; set; }
        public float CabinetTemperature { get; set; }
        public float HeatsinkTemperature { get; set; }
        public float TransformerTemperature { get; set; }
        public float OtherTemperature { get; set; }
        public OperatingState OperatingState { get; set; }
        public VendorState VendorState { get; set; }
        public Evt1 Evt1 { get; set; }
        public Evt2 Evt2 { get; set; }
        public EvtVnd1 EvtVnd1 { get; set; }
        public EvtVnd2 EvtVnd2 { get; set; }
        public EvtVnd3 EvtVnd3 { get; set; }
        public EvtVnd4 EvtVnd4 { get; set; }

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
                // Inverter Model (I113)
                I113ID = data.I113ID;
                I113Length = data.I113Length;
                TotalCurrentAC = data.TotalCurrentAC;
                CurrentL1 = data.CurrentL1;
                CurrentL2 = data.CurrentL2;
                CurrentL3 = data.CurrentL3;
                VoltageL1L2 = data.VoltageL1L2;
                VoltageL2L3 = data.VoltageL2L3;
                VoltageL3L1 = data.VoltageL3L1;
                VoltageL1N = data.VoltageL1N;
                VoltageL2N = data.VoltageL2N;
                VoltageL3N = data.VoltageL3N;
                PowerAC = data.PowerAC;
                Frequency = data.Frequency;
                ApparentPower = data.ApparentPower;
                ReactivePower = data.ReactivePower;
                PowerFactor = data.PowerFactor;
                LifeTimeEnergy = data.LifeTimeEnergy;
                CurrentDC = data.CurrentDC;
                VoltageDC = data.VoltageDC;
                PowerDC = data.PowerDC;
                CabinetTemperature = data.CabinetTemperature;
                HeatsinkTemperature = data.HeatsinkTemperature;
                TransformerTemperature = data.TransformerTemperature;
                OtherTemperature = data.OtherTemperature;
                OperatingState = data.OperatingState;
                VendorState = data.VendorState;
                Evt1 = data.Evt1;
                Evt2 = data.Evt2;
                EvtVnd1 = data.EvtVnd1;
                EvtVnd2 = data.EvtVnd2;
                EvtVnd3 = data.EvtVnd3;
                EvtVnd4 = data.EvtVnd4;
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