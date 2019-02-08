// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusTest
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

    using FroniusLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Fronius Test Collection")]
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
            _logger = loggerFactory.CreateLogger<Fronius>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Fronius web service with version", 0)]
        [InlineData("-?", "Usage: FroniusApp [options] [command]", 0)]
        [InlineData("--help", "Usage: FroniusApp [options] [command]", 0)]
        [InlineData("--address http://10.0.1.6", "Fronius web service with version", 0)]
        [InlineData("--timeout 10", "Fronius web service with version", 0)]
        [InlineData("--device 1", "Fronius web service with version", 0)]
        public void TestRootCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("info", "Select an info option", 0)]
        [InlineData("info -?", "Usage: FroniusApp info [options]", 0)]
        [InlineData("info --help", "Usage: FroniusApp info [options]", 0)]
        [InlineData("info -c", "CommonData:", 0)]
        [InlineData("info --common", "CommonData:", 0)]
        [InlineData("info -i", "InverterInfo:", 0)]
        [InlineData("info --inverter", "InverterInfo:", 0)]
        [InlineData("info -l", "LoggerInfo:", 0)]
        [InlineData("info --logger", "LoggerInfo:", 0)]
        [InlineData("info -m", "MinMaxData:", 0)]
        [InlineData("info --minmax", "MinMaxData:", 0)]
        [InlineData("info -p", "PhaseData:", 0)]
        [InlineData("info --phase", "PhaseData:", 0)]
        public void TestInfoCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("read", "Select a data option or specify a property", 0)]
        [InlineData("read -?", "Usage: FroniusApp read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: FroniusApp read [arguments] [options]", 0)]
        [InlineData("read -a", "FroniusData:", 0)]
        [InlineData("read --all", "FroniusData:", 0)]
        [InlineData("read -c", "CommonData:", 0)]
        [InlineData("read --common", "CommonData:", 0)]
        [InlineData("read -i", "InverterInfo:", 0)]
        [InlineData("read --inverter", "InverterInfo:", 0)]
        [InlineData("read -l", "LoggerInfo:", 0)]
        [InlineData("read --logger", "LoggerInfo:", 0)]
        [InlineData("read -m", "MinMaxData:", 0)]
        [InlineData("read --minmax", "MinMaxData:", 0)]
        [InlineData("read -p", "PhaseData:", 0)]
        [InlineData("read --phase", "PhaseData:", 0)]

        [InlineData("read Fronius", "Value of property 'Fronius' =", 0)]

        [InlineData("read Fronius.CommonData", "Value of property 'Fronius.CommonData' =", 0)]
        [InlineData("read Fronius.CommonData.Frequency", "Value of property 'Fronius.CommonData.Frequency' =", 0)]
        [InlineData("read Fronius.CommonData.CurrentDC", "Value of property 'Fronius.CommonData.CurrentDC' =", 0)]
        [InlineData("read Fronius.CommonData.CurrentAC", "Value of property 'Fronius.CommonData.CurrentAC' =", 0)]
        [InlineData("read Fronius.CommonData.VoltageDC", "Value of property 'Fronius.CommonData.VoltageDC' =", 0)]
        [InlineData("read Fronius.CommonData.VoltageAC", "Value of property 'Fronius.CommonData.VoltageAC' =", 0)]
        [InlineData("read Fronius.CommonData.PowerAC", "Value of property 'Fronius.CommonData.PowerAC' =", 0)]
        [InlineData("read Fronius.CommonData.DailyEnergy", "Value of property 'Fronius.CommonData.DailyEnergy' =", 0)]
        [InlineData("read Fronius.CommonData.YearlyEnergy", "Value of property 'Fronius.CommonData.YearlyEnergy' =", 0)]
        [InlineData("read Fronius.CommonData.TotalEnergy", "Value of property 'Fronius.CommonData.TotalEnergy' =", 0)]
        [InlineData("read Fronius.CommonData.StatusCode", "Value of property 'Fronius.CommonData.StatusCode' =", 0)]

        [InlineData("read Fronius.InverterInfo", "Value of property 'Fronius.InverterInfo' =", 0)]
        [InlineData("read Fronius.InverterInfo.Index", "Value of property 'Fronius.InverterInfo.Index' =", 0)]
        [InlineData("read Fronius.InverterInfo.DeviceType", "Value of property 'Fronius.InverterInfo.DeviceType' =", 0)]
        [InlineData("read Fronius.InverterInfo.PVPower", "Value of property 'Fronius.InverterInfo.PVPower' =", 0)]
        [InlineData("read Fronius.InverterInfo.CustomName", "Value of property 'Fronius.InverterInfo.CustomName' =", 0)]
        [InlineData("read Fronius.InverterInfo.Show", "Value of property 'Fronius.InverterInfo.Show' =", 0)]
        [InlineData("read Fronius.InverterInfo.UniqueID", "Value of property 'Fronius.InverterInfo.UniqueID' =", 0)]
        [InlineData("read Fronius.InverterInfo.ErrorCode", "Value of property 'Fronius.InverterInfo.ErrorCode' =", 0)]
        [InlineData("read Fronius.InverterInfo.StatusCode", "Value of property 'Fronius.InverterInfo.StatusCode' =", 0)]

        [InlineData("read Fronius.LoggerInfo", "Value of property 'Fronius.LoggerInfo' =", 0)]
        [InlineData("read Fronius.LoggerInfo.UniqueID", "Value of property 'Fronius.LoggerInfo.UniqueID' =", 0)]
        [InlineData("read Fronius.LoggerInfo.ProductID", "Value of property 'Fronius.LoggerInfo.ProductID' =", 0)]
        [InlineData("read Fronius.LoggerInfo.PlatformID", "Value of property 'Fronius.LoggerInfo.PlatformID' =", 0)]
        [InlineData("read Fronius.LoggerInfo.HWVersion", "Value of property 'Fronius.LoggerInfo.HWVersion' =", 0)]
        [InlineData("read Fronius.LoggerInfo.SWVersion", "Value of property 'Fronius.LoggerInfo.SWVersion' =", 0)]
        [InlineData("read Fronius.LoggerInfo.TimezoneLocation", "Value of property 'Fronius.LoggerInfo.TimezoneLocation' =", 0)]
        [InlineData("read Fronius.LoggerInfo.TimezoneName", "Value of property 'Fronius.LoggerInfo.TimezoneName' =", 0)]
        [InlineData("read Fronius.LoggerInfo.UTCOffset", "Value of property 'Fronius.LoggerInfo.UTCOffset' =", 0)]
        [InlineData("read Fronius.LoggerInfo.DefaultLanguage", "Value of property 'Fronius.LoggerInfo.DefaultLanguage' =", 0)]
        [InlineData("read Fronius.LoggerInfo.CashFactor", "Value of property 'Fronius.LoggerInfo.CashFactor' =", 0)]
        [InlineData("read Fronius.LoggerInfo.CashCurrency", "Value of property 'Fronius.LoggerInfo.CashCurrency' =", 0)]
        [InlineData("read Fronius.LoggerInfo.CO2Factor", "Value of property 'Fronius.LoggerInfo.CO2Factor' =", 0)]
        [InlineData("read Fronius.LoggerInfo.CO2Unit", "Value of property 'Fronius.LoggerInfo.CO2Unit' =", 0)]

        [InlineData("read Fronius.MinMaxData", "Value of property 'Fronius.MinMaxData' =", 0)]
        [InlineData("read Fronius.MinMaxData.DailyMaxVoltageDC", "Value of property 'Fronius.MinMaxData.DailyMaxVoltageDC' =", 0)]
        [InlineData("read Fronius.MinMaxData.DailyMaxVoltageAC", "Value of property 'Fronius.MinMaxData.DailyMaxVoltageAC' =", 0)]
        [InlineData("read Fronius.MinMaxData.DailyMinVoltageAC", "Value of property 'Fronius.MinMaxData.DailyMinVoltageAC' =", 0)]
        [InlineData("read Fronius.MinMaxData.YearlyMaxVoltageDC", "Value of property 'Fronius.MinMaxData.YearlyMaxVoltageDC' =", 0)]
        [InlineData("read Fronius.MinMaxData.YearlyMaxVoltageAC", "Value of property 'Fronius.MinMaxData.YearlyMaxVoltageAC' =", 0)]
        [InlineData("read Fronius.MinMaxData.YearlyMinVoltageAC", "Value of property 'Fronius.MinMaxData.YearlyMinVoltageAC' =", 0)]
        [InlineData("read Fronius.MinMaxData.TotalMaxVoltageDC", "Value of property 'Fronius.MinMaxData.TotalMaxVoltageDC' =", 0)]
        [InlineData("read Fronius.MinMaxData.TotalMaxVoltageAC", "Value of property 'Fronius.MinMaxData.TotalMaxVoltageAC' =", 0)]
        [InlineData("read Fronius.MinMaxData.TotalMinVoltageAC", "Value of property 'Fronius.MinMaxData.TotalMinVoltageAC' =", 0)]
        [InlineData("read Fronius.MinMaxData.DailyMaxPower", "Value of property 'Fronius.MinMaxData.DailyMaxPower' =", 0)]
        [InlineData("read Fronius.MinMaxData.YearlyMaxPower", "Value of property 'Fronius.MinMaxData.YearlyMaxPower' =", 0)]
        [InlineData("read Fronius.MinMaxData.TotalMaxPower", "Value of property 'Fronius.MinMaxData.TotalMaxPower' =", 0)]

        [InlineData("read Fronius.PhaseData", "Value of property 'Fronius.PhaseData' =", 0)]
        [InlineData("read Fronius.PhaseData.CurrentL1", "Value of property 'Fronius.PhaseData.CurrentL1' =", 0)]
        [InlineData("read Fronius.PhaseData.CurrentL2", "Value of property 'Fronius.PhaseData.CurrentL2' =", 0)]
        [InlineData("read Fronius.PhaseData.CurrentL3", "Value of property 'Fronius.PhaseData.CurrentL3' =", 0)]
        [InlineData("read Fronius.PhaseData.VoltageL1N", "Value of property 'Fronius.PhaseData.VoltageL1N' =", 0)]
        [InlineData("read Fronius.PhaseData.VoltageL2N", "Value of property 'Fronius.PhaseData.VoltageL2N' =", 0)]
        [InlineData("read Fronius.PhaseData.VoltageL3N", "Value of property 'Fronius.PhaseData.VoltageL3N' =", 0)]

        [InlineData("read Common", "Value of property 'Common' =", 0)]
        [InlineData("read Common.Frequency", "Value of property 'Common.Frequency' =", 0)]
        [InlineData("read Common.CurrentDC", "Value of property 'Common.CurrentDC' =", 0)]
        [InlineData("read Common.CurrentAC", "Value of property 'Common.CurrentAC' =", 0)]
        [InlineData("read Common.VoltageDC", "Value of property 'Common.VoltageDC' =", 0)]
        [InlineData("read Common.VoltageAC", "Value of property 'Common.VoltageAC' =", 0)]
        [InlineData("read Common.PowerAC", "Value of property 'Common.PowerAC' =", 0)]
        [InlineData("read Common.DailyEnergy", "Value of property 'Common.DailyEnergy' =", 0)]
        [InlineData("read Common.YearlyEnergy", "Value of property 'Common.YearlyEnergy' =", 0)]
        [InlineData("read Common.TotalEnergy", "Value of property 'Common.TotalEnergy' =", 0)]
        [InlineData("read Common.StatusCode", "Value of property 'Common.StatusCode' =", 0)]

        [InlineData("read Inverter", "Value of property 'Inverter' =", 0)]
        [InlineData("read Inverter.Index", "Value of property 'Inverter.Index' =", 0)]
        [InlineData("read Inverter.DeviceType", "Value of property 'Inverter.DeviceType' =", 0)]
        [InlineData("read Inverter.PVPower", "Value of property 'Inverter.PVPower' =", 0)]
        [InlineData("read Inverter.CustomName", "Value of property 'Inverter.CustomName' =", 0)]
        [InlineData("read Inverter.Show", "Value of property 'Inverter.Show' =", 0)]
        [InlineData("read Inverter.UniqueID", "Value of property 'Inverter.UniqueID' =", 0)]
        [InlineData("read Inverter.ErrorCode", "Value of property 'Inverter.ErrorCode' =", 0)]
        [InlineData("read Inverter.StatusCode", "Value of property 'Inverter.StatusCode' =", 0)]

        [InlineData("read Logger", "Value of property 'Logger' =", 0)]
        [InlineData("read Logger.UniqueID", "Value of property 'Logger.UniqueID' =", 0)]
        [InlineData("read Logger.ProductID", "Value of property 'Logger.ProductID' =", 0)]
        [InlineData("read Logger.PlatformID", "Value of property 'Logger.PlatformID' =", 0)]
        [InlineData("read Logger.HWVersion", "Value of property 'Logger.HWVersion' =", 0)]
        [InlineData("read Logger.SWVersion", "Value of property 'Logger.SWVersion' =", 0)]
        [InlineData("read Logger.TimezoneLocation", "Value of property 'Logger.TimezoneLocation' =", 0)]
        [InlineData("read Logger.TimezoneName", "Value of property 'Logger.TimezoneName' =", 0)]
        [InlineData("read Logger.UTCOffset", "Value of property 'Logger.UTCOffset' =", 0)]
        [InlineData("read Logger.DefaultLanguage", "Value of property 'Logger.DefaultLanguage' =", 0)]
        [InlineData("read Logger.CashFactor", "Value of property 'Logger.CashFactor' =", 0)]
        [InlineData("read Logger.CashCurrency", "Value of property 'Logger.CashCurrency' =", 0)]
        [InlineData("read Logger.CO2Factor", "Value of property 'Logger.CO2Factor' =", 0)]
        [InlineData("read Logger.CO2Unit", "Value of property 'Logger.CO2Unit' =", 0)]

        [InlineData("read MinMax", "Value of property 'MinMax' =", 0)]
        [InlineData("read MinMax.DailyMaxVoltageDC", "Value of property 'MinMax.DailyMaxVoltageDC' =", 0)]
        [InlineData("read MinMax.DailyMaxVoltageAC", "Value of property 'MinMax.DailyMaxVoltageAC' =", 0)]
        [InlineData("read MinMax.DailyMinVoltageAC", "Value of property 'MinMax.DailyMinVoltageAC' =", 0)]
        [InlineData("read MinMax.YearlyMaxVoltageDC", "Value of property 'MinMax.YearlyMaxVoltageDC' =", 0)]
        [InlineData("read MinMax.YearlyMaxVoltageAC", "Value of property 'MinMax.YearlyMaxVoltageAC' =", 0)]
        [InlineData("read MinMax.YearlyMinVoltageAC", "Value of property 'MinMax.YearlyMinVoltageAC' =", 0)]
        [InlineData("read MinMax.TotalMaxVoltageDC", "Value of property 'MinMax.TotalMaxVoltageDC' =", 0)]
        [InlineData("read MinMax.TotalMaxVoltageAC", "Value of property 'MinMax.TotalMaxVoltageAC' =", 0)]
        [InlineData("read MinMax.TotalMinVoltageAC", "Value of property 'MinMax.TotalMinVoltageAC' =", 0)]
        [InlineData("read MinMax.DailyMaxPower", "Value of property 'MinMax.DailyMaxPower' =", 0)]
        [InlineData("read MinMax.YearlyMaxPower", "Value of property 'MinMax.YearlyMaxPower' =", 0)]
        [InlineData("read MinMax.TotalMaxPower", "Value of property 'MinMax.TotalMaxPower' =", 0)]

        [InlineData("read Phase", "Value of property 'Phase' =", 0)]
        [InlineData("read Phase.CurrentL1", "Value of property 'Phase.CurrentL1' =", 0)]
        [InlineData("read Phase.CurrentL2", "Value of property 'Phase.CurrentL2' =", 0)]
        [InlineData("read Phase.CurrentL3", "Value of property 'Phase.CurrentL3' =", 0)]
        [InlineData("read Phase.VoltageL1N", "Value of property 'Phase.VoltageL1N' =", 0)]
        [InlineData("read Phase.VoltageL2N", "Value of property 'Phase.VoltageL2N' =", 0)]
        [InlineData("read Phase.VoltageL3N", "Value of property 'Phase.VoltageL3N' =", 0)]

        [InlineData("read", "Select a data option or specify a property.", 0)]
        [InlineData("read -", "The property '-' has not been found.", 0)]
        public void TestReadCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("monitor", "Select a data option.", 0)]
        [InlineData("monitor -?", "FroniusApp monitor [options]", 0)]
        [InlineData("monitor --help", "FroniusApp monitor [options]", 0)]
        [InlineData("monitor -a", "Fronius Data", 0)]
        [InlineData("monitor --all", "Fronius Data", 0)]
        [InlineData("monitor -c", "Common Data", 0)]
        [InlineData("monitor --common", "Common Data", 0)]
        [InlineData("monitor -i", "Inverter Info", 0)]
        [InlineData("monitor --inverter", "Inverter Info", 0)]
        [InlineData("monitor -l", "Logger Info", 0)]
        [InlineData("monitor --logger", "Logger Info", 0)]
        [InlineData("monitor -m", "MinMax Data", 0)]
        [InlineData("monitor --minmax", "MinMax Data", 0)]
        [InlineData("monitor -p", "Phase Data", 0)]
        [InlineData("monitor --phase", "Phase Data", 0)]
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
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\Fronius\FroniusApp";

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
