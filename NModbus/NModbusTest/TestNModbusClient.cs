// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestNModbusClient.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusTest
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Threading.Tasks;

    using Xunit;
    using NModbus;
    using NModbus.Extensions;
    using NModbusLib;

    #endregion

    /// <summary>
    /// Test class for testing the Modbus TCP routines.
    /// </summary>
    public class TestModbusClient : IDisposable
    {
        #region Private Data Members

        private const string MODBUS_IP = "127.0.0.1";
        private const int MODBUS_PORT = 502;
        private const byte MODBUS_SLAVE = 1;

        private TcpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// The test class initialization.
        /// </summary>
        public TestModbusClient()
        {
            _client = new TcpClient();
            _client.TcpSlave.Address = MODBUS_IP;
            _client.TcpSlave.Port = MODBUS_PORT;
            _client.TcpSlave.ID = MODBUS_SLAVE;
        }

        /// <summary>
        /// The test class cleanup.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Freeing managed resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client.Connected)
                {
                    _client.Disconnect();
                }
            }
        }

        #endregion

        [Fact]
        public async Task TestInput()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            bool[] data = await _client.ReadInputsAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestCoil()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            bool[] data = await _client.ReadCoilsAsync(0, 1);
            Assert.Single(data);
            await _client.WriteSingleCoilAsync(0, true);
            data = await _client.ReadCoilsAsync(0, 1);
            Assert.Single(data);
            Assert.True(data[0]);
            await _client.WriteSingleCoilAsync(0, false);
            data = await _client.ReadCoilsAsync(0, 1);
            Assert.Single(data);
            Assert.False(data[0]);
        }

        [Fact]
        public async Task TestCoils()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            bool[] data = await _client.ReadCoilsAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteMultipleCoilsAsync(0, new bool[] { true, true, true, true, true });
            data = await _client.ReadCoilsAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { true, true, true, true, true }, data);
            await _client.WriteMultipleCoilsAsync(0, new bool[] { false, false, false, false, false });
            data = await _client.ReadCoilsAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { false, false, false, false, false }, data);
        }

        [Fact]
        public async Task TestInputRegisters()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ushort[] data = await _client.ReadInputRegistersAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestHoldingRegister()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ushort[] data = await _client.ReadHoldingRegistersAsync(0, 1);
            Assert.Single(data);
            await _client.WriteSingleRegisterAsync(0, 1);
            data = await _client.ReadHoldingRegistersAsync(0, 1);
            Assert.Single(data);
            Assert.Equal(1, data[0]);
            await _client.WriteSingleRegisterAsync(0, 0);
            data = await _client.ReadHoldingRegistersAsync(0, 1);
            Assert.Single(data);
            Assert.Equal(0, data[0]);
        }

        [Fact]
        public async Task TestHoldingRegisters()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ushort[] data = await _client.ReadHoldingRegistersAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteMultipleRegistersAsync(0, new ushort[] { 1, 2, 3, 4, 5 });
            data = await _client.ReadHoldingRegistersAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 1, 2, 3, 4, 5 }, data);
            await _client.WriteMultipleRegistersAsync(0, new ushort[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadHoldingRegistersAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 0, 0, 0, 0, 0 }, data);
        }

        [Fact]
        public async Task TestBool()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            bool? data = await _client.ReadBoolAsync(0);
            Assert.NotNull(data);
            await _client.WriteBoolAsync(0, true);
            data = await _client.ReadBoolAsync(0);
            Assert.NotNull(data);
            Assert.True(data);
            await _client.WriteBoolAsync(0, false);
            data = await _client.ReadBoolAsync(0);
            Assert.NotNull(data);
            Assert.False(data);
            data = await _client.ReadOnlyBoolAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestBits()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            BitArray data = await _client.ReadBitsAsync(0);
            Assert.Equal(16, data.Length);
            await _client.WriteBitsAsync(0, new BitArray(16, true));
            data = await _client.ReadBitsAsync(0);
            Assert.Equal(16, data.Length);
            Assert.Equal(new BitArray(16, true), data);
            await _client.WriteBitsAsync(0, new BitArray(16, false));
            data = await _client.ReadBitsAsync(0);
            Assert.Equal(16, data.Length);
            Assert.Equal(new BitArray(16, false), data);
            data = await _client.ReadOnlyBitsAsync(0);
            Assert.Equal(16, data.Length);
        }

        [Fact]
        public async Task TestShort()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            short? data = await _client.ReadShortAsync(0);
            Assert.NotNull(data);
            await _client.WriteShortAsync(0, -2);
            data = await _client.ReadShortAsync(0);
            Assert.NotNull(data);
            Assert.Equal((short)-2, data);
            await _client.WriteShortAsync(0, 0);
            data = await _client.ReadShortAsync(0);
            Assert.NotNull(data);
            Assert.Equal((short)0, data);
            data = await _client.ReadOnlyShortAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestUShort()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ushort? data = await _client.ReadUShortAsync(0);
            Assert.NotNull(data);
            await _client.WriteUShortAsync(0, 2);
            data = await _client.ReadUShortAsync(0);
            Assert.NotNull(data);
            Assert.Equal((ushort)2, data);
            await _client.WriteUShortAsync(0, 0);
            data = await _client.ReadUShortAsync(0);
            Assert.NotNull(data);
            Assert.Equal((ushort)0, data);
            data = await _client.ReadOnlyUShortAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestInt32()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            int? data = await _client.ReadInt32Async(0);
            Assert.NotNull(data);
            await _client.WriteInt32Async(0, -2000);
            data = await _client.ReadInt32Async(0);
            Assert.NotNull(data);
            Assert.Equal(-2000, data);
            await _client.WriteInt32Async(0, 0);
            data = await _client.ReadInt32Async(0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await _client.ReadOnlyInt32Async(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestUInt32()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            uint? data = await _client.ReadUInt32Async(0);
            Assert.NotNull(data);
            await _client.WriteUInt32Async(0, 2000);
            data = await _client.ReadUInt32Async(0);
            Assert.NotNull(data);
            Assert.Equal((uint)2000, data);
            await _client.WriteUInt32Async(0, 0);
            data = await _client.ReadUInt32Async(0);
            Assert.NotNull(data);
            Assert.Equal((uint)0, data);
            data = await _client.ReadOnlyUInt32Async(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestFloat()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            float? data = await _client.ReadFloatAsync(0);
            Assert.NotNull(data);
            await _client.WriteFloatAsync(0, 1.234F);
            data = await _client.ReadFloatAsync(0);
            Assert.NotNull(data);
            Assert.Equal(1.234F, data);
            await _client.WriteFloatAsync(0, 0);
            data = await _client.ReadFloatAsync(0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await _client.ReadOnlyFloatAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestDouble()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            double? data = await _client.ReadDoubleAsync(0);
            Assert.NotNull(data);
            await _client.WriteDoubleAsync(0, 1.23456789);
            data = await _client.ReadDoubleAsync(0);
            Assert.NotNull(data);
            Assert.Equal(1.23456789, data);
            await _client.WriteDoubleAsync(0, 0);
            data = await _client.ReadDoubleAsync(0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await _client.ReadOnlyDoubleAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestLong()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            long? data = await _client.ReadLongAsync(0);
            Assert.NotNull(data);
            await _client.WriteLongAsync(0, -2000000);
            data = await _client.ReadLongAsync(0);
            Assert.NotNull(data);
            Assert.Equal(-2000000, data);
            await _client.WriteLongAsync(0, 0);
            data = await _client.ReadLongAsync(0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await _client.ReadOnlyLongAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestULong()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ulong? data = await _client.ReadULongAsync(0);
            Assert.NotNull(data);
            await _client.WriteULongAsync(0, 2000000);
            data = await _client.ReadULongAsync(0);
            Assert.NotNull(data);
            Assert.Equal((ulong)2000000, data);
            await _client.WriteULongAsync(0, 0);
            data = await _client.ReadULongAsync(0);
            Assert.NotNull(data);
            Assert.Equal((ulong)0, data);
            data = await _client.ReadOnlyULongAsync(0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestBoolArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            bool[] data = await _client.ReadBoolArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteBoolArrayAsync(0, new bool[] { true, false, true, true, false });
            data = await _client.ReadBoolArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { true, false, true, true, false }, data);
            await _client.WriteBoolArrayAsync(0, new bool[] { false, false, false, false, false });
            data = await _client.ReadBoolArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { false, false, false, false, false }, data);
            data = await _client.ReadOnlyBoolArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestShortArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            short[] data = await _client.ReadShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteShortArrayAsync(0, new short[] { -1, -2, -3, -4, -5 });
            data = await _client.ReadShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new short[] { -1, -2, -3, -4, -5 }, data);
            await _client.WriteShortArrayAsync(0, new short[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new short[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestUShortArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ushort[] data = await _client.ReadUShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteUShortArrayAsync(0, new ushort[] { 1, 2, 3, 4, 5 });
            data = await _client.ReadUShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 1, 2, 3, 4, 5 }, data);
            await _client.WriteUShortArrayAsync(0, new ushort[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadUShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyUShortArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestInt32ArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            int[] data = await _client.ReadInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteInt32ArrayAsync(0, new int[] { -1, -2, -3, -4, -5 });
            data = await _client.ReadInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new int[] { -1, -2, -3, -4, -5 }, data);
            await _client.WriteInt32ArrayAsync(0, new int[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new int[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestUInt32ArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            uint[] data = await _client.ReadUInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteUInt32ArrayAsync(0, new uint[] { 1, 2, 3, 4, 5 });
            data = await _client.ReadUInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new uint[] { 1, 2, 3, 4, 5 }, data);
            await _client.WriteUInt32ArrayAsync(0, new uint[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadUInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new uint[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyUInt32ArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestFloatArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            float[] data = await _client.ReadFloatArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteFloatArrayAsync(0, new float[] { 0.12345F, 1.2345F, 12.345F, 123.45F, 1234.5F });
            data = await _client.ReadFloatArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new float[] { 0.12345F, 1.2345F, 12.345F, 123.45F, 1234.5F }, data);
            await _client.WriteFloatArrayAsync(0, new float[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadFloatArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new float[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyFloatArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestDoubleArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            double[] data = await _client.ReadDoubleArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteDoubleArrayAsync(0, new double[] { 0.12345, 1.2345, 12.345, 123.45, 1234.5 });
            data = await _client.ReadDoubleArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new double[] { 0.12345, 1.2345, 12.345, 123.45, 1234.5 }, data);
            await _client.WriteDoubleArrayAsync(0, new double[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadDoubleArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new double[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyDoubleArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestLongArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            long[] data = await _client.ReadLongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteLongArrayAsync(0, new long[] { -1, -2, -3, -4, -5 });
            data = await _client.ReadLongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new long[] { -1, -2, -3, -4, -5 }, data);
            await _client.WriteLongArrayAsync(0, new long[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadLongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new long[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyLongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestULongArrayAsync()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            ulong[] data = await _client.ReadULongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteULongArrayAsync(0, new ulong[] { 1, 2, 3, 4, 5 });
            data = await _client.ReadULongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ulong[] { 1, 2, 3, 4, 5 }, data);
            await _client.WriteULongArrayAsync(0, new ulong[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadULongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ulong[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyULongArrayAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestBytes()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            byte[] data = await _client.ReadBytesAsync(0, 5);
            Assert.Equal(5, data.Length);
            await _client.WriteBytesAsync(0, new byte[] { 1, 2, 3, 4, 5 });
            data = await _client.ReadBytesAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5 }, data);
            await _client.WriteBytesAsync(0, new byte[] { 0, 0, 0, 0, 0 });
            data = await _client.ReadBytesAsync(0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new byte[] { 0, 0, 0, 0, 0 }, data);
            data = await _client.ReadOnlyBytesAsync(0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestString()
        {
            _client.Connect();
            Assert.True(_client.Connected);
            await _client.WriteStringAsync(0, "Hello");
            string text = await _client.ReadStringAsync(0, 5);
            Assert.Equal(5, text.Length);
            Assert.Equal("Hello", text);
            text = await _client.ReadOnlyStringAsync(0, 5);
        }
    }
}
