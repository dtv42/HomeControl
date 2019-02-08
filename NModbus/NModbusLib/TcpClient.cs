// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpClient.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusLib
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Threading.Tasks;

    using NModbus;
    using NModbus.Extensions;
    using NModbusLib.Models;

    #endregion

    public class TcpClient : ITcpClient
    {
        #region Private Data Members

        private System.Net.Sockets.TcpClient _client;
        private ModbusFactory _factory;
        private IModbusMaster _modbus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the NModbus master.
        /// </summary>
        public IModbusMaster ModbusMaster { get => _modbus; }

        /// <summary>
        /// Gets or sets the TCP/IP Modbus master data.
        /// </summary>
        public TcpMasterData TcpMaster { get; set; } = new TcpMasterData();

        /// <summary>
        /// Gets or sets the TCP/IP Modbus slave data.
        /// </summary>
        public TcpSlaveData TcpSlave { get; set; } = new TcpSlaveData();

        /// <summary>
        /// Gets or sets the swap byte flag.
        /// </summary>
        public bool SwapBytes { get; set; }

        /// <summary>
        /// Gets or sets the swap word flag.
        /// </summary>
        public bool SwapWords { get; set; }

        /// <summary>
        /// Gets a value indicating whether the socket is connected to a host.
        /// </summary>
        public bool Connected { get => _client?.Connected ?? false; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClient"/> class.
        /// </summary>
        public TcpClient()
        {
            _factory = new ModbusFactory();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connects the client and returns the Modbus _modbus.
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                _client = new System.Net.Sockets.TcpClient();

                if (_client != null)
                {
                    _client.ExclusiveAddressUse = TcpMaster.ExclusiveAddressUse;
                    _client.ReceiveTimeout = TcpMaster.ReceiveTimeout;
                    _client.SendTimeout = TcpMaster.SendTimeout;
                    _client.Connect(TcpSlave.Address, TcpSlave.Port);

                    if (_client.Connected)
                    {
                        _modbus = _factory.CreateMaster(_client);
                        return true;
                    }
                    else
                    {
                        _client.Dispose();
                        _client = null;
                    }
                }
            }
            catch (Exception)
            {
                _client?.Dispose();
                _client = null;
            }

            _modbus = null;
            return false;
        }

        /// <summary>
        /// Disconnects the client and disposes the Modbus instance.
        /// </summary>
        public void Disconnect()
        {
            if (_client?.Connected ?? false)
            {
                _modbus?.Dispose();
                _modbus = null;
            }
        }

        #endregion

        #region Modbus Functions

        #region Read Functions

        /// <summary>
        /// Reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status.</returns>
        public bool[] ReadCoils(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoils(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoilsAsync(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegisters(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegistersAsync(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status.</returns>
        public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegisters(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegistersAsync(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status.</returns>
        public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputs(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputsAsync(TcpSlave.ID, startAddress, numberOfPoints);

        #endregion

        #region Write Functions

        /// <summary>
        /// Performs a combination of one read operation and one write operation in a single
        /// Modbus transaction. The write operation is performed before the read.
        /// </summary>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadWriteMultipleRegisters(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            => _modbus.ReadWriteMultipleRegisters(TcpSlave.ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Asynchronously performs a combination of one read operation and one write operation
        /// in a single Modbus transaction. The write operation is performed before the read.
        /// </summary>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task<ushort[]> ReadWriteMultipleRegistersAsync(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            => _modbus.ReadWriteMultipleRegistersAsync(TcpSlave.ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Writes a sequence of coils.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoils(ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoils(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Asynchronously writes a sequence of coils.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoilsAsync(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegisters(ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegisters(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Asynchronously writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegistersAsync(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoil(ushort coilAddress, bool value)
            => _modbus.WriteSingleCoil(TcpSlave.ID, coilAddress, value);

        /// <summary>
        /// Asynchronously writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleCoilAsync(ushort coilAddress, bool value)
            => _modbus.WriteSingleCoilAsync(TcpSlave.ID, coilAddress, value);

        /// <summary>
        /// Writes a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegister(ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegister(TcpSlave.ID, registerAddress, value);

        /// <summary>
        /// Asynchronously writes a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleRegisterAsync(ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegisterAsync(TcpSlave.ID, registerAddress, value);

        #endregion

        #region Extended Read Functions

        /// <summary>
        /// <summary>
        /// Reads an ASCII string (multiple holding register).
        /// </summary>
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public string ReadString(ushort startAddress, ushort numberOfCharacters)
            => _modbus.ReadString(TcpSlave.ID, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Reads a HEX string (multiple holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of bytes to read.</param>
        /// <returns>HEX string</returns>
        public string ReadHexString(ushort startAddress, ushort numberOfHex)
            => _modbus.ReadHexString(TcpSlave.ID, startAddress, numberOfHex, SwapBytes);

        /// <summary>
        /// Reads a single boolean value.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public bool ReadBool(ushort startAddress)
            => _modbus.ReadBool(TcpSlave.ID, startAddress);

        /// <summary>
        /// Reads a 16 bit array (single holding register)
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public BitArray ReadBits(ushort startAddress)
            => _modbus.ReadBits(TcpSlave.ID, startAddress);

        /// <summary>
        /// Reads a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public short ReadShort(ushort startAddress)
            => _modbus.ReadShort(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Reads a single unsigned 16 bit integer (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public ushort ReadUShort(ushort startAddress)
            => _modbus.ReadUShort(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Reads an 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public int ReadInt32(ushort startAddress)
            => _modbus.ReadInt32(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single unsigned 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public uint ReadUInt32(ushort startAddress)
            => _modbus.ReadUInt32(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single float value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public float ReadFloat(ushort startAddress)
            => _modbus.ReadFloat(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single double value (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public double ReadDouble(ushort startAddress)
            => _modbus.ReadDouble(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public long ReadLong(ushort startAddress)
            => _modbus.ReadLong(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public ulong ReadULong(ushort startAddress)
            => _modbus.ReadULong(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of boolean values (multiple coils).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public bool[] ReadBoolArray(ushort startAddress, ushort length)
            => _modbus.ReadBoolArray(TcpSlave.ID, startAddress, length);

        /// <summary>
        /// Reads 8 bit values (multiple holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] ReadBytes(ushort startAddress, ushort length)
            => _modbus.ReadBytes(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public short[] ReadShortArray(ushort startAddress, ushort length)
            => _modbus.ReadShortArray(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public ushort[] ReadUShortArray(ushort startAddress, ushort length)
            => _modbus.ReadUShortArray(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public int[] ReadInt32Array(ushort startAddress, ushort length)
            => _modbus.ReadInt32Array(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public uint[] ReadUInt32Array(ushort startAddress, ushort length)
            => _modbus.ReadUInt32Array(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public float[] ReadFloatArray(ushort startAddress, ushort length)
            => _modbus.ReadFloatArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public double[] ReadDoubleArray(ushort startAddress, ushort length)
            => _modbus.ReadDoubleArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public long[] ReadLongArray(ushort startAddress, ushort length)
            => _modbus.ReadLongArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public ulong[] ReadULongArray(ushort startAddress, ushort length)
            => _modbus.ReadULongArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #region Extended Write Functions

        /// <summary>
        /// Writes an ASCII string (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="text">ASCII string to be written.</param>
        /// <returns>The task representing the async void write string method.</returns>
        public void WriteString(ushort startAddress, string text)
             => _modbus.WriteString(TcpSlave.ID, startAddress, text, SwapBytes);

        /// <summary>
        /// Writes a HEX string (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="hex">HEX string to be written.</param>
        /// <returns>The task representing the async void write HEX string method.</returns>
        public void WriteHexString(ushort startAddress, string hex)
             => _modbus.WriteHexString(TcpSlave.ID, startAddress, hex, SwapBytes);

        /// <summary>
        /// Writes a single boolean value (single coil).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write bool method.</returns>
        public void WriteBool(ushort startAddress, bool value)
            => _modbus.WriteBool(TcpSlave.ID, startAddress, value);

        /// <summary>
        /// Writes a 16 bit array (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">BitArray value to be written.</param>
        /// <returns>The task representing the async void write bits method.</returns>
        public void WriteBits(ushort startAddress, BitArray value)
            => _modbus.WriteBits(TcpSlave.ID, startAddress, value);

        /// <summary>
        /// Writes a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write short method.</returns>
        public void WriteShort(ushort startAddress, short value)
            => _modbus.WriteShort(TcpSlave.ID, startAddress, value, SwapBytes);

        /// <summary>
        /// Writes a single unsigned 16 bit integer value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned short method.</returns>
        public void WriteUShort(ushort startAddress, ushort value)
            => _modbus.WriteUShort(TcpSlave.ID, startAddress, value, SwapBytes);

        /// <summary>
        /// Writes a single 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer method.</returns>
        public void WriteInt32(ushort startAddress, int value)
            => _modbus.WriteInt32(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a single unsigned 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer method.</returns>
        public void WriteUInt32(ushort startAddress, uint value)
            => _modbus.WriteUInt32(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a single float value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">float value to be written.</param>
        /// <returns>The task representing the async void write float method.</returns>
        public void WriteFloat(ushort startAddress, float value)
            => _modbus.WriteFloat(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a single double value (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">double value to be written.</param>
        /// <returns>The task representing the async void write double method.</returns>
        public void WriteDouble(ushort startAddress, double value)
            => _modbus.WriteDouble(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Long value to be written.</param>
        /// <returns>The task representing the async void write long method.</returns>
        public void WriteLong(ushort startAddress, long value)
            => _modbus.WriteLong(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write unsigned long method.</returns>
        public void WriteULong(ushort startAddress, ulong value)
            => _modbus.WriteULong(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of boolean values (multiple coils)
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of boolean values to be written.</param>
        /// <returns>The task representing the async void write bool array method.</returns>
        public void WriteBoolArray(ushort startAddress, bool[] values)
            => _modbus.WriteBoolArray(TcpSlave.ID, startAddress, values);

        /// <summary>
        /// Writes 8 bit values (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write byte array method.</returns>
        public void WriteBytes(ushort startAddress, byte[] values)
            => _modbus.WriteBytes(TcpSlave.ID, startAddress, values);

        /// <summary>
        /// Writes an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of short values to be written.</param>
        /// <returns>The task representing the async void write short array method.</returns>
        public void WriteShortArray(ushort startAddress, short[] values)
            => _modbus.WriteShortArray(TcpSlave.ID, startAddress, values, SwapBytes);

        /// <summary>
        /// Writes an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned short values to be written.</param>
        /// <returns>The task representing the async void write unsigned short array method.</returns>
        public void WriteUShortArray(ushort startAddress, ushort[] values)
            => _modbus.WriteUShortArray(TcpSlave.ID, startAddress, values, SwapBytes);

        /// <summary>
        /// Writes an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of Int32 values to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer array method.</returns>
        public void WriteInt32Array(ushort startAddress, int[] values)
            => _modbus.WriteInt32Array(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of UInt32 values to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer array method.</returns>
        public void WriteUInt32Array(ushort startAddress, uint[] values)
            => _modbus.WriteUInt32Array(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write float array method.</returns>
        public void WriteFloatArray(ushort startAddress, float[] values)
            => _modbus.WriteFloatArray(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write double array method.</returns>
        public void WriteDoubleArray(ushort startAddress, double[] values)
            => _modbus.WriteDoubleArray(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of long values to be written.</param>
        /// <returns>The task representing the async void write long array method.</returns>
        public void WriteLongArray(ushort startAddress, long[] values)
            => _modbus.WriteLongArray(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned long values to be written.</param>
        /// <returns>The task representing the async void write unsigned long array method.</returns>
        public void WriteULongArray(ushort startAddress, ulong[] values)
            => _modbus.WriteULongArray(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        #endregion

        #region Extended Read Only Functions

        /// <summary>
        /// Reads an ASCII string (multiple input register).
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public string ReadOnlyString(ushort startAddress, ushort numberOfCharacters)
            => _modbus.ReadOnlyString(TcpSlave.ID, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Reads a HEX string (multiple input register).
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public string ReadOnlyHexString(ushort startAddress, ushort numberOfHex)
            => _modbus.ReadOnlyHexString(TcpSlave.ID, startAddress, numberOfHex, SwapBytes);


        /// <summary>
        /// Reads a single boolean value.
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public bool ReadOnlyBool(ushort startAddress)
            => _modbus.ReadOnlyBool(TcpSlave.ID, startAddress);

        /// <summary>
        /// Reads a 16 bit array (single input register)
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public BitArray ReadOnlyBits(ushort startAddress)
            => _modbus.ReadOnlyBits(TcpSlave.ID, startAddress);

        /// <summary>
        /// Reads a 16 bit integer (single input register).
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public short ReadOnlyShort(ushort startAddress)
            => _modbus.ReadOnlyShort(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Reads a single unsigned 16 bit integer (single input register).
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public ushort ReadOnlyUShort(ushort startAddress)
            => _modbus.ReadOnlyUShort(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Reads an 32 bit integer (two input registers).
        /// </summary>

        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public int ReadOnlyInt32(ushort startAddress)
            => _modbus.ReadOnlyInt32(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single unsigned 32 bit integer (two input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public uint ReadOnlyUInt32(ushort startAddress)
            => _modbus.ReadOnlyUInt32(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single float value (two input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public float ReadOnlyFloat(ushort startAddress)
            => _modbus.ReadOnlyFloat(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single double value (four input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public double ReadOnlyDouble(ushort startAddress)
            => _modbus.ReadOnlyDouble(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a 64 bit integer (four input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public long ReadOnlyLong(ushort startAddress)
            => _modbus.ReadOnlyLong(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an unsigned 64 bit integer (four input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public ulong ReadOnlyULong(ushort startAddress)
            => _modbus.ReadOnlyULong(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of boolean values (multiple discrete inputs).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public bool[] ReadOnlyBoolArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyBoolArray(TcpSlave.ID, startAddress, length);

        /// <summary>
        /// Reads 8 bit values (multiple input register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Arroy of bytes.</returns>
        public byte[] ReadOnlyBytes(ushort startAddress, ushort length)
            => _modbus.ReadOnlyBytes(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 16 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public short[] ReadOnlyShortArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyShortArray(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of unsigned 16 bit integer (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public ushort[] ReadOnlyUShortArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyUShortArray(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public int[] ReadOnlyInt32Array(ushort startAddress, ushort length)
            => _modbus.ReadOnlyInt32Array(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public uint[] ReadOnlyUInt32Array(ushort startAddress, ushort length)
            => _modbus.ReadOnlyUInt32Array(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 32 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public float[] ReadOnlyFloatArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyFloatArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public double[] ReadOnlyDoubleArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyDoubleArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public long[] ReadOnlyLongArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyLongArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public ulong[] ReadOnlyULongArray(ushort startAddress, ushort length)
            => _modbus.ReadOnlyULongArray(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #region Extended Async Read Functions

        /// <summary>
        /// <summary>
        /// Asynchronously reads an ASCII string (multiple holding register).
        /// </summary>
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public async Task<string> ReadStringAsync(ushort startAddress, ushort numberOfCharacters)
            => await _modbus.ReadStringAsync(TcpSlave.ID, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Asynchronously reads a HEX string (multiple holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of bytes to read.</param>
        /// <returns>HEX string</returns>
        public async Task<string> ReadHexStringAsync(ushort startAddress, ushort numberOfHex)
            => await _modbus.ReadHexStringAsync(TcpSlave.ID, startAddress, numberOfHex, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single boolean value.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public async Task<bool> ReadBoolAsync(ushort startAddress)
            => await _modbus.ReadBoolAsync(TcpSlave.ID, startAddress);

        /// <summary>
        /// Asynchronously reads a 16 bit array (single holding register)
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public async Task<BitArray> ReadBitsAsync(ushort startAddress)
            => await _modbus.ReadBitsAsync(TcpSlave.ID, startAddress);

        /// <summary>
        /// Asynchronously reads a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public async Task<short> ReadShortAsync(ushort startAddress)
            => await _modbus.ReadShortAsync(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single unsigned 16 bit integer (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public async Task<ushort> ReadUShortAsync(ushort startAddress)
            => await _modbus.ReadUShortAsync(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads an 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public async Task<Int32> ReadInt32Async(ushort startAddress)
            => await _modbus.ReadInt32Async(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single unsigned 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public async Task<uint> ReadUInt32Async(ushort startAddress)
            => await _modbus.ReadUInt32Async(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single float value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public async Task<float> ReadFloatAsync(ushort startAddress)
            => await _modbus.ReadFloatAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single double value (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public async Task<double> ReadDoubleAsync(ushort startAddress)
            => await _modbus.ReadDoubleAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public async Task<long> ReadLongAsync(ushort startAddress)
            => await _modbus.ReadLongAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public async Task<ulong> ReadULongAsync(ushort startAddress)
            => await _modbus.ReadULongAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of boolean values (multiple coils).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public async Task<bool[]> ReadBoolArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadBoolArrayAsync(TcpSlave.ID, startAddress, length);

        /// <summary>
        /// Asynchronously reads 8 bit values (multiple holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of bytes.</returns>
        public async Task<byte[]> ReadBytesAsync(ushort startAddress, ushort length)
            => await _modbus.ReadBytesAsync(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public async Task<short[]> ReadShortArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadShortArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public async Task<ushort[]> ReadUShortArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadUShortArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public async Task<Int32[]> ReadInt32ArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadInt32ArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public async Task<UInt32[]> ReadUInt32ArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadUInt32ArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public async Task<float[]> ReadFloatArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadFloatArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public async Task<double[]> ReadDoubleArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadDoubleArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public async Task<long[]> ReadLongArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadLongArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public async Task<ulong[]> ReadULongArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadULongArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #region Extended Async Write Functions

        /// <summary>
        /// Asynchronously writes an ASCII string (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="text">ASCII string to be written.</param>
        /// <returns>The task representing the async void write string method.</returns>
        public async Task WriteStringAsync(ushort startAddress, string text)
            => await _modbus.WriteStringAsync(TcpSlave.ID, startAddress, text, SwapBytes);

        /// <summary>
        /// Asynchronously writes a HEX string (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="hex">HEX string to be written.</param>
        /// <returns>The task representing the async void write HEX string method.</returns>
        public async Task WriteHexStringAsync(ushort startAddress, string hex)
            => await _modbus.WriteHexStringAsync(TcpSlave.ID, startAddress, hex, SwapBytes);

        /// <summary>
        /// Asynchronously writes a single boolean value (single coil).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write bool method.</returns>
        public async Task WriteBoolAsync(ushort startAddress, bool value)
            => await _modbus.WriteBoolAsync(TcpSlave.ID, startAddress, value);

        /// <summary>
        /// Writes a 16 bit array (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">BitArray value to be written.</param>
        /// <returns>The task representing the async void write bits method.</returns>
        public async Task WriteBitsAsync(ushort startAddress, BitArray value)
            => await _modbus.WriteBitsAsync(TcpSlave.ID, startAddress, value);

        /// <summary>
        /// Asynchronously writes a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write short method.</returns>
        public async Task WriteShortAsync(ushort startAddress, short value)
            => await _modbus.WriteShortAsync(TcpSlave.ID, startAddress, value, SwapBytes);

        /// <summary>
        /// Asynchronously writes a single unsigned 16 bit integer value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned short method.</returns>
        public async Task WriteUShortAsync(ushort startAddress, ushort value)
            => await _modbus.WriteUShortAsync(TcpSlave.ID, startAddress, value, SwapBytes);

        /// <summary>
        /// Asynchronously writes a single 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer method.</returns>
        public async Task WriteInt32Async(ushort startAddress, int value)
            => await _modbus.WriteInt32Async(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a single unsigned 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer method.</returns>
        public async Task WriteUInt32Async(ushort startAddress, uint value)
            => await _modbus.WriteUInt32Async(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a single float value (two holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">float value to be written.</param>
        /// <returns>The task representing the async void write float method.</returns>
        public async Task WriteFloatAsync(ushort startAddress, float value)
            => await _modbus.WriteFloatAsync(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a single double value (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">double value to be written.</param>
        /// <returns>The task representing the async void write double method.</returns>
        public async Task WriteDoubleAsync(ushort startAddress, double value)
            => await _modbus.WriteDoubleAsync(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Long value to be written.</param>
        /// <returns>The task representing the async void write long method.</returns>
        public async Task WriteLongAsync(ushort startAddress, long value)
            => await _modbus.WriteLongAsync(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write unsigned long method.</returns>
        public async Task WriteULongAsync(ushort startAddress, ulong value)
            => await _modbus.WriteULongAsync(TcpSlave.ID, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of boolean values (multiple coils)
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of boolean values to be written.</param>
        /// <returns>The task representing the async void write bool array method.</returns>
        public async Task WriteBoolArrayAsync(ushort startAddress, bool[] values)
            => await _modbus.WriteBoolArrayAsync(TcpSlave.ID, startAddress, values);

        /// <summary>
        /// Asynchronously writes 8 bit values (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write byte array method.</returns>
        public async Task WriteBytesAsync(ushort startAddress, byte[] values)
            => await _modbus.WriteBytesAsync(TcpSlave.ID, startAddress, values);

        /// <summary>
        /// Asynchronously writes an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of short values to be written.</param>
        /// <returns>The task representing the async void write short array method.</returns>
        public async Task WriteShortArrayAsync(ushort startAddress, short[] values)
            => await _modbus.WriteShortArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes);

        /// <summary>
        /// Asynchronously writes an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned short values to be written.</param>
        /// <returns>The task representing the async void write unsigned short array method.</returns>
        public async Task WriteUShortArrayAsync(ushort startAddress, ushort[] values)
            => await _modbus.WriteUShortArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes);

        /// <summary>
        /// Asynchronously writes an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of Int32 values to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer array method.</returns>
        public async Task WriteInt32ArrayAsync(ushort startAddress, Int32[] values)
            => await _modbus.WriteInt32ArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of UInt32 values to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer array method.</returns>
        public async Task WriteUInt32ArrayAsync(ushort startAddress, UInt32[] values)
            => await _modbus.WriteUInt32ArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write float array method.</returns>
        public async Task WriteFloatArrayAsync(ushort startAddress, float[] values)
            => await _modbus.WriteFloatArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write double array method.</returns>
        public async Task WriteDoubleArrayAsync(ushort startAddress, double[] values)
            => await _modbus.WriteDoubleArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of long values to be written.</param>
        /// <returns>The task representing the async void write long array method.</returns>
        public async Task WriteLongArrayAsync(ushort startAddress, long[] values)
            => await _modbus.WriteLongArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned long values to be written.</param>
        /// <returns>The task representing the async void write unsigned long array method.</returns>
        public async Task WriteULongArrayAsync(ushort startAddress, ulong[] values)
            => await _modbus.WriteULongArrayAsync(TcpSlave.ID, startAddress, values, SwapBytes, SwapWords);

        #endregion

        #region Extended Async Read Only Functions

        /// <summary>
        /// Asynchronously reads an ASCII string (multiple input register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public async Task<string> ReadOnlyStringAsync(ushort startAddress, ushort numberOfCharacters)
            => await _modbus.ReadOnlyStringAsync(TcpSlave.ID, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Asynchronously reads a HEX string (multiple input register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public async Task<string> ReadOnlyHexStringAsync(ushort startAddress, ushort numberOfHex)
            => await _modbus.ReadOnlyHexStringAsync(TcpSlave.ID, startAddress, numberOfHex, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single boolean value.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public async Task<bool> ReadOnlyBoolAsync(ushort startAddress)
            => await _modbus.ReadOnlyBoolAsync(TcpSlave.ID, startAddress);

        /// <summary>
        /// Asynchronously reads a 16 bit array (single input register)
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public async Task<BitArray> ReadOnlyBitsAsync(ushort startAddress)
            => await _modbus.ReadOnlyBitsAsync(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads a 16 bit integer (single input register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public async Task<short> ReadOnlyShortAsync(ushort startAddress)
            => await _modbus.ReadOnlyShortAsync(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single unsigned 16 bit integer (single input register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public async Task<ushort> ReadOnlyUShortAsync(ushort startAddress)
            => await _modbus.ReadOnlyUShortAsync(TcpSlave.ID, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads an 32 bit integer (two input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public async Task<Int32> ReadOnlyInt32Async(ushort startAddress)
            => await _modbus.ReadOnlyInt32Async(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single unsigned 32 bit integer (two input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public async Task<uint> ReadOnlyUInt32Async(ushort startAddress)
            => await _modbus.ReadOnlyUInt32Async(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single float value (two input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public async Task<float> ReadOnlyFloatAsync(ushort startAddress)
            => await _modbus.ReadOnlyFloatAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single double value (four input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public async Task<double> ReadOnlyDoubleAsync(ushort startAddress)
            => await _modbus.ReadOnlyDoubleAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a 64 bit integer (four input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public async Task<long> ReadOnlyLongAsync(ushort startAddress)
            => await _modbus.ReadOnlyLongAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an unsigned 64 bit integer (four input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public async Task<ulong> ReadOnlyULongAsync(ushort startAddress)
            => await _modbus.ReadOnlyULongAsync(TcpSlave.ID, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of boolean values (multiple discrete inputs).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public async Task<bool[]> ReadOnlyBoolArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyBoolArrayAsync(TcpSlave.ID, startAddress, length);

        /// <summary>
        /// Asynchronously reads 8 bit values (multiple input register).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Arroy of bytes.</returns>
        public async Task<byte[]> ReadOnlyBytesAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyBytesAsync(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 16 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public async Task<short[]> ReadOnlyShortArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyShortArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of unsigned 16 bit integer (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public async Task<ushort[]> ReadOnlyUShortArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyUShortArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public async Task<Int32[]> ReadOnlyInt32ArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyInt32ArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public async Task<UInt32[]> ReadOnlyUInt32ArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyUInt32ArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 32 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public async Task<float[]> ReadOnlyFloatArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyFloatArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public async Task<double[]> ReadOnlyDoubleArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyDoubleArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public async Task<long[]> ReadOnlyLongArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyLongArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public async Task<ulong[]> ReadOnlyULongArrayAsync(ushort startAddress, ushort length)
            => await _modbus.ReadOnlyULongArrayAsync(TcpSlave.ID, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #endregion

        #region Modbus Functions (slave id)

        #region Read Functions

        /// <summary>
        /// Reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status.</returns>
        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoils(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadCoilsAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoilsAsync(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of holding registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of holding registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of input registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status.</returns>
        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of input registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadInputRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegistersAsync(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status.</returns>
        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputs(slaveAddress, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadInputsAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputsAsync(slaveAddress, startAddress, numberOfPoints);

        #endregion

        #region Write Functions

        /// <summary>
        /// Performs a combination of one read operation and one write operation in a single
        /// Modbus transaction. The write operation is performed before the read.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadWriteMultipleRegisters(byte slaveAddress, ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            => _modbus.ReadWriteMultipleRegisters(slaveAddress, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Asynchronously performs a combination of one read operation and one write operation
        /// in a single Modbus transaction. The write operation is performed before the read.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task<ushort[]> ReadWriteMultipleRegistersAsync(byte slaveAddress, ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            => _modbus.ReadWriteMultipleRegistersAsync(slaveAddress, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Writes a sequence of coils.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoils(slaveAddress, startAddress, data);

        /// <summary>
        /// Asynchronously writes a sequence of coils.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleCoilsAsync(byte slaveAddress, ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoilsAsync(slaveAddress, startAddress, data);

        /// <summary>
        /// Writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegisters(slaveAddress, startAddress, data);

        /// <summary>
        /// Asynchronously writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);

        /// <summary>
        /// Writes a single coil value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
            => _modbus.WriteSingleCoil(slaveAddress, coilAddress, value);

        /// <summary>
        /// Asynchronously writes a single coil value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleCoilAsync(byte slaveAddress, ushort coilAddress, bool value)
            => _modbus.WriteSingleCoilAsync(slaveAddress, coilAddress, value);

        /// <summary>
        /// Writes a single holding register.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegister(slaveAddress, registerAddress, value);

        /// <summary>
        /// Asynchronously writes a single holding register.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleRegisterAsync(byte slaveAddress, ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegisterAsync(slaveAddress, registerAddress, value);

        #endregion

        #region Extended Read Functions

        /// <summary>
        /// Reads an ASCII string (multiple holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public string ReadString(byte slaveAddress, ushort startAddress, ushort numberOfCharacters)
            => _modbus.ReadString(slaveAddress, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Reads a HEX string (multiple holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of bytes to read.</param>
        /// <returns>HEX string</returns>
        public string ReadHexString(byte slaveAddress, ushort startAddress, ushort numberOfHex)
            => _modbus.ReadHexString(slaveAddress, startAddress, numberOfHex, SwapBytes);

        /// <summary>
        /// Reads a single boolean value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public bool ReadBool(byte slaveAddress, ushort startAddress)
            => _modbus.ReadBool(slaveAddress, startAddress);

        /// <summary>
        /// Reads a 16 bit array (single holding register)
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public BitArray ReadBits(byte slaveAddress, ushort startAddress)
            => _modbus.ReadBits(slaveAddress, startAddress);

        /// <summary>
        /// Reads a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public short ReadShort(byte slaveAddress, ushort startAddress)
            => _modbus.ReadShort(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Reads a single unsigned 16 bit integer (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public ushort ReadUShort(byte slaveAddress, ushort startAddress)
            => _modbus.ReadUShort(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Reads an 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public int ReadInt32(byte slaveAddress, ushort startAddress)
            => _modbus.ReadInt32(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single unsigned 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public uint ReadUInt32(byte slaveAddress, ushort startAddress)
            => _modbus.ReadUInt32(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single float value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public float ReadFloat(byte slaveAddress, ushort startAddress)
            => _modbus.ReadFloat(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single double value (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public double ReadDouble(byte slaveAddress, ushort startAddress)
            => _modbus.ReadDouble(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public long ReadLong(byte slaveAddress, ushort startAddress)
            => _modbus.ReadLong(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public ulong ReadULong(byte slaveAddress, ushort startAddress)
            => _modbus.ReadULong(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of boolean values (multiple coils).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public bool[] ReadBoolArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadBoolArray(slaveAddress, startAddress, length);

        /// <summary>
        /// Reads 8 bit values (multiple holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of bytes.</returns>
        public byte[] ReadBytes(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadBytes(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public short[] ReadShortArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadShortArray(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public ushort[] ReadUShortArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadUShortArray(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public int[] ReadInt32Array(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadInt32Array(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public uint[] ReadUInt32Array(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadUInt32Array(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public float[] ReadFloatArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadFloatArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public double[] ReadDoubleArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadDoubleArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public long[] ReadLongArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadLongArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public ulong[] ReadULongArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadULongArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #region Extended Write Functions

        /// <summary>
        /// Writes an ASCII string (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="text">ASCII string to be written.</param>
        /// <returns>The task representing the async void write string method.</returns>
        public void WriteString(byte slaveAddress, ushort startAddress, string text)
             => _modbus.WriteString(slaveAddress, startAddress, text, SwapBytes);

        /// <summary>
        /// Writes a HEX string (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="hex">HEX string to be written.</param>
        /// <returns>The task representing the async void write HEX string method.</returns>
        public void WriteHexString(byte slaveAddress, ushort startAddress, string hex)
             => _modbus.WriteHexString(slaveAddress, startAddress, hex, SwapBytes);

        /// <summary>
        /// Writes a single boolean value (single coil).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write bool method.</returns>
        public void WriteBool(byte slaveAddress, ushort startAddress, bool value)
            => _modbus.WriteBool(slaveAddress, startAddress, value);

        /// <summary>
        /// Writes a 16 bit array (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">BitArray value to be written.</param>
        /// <returns>The task representing the async void write bits method.</returns>
        public void WriteBits(byte slaveAddress, ushort startAddress, BitArray value)
            => _modbus.WriteBits(slaveAddress, startAddress, value);

        /// <summary>
        /// Writes a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write short method.</returns>
        public void WriteShort(byte slaveAddress, ushort startAddress, short value)
            => _modbus.WriteShort(slaveAddress, startAddress, value, SwapBytes);

        /// <summary>
        /// Writes a single unsigned 16 bit integer value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned short method.</returns>
        public void WriteUShort(byte slaveAddress, ushort startAddress, ushort value)
            => _modbus.WriteUShort(slaveAddress, startAddress, value, SwapBytes);

        /// <summary>
        /// Writes a single 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer method.</returns>
        public void WriteInt32(byte slaveAddress, ushort startAddress, int value)
            => _modbus.WriteInt32(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a single unsigned 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer method.</returns>
        public void WriteUInt32(byte slaveAddress, ushort startAddress, uint value)
            => _modbus.WriteUInt32(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a single float value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">float value to be written.</param>
        /// <returns>The task representing the async void write float method.</returns>
        public void WriteFloat(byte slaveAddress, ushort startAddress, float value)
            => _modbus.WriteFloat(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a single double value (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">double value to be written.</param>
        /// <returns>The task representing the async void write double method.</returns>
        public void WriteDouble(byte slaveAddress, ushort startAddress, double value)
            => _modbus.WriteDouble(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Long value to be written.</param>
        /// <returns>The task representing the async void write long method.</returns>
        public void WriteLong(byte slaveAddress, ushort startAddress, long value)
            => _modbus.WriteLong(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write unsigned long method.</returns>
        public void WriteULong(byte slaveAddress, ushort startAddress, ulong value)
            => _modbus.WriteULong(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of boolean values (multiple coils)
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of boolean values to be written.</param>
        /// <returns>The task representing the async void write bool array method.</returns>
        public void WriteBoolArray(byte slaveAddress, ushort startAddress, bool[] values)
            => _modbus.WriteBoolArray(slaveAddress, startAddress, values);

        /// <summary>
        /// Writes 8 bit values (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write byte array method.</returns>
        public void WriteBytes(byte slaveAddress, ushort startAddress, byte[] values)
            => _modbus.WriteBytes(slaveAddress, startAddress, values);

        /// <summary>
        /// Writes an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of short values to be written.</param>
        /// <returns>The task representing the async void write short array method.</returns>
        public void WriteShortArray(byte slaveAddress, ushort startAddress, short[] values)
            => _modbus.WriteShortArray(slaveAddress, startAddress, values, SwapBytes);

        /// <summary>
        /// Writes an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned short values to be written.</param>
        /// <returns>The task representing the async void write unsigned short array method.</returns>
        public void WriteUShortArray(byte slaveAddress, ushort startAddress, ushort[] values)
            => _modbus.WriteUShortArray(slaveAddress, startAddress, values, SwapBytes);

        /// <summary>
        /// Writes an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of Int32 values to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer array method.</returns>
        public void WriteInt32Array(byte slaveAddress, ushort startAddress, int[] values)
            => _modbus.WriteInt32Array(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of UInt32 values to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer array method.</returns>
        public void WriteUInt32Array(byte slaveAddress, ushort startAddress, uint[] values)
            => _modbus.WriteUInt32Array(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write float array method.</returns>
        public void WriteFloatArray(byte slaveAddress, ushort startAddress, float[] values)
            => _modbus.WriteFloatArray(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write double array method.</returns>
        public void WriteDoubleArray(byte slaveAddress, ushort startAddress, double[] values)
            => _modbus.WriteDoubleArray(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of long values to be written.</param>
        /// <returns>The task representing the async void write long array method.</returns>
        public void WriteLongArray(byte slaveAddress, ushort startAddress, long[] values)
            => _modbus.WriteLongArray(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Writes an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned long values to be written.</param>
        /// <returns>The task representing the async void write unsigned long array method.</returns>
        public void WriteULongArray(byte slaveAddress, ushort startAddress, ulong[] values)
            => _modbus.WriteULongArray(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        #endregion

        #region Extended Read Only Functions

        /// <summary>
        /// Reads an ASCII string (multiple input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public string ReadOnlyString(byte slaveAddress, ushort startAddress, ushort numberOfCharacters)
            => _modbus.ReadOnlyString(slaveAddress, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Reads a HEX string (multiple input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public string ReadOnlyHexString(byte slaveAddress, ushort startAddress, ushort numberOfHex)
            => _modbus.ReadOnlyHexString(slaveAddress, startAddress, numberOfHex, SwapBytes);


        /// <summary>
        /// Reads a single boolean value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public bool ReadOnlyBool(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyBool(slaveAddress, startAddress);

        /// <summary>
        /// Reads a 16 bit array (single input register)
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public BitArray ReadOnlyBits(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyBits(slaveAddress, startAddress);

        /// <summary>
        /// Reads a 16 bit integer (single input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public short ReadOnlyShort(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyShort(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Reads a single unsigned 16 bit integer (single input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public ushort ReadOnlyUShort(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyUShort(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Reads an 32 bit integer (two input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public int ReadOnlyInt32(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyInt32(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single unsigned 32 bit integer (two input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public uint ReadOnlyUInt32(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyUInt32(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single float value (two input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public float ReadOnlyFloat(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyFloat(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a single double value (four input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public double ReadOnlyDouble(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyDouble(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads a 64 bit integer (four input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public long ReadOnlyLong(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyLong(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an unsigned 64 bit integer (four input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public ulong ReadOnlyULong(byte slaveAddress, ushort startAddress)
            => _modbus.ReadOnlyULong(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of boolean values (multiple discrete inputs).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public bool[] ReadOnlyBoolArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyBoolArray(slaveAddress, startAddress, length);

        /// <summary>
        /// Reads 8 bit values (multiple input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Arroy of bytes.</returns>
        public byte[] ReadOnlyBytes(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyBytes(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 16 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public short[] ReadOnlyShortArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyShortArray(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of unsigned 16 bit integer (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public ushort[] ReadOnlyUShortArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyUShortArray(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Reads an array of 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public int[] ReadOnlyInt32Array(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyInt32Array(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public uint[] ReadOnlyUInt32Array(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyUInt32Array(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 32 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public float[] ReadOnlyFloatArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyFloatArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public double[] ReadOnlyDoubleArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyDoubleArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public long[] ReadOnlyLongArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyLongArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Reads an array of unsigned 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public ulong[] ReadOnlyULongArray(byte slaveAddress, ushort startAddress, ushort length)
            => _modbus.ReadOnlyULongArray(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #region Extended Async Read Functions

        /// <summary>
        /// Asynchronously reads an ASCII string (multiple holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public async Task<string> ReadStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfCharacters)
            => await _modbus.ReadStringAsync(slaveAddress, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Asynchronously reads a HEX string (multiple holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of bytes to read.</param>
        /// <returns>HEX string</returns>
        public async Task<string> ReadHexStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfHex)
            => await _modbus.ReadHexStringAsync(slaveAddress, startAddress, numberOfHex, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single boolean value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public async Task<bool> ReadBoolAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadBoolAsync(slaveAddress, startAddress);

        /// <summary>
        /// Asynchronously reads a 16 bit array (single holding register)
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public async Task<BitArray> ReadBitsAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadBitsAsync(slaveAddress, startAddress);

        /// <summary>
        /// Asynchronously reads a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public async Task<short> ReadShortAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadShortAsync(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single unsigned 16 bit integer (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public async Task<ushort> ReadUShortAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadUShortAsync(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads an 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public async Task<Int32> ReadInt32Async(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadInt32Async(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single unsigned 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public async Task<uint> ReadUInt32Async(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadUInt32Async(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single float value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public async Task<float> ReadFloatAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadFloatAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single double value (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public async Task<double> ReadDoubleAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadDoubleAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public async Task<long> ReadLongAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadLongAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public async Task<ulong> ReadULongAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadULongAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of boolean values (multiple coils).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public async Task<bool[]> ReadBoolArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadBoolArrayAsync(slaveAddress, startAddress, length);

        /// <summary>
        /// Asynchronously reads 8 bit values (multiple holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of bytes.</returns>
        public async Task<byte[]> ReadBytesAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadBytesAsync(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public async Task<short[]> ReadShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadShortArrayAsync(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public async Task<ushort[]> ReadUShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadUShortArrayAsync(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public async Task<Int32[]> ReadInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadInt32ArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public async Task<UInt32[]> ReadUInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadUInt32ArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public async Task<float[]> ReadFloatArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadFloatArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public async Task<double[]> ReadDoubleArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadDoubleArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public async Task<long[]> ReadLongArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadLongArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public async Task<ulong[]> ReadULongArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadULongArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #region Extended Async Write Functions

        /// <summary>
        /// Asynchronously writes an ASCII string (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="text">ASCII string to be written.</param>
        /// <returns>The task representing the async void write string method.</returns>
        public async Task WriteStringAsync(byte slaveAddress, ushort startAddress, string text)
            => await _modbus.WriteStringAsync(slaveAddress, startAddress, text, SwapBytes);

        /// <summary>
        /// Asynchronously writes a HEX string (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="hex">HEX string to be written.</param>
        /// <returns>The task representing the async void write HEX string method.</returns>
        public async Task WriteHexStringAsync(byte slaveAddress, ushort startAddress, string hex)
            => await _modbus.WriteHexStringAsync(slaveAddress, startAddress, hex, SwapBytes);

        /// <summary>
        /// Asynchronously writes a single boolean value (single coil).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write bool method.</returns>
        public async Task WriteBoolAsync(byte slaveAddress, ushort startAddress, bool value)
            => await _modbus.WriteBoolAsync(slaveAddress, startAddress, value);

        /// <summary>
        /// Writes a 16 bit array (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">BitArray value to be written.</param>
        /// <returns>The task representing the async void write bits method.</returns>
        public async Task WriteBitsAsync(byte slaveAddress, ushort startAddress, BitArray value)
            => await _modbus.WriteBitsAsync(slaveAddress, startAddress, value);

        /// <summary>
        /// Asynchronously writes a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write short method.</returns>
        public async Task WriteShortAsync(byte slaveAddress, ushort startAddress, short value)
            => await _modbus.WriteShortAsync(slaveAddress, startAddress, value, SwapBytes);

        /// <summary>
        /// Asynchronously writes a single unsigned 16 bit integer value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned short method.</returns>
        public async Task WriteUShortAsync(byte slaveAddress, ushort startAddress, ushort value)
            => await _modbus.WriteUShortAsync(slaveAddress, startAddress, value, SwapBytes);

        /// <summary>
        /// Asynchronously writes a single 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer method.</returns>
        public async Task WriteInt32Async(byte slaveAddress, ushort startAddress, int value)
            => await _modbus.WriteInt32Async(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a single unsigned 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer method.</returns>
        public async Task WriteUInt32Async(byte slaveAddress, ushort startAddress, uint value)
            => await _modbus.WriteUInt32Async(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a single float value (two holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">float value to be written.</param>
        /// <returns>The task representing the async void write float method.</returns>
        public async Task WriteFloatAsync(byte slaveAddress, ushort startAddress, float value)
            => await _modbus.WriteFloatAsync(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a single double value (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">double value to be written.</param>
        /// <returns>The task representing the async void write double method.</returns>
        public async Task WriteDoubleAsync(byte slaveAddress, ushort startAddress, double value)
            => await _modbus.WriteDoubleAsync(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Long value to be written.</param>
        /// <returns>The task representing the async void write long method.</returns>
        public async Task WriteLongAsync(byte slaveAddress, ushort startAddress, long value)
            => await _modbus.WriteLongAsync(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <returns>The task representing the async void write unsigned long method.</returns>
        public async Task WriteULongAsync(byte slaveAddress, ushort startAddress, ulong value)
            => await _modbus.WriteULongAsync(slaveAddress, startAddress, value, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of boolean values (multiple coils)
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of boolean values to be written.</param>
        /// <returns>The task representing the async void write bool array method.</returns>
        public async Task WriteBoolArrayAsync(byte slaveAddress, ushort startAddress, bool[] values)
            => await _modbus.WriteBoolArrayAsync(slaveAddress, startAddress, values);

        /// <summary>
        /// Asynchronously writes 8 bit values (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write byte array method.</returns>
        public async Task WriteBytesAsync(byte slaveAddress, ushort startAddress, byte[] values)
            => await _modbus.WriteBytesAsync(slaveAddress, startAddress, values);

        /// <summary>
        /// Asynchronously writes an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of short values to be written.</param>
        /// <returns>The task representing the async void write short array method.</returns>
        public async Task WriteShortArrayAsync(byte slaveAddress, ushort startAddress, short[] values)
            => await _modbus.WriteShortArrayAsync(slaveAddress, startAddress, values, SwapBytes);

        /// <summary>
        /// Asynchronously writes an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned short values to be written.</param>
        /// <returns>The task representing the async void write unsigned short array method.</returns>
        public async Task WriteUShortArrayAsync(byte slaveAddress, ushort startAddress, ushort[] values)
            => await _modbus.WriteUShortArrayAsync(slaveAddress, startAddress, values, SwapBytes);

        /// <summary>
        /// Asynchronously writes an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of Int32 values to be written.</param>
        /// <returns>The task representing the async void write 32-bit integer array method.</returns>
        public async Task WriteInt32ArrayAsync(byte slaveAddress, ushort startAddress, Int32[] values)
            => await _modbus.WriteInt32ArrayAsync(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of UInt32 values to be written.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer array method.</returns>
        public async Task WriteUInt32ArrayAsync(byte slaveAddress, ushort startAddress, UInt32[] values)
            => await _modbus.WriteUInt32ArrayAsync(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write float array method.</returns>
        public async Task WriteFloatArrayAsync(byte slaveAddress, ushort startAddress, float[] values)
            => await _modbus.WriteFloatArrayAsync(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write double array method.</returns>
        public async Task WriteDoubleArrayAsync(byte slaveAddress, ushort startAddress, double[] values)
            => await _modbus.WriteDoubleArrayAsync(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of long values to be written.</param>
        /// <returns>The task representing the async void write long array method.</returns>
        public async Task WriteLongArrayAsync(byte slaveAddress, ushort startAddress, long[] values)
            => await _modbus.WriteLongArrayAsync(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously writes an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned long values to be written.</param>
        /// <returns>The task representing the async void write unsigned long array method.</returns>
        public async Task WriteULongArrayAsync(byte slaveAddress, ushort startAddress, ulong[] values)
            => await _modbus.WriteULongArrayAsync(slaveAddress, startAddress, values, SwapBytes, SwapWords);

        #endregion

        #region Extended Async Read Only Functions

        /// <summary>
        /// Asynchronously reads an ASCII string (multiple input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public async Task<string> ReadOnlyStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfCharacters)
            => await _modbus.ReadOnlyStringAsync(slaveAddress, startAddress, numberOfCharacters, SwapBytes);

        /// <summary>
        /// Asynchronously reads a HEX string (multiple input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of characters to read.</param>
        /// <returns>ASCII string</returns>
        public async Task<string> ReadOnlyHexStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfHex)
            => await _modbus.ReadOnlyHexStringAsync(slaveAddress, startAddress, numberOfHex, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single boolean value.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public async Task<bool> ReadOnlyBoolAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyBoolAsync(slaveAddress, startAddress);

        /// <summary>
        /// Asynchronously reads a 16 bit array (single input register)
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public async Task<BitArray> ReadOnlyBitsAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyBitsAsync(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads a 16 bit integer (single input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit integer.</returns>
        public async Task<short> ReadOnlyShortAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyShortAsync(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads a single unsigned 16 bit integer (single input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public async Task<ushort> ReadOnlyUShortAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyUShortAsync(slaveAddress, startAddress, SwapBytes);

        /// <summary>
        /// Asynchronously reads an 32 bit integer (two input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>32 bit integer.</returns>
        public async Task<Int32> ReadOnlyInt32Async(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyInt32Async(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single unsigned 32 bit integer (two input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public async Task<uint> ReadOnlyUInt32Async(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyUInt32Async(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single float value (two input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Float value.</returns>
        public async Task<float> ReadOnlyFloatAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyFloatAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a single double value (four input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Double value.</returns>
        public async Task<double> ReadOnlyDoubleAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyDoubleAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads a 64 bit integer (four input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>64 bit integer.</returns>
        public async Task<long> ReadOnlyLongAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyLongAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an unsigned 64 bit integer (four input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public async Task<ulong> ReadOnlyULongAsync(byte slaveAddress, ushort startAddress)
            => await _modbus.ReadOnlyULongAsync(slaveAddress, startAddress, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of boolean values (multiple discrete inputs).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public async Task<bool[]> ReadOnlyBoolArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyBoolArrayAsync(slaveAddress, startAddress, length);

        /// <summary>
        /// Asynchronously reads 8 bit values (multiple input register).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Arroy of bytes.</returns>
        public async Task<byte[]> ReadOnlyBytesAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyBytesAsync(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 16 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public async Task<short[]> ReadOnlyShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyShortArrayAsync(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of unsigned 16 bit integer (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public async Task<ushort[]> ReadOnlyUShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyUShortArrayAsync(slaveAddress, startAddress, length, SwapBytes);

        /// <summary>
        /// Asynchronously reads an array of 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public async Task<Int32[]> ReadOnlyInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyInt32ArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public async Task<UInt32[]> ReadOnlyUInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyUInt32ArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 32 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public async Task<float[]> ReadOnlyFloatArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyFloatArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public async Task<double[]> ReadOnlyDoubleArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyDoubleArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public async Task<long[]> ReadOnlyLongArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyLongArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        /// <summary>
        /// Asynchronously reads an array of unsigned 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public async Task<ulong[]> ReadOnlyULongArrayAsync(byte slaveAddress, ushort startAddress, ushort length)
            => await _modbus.ReadOnlyULongArrayAsync(slaveAddress, startAddress, length, SwapBytes, SwapWords);

        #endregion

        #endregion
    }
}
