// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRTest
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

    using EM300LRLib;

    #endregion Using Directives

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("EM300LR Test Collection")]
    public class TestApp
    {
        #region Private Data Members

        private readonly ILogger _logger;

        #endregion Private Data Members

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
            _logger = loggerFactory.CreateLogger<EM300LR>();
        }

        #endregion Constructors

        #region Test Methods

        [Theory]
        [InlineData("", "EM300LR web service with serial number", 0)]
        [InlineData("-?", "Usage: EM300LRApp [options] [command]", 0)]
        [InlineData("--help", "Usage: EM300LRApp [options] [command]", 0)]
        [InlineData("--address http://10.0.1.10 --serialnumber 72130511", "EM300LR web service with serial number", 0)]
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
        [InlineData("info -?", "Usage: EM300LRApp info [options]", 0)]
        [InlineData("info --help", "Usage: EM300LRApp info [options]", 0)]
        [InlineData("info -t", "TotalData", 0)]
        [InlineData("info --total", "TotalData", 0)]
        [InlineData("info -1", "Phase1Data", 0)]
        [InlineData("info --phase1", "Phase1Data", 0)]
        [InlineData("info -2", "Phase2Data", 0)]
        [InlineData("info --phase2", "Phase2Data", 0)]
        [InlineData("info -3", "Phase3Data", 0)]
        [InlineData("info --phase3", "Phase3Data", 0)]
        public void TestInfoCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("read", "EM300LRData:", 0)]
        [InlineData("read -?", "Usage: EM300LRApp read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: EM300LRApp read [arguments] [options]", 0)]
        [InlineData("read ActivePowerPlus", "Value of property 'ActivePowerPlus'", 0)]
        [InlineData("read ActiveEnergyPlus", "Value of property 'ActiveEnergyPlus'", 0)]
        [InlineData("read ActivePowerMinus", "Value of property 'ActivePowerMinus'", 0)]
        [InlineData("read ActiveEnergyMinus", "Value of property 'ActiveEnergyMinus'", 0)]
        [InlineData("read ReactivePowerPlus", "Value of property 'ReactivePowerPlus'", 0)]
        [InlineData("read ReactiveEnergyPlus", "Value of property 'ReactiveEnergyPlus'", 0)]
        [InlineData("read ReactivePowerMinus", "Value of property 'ReactivePowerMinus'", 0)]
        [InlineData("read ReactiveEnergyMinus", "Value of property 'ReactiveEnergyMinus'", 0)]
        [InlineData("read ApparentPowerPlus", "Value of property 'ApparentPowerPlus'", 0)]
        [InlineData("read ApparentEnergyPlus", "Value of property 'ApparentEnergyPlus'", 0)]
        [InlineData("read ApparentPowerMinus", "Value of property 'ApparentPowerMinus'", 0)]
        [InlineData("read ApparentEnergyMinus", "Value of property 'ApparentEnergyMinus'", 0)]
        [InlineData("read PowerFactor", "Value of property 'PowerFactor'", 0)]
        [InlineData("read SupplyFrequency", "Value of property 'SupplyFrequency'", 0)]
        [InlineData("read ActivePowerPlusL1", "Value of property 'ActivePowerPlusL1'", 0)]
        [InlineData("read ActiveEnergyPlusL1", "Value of property 'ActiveEnergyPlusL1'", 0)]
        [InlineData("read ActivePowerMinusL1", "Value of property 'ActivePowerMinusL1'", 0)]
        [InlineData("read ActiveEnergyMinusL1", "Value of property 'ActiveEnergyMinusL1'", 0)]
        [InlineData("read ReactivePowerPlusL1", "Value of property 'ReactivePowerPlusL1'", 0)]
        [InlineData("read ReactiveEnergyPlusL1", "Value of property 'ReactiveEnergyPlusL1'", 0)]
        [InlineData("read ReactivePowerMinusL1", "Value of property 'ReactivePowerMinusL1'", 0)]
        [InlineData("read ReactiveEnergyMinusL1", "Value of property 'ReactiveEnergyMinusL1'", 0)]
        [InlineData("read ApparentPowerPlusL1", "Value of property 'ApparentPowerPlusL1'", 0)]
        [InlineData("read ApparentEnergyPlusL1", "Value of property 'ApparentEnergyPlusL1'", 0)]
        [InlineData("read ApparentPowerMinusL1", "Value of property 'ApparentPowerMinusL1'", 0)]
        [InlineData("read ApparentEnergyMinusL1", "Value of property 'ApparentEnergyMinusL1'", 0)]
        [InlineData("read CurrentL1", "Value of property 'CurrentL1'", 0)]
        [InlineData("read VoltageL1", "Value of property 'VoltageL1'", 0)]
        [InlineData("read PowerFactorL1", "Value of property 'PowerFactorL1'", 0)]
        [InlineData("read ActivePowerPlusL2", "Value of property 'ActivePowerPlusL2'", 0)]
        [InlineData("read ActiveEnergyPlusL2", "Value of property 'ActiveEnergyPlusL2'", 0)]
        [InlineData("read ActivePowerMinusL2", "Value of property 'ActivePowerMinusL2'", 0)]
        [InlineData("read ActiveEnergyMinusL2", "Value of property 'ActiveEnergyMinusL2'", 0)]
        [InlineData("read ReactivePowerPlusL2", "Value of property 'ReactivePowerPlusL2'", 0)]
        [InlineData("read ReactiveEnergyPlusL2", "Value of property 'ReactiveEnergyPlusL2'", 0)]
        [InlineData("read ReactivePowerMinusL2", "Value of property 'ReactivePowerMinusL2'", 0)]
        [InlineData("read ReactiveEnergyMinusL2", "Value of property 'ReactiveEnergyMinusL2'", 0)]
        [InlineData("read ApparentPowerPlusL2", "Value of property 'ApparentPowerPlusL2'", 0)]
        [InlineData("read ApparentEnergyPlusL2", "Value of property 'ApparentEnergyPlusL2'", 0)]
        [InlineData("read ApparentPowerMinusL2", "Value of property 'ApparentPowerMinusL2'", 0)]
        [InlineData("read ApparentEnergyMinusL2", "Value of property 'ApparentEnergyMinusL2'", 0)]
        [InlineData("read CurrentL2", "Value of property 'CurrentL2'", 0)]
        [InlineData("read VoltageL2", "Value of property 'VoltageL2'", 0)]
        [InlineData("read PowerFactorL2", "Value of property 'PowerFactorL2'", 0)]
        [InlineData("read ActivePowerPlusL3", "Value of property 'ActivePowerPlusL3'", 0)]
        [InlineData("read ActiveEnergyPlusL3", "Value of property 'ActiveEnergyPlusL3'", 0)]
        [InlineData("read ActivePowerMinusL3", "Value of property 'ActivePowerMinusL3'", 0)]
        [InlineData("read ActiveEnergyMinusL3", "Value of property 'ActiveEnergyMinusL3'", 0)]
        [InlineData("read ReactivePowerPlusL3", "Value of property 'ReactivePowerPlusL3'", 0)]
        [InlineData("read ReactiveEnergyPlusL3", "Value of property 'ReactiveEnergyPlusL3'", 0)]
        [InlineData("read ReactivePowerMinusL3", "Value of property 'ReactivePowerMinusL3'", 0)]
        [InlineData("read ReactiveEnergyMinusL3", "Value of property 'ReactiveEnergyMinusL3'", 0)]
        [InlineData("read ApparentPowerPlusL3", "Value of property 'ApparentPowerPlusL3'", 0)]
        [InlineData("read ApparentEnergyPlusL3", "Value of property 'ApparentEnergyPlusL3'", 0)]
        [InlineData("read ApparentPowerMinusL3", "Value of property 'ApparentPowerMinusL3'", 0)]
        [InlineData("read ApparentEnergyMinusL3", "Value of property 'ApparentEnergyMinusL3'", 0)]
        [InlineData("read CurrentL3", "Value of property 'CurrentL3'", 0)]
        [InlineData("read VoltageL3", "Value of property 'VoltageL3'", 0)]
        [InlineData("read PowerFactorL3", "Value of property 'PowerFactorL3'", 0)]
        [InlineData("read Data", "Value of property 'Data'", 0)]
        [InlineData("read Data.ActivePowerPlus", "Value of property 'Data.ActivePowerPlus'", 0)]
        [InlineData("read Data.ActiveEnergyPlus", "Value of property 'Data.ActiveEnergyPlus'", 0)]
        [InlineData("read Data.ActivePowerMinus", "Value of property 'Data.ActivePowerMinus'", 0)]
        [InlineData("read Data.ActiveEnergyMinus", "Value of property 'Data.ActiveEnergyMinus'", 0)]
        [InlineData("read Data.ReactivePowerPlus", "Value of property 'Data.ReactivePowerPlus'", 0)]
        [InlineData("read Data.ReactiveEnergyPlus", "Value of property 'Data.ReactiveEnergyPlus'", 0)]
        [InlineData("read Data.ReactivePowerMinus", "Value of property 'Data.ReactivePowerMinus'", 0)]
        [InlineData("read Data.ReactiveEnergyMinus", "Value of property 'Data.ReactiveEnergyMinus'", 0)]
        [InlineData("read Data.ApparentPowerPlus", "Value of property 'Data.ApparentPowerPlus'", 0)]
        [InlineData("read Data.ApparentEnergyPlus", "Value of property 'Data.ApparentEnergyPlus'", 0)]
        [InlineData("read Data.ApparentPowerMinus", "Value of property 'Data.ApparentPowerMinus'", 0)]
        [InlineData("read Data.ApparentEnergyMinus", "Value of property 'Data.ApparentEnergyMinus'", 0)]
        [InlineData("read Data.PowerFactor", "Value of property 'Data.PowerFactor'", 0)]
        [InlineData("read Data.SupplyFrequency", "Value of property 'Data.SupplyFrequency'", 0)]
        [InlineData("read Data.ActivePowerPlusL1", "Value of property 'Data.ActivePowerPlusL1'", 0)]
        [InlineData("read Data.ActiveEnergyPlusL1", "Value of property 'Data.ActiveEnergyPlusL1'", 0)]
        [InlineData("read Data.ActivePowerMinusL1", "Value of property 'Data.ActivePowerMinusL1'", 0)]
        [InlineData("read Data.ActiveEnergyMinusL1", "Value of property 'Data.ActiveEnergyMinusL1'", 0)]
        [InlineData("read Data.ReactivePowerPlusL1", "Value of property 'Data.ReactivePowerPlusL1'", 0)]
        [InlineData("read Data.ReactiveEnergyPlusL1", "Value of property 'Data.ReactiveEnergyPlusL1'", 0)]
        [InlineData("read Data.ReactivePowerMinusL1", "Value of property 'Data.ReactivePowerMinusL1'", 0)]
        [InlineData("read Data.ReactiveEnergyMinusL1", "Value of property 'Data.ReactiveEnergyMinusL1'", 0)]
        [InlineData("read Data.ApparentPowerPlusL1", "Value of property 'Data.ApparentPowerPlusL1'", 0)]
        [InlineData("read Data.ApparentEnergyPlusL1", "Value of property 'Data.ApparentEnergyPlusL1'", 0)]
        [InlineData("read Data.ApparentPowerMinusL1", "Value of property 'Data.ApparentPowerMinusL1'", 0)]
        [InlineData("read Data.ApparentEnergyMinusL1", "Value of property 'Data.ApparentEnergyMinusL1'", 0)]
        [InlineData("read Data.CurrentL1", "Value of property 'Data.CurrentL1'", 0)]
        [InlineData("read Data.VoltageL1", "Value of property 'Data.VoltageL1'", 0)]
        [InlineData("read Data.PowerFactorL1", "Value of property 'Data.PowerFactorL1'", 0)]
        [InlineData("read Data.ActivePowerPlusL2", "Value of property 'Data.ActivePowerPlusL2'", 0)]
        [InlineData("read Data.ActiveEnergyPlusL2", "Value of property 'Data.ActiveEnergyPlusL2'", 0)]
        [InlineData("read Data.ActivePowerMinusL2", "Value of property 'Data.ActivePowerMinusL2'", 0)]
        [InlineData("read Data.ActiveEnergyMinusL2", "Value of property 'Data.ActiveEnergyMinusL2'", 0)]
        [InlineData("read Data.ReactivePowerPlusL2", "Value of property 'Data.ReactivePowerPlusL2'", 0)]
        [InlineData("read Data.ReactiveEnergyPlusL2", "Value of property 'Data.ReactiveEnergyPlusL2'", 0)]
        [InlineData("read Data.ReactivePowerMinusL2", "Value of property 'Data.ReactivePowerMinusL2'", 0)]
        [InlineData("read Data.ReactiveEnergyMinusL2", "Value of property 'Data.ReactiveEnergyMinusL2'", 0)]
        [InlineData("read Data.ApparentPowerPlusL2", "Value of property 'Data.ApparentPowerPlusL2'", 0)]
        [InlineData("read Data.ApparentEnergyPlusL2", "Value of property 'Data.ApparentEnergyPlusL2'", 0)]
        [InlineData("read Data.ApparentPowerMinusL2", "Value of property 'Data.ApparentPowerMinusL2'", 0)]
        [InlineData("read Data.ApparentEnergyMinusL2", "Value of property 'Data.ApparentEnergyMinusL2'", 0)]
        [InlineData("read Data.CurrentL2", "Value of property 'Data.CurrentL2'", 0)]
        [InlineData("read Data.VoltageL2", "Value of property 'Data.VoltageL2'", 0)]
        [InlineData("read Data.PowerFactorL2", "Value of property 'Data.PowerFactorL2'", 0)]
        [InlineData("read Data.ActivePowerPlusL3", "Value of property 'Data.ActivePowerPlusL3'", 0)]
        [InlineData("read Data.ActiveEnergyPlusL3", "Value of property 'Data.ActiveEnergyPlusL3'", 0)]
        [InlineData("read Data.ActivePowerMinusL3", "Value of property 'Data.ActivePowerMinusL3'", 0)]
        [InlineData("read Data.ActiveEnergyMinusL3", "Value of property 'Data.ActiveEnergyMinusL3'", 0)]
        [InlineData("read Data.ReactivePowerPlusL3", "Value of property 'Data.ReactivePowerPlusL3'", 0)]
        [InlineData("read Data.ReactiveEnergyPlusL3", "Value of property 'Data.ReactiveEnergyPlusL3'", 0)]
        [InlineData("read Data.ReactivePowerMinusL3", "Value of property 'Data.ReactivePowerMinusL3'", 0)]
        [InlineData("read Data.ReactiveEnergyMinusL3", "Value of property 'Data.ReactiveEnergyMinusL3'", 0)]
        [InlineData("read Data.ApparentPowerPlusL3", "Value of property 'Data.ApparentPowerPlusL3'", 0)]
        [InlineData("read Data.ApparentEnergyPlusL3", "Value of property 'Data.ApparentEnergyPlusL3'", 0)]
        [InlineData("read Data.ApparentPowerMinusL3", "Value of property 'Data.ApparentPowerMinusL3'", 0)]
        [InlineData("read Data.ApparentEnergyMinusL3", "Value of property 'Data.ApparentEnergyMinusL3'", 0)]
        [InlineData("read Data.CurrentL3", "Value of property 'Data.CurrentL3'", 0)]
        [InlineData("read Data.VoltageL3", "Value of property 'Data.VoltageL3'", 0)]
        [InlineData("read Data.PowerFactorL3", "Value of property 'Data.PowerFactorL3'", 0)]
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
        [InlineData("monitor -?", "EM300LRApp monitor [options]", 0)]
        [InlineData("monitor --help", "EM300LRApp monitor [options]", 0)]
        [InlineData("monitor -t", "TotalData", 0)]
        [InlineData("monitor --total", "TotalData", 0)]
        [InlineData("monitor -1", "Phase1Data", 0)]
        [InlineData("monitor --phase1", "Phase1Data", 0)]
        [InlineData("monitor -2", "Phase2Data", 0)]
        [InlineData("monitor --phase2", "Phase2Data", 0)]
        [InlineData("monitor -3", "Phase3Data", 0)]
        [InlineData("monitor --phase3", "Phase3Data", 0)]
        public void TestMonitorCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        #endregion Test Methods

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
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\EM300LR\EM300LRApp";

            // start and wait for exit
            proc.Start();
            proc.WaitForExit(10000);

            // get output to testing console.
            System.Console.WriteLine(proc.StandardOutput.ReadToEnd());
            System.Console.Write(proc.StandardError.ReadToEnd());

            // return exit code
            return proc.ExitCode;
        }

        #endregion Private Methods
    }
}