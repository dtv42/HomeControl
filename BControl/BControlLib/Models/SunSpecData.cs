// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SunSpecData.cs" company="DTV-Online">
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
    using SunSpecLib;

    #endregion

    public class SunSpecData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public uint32 C_SunSpec_ID { get; set; }
        public uint16 C_SunSpec_DID1 { get; set; }
        public uint16 C_SunSpec_Length1 { get; set; }
        public string C_Manufacturer { get; set; }
        public string C_Model { get; set; }
        public string C_Options { get; set; }
        public string C_Version { get; set; }
        public string C_SerialNumber { get; set; }
        public uint16 C_DeviceAddress { get; set; }
        public uint16 C_SunSpec_DID2 { get; set; }
        public uint16 C_SunSpec_Length2 { get; set; }
        public int16 M_AC_Current { get; set; }
        public int16 M_AC_Current_A { get; set; }
        public int16 M_AC_Current_B { get; set; }
        public int16 M_AC_Current_C { get; set; }
        public sunssf M_AC_Current_SF { get; set; }
        public int16 M_AC_Voltage_LN { get; set; }
        public int16 M_AC_Voltage_AN { get; set; }
        public int16 M_AC_Voltage_BN { get; set; }
        public int16 M_AC_Voltage_CN { get; set; }
        public int16 M_AC_Voltage_LL { get; set; }
        public int16 M_AC_Voltage_AB { get; set; }
        public int16 M_AC_Voltage_BC { get; set; }
        public int16 M_AC_Voltage_CA { get; set; }
        public sunssf M_AC_Voltage_SF { get; set; }
        public int16 M_AC_Freq { get; set; }
        public sunssf M_AC_Freq_SF { get; set; }
        public int16 M_AC_Power { get; set; }
        public int16 M_AC_Power_A { get; set; }
        public int16 M_AC_Power_B { get; set; }
        public int16 M_AC_Power_C { get; set; }
        public sunssf M_AC_Power_SF { get; set; }
        public int16 M_AC_VA { get; set; }
        public int16 M_AC_VA_A { get; set; }
        public int16 M_AC_VA_B { get; set; }
        public int16 M_AC_VA_C { get; set; }
        public sunssf M_AC_VA_SF { get; set; }
        public int16 M_AC_VAR { get; set; }
        public int16 M_AC_VAR_A { get; set; }
        public int16 M_AC_VAR_B { get; set; }
        public int16 M_AC_VAR_C { get; set; }
        public sunssf M_AC_VAR_SF { get; set; }
        public int16 M_AC_PF { get; set; }
        public int16 M_AC_PF_A { get; set; }
        public int16 M_AC_PF_B { get; set; }
        public int16 M_AC_PF_C { get; set; }
        public sunssf M_AC_PF_SF { get; set; }
        public uint32 M_Exported { get; set; }
        public uint32 M_Exported_A { get; set; }
        public uint32 M_Exported_B { get; set; }
        public uint32 M_Exported_C { get; set; }
        public uint32 M_Imported { get; set; }
        public uint32 M_Imported_A { get; set; }
        public uint32 M_Imported_B { get; set; }
        public uint32 M_Imported_C { get; set; }
        public sunssf M_Energy_W_SF { get; set; }
        public uint32 M_Exported_VA { get; set; }
        public uint32 M_Exported_VA_A { get; set; }
        public uint32 M_Exported_VA_B { get; set; }
        public uint32 M_Exported_VA_C { get; set; }
        public uint32 M_Imported_VA { get; set; }
        public uint32 M_Imported_VA_A { get; set; }
        public uint32 M_Imported_VA_B { get; set; }
        public uint32 M_Imported_VA_C { get; set; }
        public sunssf M_Energy_VA_SF { get; set; }
        public uint32 M_Import_VARh_Q1 { get; set; }
        public uint32 M_Import_VARh_Q1A { get; set; }
        public uint32 M_Import_VARh_Q1B { get; set; }
        public uint32 M_Import_VARh_Q1C { get; set; }
        public uint32 M_Import_VARh_Q2 { get; set; }
        public uint32 M_Import_VARh_Q2A { get; set; }
        public uint32 M_Import_VARh_Q2B { get; set; }
        public uint32 M_Import_VARh_Q2C { get; set; }
        public uint32 M_Import_VARh_Q3 { get; set; }
        public uint32 M_Import_VARh_Q3A { get; set; }
        public uint32 M_Import_VARh_Q3B { get; set; }
        public uint32 M_Import_VARh_Q3C { get; set; }
        public uint32 M_Import_VARh_Q4 { get; set; }
        public uint32 M_Import_VARh_Q4A { get; set; }
        public uint32 M_Import_VARh_Q4B { get; set; }
        public uint32 M_Import_VARh_Q4C { get; set; }
        public sunssf M_Energy_VAR_SF { get; set; }
        public uint32 M_Events { get; set; }
        public uint16 C_SunSpec_DID3 { get; set; }
        public uint16 C_SunSpec_Length3 { get; set; }

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
                C_SunSpec_ID = data.C_SunSpec_ID;
                C_SunSpec_DID1 = data.C_SunSpec_DID1;
                C_SunSpec_Length1 = data.C_SunSpec_Length1;
                C_Manufacturer = data.C_Manufacturer;
                C_Model = data.C_Model;
                C_Options = data.C_Options;
                C_Version = data.C_Version;
                C_SerialNumber = data.C_SerialNumber;
                C_DeviceAddress = data.C_DeviceAddress;
                C_SunSpec_DID2 = data.C_SunSpec_DID2;
                C_SunSpec_Length2 = data.C_SunSpec_Length2;
                M_AC_Current = data.M_AC_Current;
                M_AC_Current_A = data.M_AC_Current_A;
                M_AC_Current_B = data.M_AC_Current_B;
                M_AC_Current_C = data.M_AC_Current_C;
                M_AC_Current_SF = data.M_AC_Current_SF;
                M_AC_Voltage_LN = data.M_AC_Voltage_LN;
                M_AC_Voltage_AN = data.M_AC_Voltage_AN;
                M_AC_Voltage_BN = data.M_AC_Voltage_BN;
                M_AC_Voltage_CN = data.M_AC_Voltage_CN;
                M_AC_Voltage_LL = data.M_AC_Voltage_LL;
                M_AC_Voltage_AB = data.M_AC_Voltage_AB;
                M_AC_Voltage_BC = data.M_AC_Voltage_BC;
                M_AC_Voltage_CA = data.M_AC_Voltage_CA;
                M_AC_Voltage_SF = data.M_AC_Voltage_SF;
                M_AC_Freq = data.M_AC_Freq;
                M_AC_Freq_SF = data.M_AC_Freq_SF;
                M_AC_Power = data.M_AC_Power;
                M_AC_Power_A = data.M_AC_Power_A;
                M_AC_Power_B = data.M_AC_Power_B;
                M_AC_Power_C = data.M_AC_Power_C;
                M_AC_Power_SF = data.M_AC_Power_SF;
                M_AC_VA = data.M_AC_VA;
                M_AC_VA_A = data.M_AC_VA_A;
                M_AC_VA_B = data.M_AC_VA_B;
                M_AC_VA_C = data.M_AC_VA_C;
                M_AC_VA_SF = data.M_AC_VA_SF;
                M_AC_VAR = data.M_AC_VAR;
                M_AC_VAR_A = data.M_AC_VAR_A;
                M_AC_VAR_B = data.M_AC_VAR_B;
                M_AC_VAR_C = data.M_AC_VAR_C;
                M_AC_VAR_SF = data.M_AC_VAR_SF;
                M_AC_PF = data.M_AC_PF;
                M_AC_PF_A = data.M_AC_PF_A;
                M_AC_PF_B = data.M_AC_PF_B;
                M_AC_PF_C = data.M_AC_PF_C;
                M_AC_PF_SF = data.M_AC_PF_SF;
                M_Exported = data.M_Exported;
                M_Exported_A = data.M_Exported_A;
                M_Exported_B = data.M_Exported_B;
                M_Exported_C = data.M_Exported_C;
                M_Imported = data.M_Imported;
                M_Imported_A = data.M_Imported_A;
                M_Imported_B = data.M_Imported_B;
                M_Imported_C = data.M_Imported_C;
                M_Energy_W_SF = data.M_Energy_W_SF;
                M_Exported_VA = data.M_Exported_VA;
                M_Exported_VA_A = data.M_Exported_VA_A;
                M_Exported_VA_B = data.M_Exported_VA_B;
                M_Exported_VA_C = data.M_Exported_VA_C;
                M_Imported_VA = data.M_Imported_VA;
                M_Imported_VA_A = data.M_Imported_VA_A;
                M_Imported_VA_B = data.M_Imported_VA_B;
                M_Imported_VA_C = data.M_Imported_VA_C;
                M_Energy_VA_SF = data.M_Energy_VA_SF;
                M_Import_VARh_Q1 = data.M_Import_VARh_Q1;
                M_Import_VARh_Q1A = data.M_Import_VARh_Q1A;
                M_Import_VARh_Q1B = data.M_Import_VARh_Q1B;
                M_Import_VARh_Q1C = data.M_Import_VARh_Q1C;
                M_Import_VARh_Q2 = data.M_Import_VARh_Q2;
                M_Import_VARh_Q2A = data.M_Import_VARh_Q2A;
                M_Import_VARh_Q2B = data.M_Import_VARh_Q2B;
                M_Import_VARh_Q2C = data.M_Import_VARh_Q2C;
                M_Import_VARh_Q3 = data.M_Import_VARh_Q3;
                M_Import_VARh_Q3A = data.M_Import_VARh_Q3A;
                M_Import_VARh_Q3B = data.M_Import_VARh_Q3B;
                M_Import_VARh_Q3C = data.M_Import_VARh_Q3C;
                M_Import_VARh_Q4 = data.M_Import_VARh_Q4;
                M_Import_VARh_Q4A = data.M_Import_VARh_Q4A;
                M_Import_VARh_Q4B = data.M_Import_VARh_Q4B;
                M_Import_VARh_Q4C = data.M_Import_VARh_Q4C;
                M_Energy_VAR_SF = data.M_Energy_VAR_SF;
                M_Events = data.M_Events;
                C_SunSpec_DID3 = data.C_SunSpec_DID3;
                C_SunSpec_Length3 = data.C_SunSpec_Length3;
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
