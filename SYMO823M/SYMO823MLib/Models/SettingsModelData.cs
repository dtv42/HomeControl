// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsModelData.cs" company="DTV-Online">
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

    public class SettingsModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint16 IC121ID { get; set; } = 121;
        public uint16 IC121Length { get; set; } = 30;
        public uint16 WMax { get; set; }
        public uint16 VRef { get; set; }
        public int16 VRefOfs { get; set; }
        public uint16 VMax { get; set; }
        public uint16 VMin { get; set; }
        public uint16 VAMax { get; set; }
        public int16 VARMaxQ1 { get; set; }
        public int16 VARMaxQ2 { get; set; }
        public int16 VARMaxQ3 { get; set; }
        public int16 VARMaxQ4 { get; set; }
        public uint16 WGra { get; set; }
        public enum16 VArAct { get; set; }
        public enum16 ClcTotVA { get; set; }
        public int16 PFMinQ1 { get; set; }
        public int16 PFMinQ2 { get; set; }
        public int16 PFMinQ3 { get; set; }
        public int16 PFMinQ4 { get; set; }
        public uint16 MaxRmpRte { get; set; }
        public uint16 ECPNomHz { get; set; }
        public enum16 ConnectedPhase { get; set; }
        public sunssf ScaleFactorWMax { get; set; }
        public sunssf ScaleFactorVRef { get; set; }
        public sunssf ScaleFactorVRefOfs { get; set; }
        public sunssf ScaleFactorVMinMax { get; set; }
        public sunssf ScaleFactorVAMax { get; set; }
        public sunssf ScaleFactorVARMax { get; set; }
        public sunssf ScaleFactorWGra { get; set; }
        public sunssf ScaleFactorPFMin { get; set; }
        public sunssf ScaleFactorMaxRmpRte { get; set; }
        public sunssf ScaleFactorECPNomHz { get; set; }

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
                // Basic Settings Model(IC121)
                IC121ID = data.IC121ID;
                IC121Length = data.IC121Length;
                WMax = data.WMax;
                VRef = data.VRef;
                VRefOfs = data.VRefOfs;
                VMax = data.VMax;
                VMin = data.VMin;
                VAMax = data.VAMax;
                VARMaxQ1 = data.VARMaxQ1;
                VARMaxQ2 = data.VARMaxQ2;
                VARMaxQ3 = data.VARMaxQ3;
                VARMaxQ4 = data.VARMaxQ4;
                WGra = data.WGra;
                PFMinQ1 = data.PFMinQ1;
                PFMinQ2 = data.PFMinQ2;
                PFMinQ3 = data.PFMinQ3;
                PFMinQ4 = data.PFMinQ4;
                MaxRmpRte = data.MaxRmpRte;
                ECPNomHz = data.ECPNomHz;
                ConnectedPhase = data.ConnectedPhase;
                ScaleFactorWMax = data.ScaleFactorWMax;
                ScaleFactorVRef = data.ScaleFactorVRef;
                ScaleFactorVRefOfs = data.ScaleFactorVRefOfs;
                ScaleFactorVMinMax = data.ScaleFactorVMinMax;
                ScaleFactorVAMax = data.ScaleFactorVAMax;
                ScaleFactorVARMax = data.ScaleFactorVARMax;
                ScaleFactorWGra = data.ScaleFactorWGra;
                ScaleFactorPFMin = data.ScaleFactorPFMin;
                ScaleFactorMaxRmpRte = data.ScaleFactorMaxRmpRte;
                ScaleFactorECPNomHz = data.ScaleFactorECPNomHz;
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