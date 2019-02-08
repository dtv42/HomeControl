// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RawStationData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using Newtonsoft.Json;
    using NetatmoLib.Converter;

    #endregion

    /// <summary>
    /// Class holding JSON data from the Netatmo weather station.
    /// </summary>
    public class RawStationData
    {
        public class BodyData
        {
            public class DeviceData
            {
                [JsonConverter(typeof(StationModuleDataConverter))]
                public class ModuleData
                {
                    [JsonProperty("_id")]
                    public string ID { get; set; } = string.Empty;

                    [JsonProperty("type")]
                    public string Type { get; set; } = string.Empty;

                    [JsonProperty("last_message")]
                    public long LastMessage { get; set; }

                    [JsonProperty("last_seen")]
                    public long LastSeen { get; set; }

                    [JsonProperty("data_type")]
                    public List<string> DataType { get; set; } = new List<string> { };

                    [JsonProperty("module_name")]
                    public string ModuleName { get; set; } = string.Empty;

                    [JsonProperty("last_setup")]
                    public long LastSetup { get; set; }

                    [JsonProperty("battery_vp")]
                    public int BatteryVP { get; set; }

                    [JsonProperty("battery_percent")]
                    public int BatteryPercent { get; set; }

                    [JsonProperty("rf_status")]
                    public int RFStatus { get; set; }

                    [JsonProperty("firmware")]
                    public int Firmware { get; set; }
                }

                [JsonConverter(typeof(StationModuleDataConverter))]
                public class Module1Data : ModuleData
                {
                    public class Dashboard
                    {
                        [JsonProperty("time_utc")]
                        public long TimeUTC { get; set; }

                        public double Temperature { get; set; }

                        [JsonProperty("temp_trend")]
                        public string TempTrend { get; set; } = string.Empty;

                        public double Humidity { get; set; }

                        [JsonProperty("date_max_temp")]
                        public long DateMaxTemp { get; set; }

                        [JsonProperty("date_min_temp")]
                        public long DateMinTemp { get; set; }

                        [JsonProperty("min_temp")]
                        public double MinTemp { get; set; }

                        [JsonProperty("max_temp")]
                        public double MaxTemp { get; set; }
                    }

                    [JsonProperty("dashboard_data")]
                    public Dashboard DashboardData { get; set; } = new Dashboard();
                }

                [JsonConverter(typeof(StationModuleDataConverter))]
                public class Module2Data : ModuleData
                {
                    public class Dashboard
                    {
                        public class WindHistoricData
                        {
                            public double WindStrength { get; set; }

                            public double WindAngle { get; set; }

                            [JsonProperty("time_utc")]
                            public long TimeUTC { get; set; }
                        }

                        public double WindAngle { get; set; }

                        public double WindStrength { get; set; }

                        public double GustAngle { get; set; }

                        public double GustStrength { get; set; }

                        [JsonProperty("time_utc")]
                        public long TimeUTC { get; set; }

                        public List<WindHistoricData> WindHistoric { get; set; } = new List<WindHistoricData> { };

                        [JsonProperty("date_max_wind_str")]
                        public long DateMaxWindStrength { get; set; }

                        [JsonProperty("date_max_temp")]
                        public long DateMaxTemp { get; set; }

                        [JsonProperty("date_min_temp")]
                        public long DateMinTemp { get; set; }

                        [JsonProperty("min_temp")]
                        public double MinTemp { get; set; }

                        [JsonProperty("max_temp")]
                        public double MaxTemp { get; set; }

                        [JsonProperty("max_wind_angle")]
                        public double MaxWindAngle { get; set; }

                        [JsonProperty("max_wind_str")]
                        public double MaxWindStrength { get; set; }
                    }

                    [JsonProperty("dashboard_data")]
                    public Dashboard DashboardData { get; set; } = new Dashboard();
                }

                [JsonConverter(typeof(StationModuleDataConverter))]
                public class Module3Data : ModuleData
                {
                    public class Dashboard
                    {
                        [JsonProperty("time_utc")]
                        public long TimeUTC { get; set; }

                        public double Rain { get; set; }

                        [JsonProperty("sum_rain_24")]
                        public double SumRain24 { get; set; }

                        [JsonProperty("sum_rain_1")]
                        public double SumRain1 { get; set; }
                    }

                    [JsonProperty("dashboard_data")]
                    public Dashboard DashboardData { get; set; } = new Dashboard();
                }

                [JsonConverter(typeof(StationModuleDataConverter))]
                public class Module4Data : ModuleData
                {
                    public class Dashboard
                    {
                        [JsonProperty("time_utc")]
                        public long TimeUTC { get; set; }

                        public double Temperature { get; set; }

                        [JsonProperty("temp_trend")]
                        public string TempTrend { get; set; } = string.Empty;

                        public double Humidity { get; set; }

                        public double CO2 { get; set; }

                        [JsonProperty("date_max_temp")]
                        public long DateMaxTemp { get; set; }

                        [JsonProperty("date_min_temp")]
                        public long DateMinTemp { get; set; }

                        [JsonProperty("min_temp")]
                        public double MinTemp { get; set; }

                        [JsonProperty("max_temp")]
                        public double MaxTemp { get; set; }
                    }

                    [JsonProperty("dashboard_data")]
                    public Dashboard DashboardData { get; set; } = new Dashboard();
                }

                public class PlaceData
                {
                    [JsonProperty("altitude")]
                    public double Altitude { get; set; }

                    [JsonProperty("city")]
                    public string City { get; set; } = string.Empty;

                    [JsonProperty("country")]
                    public string Country { get; set; } = string.Empty;

                    [JsonProperty("geoip_city")]
                    public string GeoIpCity { get; set; } = string.Empty;

                    [JsonProperty("improveLocProposed")]
                    public bool ImproveLocProposed { get; set; }

                    [JsonProperty("location")]
                    public List<double> Location { get; set; } = new List<double> { };

                    [JsonProperty("timezone")]
                    public string Timezone { get; set; } = string.Empty;
                }

                public class Dashboard
                {
                    [JsonProperty("time_utc")]
                    public long TimeUTC { get; set; }

                    public double AbsolutePressure { get; set; }

                    public double Noise { get; set; }

                    public double Temperature { get; set; }

                    [JsonProperty("temp_trend")]
                    public string TempTrend { get; set; } = string.Empty;

                    public double Humidity { get; set; }

                    public double Pressure { get; set; }

                    [JsonProperty("pressure_trend")]
                    public string PressureTrend { get; set; } = string.Empty;

                    public double CO2 { get; set; }

                    [JsonProperty("date_max_temp")]
                    public long DateMaxTemp { get; set; }

                    [JsonProperty("date_min_temp")]
                    public long DateMinTemp { get; set; }

                    [JsonProperty("min_temp")]
                    public double MinTemp { get; set; }

                    [JsonProperty("max_temp")]
                    public double MaxTemp { get; set; }
                }

                [JsonProperty("_id")]
                public string ID { get; set; } = string.Empty;

                [JsonProperty("cipher_id")]
                public string CipherID { get; set; } = string.Empty;

                [JsonProperty("last_status_store")]
                public long LastStatusStore { get; set; }

                [JsonProperty("modules")]
                public List<ModuleData> Modules { get; set; } = new List<ModuleData> { };

                [JsonProperty("place")]
                public PlaceData Place { get; set; }

                [JsonProperty("station_name")]
                public string StationName { get; set; } = string.Empty;

                [JsonProperty("type")]
                public string Type { get; set; } = string.Empty;

                [JsonProperty("dashboard_data")]
                public Dashboard DashboardData { get; set; }

                [JsonProperty("data_type")]
                public List<string> DataType { get; set; } = new List<string> { };

                [JsonProperty("co2_calibrating")]
                public bool CO2Calibrating { get; set; }

                [JsonProperty("date_setup")]
                public long DateSetup { get; set; }

                [JsonProperty("last_setup")]
                public long LastSetup { get; set; }

                [JsonProperty("module_name")]
                public string ModuleName { get; set; } = string.Empty;

                [JsonProperty("firmware")]
                public int Firmware { get; set; }

                [JsonProperty("last_upgrade")]
                public long LastUpgrade { get; set; }

                [JsonProperty("wifi_status")]
                public int WifiStatus { get; set; }
            }

            public class UserData
            {
                public class AdministrativeData
                {
                    [JsonProperty("country")]
                    public string Country { get; set; } = string.Empty;

                    [JsonProperty("feel_like_algo")]
                    public int FeelsLikeAlgorithm { get; set; }

                    [JsonProperty("lang")]
                    public string Language { get; set; } = string.Empty;

                    [JsonProperty("pressureunit")]
                    public int PressureUnit { get; set; }

                    [JsonProperty("reg_locale")]
                    public string RegLocale { get; set; } = string.Empty;

                    [JsonProperty("unit")]
                    public int Unit { get; set; }

                    [JsonProperty("windunit")]
                    public int WindUnit { get; set; }
                }

                [JsonProperty("mail")]
                public string Mail { get; set; } = string.Empty;

                [JsonProperty("administrative")]
                public AdministrativeData Administrative { get; set; } = new AdministrativeData();
            }

            [JsonProperty("devices")]
            public List<DeviceData> Devices { get; set; } = new List<DeviceData> { };

            [JsonProperty("user")]
            public UserData User { get; set; } = new UserData();
        }

        [JsonProperty("body")]
        public BodyData Body { get; set; } = new BodyData();

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("time_exec")]
        public double TimeExec { get; set; }

        [JsonProperty("time_server")]
        public long TimeServer { get; set; }
    }
}
