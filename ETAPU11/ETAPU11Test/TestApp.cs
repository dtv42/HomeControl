// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Test
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

    using ETAPU11Lib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("ETAPU11 Test Collection")]
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
            _logger = loggerFactory.CreateLogger<ETAPU11>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Modbus TCP client found", 0)]
        [InlineData("-?", "Usage: ETAPU11App [options] [command]", 0)]
        [InlineData("--help", "Usage: ETAPU11App [options] [command]", 0)]
        [InlineData("--address 10.0.1.4", "Modbus TCP client found", 0)]
        [InlineData("--port 502", "Modbus TCP client found", 0)]
        [InlineData("--slaveid 1", "Modbus TCP client found", 0)]
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
        [InlineData("info", "Select an info option", 0)]
        [InlineData("info -?", "Usage: ETAPU11App info [options]", 0)]
        [InlineData("info --help", "Usage: ETAPU11App info [options]", 0)]
        [InlineData("info -a", "Data:", 0)]
        [InlineData("info --all", "Data:", 0)]
        [InlineData("info -b", "Boiler:", 0)]
        [InlineData("info --boiler", "Boiler:", 0)]
        [InlineData("info -w", "Hotwater:", 0)]
        [InlineData("info --hotwater", "Hotwater:", 0)]
        [InlineData("info -h", "Heating:", 0)]
        [InlineData("info --heating", "Heating:", 0)]
        [InlineData("info -s", "Storage:", 0)]
        [InlineData("info --storage", "Storage:", 0)]
        [InlineData("info -y", "System:", 0)]
        [InlineData("info --system", "System:", 0)]
        public void TestInfoCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("read", "ETAPU11:", 0)]
        [InlineData("read -b", "ETAPU11:", 0)]
        [InlineData("read -?", "Usage: ETAPU11App read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: ETAPU11App read [arguments] [options]", 0)]

        [InlineData("read FullLoadHours", "Value of property 'FullLoadHours'", 0)]
        [InlineData("read TotalConsumed", "Value of property 'TotalConsumed'", 0)]
        [InlineData("read BoilerState", "Value of property 'BoilerState'", 0)]
        [InlineData("read BoilerPressure", "Value of property 'BoilerPressure'", 0)]
        [InlineData("read BoilerTemperature", "Value of property 'BoilerTemperature'", 0)]
        [InlineData("read BoilerTarget", "Value of property 'BoilerTarget'", 0)]
        [InlineData("read BoilerBottom", "Value of property 'BoilerBottom'", 0)]
        [InlineData("read FlowControlState", "Value of property 'FlowControlState'", 0)]
        [InlineData("read DiverterValveState", "Value of property 'DiverterValveState'", 0)]
        [InlineData("read DiverterValveDemand", "Value of property 'DiverterValveDemand'", 0)]
        [InlineData("read DemandedOutput", "Value of property 'DemandedOutput'", 0)]
        [InlineData("read FlowMixValveTarget", "Value of property 'FlowMixValveTarget'", 0)]
        [InlineData("read FlowMixValveState", "Value of property 'FlowMixValveState'", 0)]
        [InlineData("read FlowMixValveCurrTemp", "Value of property 'FlowMixValveCurrTemp'", 0)]
        [InlineData("read FlowMixValvePosition", "Value of property 'FlowMixValvePosition'", 0)]
        [InlineData("read BoilerPumpOutput", "Value of property 'BoilerPumpOutput'", 0)]
        [InlineData("read BoilerPumpDemand", "Value of property 'BoilerPumpDemand'", 0)]
        [InlineData("read FlueGasTemperature", "Value of property 'FlueGasTemperature'", 0)]
        [InlineData("read DraughtFanSpeed", "Value of property 'DraughtFanSpeed'", 0)]
        [InlineData("read ResidualO2", "Value of property 'ResidualO2'", 0)]
        [InlineData("read StokerScrewDemand", "Value of property 'StokerScrewDemand'", 0)]
        [InlineData("read StokerScrewClockRate", "Value of property 'StokerScrewClockRate'", 0)]
        [InlineData("read StokerScrewState", "Value of property 'StokerScrewState'", 0)]
        [InlineData("read StokerScrewMotorCurr", "Value of property 'StokerScrewMotorCurr'", 0)]
        [InlineData("read AshRemovalState", "Value of property 'AshRemovalState'", 0)]
        [InlineData("read AshRemovalStartIdleTime", "Value of property 'AshRemovalStartIdleTime'", 0)]
        [InlineData("read AshRemovalDurationIdleTime", "Value of property 'AshRemovalDurationIdleTime'", 0)]
        [InlineData("read ConsumptionSinceDeAsh", "Value of property 'ConsumptionSinceDeAsh'", 0)]
        [InlineData("read ConsumptionSinceAshBoxEmptied", "Value of property 'ConsumptionSinceAshBoxEmptied'", 0)]
        [InlineData("read EmptyAshBoxAfter", "Value of property 'EmptyAshBoxAfter'", 0)]
        [InlineData("read ConsumptionSinceMaintainence", "Value of property 'ConsumptionSinceMaintainence'", 0)]
        [InlineData("read HopperState", "Value of property 'HopperState'", 0)]
        [InlineData("read HopperFillUpPelletBin", "Value of property 'HopperFillUpPelletBin'", 0)]
        [InlineData("read HopperPelletBinContents", "Value of property 'HopperPelletBinContents'", 0)]
        [InlineData("read HopperFillUpTime", "Value of property 'HopperFillUpTime'", 0)]
        [InlineData("read HopperVacuumState", "Value of property 'HopperVacuumState'", 0)]
        [InlineData("read HopperVacuumDemand", "Value of property 'HopperVacuumDemand'", 0)]
        [InlineData("read OnOffButton", "Value of property 'OnOffButton'", 0)]
        [InlineData("read DeAshButton", "Value of property 'DeAshButton'", 0)]
        [InlineData("read HotwaterTankState", "Value of property 'HotwaterTankState'", 0)]
        [InlineData("read ChargingTimesState", "Value of property 'ChargingTimesState'", 0)]
        [InlineData("read ChargingTimesSwitchStatus", "Value of property 'ChargingTimesSwitchStatus'", 0)]
        [InlineData("read ChargingTimesTemperature", "Value of property 'ChargingTimesTemperature'", 0)]
        [InlineData("read HotwaterSwitchonDiff", "Value of property 'HotwaterSwitchonDiff'", 0)]
        [InlineData("read HotwaterTarget", "Value of property 'HotwaterTarget'", 0)]
        [InlineData("read HotwaterTemperature", "Value of property 'HotwaterTemperature'", 0)]
        [InlineData("read ChargeButton", "Value of property 'ChargeButton'", 0)]
        [InlineData("read RoomSensor", "Value of property 'RoomSensor'", 0)]
        [InlineData("read HeatingCircuitState", "Value of property 'HeatingCircuitState'", 0)]
        [InlineData("read RunningState", "Value of property 'RunningState'", 0)]
        [InlineData("read HeatingTimes", "Value of property 'HeatingTimes'", 0)]
        [InlineData("read HeatingSwitchStatus", "Value of property 'HeatingSwitchStatus'", 0)]
        [InlineData("read HeatingTemperature", "Value of property 'HeatingTemperature'", 0)]
        [InlineData("read RoomTemperature", "Value of property 'RoomTemperature'", 0)]
        [InlineData("read RoomTarget", "Value of property 'RoomTarget'", 0)]
        [InlineData("read Flow", "Value of property 'Flow'", 0)]
        [InlineData("read HeatingCurve", "Value of property 'HeatingCurve'", 0)]
        [InlineData("read FlowAtMinus10", "Value of property 'FlowAtMinus10'", 0)]
        [InlineData("read FlowAtPlus10", "Value of property 'FlowAtPlus10'", 0)]
        [InlineData("read FlowSetBack", "Value of property 'FlowSetBack'", 0)]
        [InlineData("read OutsideTemperatureDelayed", "Value of property 'OutsideTemperatureDelayed'", 0)]
        [InlineData("read DayHeatingThreshold", "Value of property 'DayHeatingThreshold'", 0)]
        [InlineData("read NightHeatingThreshold", "Value of property 'NightHeatingThreshold'", 0)]
        [InlineData("read HeatingDayButton", "Value of property 'HeatingDayButton'", 0)]
        [InlineData("read HeatingAutoButton", "Value of property 'HeatingAutoButton'", 0)]
        [InlineData("read HeatingNightButton", "Value of property 'HeatingNightButton'", 0)]
        [InlineData("read HeatingOnOffButton", "Value of property 'HeatingOnOffButton'", 0)]
        [InlineData("read HeatingHomeButton", "Value of property 'HeatingHomeButton'", 0)]
        [InlineData("read HeatingAwayButton", "Value of property 'HeatingAwayButton'", 0)]
        [InlineData("read HeatingHolidayStart", "Value of property 'HeatingHolidayStart'", 0)]
        [InlineData("read HeatingHolidayEnd", "Value of property 'HeatingHolidayEnd'", 0)]
        [InlineData("read DischargeScrewDemand", "Value of property 'DischargeScrewDemand'", 0)]
        [InlineData("read DischargeScrewClockRate", "Value of property 'DischargeScrewClockRate'", 0)]
        [InlineData("read DischargeScrewState", "Value of property 'DischargeScrewState'", 0)]
        [InlineData("read DischargeScrewMotorCurr", "Value of property 'DischargeScrewMotorCurr'", 0)]
        [InlineData("read ConveyingSystem", "Value of property 'ConveyingSystem'", 0)]
        [InlineData("read Stock", "Value of property 'Stock'", 0)]
        [InlineData("read StockWarningLimit", "Value of property 'StockWarningLimit'", 0)]
        [InlineData("read OutsideTemperature", "Value of property 'OutsideTemperature'", 0)]
        [InlineData("read FirebedState", "Value of property 'FirebedState'", 0)]
        [InlineData("read SupplyDemand", "Value of property 'SupplyDemand'", 0)]
        [InlineData("read IgnitionDemand", "Value of property 'IgnitionDemand'", 0)]
        [InlineData("read FlowMixValveTemperature", "Value of property 'FlowMixValveTemperature'", 0)]
        [InlineData("read AirValveSetPosition", "Value of property 'AirValveSetPosition'", 0)]
        [InlineData("read AirValveCurrPosition", "Value of property 'AirValveCurrPosition'", 0)]
        public void TestReadCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("write", "The property name field is required", 1)]
        [InlineData("write -?", "Usage: ETAPU11App write [arguments] [options]", 0)]
        [InlineData("write --help", "Usage: ETAPU11App write [arguments] [options]", 0)]
        public void TestWriteCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("monitor", "Select a data option", 0)]
        [InlineData("monitor -?", "ETAPU11App monitor [options]", 0)]
        [InlineData("monitor --help", "ETAPU11App monitor [options]", 0)]
        [InlineData("monitor -a", "Data:", 0)]
        [InlineData("monitor --all", "Data:", 0)]
        [InlineData("monitor -b", "BoilerData:", 0)]
        [InlineData("monitor --boiler", "BoilerData:", 0)]
        [InlineData("monitor -w", "HotwaterData:", 0)]
        [InlineData("monitor --hotwater", "HotwaterData:", 0)]
        [InlineData("monitor -h", "HeatingData:", 0)]
        [InlineData("monitor --heating", "HeatingData:", 0)]
        [InlineData("monitor -s", "StorageData:", 0)]
        [InlineData("monitor --storage", "StorageData:", 0)]
        [InlineData("monitor -y", "SystemData:", 0)]
        [InlineData("monitor --system", "SystemData:", 0)]
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
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\ETAPU11\ETAPU11App";

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
