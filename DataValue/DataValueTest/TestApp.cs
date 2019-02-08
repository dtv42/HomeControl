namespace DataValueTest
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

    using DataValueLib;

    #endregion Using Directives

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("DataValue Test Collection")]
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
            _logger = loggerFactory.CreateLogger<DataValue>();
        }

        #endregion Constructors

        #region Test Methods

        [Theory]
        [InlineData("", "DataValue:", 0)]
        [InlineData("-?", "Usage: DataValueApp [options]", 0)]
        [InlineData("--help", "Usage: DataValueApp [options]", 0)]
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
        [InlineData("-t", "The operation completed successfully.", 0)]
        [InlineData("--testdata", "The operation completed successfully.", 0)]
        public void TestRootCommandOptionT(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("-i", "Property:          Test.Value", 0)]
        [InlineData("--info", "Property:          Test.Value", 0)]
        public void TestRootCommandOptionI(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("-v", "Property: Test.IsGood => True", 0)]
        [InlineData("--value", "Property: Test.IsGood => True", 0)]
        public void TestRootCommandOptionV(string args, string result, int exit)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);
            var code = StartConsoleApplication(args);
            Assert.Equal(exit, code);
            Assert.Contains(result, sw.ToString());
        }

        [Theory]
        [InlineData("-p Test", "Property: Test => DataValueApp.Models.AppData", 0)]
        [InlineData("-p Test.Value", "Property: Test.Value => 42", 0)]
        [InlineData("-p Test.Name", "Property: Test.Name => Test", 0)]
        [InlineData("-p Test.Info", "Property: Test.Info => Info", 0)]
        [InlineData("-p Test.Data", "Property: Test.Data => DataValueApp.Models.AppData+TestData", 0)]
        [InlineData("-p Test.Data.Name", "Property: Test.Data.Name => Data", 0)]
        [InlineData("-p Test.Data.Value", "Property: Test.Data.Value => 42", 0)]
        [InlineData("-p Test.Data.Url", "Property: Test.Data.Url => DataValueApp.Models.AppData+IPAddress", 0)]
        [InlineData("-p Test.Data.Code", "Property: Test.Data.Code => Ok", 0)]
        [InlineData("-p Test.Data.Url.Address", "Property: Test.Data.Url.Address => 127.0.0.1", 0)]
        [InlineData("-p Test.Data.Url.Bytes", "Property: Test.Data.Url.Bytes => System.Byte[]", 0)]
        [InlineData("-p Test.Data.Url.Operations", "Property: Test.Data.Url.Operations => System.Collections.Generic.List`1[System.String]", 0)]
        [InlineData("-p Test.Data.Url.Address.Length", "Property: Test.Data.Url.Address.Length => 9", 0)]
        [InlineData("-p Test.Data.Url.Bytes.Length", "Property: Test.Data.Url.Bytes.Length => 4", 0)]
        [InlineData("-p Test.Data.Url.Bytes[0]", "Property: Test.Data.Url.Bytes[0] => 127", 0)]
        [InlineData("-p Test.Data.Url.Bytes[1]", "Property: Test.Data.Url.Bytes[1] => 0", 0)]
        [InlineData("-p Test.Data.Url.Bytes[2]", "Property: Test.Data.Url.Bytes[2] => 0", 0)]
        [InlineData("-p Test.Data.Url.Bytes[3]", "Property: Test.Data.Url.Bytes[3] => 1", 0)]
        [InlineData("-p Test.Data.Url.Operations.Count", "Property: Test.Data.Url.Operations.Count => 3", 0)]
        [InlineData("-p Test.Data.Url.Operations[0]", "Property: Test.Data.Url.Operations[0] => GET", 0)]
        [InlineData("-p Test.Data.Url.Operations[1]", "Property: Test.Data.Url.Operations[1] => PUT", 0)]
        [InlineData("-p Test.Data.Url.Operations[2]", "Property: Test.Data.Url.Operations[2] => POST", 0)]
        [InlineData("-p Test.Data.Url.Operations[0].Length", "Property: Test.Data.Url.Operations[0].Length => 3", 0)]
        [InlineData("-p Test.Data.Url.Operations[1].Length", "Property: Test.Data.Url.Operations[1].Length => 3", 0)]
        [InlineData("-p Test.Data.Url.Operations[2].Length", "Property: Test.Data.Url.Operations[2].Length => 4", 0)]
        [InlineData("-p Test.Array", "Property: Test.Array => DataValueApp.Models.AppData+TestData[]", 0)]
        [InlineData("-p Test.Array.Length", "Property: Test.Array.Length => 2", 0)]
        [InlineData("-p Test.Array[0]", "Property: Test.Array[0] => DataValueApp.Models.AppData+TestData", 0)]
        [InlineData("-p Test.Array[0].Name", "Property: Test.Array[0].Name => Test1", 0)]
        [InlineData("-p Test.Array[0].Value", "Property: Test.Array[0].Value => 42", 0)]
        [InlineData("-p Test.Array[0].Url", "Property: Test.Array[0].Url => DataValueApp.Models.AppData+IPAddress", 0)]
        [InlineData("-p Test.Array[0].Code", "Property: Test.Array[0].Code => Ok", 0)]
        [InlineData("-p Test.Array[0].Url.Address", "Property: Test.Array[0].Url.Address => 127.0.0.1", 0)]
        [InlineData("-p Test.Array[0].Url.Address.Length", "Property: Test.Array[0].Url.Address.Length => 9", 0)]
        [InlineData("-p Test.Array[0].Url.Bytes", "Property: Test.Array[0].Url.Bytes => System.Byte[]", 0)]
        [InlineData("-p Test.Array[0].Url.Bytes.Length", "Property: Test.Array[0].Url.Bytes.Length => 4", 0)]
        [InlineData("-p Test.Array[0].Url.Bytes[0]", "Property: Test.Array[0].Url.Bytes[0] => 127", 0)]
        [InlineData("-p Test.Array[0].Url.Bytes[1]", "Property: Test.Array[0].Url.Bytes[1] => 0", 0)]
        [InlineData("-p Test.Array[0].Url.Bytes[2]", "Property: Test.Array[0].Url.Bytes[2] => 0", 0)]
        [InlineData("-p Test.Array[0].Url.Bytes[3]", "Property: Test.Array[0].Url.Bytes[3] => 1", 0)]
        [InlineData("-p Test.Array[0].Url.Operations", "Property: Test.Array[0].Url.Operations => System.Collections.Generic.List`1[System.String]", 0)]
        [InlineData("-p Test.Array[0].Url.Operations.Count", "Property: Test.Array[0].Url.Operations.Count => 3", 0)]
        [InlineData("-p Test.Array[0].Url.Operations[0]", "Property: Test.Array[0].Url.Operations[0] => GET", 0)]
        [InlineData("-p Test.Array[0].Url.Operations[1]", "Property: Test.Array[0].Url.Operations[1] => PUT", 0)]
        [InlineData("-p Test.Array[0].Url.Operations[2]", "Property: Test.Array[0].Url.Operations[2] => POST", 0)]
        [InlineData("-p Test.Array[0].Url.Operations[0].Length", "Property: Test.Array[0].Url.Operations[0].Length => 3", 0)]
        [InlineData("-p Test.Array[0].Url.Operations[1].Length", "Property: Test.Array[0].Url.Operations[1].Length => 3", 0)]
        [InlineData("-p Test.Array[0].Url.Operations[2].Length", "Property: Test.Array[0].Url.Operations[2].Length => 4", 0)]
        [InlineData("-p Test.Array[1]", "Property: Test.Array[1] => DataValueApp.Models.AppData+TestData", 0)]
        [InlineData("-p Test.Array[1].Name", "Property: Test.Array[1].Name => Test2", 0)]
        [InlineData("-p Test.Array[1].Value", "Property: Test.Array[1].Value => 42", 0)]
        [InlineData("-p Test.Array[1].Url", "Property: Test.Array[1].Url => DataValueApp.Models.AppData+IPAddress", 0)]
        [InlineData("-p Test.Array[1].Code", "Property: Test.Array[1].Code => Ok", 0)]
        [InlineData("-p Test.Array[1].Url.Address", "Property: Test.Array[1].Url.Address => 127.0.0.1", 0)]
        [InlineData("-p Test.Array[1].Url.Address.Length", "Property: Test.Array[1].Url.Address.Length => 9", 0)]
        [InlineData("-p Test.Array[1].Url.Bytes", "Property: Test.Array[1].Url.Bytes => System.Byte[]", 0)]
        [InlineData("-p Test.Array[1].Url.Bytes.Length", "Property: Test.Array[1].Url.Bytes.Length => 4", 0)]
        [InlineData("-p Test.Array[1].Url.Bytes[0]", "Property: Test.Array[1].Url.Bytes[0] => 127", 0)]
        [InlineData("-p Test.Array[1].Url.Bytes[1]", "Property: Test.Array[1].Url.Bytes[1] => 0", 0)]
        [InlineData("-p Test.Array[1].Url.Bytes[2]", "Property: Test.Array[1].Url.Bytes[2] => 0", 0)]
        [InlineData("-p Test.Array[1].Url.Bytes[3]", "Property: Test.Array[1].Url.Bytes[3] => 1", 0)]
        [InlineData("-p Test.Array[1].Url.Operations", "Property: Test.Array[1].Url.Operations => System.Collections.Generic.List`1[System.String]", 0)]
        [InlineData("-p Test.Array[1].Url.Operations.Count", "Property: Test.Array[1].Url.Operations.Count => 3", 0)]
        [InlineData("-p Test.Array[1].Url.Operations[0]", "Property: Test.Array[1].Url.Operations[0] => GET", 0)]
        [InlineData("-p Test.Array[1].Url.Operations[1]", "Property: Test.Array[1].Url.Operations[1] => PUT", 0)]
        [InlineData("-p Test.Array[1].Url.Operations[2]", "Property: Test.Array[1].Url.Operations[2] => POST", 0)]
        [InlineData("-p Test.Array[1].Url.Operations[0].Length", "Property: Test.Array[1].Url.Operations[0].Length => 3", 0)]
        [InlineData("-p Test.Array[1].Url.Operations[1].Length", "Property: Test.Array[1].Url.Operations[1].Length => 3", 0)]
        [InlineData("-p Test.Array[1].Url.Operations[2].Length", "Property: Test.Array[1].Url.Operations[2].Length => 4", 0)]
        [InlineData("-p Test.List", "Property: Test.List => System.Collections.Generic.List`1[DataValueApp.Models.AppData+TestData]", 0)]
        [InlineData("-p Test.List.Count", "Property: Test.List.Count => 2", 0)]
        [InlineData("-p Test.List[0]", "Property: Test.List[0] => DataValueApp.Models.AppData+TestData", 0)]
        [InlineData("-p Test.List[0].Name", "Property: Test.List[0].Name => Test1", 0)]
        [InlineData("-p Test.List[0].Value", "Property: Test.List[0].Value => 42", 0)]
        [InlineData("-p Test.List[0].Url", "Property: Test.List[0].Url => DataValueApp.Models.AppData+IPAddress", 0)]
        [InlineData("-p Test.List[0].Code", "Property: Test.List[0].Code => Ok", 0)]
        [InlineData("-p Test.List[0].Url.Address", "Property: Test.List[0].Url.Address => 127.0.0.1", 0)]
        [InlineData("-p Test.List[0].Url.Address.Length", "Property: Test.List[0].Url.Address.Length => 9", 0)]
        [InlineData("-p Test.List[0].Url.Bytes", "Property: Test.List[0].Url.Bytes => System.Byte[]", 0)]
        [InlineData("-p Test.List[0].Url.Bytes.Length", "Property: Test.List[0].Url.Bytes.Length => 4", 0)]
        [InlineData("-p Test.List[0].Url.Bytes[0]", "Property: Test.List[0].Url.Bytes[0] => 127", 0)]
        [InlineData("-p Test.List[0].Url.Bytes[1]", "Property: Test.List[0].Url.Bytes[1] => 0", 0)]
        [InlineData("-p Test.List[0].Url.Bytes[2]", "Property: Test.List[0].Url.Bytes[2] => 0", 0)]
        [InlineData("-p Test.List[0].Url.Bytes[3]", "Property: Test.List[0].Url.Bytes[3] => 1", 0)]
        [InlineData("-p Test.List[0].Url.Operations", "Property: Test.List[0].Url.Operations => System.Collections.Generic.List`1[System.String]", 0)]
        [InlineData("-p Test.List[0].Url.Operations.Count", "Property: Test.List[0].Url.Operations.Count => 3", 0)]
        [InlineData("-p Test.List[0].Url.Operations[0]", "Property: Test.List[0].Url.Operations[0] => GET", 0)]
        [InlineData("-p Test.List[0].Url.Operations[1]", "Property: Test.List[0].Url.Operations[1] => PUT", 0)]
        [InlineData("-p Test.List[0].Url.Operations[2]", "Property: Test.List[0].Url.Operations[2] => POST", 0)]
        [InlineData("-p Test.List[0].Url.Operations[0].Length", "Property: Test.List[0].Url.Operations[0].Length => 3", 0)]
        [InlineData("-p Test.List[0].Url.Operations[1].Length", "Property: Test.List[0].Url.Operations[1].Length => 3", 0)]
        [InlineData("-p Test.List[0].Url.Operations[2].Length", "Property: Test.List[0].Url.Operations[2].Length => 4", 0)]
        [InlineData("-p Test.List[1]", "Property: Test.List[1] => DataValueApp.Models.AppData+TestData", 0)]
        [InlineData("-p Test.List[1].Name", "Property: Test.List[1].Name => Test2", 0)]
        [InlineData("-p Test.List[1].Value", "Property: Test.List[1].Value => 42", 0)]
        [InlineData("-p Test.List[1].Url", "Property: Test.List[1].Url => DataValueApp.Models.AppData+IPAddress", 0)]
        [InlineData("-p Test.List[1].Code", "Property: Test.List[1].Code => Ok", 0)]
        [InlineData("-p Test.List[1].Url.Address", "Property: Test.List[1].Url.Address => 127.0.0.1", 0)]
        [InlineData("-p Test.List[1].Url.Address.Length", "Property: Test.List[1].Url.Address.Length => 9", 0)]
        [InlineData("-p Test.List[1].Url.Bytes", "Property: Test.List[1].Url.Bytes => System.Byte[]", 0)]
        [InlineData("-p Test.List[1].Url.Bytes.Length", "Property: Test.List[1].Url.Bytes.Length => 4", 0)]
        [InlineData("-p Test.List[1].Url.Bytes[0]", "Property: Test.List[1].Url.Bytes[0] => 127", 0)]
        [InlineData("-p Test.List[1].Url.Bytes[1]", "Property: Test.List[1].Url.Bytes[1] => 0", 0)]
        [InlineData("-p Test.List[1].Url.Bytes[2]", "Property: Test.List[1].Url.Bytes[2] => 0", 0)]
        [InlineData("-p Test.List[1].Url.Bytes[3]", "Property: Test.List[1].Url.Bytes[3] => 1", 0)]
        [InlineData("-p Test.List[1].Url.Operations", "Property: Test.List[1].Url.Operations => System.Collections.Generic.List`1[System.String]", 0)]
        [InlineData("-p Test.List[1].Url.Operations.Count", "Property: Test.List[1].Url.Operations.Count => 3", 0)]
        [InlineData("-p Test.List[1].Url.Operations[0]", "Property: Test.List[1].Url.Operations[0] => GET", 0)]
        [InlineData("-p Test.List[1].Url.Operations[1]", "Property: Test.List[1].Url.Operations[1] => PUT", 0)]
        [InlineData("-p Test.List[1].Url.Operations[2]", "Property: Test.List[1].Url.Operations[2] => POST", 0)]
        [InlineData("-p Test.List[1].Url.Operations[0].Length", "Property: Test.List[1].Url.Operations[0].Length => 3", 0)]
        [InlineData("-p Test.List[1].Url.Operations[1].Length", "Property: Test.List[1].Url.Operations[1].Length => 3", 0)]
        [InlineData("-p Test.List[1].Url.Operations[2].Length", "Property: Test.List[1].Url.Operations[2].Length => 4", 0)]
        [InlineData("-p Test.Status", "Property: Test.Status => DataValueLib.DataStatus", 0)]
        [InlineData("-p Test.Status.Code", "Property: Test.Status.Code => 0", 0)]
        [InlineData("-p Test.Status.Name", "Property: Test.Status.Name => Good", 0)]
        [InlineData("-p Test.Status.Explanation", "Property: Test.Status.Explanation => The operation completed successfully.", 0)]
        [InlineData("-p Test.Timestamp", "Property: Test.Timestamp => ", 0)]
        [InlineData("-p Test.IsGood", "Property: Test.IsGood => True", 0)]
        [InlineData("-p Test.IsBad", "Property: Test.IsBad => False", 0)]
        [InlineData("-p Test.IsUncertain", "Property: Test.IsUncertain => False", 0)]
        [InlineData("-p Text", "Property Text not found.", 0)]
        [InlineData("-p", "Missing value for option 'p'.", -1)]
        [InlineData("--property", "Missing value for option 'property'.", -1)]
        public void TestRootCommandOptionP(string args, string result, int exit)
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
            proc.StartInfo.WorkingDirectory = @"C:\Users\peter\source\repos\HomeControl.2.2\DataValue\DataValueApp";

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