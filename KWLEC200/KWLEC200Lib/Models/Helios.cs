// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helios.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Lib.Models
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using NModbus;
    using NModbus.Extensions;

    using DataValueLib;
    using static DataValueLib.DataValue;

    #endregion

    /// <summary>
    /// Class providing common methods to convert values and to read and write Modbus data.
    /// </summary>
    public class Helios
    {
        #region Private Data Members

        private const ushort OFFSET = 1;

        private static Object _lock = new Object();
        private readonly ILogger<Helios> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Helios"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        public Helios(ILogger<Helios> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a string value from the KWLEC200 raw string data.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The raw string data.</param>
        /// <returns>The string value read from the KWLEC200.</returns>
        public (string Data, DataStatus Status) ParseStringData(string name, ushort size, ushort count, string data)
        {
            if (data.Length > name.Length)
            {
                if (data.Substring(0, name.Length) == name)
                {
                    _logger?.LogDebug($"Helios: '{name}' => '{data}'.");
                    return (data.Substring(name.Length + 1).Trim('\0'), Good);
                }
                else
                {
                    _logger?.LogError($"Helios: Unexpected name in '{data}'.");
                    return (string.Empty, BadOutOfRange);
                }
            }
            else
            {
                _logger?.LogError($"Helios: Unexpected text for '{name}': '{data}'");
                return (string.Empty, BadUnknownResponse);
            }
        }

        /// <summary>
        /// Gets a boolean value from the KWLEC200 raw string data.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The raw string data.</param>
        /// <returns>The boolean value read from the KWLEC200.</returns>
        public (bool Data, DataStatus Status) ParseBoolData(string name, ushort size, ushort count, string data)
        {
            var (result, status) = ParseStringData(name, size, count, data);

            if (status.IsGood)
            {
                return (!(result == "0"), Good);
            }
            else
            {
                return (false, status);
            }
        }

        /// <summary>
        /// Gets a double value from the KWLEC200 raw string data.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The raw string data.</param>
        /// <returns>The double value read from the KWLEC200.</returns>
        public (double Data, DataStatus Status) ParseDoubleData(string name, ushort size, ushort count, string data)
        {
            var (result, status) = ParseStringData(name, size, count, data);

            if (status.IsGood)
            {
                if (result == "-")
                {
                    return (0.0, Good);
                }
                else if (double.TryParse(result, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    return (value, Good);
                }
                else
                {
                    _logger?.LogWarning($"Helios: Unable to parse double: '{result}'");
                    return (0.0, BadDecodingError);
                }
            }
            else
            {
                return (0.0, status);
            }
        }

        /// <summary>
        /// Gets an integer value from the KWLEC200 raw string data.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The raw string data.</param>
        /// <returns>The integer value read from the KWLEC200.</returns>
        public (int Data, DataStatus Status) ParseIntegerData(string name, ushort size, ushort count, string data)
        {
            var (result, status) = ParseStringData(name, size, count, data);

            if (status.IsGood)
            {
                if (result == "-")
                {
                    return (0, Good);
                }
                else if (int.TryParse(result, out int value))
                {
                    return (value, Good);
                }
                else
                {
                    _logger?.LogWarning($"Helios: Unable to parse integer: '{result}'");
                    return (0, BadDecodingError);
                }
            }
            else
            {
                return (0, status);
            }
        }

        /// <summary>
        /// Gets a Date value from the KWLEC200 raw string data.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The raw string data.</param>
        /// <returns>The DateTime value read from the KWLEC200.</returns>
        public (DateTime Data, DataStatus Status) ParseDateData(string name, ushort size, ushort count, string data)
        {
            var (result, status) = ParseStringData(name, size, count, data);

            if (status.IsGood)
            {
                if (result == string.Empty)
                {
                    return (new DateTime(), status);
                }
                else
                {
                    if (DateTime.TryParse(result, new CultureInfo("de-DE"), DateTimeStyles.AssumeLocal, out DateTime value))
                    {
                        return (value, Good);
                    }
                    else
                    {
                        _logger?.LogWarning($"Helios: Unable to parse date value: '{result}'");
                        return (new DateTime(), BadDecodingError);
                    }
                }
            }
            else
            {
                return (new DateTime(), status);
            }
        }

        /// <summary>
        /// Gets a Time value from the KWLEC200 raw string data.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The raw string data.</param>
        /// <returns>The Timespan value read from the KWLEC200.</returns>
        public (TimeSpan Data, DataStatus Status) ParseTimeData(string name, ushort size, ushort count, string data)
        {
            var (result, status) = ParseStringData(name, size, count, data);

            if (status.IsGood)
            {
                if (result == string.Empty)
                {
                    return (new TimeSpan(), status);
                }
                else
                {
                    if (TimeSpan.TryParse(result, out TimeSpan value))
                    {
                        return (value, Good);
                    }
                    else
                    {
                        _logger?.LogWarning($"Helios: Unable to parse time value: '{result}'");
                        return (new TimeSpan(), BadDecodingError);
                    }
                }
            }
            else
            {
                return (new TimeSpan(), status);
            }
        }

        /// <summary>
        /// Gets a string value for the KWLEC200.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The string data item.</param>
        /// <returns>The string value.</returns>
        public string GetStringData(string name, ushort size, ushort count, string data)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(data))
            {
                int length = count * 2;
                string text = name + "=" + data.Substring(0, data.Length);

                if (text.Length > length)
                {
                    text = text.Substring(0, length);
                }
                else
                {
                    text = text.PadRight(length, '\0');
                }

                return text;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets a boolean value for the KWLEC200.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The boolean data item.</param>
        /// <returns>The string value.</returns>
        public string GetBoolData(string name, ushort size, ushort count, bool data)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string text = name + "=" + (data ? "o" : "0");
                return text.PadRight(count * 2, '\0');
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets an integer value for the KWLEC200.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The int data item.</param>
        /// <returns>The string value.</returns>
        public string GetIntegerData(string name, ushort size, ushort count, int data)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string format = "0";

                switch (size)
                {
                    case 2: { format = "00"; break; }
                    case 3: { format = "000"; break; }
                    case 4: { format = "0000"; break; }
                }

                string text = name + "=" + data.ToString(format);
                return text.PadRight(count * 2, '\0');
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets a double value for the KWLEC200.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The double data item.</param>
        /// <returns>The string value.</returns>
        public string GetDoubleData(string name, ushort size, ushort count, double data)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string format = "0.0";

                if (size == 4)
                {
                    format = "00.0";
                }

                string text = name + "=" + data.ToString(format);
                return text.PadRight(count * 2, '\0');
            }

            return string.Empty;
        }

        /// <summary>
        /// Get a DateTime value for the KWLEC200.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The DateTime data item.</param>
        /// <returns>The string value.</returns>
        public string GetDateData(string name, ushort size, ushort count, DateTime data)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string format = "dd.MM.yy";
                string text = name + "=" + data.ToString(format, new CultureInfo("de-DE"));
                return text.PadRight(count * 2, '\0');
            }

            return string.Empty;
        }

        /// <summary>
        /// Get a TimeSpan value for the KWLEC200.
        /// </summary>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The TimeSpan data item.</param>
        /// <returns>The string value.</returns>
        public string GetTimeData(string name, ushort size, ushort count, TimeSpan data)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string format = @"hh\:mm\:ss";
                string text = name + "=" + data.ToString(format);
                return text.PadRight(count * 2, '\0');
            }

            return string.Empty;
        }

        /// <summary>
        /// Reads a string value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The string value read from the KWLEC200.</returns>
        public (string Data, DataStatus Status) ReadRawData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            string text = string.Empty;

            try
            {
                lock (_lock)
                {
                    _logger?.LogDebug($"ReadRawData write '{name}'.");
                    modbus.WriteStringAsync(slave, OFFSET, name + "\0\0").Wait();
                    Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                    text = modbus.ReadStringAsync(slave, OFFSET, (ushort)(count * 2)).Result;
                    _logger?.LogDebug($"ReadRawData read: '{text}'.");
                }

                if (!string.IsNullOrEmpty(text))
                {
                    return (text, Good);
                }
                else
                {
                    _logger?.LogError($"ReadRawData invalid response.");
                    return (string.Empty, BadUnknownResponse);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in ReadRawData.");
                return (string.Empty, BadUnknownResponse);
            }
        }

        /// <summary>
        /// Reads a string value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The string value read from the KWLEC200.</returns>
        public (string Data, DataStatus Status) ReadStringData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            var result = ReadRawData(modbus, slave, name, size, count);

            if (!string.IsNullOrEmpty(result.Data))
            {
                return ParseStringData(name, size, count, result.Data);
            }
            else
            {
                return (string.Empty, BadUnknownResponse);
            }
        }

        /// <summary>
        /// Reads a single boolean value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The boolean value read from the KWLEC200.</returns>
        public (bool Data, DataStatus Status) ReadBoolData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            var (result, status) = ReadRawData(modbus, slave, name, size, count);

            if (status.IsGood)
            {
                return ParseBoolData(name, size, count, result);
            }
            else
            {
                return (false, status);
            }
        }

        /// <summary>
        /// Reads a single double value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The double value read from the KWLEC200.</returns>
        public (double Data, DataStatus Status) ReadDoubleData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            var (result, status) = ReadRawData(modbus, slave, name, size, count);

            if (status.IsGood)
            {
                return ParseDoubleData(name, size, count, result);
            }
            else
            {
                return (0.0, status);
            }
        }

        /// <summary>
        /// Reads a single integer value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The integer value read from the KWLEC200.</returns>
        public (int Data, DataStatus Status) ReadIntegerData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            var (result, status) = ReadRawData(modbus, slave, name, size, count);

            if (status.IsGood)
            {
                return ParseIntegerData(name, size, count, result);
            }
            else
            {
                return (0, status);
            }
        }

        /// <summary>
        /// Reads a single Date value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The DateTime value read from the KWLEC200.</returns>
        public (DateTime Data, DataStatus Status) ReadDateData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            var (result, status) = ReadRawData(modbus, slave, name, size, count);

            if (status.IsGood)
            {
                return ParseDateData(name, size, count, result);
            }
            else
            {
                return (new DateTime(), status);
            }
        }

        /// <summary>
        /// Reads a single Time value from the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <returns>The Timespan value read from the KWLEC200.</returns>
        public (TimeSpan Data, DataStatus Status) ReadTimeData(IModbusMaster modbus, byte slave, string name, ushort size, ushort count)
        {
            var (result, status) = ReadRawData(modbus, slave, name, size, count);

            if (status.IsGood)
            {
                return ParseTimeData(name, size, count, result);
            }
            else
            {
                return (new TimeSpan(), status);
            }
        }

        /// <summary>
        /// Writes a single string value to the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The string data item.</param>
        /// <returns></returns>
        public async Task WriteStringDataAsync(IModbusMaster modbus, byte slave, string name, ushort size, ushort count, string data)
        {
            string text = GetStringData(name, size, count, data);

            if (!string.IsNullOrEmpty(text))
            {
                await modbus.WriteStringAsync(slave, OFFSET, text);
            }
        }

        /// <summary>
        /// Writes a single boolean value to the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The boolean data item.</param>
        /// <returns></returns>
        public async Task WriteBoolDataAsync(IModbusMaster modbus, byte slave, string name, ushort size, ushort count, bool data)
        {
            string text = GetBoolData(name, size, count, data);

            if (!string.IsNullOrEmpty(text))
            {
                await modbus.WriteStringAsync(slave, OFFSET, text);
            }
        }

        /// <summary>
        /// Writes a single integer value to the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The int data item.</param>
        /// <returns></returns>
        public async Task WriteIntegerDataAsync(IModbusMaster modbus, byte slave, string name, ushort size, ushort count, int data)
        {
            string text = GetIntegerData(name, size, count, data);

            if (!string.IsNullOrEmpty(text))
            {
                await modbus.WriteStringAsync(slave, OFFSET, text);
            }
        }

        /// <summary>
        /// Writes a single double value to the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The double data item.</param>
        /// <returns></returns>
        public async Task WriteDoubleDataAsync(IModbusMaster modbus, byte slave, string name, ushort size, ushort count, double data)
        {
            string text = GetDoubleData(name, size, count, data);

            if (!string.IsNullOrEmpty(text))
            {
                await modbus.WriteStringAsync(slave, OFFSET, text);
            }
        }

        /// <summary>
        /// Writes a single DateTime value to the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The DateTime data item.</param>
        /// <returns></returns>
        public async Task WriteDateDataAsync(IModbusMaster modbus, byte slave, string name, ushort size, ushort count, DateTime data)
        {
            string text = GetDateData(name, size, count, data);

            if (!string.IsNullOrEmpty(text))
            {
                await modbus.WriteStringAsync(slave, OFFSET, text);
            }
        }

        /// <summary>
        /// Writes a single TimeSpan value to the KWLEC200.
        /// </summary>
        /// <param name="modbus">The Modbus master device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="name">The Helios value name attribute.</param>
        /// <param name="size">The Helios value size attribute.</param>
        /// <param name="count">The Helios value count attribute.</param>
        /// <param name="data">The TimeSpan data item.</param>
        /// <returns></returns>
        public async Task WriteTimeDataAsync(IModbusMaster modbus, byte slave, string name, ushort size, ushort count, TimeSpan data)
        {
            string text = GetTimeData(name, size, count, data);

            if (!string.IsNullOrEmpty(text))
            {
                await modbus.WriteStringAsync(slave, OFFSET, text);
            }
        }

        /// <summary>
        /// Helper method to read the property from the KWLEC200.
        /// </summary>
        /// <param name="data">The KWLEC200 data.</param>
        /// <param name="modbus">The Modbus device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        public DataStatus ReadProperty(KWLEC200Data data, IModbusMaster modbus, byte slave, string property)
        {
            DataStatus status = Good;

            try
            {
                object value = data.GetPropertyValue(property);
                string name = KWLEC200Data.GetName(property);
                ushort size = KWLEC200Data.GetSize(property);
                ushort count = KWLEC200Data.GetCount(property);

                _logger?.LogDebug($"ReadProperty property '{property}' => Type: {value.GetType()}, Name: {name}, Size: {size}, Count: {count}.");

                if (value is bool)
                {
                    var result = ReadBoolData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty boolean property '{property}' => Value: {result.Data}.");
                    }

                    status = result.Status;
                }
                else if (value is int)
                {
                    var result = ReadIntegerData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty integer property '{property}' => Value: {result.Data}.");
                    }

                    status = result.Status;
                }
                else if (value is double)
                {
                    var result = ReadDoubleData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty double property '{property}' => Value: {result.Data}.");
                    }

                    status = result.Status;
                }
                else if (value is DateTime)
                {
                    var result = ReadDateData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty datetime property '{property}' => Value: {result.Data.Date}.");
                    }

                    status = result.Status;
                }
                else if (value is TimeSpan)
                {
                    var result = ReadTimeData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty timespan property '{property}' => Value: {result.Data}.");
                    }

                    status = result.Status;
                }
                else if (((dynamic)value).GetType().IsEnum)
                {
                    var result = ReadIntegerData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty enum property '{property}' => Value: '{result.Data}'.");
                    }

                    status = result.Status;
                }
                else if (value is string)
                {
                    var result = ReadStringData(modbus, slave, name, size, count);

                    if (result.Status.IsGood)
                    {
                        data.SetPropertyValue(property, result.Data);
                        _logger?.LogDebug($"ReadProperty string property '{property}' => Value: {result.Data}");
                    }

                    status = result.Status;
                }
            }
            catch (ArgumentNullException anx)
            {
                _logger?.LogError(anx, $"ArgumentNullException in ReadProperty property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                _logger?.LogError(aor, $"ArgumentOutOfRangeException in ReadProperty property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentException aex)
            {
                _logger?.LogError(aex, $"ArgumentException in ReadAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ObjectDisposedException odx)
            {
                _logger?.LogError(odx, $"ObjectDisposedException in ReadProperty property '{property}'.");
                status = BadInternalError;
            }
            catch (FormatException fex)
            {
                _logger?.LogError(fex, $"FormatException in ReadProperty property '{property}'.");
                status = BadEncodingError;
            }
            catch (IOException iox)
            {
                _logger?.LogError(iox, $"IOException in ReadAsync property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidModbusRequestException imr)
            {
                _logger?.LogError(imr, $"InvalidModbusRequestException in ReadProperty property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidOperationException iox)
            {
                _logger?.LogError(iox, $"InvalidOperationException in ReadProperty property '{property}'.");
                status = BadInternalError;
            }
            catch (SlaveException slx)
            {
                _logger?.LogError(slx, $"SlaveException in ReadProperty property '{property}'.");
                status = BadDeviceFailure;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in ReadProperty property '{property}'.");
                status = BadInternalError;
            }

            return status;
        }

        /// <summary>
        /// Helper method to write the property to the KWLEC200.
        /// </summary>
        /// <param name="data">The KWLEC200 data.</param>
        /// <param name="modbus">The Modbus device.</param>
        /// <param name="slave">The slave ID of the Modbus device.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        public DataStatus WriteProperty(KWLEC200Data data, IModbusMaster modbus, byte slave, string property)
        {
            DataStatus status = Good;

            try
            {
                dynamic value = data.GetPropertyValue(property);
                string name = KWLEC200Data.GetName(property);
                ushort size = KWLEC200Data.GetSize(property);
                ushort count = KWLEC200Data.GetCount(property);

                if (value is bool)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {value}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteBoolDataAsync(modbus, slave, name, size, count, (bool)value).Wait();
                }
                else if (value is int)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {value}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteIntegerDataAsync(modbus, slave, name, size, count, (int)value).Wait();
                }
                else if (value is double)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {value}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteDoubleDataAsync(modbus, slave, name, size, count, (double)value).Wait();
                }
                else if (value is DateTime)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {((DateTime)value).Date}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteDateDataAsync(modbus, slave, name, size, count, (DateTime)value).Wait();
                }
                else if (value is TimeSpan)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {value}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteTimeDataAsync(modbus, slave, name, size, count, (TimeSpan)value).Wait();
                }
                else if (value.GetType().IsEnum)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {value}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteIntegerDataAsync(modbus, slave, name, size, count, (int)value).Wait();
                }
                else if (value is string)
                {
                    _logger?.LogDebug($"WriteProperty property '{property}' => Type: {value.GetType()}, Value: {value}, Name: {name}, Size: {size}, Count: {count}.");
                    WriteStringDataAsync(modbus, slave, name, size, count, (string)value).Wait();
                }
            }
            catch (ArgumentNullException anx)
            {
                _logger?.LogError(anx, $"ArgumentNullException in WriteProperty property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                _logger?.LogError(aor, $"ArgumentOutOfRangeException in WriteProperty property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentException aex)
            {
                _logger?.LogError(aex, $"ArgumentException in WriteProperty property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ObjectDisposedException odx)
            {
                _logger?.LogError(odx, $"ObjectDisposedException in WriteProperty property '{property}'.");
                status = BadInternalError;
            }
            catch (FormatException fex)
            {
                _logger?.LogError(fex, $"FormatException in WriteProperty property '{property}'.");
                status = BadEncodingError;
            }
            catch (IOException iox)
            {
                _logger?.LogError(iox, $"IOException in WriteProperty property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidModbusRequestException imr)
            {
                _logger?.LogError(imr, $"InvalidModbusRequestException in WriteProperty property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidOperationException iox)
            {
                _logger?.LogError(iox, $"InvalidOperationException in WriteProperty property '{property}'.");
                status = BadInternalError;
            }
            catch (SlaveException slx)
            {
                _logger?.LogError(slx, $"SlaveException in WriteProperty property '{property}'.");
                status = BadDeviceFailure;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in WriteProperty property '{property}'.");
                status = BadInternalError;
            }

            return status;
        }

        #endregion Public Methods
    }
}
