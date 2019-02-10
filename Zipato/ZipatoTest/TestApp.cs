// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoTest
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

    using ZipatoLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
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
            _logger = loggerFactory.CreateLogger<Zipato>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Zipato web service found", 0)]
        [InlineData("-?", "Usage: ZipatoApp [options] [command]", 0)]
        [InlineData("--help", "Usage: ZipatoApp [options] [command]", 0)]
        [InlineData("--address https://my.zipato.com/zipato-web/v2/", "Zipato web service found", 0)]
        [InlineData("--timeout 10", "Zipato web service found", 0)]
        [InlineData("--user peter.trimmel@live.com", "Zipato web service found", 0)]
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
        [InlineData("info", "Usage: ZipatoApp info [options]", 0)]
        [InlineData("info -?", "Usage: ZipatoApp info [options]", 0)]
        [InlineData("info --help", "Usage: ZipatoApp info [options]", 0)]
        public void TestInfoCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("find", "Usage: ZipatoApp find [options]", 0)]
        [InlineData("find -?", "Usage: ZipatoApp find [options]", 0)]
        [InlineData("find --help", "Usage: ZipatoApp find [options]", 0)]
        public void TestFindCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("read -bx", "ZT7E104099D0D75BCC", 0)]
        [InlineData("read -?", "Usage: ZipatoApp read [options]", 0)]
        [InlineData("read --help", "Usage: ZipatoApp read [options]", 0)]
        public void TestReadCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("value", "Usage: ZipatoApp value [options]", 0)]
        [InlineData("value -?", "Usage: ZipatoApp value [options]", 0)]
        [InlineData("value --help", "Usage: ZipatoApp value [options]", 0)]
        public void TestValueCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("write", "The attribute value field is required", 1)]
        [InlineData("write -?", "Usage: ZipatoApp write [arguments] [options]", 0)]
        [InlineData("write --help", "Usage: ZipatoApp write [arguments] [options]", 0)]
        public void TestWriteCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("clean", "Usage: ZipatoApp clean [options]", 0)]
        [InlineData("clean -?", "Usage: ZipatoApp clean [options]", 0)]
        [InlineData("clean --help", "Usage: ZipatoApp clean [options]", 0)]
        public void TestCleanCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("color", "The endpoint (UUID) field is required", 1)]
        [InlineData("color -?", "Usage: ZipatoApp color [arguments] [options]", 0)]
        [InlineData("color --help", "Usage: ZipatoApp color [arguments] [options]", 0)]
        public void TestColorCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("delete", "Usage: ZipatoApp delete [options]", 0)]
        [InlineData("delete -?", "Usage: ZipatoApp delete [options]", 0)]
        [InlineData("delete --help", "Usage: ZipatoApp delete [options]", 0)]
        public void TestDeleteCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("monitor", "ZipatoApp monitor [options]", 0)]
        [InlineData("monitor -?", "ZipatoApp monitor [options]", 0)]
        [InlineData("monitor --help", "ZipatoApp monitor [options]", 0)]
        public void TestMonitorCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("devices", "Zipato Devices:", 0)]
        [InlineData("devices -?", "ZipatoApp devices [options] [command]", 0)]
        [InlineData("devices --help", "ZipatoApp devices [options] [command]", 0)]
        [InlineData("devices dimmer", "Dimmers:", 0)]
        [InlineData("devices dimmer -?", "Usage: ZipatoApp devices dimmer [options]", 0)]
        [InlineData("devices dimmer --help", "Usage: ZipatoApp devices dimmer [options]", 0)]
        [InlineData("devices switch", "Switch devices:", 0)]
        [InlineData("devices switch -?", "Usage: ZipatoApp devices switch [options]", 0)]
        [InlineData("devices switch --help", "Usage: ZipatoApp devices switch [options]", 0)]
        [InlineData("devices onoff", "OnOff devices:", 0)]
        [InlineData("devices onoff -?", "Usage: ZipatoApp devices onoff [options]", 0)]
        [InlineData("devices onoff --help", "Usage: ZipatoApp devices onoff [options]", 0)]
        [InlineData("devices plug", "Wallplugs:", 0)]
        [InlineData("devices plug -?", "Usage: ZipatoApp devices plug [options]", 0)]
        [InlineData("devices plug --help", "Usage: ZipatoApp devices plug [options]", 0)]
        [InlineData("devices rgb", "RGB devices:", 0)]
        [InlineData("devices rgb -?", "Usage: ZipatoApp devices rgb [options]", 0)]
        [InlineData("devices rgb --help", "Usage: ZipatoApp devices rgb [options]", 0)]
        public void TestDevicesCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("others", "Zipato Others:", 0)]
        [InlineData("others -?", "ZipatoApp others [options] [command]", 0)]
        [InlineData("others --help", "ZipatoApp others [options] [command]", 0)]
        [InlineData("others camera", "Cameras:", 0)]
        [InlineData("others camera -?", "Usage: ZipatoApp others camera [options]", 0)]
        [InlineData("others camera --help", "Usage: ZipatoApp others camera [options]", 0)]
        [InlineData("others scene", "Scenes:", 0)]
        [InlineData("others scene -?", "Usage: ZipatoApp others scene [options]", 0)]
        [InlineData("others scene --help", "Usage: ZipatoApp others scene [options]", 0)]
        public void TestOthersCommand(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("sensors", "Zipato Sensors:", 0)]
        [InlineData("sensors -?", "ZipatoApp sensors [options] [command]", 0)]
        [InlineData("sensors --help", "ZipatoApp sensors [options] [command]", 0)]
        [InlineData("sensors meter", "Meter:", 0)]
        [InlineData("sensors meter -?", "Usage: ZipatoApp sensors meter [options]", 0)]
        [InlineData("sensors meter --help", "Usage: ZipatoApp sensors meter [options]", 0)]
        [InlineData("sensors virtual", "Virtual Meter:", 0)]
        [InlineData("sensors virtual -?", "Usage: ZipatoApp sensors virtual [options]", 0)]
        [InlineData("sensors virtual --help", "Usage: ZipatoApp sensors virtual [options]", 0)]
        [InlineData("sensors temperature", "Temperature Sensors:", 0)]
        [InlineData("sensors temperature -?", "Usage: ZipatoApp sensors temperature [options]", 0)]
        [InlineData("sensors temperature --help", "Usage: ZipatoApp sensors temperature [options]", 0)]
        [InlineData("sensors humidity", "Humidity Sensors:", 0)]
        [InlineData("sensors humidity -?", "Usage: ZipatoApp sensors humidity [options]", 0)]
        [InlineData("sensors humidity --help", "Usage: ZipatoApp sensors humidity [options]", 0)]
        [InlineData("sensors luminance", "Luminance Sensors:", 0)]
        [InlineData("sensors luminance -?", "Usage: ZipatoApp sensors luminance [options]", 0)]
        [InlineData("sensors luminance --help", "Usage: ZipatoApp sensors luminance [options]", 0)]
        public void TestServicesCommand(string args, string result, int exit)
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
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\Zipato\ZipatoApp";

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
