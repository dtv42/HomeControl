// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameplateModelData.cs" company="DTV-Online">
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

    public class NameplateModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint16 IC120ID { get; set; } = 120;
        public uint16 IC120Length { get; set; } = 26;
        public DERType DERType { get; set; }
        public uint16 OutputW { get; set; }
        public sunssf ScaleFactorOutputW { get; set; }
        public uint16 OutputVA { get; set; }
        public sunssf ScaleFactorOutputVA { get; set; }
        public int16 OutputVArQ1 { get; set; }
        public int16 OutputVArQ2 { get; set; }
        public int16 OutputVArQ3 { get; set; }
        public int16 OutputVArQ4 { get; set; }
        public sunssf ScaleFactorOutputVAr { get; set; }
        public uint16 MaxRMS { get; set; }
        public sunssf ScaleFactorMaxRMS { get; set; }
        public int16 MinimumPFQ1 { get; set; }
        public int16 MinimumPFQ2 { get; set; }
        public int16 MinimumPFQ3 { get; set; }
        public int16 MinimumPFQ4 { get; set; }
        public sunssf ScaleFactorMinimumPF { get; set; }
        public uint16 EnergyRating { get; set; }
        public sunssf ScaleFactorEnergyRating { get; set; }
        public uint16 BatteryCapacity { get; set; }
        public sunssf ScaleFactorBatteryCapacity { get; set; }
        public uint16 MaxCharge { get; set; }
        public sunssf ScaleFactorMaxCharge { get; set; }
        public uint16 MaxDischarge { get; set; }
        public sunssf ScaleFactorMaxDischarge { get; set; }
        public uint16 Pad { get; set; }

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
                // Nameplate Model (IC120)
                IC120ID = data.IC120ID;
                IC120Length = data.IC120Length;
                DERType = data.DERType;
                OutputW = data.OutputW;
                ScaleFactorOutputW = data.ScaleFactorOutputW;
                OutputVA = data.OutputVA;
                ScaleFactorOutputVA = data.ScaleFactorOutputVA;
                OutputVArQ1 = data.OutputVArQ1;
                OutputVArQ2 = data.OutputVArQ2;
                OutputVArQ3 = data.OutputVArQ3;
                OutputVArQ4 = data.OutputVArQ4;
                ScaleFactorOutputVAr = data.ScaleFactorOutputVAr;
                MaxRMS = data.MaxRMS;
                ScaleFactorMaxRMS = data.ScaleFactorMaxRMS;
                MinimumPFQ1 = data.MinimumPFQ1;
                MinimumPFQ2 = data.MinimumPFQ2;
                MinimumPFQ3 = data.MinimumPFQ3;
                MinimumPFQ4 = data.MinimumPFQ4;
                ScaleFactorMinimumPF = data.ScaleFactorMinimumPF;
                EnergyRating = data.EnergyRating;
                ScaleFactorEnergyRating = data.ScaleFactorEnergyRating;
                BatteryCapacity = data.BatteryCapacity;
                ScaleFactorBatteryCapacity = data.ScaleFactorBatteryCapacity;
                MaxCharge = data.MaxCharge;
                ScaleFactorMaxCharge = data.ScaleFactorMaxCharge;
                MaxDischarge = data.MaxDischarge;
                ScaleFactorMaxDischarge = data.ScaleFactorMaxDischarge;
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