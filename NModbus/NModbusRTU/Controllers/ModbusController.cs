// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusController.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusRTU.Controllers
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NModbus.Extensions;

    using BaseClassLib;
    using NModbusLib;
    using NModbusRTU.Models;

    #endregion

    /// <summary>
    /// Baseclass for all Modbus Gateway MVC Controller reading and writing data.
    /// </summary>
    public class ModbusController : BaseController<AppSettings>
    {
        #region Protected Fields

        /// <summary>
        /// The Modbus RTU client instance.
        /// </summary>
        protected readonly IRtuClient _client;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModbusController"/> class.
        /// </summary>
        /// <param name="client">The Modbus client instance.</param>
        /// <param name="options">The application options.</param>
        /// <param name="logger">The application _logger.</param>
        public ModbusController(IRtuClient client,
                                IOptions<AppSettings> options,
                                ILogger<ModbusController> logger)
            : base(logger, options)
        {
            _client = client;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to send a Modbus read request to a Modbus slave and returns the requested value(s)
        /// providing common communication setup and associated exception handling (logging).
        /// A TCP client is created and used to send the request to the Modbus TCP client.
        /// The following requests are supported:
        ///
        ///     Single Coil
        ///     Single Discrete Input
        ///     Single Holding Register
        ///     Single Input Register
        ///
        ///     Multiple Coils
        ///     Multiple Discrete Inputs
        ///     Multiple Holding Registers
        ///     Multiple Input Registers
        ///
        /// Additional datatypes (single values, value arrays and strings) with read
        /// only access (discrete inputs and input registers) and read / write access
        /// (coils and holding registers) are supported:
        ///
        ///     ASCII String    (multiple registers)
        ///     Hex String      (multiple registers)
        ///     Bool            (single coil)
        ///     Bits            (single register)
        ///     Short           (single register)
        ///     UShort          (single register)
        ///     Int32           (two registers)
        ///     UInt32          (two registers)
        ///     Float           (two registers)
        ///     Double          (four registers)
        ///     Long            (four registers)
        ///     ULong           (four registers)
        ///
        /// </summary>
        /// <param name="request">The <see cref="ModbusRequestData"/> data.</param>
        /// <param name="function">The function name.</param>
        /// <returns>A task returning an action method result.</returns>
        protected async Task<IActionResult> ModbusReadRequest(ModbusRequestData request, string function)
        {
            try
            {
                request.Master = _client.RtuMaster;

                if (_client.Connect())
                {
                    switch (function)
                    {
                        case "ReadCoilAsync":
                            {
                                bool[] values = await _client.ReadCoilsAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseData<bool>(request, values[0]));
                            }
                        case "ReadCoilsAsync":
                            {
                                bool[] values = await _client.ReadCoilsAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<bool>(request, values));
                            }
                        case "ReadInputAsync":
                            {
                                bool[] values = await _client.ReadInputsAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseData<bool>(request, values[0]));
                            }
                        case "ReadInputsAsync":
                            {
                                bool[] values = await _client.ReadInputsAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<bool>(request, values));
                            }
                        case "ReadHoldingRegisterAsync":
                            {
                                ushort[] values = await _client.ReadHoldingRegistersAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseData<ushort>(request, values[0]));
                            }
                        case "ReadHoldingRegistersAsync":
                            {
                                ushort[] values = await _client.ReadHoldingRegistersAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<ushort>(request, values));
                            }
                        case "ReadInputRegisterAsync":
                            {
                                ushort[] values = await _client.ReadInputRegistersAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseData<ushort>(request, values[0]));
                            }
                        case "ReadInputRegistersAsync":
                            {
                                ushort[] values = await _client.ReadInputRegistersAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<ushort>(request, values));
                            }
                        case "ReadOnlyStringAsync":
                            {
                                string value = await _client.ReadOnlyStringAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseStringData(request, value));

                            }
                        case "ReadOnlyHexStringAsync":
                            {
                                string value = await _client.ReadOnlyHexStringAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseStringData(request, value));
                            }
                        case "ReadOnlyBoolAsync":
                            {
                                bool value = await _client.ReadOnlyBoolAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<bool>(request, value));
                            }
                        case "ReadOnlyBitsAsync":
                            {
                                BitArray value = await _client.ReadOnlyBitsAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseStringData(request, value.ToDigitString()));
                            }
                        case "ReadOnlyShortAsync":
                            {
                                short value = await _client.ReadOnlyShortAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<short>(request, value));
                            }
                        case "ReadOnlyUShortAsync":
                            {
                                ushort value = await _client.ReadOnlyUShortAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<ushort>(request, value));
                            }
                        case "ReadOnlyInt32Async":
                            {
                                int value = await _client.ReadOnlyInt32Async(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<int>(request, value));
                            }
                        case "ReadOnlyUInt32Async":
                            {
                                uint value = await _client.ReadOnlyUInt32Async(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<uint>(request, value));
                            }
                        case "ReadOnlyFloatAsync":
                            {
                                float value = await _client.ReadOnlyFloatAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<float>(request, value));
                            }
                        case "ReadOnlyDoubleAsync":
                            {
                                double value = await _client.ReadOnlyDoubleAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<double>(request, value));
                            }
                        case "ReadOnlyLongAsync":
                            {
                                long value = await _client.ReadOnlyLongAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<long>(request, value));
                            }
                        case "ReadOnlyULongAsync":
                            {
                                ulong value = await _client.ReadOnlyULongAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<ulong>(request, value));
                            }
                        case "ReadOnlyBoolArrayAsync":
                            {
                                bool[] values = await _client.ReadOnlyBoolArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<bool>(request, values));
                            }
                        case "ReadOnlyBytesAsync":
                            {
                                byte[] values = await _client.ReadOnlyBytesAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<byte>(request, values));
                            }
                        case "ReadOnlyShortArrayAsync":
                            {
                                short[] values = await _client.ReadOnlyShortArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<short>(request, values));
                            }
                        case "ReadOnlyUShortArrayAsync":
                            {
                                ushort[] values = await _client.ReadOnlyUShortArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<ushort>(request, values));
                            }
                        case "ReadOnlyInt32ArrayAsync":
                            {
                                int[] values = await _client.ReadOnlyInt32ArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<int>(request, values));
                            }
                        case "ReadOnlyUInt32ArrayAsync":
                            {
                                uint[] values = await _client.ReadOnlyUInt32ArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<uint>(request, values));
                            }
                        case "ReadOnlyFloatArrayAsync":
                            {
                                float[] values = await _client.ReadOnlyFloatArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<float>(request, values));
                            }
                        case "ReadOnlyDoubleArrayAsync":
                            {
                                double[] values = await _client.ReadOnlyDoubleArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<double>(request, values));
                            }
                        case "ReadOnlyLongArrayAsync":
                            {
                                long[] values = await _client.ReadOnlyLongArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<long>(request, values));
                            }
                        case "ReadOnlyULongArrayAsync":
                            {
                                ulong[] values = await _client.ReadOnlyULongArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<ulong>(request, values));
                            }
                        case "ReadStringAsync":
                            {
                                string value = await _client.ReadStringAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseStringData(request, value));

                            }
                        case "ReadHexStringAsync":
                            {
                                string value = await _client.ReadHexStringAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseStringData(request, value));
                            }
                        case "ReadBoolAsync":
                            {
                                bool value = await _client.ReadBoolAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<bool>(request, value));
                            }
                        case "ReadBitsAsync":
                            {
                                BitArray value = await _client.ReadBitsAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseStringData(request, value.ToDigitString()));
                            }
                        case "ReadShortAsync":
                            {
                                short value = await _client.ReadShortAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<short>(request, value));
                            }
                        case "ReadUShortAsync":
                            {
                                ushort value = await _client.ReadUShortAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<ushort>(request, value));
                            }
                        case "ReadInt32Async":
                            {
                                int value = await _client.ReadInt32Async(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<int>(request, value));
                            }
                        case "ReadUInt32Async":
                            {
                                uint value = await _client.ReadUInt32Async(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<uint>(request, value));
                            }
                        case "ReadFloatAsync":
                            {
                                float value = await _client.ReadFloatAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<float>(request, value));
                            }
                        case "ReadDoubleAsync":
                            {
                                double value = await _client.ReadDoubleAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<double>(request, value));
                            }
                        case "ReadLongAsync":
                            {
                                long value = await _client.ReadLongAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<long>(request, value));
                            }
                        case "ReadULongAsync":
                            {
                                ulong value = await _client.ReadULongAsync(request.Slave.ID, request.Offset);
                                _logger.LogTrace($"{function}(): {value}");
                                return Ok(new ModbusResponseData<ulong>(request, value));
                            }
                        case "ReadBoolArrayAsync":
                            {
                                bool[] values = await _client.ReadBoolArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<bool>(request, values));
                            }
                        case "ReadBytesAsync":
                            {
                                byte[] values = await _client.ReadBytesAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<byte>(request, values));
                            }
                        case "ReadShortArrayAsync":
                            {
                                short[] values = await _client.ReadShortArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<short>(request, values));
                            }
                        case "ReadUShortArrayAsync":
                            {
                                ushort[] values = await _client.ReadUShortArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<ushort>(request, values));
                            }
                        case "ReadInt32ArrayAsync":
                            {
                                int[] values = await _client.ReadInt32ArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<int>(request, values));
                            }
                        case "ReadUInt32ArrayAsync":
                            {
                                uint[] values = await _client.ReadUInt32ArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<uint>(request, values));
                            }
                        case "ReadFloatArrayAsync":
                            {
                                float[] values = await _client.ReadFloatArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<float>(request, values));
                            }
                        case "ReadDoubleArrayAsync":
                            {
                                double[] values = await _client.ReadDoubleArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<double>(request, values));
                            }
                        case "ReadLongArrayAsync":
                            {
                                long[] values = await _client.ReadLongArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<long>(request, values));
                            }
                        case "ReadULongArrayAsync":
                            {
                                ulong[] values = await _client.ReadULongArrayAsync(request.Slave.ID, request.Offset, request.Number);
                                _logger.LogTrace($"{function}(): {values}");
                                return Ok(new ModbusResponseArrayData<ulong>(request, values));
                            }
                        default:
                            _client.Disconnect();
                            _logger.LogError($"RTU master read request {function}() not supported.");
                            return NotFound($"RTU master read request {function}() not supported.");
                    }
                }
                else
                {
                    _logger.LogError($"RTU master ({request.Master.SerialPort}) not open.");
                    return NotFound("RTU master COM port not open.");
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                _logger.LogError(uae, $"{function}() Unauthorized Access Exception.");
                return NotFound($"Unauthorized Access Exception: {uae.Message}");
            }
            catch (ArgumentOutOfRangeException are)
            {
                _logger.LogError(are, $"{function}() Argument out of Range Exception.");
                return BadRequest($"Argument out of Range Exception: {are.Message}");
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex, $"{function}() Argument Exception.");
                return BadRequest($"Argument Exception: {aex.Message}");
            }
            catch (NModbus.SlaveException mse)
            {
                _logger.LogError(mse, $"{function}() Modbus SlaveException.");
                return StatusCode(502, $"Modbus SlaveException: {mse.Message}");
            }
            catch (System.IO.IOException ioe)
            {
                _logger.LogError(ioe, $"{function}() IO Exception.");
                return StatusCode(500, $"IO Exception: {ioe.Message}");
            }
            catch (TimeoutException tex)
            {
                _logger.LogError(tex, $"{function}() Timeout Exception.");
                return StatusCode(500, $"Timeout Exception: {tex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{function}() Exception.");
                return StatusCode(500, $"Exception: {ex.Message}");
            }
            finally
            {
                if (_client.Connected)
                {
                    _client.Disconnect();
                }
            }
        }

        /// <summary>
        /// Method to send a Modbus write request to a Modbus slave. providing common
        /// communication setup and associated exception handling (logging).
        /// A TCP client is created and used to send the request to the Modbus TCP client.
        /// The following requests are supported:
        ///
        ///     Single Coil
        ///     Single Holding Register
        ///
        /// Additional datatypes with read / write access (coils and holding registers) are supported:
        ///
        ///     ASCII String    (multiple holding registers)
        ///     Hex String      (multiple holding registers)
        ///     Bool            (single coil)
        ///     Bits            (single holding register)
        ///     Short           (single holding register)
        ///     UShort          (single holding register)
        ///     Int32           (two holding registers)
        ///     UInt32          (two holding registers)
        ///     Float           (two holding registers)
        ///     Double          (four holding registers)
        ///     Long            (four holding registers)
        ///     ULong           (four holding registers)
        ///
        /// </summary>
        /// <param name="request">The <see cref="ModbusRequestData"/> data.</param>
        /// <param name="data">The data value.</param>
        /// <param name="function">The function name.</param>
        /// <returns>A task returning an action method result.</returns>
        protected async Task<IActionResult> ModbusWriteSingleRequest<T>(ModbusRequestData request, T data, string function)
        {
            try
            {
                request.Master = _client.RtuMaster;

                if (_client.Connect())
                {
                    switch (function)
                    {
                        case "WriteCoilAsync":
                            {
                                bool value = (bool)Convert.ChangeType(data, typeof(bool));
                                await _client.WriteSingleCoilAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteHoldingRegisterAsync":
                            {
                                ushort value = (ushort)Convert.ChangeType(data, typeof(ushort));
                                await _client.WriteSingleRegisterAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteStringAsync":
                            {
                                string value = (string)Convert.ChangeType(data, typeof(string));
                                await _client.WriteStringAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteHexStringAsync":
                            {
                                string value = (string)Convert.ChangeType(data, typeof(string));
                                await _client.WriteHexStringAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteBoolAsync":
                            {
                                bool value = (bool)Convert.ChangeType(data, typeof(bool));
                                await _client.WriteBoolAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteBitsAsync":
                            {
                                BitArray value = ((string)Convert.ChangeType(data, typeof(string))).ToBitArray();
                                await _client.WriteBitsAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteShortAsync":
                            {
                                short value = (short)Convert.ChangeType(data, typeof(short));
                                await _client.WriteShortAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteUShortAsync":
                            {
                                ushort value = (ushort)Convert.ChangeType(data, typeof(ushort));
                                await _client.WriteUShortAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteInt32Async":
                            {
                                int value = (int)Convert.ChangeType(data, typeof(int));
                                await _client.WriteInt32Async(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteUInt32Async":
                            {
                                uint value = (uint)Convert.ChangeType(data, typeof(uint));
                                await _client.WriteUInt32Async(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteFloatAsync":
                            {
                                float value = (float)Convert.ChangeType(data, typeof(float));
                                await _client.WriteFloatAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteDoubleAsync":
                            {
                                double value = (double)Convert.ChangeType(data, typeof(double));
                                await _client.WriteDoubleAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteLongAsync":
                            {
                                long value = (long)Convert.ChangeType(data, typeof(long));
                                await _client.WriteLongAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteULongAsync":
                            {
                                ulong value = (ulong)Convert.ChangeType(data, typeof(ulong));
                                await _client.WriteULongAsync(request.Slave.ID, request.Offset, value);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        default:
                            _client.Disconnect();
                            _logger.LogError($"RTU master write request {function}() not supported.");
                            return NotFound($"RTU master write request {function}() not supported.");
                    }
                }
                else
                {
                    _logger.LogError($"RTU master ({request.Master.SerialPort}) not open.");
                    return NotFound("RTU master COM port not open.");
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                _logger.LogError(uae, $"{function}() Unauthorized Access Exception.");
                return NotFound($"Unauthorized Access Exception: {uae.Message}");
            }
            catch (ArgumentOutOfRangeException are)
            {
                _logger.LogError(are, $"{function}() Argument out of Range Exception.");
                return BadRequest($"Argument out of Range Exception: {are.Message}");
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex, $"{function}() Argument Exception.");
                return BadRequest($"Argument Exception: {aex.Message}");
            }
            catch (NModbus.SlaveException mse)
            {
                _logger.LogError(mse, $"{function}() Modbus SlaveException.");
                return StatusCode(502, $"Modbus SlaveException: {mse.Message}");
            }
            catch (System.IO.IOException ioe)
            {
                _logger.LogError(ioe, $"{function}() IO Exception.");
                return StatusCode(500, $"IO Exception: {ioe.Message}");
            }
            catch (TimeoutException tex)
            {
                _logger.LogError(tex, $"{function}() Timeout Exception.");
                return StatusCode(500, $"Timeout Exception: {tex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{function}() Exception.");
                return StatusCode(500, $"Exception: {ex.Message}");
            }
            finally
            {
                if (_client.Connected)
                {
                    _client.Disconnect();
                }
            }
        }

        /// <summary>
        /// Method to send a Modbus write request to a Modbus slave providing common
        /// communication setup and associated exception handling (logging).
        /// A TCP client is created and used to send the request to the Modbus TCP client.
        /// The following requests are supported:
        ///
        ///     Multiple Coils
        ///     Multiple Holding Register
        ///
        /// Additional datatypes with read / write access (coils and holding registers) are supported:
        ///
        ///     Bool            (multiple coils)
        ///     Bytes           (multiple holding registers)
        ///     Short           (multiple holding registers)
        ///     UShort          (multiple holding registers)
        ///     Int32           (multiple holding registers)
        ///     UInt32          (multiple holding registers)
        ///     Float           (multiple holding registers)
        ///     Double          (multiple holding registers)
        ///     Long            (multiple holding registers)
        ///     ULong           (multiple holding registers)
        ///
        /// </summary>
        /// <param name="request">The <see cref="ModbusRequestData"/> data.</param>
        /// <param name="data">The data value.</param>
        /// <param name="function">The function name.</param>
        /// <returns>A task returning an action method result.</returns>
        protected async Task<IActionResult> ModbusWriteArrayRequest<T>(ModbusRequestData request, T[] data, string function)
        {
            try
            {
                request.Master = _client.RtuMaster;

                if (_client.Connect())
                {
                    switch (function)
                    {
                        case "WriteCoilsAsync":
                            {
                                bool[] values = (bool[])Convert.ChangeType(data, typeof(bool[]));
                                await _client.WriteMultipleCoilsAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteHoldingRegistersAsync":
                            {
                                ushort[] values = (ushort[])Convert.ChangeType(data, typeof(ushort[]));
                                await _client.WriteMultipleRegistersAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteBoolArrayAsync":
                            {
                                bool[] values = (bool[])Convert.ChangeType(data, typeof(bool[]));
                                await _client.WriteBoolArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteBytesAsync":
                            {
                                byte[] values = (byte[])Convert.ChangeType(data, typeof(byte[]));
                                await _client.WriteBytesAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteShortArrayAsync":
                            {
                                short[] values = (short[])Convert.ChangeType(data, typeof(short[]));
                                await _client.WriteShortArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteUShortArrayAsync":
                            {
                                ushort[] values = (ushort[])Convert.ChangeType(data, typeof(ushort[]));
                                await _client.WriteUShortArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteInt32ArrayAsync":
                            {
                                int[] values = (int[])Convert.ChangeType(data, typeof(int[]));
                                await _client.WriteInt32ArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteUInt32ArrayAsync":
                            {
                                uint[] values = (uint[])Convert.ChangeType(data, typeof(uint[]));
                                await _client.WriteUInt32ArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteFloatArrayAsync":
                            {
                                float[] values = (float[])Convert.ChangeType(data, typeof(float[]));
                                await _client.WriteFloatArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteDoubleArrayAsync":
                            {
                                double[] values = (double[])Convert.ChangeType(data, typeof(double[]));
                                await _client.WriteDoubleArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteLongArrayAsync":
                            {
                                long[] values = (long[])Convert.ChangeType(data, typeof(long[]));
                                await _client.WriteLongArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        case "WriteULongArrayAsync":
                            {
                                ulong[] values = (ulong[])Convert.ChangeType(data, typeof(ulong[]));
                                await _client.WriteULongArrayAsync(request.Slave.ID, request.Offset, values);
                                _logger.LogTrace($"{function}() OK.");
                                return Ok(request);
                            }
                        default:
                            _client.Disconnect();
                            _logger.LogError($"RTU master write request {function}() not supported.");
                            return NotFound($"RTU master write request {function}() not supported.");
                    }
                }
                else
                {
                    _logger.LogError($"RTU master ({request.Master.SerialPort}) not open.");
                    return NotFound("RTU master COM port not open.");
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                _logger.LogError(uae, $"{function}() Unauthorized Access Exception.");
                return NotFound($"Unauthorized Access Exception: {uae.Message}");
            }
            catch (ArgumentOutOfRangeException are)
            {
                _logger.LogError(are, $"{function}() Argument out of Range Exception.");
                return BadRequest($"Argument out of Range Exception: {are.Message}");
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex, $"{function}() Argument Exception.");
                return BadRequest($"Argument Exception: {aex.Message}");
            }
            catch (NModbus.SlaveException mse)
            {
                _logger.LogError(mse, $"{function}() Modbus SlaveException.");
                return StatusCode(502, $"Modbus SlaveException: {mse.Message}");
            }
            catch (System.IO.IOException ioe)
            {
                _logger.LogError(ioe, $"{function}() IO Exception.");
                return StatusCode(500, $"IO Exception: {ioe.Message}");
            }
            catch (TimeoutException tex)
            {
                _logger.LogError(tex, $"{function}() Timeout Exception.");
                return StatusCode(500, $"Timeout Exception: {tex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{function}() Exception.");
                return StatusCode(500, $"Exception: {ex.Message}");
            }
            finally
            {
                if (_client.Connected)
                {
                    _client.Disconnect();
                }
            }
        }

        #endregion
    }
}
