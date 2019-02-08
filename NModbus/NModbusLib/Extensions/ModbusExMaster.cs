// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModbusExMaster.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbus.Extensions
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Threading.Tasks;

    using NModbus;

    #endregion

    /// <summary>
    /// Implementation of Modbus operations providing asynchronous and synchronous functions.
    /// <para>
    /// Additonal data types (read/write coils and holding registers) are supported:
    /// </para>
    /// <para>
    ///     ReadString          Reads an ASCII string (multiple holding registers)
    ///     ReadHexString       Reads an HEX string (multiple holding registers)
    ///     ReadBool            Reads a boolean value (single coil)
    ///     ReadBits            Reads a 16 bit array (single holding register)
    ///     ReadShort           Reads a 16 bit integer (single holding register)
    ///     ReadUShort          Reads an unsigned 16 bit integer (single holding register)
    ///     ReadInt32           Reads a 32 bit integer (two holding registers)
    ///     ReadUInt32          Reads an unsigned 32 bit integer (two holding registers)
    ///     ReadFloat           Reads a 32 bit IEEE floating point number (two holding registers)
    ///     ReadDouble          Reads a 64 bit IEEE floating point number (four holding registers)
    ///     ReadLong            Reads a 64 bit integer (four holding registers)
    ///     ReadULong           Reads an unsigned 64 bit integer (four holding registers)
    ///     ReadBoolArray       Reads an array of boolean values (multiple coils)
    ///     ReadBytes           Reads 8 bit values (multiple holding register)
    ///     ReadShortArray      Reads an array of 16 bit integers (multiple holding register)
    ///     ReadUShortArray     Reads an array of unsigned 16 bit integer (multiple holding register)
    ///     ReadInt32Array      Reads an array of 32 bit integers (multiple holding registers)
    ///     ReadUInt32Array     Reads an array of unsigned 32 bit integers (multiple holding registers)
    ///     ReadFloatArray      Reads an array of 32 bit IEEE floating point numbers (multiple holding registers)
    ///     ReadDoubleArray     Reads an array of 64 bit IEEE floating point numbers (multiple holding registers)
    ///     ReadLongArray       Reads an array of 64 bit integers (multiple holding registers)
    ///     ReadULongArray      Reads an array of unsigned 64 bit integers (multiple holding registers)
    /// </para>
    /// <para>
    ///     WriteString         Writes an ASCII string (multiple holding registers)
    ///     WriteHexString      Writes an HEX string (multiple holding registers)
    ///     WriteBool           Writes a boolean value (single coil)
    ///     WriteBits           Writes a 16 bit array (single holding register)
    ///     WriteShort          Writes a 16 bit integer (single holding register)
    ///     WriteUShort         Writes an unsigned 16 bit integer (single holding register)
    ///     WriteInt32          Writes a 32 bit integer (two holding registers)
    ///     WriteUInt32         Writes an unsigned 32 bit integer (two holding registers)
    ///     WriteFloat          Writes a 32 bit IEEE floating point number (two holding registers)
    ///     WriteDouble         Writes a 64 bit IEEE floating point number (four holding registers)
    ///     WriteLong           Writes a 64 bit integer (four holding registers)
    ///     WriteULong          Writes an unsigned 64 bit integer (four holding registers)
    ///     WriteBoolArray      Writes an array of boolean values (multiple coils)
    ///     WriteBytes          Writes 8 bit values (multiple holding register)
    ///     WriteShortArray     Writes an array of 16 bit integers (multiple holding register)
    ///     WriteUShortArray    Writes an array of unsigned 16 bit integer (multiple holding register)
    ///     WriteInt32Array     Writes an array of 32 bit integers (multiple holding registers)
    ///     WriteUInt32Array    Writes an array of unsigned 32 bit integers (multiple holding registers)
    ///     WriteFloatArray     Writes an array of 32 bit IEEE floating point numbers (multiple holding registers)
    ///     WriteDoubleArray    Writes an array of 64 bit IEEE floating point numbers (multiple holding registers)
    ///     WriteLongArray      Writes an array of 64 bit integers (multiple holding registers)
    ///     WriteULongArray     Writes an array of unsigned 64 bit integers (multiple holding registers)
    /// </para>
    /// <para>
    /// Additonal data types (read discrete inputs and input registers) are supported:
    /// </para>
    /// <para>
    ///     ReadOnlyString      Reads an ASCII string (multiple input registers)
    ///     ReadOnlyHexString   Reads an HEX string (multiple input registers)
    ///     ReadOnlyBool        Reads a boolean value (single discrete input)
    ///     ReadOnlyBits        Reads a 16 bit array (single holding register)
    ///     ReadOnlyShort       Reads a 16 bit integer (single holding register)
    ///     ReadOnlyUShort      Reads an unsigned 16 bit integer (single input register)
    ///     ReadOnlyInt32       Reads a 32 bit integer (two input registers)
    ///     ReadOnlyUInt32      Reads an unsigned 32 bit integer (two input registers)
    ///     ReadOnlyFloat       Reads a 32 bit IEEE floating point number (two input registers)
    ///     ReadOnlyDouble      Reads a 64 bit IEEE floating point number (four input registers)
    ///     ReadOnlyLong        Reads a 64 bit integer (four input registers)
    ///     ReadOnlyULong       Reads an unsigned 64 bit integer (four input registers)
    ///     ReadOnlyBoolArray   Reads an array of boolean values (multiple discrete inputs)
    ///     ReadOnlyBytes       Reads 8 bit values (multiple input register)
    ///     ReadOnlyShortArray  Reads an array of 16 bit integers (multiple input register)
    ///     ReadOnlyUShortArray Reads an array of unsigned 16 bit integer (multiple input register)
    ///     ReadOnlyInt32Array  Reads an array of 32 bit integers (multiple input registers)
    ///     ReadOnlyUInt32Array Reads an array of unsigned 32 bit integers (multiple input registers)
    ///     ReadOnlyFloatArray  Reads an array of 32 bit IEEE floating point numbers (multiple input registers)
    ///     ReadOnlyDoubleArray Reads an array of 64 bit IEEE floating point numbers (multiple input registers)
    ///     ReadOnlyLongArray   Reads an array of 64 bit integers (multiple input registers)
    ///     ReadOnlyULongArray  Reads an array of unsigned 64 bit integers (multiple input registers)
    /// </para>
    /// </summary>
    public static class ModbusMasterEx
    {
        #region Extended Read Functions

        /// <summary>
        /// <summary>
        /// Reads an ASCII string (multiple holding register).
        /// </summary>
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>ASCII string</returns>
        public static string ReadString(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfCharacters, bool swapBytes = false) =>
            master.ReadStringAsync(slaveAddress, startAddress, numberOfCharacters, swapBytes).Result;

        /// <summary>
        /// Reads a HEX string (multiple holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of bytes to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>HEX string</returns>
        public static string ReadHexString(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfHex, bool swapBytes = false) =>
            master.ReadHexStringAsync(slaveAddress, startAddress, numberOfHex, swapBytes).Result;

        /// <summary>
        /// Reads a single boolean value.
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public static bool ReadBool(this IModbusMaster master, byte slaveAddress, ushort startAddress) =>
            master.ReadBoolAsync(slaveAddress, startAddress).Result;

        /// <summary>
        /// Reads a 16 bit array (single holding register)
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public static BitArray ReadBits(this IModbusMaster master, byte slaveAddress, ushort startAddress) =>
            master.ReadBitsAsync(slaveAddress, startAddress).Result;

        /// <summary>
        /// Reads a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>16 bit integer.</returns>
        public static short ReadShort(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false) =>
            master.ReadShortAsync(slaveAddress, startAddress, swapBytes).Result;

        /// <summary>
        /// Reads a single unsigned 16 bit integer (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public static ushort ReadUShort(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false) =>
            master.ReadUShortAsync(slaveAddress, startAddress, swapBytes).Result;

        /// <summary>
        /// Reads an 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>32 bit integer.</returns>
        public static int ReadInt32(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadInt32Async(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a single unsigned 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public static uint ReadUInt32(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadUInt32Async(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a single float value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Float value.</returns>
        public static float ReadFloat(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadFloatAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a single double value (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Double value.</returns>
        public static double ReadDouble(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadDoubleAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>64 bit integer.</returns>
        public static long ReadLong(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadLongAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public static ulong ReadULong(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadULongAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of boolean values (multiple coils).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public static bool[] ReadBoolArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length) =>
            master.ReadBoolArrayAsync(slaveAddress, startAddress, length).Result;

        /// <summary>
        /// Reads 8 bit values (multiple holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of bytes.</returns>
        public static byte[] ReadBytes(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false) =>
            master.ReadBytesAsync(slaveAddress, startAddress, length, swapBytes).Result;

        /// <summary>
        /// Reads an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public static short[] ReadShortArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false) =>
            master.ReadShortArrayAsync(slaveAddress, startAddress, length, swapBytes).Result;

        /// <summary>
        /// Reads an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public static ushort[] ReadUShortArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false) =>
            master.ReadUShortArrayAsync(slaveAddress, startAddress, length, swapBytes).Result;

        /// <summary>
        /// Reads an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public static int[] ReadInt32Array(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadInt32ArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public static uint[] ReadUInt32Array(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadUInt32ArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public static float[] ReadFloatArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadFloatArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public static double[] ReadDoubleArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadDoubleArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public static long[] ReadLongArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadLongArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public static ulong[] ReadULongArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadULongArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        #endregion

        #region Extended Write Functions

        /// <summary>
        /// Writes an ASCII string (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="text">ASCII string to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write string method.</returns>
        public static void WriteString(this IModbusMaster master, byte slaveAddress, ushort startAddress, string text, bool swapBytes = false) =>
             master.WriteStringAsync(slaveAddress, startAddress, text, swapBytes).Wait();

        /// <summary>
        /// Writes a HEX string (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="hex">HEX string to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write HEX string method.</returns>
        public static void WriteHexString(this IModbusMaster master, byte slaveAddress, ushort startAddress, string hex, bool swapBytes = false) =>
             master.WriteHexStringAsync(slaveAddress, startAddress, hex, swapBytes).Wait();

        /// <summary>
        /// Writes a single boolean value (single coil).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write bool method.</returns>
        public static void WriteBool(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool value) =>
            master.WriteBoolAsync(slaveAddress, startAddress, value).Wait();

        /// <summary>
        /// Writes a 16 bit array (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">BitArray value to be written.</param>
        /// <returns>The task representing the async void write bits method.</returns>
        public static void WriteBits(this IModbusMaster master, byte slaveAddress, ushort startAddress, BitArray value) =>
            master.WriteBitsAsync(slaveAddress, startAddress, value).Wait();

        /// <summary>
        /// Writes a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write short method.</returns>
        public static void WriteShort(this IModbusMaster master, byte slaveAddress, ushort startAddress, short value, bool swapBytes = false) =>
            master.WriteShortAsync(slaveAddress, startAddress, value, swapBytes).Wait();

        /// <summary>
        /// Writes a single unsigned 16 bit integer value.
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write unsigned short method.</returns>
        public static void WriteUShort(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort value, bool swapBytes = false) =>
            master.WriteUShortAsync(slaveAddress, startAddress, value, swapBytes).Wait();

        /// <summary>
        /// Writes a single 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write 32-bit integer method.</returns>
        public static void WriteInt32(this IModbusMaster master, byte slaveAddress, ushort startAddress, int value, bool swapBytes = false, bool swapWords = false) =>
            master.WriteInt32Async(slaveAddress, startAddress, value, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes a single unsigned 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer method.</returns>
        public static void WriteUInt32(this IModbusMaster master, byte slaveAddress, ushort startAddress, uint value, bool swapBytes = false, bool swapWords = false) =>
            master.WriteUInt32Async(slaveAddress, startAddress, value, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes a single float value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">float value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write float method.</returns>
        public static void WriteFloat(this IModbusMaster master, byte slaveAddress, ushort startAddress, float value, bool swapBytes = false, bool swapWords = false) =>
            master.WriteFloatAsync(slaveAddress, startAddress, value, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes a single double value (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">double value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write double method.</returns>
        public static void WriteDouble(this IModbusMaster master, byte slaveAddress, ushort startAddress, double value, bool swapBytes = false, bool swapWords = false) =>
            master.WriteDoubleAsync(slaveAddress, startAddress, value, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Long value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write long method.</returns>
        public static void WriteLong(this IModbusMaster master, byte slaveAddress, ushort startAddress, long value, bool swapBytes = false, bool swapWords = false) =>
            master.WriteLongAsync(slaveAddress, startAddress, value, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned long method.</returns>
        public static void WriteULong(this IModbusMaster master, byte slaveAddress, ushort startAddress, ulong value, bool swapBytes = false, bool swapWords = false) =>
            master.WriteULongAsync(slaveAddress, startAddress, value, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an array of boolean values (multiple coils)
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of boolean values to be written.</param>
        /// <returns>The task representing the async void write bool array method.</returns>
        public static void WriteBoolArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool[] values) =>
            master.WriteBoolArrayAsync(slaveAddress, startAddress, values).Wait();

        /// <summary>
        /// Writes 8 bit values (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write byte array method.</returns>
        public static void WriteBytes(this IModbusMaster master, byte slaveAddress, ushort startAddress, byte[] values) =>
            master.WriteBytesAsync(slaveAddress, startAddress, values).Wait();

        /// <summary>
        /// Writes an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of short values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write short array method.</returns>
        public static void WriteShortArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, short[] values, bool swapBytes = false) =>
            master.WriteShortArrayAsync(slaveAddress, startAddress, values, swapBytes).Wait();

        /// <summary>
        /// Writes an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned short values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write unsigned short array method.</returns>
        public static void WriteUShortArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort[] values, bool swapBytes = false) =>
            master.WriteUShortArrayAsync(slaveAddress, startAddress, values, swapBytes).Wait();

        /// <summary>
        /// Writes an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of Int32 values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write 32-bit integer array method.</returns>
        public static void WriteInt32Array(this IModbusMaster master, byte slaveAddress, ushort startAddress, int[] values, bool swapBytes = false, bool swapWords = false) =>
            master.WriteInt32ArrayAsync(slaveAddress, startAddress, values, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of UInt32 values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer array method.</returns>
        public static void WriteUInt32Array(this IModbusMaster master, byte slaveAddress, ushort startAddress, uint[] values, bool swapBytes = false, bool swapWords = false) =>
            master.WriteUInt32ArrayAsync(slaveAddress, startAddress, values, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write float array method.</returns>
        public static void WriteFloatArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, float[] values, bool swapBytes = false, bool swapWords = false) =>
            master.WriteFloatArrayAsync(slaveAddress, startAddress, values, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write double array method.</returns>
        public static void WriteDoubleArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, double[] values, bool swapBytes = false, bool swapWords = false) =>
            master.WriteDoubleArrayAsync(slaveAddress, startAddress, values, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of long values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write long array method.</returns>
        public static void WriteLongArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, long[] values, bool swapBytes = false, bool swapWords = false) =>
            master.WriteLongArrayAsync(slaveAddress, startAddress, values, swapBytes, swapWords).Wait();

        /// <summary>
        /// Writes an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned long values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned long array method.</returns>
        public static void WriteULongArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ulong[] values, bool swapBytes = false, bool swapWords = false) =>
            master.WriteULongArrayAsync(slaveAddress, startAddress, values, swapBytes, swapWords).Wait();

        #endregion

        #region Extended Read Only Functions

        /// <summary>
        /// Reads an ASCII string (multiple input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>ASCII string</returns>
        public static string ReadOnlyString(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfCharacters, bool swapBytes = false) =>
            master.ReadOnlyStringAsync(slaveAddress, startAddress, numberOfCharacters, swapBytes).Result;

        /// <summary>
        /// Reads a HEX string (multiple input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of characters to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>ASCII string</returns>
        public static string ReadOnlyHexString(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfHex, bool swapBytes = false) =>
            master.ReadOnlyHexStringAsync(slaveAddress, startAddress, numberOfHex, swapBytes).Result;


        /// <summary>
        /// Reads a single boolean value.
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public static bool ReadOnlyBool(this IModbusMaster master, byte slaveAddress, ushort startAddress) =>
            master.ReadOnlyBoolAsync(slaveAddress, startAddress).Result;

        /// <summary>
        /// Reads a 16 bit array (single input register)
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>16 bit BitArray.</returns>
        public static BitArray ReadOnlyBits(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false) =>
            master.ReadOnlyBitsAsync(slaveAddress, startAddress).Result;

        /// <summary>
        /// Reads a 16 bit integer (single input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>16 bit integer.</returns>
        public static short ReadOnlyShort(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false) =>
            master.ReadOnlyShortAsync(slaveAddress, startAddress, swapBytes).Result;

        /// <summary>
        /// Reads a single unsigned 16 bit integer (single input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public static ushort ReadOnlyUShort(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false) =>
            master.ReadOnlyUShortAsync(slaveAddress, startAddress, swapBytes).Result;

        /// <summary>
        /// Reads an 32 bit integer (two input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>32 bit integer.</returns>
        public static int ReadOnlyInt32(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyInt32Async(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a single unsigned 32 bit integer (two input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public static uint ReadOnlyUInt32(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyUInt32Async(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a single float value (two input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Float value.</returns>
        public static float ReadOnlyFloat(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyFloatAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a single double value (four input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Double value.</returns>
        public static double ReadOnlyDouble(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyDoubleAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads a 64 bit integer (four input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>64 bit integer.</returns>
        public static long ReadOnlyLong(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyLongAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an unsigned 64 bit integer (four input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public static ulong ReadOnlyULong(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyULongAsync(slaveAddress, startAddress, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of boolean values (multiple discrete inputs).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public static bool[] ReadOnlyBoolArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length) =>
            master.ReadOnlyBoolArrayAsync(slaveAddress, startAddress, length).Result;

        /// <summary>
        /// Reads 8 bit values (multiple input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Arroy of bytes.</returns>
        public static byte[] ReadOnlyBytes(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false) =>
            master.ReadOnlyBytesAsync(slaveAddress, startAddress, length, swapBytes).Result;

        /// <summary>
        /// Reads an array of 16 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public static short[] ReadOnlyShortArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false) =>
            master.ReadOnlyShortArrayAsync(slaveAddress, startAddress, length, swapBytes).Result;

        /// <summary>
        /// Reads an array of unsigned 16 bit integer (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public static ushort[] ReadOnlyUShortArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false) =>
            master.ReadOnlyUShortArrayAsync(slaveAddress, startAddress, length, swapBytes).Result;

        /// <summary>
        /// Reads an array of 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public static int[] ReadOnlyInt32Array(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyInt32ArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of unsigned 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public static uint[] ReadOnlyUInt32Array(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyUInt32ArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of 32 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public static float[] ReadOnlyFloatArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyFloatArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of 64 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public static double[] ReadOnlyDoubleArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyDoubleArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public static long[] ReadOnlyLongArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyLongArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        /// <summary>
        /// Reads an array of unsigned 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public static ulong[] ReadOnlyULongArray(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false) =>
            master.ReadOnlyULongArrayAsync(slaveAddress, startAddress, length, swapBytes, swapWords).Result;

        #endregion

        #region Extended Async Read Functions

        /// <summary>
        /// <summary>
        /// Asynchronously reads an ASCII string (multiple holding register).
        /// </summary>
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>ASCII string</returns>
        public static async Task<string> ReadStringAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfCharacters, bool swapBytes = false)
        {
            ushort numberOfRegisters = (ushort)((numberOfCharacters + 1) / 2);
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfRegisters);

            if (data.Length == numberOfRegisters)
            {
                return data.ToASCII(swapBytes);
            }
            else
            {
                throw new Exception($"ReadStringAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a HEX string (multiple holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of bytes to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>HEX string</returns>
        public static async Task<string> ReadHexStringAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfHex, bool swapBytes = false)
        {
            ushort numberOfRegisters = (ushort)((numberOfHex + 1) / 2);
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfRegisters);

            if (data.Length == numberOfRegisters)
            {
                return data.ToHex(numberOfHex, swapBytes);
            }
            else
            {
                throw new Exception($"ReadHexStringAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single boolean value.
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public static async Task<bool> ReadBoolAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress)
        {
            bool[] data = await master.ReadCoilsAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data[0];
            }
            else
            {
                throw new Exception($"ReadBoolAsync: Error in reading coil at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a 16 bit array (single holding register)
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>16 bit BitArray.</returns>
        public static async Task<BitArray> ReadBitsAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data.ToBitArray();
            }
            else
            {
                throw new Exception($"ReadBitsAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>16 bit integer.</returns>
        public static async Task<short> ReadShortAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data.ToShort(swapBytes);
            }
            else
            {
                throw new Exception($"ReadShortAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single unsigned 16 bit integer (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public static async Task<ushort> ReadUShortAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data.ToUShort(swapBytes);
            }
            else
            {
                throw new Exception($"ReadUShortAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads an 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>32 bit integer.</returns>
        public static async Task<Int32> ReadInt32Async(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 2);

            if (data.Length == 2)
            {
                return data.ToInt32(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadInt32Async: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single unsigned 32 bit integer (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public static async Task<uint> ReadUInt32Async(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 2);

            if (data.Length == 2)
            {
                return data.ToUInt32(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadUInt32Async: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single float value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Float value.</returns>
        public static async Task<float> ReadFloatAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 2);

            if (data.Length == 2)
            {
                return data.ToFloat(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadFloatAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single double value (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Double value.</returns>
        public static async Task<double> ReadDoubleAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 4);

            if (data.Length == 4)
            {
                return data.ToDouble(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadDoubleAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>64 bit integer.</returns>
        public static async Task<long> ReadLongAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 4);

            if (data.Length == 4)
            {
                return data.ToLong(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadLongAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public static async Task<ulong> ReadULongAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, 4);

            if (data.Length == 4)
            {
                return data.ToULong(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadULongAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads an array of boolean values (multiple coils).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public static async Task<bool[]> ReadBoolArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length)
        {
            bool[] data = await master.ReadCoilsAsync(slaveAddress, startAddress, length);
            return data;
        }

        /// <summary>
        /// Asynchronously reads 8 bit values (multiple holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of bytes.</returns>
        public static async Task<byte[]> ReadBytesAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false)
        {
            ushort numberOfRegisters = (ushort)((length + 1) / 2);
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfRegisters);
            byte[] bytes = data.ToBytes(swapBytes);

            if (bytes.Length > length)
            {
                Array.Resize(ref bytes, length);
            }

            return bytes;
        }

        /// <summary>
        /// Asynchronously reads an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public static async Task<short[]> ReadShortArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, length);
            short[] values = new short[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                ushort[] slice = new ushort[] { data[i] };
                values[i] = slice.ToShort(swapBytes);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public static async Task<ushort[]> ReadUShortArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, length);
            ushort[] values = new ushort[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                ushort[] slice = new ushort[] { data[i] };
                values[i] = slice.ToUShort(swapBytes);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public static async Task<Int32[]> ReadInt32ArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, (ushort)(2 * length));
            Int32[] values = new Int32[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(2 * i)], data[(2 * i) + 1] };
                values[i] = slice.ToInt32(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public static async Task<UInt32[]> ReadUInt32ArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, (ushort)(2 * length));
            UInt32[] values = new UInt32[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(2 * i)], data[(2 * i) + 1] };
                values[i] = slice.ToUInt32(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public static async Task<float[]> ReadFloatArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, (ushort)(2 * length));
            float[] values = new float[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(2 * i)], data[(2 * i) + 1] };
                values[i] = slice.ToFloat(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public static async Task<double[]> ReadDoubleArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, (ushort)(4 * length));
            double[] values = new double[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(4 * i)], data[(4 * i) + 1], data[(4 * i) + 2], data[(4 * i) + 3] };
                values[i] = slice.ToDouble(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public static async Task<long[]> ReadLongArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, (ushort)(4 * length));
            long[] values = new long[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(4 * i)], data[(4 * i) + 1], data[(4 * i) + 2], data[(4 * i) + 3] };
                values[i] = slice.ToLong(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public static async Task<ulong[]> ReadULongArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, (ushort)(4 * length));
            ulong[] values = new ulong[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(4 * i)], data[(4 * i) + 1], data[(4 * i) + 2], data[(4 * i) + 3] };
                values[i] = slice.ToULong(swapBytes, swapWords);
            }

            return values;
        }

        #endregion

        #region Extended Async Write Functions

        /// <summary>
        /// Asynchronously writes an ASCII string (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="text">ASCII string to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write string method.</returns>
        public static async Task WriteStringAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, string text, bool swapBytes = false)
        {
            ushort[] data = text.ToRegisters(swapBytes);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a HEX string (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="hex">HEX string to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write HEX string method.</returns>
        public static async Task WriteHexStringAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, string hex, bool swapBytes = false)
        {
            byte[] bytes = new byte[(hex.Length + 1) / 2];

            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            ushort[] data = bytes.ToRegisters(swapBytes);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a single boolean value (single coil).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <returns>The task representing the async void write bool method.</returns>
        public static async Task WriteBoolAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool value)
        {
            await master.WriteSingleCoilAsync(slaveAddress, startAddress, value);
        }

        /// <summary>
        /// Writes a 16 bit array (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">BitArray value to be written.</param>
        /// <returns>The task representing the async void write bits method.</returns>
        public static async Task WriteBitsAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, BitArray value)
        {
            ushort[] data = value.ToRegisters();
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a 16 bit integer (single holding register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write short method.</returns>
        public static async Task WriteShortAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, short value, bool swapBytes = false)
        {
            ushort[] data = value.ToRegisters(swapBytes);
            await master.WriteSingleRegisterAsync(slaveAddress, startAddress, data[0]);
        }

        /// <summary>
        /// Asynchronously writes a single unsigned 16 bit integer value.
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write unsigned short method.</returns>
        public static async Task WriteUShortAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort value, bool swapBytes = false)
        {
            ushort[] data = value.ToRegisters(swapBytes);
            await master.WriteSingleRegisterAsync(slaveAddress, startAddress, data[0]);
        }

        /// <summary>
        /// Asynchronously writes a single 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write 32-bit integer method.</returns>
        public static async Task WriteInt32Async(this IModbusMaster master, byte slaveAddress, ushort startAddress, int value, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = value.ToRegisters(swapBytes, swapWords);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a single unsigned 32 bit integer value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">uint value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer method.</returns>
        public static async Task WriteUInt32Async(this IModbusMaster master, byte slaveAddress, ushort startAddress, uint value, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = value.ToRegisters(swapBytes, swapWords);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a single float value (two holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">float value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write float method.</returns>
        public static async Task WriteFloatAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, float value, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = value.ToRegisters(swapBytes, swapWords);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a single double value (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">double value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write double method.</returns>
        public static async Task WriteDoubleAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, double value, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = value.ToRegisters(swapBytes, swapWords);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes a 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Long value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write long method.</returns>
        public static async Task WriteLongAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, long value, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = value.ToRegisters(swapBytes, swapWords);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an unsigned 64 bit integer (four holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="value">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned long method.</returns>
        public static async Task WriteULongAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ulong value, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = value.ToRegisters(swapBytes, swapWords);
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of boolean values (multiple coils)
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of boolean values to be written.</param>
        /// <returns>The task representing the async void write bool array method.</returns>
        public static async Task WriteBoolArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool[] values)
        {
            await master.WriteMultipleCoilsAsync(slaveAddress, startAddress, values);
        }

        /// <summary>
        /// Asynchronously writes 8 bit values (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <returns>The task representing the async void write byte array method.</returns>
        public static async Task WriteBytesAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, byte[] values)
        {
            ushort[] data = values.ToRegisters();
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of 16 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of short values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write short array method.</returns>
        public static async Task WriteShortArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, short[] values, bool swapBytes = false)
        {
            ushort[] data = new ushort[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                data[i] = values[i].ToRegisters(swapBytes)[0];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of unsigned 16 bit integer (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned short values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>The task representing the async void write unsigned short array method.</returns>
        public static async Task WriteUShortArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort[] values, bool swapBytes = false)
        {
            ushort[] data = new ushort[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                data[i] = values[i].ToRegisters(swapBytes)[0];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, values);
        }

        /// <summary>
        /// Asynchronously writes an array of 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of Int32 values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write 32-bit integer array method.</returns>
        public static async Task WriteInt32ArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, Int32[] values, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = new ushort[2 * values.Length];
            int index = 0;

            for (int i = 0; i < values.Length; i++)
            {
                ushort[] array = values[i].ToRegisters(swapBytes, swapWords);
                data[index++] = array[0];
                data[index++] = array[1];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of unsigned 32 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of UInt32 values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned 32-bit integer array method.</returns>
        public static async Task WriteUInt32ArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, UInt32[] values, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = new ushort[2 * values.Length];
            int index = 0;

            for (int i = 0; i < values.Length; i++)
            {
                ushort[] array = values[i].ToRegisters(swapBytes, swapWords);
                data[index++] = array[0];
                data[index++] = array[1];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of 32 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write float array method.</returns>
        public static async Task WriteFloatArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, float[] values, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = new ushort[2 * values.Length];
            int index = 0;

            for (int i = 0; i < values.Length; i++)
            {
                ushort[] array = values[i].ToRegisters(swapBytes, swapWords);
                data[index++] = array[0];
                data[index++] = array[1];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of 64 bit IEEE floating point numbers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Short value to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write double array method.</returns>
        public static async Task WriteDoubleArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, double[] values, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = new ushort[4 * values.Length];
            int index = 0;

            for (int i = 0; i < values.Length; i++)
            {
                ushort[] array = values[i].ToRegisters(swapBytes, swapWords);
                data[index++] = array[0];
                data[index++] = array[1];
                data[index++] = array[2];
                data[index++] = array[3];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of long values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write long array method.</returns>
        public static async Task WriteLongArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, long[] values, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = new ushort[4 * values.Length];
            int index = 0;

            for (int i = 0; i < values.Length; i++)
            {
                ushort[] array = values[i].ToRegisters(swapBytes, swapWords);
                data[index++] = array[0];
                data[index++] = array[1];
                data[index++] = array[2];
                data[index++] = array[3];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        /// <summary>
        /// Asynchronously writes an array of unsigned 64 bit integers (multiple holding registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to write values to.</param>
        /// <param name="startAddress">Address to begin writing.</param>
        /// <param name="values">Array of unsigned long values to be written.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>The task representing the async void write unsigned long array method.</returns>
        public static async Task WriteULongArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ulong[] values, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = new ushort[4 * values.Length];
            int index = 0;

            for (int i = 0; i < values.Length; i++)
            {
                ushort[] array = values[i].ToRegisters(swapBytes, swapWords);
                data[index++] = array[0];
                data[index++] = array[1];
                data[index++] = array[2];
                data[index++] = array[3];
            }

            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        #endregion

        #region Extended Async Read Only Functions

        /// <summary>
        /// Asynchronously reads an ASCII string (multiple input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfCharacters">Number of characters to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>ASCII string</returns>
        public static async Task<string> ReadOnlyStringAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfCharacters, bool swapBytes = false)
        {
            ushort numberOfRegisters = (ushort)((numberOfCharacters + 1) / 2);
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, numberOfRegisters);

            if (data.Length == numberOfRegisters)
            {
                return data.ToASCII(swapBytes);
            }
            else
            {
                throw new Exception($"ReadOnlyStringAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a HEX string (multiple input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfHex">Number of characters to read.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>ASCII string</returns>
        public static async Task<string> ReadOnlyHexStringAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort numberOfHex, bool swapBytes = false)
        {
            ushort numberOfRegisters = (ushort)((numberOfHex + 1) / 2);
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, numberOfRegisters);

            if (data.Length == numberOfRegisters)
            {
                return data.ToHex(numberOfHex, swapBytes);
            }
            else
            {
                throw new Exception($"ReadOnlyHexStringAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single boolean value.
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <returns>bool value.</returns>
        public static async Task<bool> ReadOnlyBoolAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress)
        {
            bool[] data = await master.ReadInputsAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data[0];
            }
            else
            {
                throw new Exception($"ReadOnlyBoolAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a 16 bit array (single input register)
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>16 bit BitArray.</returns>
        public static async Task<BitArray> ReadOnlyBitsAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data.ToBitArray(swapBytes);
            }
            else
            {
                throw new Exception($"ReadOnlyBitsAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a 16 bit integer (single input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>16 bit integer.</returns>
        public static async Task<short> ReadOnlyShortAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data.ToShort(swapBytes);
            }
            else
            {
                throw new Exception($"ReadOnlyShortAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single unsigned 16 bit integer (single input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Unsigned 16 bit integer.</returns>
        public static async Task<ushort> ReadOnlyUShortAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 1);

            if (data.Length == 1)
            {
                return data.ToUShort(swapBytes);
            }
            else
            {
                throw new Exception($"ReadOnlyUShortAsync: Error in reading holding register at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads an 32 bit integer (two input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>32 bit integer.</returns>
        public static async Task<Int32> ReadOnlyInt32Async(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 2);

            if (data.Length == 2)
            {
                return data.ToInt32(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadOnlyInt32Async: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single unsigned 32 bit integer (two input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 32 bit integer.</returns>
        public static async Task<uint> ReadOnlyUInt32Async(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 2);

            if (data.Length == 2)
            {
                return data.ToUInt32(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadOnlyUInt32Async: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single float value (two input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Float value.</returns>
        public static async Task<float> ReadOnlyFloatAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 2);

            if (data.Length == 2)
            {
                return data.ToFloat(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadHexStriReadOnlyFloatAsyncngAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a single double value (four input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Double value.</returns>
        public static async Task<double> ReadOnlyDoubleAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 4);

            if (data.Length == 4)
            {
                return data.ToDouble(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadOnlyDoubleAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads a 64 bit integer (four input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>64 bit integer.</returns>
        public static async Task<long> ReadOnlyLongAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 4);

            if (data.Length == 4)
            {
                return data.ToLong(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadOnlyLongAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads an unsigned 64 bit integer (four input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Unsigned 64 bit integer.</returns>
        public static async Task<ulong> ReadOnlyULongAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, 4);

            if (data.Length == 4)
            {
                return data.ToULong(swapBytes, swapWords);
            }
            else
            {
                throw new Exception($"ReadOnlyULongAsync: Error in reading holding registers at {startAddress}.");
            }
        }

        /// <summary>
        /// Asynchronously reads an array of boolean values (multiple discrete inputs).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <returns>Array of Bool values.</returns>
        public static async Task<bool[]> ReadOnlyBoolArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length)
        {
            bool[] data = await master.ReadInputsAsync(slaveAddress, startAddress, length);
            return data;
        }

        /// <summary>
        /// Asynchronously reads 8 bit values (multiple input register).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Arroy of bytes.</returns>
        public static async Task<byte[]> ReadOnlyBytesAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false)
        {
            ushort numberOfRegisters = (ushort)((length + 1) / 2);
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, numberOfRegisters);
            byte[] bytes = data.ToBytes(swapBytes);

            if (bytes.Length > length)
            {
                Array.Resize(ref bytes, length);
            }

            return bytes;
        }

        /// <summary>
        /// Asynchronously reads an array of 16 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of 16 bit integers.</returns>
        public static async Task<short[]> ReadOnlyShortArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, length);
            short[] values = new short[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                ushort[] slice = new ushort[] { data[i] };
                values[i] = slice.ToShort(swapBytes);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of unsigned 16 bit integer (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <returns>Array of unsigned 16 bit integer.</returns>
        public static async Task<ushort[]> ReadOnlyUShortArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, length);
            ushort[] values = new ushort[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                ushort[] slice = new ushort[] { data[i] };
                values[i] = slice.ToUShort(swapBytes);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit integers.</returns>
        public static async Task<Int32[]> ReadOnlyInt32ArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, (ushort)(2 * length));
            Int32[] values = new Int32[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(2 * i)], data[(2 * i) + 1] };
                values[i] = slice.ToInt32(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of unsigned 32 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 32 bit integers.</returns>
        public static async Task<UInt32[]> ReadOnlyUInt32ArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, (ushort)(2 * length));
            UInt32[] values = new UInt32[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(2 * i)], data[(2 * i) + 1] };
                values[i] = slice.ToUInt32(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 32 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 32 bit IEEE floating point numbers.</returns>
        public static async Task<float[]> ReadOnlyFloatArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, (ushort)(2 * length));
            float[] values = new float[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(2 * i)], data[(2 * i) + 1] };
                values[i] = slice.ToFloat(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 64 bit IEEE floating point numbers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit IEEE floating point numbers.</returns>
        public static async Task<double[]> ReadOnlyDoubleArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, (ushort)(4 * length));
            double[] values = new double[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(4 * i)], data[(4 * i) + 1], data[(4 * i) + 2], data[(4 * i) + 3] };
                values[i] = slice.ToDouble(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of 64 bit integers.</returns>
        public static async Task<long[]> ReadOnlyLongArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, (ushort)(4 * length));
            long[] values = new long[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(4 * i)], data[(4 * i) + 1], data[(4 * i) + 2], data[(4 * i) + 3] };
                values[i] = slice.ToLong(swapBytes, swapWords);
            }

            return values;
        }

        /// <summary>
        /// Asynchronously reads an array of unsigned 64 bit integers (multiple input registers).
        /// </summary>
        /// <param name="master">The modbus master instance.</param>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="length">Size of array.</param>
        /// <param name="swapBytes">Flag indicating that bytes are swapped.</param>
        /// <param name="swapWords">Flag indicating that words are swapped.</param>
        /// <returns>Array of unsigned 64 bit integers.</returns>
        public static async Task<ulong[]> ReadOnlyULongArrayAsync(this IModbusMaster master, byte slaveAddress, ushort startAddress, ushort length, bool swapBytes = false, bool swapWords = false)
        {
            ushort[] data = await master.ReadInputRegistersAsync(slaveAddress, startAddress, (ushort)(4 * length));
            ulong[] values = new ulong[length];

            for (int i = 0; i < length; i++)
            {
                ushort[] slice = new ushort[] { data[(4 * i)], data[(4 * i) + 1], data[(4 * i) + 2], data[(4 * i) + 3] };
                values[i] = slice.ToULong(swapBytes, swapWords);
            }

            return values;
        }

        #endregion
    }
}