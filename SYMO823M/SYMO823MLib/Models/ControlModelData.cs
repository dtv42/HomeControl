// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlModelData.cs" company="DTV-Online">
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

    public class ControlModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint16 IC123ID { get; set; } = 123;
        public uint16 IC123Length { get; set; } = 24;
        public uint16 ConnWinTms { get; set; }
        public uint16 ConnRvrtTms { get; set; }
        public uint16 Conn { get; set; }
        public uint16 WMaxLimPct { get; set; }
        public uint16 WMaxLimPctWinTms { get; set; }
        public uint16 WMaxLimPctRvrtTms { get; set; }
        public uint16 WMaxLimPctRmpTms { get; set; }
        public uint16 WMaxLimEna { get; set; }
        public int16 OutPFSet { get; set; }
        public uint16 OutPFSetWinTms { get; set; }
        public uint16 OutPFSetRvrtTms { get; set; }
        public uint16 OutPFSetRmpTms { get; set; }
        public uint16 OutPFSetEna { get; set; }
        public int16 VArWMaxPct { get; set; }
        public int16 VArMaxPct { get; set; }
        public int16 VArAvalPct { get; set; }
        public uint16 VArPctWinTms { get; set; }
        public uint16 VArPctRvrtTms { get; set; }
        public uint16 VArPctRmpTms { get; set; }
        public uint16 VArPctMod { get; set; }
        public uint16 VArPctEna { get; set; }
        public sunssf ScaleFactorWMaxLimPct { get; set; }
        public sunssf ScaleFactorOutPFSet { get; set; }
        public sunssf ScaleFactorVArPct { get; set; }

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
                // Immediate Control Model (IC123)
                IC123ID = data.IC123ID;
                IC123Length = data.IC123Length;
                ConnWinTms = data.ConnWinTms;
                ConnRvrtTms = data.ConnRvrtTms;
                Conn = data.Conn;
                WMaxLimPct = data.WMaxLimPct;
                WMaxLimPctWinTms = data.WMaxLimPctWinTms;
                WMaxLimPctRvrtTms = data.WMaxLimPctRvrtTms;
                WMaxLimPctRmpTms = data.WMaxLimPctRmpTms;
                WMaxLimEna = data.WMaxLimEna;
                OutPFSet = data.OutPFSet;
                OutPFSetWinTms = data.OutPFSetWinTms;
                OutPFSetRvrtTms = data.OutPFSetRvrtTms;
                OutPFSetRmpTms = data.OutPFSetRmpTms;
                OutPFSetEna = data.OutPFSetEna;
                VArWMaxPct = data.VArWMaxPct;
                VArMaxPct = data.VArMaxPct;
                VArAvalPct = data.VArAvalPct;
                VArPctWinTms = data.VArPctWinTms;
                VArPctRvrtTms = data.VArPctRvrtTms;
                VArPctRmpTms = data.VArPctRmpTms;
                VArPctMod = data.VArPctMod;
                VArPctEna = data.VArPctEna;
                ScaleFactorWMaxLimPct = data.ScaleFactorWMaxLimPct;
                ScaleFactorOutPFSet = data.ScaleFactorOutPFSet;
                ScaleFactorVArPct = data.ScaleFactorVArPct;
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