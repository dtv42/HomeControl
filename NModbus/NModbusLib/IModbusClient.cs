// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModbusClient.cs" company="DTV-Online">
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

    #endregion

    public interface IModbusClient
    {
        IModbusMaster ModbusMaster { get; }

        #region Read Functions
        bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadCoilsAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadInputRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadInputsAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        #endregion

        #region Write Functions
        ushort[] ReadWriteMultipleRegisters(byte slaveAddress, ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);
        Task<ushort[]> ReadWriteMultipleRegistersAsync(byte slaveAddress, ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);
        void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data);
        Task WriteMultipleCoilsAsync(byte slaveAddress, ushort startAddress, bool[] data);
        void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data);
        Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data);
        void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value);
        Task WriteSingleCoilAsync(byte slaveAddress, ushort coilAddress, bool value);
        void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value);
        Task WriteSingleRegisterAsync(byte slaveAddress, ushort registerAddress, ushort value);
        #endregion

        #region Extended Read Functions
        string ReadString(byte slaveAddress, ushort startAddress, ushort numberOfCharacters);
        string ReadHexString(byte slaveAddress, ushort startAddress, ushort numberOfHex);
        bool ReadBool(byte slaveAddress, ushort startAddress);
        BitArray ReadBits(byte slaveAddress, ushort startAddress);
        short ReadShort(byte slaveAddress, ushort startAddress);
        ushort ReadUShort(byte slaveAddress, ushort startAddress);
        int ReadInt32(byte slaveAddress, ushort startAddress);
        uint ReadUInt32(byte slaveAddress, ushort startAddress);
        float ReadFloat(byte slaveAddress, ushort startAddress);
        double ReadDouble(byte slaveAddress, ushort startAddress);
        long ReadLong(byte slaveAddress, ushort startAddress);
        ulong ReadULong(byte slaveAddress, ushort startAddress);
        bool[] ReadBoolArray(byte slaveAddress, ushort startAddress, ushort length);
        byte[] ReadBytes(byte slaveAddress, ushort startAddress, ushort length);
        short[] ReadShortArray(byte slaveAddress, ushort startAddress, ushort length);
        ushort[] ReadUShortArray(byte slaveAddress, ushort startAddress, ushort length);
        int[] ReadInt32Array(byte slaveAddress, ushort startAddress, ushort length);
        uint[] ReadUInt32Array(byte slaveAddress, ushort startAddress, ushort length);
        float[] ReadFloatArray(byte slaveAddress, ushort startAddress, ushort length);
        double[] ReadDoubleArray(byte slaveAddress, ushort startAddress, ushort length);
        long[] ReadLongArray(byte slaveAddress, ushort startAddress, ushort length);
        ulong[] ReadULongArray(byte slaveAddress, ushort startAddress, ushort length);
        #endregion

        #region Extended Write Functions
        void WriteString(byte slaveAddress, ushort startAddress, string text);
        void WriteHexString(byte slaveAddress, ushort startAddress, string hex);
        void WriteBool(byte slaveAddress, ushort startAddress, bool value);
        void WriteBits(byte slaveAddress, ushort startAddress, BitArray value);
        void WriteShort(byte slaveAddress, ushort startAddress, short value);
        void WriteUShort(byte slaveAddress, ushort startAddress, ushort value);
        void WriteInt32(byte slaveAddress, ushort startAddress, int value);
        void WriteUInt32(byte slaveAddress, ushort startAddress, uint value);
        void WriteFloat(byte slaveAddress, ushort startAddress, float value);
        void WriteDouble(byte slaveAddress, ushort startAddress, double value);
        void WriteLong(byte slaveAddress, ushort startAddress, long value);
        void WriteULong(byte slaveAddress, ushort startAddress, ulong value);
        void WriteBoolArray(byte slaveAddress, ushort startAddress, bool[] values);
        void WriteBytes(byte slaveAddress, ushort startAddress, byte[] values);
        void WriteShortArray(byte slaveAddress, ushort startAddress, short[] values);
        void WriteUShortArray(byte slaveAddress, ushort startAddress, ushort[] values);
        void WriteInt32Array(byte slaveAddress, ushort startAddress, int[] values);
        void WriteUInt32Array(byte slaveAddress, ushort startAddress, uint[] values);
        void WriteFloatArray(byte slaveAddress, ushort startAddress, float[] values);
        void WriteDoubleArray(byte slaveAddress, ushort startAddress, double[] values);
        void WriteLongArray(byte slaveAddress, ushort startAddress, long[] values);
        void WriteULongArray(byte slaveAddress, ushort startAddress, ulong[] values);
        #endregion

        #region Extended Read Only Functions
        string ReadOnlyString(byte slaveAddress, ushort startAddress, ushort numberOfCharacters);
        string ReadOnlyHexString(byte slaveAddress, ushort startAddress, ushort numberOfHex);
        bool ReadOnlyBool(byte slaveAddress, ushort startAddress);
        BitArray ReadOnlyBits(byte slaveAddress, ushort startAddress);
        short ReadOnlyShort(byte slaveAddress, ushort startAddress);
        ushort ReadOnlyUShort(byte slaveAddress, ushort startAddress);
        int ReadOnlyInt32(byte slaveAddress, ushort startAddress);
        uint ReadOnlyUInt32(byte slaveAddress, ushort startAddress);
        float ReadOnlyFloat(byte slaveAddress, ushort startAddress);
        double ReadOnlyDouble(byte slaveAddress, ushort startAddress);
        long ReadOnlyLong(byte slaveAddress, ushort startAddress);
        ulong ReadOnlyULong(byte slaveAddress, ushort startAddress);
        bool[] ReadOnlyBoolArray(byte slaveAddress, ushort startAddress, ushort length);
        byte[] ReadOnlyBytes(byte slaveAddress, ushort startAddress, ushort length);
        short[] ReadOnlyShortArray(byte slaveAddress, ushort startAddress, ushort length);
        ushort[] ReadOnlyUShortArray(byte slaveAddress, ushort startAddress, ushort length);
        int[] ReadOnlyInt32Array(byte slaveAddress, ushort startAddress, ushort length);
        uint[] ReadOnlyUInt32Array(byte slaveAddress, ushort startAddress, ushort length);
        float[] ReadOnlyFloatArray(byte slaveAddress, ushort startAddress, ushort length);
        double[] ReadOnlyDoubleArray(byte slaveAddress, ushort startAddress, ushort length);
        long[] ReadOnlyLongArray(byte slaveAddress, ushort startAddress, ushort length);
        ulong[] ReadOnlyULongArray(byte slaveAddress, ushort startAddress, ushort length);
        #endregion

        #region Extended Read Functions
        Task<string> ReadStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfCharacters);
        Task<string> ReadHexStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfHex);
        Task<bool> ReadBoolAsync(byte slaveAddress, ushort startAddress);
        Task<BitArray> ReadBitsAsync(byte slaveAddress, ushort startAddress);
        Task<short> ReadShortAsync(byte slaveAddress, ushort startAddress);
        Task<ushort> ReadUShortAsync(byte slaveAddress, ushort startAddress);
        Task<Int32> ReadInt32Async(byte slaveAddress, ushort startAddress);
        Task<uint> ReadUInt32Async(byte slaveAddress, ushort startAddress);
        Task<float> ReadFloatAsync(byte slaveAddress, ushort startAddress);
        Task<double> ReadDoubleAsync(byte slaveAddress, ushort startAddress);
        Task<long> ReadLongAsync(byte slaveAddress, ushort startAddress);
        Task<ulong> ReadULongAsync(byte slaveAddress, ushort startAddress);
        Task<bool[]> ReadBoolArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<byte[]> ReadBytesAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<short[]> ReadShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<ushort[]> ReadUShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<Int32[]> ReadInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<UInt32[]> ReadUInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<float[]> ReadFloatArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<double[]> ReadDoubleArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<long[]> ReadLongArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<ulong[]> ReadULongArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        #endregion

        #region Extended Write Functions
        Task WriteStringAsync(byte slaveAddress, ushort startAddress, string text);
        Task WriteHexStringAsync(byte slaveAddress, ushort startAddress, string hex);
        Task WriteBoolAsync(byte slaveAddress, ushort startAddress, bool value);
        Task WriteBitsAsync(byte slaveAddress, ushort startAddress, BitArray value);
        Task WriteShortAsync(byte slaveAddress, ushort startAddress, short value);
        Task WriteUShortAsync(byte slaveAddress, ushort startAddress, ushort value);
        Task WriteInt32Async(byte slaveAddress, ushort startAddress, int value);
        Task WriteUInt32Async(byte slaveAddress, ushort startAddress, uint value);
        Task WriteFloatAsync(byte slaveAddress, ushort startAddress, float value);
        Task WriteDoubleAsync(byte slaveAddress, ushort startAddress, double value);
        Task WriteLongAsync(byte slaveAddress, ushort startAddress, long value);
        Task WriteULongAsync(byte slaveAddress, ushort startAddress, ulong value);
        Task WriteBoolArrayAsync(byte slaveAddress, ushort startAddress, bool[] values);
        Task WriteBytesAsync(byte slaveAddress, ushort startAddress, byte[] values);
        Task WriteShortArrayAsync(byte slaveAddress, ushort startAddress, short[] values);
        Task WriteUShortArrayAsync(byte slaveAddress, ushort startAddress, ushort[] values);
        Task WriteInt32ArrayAsync(byte slaveAddress, ushort startAddress, Int32[] values);
        Task WriteUInt32ArrayAsync(byte slaveAddress, ushort startAddress, UInt32[] values);
        Task WriteFloatArrayAsync(byte slaveAddress, ushort startAddress, float[] values);
        Task WriteDoubleArrayAsync(byte slaveAddress, ushort startAddress, double[] values);
        Task WriteLongArrayAsync(byte slaveAddress, ushort startAddress, long[] values);
        Task WriteULongArrayAsync(byte slaveAddress, ushort startAddress, ulong[] values);
        #endregion

        #region Extended Read Only Functions
        Task<string> ReadOnlyStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfCharacters);
        Task<string> ReadOnlyHexStringAsync(byte slaveAddress, ushort startAddress, ushort numberOfHex);
        Task<bool> ReadOnlyBoolAsync(byte slaveAddress, ushort startAddress);
        Task<BitArray> ReadOnlyBitsAsync(byte slaveAddress, ushort startAddress);
        Task<short> ReadOnlyShortAsync(byte slaveAddress, ushort startAddress);
        Task<ushort> ReadOnlyUShortAsync(byte slaveAddress, ushort startAddress);
        Task<Int32> ReadOnlyInt32Async(byte slaveAddress, ushort startAddress);
        Task<uint> ReadOnlyUInt32Async(byte slaveAddress, ushort startAddress);
        Task<float> ReadOnlyFloatAsync(byte slaveAddress, ushort startAddress);
        Task<double> ReadOnlyDoubleAsync(byte slaveAddress, ushort startAddress);
        Task<long> ReadOnlyLongAsync(byte slaveAddress, ushort startAddress);
        Task<ulong> ReadOnlyULongAsync(byte slaveAddress, ushort startAddress);
        Task<bool[]> ReadOnlyBoolArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<byte[]> ReadOnlyBytesAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<short[]> ReadOnlyShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<ushort[]> ReadOnlyUShortArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<Int32[]> ReadOnlyInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<UInt32[]> ReadOnlyUInt32ArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<float[]> ReadOnlyFloatArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<double[]> ReadOnlyDoubleArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<long[]> ReadOnlyLongArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        Task<ulong[]> ReadOnlyULongArrayAsync(byte slaveAddress, ushort startAddress, ushort length);
        #endregion
    }
}
