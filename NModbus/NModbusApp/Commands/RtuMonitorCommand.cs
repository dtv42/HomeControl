// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RtuMonitorCommand.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusApp.Commands
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;
    using Newtonsoft.Json;
    using NModbus.Extensions;

    using BaseClassLib;
    using NModbusLib;

    #endregion

    [Command(Name = "monitor",
             Description = "Supporting Modbus RTU monitor operations.",
             ExtendedHelpText = "Please specify the monitor option (coils, discrete inputs, holding registers, or input registers).")]
    [HelpOption("-?|--help")]
    public class RtuMonitorCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        private readonly IRtuClient _client;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RtuCommand"/>.
        /// </summary>
        private RtuCommand Parent { get; set; }

        /// <summary>
        /// Parent Command options.
        /// </summary>
        private bool OptionSettings { get; set; }

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionN { get; set; }
        private bool OptionO { get; set; }
        private bool OptionT { get; set; }
        private bool OptionR { get; set; }
        private bool OptionS { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionC || OptionD || OptionH || OptionI); }

        #endregion

        #region Public Properties

        [Option("-c|--coil", "Reads coil(s).", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-d|--discrete", "Reads discrete input(s).", CommandOptionType.NoValue)]
        public bool OptionD { get; set; }

        [Option("-h|--holding", "Reads holding register(s).", CommandOptionType.NoValue)]
        public bool OptionH { get; set; }

        [Option("-i|--input", "Reads input register(s).", CommandOptionType.NoValue)]
        public bool OptionI { get; set; }

        [Option("-x|--hex", "Displays the register values in HEX.", CommandOptionType.NoValue)]
        public bool OptionX { get; set; }

        [Option("-n|--number <number>", "The number of items to read.", CommandOptionType.SingleValue)]
        public ushort Number { get; set; } = 1;

        [Option("-o|--offset <number>", "The offset of the first item to read.", CommandOptionType.SingleValue)]
        public ushort Offset { get; set; }

        [Option("-t|--type <string>", "Reads the specified data type", CommandOptionType.SingleValue)]
        public string Type { get; set; } = string.Empty;

        [Option("-r|--repeat <number>", "The number of times to read (default: forever).", CommandOptionType.SingleValue)]
        public uint Repeat { get; set; } = 0;

        [Option("-s|--seconds <number>", "The seconds between times to read (default: 10).", CommandOptionType.SingleValue)]
        public uint Seconds { get; set; } = 10;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RtuMonitorCommand"/> class.
        /// Selected properties are initialized with data from the AppSettings instance.
        /// </summary>
        /// <param name="client">The Modbus client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public RtuMonitorCommand(IRtuClient client,
                                 ILogger<RtuMonitorCommand> logger,
                                 IOptions<AppSettings> options,
                                 IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger.LogDebug("RtuMonitorCommand()");

            // Setting the RTU client instance.
            _client = client;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is called processing the tcp read command.
        /// </summary>
        /// <returns></returns>
        public async Task<int> OnExecute(CommandLineApplication app)
        {
            _logger.LogDebug("OnExecute()");

            try
            {
                if (CheckOptions(app))
                {
                    // Overriding TCP client options.
                    OptionSettings = Parent.Parent.OptionSettings;
                    _client.RtuMaster.SerialPort = Parent.SerialPort;
                    _client.RtuMaster.Baudrate = Parent.Baudrate;
                    _client.RtuMaster.Parity = Parent.Parity;
                    _client.RtuMaster.DataBits = Parent.DataBits;
                    _client.RtuMaster.StopBits = Parent.StopBits;
                    _client.RtuMaster.ReadTimeout = Parent.ReadTimeout;
                    _client.RtuMaster.WriteTimeout = Parent.WriteTimeout;
                    _client.RtuSlave.ID = Parent.SlaveID;

                    if (OptionSettings)
                    {
                        Console.WriteLine($"RtuMaster: {JsonConvert.SerializeObject(_settings.RtuMaster, Formatting.Indented)}");
                        Console.WriteLine($"RtuSlave: {JsonConvert.SerializeObject(_settings.RtuSlave, Formatting.Indented)}");
                    }

                    if (_client.Connect())
                    {
                        try
                        {
                            var source = new CancellationTokenSource();
                            var cancellationToken = source.Token;
                            bool forever = (Repeat == 0);
                            bool verbose = true;

                            await Task.Factory.StartNew(async () => {
                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    try
                                    {
                                        // Read the specified data.
                                        var start = DateTime.UtcNow;
                                        ReadingData(verbose);
                                        // Only first call is verbose.
                                        verbose = false;
                                        var end = DateTime.UtcNow;
                                        double delay = ((Seconds * 1000.0) - (end - start).TotalMilliseconds) / 1000.0;

                                        if (Seconds > 0)
                                        {
                                            if (delay < 0)
                                            {
                                                _logger?.LogWarning("Monitoring: no time between reads.");
                                            }
                                            else
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger?.LogWarning(ex, "Monitoring: Exception");
                                    }

                                    if (!forever && (--Number <= 0))
                                    {
                                        _closing.Set();
                                    }
                                }

                            }, cancellationToken);

                            Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, args) =>
                            {
                                Console.WriteLine($"Monitoring cancelled.");
                                _closing.Set();
                            });

                            _closing.WaitOne();
                        }
                        catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
                        {
                            Console.WriteLine($"Monitoring cancelled.");
                        }
                        catch (OperationCanceledException)
                        {
                            Console.WriteLine($"Monitoring cancelled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Modbus RTU slave not found at {Parent.SerialPort}.");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RtuReadCommand - {ex.Message}");
                return 0;
            }
            finally
            {
                if (_client.Connected)
                {
                    _client.Disconnect();
                }
            }

            return 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reading the specified data.
        /// </summary>
        private void ReadingData(bool verbose = false)
        {
            _logger?.LogDebug("TcpMonitor: Reading data...");

            // Reading coils.
            if (OptionC)
            {
                if (Number == 1)
                {
                    Console.WriteLine(verbose ? $"Monitoring a single coil[{Offset}]" : "");
                    bool[] values = _client.ReadCoils(Offset, Number);
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Value of coil[{Offset}] = {values[0]}");
                }
                else
                {
                    Console.WriteLine(verbose ? $"Monitoring {Number} coils starting at {Offset}" : "");
                    bool[] values = _client.ReadCoils(Offset, Number);

                    for (int index = 0; index < values.Length; ++index)
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of coil[{index}] = {values[index]}");
                    }
                }
            }

            // Reading discrete inputs.
            if (OptionD)
            {
                if (Number == 1)
                {
                    Console.WriteLine(verbose ? $"Monitoring a discrete input[{Offset}]" : "");
                    bool[] values = _client.ReadInputs(Offset, Number);
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of discrete input[{Offset}] = {values[0]}");
                }
                else
                {
                    Console.WriteLine(verbose ? $"Monitoring {Number} discrete inputs starting at {Offset}" : "");
                    bool[] values = _client.ReadInputs(Offset, Number);

                    for (int index = 0; index < values.Length; ++index)
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of discrete input[{index}] = {values[index]}");
                    }
                }
            }

            // Reading holding registers.
            if (OptionH)
            {
                if (string.Equals(Type, "string", StringComparison.OrdinalIgnoreCase))
                {
                    if (OptionX)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a HEX string from offset = {Offset}" : "");
                        string value = _client.ReadHexString(Offset, Number);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of HEX string = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring an ASCII string from offset = {Offset}" : "");
                        string value = _client.ReadString(Offset, Number);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of ASCII string = {value}");
                    }
                }
                else if (string.Equals(Type, "bits", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(verbose ? $"Monitoring a 16 bit array from offset = {Offset}" : "");
                    BitArray value = _client.ReadBits(Offset);
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of 16 bit array = {value.ToDigitString()}");
                }
                else if (string.Equals(Type, "byte", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single byte from offset = {Offset}" : "");
                        byte[] values = _client.ReadBytes(Offset, Number);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single byte = {values[0]}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} bytes from offset = {Offset}" : "");
                        byte[] values = _client.ReadBytes(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of byte array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "short", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single short from offset = {Offset}" : "");
                        short value = _client.ReadShort(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single short = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} shorts from offset = {Offset}" : "");
                        short[] values = _client.ReadShortArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of short array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "ushort", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single ushort from offset = {Offset}" : "");
                        ushort value = _client.ReadUShort(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single ushort = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} ushorts from offset = {Offset}" : "");
                        ushort[] values = _client.ReadUShortArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of ushort array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "int", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single integer from offset = {Offset}" : "");
                        Int32 value = _client.ReadInt32(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single integer = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number}  integers from offset = {Offset}" : "");
                        Int32[] values = _client.ReadInt32Array(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of integer array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "uint", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single unsigned integer from offset = {Offset}" : "");
                        UInt32 value = _client.ReadUInt32(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single unsigned integer = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} unsigned integers from offset = {Offset}" : "");
                        UInt32[] values = _client.ReadUInt32Array(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of unsigned integer array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "float", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single float from offset = {Offset}" : "");
                        float value = _client.ReadFloat(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single float = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} floats from offset = {Offset}" : "");
                        float[] values = _client.ReadFloatArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of float array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "double", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single double from offset = {Offset}" : "");
                        double value = _client.ReadDouble(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single double = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} doubles from offset = {Offset}" : "");
                        double[] values = _client.ReadDoubleArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of double array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "long", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single long from offset = {Offset}" : "");
                        long value = _client.ReadLong(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single long = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} longs from offset = {Offset}" : "");
                        long[] values = _client.ReadLongArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of long array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "ulong", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single ulong from offset = {Offset}" : "");
                        ulong value = _client.ReadULong(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single ulong = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} ulongs from offset = {Offset}" : "");
                        ulong[] values = _client.ReadULongArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of ulong array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (Number == 1)
                {
                    Console.WriteLine(verbose ? $"Monitoring a holding register[{Offset}]" : "");
                    ushort[] values = _client.ReadHoldingRegisters(Offset, Number);
                    if (OptionX) Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of holding register[{Offset}] = {values[0]:X2}");
                    else Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of holding register[{Offset}] = {values[0]}");
                }
                else
                {
                    Console.WriteLine(verbose ? $"Monitoring {Number} holding registers starting at {Offset}" : "");
                    ushort[] values = _client.ReadHoldingRegisters(Offset, Number);

                    for (int index = 0; index < values.Length; ++index)
                    {
                        if (OptionX) Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of holding register[{index}] = {values[index]:X2}");
                        else Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of holding register[{index}] = {values[index]}");
                    }
                }
            }

            // Reading input registers.
            if (OptionI)
            {
                if (string.Equals(Type, "string", StringComparison.OrdinalIgnoreCase))
                {
                    if (OptionX)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a HEX string from offset = {Offset}" : "");
                        string value = _client.ReadOnlyHexString(Offset, Number);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of HEX string = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring an ASCII string from offset = {Offset}" : "");
                        string value = _client.ReadOnlyString(Offset, Number);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of ASCII string = {value}");
                    }
                }
                else if (string.Equals(Type, "bits", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(verbose ? $"Monitoring a 16 bit array from offset = {Offset}" : "");
                    BitArray value = _client.ReadOnlyBits(Offset);
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of 16 bit array = {value.ToDigitString()}");
                }
                else if (string.Equals(Type, "byte", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single byte from offset = {Offset}" : "");
                        byte[] values = _client.ReadOnlyBytes(Offset, Number);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single byte = {values[0]}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} bytes from offset = {Offset}" : "");
                        byte[] values = _client.ReadOnlyBytes(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of byte array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "short", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single short from offset = {Offset}" : "");
                        short value = _client.ReadOnlyShort(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single short = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} short values from offset = {Offset}" : "");
                        short[] values = _client.ReadOnlyShortArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of short array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "ushort", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single ushort from offset = {Offset}" : "");
                        ushort value = _client.ReadOnlyUShort(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single ushort = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} ushort values from offset = {Offset}" : "");
                        ushort[] values = _client.ReadOnlyUShortArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of ushort array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "int", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single int from offset = {Offset}" : "");
                        Int32 value = _client.ReadOnlyInt32(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single integer = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} int values from offset = {Offset}" : "");
                        Int32[] values = _client.ReadOnlyInt32Array(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of int array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "uint", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single unsigned int from offset = {Offset}" : "");
                        UInt32 value = _client.ReadOnlyUInt32(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single unsigned int = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} unsigned int values from offset = {Offset}" : "");
                        UInt32[] values = _client.ReadOnlyUInt32Array(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of unsigned int array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "float", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single float from offset = {Offset}" : "");
                        float value = _client.ReadOnlyFloat(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single float = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} float values from offset = {Offset}" : "");
                        float[] values = _client.ReadOnlyFloatArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of float array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "double", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single double from offset = {Offset}" : "");
                        double value = _client.ReadOnlyDouble(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single double = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} double values from offset = {Offset}" : "");
                        double[] values = _client.ReadOnlyDoubleArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of double array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "long", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single long from offset = {Offset}" : "");
                        long value = _client.ReadOnlyLong(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single long = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} long values from offset = {Offset}" : "");
                        long[] values = _client.ReadOnlyLongArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of long array[{index}] = {values[index]}");
                        }
                    }
                }
                else if (string.Equals(Type, "ulong", StringComparison.OrdinalIgnoreCase))
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a single unsigned long from offset = {Offset}" : "");
                        ulong value = _client.ReadOnlyULong(Offset);
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of single ulong = {value}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} unsigned long values from offset = {Offset}" : "");
                        ulong[] values = _client.ReadOnlyULongArray(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of ulong array[{index}] = {values[index]}");
                        }
                    }
                }
                else
                {
                    if (Number == 1)
                    {
                        Console.WriteLine(verbose ? $"Monitoring a input register[{Offset}]" : "");
                        ushort[] values = _client.ReadInputRegisters(Offset, Number);
                        if (OptionX) Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of input register[{Offset}] = {values[0]:X2}");
                        else Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of input register[{Offset}] = {values[0]}");
                    }
                    else
                    {
                        Console.WriteLine(verbose ? $"Monitoring {Number} input registers starting at {Offset}" : "");
                        ushort[] values = _client.ReadInputRegisters(Offset, Number);

                        for (int index = 0; index < values.Length; ++index)
                        {
                            if (OptionX) Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of input register[{index}] = {values[index]:X2}");
                            else Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: Value of input register[{index}] = {values[index]}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            // Getting additional option flags.
            var options = app.GetOptions().ToList();

            foreach (var option in options)
            {
                switch (option.LongName)
                {
                    case "settings": OptionSettings = option.HasValue(); break;
                    case "number": OptionN = option.HasValue(); break;
                    case "offset": OptionO = option.HasValue(); break;
                    case "type": OptionT = option.HasValue(); break;
                    case "repeat": OptionR = option.HasValue(); break;
                    case "seconds": OptionS = option.HasValue(); break;
                }
            }

            if (!OptionC && !OptionD && !OptionH && !OptionI)
            {
                Console.WriteLine($"Specify the read option (coils, discrete inputs, holding registers, input registers).");
                return false;
            }

            if ((OptionC && (OptionD || OptionH || OptionI)) ||
                (OptionD && (OptionC || OptionH || OptionI)) ||
                (OptionH && (OptionD || OptionC || OptionI)) ||
                (OptionI && (OptionD || OptionH || OptionC)))
            {
                Console.WriteLine($"Specify only a single type (coils, discrete inputs, holding registers, input register).");
                return false;
            }

            if ((OptionC || OptionD) && OptionX)
            {
                Console.WriteLine($"HEX output option is ignored.");
            }

            if (OptionT)
            {
                if (OptionI || OptionH)
                {
                    switch (Type.ToLower(CultureInfo.CurrentCulture))
                    {
                        case "bits":
                            if (Number > 1)
                            {
                                Console.WriteLine($"Only a single bit array value is supported (Number == 1).");
                                Number = 1;
                            }

                            return true;
                        case "string":
                            return true;
                        case "byte":
                        case "short":
                        case "ushort":
                        case "int":
                        case "uint":
                        case "float":
                        case "double":
                        case "long":
                        case "ulong":
                            if (OptionX)
                            {
                                Console.WriteLine($"HEX output option is ignored.");
                            }

                            return true;
                        default:
                            Console.WriteLine($"Unsupported data type '{Type}'.");
                            return false;
                    }
                }
                else
                {
                    Console.WriteLine($"Specified type '{Type}' is ignored.");
                }
            }

            return true;
        }

        #endregion
    }
}
