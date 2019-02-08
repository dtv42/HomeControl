// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerInfo.cs" company="DTV-Online">
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

    #endregion

    /// <summary>
    /// Class holding selected data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class LoggerInfo : DataValue, IPropertyHelper
    {
        #region Private Data Members

        private string _uniqueID = string.Empty;
        private string _productID = string.Empty;
        private string _platformID = string.Empty;
        private string _hWVersion = string.Empty;
        private string _sWVersion = string.Empty;
        private string _timezoneLocation = string.Empty;
        private string _timezoneName = string.Empty;
        private int _uTCOffset;
        private string _defaultLanguage = string.Empty;
        private double _cashFactor;
        private string _cashCurrency = string.Empty;
        private double _cO2Factor;
        private string _cO2Unit = string.Empty;

        #endregion

        #region Public Properties

        public string UniqueID { get => _uniqueID; set { _uniqueID = value; OnPropertyChanged("UniqueID"); } }
        public string ProductID { get => _productID; set { _productID = value; OnPropertyChanged("ProductID"); } }
        public string PlatformID { get => _platformID; set { _platformID = value; OnPropertyChanged("PlatformID"); } }
        public string HWVersion { get => _hWVersion; set { _hWVersion = value; OnPropertyChanged("HWVersion"); } }
        public string SWVersion { get => _sWVersion; set { _sWVersion = value; OnPropertyChanged("SWVersion"); } }
        public string TimezoneLocation { get => _timezoneLocation; set { _timezoneLocation = value; OnPropertyChanged("TimezoneLocation"); } }
        public string TimezoneName { get => _timezoneName; set { _timezoneName = value; OnPropertyChanged("TimezoneName"); } }
        public int UTCOffset { get => _uTCOffset; set { _uTCOffset = value; OnPropertyChanged("UTCOffset"); } }
        public string DefaultLanguage { get => _defaultLanguage; set { _defaultLanguage = value; OnPropertyChanged("DefaultLanguage"); } }
        public double CashFactor { get => _cashFactor; set { _cashFactor = value; OnPropertyChanged("CashFactor"); } }
        public string CashCurrency { get => _cashCurrency; set { _cashCurrency = value; OnPropertyChanged("CashCurrency"); } }
        public double CO2Factor { get => _cO2Factor; set { _cO2Factor = value; OnPropertyChanged("CO2Factor"); } }
        public string CO2Unit { get => _cO2Unit; set { _cO2Unit = value; OnPropertyChanged("CO2Unit"); } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in LoggerInfo.
        /// </summary>
        /// <param name="data">The Fronius data.</param>
        public void Refresh(FroniusData data)
        {
            if (data != null)
            {
                UniqueID = data.LoggerInfo.Logger.UniqueID;
                ProductID = data.LoggerInfo.Logger.ProductID;
                PlatformID = data.LoggerInfo.Logger.PlatformID;
                HWVersion = data.LoggerInfo.Logger.HWVersion;
                SWVersion = data.LoggerInfo.Logger.SWVersion;
                TimezoneLocation = data.LoggerInfo.Logger.TimezoneLocation;
                TimezoneName = data.LoggerInfo.Logger.TimezoneName;
                UTCOffset = data.LoggerInfo.Logger.UTCOffset;
                DefaultLanguage = data.LoggerInfo.Logger.DefaultLanguage;
                CashFactor = data.LoggerInfo.Logger.CashFactor;
                CashCurrency = data.LoggerInfo.Logger.CashCurrency;
                CO2Factor = data.LoggerInfo.Logger.CO2Factor;
                CO2Unit = data.LoggerInfo.Logger.CO2Unit;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the LoggerInfo class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(LoggerInfo).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the LoggerInfo class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(LoggerInfo), property) != null) ? true : false;

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
