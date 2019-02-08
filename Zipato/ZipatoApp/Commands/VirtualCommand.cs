// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualCommand.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoApp.Commands
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoApp.Models;
    using ZipatoLib.Models.Sensors;

    #endregion

    /// <summary>
    /// Application command "virtual".
    /// </summary>
    [Command(Name = "virtual",
             FullName = "Zipato Virtual Meter Command",
             Description = "Virtual Meter device access from Zipato home control.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class VirtualCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IZipato _zipato;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="SensorsCommand"/>.
        /// </summary>
        private SensorsCommand Parent { get; set; }

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionIndex { get; set; }
        private bool OptionName { get; set; }
        private bool OptionUuid { get; set; }

        private bool OptionA { get; set; }
        private bool OptionV { get; set; }
        private bool OptionV1 { get; set; }
        private bool OptionV2 { get; set; }
        private bool OptionV3 { get; set; }
        private bool OptionV4 { get; set; }
        private bool OptionV5 { get; set; }
        private bool OptionV6 { get; set; }
        private bool OptionV7 { get; set; }
        private bool OptionV8 { get; set; }
        private bool OptionV9 { get; set; }
        private bool OptionV10 { get; set; }
        private bool OptionV11 { get; set; }
        private bool OptionV12 { get; set; }
        private bool OptionV13 { get; set; }
        private bool OptionV14 { get; set; }
        private bool OptionV15 { get; set; }
        private bool OptionV16 { get; set; }

        /// <summary>
        /// Returns true if no parent option is selected.
        /// </summary>
        private bool NoParentOptions { get => !(OptionIndex || OptionName || OptionUuid); }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionA || OptionV ||
                                          OptionV1 || OptionV2 || OptionV3 || OptionV4 ||
                                          OptionV5 || OptionV6 || OptionV7 || OptionV8 ||
                                          OptionV9 || OptionV10 || OptionV11 || OptionV12 ||
                                          OptionV13 || OptionV14 || OptionV15 || OptionV16); }

        #endregion

        #region Public Properties

        [Option("-v|--value <number>", "The attribute value.", CommandOptionType.SingleValue)]
        public double Value { get; set; }

        [Option("-a|--aindex <number>", "The attribute index (0..15).", CommandOptionType.SingleValue)]
        public int Index { get; set; }

        [Option("-v1|--value1 <number>", "The value at attribute[0].", CommandOptionType.SingleValue)]
        public double Value1 { get; set; }

        [Option("-v2|--value2 <number>", "The value at attribute[1].", CommandOptionType.SingleValue)]
        public double Value2 { get; set; }

        [Option("-v3|--value3 <number>", "The value at attribute[2].", CommandOptionType.SingleValue)]
        public double Value3 { get; set; }

        [Option("-v4|--value4 <number>", "The value at attribute[3].", CommandOptionType.SingleValue)]
        public double Value4 { get; set; }

        [Option("-v5|--value5 <number>", "The value at attribute[4].", CommandOptionType.SingleValue)]
        public double Value5 { get; set; }

        [Option("-v6|--value6 <number>", "The value at attribute[5].", CommandOptionType.SingleValue)]
        public double Value6 { get; set; }

        [Option("-v7|--value7 <number>", "The value at attribute[6].", CommandOptionType.SingleValue)]
        public double Value7 { get; set; }

        [Option("-v8|--value8 <number>", "The value at attribute[7].", CommandOptionType.SingleValue)]
        public double Value8 { get; set; }

        [Option("-v9|--value9 <number>", "The value at attribute[8].", CommandOptionType.SingleValue)]
        public double Value9 { get; set; }

        [Option("-v10|--value10 <number>", "The value at attribute[9].", CommandOptionType.SingleValue)]
        public double Value10 { get; set; }

        [Option("-v11|--value11 <number>", "The value at attribute[10].", CommandOptionType.SingleValue)]
        public double Value11 { get; set; }

        [Option("-v12|--value12 <number>", "The value at attribute[11].", CommandOptionType.SingleValue)]
        public double Value12 { get; set; }

        [Option("-v13|--value13 <number>", "The value at attribute[12].", CommandOptionType.SingleValue)]
        public double Value13 { get; set; }

        [Option("-v14|--value14 <number>", "The value at attribute[13].", CommandOptionType.SingleValue)]
        public double Value14 { get; set; }

        [Option("-v15|--value15 <number>", "The value at attribute[14].", CommandOptionType.SingleValue)]
        public double Value15 { get; set; }

        [Option("-v16|--value16 <number>", "The value at attribute[15].", CommandOptionType.SingleValue)]
        public double Value16 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualCommand"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public VirtualCommand(IZipato zipato,
                              ILogger<MeterCommand> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("VirtualCommand()");

            // Setting the Zipato instance.
            _zipato = zipato;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public async Task<int> OnExecute(CommandLineApplication app)
        {
            if (CheckOptions(app))
            {
                try
                {
                    // Overriding Zipato options.
                    _zipato.BaseAddress = Parent.Parent.BaseAddress;
                    _zipato.Timeout = Parent.Parent.Timeout;
                    _zipato.User = Parent.Parent.User;
                    _zipato.Password = Parent.Parent.Password;
                    _zipato.IsLocal = Parent.Parent.IsLocal;
                    _zipato.StartSession();

                    if (!_zipato.IsSessionActive)
                    {
                        Console.WriteLine($"Cannot establish a communcation session.");
                        return 0;
                    }

                    await _zipato.ReadAllDataAsync();

                    VirtualMeter meter = null;

                    if (NoParentOptions)
                    {
                        Console.WriteLine($"Virtual Meter: {JsonConvert.SerializeObject(_zipato.Sensors.VirtualMeters, Formatting.Indented)}");
                        return 0;
                    }
                    else if (OptionIndex)
                    {
                        var index = Parent.Index;

                        if ((index >= 0) && (index < _zipato.Sensors.VirtualMeters.Count))
                        {
                            meter = _zipato.Sensors.VirtualMeters[index];

                            if (meter == null)
                            {
                                Console.WriteLine($"Virtual Meter with index '{index}' not found.");
                                return -1;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Virtual Meter index '{index}' out of bounds (0 - {_zipato.Sensors.VirtualMeters.Count - 1}).");
                            return -1;
                        }
                    }
                    else if (OptionName)
                    {
                        var name = Parent.Name;
                        meter = _zipato.Sensors.VirtualMeters.FirstOrDefault(d => d.Name == name);

                        if (meter == null)
                        {
                            Console.WriteLine($"Virtual Meter with name '{name}' not found.");
                            return -1;
                        }
                    }
                    else if (OptionUuid)
                    {
                        var uuid = new Guid(Parent.Uuid);
                        meter = _zipato.Sensors.VirtualMeters.FirstOrDefault(d => d.Uuid == uuid);

                        if (meter == null)
                        {
                            Console.WriteLine($"Virtual Meter with UUID '{uuid}' not found.");
                            return -1;
                        }
                    }

                    if (OptionV && OptionA)
                    {
                        Console.WriteLine($"Virtual Meter[{Index}] set value {(meter.SetValue(Index, Value) ? "OK" : "not OK")}");
                    }

                    if (OptionV1)
                    {
                        Console.WriteLine($"Virtual Meter set value1 {(meter.SetValue1(Value1) ? "OK" : "not OK")}");
                    }

                    if (OptionV2)
                    {
                        Console.WriteLine($"Virtual Meter set value2 {(meter.SetValue2(Value2) ? "OK" : "not OK")}");
                    }

                    if (OptionV3)
                    {
                        Console.WriteLine($"Virtual Meter set value3 {(meter.SetValue3(Value3) ? "OK" : "not OK")}");
                    }

                    if (OptionV4)
                    {
                        Console.WriteLine($"Virtual Meter set value4 {(meter.SetValue4(Value4) ? "OK" : "not OK")}");
                    }

                    if (OptionV5)
                    {
                        Console.WriteLine($"Virtual Meter set value5 {(meter.SetValue5(Value5) ? "OK" : "not OK")}");
                    }

                    if (OptionV6)
                    {
                        Console.WriteLine($"Virtual Meter set value6 {(meter.SetValue6(Value6) ? "OK" : "not OK")}");
                    }

                    if (OptionV7)
                    {
                        Console.WriteLine($"Virtual Meter set value7 {(meter.SetValue7(Value7) ? "OK" : "not OK")}");
                    }

                    if (OptionV8)
                    {
                        Console.WriteLine($"Virtual Meter set value8 {(meter.SetValue8(Value8) ? "OK" : "not OK")}");
                    }

                    if (OptionV9)
                    {
                        Console.WriteLine($"Virtual Meter set value9 {(meter.SetValue9(Value9) ? "OK" : "not OK")}");
                    }

                    if (OptionV10)
                    {
                        Console.WriteLine($"Virtual Meter set value10 {(meter.SetValue10(Value10) ? "OK" : "not OK")}");
                    }

                    if (OptionV11)
                    {
                        Console.WriteLine($"Virtual Meter set value11 {(meter.SetValue11(Value11) ? "OK" : "not OK")}");
                    }

                    if (OptionV12)
                    {
                        Console.WriteLine($"Virtual Meter set value12 {(meter.SetValue12(Value12) ? "OK" : "not OK")}");
                    }

                    if (OptionV13)
                    {
                        Console.WriteLine($"Virtual Meter set value13 {(meter.SetValue13(Value13) ? "OK" : "not OK")}");
                    }

                    if (OptionV14)
                    {
                        Console.WriteLine($"Virtual Meter set value14 {(meter.SetValue14(Value14) ? "OK" : "not OK")}");
                    }

                    if (OptionV15)
                    {
                        Console.WriteLine($"Virtual Meter set value15 {(meter.SetValue15(Value15) ? "OK" : "not OK")}");
                    }

                    if (OptionV16)
                    {
                        Console.WriteLine($"Virtual Meter set value16 {(meter.SetValue16(Value16) ? "OK" : "not OK")}");
                    }

                    Console.WriteLine($"Virtual Meter: {JsonConvert.SerializeObject(meter, Formatting.Indented)}");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception VirtualCommand.");
                    return -1;
                }
                finally
                {
                    _zipato.EndSession();
                }
            }

            return 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                switch (option.LongName)
                {
                    case "index": OptionIndex = option.HasValue(); break;
                    case "name": OptionName = option.HasValue(); break;
                    case "uuid": OptionUuid = option.HasValue(); break;

                    case "aindex": OptionA = option.HasValue(); break;
                    case "value": OptionV = option.HasValue(); break;
                    case "value1": OptionV1 = option.HasValue(); break;
                    case "value2": OptionV2 = option.HasValue(); break;
                    case "value3": OptionV3 = option.HasValue(); break;
                    case "value4": OptionV4 = option.HasValue(); break;
                    case "value5": OptionV5 = option.HasValue(); break;
                    case "value6": OptionV6 = option.HasValue(); break;
                    case "value7": OptionV7 = option.HasValue(); break;
                    case "value8": OptionV8 = option.HasValue(); break;
                    case "value9": OptionV9 = option.HasValue(); break;
                    case "value10": OptionV10 = option.HasValue(); break;
                    case "value11": OptionV11 = option.HasValue(); break;
                    case "value12": OptionV12 = option.HasValue(); break;
                    case "value13": OptionV13 = option.HasValue(); break;
                    case "value14": OptionV14 = option.HasValue(); break;
                    case "value15": OptionV15 = option.HasValue(); break;
                    case "value16": OptionV16 = option.HasValue(); break;
                }
            }

            if ((OptionIndex && (OptionName || OptionUuid)) ||
                (OptionName && (OptionUuid || OptionIndex)) ||
                (OptionUuid && (OptionIndex || OptionName)))
            {
                Console.WriteLine("Select only a single option from '--index', '--name', and '--uuid'.");
                return false;
            }

            if ((OptionV && !OptionA) || (OptionA && !OptionV))
            {
                Console.WriteLine("Select option '-a' and '-v' together.");
                return false;
            }

            if ((OptionV && OptionA) && (OptionV1 || OptionV2 || OptionV3 || OptionV4 ||
                                         OptionV5 || OptionV6 || OptionV7 || OptionV8 ||
                                         OptionV9 || OptionV10 || OptionV11 || OptionV12 ||
                                         OptionV13 || OptionV14 || OptionV15 || OptionV16))
            {
                Console.WriteLine("Select option '-a' and '-v' or the other options (v1..v16).");
                return false;
            }

            if ((Index < 0) || (Index > 15))
            {
                Console.WriteLine($"Index (option -a) '{Index}' is out of range (0..15).");
                return false;
            }

            return true;
        }

        #endregion
    }
}
