// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueApp.Commands
{
    #region Using Directives

    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;
    using DataValueApp.Models;

    #endregion Using Directives

    /// <summary>
    /// This is the root command of the application.
    /// </summary>
    [Command(Name = "DataValueApp",
             FullName = "Data Value Application",
             Description = "A .NET core 2.2 console application.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption()]
    public class RootCommand : BaseCommand<AppSettings>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCommand"/> class.
        /// The RootCommand sets default values for some properties using the application settings.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RootCommand(ILogger<RootCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("RootCommand()");

            Test = _settings.TestData;
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// The version is determined using the assembly.
        /// </summary>
        /// <returns></returns>
        private static string GetVersion() => Assembly.GetEntryAssembly().GetName().Version.ToString();

        #endregion Private Methods

        #region Public Properties

        /// <summary>
        /// The test class property.
        /// </summary>
        public AppData Test { get; set; } = new AppData();

        [Option("-t|--testdata", "Shows test data (JSON).", CommandOptionType.NoValue)]
        public bool OptionT { get; set; }

        [Option("-i|--info", "Shows all property infos.", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-v|--value", "Shows all property values.", CommandOptionType.NoValue)]
        public bool OptionV { get; set; }

        [Option("-p|--property <string>", "Shows property data.", CommandOptionType.SingleValue)]
        public string Property { get; set; } = string.Empty;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Method to run when the root command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            if (!OptionI && !OptionT && !OptionV && (Property.Length == 0))
            {
                Console.WriteLine("");
                Console.WriteLine($"DataValue: {JsonConvert.SerializeObject(_settings.TestData, Formatting.Indented)}");
                Console.WriteLine("");
            }

            Test.Status = DataValue.Good;

            if (OptionT)
            {
                Console.WriteLine("");
                Console.WriteLine($"DataValue: {JsonConvert.SerializeObject(Test, Formatting.Indented)}");
                Console.WriteLine("");
            }

            if (Property.Length > 0)
            {
                Console.WriteLine("");

                if (IsProperty(Property))
                {
                    ShowPropertyInfo(Property);
                    ShowPropertyValue(Property);
                }
                else
                {
                    Console.WriteLine($"Property {Property} not found.");
                }

                Console.WriteLine("");
            }
            else
            {
                if (OptionI)
                {
                    Console.WriteLine("");
                    ShowPropertyInfo("Test.Value");
                    ShowPropertyInfo("Test.Name");
                    ShowPropertyInfo("Test.Info");
                    ShowPropertyInfo("Test.Data");
                    ShowPropertyInfo("Test.Data.Name");
                    ShowPropertyInfo("Test.Data.Value");
                    ShowPropertyInfo("Test.Data.Url");
                    ShowPropertyInfo("Test.Data.Code");
                    ShowPropertyInfo("Test.Data.Url.Address");
                    ShowPropertyInfo("Test.Data.Url.Address.Length");
                    ShowPropertyInfo("Test.Data.Url.Bytes");
                    ShowPropertyInfo("Test.Data.Url.Bytes.Length");
                    ShowPropertyInfo("Test.Data.Url.Operations");
                    ShowPropertyInfo("Test.Data.Url.Operations.Count");
                    ShowPropertyInfo("Test.Data.Url.Operations[].Length");
                    ShowPropertyInfo("Test.Array");
                    ShowPropertyInfo("Test.Array.Length");
                    ShowPropertyInfo("Test.Array[].Name");
                    ShowPropertyInfo("Test.Array[].Value");
                    ShowPropertyInfo("Test.Array[].Url");
                    ShowPropertyInfo("Test.Array[].Code");
                    ShowPropertyInfo("Test.Array[].Url.Address");
                    ShowPropertyInfo("Test.Array[].Url.Address.Length");
                    ShowPropertyInfo("Test.Array[].Url.Bytes");
                    ShowPropertyInfo("Test.Array[].Url.Bytes.Length");
                    ShowPropertyInfo("Test.Array[].Url.Operations");
                    ShowPropertyInfo("Test.Array[].Url.Operations.Count");
                    ShowPropertyInfo("Test.Array[].Url.Operations[].Length");
                    ShowPropertyInfo("Test.List");
                    ShowPropertyInfo("Test.List.Count");
                    ShowPropertyInfo("Test.List[].Name");
                    ShowPropertyInfo("Test.List[].Value");
                    ShowPropertyInfo("Test.List[].Url");
                    ShowPropertyInfo("Test.List[].Code");
                    ShowPropertyInfo("Test.List[].Url.Address");
                    ShowPropertyInfo("Test.List[].Url.Address.Length");
                    ShowPropertyInfo("Test.List[].Url.Bytes");
                    ShowPropertyInfo("Test.List[].Url.Bytes.Length");
                    ShowPropertyInfo("Test.List[].Url.Operations");
                    ShowPropertyInfo("Test.List[].Url.Operations.Count");
                    ShowPropertyInfo("Test.List[].Url.Operations[].Length");
                    ShowPropertyInfo("Test.Status");
                    ShowPropertyInfo("Test.Status.Code");
                    ShowPropertyInfo("Test.Status.Name");
                    ShowPropertyInfo("Test.Status.Explanation");
                    ShowPropertyInfo("Test.Timestamp");
                    ShowPropertyInfo("Test.IsGood");
                    ShowPropertyInfo("Test.IsBad");
                    ShowPropertyInfo("Test.IsUncertain");
                }

                if (OptionV)
                {
                    Console.WriteLine("");
                    ShowPropertyValue("Test.Value");
                    ShowPropertyValue("Test.Name");
                    ShowPropertyValue("Test.Info");
                    ShowPropertyValue("Test.Data");
                    ShowPropertyValue("Test.Data.Name");
                    ShowPropertyValue("Test.Data.Value");
                    ShowPropertyValue("Test.Data.Url");
                    ShowPropertyValue("Test.Data.Code");
                    ShowPropertyValue("Test.Data.Url.Address");
                    ShowPropertyValue("Test.Data.Url.Bytes");
                    ShowPropertyValue("Test.Data.Url.Operations");
                    ShowPropertyValue("Test.Data.Url.Address.Length");
                    ShowPropertyValue("Test.Data.Url.Bytes.Length");
                    ShowPropertyValue("Test.Data.Url.Bytes[0]");
                    ShowPropertyValue("Test.Data.Url.Bytes[1]");
                    ShowPropertyValue("Test.Data.Url.Bytes[2]");
                    ShowPropertyValue("Test.Data.Url.Bytes[3]");
                    ShowPropertyValue("Test.Data.Url.Operations.Count");
                    ShowPropertyValue("Test.Data.Url.Operations[0]");
                    ShowPropertyValue("Test.Data.Url.Operations[1]");
                    ShowPropertyValue("Test.Data.Url.Operations[2]");
                    ShowPropertyValue("Test.Data.Url.Operations[0].Length");
                    ShowPropertyValue("Test.Data.Url.Operations[1].Length");
                    ShowPropertyValue("Test.Data.Url.Operations[2].Length");
                    ShowPropertyValue("Test.Array");
                    ShowPropertyValue("Test.Array.Length");
                    ShowPropertyValue("Test.Array[0]");
                    ShowPropertyValue("Test.Array[0].Name");
                    ShowPropertyValue("Test.Array[0].Value");
                    ShowPropertyValue("Test.Array[0].Url");
                    ShowPropertyValue("Test.Array[0].Code");
                    ShowPropertyValue("Test.Array[0].Url.Address");
                    ShowPropertyValue("Test.Array[0].Url.Address.Length");
                    ShowPropertyValue("Test.Array[0].Url.Bytes");
                    ShowPropertyValue("Test.Array[0].Url.Bytes.Length");
                    ShowPropertyValue("Test.Array[0].Url.Bytes[0]");
                    ShowPropertyValue("Test.Array[0].Url.Bytes[1]");
                    ShowPropertyValue("Test.Array[0].Url.Bytes[2]");
                    ShowPropertyValue("Test.Array[0].Url.Bytes[3]");
                    ShowPropertyValue("Test.Array[0].Url.Operations");
                    ShowPropertyValue("Test.Array[0].Url.Operations.Count");
                    ShowPropertyValue("Test.Array[0].Url.Operations[0]");
                    ShowPropertyValue("Test.Array[0].Url.Operations[1]");
                    ShowPropertyValue("Test.Array[0].Url.Operations[2]");
                    ShowPropertyValue("Test.Array[0].Url.Operations[0].Length");
                    ShowPropertyValue("Test.Array[0].Url.Operations[1].Length");
                    ShowPropertyValue("Test.Array[0].Url.Operations[2].Length");
                    ShowPropertyValue("Test.Array[1]");
                    ShowPropertyValue("Test.Array[1].Name");
                    ShowPropertyValue("Test.Array[1].Value");
                    ShowPropertyValue("Test.Array[1].Url");
                    ShowPropertyValue("Test.Array[1].Code");
                    ShowPropertyValue("Test.Array[1].Url.Address");
                    ShowPropertyValue("Test.Array[1].Url.Address.Length");
                    ShowPropertyValue("Test.Array[1].Url.Bytes");
                    ShowPropertyValue("Test.Array[1].Url.Bytes.Length");
                    ShowPropertyValue("Test.Array[1].Url.Bytes[0]");
                    ShowPropertyValue("Test.Array[1].Url.Bytes[1]");
                    ShowPropertyValue("Test.Array[1].Url.Bytes[2]");
                    ShowPropertyValue("Test.Array[1].Url.Bytes[3]");
                    ShowPropertyValue("Test.Array[1].Url.Operations");
                    ShowPropertyValue("Test.Array[1].Url.Operations.Count");
                    ShowPropertyValue("Test.Array[1].Url.Operations[0]");
                    ShowPropertyValue("Test.Array[1].Url.Operations[1]");
                    ShowPropertyValue("Test.Array[1].Url.Operations[2]");
                    ShowPropertyValue("Test.Array[1].Url.Operations[0].Length");
                    ShowPropertyValue("Test.Array[1].Url.Operations[1].Length");
                    ShowPropertyValue("Test.Array[1].Url.Operations[2].Length");
                    ShowPropertyValue("Test.List");
                    ShowPropertyValue("Test.List.Count");
                    ShowPropertyValue("Test.List[0]");
                    ShowPropertyValue("Test.List[0].Name");
                    ShowPropertyValue("Test.List[0].Value");
                    ShowPropertyValue("Test.List[0].Url");
                    ShowPropertyValue("Test.List[0].Code");
                    ShowPropertyValue("Test.List[0].Url.Address");
                    ShowPropertyValue("Test.List[0].Url.Address.Length");
                    ShowPropertyValue("Test.List[0].Url.Bytes");
                    ShowPropertyValue("Test.List[0].Url.Bytes.Length");
                    ShowPropertyValue("Test.List[0].Url.Bytes[0]");
                    ShowPropertyValue("Test.List[0].Url.Bytes[1]");
                    ShowPropertyValue("Test.List[0].Url.Bytes[2]");
                    ShowPropertyValue("Test.List[0].Url.Bytes[3]");
                    ShowPropertyValue("Test.List[0].Url.Operations");
                    ShowPropertyValue("Test.List[0].Url.Operations.Count");
                    ShowPropertyValue("Test.List[0].Url.Operations[0]");
                    ShowPropertyValue("Test.List[0].Url.Operations[1]");
                    ShowPropertyValue("Test.List[0].Url.Operations[2]");
                    ShowPropertyValue("Test.List[0].Url.Operations[0].Length");
                    ShowPropertyValue("Test.List[0].Url.Operations[1].Length");
                    ShowPropertyValue("Test.List[0].Url.Operations[2].Length");
                    ShowPropertyValue("Test.List[1]");
                    ShowPropertyValue("Test.List[1].Name");
                    ShowPropertyValue("Test.List[1].Value");
                    ShowPropertyValue("Test.List[1].Url");
                    ShowPropertyValue("Test.List[1].Code");
                    ShowPropertyValue("Test.List[1].Url.Address");
                    ShowPropertyValue("Test.List[1].Url.Address.Length");
                    ShowPropertyValue("Test.List[1].Url.Bytes");
                    ShowPropertyValue("Test.List[1].Url.Bytes.Length");
                    ShowPropertyValue("Test.List[1].Url.Bytes[0]");
                    ShowPropertyValue("Test.List[1].Url.Bytes[1]");
                    ShowPropertyValue("Test.List[1].Url.Bytes[2]");
                    ShowPropertyValue("Test.List[1].Url.Bytes[3]");
                    ShowPropertyValue("Test.List[1].Url.Operations");
                    ShowPropertyValue("Test.List[1].Url.Operations.Count");
                    ShowPropertyValue("Test.List[1].Url.Operations[0]");
                    ShowPropertyValue("Test.List[1].Url.Operations[1]");
                    ShowPropertyValue("Test.List[1].Url.Operations[2]");
                    ShowPropertyValue("Test.List[1].Url.Operations[0].Length");
                    ShowPropertyValue("Test.List[1].Url.Operations[1].Length");
                    ShowPropertyValue("Test.List[1].Url.Operations[2].Length");
                    ShowPropertyValue("Test.Status");
                    ShowPropertyValue("Test.Status.Code");
                    ShowPropertyValue("Test.Status.Name");
                    ShowPropertyValue("Test.Status.Explanation");
                    ShowPropertyValue("Test.Timestamp");
                    ShowPropertyValue("Test.IsGood");
                    ShowPropertyValue("Test.IsBad");
                    ShowPropertyValue("Test.IsUncertain");
                    Console.WriteLine("");
                }
            }

            return Task.FromResult(0);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Method to print selected property infos.
        /// </summary>
        /// <param name="property">The property name.</param>
        private static void ShowPropertyInfo(string property)
        {
            var info = GetPropertyInfo(property);

            Console.WriteLine($"Property:          {property}");
            Console.WriteLine($"    MemberType     {info?.PropertyType.MemberType}");
            Console.WriteLine($"    PropertyType   {info?.PropertyType}");
            Console.WriteLine($"    CanRead        {info?.CanRead}");
            Console.WriteLine($"    CanWrite       {info?.CanWrite}");
            Console.WriteLine($"    HasElementType {info?.PropertyType.HasElementType}");
            Console.WriteLine($"    IsClass        {info?.PropertyType.IsClass}");
            Console.WriteLine($"    IsEnum         {info?.PropertyType.IsEnum}");
            Console.WriteLine($"    IsArray        {info?.PropertyType.IsArray}");
            Console.WriteLine("");
        }

        /// <summary>
        /// Method to print the property value.
        /// </summary>
        /// <param name="property">The property name.</param>
        private void ShowPropertyValue(string property)
        {
            var value = GetPropertyValue(property);
            Console.WriteLine($"Property: {property} => {value}");
        }

        #endregion Private Methods

        #region Helper Methods

        public static bool IsProperty(string property) => (PropertyValue.GetPropertyInfo(typeof(RootCommand), property) != null) ? true : false;

        public static System.Reflection.PropertyInfo GetPropertyInfo(string property) => PropertyValue.GetPropertyInfo(typeof(RootCommand), property);

        public object GetPropertyValue(string property) => PropertyValue.GetPropertyValue(this, property);

        public void SetPropertyValue(string property, object value) => PropertyValue.SetPropertyValue(this, property, value);

        #endregion Helper Methods
    }
}