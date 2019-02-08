// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds a the mapped Netatmo Station data.
    /// </summary>
    public class NetatmoData : DataValue, IPropertyHelper
    {
        #region Public Properties

        public StationDeviceData Device { get; set; } = new StationDeviceData();

        public UserData User { get; set; } = new UserData();

        public ResponseData Response { get; set; } = new ResponseData();

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in StationData.
        /// </summary>
        /// <param name="data">The Netatmo raw station data.</param>
        public void Refresh(RawStationData data)
        {
            if (data != null)
            {
                // Response data
                Response.Status = data.Status;
                Response.TimeExec = data.TimeExec;
                Response.TimeServer = DateTimeOffset.FromUnixTimeSeconds(data.TimeServer);
                // User data
                User.Mail = data.Body.User.Mail;
                User.Administrative.Country = data.Body.User.Administrative.Country;
                User.Administrative.FeelsLikeAlgorithm = data.Body.User.Administrative.FeelsLikeAlgorithm;
                User.Administrative.Language = data.Body.User.Administrative.Language;
                User.Administrative.PressureUnit = data.Body.User.Administrative.PressureUnit;
                User.Administrative.RegLocale = data.Body.User.Administrative.RegLocale;
                User.Administrative.Unit = data.Body.User.Administrative.Unit;
                User.Administrative.WindUnit = data.Body.User.Administrative.WindUnit;

                // Device data (only a single device is supported)
                if (data.Body.Devices.Count > 0)
                {
                    Device.ID = data.Body.Devices[0].ID;
                    Device.CipherID = data.Body.Devices[0].CipherID;
                    Device.StationName = data.Body.Devices[0].StationName;
                    Device.ModuleName = data.Body.Devices[0].ModuleName;
                    Device.Firmware = data.Body.Devices[0].Firmware;
                    Device.WifiStatus = StationDeviceData.GetWifiSignal(data.Body.Devices[0].WifiStatus);
                    Device.CO2Calibrating = data.Body.Devices[0].CO2Calibrating;
                    Device.Type = data.Body.Devices[0].Type;
                    Device.DataType = data.Body.Devices[0].DataType;
                    Device.LastStatusStore = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].LastStatusStore);
                    Device.DateSetup = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].DateSetup);
                    Device.LastSetup = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].LastSetup);
                    Device.LastUpgrade = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].LastUpgrade);
                    // Place data
                    Device.Place.Altitude = data.Body.Devices[0].Place.Altitude;
                    Device.Place.City = data.Body.Devices[0].Place.City;
                    Device.Place.Country = data.Body.Devices[0].Place.Country;
                    Device.Place.GeoIpCity = data.Body.Devices[0].Place.GeoIpCity;
                    Device.Place.ImproveLocProposed = data.Body.Devices[0].Place.ImproveLocProposed;

                    if (data.Body.Devices[0].Place.Location.Count == 2)
                    {
                        Device.Place.Location.Latitude = data.Body.Devices[0].Place.Location[0];
                        Device.Place.Location.Longitude = data.Body.Devices[0].Place.Location[1];
                    }

                    Device.Place.Timezone = data.Body.Devices[0].Place.Timezone;
                    // Dashboard data
                    Device.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].DashboardData.TimeUTC);
                    Device.DashboardData.Temperature = data.Body.Devices[0].DashboardData.Temperature;
                    Device.DashboardData.TempTrend = data.Body.Devices[0].DashboardData.TempTrend;
                    Device.DashboardData.Humidity = data.Body.Devices[0].DashboardData.Humidity;
                    Device.DashboardData.Noise = data.Body.Devices[0].DashboardData.Noise;
                    Device.DashboardData.CO2 = data.Body.Devices[0].DashboardData.CO2;
                    Device.DashboardData.Pressure = data.Body.Devices[0].DashboardData.Pressure;
                    Device.DashboardData.PressureTrend = data.Body.Devices[0].DashboardData.PressureTrend;
                    Device.DashboardData.AbsolutePressure = data.Body.Devices[0].DashboardData.AbsolutePressure;
                    Device.DashboardData.DateMaxTemp = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].DashboardData.DateMaxTemp);
                    Device.DashboardData.DateMinTemp = DateTimeOffset.FromUnixTimeSeconds(data.Body.Devices[0].DashboardData.DateMinTemp);
                    Device.DashboardData.MaxTemp = data.Body.Devices[0].DashboardData.MaxTemp;
                    Device.DashboardData.MinTemp = data.Body.Devices[0].DashboardData.MinTemp;

                    if (data.Body.Devices[0].Modules.Count > 0)
                    {
                        var outdoormodules = data.Body.Devices[0].Modules.Where(m => m.Type == "NAModule1").ToList();

                        if ((outdoormodules != null) && (outdoormodules.Count() == 1))
                        {
                            var module = outdoormodules[0] as RawStationData.BodyData.DeviceData.Module1Data;
                            // Outdoor module data
                            Device.OutdoorModule.ID = module.ID;
                            Device.OutdoorModule.Type = module.Type;
                            Device.OutdoorModule.DataType = module.DataType;
                            Device.OutdoorModule.ModuleName = module.ModuleName;
                            Device.OutdoorModule.LastMessage = DateTimeOffset.FromUnixTimeSeconds(module.LastMessage);
                            Device.OutdoorModule.LastSeen = DateTimeOffset.FromUnixTimeSeconds(module.LastSeen);
                            Device.OutdoorModule.LastSetup = DateTimeOffset.FromUnixTimeSeconds(module.LastSetup);
                            Device.OutdoorModule.BatteryVP = OutdoorModuleData.GetBatteryLevel(module.BatteryVP);
                            Device.OutdoorModule.BatteryPercent = module.BatteryPercent;
                            Device.OutdoorModule.RFStatus = OutdoorModuleData.GetRFSignal(module.RFStatus);
                            Device.OutdoorModule.Firmware = module.Firmware;
                            Device.OutdoorModule.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.TimeUTC);
                            Device.OutdoorModule.DashboardData.Temperature = module.DashboardData.Temperature;
                            Device.OutdoorModule.DashboardData.TempTrend = module.DashboardData.TempTrend;
                            Device.OutdoorModule.DashboardData.Humidity = module.DashboardData.Humidity;
                            Device.OutdoorModule.DashboardData.DateMaxTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMaxTemp);
                            Device.OutdoorModule.DashboardData.DateMinTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMinTemp);
                            Device.OutdoorModule.DashboardData.MaxTemp = module.DashboardData.MaxTemp;
                            Device.OutdoorModule.DashboardData.MinTemp = module.DashboardData.MinTemp;
                        }

                        var windgauges = data.Body.Devices[0].Modules.Where(m => m.Type == "NAModule2").ToList();

                        if ((windgauges != null) && (windgauges.Count() == 1))
                        {
                            var module = windgauges[0] as RawStationData.BodyData.DeviceData.Module2Data;
                            // Wind gauge data
                            Device.WindGauge.ID = module.ID;
                            Device.WindGauge.Type = module.Type;
                            Device.WindGauge.DataType = module.DataType;
                            Device.WindGauge.ModuleName = module.ModuleName;
                            Device.WindGauge.LastMessage = DateTimeOffset.FromUnixTimeSeconds(module.LastMessage);
                            Device.WindGauge.LastSeen = DateTimeOffset.FromUnixTimeSeconds(module.LastSeen);
                            Device.WindGauge.LastSetup = DateTimeOffset.FromUnixTimeSeconds(module.LastSetup);
                            Device.WindGauge.BatteryVP = IndoorModuleData.GetBatteryLevel(module.BatteryVP);
                            Device.WindGauge.BatteryPercent = module.BatteryPercent;
                            Device.WindGauge.RFStatus = IndoorModuleData.GetRFSignal(module.RFStatus);
                            Device.WindGauge.Firmware = module.Firmware;
                            Device.WindGauge.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.TimeUTC);
                            Device.WindGauge.DashboardData.WindAngle = module.DashboardData.WindAngle;
                            Device.WindGauge.DashboardData.WindStrength = module.DashboardData.WindStrength;
                            Device.WindGauge.DashboardData.GustAngle = module.DashboardData.GustAngle;
                            Device.WindGauge.DashboardData.GustStrength = module.DashboardData.GustStrength;
                            Device.WindGauge.DashboardData.MaxWindAngle = module.DashboardData.MaxWindAngle;
                            Device.WindGauge.DashboardData.MaxWindStrength = module.DashboardData.MaxWindStrength;
                            Device.WindGauge.DashboardData.DateMaxWindStrength = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMaxWindStrength);
                        }

                        var raingauges = data.Body.Devices[0].Modules.Where(m => m.Type == "NAModule3").ToList();

                        if ((raingauges != null) && (raingauges.Count() == 1))
                        {
                            var module = raingauges[0] as RawStationData.BodyData.DeviceData.Module3Data;
                            // Rain gauge data
                            Device.RainGauge.ID = module.ID;
                            Device.RainGauge.Type = module.Type;
                            Device.RainGauge.DataType = module.DataType;
                            Device.RainGauge.ModuleName = module.ModuleName;
                            Device.RainGauge.LastMessage = DateTimeOffset.FromUnixTimeSeconds(module.LastMessage);
                            Device.RainGauge.LastSeen = DateTimeOffset.FromUnixTimeSeconds(module.LastSeen);
                            Device.RainGauge.LastSetup = DateTimeOffset.FromUnixTimeSeconds(module.LastSetup);
                            Device.RainGauge.BatteryVP = IndoorModuleData.GetBatteryLevel(module.BatteryVP);
                            Device.RainGauge.BatteryPercent = module.BatteryPercent;
                            Device.RainGauge.RFStatus = IndoorModuleData.GetRFSignal(module.RFStatus);
                            Device.RainGauge.Firmware = module.Firmware;
                            Device.RainGauge.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.TimeUTC);
                            Device.RainGauge.DashboardData.Rain = module.DashboardData.Rain;
                            Device.RainGauge.DashboardData.SumRain1 = module.DashboardData.SumRain1;
                            Device.RainGauge.DashboardData.SumRain24 = module.DashboardData.SumRain24;
                        }

                        var indoormodules = data.Body.Devices[0].Modules.Where(m => m.Type == "NAModule4").ToList();

                        if (indoormodules != null)
                        {
                            if (indoormodules.Count() > 0)
                            {
                                var module = indoormodules[0] as RawStationData.BodyData.DeviceData.Module4Data;
                                // Indoor module 1 data
                                Device.IndoorModule1.ID = module.ID;
                                Device.IndoorModule1.Type = module.Type;
                                Device.IndoorModule1.DataType = module.DataType;
                                Device.IndoorModule1.ModuleName = module.ModuleName;
                                Device.IndoorModule1.LastMessage = DateTimeOffset.FromUnixTimeSeconds(module.LastMessage);
                                Device.IndoorModule1.LastSeen = DateTimeOffset.FromUnixTimeSeconds(module.LastSeen);
                                Device.IndoorModule1.LastSetup = DateTimeOffset.FromUnixTimeSeconds(module.LastSetup);
                                Device.IndoorModule1.BatteryVP = IndoorModuleData.GetBatteryLevel(module.BatteryVP);
                                Device.IndoorModule1.BatteryPercent = module.BatteryPercent;
                                Device.IndoorModule1.RFStatus = IndoorModuleData.GetRFSignal(module.RFStatus);
                                Device.IndoorModule1.Firmware = module.Firmware;
                                Device.IndoorModule1.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.TimeUTC);
                                Device.IndoorModule1.DashboardData.Temperature = module.DashboardData.Temperature;
                                Device.IndoorModule1.DashboardData.TempTrend = module.DashboardData.TempTrend;
                                Device.IndoorModule1.DashboardData.Humidity = module.DashboardData.Humidity;
                                Device.IndoorModule1.DashboardData.CO2 = module.DashboardData.CO2;
                                Device.IndoorModule1.DashboardData.DateMaxTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMaxTemp);
                                Device.IndoorModule1.DashboardData.DateMinTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMinTemp);
                                Device.IndoorModule1.DashboardData.MaxTemp = module.DashboardData.MaxTemp;
                                Device.IndoorModule1.DashboardData.MinTemp = module.DashboardData.MinTemp;
                            }

                            if (indoormodules.Count() > 1)
                            {
                                var module = indoormodules[1] as RawStationData.BodyData.DeviceData.Module4Data;
                                // Indoor module 2 data
                                Device.IndoorModule2.ID = module.ID;
                                Device.IndoorModule2.Type = module.Type;
                                Device.IndoorModule2.DataType = module.DataType;
                                Device.IndoorModule2.ModuleName = module.ModuleName;
                                Device.IndoorModule2.LastMessage = DateTimeOffset.FromUnixTimeSeconds(module.LastMessage);
                                Device.IndoorModule2.LastSeen = DateTimeOffset.FromUnixTimeSeconds(module.LastSeen);
                                Device.IndoorModule2.LastSetup = DateTimeOffset.FromUnixTimeSeconds(module.LastSetup);
                                Device.IndoorModule2.BatteryVP = IndoorModuleData.GetBatteryLevel(module.BatteryVP);
                                Device.IndoorModule2.BatteryPercent = module.BatteryPercent;
                                Device.IndoorModule2.RFStatus = IndoorModuleData.GetRFSignal(module.RFStatus);
                                Device.IndoorModule2.Firmware = module.Firmware;
                                Device.IndoorModule2.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.TimeUTC);
                                Device.IndoorModule2.DashboardData.Temperature = module.DashboardData.Temperature;
                                Device.IndoorModule2.DashboardData.TempTrend = module.DashboardData.TempTrend;
                                Device.IndoorModule2.DashboardData.Humidity = module.DashboardData.Humidity;
                                Device.IndoorModule2.DashboardData.CO2 = module.DashboardData.CO2;
                                Device.IndoorModule2.DashboardData.DateMaxTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMaxTemp);
                                Device.IndoorModule2.DashboardData.DateMinTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMinTemp);
                                Device.IndoorModule2.DashboardData.MaxTemp = module.DashboardData.MaxTemp;
                                Device.IndoorModule2.DashboardData.MinTemp = module.DashboardData.MinTemp;
                            }

                            if (indoormodules.Count() > 2)
                            {
                                var module = indoormodules[2] as RawStationData.BodyData.DeviceData.Module4Data;
                                // Indoor module 3 data
                                Device.IndoorModule3.ID = module.ID;
                                Device.IndoorModule3.Type = module.Type;
                                Device.IndoorModule3.DataType = module.DataType;
                                Device.IndoorModule3.ModuleName = module.ModuleName;
                                Device.IndoorModule3.LastMessage = DateTimeOffset.FromUnixTimeSeconds(module.LastMessage);
                                Device.IndoorModule3.LastSeen = DateTimeOffset.FromUnixTimeSeconds(module.LastSeen);
                                Device.IndoorModule3.LastSetup = DateTimeOffset.FromUnixTimeSeconds(module.LastSetup);
                                Device.IndoorModule3.BatteryVP = IndoorModuleData.GetBatteryLevel(module.BatteryVP);
                                Device.IndoorModule3.BatteryPercent = module.BatteryPercent;
                                Device.IndoorModule3.RFStatus = ModuleData.GetRFSignal(module.RFStatus);
                                Device.IndoorModule3.Firmware = module.Firmware;
                                Device.IndoorModule3.DashboardData.TimeUTC = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.TimeUTC);
                                Device.IndoorModule3.DashboardData.Temperature = module.DashboardData.Temperature;
                                Device.IndoorModule3.DashboardData.TempTrend = module.DashboardData.TempTrend;
                                Device.IndoorModule3.DashboardData.Humidity = module.DashboardData.Humidity;
                                Device.IndoorModule3.DashboardData.CO2 = module.DashboardData.CO2;
                                Device.IndoorModule3.DashboardData.DateMaxTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMaxTemp);
                                Device.IndoorModule3.DashboardData.DateMinTemp = DateTimeOffset.FromUnixTimeSeconds(module.DashboardData.DateMinTemp);
                                Device.IndoorModule3.DashboardData.MaxTemp = module.DashboardData.MaxTemp;
                                Device.IndoorModule3.DashboardData.MinTemp = module.DashboardData.MinTemp;
                            }
                        }
                    }
                }

                Status = Good;
            }
            else
            {
                Status = Uncertain;
            }
        }

        #endregion

        #region Public Property Helper

        /// <summary>
        /// Gets the property list for the NetatmoData class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static string[] GetProperties()
            => typeof(NetatmoData).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(p => p.Name).ToArray();

        /// <summary>
        /// Returns true if property with the specified name is found in the NetatmoData class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property) => PropertyValue.GetPropertyInfo(typeof(NetatmoData), property) != null;

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> data for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(NetatmoData), property);

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

        #endregion Public Property Helper
    }
}
