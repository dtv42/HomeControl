// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    public class StationDeviceData
    {
        #region Public Properties

        public string ID { get; set; } = string.Empty;
        public string CipherID { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public int Firmware { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WifiSignal WifiStatus { get; set; }
        public bool CO2Calibrating { get; set; }
        public string Type { get; set; } = string.Empty;
        public List<string> DataType { get; set; } = new List<string> { };
        public StationPlaceData Place { get; set; } = new StationPlaceData();
        public StationDeviceDashboard DashboardData { get; set; } = new StationDeviceDashboard();
        public DateTimeOffset LastStatusStore { get; set; }
        public DateTimeOffset DateSetup { get; set; }
        public DateTimeOffset LastSetup { get; set; }
        public DateTimeOffset LastUpgrade { get; set; }

        public OutdoorModuleData OutdoorModule { get; set; } = new OutdoorModuleData();
        public IndoorModuleData IndoorModule1 { get; set; } = new IndoorModuleData();
        public IndoorModuleData IndoorModule2 { get; set; } = new IndoorModuleData();
        public IndoorModuleData IndoorModule3 { get; set; } = new IndoorModuleData();
        public RainGaugeData RainGauge { get; set; } = new RainGaugeData();
        public WindGaugeData WindGauge { get; set; } = new WindGaugeData();

        #endregion

        #region Public Methods

        public static WifiSignal GetWifiSignal(int wifistatus)
        {
            if (wifistatus >= 86) return WifiSignal.Bad;
            else if (wifistatus >= 71) return WifiSignal.Average;
            else if (wifistatus >= 56) return WifiSignal.Good;
            else if (wifistatus < 56) return WifiSignal.Good;
            else return WifiSignal.Unknown;
        }

        #endregion
    }
}
