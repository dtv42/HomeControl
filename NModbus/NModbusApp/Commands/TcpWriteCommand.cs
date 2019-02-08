// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpWriteCommand.cs" company="DTV-Online">
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;
    using NModbus.Extensions;

    using BaseClassLib;
    using NModbusLib;

    #endregion

    [Command(Name = "write",
             Description = "Supporting Modbus TCP write operations.",
             ExtendedHelpText = "Please specify the write option (coils or holding registers).")]
    [HelpOption("-?|--help")]
    public class TcpWriteCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly ITcpClient _client;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="TcpCommand"/>.
        /// </summary>
        private TcpCommand Parent { get; set; }

        /// <summary>
        /// Parent Command options.
        /// </summary>
        private bool OptionSettings { get; set; }

        /// <summary>
        /// Command options.
        /// </summary>
        private bool OptionO { get; set; }
        private bool OptionT { get; set; }

        /// <summary>
        /// Returns true if no option is selected.
        /// </summary>
        private bool NoOptions { get => !(OptionC || OptionH); }

        #endregion

        #region Public Properties

        [Required]
        [Argument(0, "Data values (JSON array format).")]
        public string Values { get; set; } = "[]";

        [Option("-c|--coil", "Writes coil(s).", CommandOptionType.NoValue)]
        public bool OptionC { get; set; }

        [Option("-h|--holding", "Writes holding register(s).", CommandOptionType.NoValue)]
        public bool OptionH { get; set; }

        [Option("-o|--offset <number>", "The offset of the first item to write.", CommandOptionType.SingleValue)]
        public ushort Offset { get; set; }

        [Option("-t|--type <string>", "Writes the specified data type", CommandOptionType.SingleValue)]
        public string Type { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpWriteCommand"/> class.
        /// Selected properties are initialized with data from the AppSettings instance.
        /// </summary>
        /// <param name="client">The Modbus client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public TcpWriteCommand(ITcpClient client,
                               ILogger<TcpWriteCommand> logger,
                               IOptions<AppSettings> options,
                               IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger.LogDebug("TcpWriteCommand()");

            // Setting the TCP client instance.
            _client = client;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is called processing the tcp write command.
        /// </summary>
        /// <returns></returns>
        public int OnExecute(CommandLineApplication app)
        {
            _logger.LogDebug("OnExecute()");

            try
            {
                if (CheckOptions(app))
                {
                    // Overriding TCP client options.
                    OptionSettings = Parent.Parent.OptionSettings;
                    _client.TcpMaster.ReceiveTimeout = Parent.ReceiveTimeout;
                    _client.TcpMaster.SendTimeout = Parent.SendTimeout;
                    _client.TcpSlave.Address = Parent.Address;
                    _client.TcpSlave.Port = Parent.Port;
                    _client.TcpSlave.ID = Parent.SlaveID;

                    if (OptionSettings)
                    {
                        Console.WriteLine($"TcpMaster: {JsonConvert.SerializeObject(_settings.TcpMaster, Formatting.Indented)}");
                        Console.WriteLine($"TcpSlave: {JsonConvert.SerializeObject(_settings.TcpSlave, Formatting.Indented)}");
                    }

                    if (_client.Connect())
                    {
                        // Writing coils.
                        if (OptionC)
                        {
                            List<bool> values = JsonConvert.DeserializeObject<List<bool>>(Values);

                            if (values.Count == 0)
                            {
                                _logger.LogWarning($"No values specified.");
                            }
                            else
                            {
                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Write single coil[{Offset}] = {values[0]}");
                                    _client.WriteSingleCoil(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} coils starting at {Offset}");

                                    for (int index = 0; index < values.Count; ++index)
                                        Console.WriteLine($"Value of coil[{Offset + index}] = {values[index]}");

                                    _client.WriteMultipleCoils(Offset, values.ToArray());
                                }
                            }
                        }

                        // Writing holding registers.
                        if (OptionH)
                        {
                            if ((Type.Length > 0) && string.IsNullOrEmpty(Values))
                            {
                                _logger.LogWarning($"No values of type '{Type}' specified.");
                            }

                            if (string.Equals(Type, "string", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Writing an ASCII string at offset = {Offset}");
                                _client.WriteString(Offset, Values);
                            }
                            else if (string.Equals(Type, "bits", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Writing a 16 bit array at offset = {Offset}");
                                _client.WriteBits(Offset, Values.ToBitArray());
                            }
                            else if (string.Equals(Type, "byte", StringComparison.OrdinalIgnoreCase))
                            {
                                List<byte> bytes = JsonConvert.DeserializeObject<List<byte>>(Values);
                                Console.WriteLine($"Writing {bytes.Count} bytes at offset = {Offset}");
                                _client.WriteBytes(Offset, bytes.ToArray());
                            }
                            else if (string.Equals(Type, "short", StringComparison.OrdinalIgnoreCase))
                            {
                                List<short> values = JsonConvert.DeserializeObject<List<short>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single short value at offset = {Offset}");
                                    _client.WriteShort(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} short values at offset = {Offset}");
                                    _client.WriteShortArray(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "ushort", StringComparison.OrdinalIgnoreCase))
                            {
                                List<ushort> values = JsonConvert.DeserializeObject<List<ushort>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single unsigned short value at offset = {Offset}");
                                    _client.WriteUShort(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} unsigned short values at offset = {Offset}");
                                    _client.WriteUShortArray(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "int", StringComparison.OrdinalIgnoreCase))
                            {
                                List<int> values = JsonConvert.DeserializeObject<List<int>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single int value at offset = {Offset}");
                                    _client.WriteInt32(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} int values at offset = {Offset}");
                                    _client.WriteInt32Array(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "uint", StringComparison.OrdinalIgnoreCase))
                            {
                                List<uint> values = JsonConvert.DeserializeObject<List<uint>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single unsigned int value at offset = {Offset}");
                                    _client.WriteUInt32(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} unsigned int values at offset = {Offset}");
                                    _client.WriteUInt32Array(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "float", StringComparison.OrdinalIgnoreCase))
                            {
                                List<float> values = JsonConvert.DeserializeObject<List<float>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single float value at offset = {Offset}");
                                    _client.WriteFloat(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} float values at offset = {Offset}");
                                    _client.WriteFloatArray(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "double", StringComparison.OrdinalIgnoreCase))
                            {
                                List<double> values = JsonConvert.DeserializeObject<List<double>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single double value at offset = {Offset}");
                                    _client.WriteDouble(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} double values at offset = {Offset}");
                                    _client.WriteDoubleArray(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "long", StringComparison.OrdinalIgnoreCase))
                            {
                                List<long> values = JsonConvert.DeserializeObject<List<long>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single long value at offset = {Offset}");
                                    _client.WriteLong(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} long values at offset = {Offset}");
                                    _client.WriteLongArray(Offset, values.ToArray());
                                }
                            }
                            else if (string.Equals(Type, "ulong", StringComparison.OrdinalIgnoreCase))
                            {
                                List<ulong> values = JsonConvert.DeserializeObject<List<ulong>>(Values);

                                if (values.Count == 1)
                                {
                                    Console.WriteLine($"Writing a single unsigned long value at offset = {Offset}");
                                    _client.WriteULong(Offset, values[0]);
                                }
                                else
                                {
                                    Console.WriteLine($"Writing {values.Count} unsigned long values at offset = {Offset}");
                                    _client.WriteULongArray(Offset, values.ToArray());
                                }
                            }
                            else
                            {
                                List<ushort> values = JsonConvert.DeserializeObject<List<ushort>>(Values);

                                if (values.Count == 0)
                                {
                                    _logger.LogWarning($"No values specified.");
                                }
                                else
                                {
                                    if (values.Count == 1)
                                    {
                                        Console.WriteLine($"Writing single holding register[{Offset}] = {values[0]}");
                                        _client.WriteSingleRegister(Offset, values[0]);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Writing {values.Count} holding registers starting at {Offset}");

                                        for (int index = 0; index < values.Count; ++index)
                                            Console.WriteLine($"Value of holding register[{Offset + index}] = {values[index]}");

                                        _client.WriteMultipleRegisters(Offset, values.ToArray());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Modbus TCP slave not found at {_client.TcpSlave.Address}:{_client.TcpSlave.Port}.");
                        return -1;
                    }
                }
            }
            catch (JsonSerializationException jsx)
            {
                _logger.LogError(jsx, $"Exception parsing data values.");
                return -1;
            }
            catch (JsonReaderException jrx)
            {
                _logger.LogError(jrx, $"Exception writing data values.");
                return -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"TcpWriteCommand - {ex.Message}");
                return -1;
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
                    case "offset": OptionO = option.HasValue(); break;
                    case "type": OptionT = option.HasValue(); break;
                }
            }

            if (NoOptions)
            {
                Console.WriteLine($"Specify the write option (coils or holding registers).");
                return false;
            }

            if (OptionC && OptionH)
            {
                Console.WriteLine($"Specify only a single write option (coils or holding registers).");
                return false;
            }

            if (OptionT)
            {
                if (OptionH)
                {
                    switch (Type.ToLower(CultureInfo.CurrentCulture))
                    {
                        case "bits":
                        case "string":
                        case "byte":
                        case "short":
                        case "ushort":
                        case "int":
                        case "uint":
                        case "float":
                        case "double":
                        case "long":
                        case "ulong":
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
