// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITcpClient.cs" company="DTV-Online">
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

    using NModbusLib.Models;

    #endregion

    public interface ITcpClient : IModbusClient, ITcpClientSettings
    {
        bool SwapBytes { get; set; }
        bool SwapWords { get; set; }
        bool Connected { get; }

        bool Connect();
        void Disconnect();

        #region Read Functions
        bool[] ReadCoils(ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints);
        ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints);
        bool[] ReadInputs(ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints);
        #endregion

        #region Write Functions
        ushort[] ReadWriteMultipleRegisters(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);
        Task<ushort[]> ReadWriteMultipleRegistersAsync(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);
        void WriteMultipleCoils(ushort startAddress, bool[] data);
        Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data);
        void WriteMultipleRegisters(ushort startAddress, ushort[] data);
        Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data);
        void WriteSingleCoil(ushort coilAddress, bool value);
        Task WriteSingleCoilAsync(ushort coilAddress, bool value);
        void WriteSingleRegister(ushort registerAddress, ushort value);
        Task WriteSingleRegisterAsync(ushort registerAddress, ushort value);
        #endregion

        #region Extended Read Functions
        string ReadString(ushort startAddress, ushort numberOfCharacters);
        string ReadHexString(ushort startAddress, ushort numberOfHex);
        bool ReadBool(ushort startAddress);
        BitArray ReadBits(ushort startAddress);
        short ReadShort(ushort startAddress);
        ushort ReadUShort(ushort startAddress);
        int ReadInt32(ushort startAddress);
        uint ReadUInt32(ushort startAddress);
        float ReadFloat(ushort startAddress);
        double ReadDouble(ushort startAddress);
        long ReadLong(ushort startAddress);
        ulong ReadULong(ushort startAddress);
        bool[] ReadBoolArray(ushort startAddress, ushort length);
        byte[] ReadBytes(ushort startAddress, ushort length);
        short[] ReadShortArray(ushort startAddress, ushort length);
        ushort[] ReadUShortArray(ushort startAddress, ushort length);
        int[] ReadInt32Array(ushort startAddress, ushort length);
        uint[] ReadUInt32Array(ushort startAddress, ushort length);
        float[] ReadFloatArray(ushort startAddress, ushort length);
        double[] ReadDoubleArray(ushort startAddress, ushort length);
        long[] ReadLongArray(ushort startAddress, ushort length);
        ulong[] ReadULongArray(ushort startAddress, ushort length);
        #endregion

        #region Extended Write Functions
        void WriteString(ushort startAddress, string text);
        void WriteHexString(ushort startAddress, string hex);
        void WriteBool(ushort startAddress, bool value);
        void WriteBits(ushort startAddress, BitArray value);
        void WriteShort(ushort startAddress, short value);
        void WriteUShort(ushort startAddress, ushort value);
        void WriteInt32(ushort startAddress, int value);
        void WriteUInt32(ushort startAddress, uint value);
        void WriteFloat(ushort startAddress, float value);
        void WriteDouble(ushort startAddress, double value);
        void WriteLong(ushort startAddress, long value);
        void WriteULong(ushort startAddress, ulong value);
        void WriteBoolArray(ushort startAddress, bool[] values);
        void WriteBytes(ushort startAddress, byte[] values);
        void WriteShortArray(ushort startAddress, short[] values);
        void WriteUShortArray(ushort startAddress, ushort[] values);
        void WriteInt32Array(ushort startAddress, int[] values);
        void WriteUInt32Array(ushort startAddress, uint[] values);
        void WriteFloatArray(ushort startAddress, float[] values);
        void WriteDoubleArray(ushort startAddress, double[] values);
        void WriteLongArray(ushort startAddress, long[] values);
        void WriteULongArray(ushort startAddress, ulong[] values);
        #endregion

        #region Extended Read Only Functions
        string ReadOnlyString(ushort startAddress, ushort numberOfCharacters);
        string ReadOnlyHexString(ushort startAddress, ushort numberOfHex);
        bool ReadOnlyBool(ushort startAddress);
        BitArray ReadOnlyBits(ushort startAddress);
        short ReadOnlyShort(ushort startAddress);
        ushort ReadOnlyUShort(ushort startAddress);
        int ReadOnlyInt32(ushort startAddress);
        uint ReadOnlyUInt32(ushort startAddress);
        float ReadOnlyFloat(ushort startAddress);
        double ReadOnlyDouble(ushort startAddress);
        long ReadOnlyLong(ushort startAddress);
        ulong ReadOnlyULong(ushort startAddress);
        bool[] ReadOnlyBoolArray(ushort startAddress, ushort length);
        byte[] ReadOnlyBytes(ushort startAddress, ushort length);
        short[] ReadOnlyShortArray(ushort startAddress, ushort length);
        ushort[] ReadOnlyUShortArray(ushort startAddress, ushort length);
        int[] ReadOnlyInt32Array(ushort startAddress, ushort length);
        uint[] ReadOnlyUInt32Array(ushort startAddress, ushort length);
        float[] ReadOnlyFloatArray(ushort startAddress, ushort length);
        double[] ReadOnlyDoubleArray(ushort startAddress, ushort length);
        long[] ReadOnlyLongArray(ushort startAddress, ushort length);
        ulong[] ReadOnlyULongArray(ushort startAddress, ushort length);
        #endregion

        #region Extended Read Functions
        Task<string> ReadStringAsync(ushort startAddress, ushort numberOfCharacters);
        Task<string> ReadHexStringAsync(ushort startAddress, ushort numberOfHex);
        Task<bool> ReadBoolAsync(ushort startAddress);
        Task<BitArray> ReadBitsAsync(ushort startAddress);
        Task<short> ReadShortAsync(ushort startAddress);
        Task<ushort> ReadUShortAsync(ushort startAddress);
        Task<Int32> ReadInt32Async(ushort startAddress);
        Task<uint> ReadUInt32Async(ushort startAddress);
        Task<float> ReadFloatAsync(ushort startAddress);
        Task<double> ReadDoubleAsync(ushort startAddress);
        Task<long> ReadLongAsync(ushort startAddress);
        Task<ulong> ReadULongAsync(ushort startAddress);
        Task<bool[]> ReadBoolArrayAsync(ushort startAddress, ushort length);
        Task<byte[]> ReadBytesAsync(ushort startAddress, ushort length);
        Task<short[]> ReadShortArrayAsync(ushort startAddress, ushort length);
        Task<ushort[]> ReadUShortArrayAsync(ushort startAddress, ushort length);
        Task<Int32[]> ReadInt32ArrayAsync(ushort startAddress, ushort length);
        Task<UInt32[]> ReadUInt32ArrayAsync(ushort startAddress, ushort length);
        Task<float[]> ReadFloatArrayAsync(ushort startAddress, ushort length);
        Task<double[]> ReadDoubleArrayAsync(ushort startAddress, ushort length);
        Task<long[]> ReadLongArrayAsync(ushort startAddress, ushort length);
        Task<ulong[]> ReadULongArrayAsync(ushort startAddress, ushort length);
        #endregion

        #region Extended Write Functions
        Task WriteStringAsync(ushort startAddress, string text);
        Task WriteHexStringAsync(ushort startAddress, string hex);
        Task WriteBoolAsync(ushort startAddress, bool value);
        Task WriteBitsAsync(ushort startAddress, BitArray value);
        Task WriteShortAsync(ushort startAddress, short value);
        Task WriteUShortAsync(ushort startAddress, ushort value);
        Task WriteInt32Async(ushort startAddress, int value);
        Task WriteUInt32Async(ushort startAddress, uint value);
        Task WriteFloatAsync(ushort startAddress, float value);
        Task WriteDoubleAsync(ushort startAddress, double value);
        Task WriteLongAsync(ushort startAddress, long value);
        Task WriteULongAsync(ushort startAddress, ulong value);
        Task WriteBoolArrayAsync(ushort startAddress, bool[] values);
        Task WriteBytesAsync(ushort startAddress, byte[] values);
        Task WriteShortArrayAsync(ushort startAddress, short[] values);
        Task WriteUShortArrayAsync(ushort startAddress, ushort[] values);
        Task WriteInt32ArrayAsync(ushort startAddress, Int32[] values);
        Task WriteUInt32ArrayAsync(ushort startAddress, UInt32[] values);
        Task WriteFloatArrayAsync(ushort startAddress, float[] values);
        Task WriteDoubleArrayAsync(ushort startAddress, double[] values);
        Task WriteLongArrayAsync(ushort startAddress, long[] values);
        Task WriteULongArrayAsync(ushort startAddress, ulong[] values);
        #endregion

        #region Extended Read Only Functions
        Task<string> ReadOnlyStringAsync(ushort startAddress, ushort numberOfCharacters);
        Task<string> ReadOnlyHexStringAsync(ushort startAddress, ushort numberOfHex);
        Task<bool> ReadOnlyBoolAsync(ushort startAddress);
        Task<BitArray> ReadOnlyBitsAsync(ushort startAddress);
        Task<short> ReadOnlyShortAsync(ushort startAddress);
        Task<ushort> ReadOnlyUShortAsync(ushort startAddress);
        Task<Int32> ReadOnlyInt32Async(ushort startAddress);
        Task<uint> ReadOnlyUInt32Async(ushort startAddress);
        Task<float> ReadOnlyFloatAsync(ushort startAddress);
        Task<double> ReadOnlyDoubleAsync(ushort startAddress);
        Task<long> ReadOnlyLongAsync(ushort startAddress);
        Task<ulong> ReadOnlyULongAsync(ushort startAddress);
        Task<bool[]> ReadOnlyBoolArrayAsync(ushort startAddress, ushort length);
        Task<byte[]> ReadOnlyBytesAsync(ushort startAddress, ushort length);
        Task<short[]> ReadOnlyShortArrayAsync(ushort startAddress, ushort length);
        Task<ushort[]> ReadOnlyUShortArrayAsync(ushort startAddress, ushort length);
        Task<Int32[]> ReadOnlyInt32ArrayAsync(ushort startAddress, ushort length);
        Task<UInt32[]> ReadOnlyUInt32ArrayAsync(ushort startAddress, ushort length);
        Task<float[]> ReadOnlyFloatArrayAsync(ushort startAddress, ushort length);
        Task<double[]> ReadOnlyDoubleArrayAsync(ushort startAddress, ushort length);
        Task<long[]> ReadOnlyLongArrayAsync(ushort startAddress, ushort length);
        Task<ulong[]> ReadOnlyULongArrayAsync(ushort startAddress, ushort length);
        #endregion
    }
}
