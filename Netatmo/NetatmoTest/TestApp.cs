// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoTest
{
    #region Using Directives

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using NetatmoLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Netatmo Test Collection")]
    public class TestApp
    {
        #region Private Data Members

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestApp(ITestOutputHelper output)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<Netatmo>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Netatmo web service found", 0)]
        [InlineData("-?", "Usage: NetatmoApp [options] [command]", 0)]
        [InlineData("--help", "Usage: NetatmoApp [options] [command]", 0)]
        [InlineData("--address https://api.netatmo.net", "Netatmo web service found", 0)]
        [InlineData("--timeout 10", "Netatmo web service found", 0)]
        [InlineData("--user peter.trimmel@live.com", "Netatmo web service found", 0)]
        [InlineData("-", "Unrecognized command or argument '-'", -1)]
        [InlineData("---", "Unrecognized option '---'", -1)]
        public void TestRootCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("info", "Usage: NetatmoApp info [options]", 0)]
        [InlineData("info -?", "Usage: NetatmoApp info [options]", 0)]
        [InlineData("info --help", "Usage: NetatmoApp info [options]", 0)]
        [InlineData("info -m", "Main Module", 0)]
        [InlineData("info --main", "Main Module", 0)]
        [InlineData("info -o", "Outdoor Module", 0)]
        [InlineData("info --outdoor", "Outdoor Module", 0)]
        [InlineData("info -1", "Indoor Module 1", 0)]
        [InlineData("info --indoor1", "Indoor Module 1", 0)]
        [InlineData("info -2", "Indoor Module 2", 0)]
        [InlineData("info --indoor2", "Indoor Module 2", 0)]
        [InlineData("info -3", "Indoor Module 3", 0)]
        [InlineData("info --indoor3", "Indoor Module 3", 0)]
        [InlineData("info -r", "Rain Gauge", 0)]
        [InlineData("info --rain", "Rain Gauge", 0)]
        [InlineData("info -w", "Wind Gauge", 0)]
        [InlineData("info --wind", "Wind Gauge", 0)]
        public void TestInfoCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("read", "Yspertal", 0)]
        [InlineData("read MainModule", "Value of property 'MainModule'", 0)]
        [InlineData("read MainModule.TimeUTC", "Value of property 'MainModule.TimeUTC'", 0)]
        [InlineData("read MainModule.Temperature", "Value of property 'MainModule.Temperature'", 0)]
        [InlineData("read MainModule.TempTrend", "Value of property 'MainModule.TempTrend'", 0)]
        [InlineData("read MainModule.Humidity", "Value of property 'MainModule.Humidity'", 0)]
        [InlineData("read MainModule.Noise", "Value of property 'MainModule.Noise'", 0)]
        [InlineData("read MainModule.CO2", "Value of property 'MainModule.CO2'", 0)]
        [InlineData("read MainModule.Pressure", "Value of property 'MainModule.Pressure'", 0)]
        [InlineData("read MainModule.PressureTrend", "Value of property 'MainModule.PressureTrend'", 0)]
        [InlineData("read MainModule.AbsolutePressure", "Value of property 'MainModule.AbsolutePressure'", 0)]
        [InlineData("read MainModule.DateMaxTemp", "Value of property 'MainModule.DateMaxTemp'", 0)]
        [InlineData("read MainModule.DateMinTemp", "Value of property 'MainModule.DateMinTemp'", 0)]
        [InlineData("read MainModule.MinTemp", "Value of property 'MainModule.MinTemp'", 0)]
        [InlineData("read MainModule.MaxTemp", "Value of property 'MainModule.MaxTemp'", 0)]

        [InlineData("read OutdoorModule", "Value of property 'OutdoorModule'", 0)]
        [InlineData("read OutdoorModule.TimeUTC", "Value of property 'OutdoorModule.TimeUTC'", 0)]
        [InlineData("read OutdoorModule.Temperature", "Value of property 'OutdoorModule.Temperature'", 0)]
        [InlineData("read OutdoorModule.TempTrend", "Value of property 'OutdoorModule.TempTrend'", 0)]
        [InlineData("read OutdoorModule.Humidity", "Value of property 'OutdoorModule.Humidity'", 0)]
        [InlineData("read OutdoorModule.DateMaxTemp", "Value of property 'OutdoorModule.DateMaxTemp'", 0)]
        [InlineData("read OutdoorModule.DateMinTemp", "Value of property 'OutdoorModule.DateMinTemp'", 0)]
        [InlineData("read OutdoorModule.MaxTemp", "Value of property 'OutdoorModule.MaxTemp'", 0)]
        [InlineData("read OutdoorModule.MinTemp", "Value of property 'OutdoorModule.MinTemp'", 0)]

        [InlineData("read IndoorModule1", "Value of property 'IndoorModule1'", 0)]
        [InlineData("read IndoorModule1.TimeUTC", "Value of property 'IndoorModule1.TimeUTC'", 0)]
        [InlineData("read IndoorModule1.Temperature", "Value of property 'IndoorModule1.Temperature'", 0)]
        [InlineData("read IndoorModule1.TempTrend", "Value of property 'IndoorModule1.TempTrend'", 0)]
        [InlineData("read IndoorModule1.Humidity", "Value of property 'IndoorModule1.Humidity'", 0)]
        [InlineData("read IndoorModule1.CO2", "Value of property 'IndoorModule1.CO2'", 0)]
        [InlineData("read IndoorModule1.DateMaxTemp", "Value of property 'IndoorModule1.DateMaxTemp'", 0)]
        [InlineData("read IndoorModule1.DateMinTemp", "Value of property 'IndoorModule1.DateMinTemp'", 0)]
        [InlineData("read IndoorModule1.MaxTemp", "Value of property 'IndoorModule1.MaxTemp'", 0)]
        [InlineData("read IndoorModule1.MinTemp", "Value of property 'IndoorModule1.MinTemp'", 0)]

        [InlineData("read IndoorModule2", "Value of property 'IndoorModule2'", 0)]
        [InlineData("read IndoorModule2.TimeUTC", "Value of property 'IndoorModule2.TimeUTC'", 0)]
        [InlineData("read IndoorModule2.Temperature", "Value of property 'IndoorModule2.Temperature'", 0)]
        [InlineData("read IndoorModule2.TempTrend", "Value of property 'IndoorModule2.TempTrend'", 0)]
        [InlineData("read IndoorModule2.Humidity", "Value of property 'IndoorModule2.Humidity'", 0)]
        [InlineData("read IndoorModule2.CO2", "Value of property 'IndoorModule2.CO2'", 0)]
        [InlineData("read IndoorModule2.DateMaxTemp", "Value of property 'IndoorModule2.DateMaxTemp'", 0)]
        [InlineData("read IndoorModule2.DateMinTemp", "Value of property 'IndoorModule2.DateMinTemp'", 0)]
        [InlineData("read IndoorModule2.MaxTemp", "Value of property 'IndoorModule2.MaxTemp'", 0)]
        [InlineData("read IndoorModule2.MinTemp", "Value of property 'IndoorModule2.MinTemp'", 0)]

        [InlineData("read IndoorModule3", "Value of property 'IndoorModule3'", 0)]
        [InlineData("read IndoorModule3.TimeUTC", "Value of property 'IndoorModule3.TimeUTC'", 0)]
        [InlineData("read IndoorModule3.Temperature", "Value of property 'IndoorModule3.Temperature'", 0)]
        [InlineData("read IndoorModule3.TempTrend", "Value of property 'IndoorModule3.TempTrend'", 0)]
        [InlineData("read IndoorModule3.Humidity", "Value of property 'IndoorModule3.Humidity'", 0)]
        [InlineData("read IndoorModule3.CO2", "Value of property 'IndoorModule3.CO2'", 0)]
        [InlineData("read IndoorModule3.DateMaxTemp", "Value of property 'IndoorModule3.DateMaxTemp'", 0)]
        [InlineData("read IndoorModule3.DateMinTemp", "Value of property 'IndoorModule3.DateMinTemp'", 0)]
        [InlineData("read IndoorModule3.MaxTemp", "Value of property 'IndoorModule3.MaxTemp'", 0)]
        [InlineData("read IndoorModule3.MinTemp", "Value of property 'IndoorModule3.MinTemp'", 0)]

        [InlineData("read RainGauge", "Value of property 'RainGauge'", 0)]
        [InlineData("read RainGauge.TimeUTC", "Value of property 'RainGauge.TimeUTC'", 0)]
        [InlineData("read RainGauge.Rain", "Value of property 'RainGauge.Rain'", 0)]
        [InlineData("read RainGauge.SumRain1", "Value of property 'RainGauge.SumRain1'", 0)]
        [InlineData("read RainGauge.SumRain24", "Value of property 'RainGauge.SumRain24'", 0)]

        [InlineData("read WindGauge", "Value of property 'WindGauge'", 0)]
        [InlineData("read WindGauge.TimeUTC", "Value of property 'WindGauge.TimeUTC'", 0)]
        [InlineData("read WindGauge.WindAngle", "Value of property 'WindGauge.WindAngle'", 0)]
        [InlineData("read WindGauge.WindStrength", "Value of property 'WindGauge.WindStrength'", 0)]
        [InlineData("read WindGauge.GustAngle", "Value of property 'WindGauge.GustAngle'", 0)]
        [InlineData("read WindGauge.GustStrength", "Value of property 'WindGauge.GustStrength'", 0)]
        [InlineData("read WindGauge.MaxWindAngle", "Value of property 'WindGauge.MaxWindAngle'", 0)]
        [InlineData("read WindGauge.MaxWindStrength", "Value of property 'WindGauge.MaxWindStrength'", 0)]
        [InlineData("read WindGauge.DateMaxWindStrength", "Value of property 'WindGauge.DateMaxWindStrength'", 0)]

        [InlineData("read Station.Device", "Value of property 'Station.Device'", 0)]
        [InlineData("read Station.Device.ID", "Value of property 'Station.Device.ID'", 0)]
        [InlineData("read Station.Device.CipherID", "Value of property 'Station.Device.CipherID'", 0)]
        [InlineData("read Station.Device.StationName", "Value of property 'Station.Device.StationName'", 0)]
        [InlineData("read Station.Device.ModuleName", "Value of property 'Station.Device.ModuleName'", 0)]
        [InlineData("read Station.Device.Firmware", "Value of property 'Station.Device.Firmware'", 0)]
        [InlineData("read Station.Device.WifiStatus", "Value of property 'Station.Device.WifiStatus'", 0)]
        [InlineData("read Station.Device.CO2Calibrating", "Value of property 'Station.Device.CO2Calibrating'", 0)]
        [InlineData("read Station.Device.Type", "Value of property 'Station.Device.Type'", 0)]
        [InlineData("read Station.Device.DataType", "Value of property 'Station.Device.DataType'[0]", 0)]

        [InlineData("read Station.Device.Place", "Value of property 'Station.Device.Place'", 0)]
        [InlineData("read Station.Device.Place.Altitude", "Value of property 'Station.Device.Place.Altitude'", 0)]
        [InlineData("read Station.Device.Place.City", "Value of property 'Station.Device.Place.City'", 0)]
        [InlineData("read Station.Device.Place.Country", "Value of property 'Station.Device.Place.Country'", 0)]
        [InlineData("read Station.Device.Place.GeoIpCity", "Value of property 'Station.Device.Place.GeoIpCity'", 0)]
        [InlineData("read Station.Device.Place.ImproveLocProposed", "Value of property 'Station.Device.Place.ImproveLocProposed'", 0)]
        [InlineData("read Station.Device.Place.Location", "Value of property 'Station.Device.Place.Location'", 0)]
        [InlineData("read Station.Device.Place.Location.Latitude", "Value of property 'Station.Device.Place.Location.Latitude'", 0)]
        [InlineData("read Station.Device.Place.Location.Longitude", "Value of property 'Station.Device.Place.Location.Longitude'", 0)]
        [InlineData("read Station.Device.Place.Timezone", "Value of property 'Station.Device.Place.Timezone'", 0)]

        [InlineData("read Station.Device.DashboardData", "Value of property 'Station.Device.DashboardData'", 0)]
        [InlineData("read Station.Device.DashboardData.TimeUTC", "Value of property 'Station.Device.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.DashboardData.Temperature", "Value of property 'Station.Device.DashboardData.Temperature'", 0)]
        [InlineData("read Station.Device.DashboardData.TempTrend", "Value of property 'Station.Device.DashboardData.TempTrend'", 0)]
        [InlineData("read Station.Device.DashboardData.Humidity", "Value of property 'Station.Device.DashboardData.Humidity'", 0)]
        [InlineData("read Station.Device.DashboardData.Noise", "Value of property 'Station.Device.DashboardData.Noise'", 0)]
        [InlineData("read Station.Device.DashboardData.CO2", "Value of property 'Station.Device.DashboardData.CO2'", 0)]
        [InlineData("read Station.Device.DashboardData.Pressure", "Value of property 'Station.Device.DashboardData.Pressure'", 0)]
        [InlineData("read Station.Device.DashboardData.PressureTrend", "Value of property 'Station.Device.DashboardData.PressureTrend'", 0)]
        [InlineData("read Station.Device.DashboardData.AbsolutePressure", "Value of property 'Station.Device.DashboardData.AbsolutePressure'", 0)]
        [InlineData("read Station.Device.DashboardData.DateMaxTemp", "Value of property 'Station.Device.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Station.Device.DashboardData.DateMinTemp", "Value of property 'Station.Device.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Station.Device.DashboardData.MinTemp", "Value of property 'Station.Device.DashboardData.MinTemp'", 0)]
        [InlineData("read Station.Device.DashboardData.MaxTemp", "Value of property 'Station.Device.DashboardData.MaxTemp'", 0)]

        [InlineData("read Station.Device.LastStatusStore", "Value of property 'Station.Device.LastStatusStore'", 0)]
        [InlineData("read Station.Device.DateSetup", "Value of property 'Station.Device.DateSetup'", 0)]
        [InlineData("read Station.Device.LastSetup", "Value of property 'Station.Device.LastSetup'", 0)]
        [InlineData("read Station.Device.LastUpgrade", "Value of property 'Station.Device.LastUpgrade'", 0)]

        [InlineData("read Station.Device.OutdoorModule", "Value of property 'Station.Device.OutdoorModule'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData", "Value of property 'Station.Device.OutdoorModule.DashboardData'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.TimeUTC", "Value of property 'Station.Device.OutdoorModule.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.Temperature", "Value of property 'Station.Device.OutdoorModule.DashboardData.Temperature'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.TempTrend", "Value of property 'Station.Device.OutdoorModule.DashboardData.TempTrend'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.Humidity", "Value of property 'Station.Device.OutdoorModule.DashboardData.Humidity'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.DateMaxTemp", "Value of property 'Station.Device.OutdoorModule.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.DateMinTemp", "Value of property 'Station.Device.OutdoorModule.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.MaxTemp", "Value of property 'Station.Device.OutdoorModule.DashboardData.MaxTemp'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DashboardData.MinTemp", "Value of property 'Station.Device.OutdoorModule.DashboardData.MinTemp'", 0)]
        [InlineData("read Station.Device.OutdoorModule.ID", "Value of property 'Station.Device.OutdoorModule.ID'", 0)]
        [InlineData("read Station.Device.OutdoorModule.Type", "Value of property 'Station.Device.OutdoorModule.Type'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DataType", "Value of property 'Station.Device.OutdoorModule.DataType'", 0)]
        [InlineData("read Station.Device.OutdoorModule.DataType", "Value of property 'Station.Device.OutdoorModule.DataType'[0]", 0)]
        [InlineData("read Station.Device.OutdoorModule.ModuleName", "Value of property 'Station.Device.OutdoorModule.ModuleName'", 0)]
        [InlineData("read Station.Device.OutdoorModule.LastMessage", "Value of property 'Station.Device.OutdoorModule.LastMessage'", 0)]
        [InlineData("read Station.Device.OutdoorModule.LastSeen", "Value of property 'Station.Device.OutdoorModule.LastSeen'", 0)]
        [InlineData("read Station.Device.OutdoorModule.LastSetup", "Value of property 'Station.Device.OutdoorModule.LastSetup'", 0)]
        [InlineData("read Station.Device.OutdoorModule.BatteryVP", "Value of property 'Station.Device.OutdoorModule.BatteryVP'", 0)]
        [InlineData("read Station.Device.OutdoorModule.BatteryPercent", "Value of property 'Station.Device.OutdoorModule.BatteryPercent'", 0)]
        [InlineData("read Station.Device.OutdoorModule.RFStatus", "Value of property 'Station.Device.OutdoorModule.RFStatus'", 0)]
        [InlineData("read Station.Device.OutdoorModule.Firmware", "Value of property 'Station.Device.OutdoorModule.Firmware'", 0)]

        [InlineData("read Station.Device.IndoorModule1", "Value of property 'Station.Device.IndoorModule1'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData", "Value of property 'Station.Device.IndoorModule1.DashboardData'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.TimeUTC", "Value of property 'Station.Device.IndoorModule1.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.Temperature", "Value of property 'Station.Device.IndoorModule1.DashboardData.Temperature'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.TempTrend", "Value of property 'Station.Device.IndoorModule1.DashboardData.TempTrend'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.Humidity", "Value of property 'Station.Device.IndoorModule1.DashboardData.Humidity'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.CO2", "Value of property 'Station.Device.IndoorModule1.DashboardData.CO2'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.DateMaxTemp", "Value of property 'Station.Device.IndoorModule1.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.DateMinTemp", "Value of property 'Station.Device.IndoorModule1.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.MaxTemp", "Value of property 'Station.Device.IndoorModule1.DashboardData.MaxTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DashboardData.MinTemp", "Value of property 'Station.Device.IndoorModule1.DashboardData.MinTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule1.ID", "Value of property 'Station.Device.IndoorModule1.ID'", 0)]
        [InlineData("read Station.Device.IndoorModule1.Type", "Value of property 'Station.Device.IndoorModule1.Type'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DataType", "Value of property 'Station.Device.IndoorModule1.DataType'", 0)]
        [InlineData("read Station.Device.IndoorModule1.DataType", "Value of property 'Station.Device.IndoorModule1.DataType'[0]", 0)]
        [InlineData("read Station.Device.IndoorModule1.ModuleName", "Value of property 'Station.Device.IndoorModule1.ModuleName'", 0)]
        [InlineData("read Station.Device.IndoorModule1.LastMessage", "Value of property 'Station.Device.IndoorModule1.LastMessage'", 0)]
        [InlineData("read Station.Device.IndoorModule1.LastSeen", "Value of property 'Station.Device.IndoorModule1.LastSeen'", 0)]
        [InlineData("read Station.Device.IndoorModule1.LastSetup", "Value of property 'Station.Device.IndoorModule1.LastSetup'", 0)]
        [InlineData("read Station.Device.IndoorModule1.BatteryVP", "Value of property 'Station.Device.IndoorModule1.BatteryVP'", 0)]
        [InlineData("read Station.Device.IndoorModule1.BatteryPercent", "Value of property 'Station.Device.IndoorModule1.BatteryPercent'", 0)]
        [InlineData("read Station.Device.IndoorModule1.RFStatus", "Value of property 'Station.Device.IndoorModule1.RFStatus'", 0)]
        [InlineData("read Station.Device.IndoorModule1.Firmware", "Value of property 'Station.Device.IndoorModule1.Firmware'", 0)]

        [InlineData("read Station.Device.IndoorModule2", "Value of property 'Station.Device.IndoorModule2'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData", "Value of property 'Station.Device.IndoorModule2.DashboardData'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.TimeUTC", "Value of property 'Station.Device.IndoorModule2.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.Temperature", "Value of property 'Station.Device.IndoorModule2.DashboardData.Temperature'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.TempTrend", "Value of property 'Station.Device.IndoorModule2.DashboardData.TempTrend'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.Humidity", "Value of property 'Station.Device.IndoorModule2.DashboardData.Humidity'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.CO2", "Value of property 'Station.Device.IndoorModule2.DashboardData.CO2'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.DateMaxTemp", "Value of property 'Station.Device.IndoorModule2.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.DateMinTemp", "Value of property 'Station.Device.IndoorModule2.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.MaxTemp", "Value of property 'Station.Device.IndoorModule2.DashboardData.MaxTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DashboardData.MinTemp", "Value of property 'Station.Device.IndoorModule2.DashboardData.MinTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule2.ID", "Value of property 'Station.Device.IndoorModule2.ID'", 0)]
        [InlineData("read Station.Device.IndoorModule2.Type", "Value of property 'Station.Device.IndoorModule2.Type'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DataType", "Value of property 'Station.Device.IndoorModule2.DataType'", 0)]
        [InlineData("read Station.Device.IndoorModule2.DataType", "Value of property 'Station.Device.IndoorModule2.DataType'[0]", 0)]
        [InlineData("read Station.Device.IndoorModule2.ModuleName", "Value of property 'Station.Device.IndoorModule2.ModuleName'", 0)]
        [InlineData("read Station.Device.IndoorModule2.LastMessage", "Value of property 'Station.Device.IndoorModule2.LastMessage'", 0)]
        [InlineData("read Station.Device.IndoorModule2.LastSeen", "Value of property 'Station.Device.IndoorModule2.LastSeen'", 0)]
        [InlineData("read Station.Device.IndoorModule2.LastSetup", "Value of property 'Station.Device.IndoorModule2.LastSetup'", 0)]
        [InlineData("read Station.Device.IndoorModule2.BatteryVP", "Value of property 'Station.Device.IndoorModule2.BatteryVP'", 0)]
        [InlineData("read Station.Device.IndoorModule2.BatteryPercent", "Value of property 'Station.Device.IndoorModule2.BatteryPercent'", 0)]
        [InlineData("read Station.Device.IndoorModule2.RFStatus", "Value of property 'Station.Device.IndoorModule2.RFStatus'", 0)]
        [InlineData("read Station.Device.IndoorModule2.Firmware", "Value of property 'Station.Device.IndoorModule2.Firmware'", 0)]

        [InlineData("read Station.Device.IndoorModule3", "Value of property 'Station.Device.IndoorModule3'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData", "Value of property 'Station.Device.IndoorModule3.DashboardData'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.TimeUTC", "Value of property 'Station.Device.IndoorModule3.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.Temperature", "Value of property 'Station.Device.IndoorModule3.DashboardData.Temperature'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.TempTrend", "Value of property 'Station.Device.IndoorModule3.DashboardData.TempTrend'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.Humidity", "Value of property 'Station.Device.IndoorModule3.DashboardData.Humidity'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.CO2", "Value of property 'Station.Device.IndoorModule3.DashboardData.CO2'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.DateMaxTemp", "Value of property 'Station.Device.IndoorModule3.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.DateMinTemp", "Value of property 'Station.Device.IndoorModule3.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.MaxTemp", "Value of property 'Station.Device.IndoorModule3.DashboardData.MaxTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DashboardData.MinTemp", "Value of property 'Station.Device.IndoorModule3.DashboardData.MinTemp'", 0)]
        [InlineData("read Station.Device.IndoorModule3.ID", "Value of property 'Station.Device.IndoorModule3.ID'", 0)]
        [InlineData("read Station.Device.IndoorModule3.Type", "Value of property 'Station.Device.IndoorModule3.Type'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DataType", "Value of property 'Station.Device.IndoorModule3.DataType'", 0)]
        [InlineData("read Station.Device.IndoorModule3.DataType", "Value of property 'Station.Device.IndoorModule3.DataType'[0]", 0)]
        [InlineData("read Station.Device.IndoorModule3.ModuleName", "Value of property 'Station.Device.IndoorModule3.ModuleName'", 0)]
        [InlineData("read Station.Device.IndoorModule3.LastMessage", "Value of property 'Station.Device.IndoorModule3.LastMessage'", 0)]
        [InlineData("read Station.Device.IndoorModule3.LastSeen", "Value of property 'Station.Device.IndoorModule3.LastSeen'", 0)]
        [InlineData("read Station.Device.IndoorModule3.LastSetup", "Value of property 'Station.Device.IndoorModule3.LastSetup'", 0)]
        [InlineData("read Station.Device.IndoorModule3.BatteryVP", "Value of property 'Station.Device.IndoorModule3.BatteryVP'", 0)]
        [InlineData("read Station.Device.IndoorModule3.BatteryPercent", "Value of property 'Station.Device.IndoorModule3.BatteryPercent'", 0)]
        [InlineData("read Station.Device.IndoorModule3.RFStatus", "Value of property 'Station.Device.IndoorModule3.RFStatus'", 0)]
        [InlineData("read Station.Device.IndoorModule3.Firmware", "Value of property 'Station.Device.IndoorModule3.Firmware'", 0)]

        [InlineData("read Station.Device.RainGauge", "Value of property 'Station.Device.RainGauge'", 0)]
        [InlineData("read Station.Device.RainGauge.DashboardData", "Value of property 'Station.Device.RainGauge.DashboardData'", 0)]
        [InlineData("read Station.Device.RainGauge.DashboardData.TimeUTC", "Value of property 'Station.Device.RainGauge.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.RainGauge.DashboardData.Rain", "Value of property 'Station.Device.RainGauge.DashboardData.Rain'", 0)]
        [InlineData("read Station.Device.RainGauge.DashboardData.SumRain1", "Value of property 'Station.Device.RainGauge.DashboardData.SumRain1'", 0)]
        [InlineData("read Station.Device.RainGauge.DashboardData.SumRain24", "Value of property 'Station.Device.RainGauge.DashboardData.SumRain24'", 0)]
        [InlineData("read Station.Device.RainGauge.ID", "Value of property 'Station.Device.RainGauge.ID'", 0)]
        [InlineData("read Station.Device.RainGauge.Type", "Value of property 'Station.Device.RainGauge.Type'", 0)]
        [InlineData("read Station.Device.RainGauge.DataType", "Value of property 'Station.Device.RainGauge.DataType'", 0)]
        [InlineData("read Station.Device.RainGauge.DataType", "Value of property 'Station.Device.RainGauge.DataType'[0]", 0)]
        [InlineData("read Station.Device.RainGauge.ModuleName", "Value of property 'Station.Device.RainGauge.ModuleName'", 0)]
        [InlineData("read Station.Device.RainGauge.LastMessage", "Value of property 'Station.Device.RainGauge.LastMessage'", 0)]
        [InlineData("read Station.Device.RainGauge.LastSeen", "Value of property 'Station.Device.RainGauge.LastSeen'", 0)]
        [InlineData("read Station.Device.RainGauge.LastSetup", "Value of property 'Station.Device.RainGauge.LastSetup'", 0)]
        [InlineData("read Station.Device.RainGauge.BatteryVP", "Value of property 'Station.Device.RainGauge.BatteryVP'", 0)]
        [InlineData("read Station.Device.RainGauge.BatteryPercent", "Value of property 'Station.Device.RainGauge.BatteryPercent'", 0)]
        [InlineData("read Station.Device.RainGauge.RFStatus", "Value of property 'Station.Device.RainGauge.RFStatus'", 0)]
        [InlineData("read Station.Device.RainGauge.Firmware", "Value of property 'Station.Device.RainGauge.Firmware'", 0)]

        [InlineData("read Station.Device.WindGauge", "Value of property 'Station.Device.WindGauge'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData", "Value of property 'Station.Device.WindGauge.DashboardData'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.TimeUTC", "Value of property 'Station.Device.WindGauge.DashboardData.TimeUTC'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.WindAngle", "Value of property 'Station.Device.WindGauge.DashboardData.WindAngle'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.WindStrength", "Value of property 'Station.Device.WindGauge.DashboardData.WindStrength'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.GustAngle", "Value of property 'Station.Device.WindGauge.DashboardData.GustAngle'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.GustStrength", "Value of property 'Station.Device.WindGauge.DashboardData.GustStrength'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.MaxWindAngle", "Value of property 'Station.Device.WindGauge.DashboardData.MaxWindAngle'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.MaxWindStrength", "Value of property 'Station.Device.WindGauge.DashboardData.MaxWindStrength'", 0)]
        [InlineData("read Station.Device.WindGauge.DashboardData.DateMaxWindStrength", "Value of property 'Station.Device.WindGauge.DashboardData.DateMaxWindStrength'", 0)]
        [InlineData("read Station.Device.WindGauge.ID", "Value of property 'Station.Device.WindGauge.ID'", 0)]
        [InlineData("read Station.Device.WindGauge.Type", "Value of property 'Station.Device.WindGauge.Type'", 0)]
        [InlineData("read Station.Device.WindGauge.DataType", "Value of property 'Station.Device.WindGauge.DataType'", 0)]
        [InlineData("read Station.Device.WindGauge.DataType", "Value of property 'Station.Device.WindGauge.DataType'[0]", 0)]
        [InlineData("read Station.Device.WindGauge.ModuleName", "Value of property 'Station.Device.WindGauge.ModuleName'", 0)]
        [InlineData("read Station.Device.WindGauge.LastMessage", "Value of property 'Station.Device.WindGauge.LastMessage'", 0)]
        [InlineData("read Station.Device.WindGauge.LastSeen", "Value of property 'Station.Device.WindGauge.LastSeen'", 0)]
        [InlineData("read Station.Device.WindGauge.LastSetup", "Value of property 'Station.Device.WindGauge.LastSetup'", 0)]
        [InlineData("read Station.Device.WindGauge.BatteryVP", "Value of property 'Station.Device.WindGauge.BatteryVP'", 0)]
        [InlineData("read Station.Device.WindGauge.BatteryPercent", "Value of property 'Station.Device.WindGauge.BatteryPercent'", 0)]
        [InlineData("read Station.Device.WindGauge.RFStatus", "Value of property 'Station.Device.WindGauge.RFStatus'", 0)]
        [InlineData("read Station.Device.WindGauge.Firmware", "Value of property 'Station.Device.WindGauge.Firmware'", 0)]

        [InlineData("read Station.User", "Value of property 'Station.User'", 0)]
        [InlineData("read Station.User.Mail", "Value of property 'Station.User.Mail'", 0)]
        [InlineData("read Station.User.Administrative", "Value of property 'Station.User.Administrative'", 0)]
        [InlineData("read Station.User.Administrative.Country", "Value of property 'Station.User.Administrative.Country'", 0)]
        [InlineData("read Station.User.Administrative.FeelsLikeAlgorithm", "Value of property 'Station.User.Administrative.FeelsLikeAlgorithm'", 0)]
        [InlineData("read Station.User.Administrative.Language", "Value of property 'Station.User.Administrative.Language'", 0)]
        [InlineData("read Station.User.Administrative.PressureUnit", "Value of property 'Station.User.Administrative.PressureUnit'", 0)]
        [InlineData("read Station.User.Administrative.RegLocale", "Value of property 'Station.User.Administrative.RegLocale'", 0)]
        [InlineData("read Station.User.Administrative.Unit", "Value of property 'Station.User.Administrative.Unit'", 0)]
        [InlineData("read Station.User.Administrative.WindUnit", "Value of property 'Station.User.Administrative.WindUnit'", 0)]

        [InlineData("read Station.Response", "Value of property 'Station.Response'", 0)]
        [InlineData("read Station.Response.Status", "Value of property 'Station.Response.Status'", 0)]
        [InlineData("read Station.Response.TimeExec", "Value of property 'Station.Response.TimeExec'", 0)]
        [InlineData("read Station.Response.TimeServer", "Value of property 'Station.Response.TimeServer'", 0)]

        [InlineData("read Netatmo.BaseUri", "Value of property 'Netatmo.BaseUri'", 0)]
        [InlineData("read Netatmo.User", "Value of property 'Netatmo.User'", 0)]
        [InlineData("read Netatmo.Password", "Value of property 'Netatmo.Password'", 0)]
        [InlineData("read Netatmo.ClientID", "Value of property 'Netatmo.ClientID'", 0)]
        [InlineData("read Netatmo.ClientSecret", "Value of property 'Netatmo.ClientSecret'", 0)]
        [InlineData("read Netatmo.Scope", "Value of property 'Netatmo.Scope'", 0)]
        [InlineData("read Netatmo.Token", "Value of property 'Netatmo.Token'", 0)]
        [InlineData("read Netatmo.Expiration", "Value of property 'Netatmo.Expiration'", 0)]

        [InlineData("read Netatmo.Station", "Value of property 'Netatmo.Station'", 0)]
        [InlineData("read Netatmo.Station.Device", "Value of property 'Netatmo.Station.Device'", 0)]
        [InlineData("read Netatmo.Station.Device.ID", "Value of property 'Netatmo.Station.Device.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.CipherID", "Value of property 'Netatmo.Station.Device.CipherID'", 0)]
        [InlineData("read Netatmo.Station.Device.StationName", "Value of property 'Netatmo.Station.Device.StationName'", 0)]
        [InlineData("read Netatmo.Station.Device.ModuleName", "Value of property 'Netatmo.Station.Device.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.Firmware", "Value of property 'Netatmo.Station.Device.Firmware'", 0)]
        [InlineData("read Netatmo.Station.Device.WifiStatus", "Value of property 'Netatmo.Station.Device.WifiStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.CO2Calibrating", "Value of property 'Netatmo.Station.Device.CO2Calibrating'", 0)]
        [InlineData("read Netatmo.Station.Device.Type", "Value of property 'Netatmo.Station.Device.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.DataType", "Value of property 'Netatmo.Station.Device.DataType'[0]", 0)]

        [InlineData("read Netatmo.Station.Device.Place", "Value of property 'Netatmo.Station.Device.Place'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.Altitude", "Value of property 'Netatmo.Station.Device.Place.Altitude'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.City", "Value of property 'Netatmo.Station.Device.Place.City'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.Country", "Value of property 'Netatmo.Station.Device.Place.Country'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.GeoIpCity", "Value of property 'Netatmo.Station.Device.Place.GeoIpCity'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.ImproveLocProposed", "Value of property 'Netatmo.Station.Device.Place.ImproveLocProposed'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.Location", "Value of property 'Netatmo.Station.Device.Place.Location'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.Location.Latitude", "Value of property 'Netatmo.Station.Device.Place.Location.Latitude'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.Location.Longitude", "Value of property 'Netatmo.Station.Device.Place.Location.Longitude'", 0)]
        [InlineData("read Netatmo.Station.Device.Place.Timezone", "Value of property 'Netatmo.Station.Device.Place.Timezone'", 0)]

        [InlineData("read Netatmo.Station.Device.DashboardData", "Value of property 'Netatmo.Station.Device.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.Temperature", "Value of property 'Netatmo.Station.Device.DashboardData.Temperature'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.TempTrend", "Value of property 'Netatmo.Station.Device.DashboardData.TempTrend'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.Humidity", "Value of property 'Netatmo.Station.Device.DashboardData.Humidity'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.Noise", "Value of property 'Netatmo.Station.Device.DashboardData.Noise'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.CO2", "Value of property 'Netatmo.Station.Device.DashboardData.CO2'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.Pressure", "Value of property 'Netatmo.Station.Device.DashboardData.Pressure'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.PressureTrend", "Value of property 'Netatmo.Station.Device.DashboardData.PressureTrend'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.AbsolutePressure", "Value of property 'Netatmo.Station.Device.DashboardData.AbsolutePressure'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.DateMaxTemp", "Value of property 'Netatmo.Station.Device.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.DateMinTemp", "Value of property 'Netatmo.Station.Device.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.MinTemp", "Value of property 'Netatmo.Station.Device.DashboardData.MinTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.DashboardData.MaxTemp", "Value of property 'Netatmo.Station.Device.DashboardData.MaxTemp'", 0)]

        [InlineData("read Netatmo.Station.Device.LastStatusStore", "Value of property 'Netatmo.Station.Device.LastStatusStore'", 0)]
        [InlineData("read Netatmo.Station.Device.DateSetup", "Value of property 'Netatmo.Station.Device.DateSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.LastSetup", "Value of property 'Netatmo.Station.Device.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.LastUpgrade", "Value of property 'Netatmo.Station.Device.LastUpgrade'", 0)]

        [InlineData("read Netatmo.Station.Device.OutdoorModule", "Value of property 'Netatmo.Station.Device.OutdoorModule'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.Temperature", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.Temperature'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.TempTrend", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.TempTrend'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.Humidity", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.Humidity'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.DateMaxTemp", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.DateMinTemp", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.MaxTemp", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.MaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DashboardData.MinTemp", "Value of property 'Netatmo.Station.Device.OutdoorModule.DashboardData.MinTemp'", 0)]

        [InlineData("read Netatmo.Station.Device.OutdoorModule.ID", "Value of property 'Netatmo.Station.Device.OutdoorModule.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.Type", "Value of property 'Netatmo.Station.Device.OutdoorModule.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DataType", "Value of property 'Netatmo.Station.Device.OutdoorModule.DataType'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.DataType", "Value of property 'Netatmo.Station.Device.OutdoorModule.DataType'[0]", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.ModuleName", "Value of property 'Netatmo.Station.Device.OutdoorModule.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.LastMessage", "Value of property 'Netatmo.Station.Device.OutdoorModule.LastMessage'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.LastSeen", "Value of property 'Netatmo.Station.Device.OutdoorModule.LastSeen'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.LastSetup", "Value of property 'Netatmo.Station.Device.OutdoorModule.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.BatteryVP", "Value of property 'Netatmo.Station.Device.OutdoorModule.BatteryVP'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.BatteryPercent", "Value of property 'Netatmo.Station.Device.OutdoorModule.BatteryPercent'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.RFStatus", "Value of property 'Netatmo.Station.Device.OutdoorModule.RFStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.OutdoorModule.Firmware", "Value of property 'Netatmo.Station.Device.OutdoorModule.Firmware'", 0)]

        [InlineData("read Netatmo.Station.Device.IndoorModule1", "Value of property 'Netatmo.Station.Device.IndoorModule1'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.Temperature", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.Temperature'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.TempTrend", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.TempTrend'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.Humidity", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.Humidity'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.CO2", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.CO2'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.DateMaxTemp", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.DateMinTemp", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.MaxTemp", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.MaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DashboardData.MinTemp", "Value of property 'Netatmo.Station.Device.IndoorModule1.DashboardData.MinTemp'", 0)]

        [InlineData("read Netatmo.Station.Device.IndoorModule1.ID", "Value of property 'Netatmo.Station.Device.IndoorModule1.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.Type", "Value of property 'Netatmo.Station.Device.IndoorModule1.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DataType", "Value of property 'Netatmo.Station.Device.IndoorModule1.DataType'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.DataType", "Value of property 'Netatmo.Station.Device.IndoorModule1.DataType'[0]", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.ModuleName", "Value of property 'Netatmo.Station.Device.IndoorModule1.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.LastMessage", "Value of property 'Netatmo.Station.Device.IndoorModule1.LastMessage'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.LastSeen", "Value of property 'Netatmo.Station.Device.IndoorModule1.LastSeen'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.LastSetup", "Value of property 'Netatmo.Station.Device.IndoorModule1.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.BatteryVP", "Value of property 'Netatmo.Station.Device.IndoorModule1.BatteryVP'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.BatteryPercent", "Value of property 'Netatmo.Station.Device.IndoorModule1.BatteryPercent'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.RFStatus", "Value of property 'Netatmo.Station.Device.IndoorModule1.RFStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule1.Firmware", "Value of property 'Netatmo.Station.Device.IndoorModule1.Firmware'", 0)]

        [InlineData("read Netatmo.Station.Device.IndoorModule2", "Value of property 'Netatmo.Station.Device.IndoorModule2'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.Temperature", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.Temperature'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.TempTrend", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.TempTrend'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.Humidity", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.Humidity'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.CO2", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.CO2'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.DateMaxTemp", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.DateMinTemp", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.MaxTemp", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.MaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DashboardData.MinTemp", "Value of property 'Netatmo.Station.Device.IndoorModule2.DashboardData.MinTemp'", 0)]

        [InlineData("read Netatmo.Station.Device.IndoorModule2.ID", "Value of property 'Netatmo.Station.Device.IndoorModule2.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.Type", "Value of property 'Netatmo.Station.Device.IndoorModule2.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DataType", "Value of property 'Netatmo.Station.Device.IndoorModule2.DataType'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.DataType", "Value of property 'Netatmo.Station.Device.IndoorModule2.DataType'[0]", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.ModuleName", "Value of property 'Netatmo.Station.Device.IndoorModule2.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.LastMessage", "Value of property 'Netatmo.Station.Device.IndoorModule2.LastMessage'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.LastSeen", "Value of property 'Netatmo.Station.Device.IndoorModule2.LastSeen'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.LastSetup", "Value of property 'Netatmo.Station.Device.IndoorModule2.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.BatteryVP", "Value of property 'Netatmo.Station.Device.IndoorModule2.BatteryVP'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.BatteryPercent", "Value of property 'Netatmo.Station.Device.IndoorModule2.BatteryPercent'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.RFStatus", "Value of property 'Netatmo.Station.Device.IndoorModule2.RFStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule2.Firmware", "Value of property 'Netatmo.Station.Device.IndoorModule2.Firmware'", 0)]

        [InlineData("read Netatmo.Station.Device.IndoorModule3", "Value of property 'Netatmo.Station.Device.IndoorModule3'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.Temperature", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.Temperature'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.TempTrend", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.TempTrend'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.Humidity", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.Humidity'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.CO2", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.CO2'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.DateMaxTemp", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.DateMaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.DateMinTemp", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.DateMinTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.MaxTemp", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.MaxTemp'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DashboardData.MinTemp", "Value of property 'Netatmo.Station.Device.IndoorModule3.DashboardData.MinTemp'", 0)]

        [InlineData("read Netatmo.Station.Device.IndoorModule3.ID", "Value of property 'Netatmo.Station.Device.IndoorModule3.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.Type", "Value of property 'Netatmo.Station.Device.IndoorModule3.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DataType", "Value of property 'Netatmo.Station.Device.IndoorModule3.DataType'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.DataType", "Value of property 'Netatmo.Station.Device.IndoorModule3.DataType'[0]", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.ModuleName", "Value of property 'Netatmo.Station.Device.IndoorModule3.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.LastMessage", "Value of property 'Netatmo.Station.Device.IndoorModule3.LastMessage'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.LastSeen", "Value of property 'Netatmo.Station.Device.IndoorModule3.LastSeen'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.LastSetup", "Value of property 'Netatmo.Station.Device.IndoorModule3.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.BatteryVP", "Value of property 'Netatmo.Station.Device.IndoorModule3.BatteryVP'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.BatteryPercent", "Value of property 'Netatmo.Station.Device.IndoorModule3.BatteryPercent'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.RFStatus", "Value of property 'Netatmo.Station.Device.IndoorModule3.RFStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.IndoorModule3.Firmware", "Value of property 'Netatmo.Station.Device.IndoorModule3.Firmware'", 0)]

        [InlineData("read Netatmo.Station.Device.RainGauge", "Value of property 'Netatmo.Station.Device.RainGauge'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DashboardData", "Value of property 'Netatmo.Station.Device.RainGauge.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.RainGauge.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DashboardData.Rain", "Value of property 'Netatmo.Station.Device.RainGauge.DashboardData.Rain'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DashboardData.SumRain1", "Value of property 'Netatmo.Station.Device.RainGauge.DashboardData.SumRain1'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DashboardData.SumRain24", "Value of property 'Netatmo.Station.Device.RainGauge.DashboardData.SumRain24'", 0)]

        [InlineData("read Netatmo.Station.Device.RainGauge.ID", "Value of property 'Netatmo.Station.Device.RainGauge.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.Type", "Value of property 'Netatmo.Station.Device.RainGauge.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DataType", "Value of property 'Netatmo.Station.Device.RainGauge.DataType'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.DataType", "Value of property 'Netatmo.Station.Device.RainGauge.DataType'[0]", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.ModuleName", "Value of property 'Netatmo.Station.Device.RainGauge.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.LastMessage", "Value of property 'Netatmo.Station.Device.RainGauge.LastMessage'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.LastSeen", "Value of property 'Netatmo.Station.Device.RainGauge.LastSeen'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.LastSetup", "Value of property 'Netatmo.Station.Device.RainGauge.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.BatteryVP", "Value of property 'Netatmo.Station.Device.RainGauge.BatteryVP'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.BatteryPercent", "Value of property 'Netatmo.Station.Device.RainGauge.BatteryPercent'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.RFStatus", "Value of property 'Netatmo.Station.Device.RainGauge.RFStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.RainGauge.Firmware", "Value of property 'Netatmo.Station.Device.RainGauge.Firmware'", 0)]

        [InlineData("read Netatmo.Station.Device.WindGauge", "Value of property 'Netatmo.Station.Device.WindGauge'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.TimeUTC", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.TimeUTC'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.WindAngle", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.WindAngle'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.WindStrength", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.WindStrength'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.GustAngle", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.GustAngle'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.GustStrength", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.GustStrength'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.MaxWindAngle", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.MaxWindAngle'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.MaxWindStrength", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.MaxWindStrength'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DashboardData.DateMaxWindStrength", "Value of property 'Netatmo.Station.Device.WindGauge.DashboardData.DateMaxWindStrength'", 0)]

        [InlineData("read Netatmo.Station.Device.WindGauge.ID", "Value of property 'Netatmo.Station.Device.WindGauge.ID'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.Type", "Value of property 'Netatmo.Station.Device.WindGauge.Type'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DataType", "Value of property 'Netatmo.Station.Device.WindGauge.DataType'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.DataType", "Value of property 'Netatmo.Station.Device.WindGauge.DataType'[0]", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.ModuleName", "Value of property 'Netatmo.Station.Device.WindGauge.ModuleName'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.LastMessage", "Value of property 'Netatmo.Station.Device.WindGauge.LastMessage'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.LastSeen", "Value of property 'Netatmo.Station.Device.WindGauge.LastSeen'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.LastSetup", "Value of property 'Netatmo.Station.Device.WindGauge.LastSetup'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.BatteryVP", "Value of property 'Netatmo.Station.Device.WindGauge.BatteryVP'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.BatteryPercent", "Value of property 'Netatmo.Station.Device.WindGauge.BatteryPercent'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.RFStatus", "Value of property 'Netatmo.Station.Device.WindGauge.RFStatus'", 0)]
        [InlineData("read Netatmo.Station.Device.WindGauge.Firmware", "Value of property 'Netatmo.Station.Device.WindGauge.Firmware'", 0)]

        [InlineData("read Netatmo.Station.User", "Value of property 'Netatmo.Station.User'", 0)]
        [InlineData("read Netatmo.Station.User.Mail", "Value of property 'Netatmo.Station.User.Mail'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative", "Value of property 'Netatmo.Station.User.Administrative'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.Country", "Value of property 'Netatmo.Station.User.Administrative.Country'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.FeelsLikeAlgorithm", "Value of property 'Netatmo.Station.User.Administrative.FeelsLikeAlgorithm'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.Language", "Value of property 'Netatmo.Station.User.Administrative.Language'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.PressureUnit", "Value of property 'Netatmo.Station.User.Administrative.PressureUnit'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.RegLocale", "Value of property 'Netatmo.Station.User.Administrative.RegLocale'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.Unit", "Value of property 'Netatmo.Station.User.Administrative.Unit'", 0)]
        [InlineData("read Netatmo.Station.User.Administrative.WindUnit", "Value of property 'Netatmo.Station.User.Administrative.WindUnit'", 0)]

        [InlineData("read Netatmo.Station.Response", "Value of property 'Netatmo.Station.Response'", 0)]
        [InlineData("read Netatmo.Station.Response.Status", "Value of property 'Netatmo.Station.Response.Status'", 0)]
        [InlineData("read Netatmo.Station.Response.TimeExec", "Value of property 'Netatmo.Station.Response.TimeExec'", 0)]
        [InlineData("read Netatmo.Station.Response.TimeServer", "Value of property 'Netatmo.Station.Response.TimeServer'", 0)]
        [InlineData("read -?", "Usage: NetatmoApp read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: NetatmoApp read [arguments] [options]", 0)]
        [InlineData("read City", "The property 'Yspertal' has not been found.", 0)]
        public void TestReadCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("monitor", "Error: Select an data option.", 0)]
        [InlineData("monitor -?", "NetatmoApp monitor [options]", 0)]
        [InlineData("monitor --help", "NetatmoApp monitor [options]", 0)]
        [InlineData("monitor -a", "Netatmo Data", 0)]
        [InlineData("monitor --all", "Netatmo Data", 0)]
        [InlineData("monitor -m", "Main Module", 0)]
        [InlineData("monitor --main", "Main Module", 0)]
        [InlineData("monitor -o", "Outdoor Module", 0)]
        [InlineData("monitor --outdoor", "Outdoor Module", 0)]
        [InlineData("monitor -1", "Indoor Module 1", 0)]
        [InlineData("monitor --indoor1", "Indoor Module 1", 0)]
        [InlineData("monitor -2", "Indoor Module 2", 0)]
        [InlineData("monitor --indoor2", "Indoor Module 2", 0)]
        [InlineData("monitor -3", "Indoor Module 3", 0)]
        [InlineData("monitor --indoor3", "Indoor Module 3", 0)]
        [InlineData("monitor -r", "Rain Gauge", 0)]
        [InlineData("monitor --rain", "Rain Gauge", 0)]
        [InlineData("monitor -w", "Wind Gauge", 0)]
        [InlineData("monitor --wind", "Wind Gauge", 0)]
        public void TestMonitorCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Starts the console application. Specify empty string to run with no arguments.
        /// </summary>
        /// <param name="arguments">The arguments for console application.</param>
        /// <returns>The exit code.</returns>
        private int StartConsoleApplication(string arguments)
        {
            // Initialize process here
            Process proc = new Process();
            proc.StartInfo.FileName = @"dotnet";

            // add arguments as whole string
            proc.StartInfo.Arguments = "run -- " + arguments;

            // use it to start from testing environment
            proc.StartInfo.UseShellExecute = false;

            // redirect outputs to have it in testing console
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            // set working directory
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\Netatmo\NetatmoApp";

            // start and wait for exit
            proc.Start();
            proc.WaitForExit(10000);

            // get output to testing console.
            System.Console.WriteLine(proc.StandardOutput.ReadToEnd());
            System.Console.Write(proc.StandardError.ReadToEnd());

            // return exit code
            return proc.ExitCode;
        }

        #endregion
    }
}
