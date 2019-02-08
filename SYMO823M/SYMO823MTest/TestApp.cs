// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApp.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MTest
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

    using SYMO823MLib;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("SYMO823M Test Collection")]
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
            _logger = loggerFactory.CreateLogger<SYMO823M>();
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("", "Modbus TCP client found", 0)]
        [InlineData("-?", "Usage: SYMO823MApp [options] [command]", 0)]
        [InlineData("--help", "Usage: SYMO823MApp [options] [command]", 0)]
        [InlineData("info", "Select an info option", 0)]
        [InlineData("info -?", "Usage: SYMO823MApp info [options]", 0)]
        [InlineData("info --help", "Usage: SYMO823MApp info [options]", 0)]
        [InlineData("read", "26341000707200154", 0)]
        [InlineData("read -?", "Usage: SYMO823MApp read [arguments] [options]", 0)]
        [InlineData("read --help", "Usage: SYMO823MApp read [arguments] [options]", 0)]
        [InlineData("write", "The property name field is required", 1)]
        [InlineData("write -?", "Usage: SYMO823MApp write [arguments] [options]", 0)]
        [InlineData("write --help", "Usage: SYMO823MApp write [arguments] [options]", 0)]
        [InlineData("monitor", "Select a data option", 0)]
        [InlineData("monitor -?", "SYMO823MApp monitor [options]", 0)]
        [InlineData("monitor --help", "SYMO823MApp monitor [options]", 0)]
        public void TestCommand(string args, string result, int exit)
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
            proc.StartInfo.FileName = @"C:\Users\peter\Documents\Visual Studio 2017\Projects\HomeControl.2.1\SYMO823M\SYMO823MApp\bin\Release\netcoreapp2.1\win-x86\SYMO823MApp.exe";

            // add arguments as whole string
            proc.StartInfo.Arguments = arguments;

            // use it to start from testing environment
            proc.StartInfo.UseShellExecute = false;

            // redirect outputs to have it in testing console
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            // set working directory
            proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;

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
