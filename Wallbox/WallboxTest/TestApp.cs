// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxTest
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

    using WallboxLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Wallbox Test Collection")]
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
            _logger = loggerFactory.CreateLogger<Wallbox>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Wallbox UDP service with firmware", 0)]
        [InlineData("-?", "Usage: WallboxApp [options] [command]", 0)]
        [InlineData("--help", "Usage: WallboxApp [options] [command]", 0)]
        [InlineData("--host 10.0.1.9", "UDP service with firmware", 0)]
        [InlineData("--port 7090", "UDP service with firmware", 0)]
        [InlineData("--timeout 10", "UDP service with firmware", 0)]
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
        [InlineData("info -?", "Usage: WallboxApp info [options]", 0)]
        [InlineData("info --help", "Usage: WallboxApp info [options]", 0)]
        [InlineData("info -1", "Report1:", 0)]
        [InlineData("info --report1", "Report1:", 0)]
        [InlineData("info -2", "Report2:", 0)]
        [InlineData("info --report2", "Report2:", 0)]
        [InlineData("info -3", "Report3:", 0)]
        [InlineData("info --report3", "Report3:", 0)]
        [InlineData("info -l", "Report100:", 0)]
        [InlineData("info --last", "Report100:", 0)]
        [InlineData("info -r", "Reports:", 0)]
        [InlineData("info --reports", "Reports:", 0)]
        [InlineData("info -i 1", "Report:", 0)]
        [InlineData("info --id 1", "Report:", 0)]
        [InlineData("info -i 2", "Report:", 0)]
        [InlineData("info --id 2", "Report:", 0)]
        [InlineData("info -i 3", "Report:", 0)]
        [InlineData("info --id 3", "Report:", 0)]
        [InlineData("info -i 100", "Report:", 0)]
        [InlineData("info --id 100", "Report:", 0)]
        [InlineData("info -i 101", "Report:", 0)]
        [InlineData("info --id 101", "Report:", 0)]
        [InlineData("info -i 4", "Report ID 4 not supported", 0)]
        [InlineData("info --id 4", "Report ID 4 not supported", 0)]
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
        [InlineData("read -?", "Usage: WallboxApp read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: WallboxApp read [arguments] [options]", 0)]
        [InlineData("read -a", "WallboxData:", 0)]
        [InlineData("read --all", "WallboxData:", 0)]
        [InlineData("read -1", "Report1:", 0)]
        [InlineData("read --report1", "Report1:", 0)]
        [InlineData("read -2", "Report2:", 0)]
        [InlineData("read --report2", "Report2:", 0)]
        [InlineData("read -3", "Report3:", 0)]
        [InlineData("read --report3", "Report3:", 0)]
        [InlineData("read -l", "Report100:", 0)]
        [InlineData("read --last", "Report100:", 0)]
        [InlineData("read -r", "Reports:", 0)]
        [InlineData("read --reports", "Reports:", 0)]

        [InlineData("read Wallbox", "Value of property 'Wallbox' =", 0)]
        [InlineData("read Wallbox.Data", "Value of property 'Wallbox.Data' =", 0)]
        [InlineData("read Wallbox.Data.Report1", "Value of property 'Wallbox.Data.Report1' =", 0)]
        [InlineData("read Wallbox.Data.Report2", "Value of property 'Wallbox.Data.Report2' =", 0)]
        [InlineData("read Wallbox.Data.Report3", "Value of property 'Wallbox.Data.Report3' =", 0)]
        [InlineData("read Wallbox.Data.Report100", "Value of property 'Wallbox.Data.Report100' =", 0)]
        [InlineData("read Wallbox.Data.Reports", "Value of property 'Wallbox.Data.Reports' =", 0)]
        [InlineData("read Wallbox.Data.Reports[0]", "Value of property 'Wallbox.Data.Reports[0]' =", 0)]
        [InlineData("read Wallbox.Data.Reports[29]", "Value of property 'Wallbox.Data.Reports[29]' =", 0)]

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
        [InlineData("monitor -?", "WallboxApp monitor [options]", 0)]
        [InlineData("monitor --help", "WallboxApp monitor [options]", 0)]
        [InlineData("monitor -a", "Wallbox Data", 0)]
        [InlineData("monitor --all", "Wallbox Data", 0)]
        [InlineData("monitor -1", "Report 1 Data", 0)]
        [InlineData("monitor --report1", "Report 1 Data", 0)]
        [InlineData("monitor -2", "Report 2 Data", 0)]
        [InlineData("monitor --report2", "Report 2 Data", 0)]
        [InlineData("monitor -3", "Report 3 Data", 0)]
        [InlineData("monitor --report3", "Report 3 Data", 0)]
        [InlineData("monitor -l", "Report 100 Data", 0)]
        [InlineData("monitor --last", "Report 100 Data", 0)]
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
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\Wallbox\WallboxApp";

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
