// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedModelData.cs" company="DTV-Online">
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

    public class ExtendedModelData : DataValue, IPropertyHelper
    {
        #region Private Data Members


        #endregion

        #region Public Properties

        /// <summary>
        /// The SYMO823M property subset.
        /// </summary>
        public uint16 IC122ID { get; set; } = 122;
        public uint16 IC122Length { get; set; } = 44;
        public PVConn PVConn { get; set; }
        public StorConn StorConn { get; set; }
        public ECPConn ECPConn { get; set; }
        public uint64 ActWh { get; set; }
        public uint64 ActVAh { get; set; }
        public uint64 ActVArhQ1 { get; set; }
        public uint64 ActVArhQ2 { get; set; }
        public uint64 ActVArhQ3 { get; set; }
        public uint64 ActVArhQ4 { get; set; }
        public uint16 AvailableVAr { get; set; }
        public sunssf ScaleFactorAvailableVAr { get; set; }
        public uint16 AvailableW { get; set; }
        public sunssf ScaleFactorAvailableW { get; set; }
        public uint32 StSetLimMsk { get; set; }
        public StActCtl StActCtl { get; set; }
        public string TmSrc { get; set; }
        public uint32 Tms { get; set; }
        public uint16 RtSt { get; set; }
        public uint16 Riso { get; set; }
        public sunssf ScaleFactorRiso { get; set; }

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
                // Extended Measurements & Status Model (IC122)
                IC122ID = data.IC122ID;
                IC122Length = data.IC122Length;
                PVConn = data.PVConn;
                StorConn = data.StorConn;
                ECPConn = data.ECPConn;
                ActWh = data.ActWh;
                ActVAh = data.ActVAh;
                ActVArhQ1 = data.ActVArhQ1;
                ActVArhQ2 = data.ActVArhQ2;
                ActVArhQ3 = data.ActVArhQ3;
                ActVArhQ4 = data.ActVArhQ4;
                AvailableVAr = data.AvailableVAr;
                ScaleFactorAvailableVAr = data.ScaleFactorAvailableVAr;
                AvailableW = data.AvailableW;
                ScaleFactorAvailableW = data.ScaleFactorAvailableW;
                StSetLimMsk = data.StSetLimMsk;
                StActCtl = data.StActCtl;
                TmSrc = data.TmSrc;
                Tms = data.Tms;
                RtSt = data.RtSt;
                Riso = data.Riso;
                ScaleFactorRiso = data.ScaleFactorRiso;
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