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
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using Xunit;
    using NModbus;

    using NModbus.Extensions;

    #endregion

    /// <summary>
    /// Test class for testing the Modbus TCP routines.
    /// </summary>
    public class TestModbusSlave : IDisposable
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
        public TestModbusSlave()
        {
            _client = new TcpClient();
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
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }
        }

        #endregion

        [Fact]
        public async Task TestInput()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            bool[] data = await modbus.ReadInputsAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestCoil()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            bool[] data = await modbus.ReadCoilsAsync(MODBUS_SLAVE, 0, 1);
            Assert.Single(data);
            await modbus.WriteSingleCoilAsync(MODBUS_SLAVE, 0, true);
            data = await modbus.ReadCoilsAsync(MODBUS_SLAVE, 0, 1);
            Assert.Single(data);
            Assert.True(data[0]);
            await modbus.WriteSingleCoilAsync(MODBUS_SLAVE, 0, false);
            data = await modbus.ReadCoilsAsync(MODBUS_SLAVE, 0, 1);
            Assert.Single(data);
            Assert.False(data[0]);
        }

        [Fact]
        public async Task TestCoils()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            bool[] data = await modbus.ReadCoilsAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteMultipleCoilsAsync(MODBUS_SLAVE, 0, new bool[] { true, true, true, true, true });
            data = await modbus.ReadCoilsAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { true, true, true, true, true }, data);
            await modbus.WriteMultipleCoilsAsync(MODBUS_SLAVE, 0, new bool[] { false, false, false, false, false });
            data = await modbus.ReadCoilsAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { false, false, false, false, false }, data);
        }

        [Fact]
        public async Task TestInputRegisters()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ushort[] data = await modbus.ReadInputRegistersAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestHoldingRegister()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ushort[] data = await modbus.ReadHoldingRegistersAsync(MODBUS_SLAVE, 0, 1);
            Assert.Single(data);
            await modbus.WriteSingleRegisterAsync(MODBUS_SLAVE, 0, 1);
            data = await modbus.ReadHoldingRegistersAsync(MODBUS_SLAVE, 0, 1);
            Assert.Single(data);
            Assert.Equal(1, data[0]);
            await modbus.WriteSingleRegisterAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadHoldingRegistersAsync(MODBUS_SLAVE, 0, 1);
            Assert.Single(data);
            Assert.Equal(0, data[0]);
        }

        [Fact]
        public async Task TestHoldingRegisters()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ushort[] data = await modbus.ReadHoldingRegistersAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteMultipleRegistersAsync(MODBUS_SLAVE, 0, new ushort[] { 1, 2, 3, 4, 5 });
            data = await modbus.ReadHoldingRegistersAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 1, 2, 3, 4, 5 }, data);
            await modbus.WriteMultipleRegistersAsync(MODBUS_SLAVE, 0, new ushort[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadHoldingRegistersAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 0, 0, 0, 0, 0 }, data);
        }

        [Fact]
        public async Task TestBool()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            bool? data = await modbus.ReadBoolAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteBoolAsync(MODBUS_SLAVE, 0, true);
            data = await modbus.ReadBoolAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.True(data);
            await modbus.WriteBoolAsync(MODBUS_SLAVE, 0, false);
            data = await modbus.ReadBoolAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.False(data);
            data = await modbus.ReadOnlyBoolAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestBits()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            BitArray data = await modbus.ReadBitsAsync(MODBUS_SLAVE, 0);
            Assert.Equal(16, data.Length);
            await modbus.WriteBitsAsync(MODBUS_SLAVE, 0, new BitArray(16, true));
            data = await modbus.ReadBitsAsync(MODBUS_SLAVE, 0);
            Assert.Equal(16, data.Length);
            Assert.Equal(new BitArray(16, true), data);
            await modbus.WriteBitsAsync(MODBUS_SLAVE, 0, new BitArray(16, false));
            data = await modbus.ReadBitsAsync(MODBUS_SLAVE, 0);
            Assert.Equal(16, data.Length);
            Assert.Equal(new BitArray(16, false), data);
            data = await modbus.ReadOnlyBitsAsync(MODBUS_SLAVE, 0);
            Assert.Equal(16, data.Length);
        }

        [Fact]
        public async Task TestShort()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            short? data = await modbus.ReadShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteShortAsync(MODBUS_SLAVE, 0, -2);
            data = await modbus.ReadShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((short)-2, data);
            await modbus.WriteShortAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((short)0, data);
            data = await modbus.ReadOnlyShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestUShort()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ushort? data = await modbus.ReadUShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteUShortAsync(MODBUS_SLAVE, 0, 2);
            data = await modbus.ReadUShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((ushort)2, data);
            await modbus.WriteUShortAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadUShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((ushort)0, data);
            data = await modbus.ReadOnlyUShortAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestInt32()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            int? data = await modbus.ReadInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteInt32Async(MODBUS_SLAVE, 0, -2000);
            data = await modbus.ReadInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(-2000, data);
            await modbus.WriteInt32Async(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await modbus.ReadOnlyInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestUInt32()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            uint? data = await modbus.ReadUInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteUInt32Async(MODBUS_SLAVE, 0, 2000);
            data = await modbus.ReadUInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((uint)2000, data);
            await modbus.WriteUInt32Async(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadUInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((uint)0, data);
            data = await modbus.ReadOnlyUInt32Async(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestFloat()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            float? data = await modbus.ReadFloatAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteFloatAsync(MODBUS_SLAVE, 0, 1.234F);
            data = await modbus.ReadFloatAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(1.234F, data);
            await modbus.WriteFloatAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadFloatAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await modbus.ReadOnlyFloatAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestDouble()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            double? data = await modbus.ReadDoubleAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteDoubleAsync(MODBUS_SLAVE, 0, 1.23456789);
            data = await modbus.ReadDoubleAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(1.23456789, data);
            await modbus.WriteDoubleAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadDoubleAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await modbus.ReadOnlyDoubleAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestLong()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            long? data = await modbus.ReadLongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteLongAsync(MODBUS_SLAVE, 0, -2000000);
            data = await modbus.ReadLongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(-2000000, data);
            await modbus.WriteLongAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadLongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal(0, data);
            data = await modbus.ReadOnlyLongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestULong()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ulong? data = await modbus.ReadULongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            await modbus.WriteULongAsync(MODBUS_SLAVE, 0, 2000000);
            data = await modbus.ReadULongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((ulong)2000000, data);
            await modbus.WriteULongAsync(MODBUS_SLAVE, 0, 0);
            data = await modbus.ReadULongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
            Assert.Equal((ulong)0, data);
            data = await modbus.ReadOnlyULongAsync(MODBUS_SLAVE, 0);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task TestBoolArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            bool[] data = await modbus.ReadBoolArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteBoolArrayAsync(MODBUS_SLAVE, 0, new bool[] { true, false, true, true, false });
            data = await modbus.ReadBoolArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { true, false, true, true, false }, data);
            await modbus.WriteBoolArrayAsync(MODBUS_SLAVE, 0, new bool[] { false, false, false, false, false });
            data = await modbus.ReadBoolArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new bool[] { false, false, false, false, false }, data);
            data = await modbus.ReadOnlyBoolArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestShortArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            short[] data = await modbus.ReadShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteShortArrayAsync(MODBUS_SLAVE, 0, new short[] { -1, -2, -3, -4, -5 });
            data = await modbus.ReadShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new short[] { -1, -2, -3, -4, -5 }, data);
            await modbus.WriteShortArrayAsync(MODBUS_SLAVE, 0, new short[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new short[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestUShortArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ushort[] data = await modbus.ReadUShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteUShortArrayAsync(MODBUS_SLAVE, 0, new ushort[] { 1, 2, 3, 4, 5 });
            data = await modbus.ReadUShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 1, 2, 3, 4, 5 }, data);
            await modbus.WriteUShortArrayAsync(MODBUS_SLAVE, 0, new ushort[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadUShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ushort[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyUShortArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestInt32ArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            int[] data = await modbus.ReadInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteInt32ArrayAsync(MODBUS_SLAVE, 0, new int[] { -1, -2, -3, -4, -5 });
            data = await modbus.ReadInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new int[] { -1, -2, -3, -4, -5 }, data);
            await modbus.WriteInt32ArrayAsync(MODBUS_SLAVE, 0, new int[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new int[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestUInt32ArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            uint[] data = await modbus.ReadUInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteUInt32ArrayAsync(MODBUS_SLAVE, 0, new uint[] { 1, 2, 3, 4, 5 });
            data = await modbus.ReadUInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new uint[] { 1, 2, 3, 4, 5 }, data);
            await modbus.WriteUInt32ArrayAsync(MODBUS_SLAVE, 0, new uint[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadUInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new uint[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyUInt32ArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestFloatArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            float[] data = await modbus.ReadFloatArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteFloatArrayAsync(MODBUS_SLAVE, 0, new float[] { 0.12345F, 1.2345F, 12.345F, 123.45F, 1234.5F });
            data = await modbus.ReadFloatArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new float[] { 0.12345F, 1.2345F, 12.345F, 123.45F, 1234.5F }, data);
            await modbus.WriteFloatArrayAsync(MODBUS_SLAVE, 0, new float[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadFloatArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new float[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyFloatArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestDoubleArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            double[] data = await modbus.ReadDoubleArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteDoubleArrayAsync(MODBUS_SLAVE, 0, new double[] { 0.12345, 1.2345, 12.345, 123.45, 1234.5 });
            data = await modbus.ReadDoubleArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new double[] { 0.12345, 1.2345, 12.345, 123.45, 1234.5 }, data);
            await modbus.WriteDoubleArrayAsync(MODBUS_SLAVE, 0, new double[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadDoubleArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new double[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyDoubleArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestLongArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            long[] data = await modbus.ReadLongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteLongArrayAsync(MODBUS_SLAVE, 0, new long[] { -1, -2, -3, -4, -5 });
            data = await modbus.ReadLongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new long[] { -1, -2, -3, -4, -5 }, data);
            await modbus.WriteLongArrayAsync(MODBUS_SLAVE, 0, new long[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadLongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new long[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyLongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestULongArrayAsync()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            ulong[] data = await modbus.ReadULongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteULongArrayAsync(MODBUS_SLAVE, 0, new ulong[] { 1, 2, 3, 4, 5 });
            data = await modbus.ReadULongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ulong[] { 1, 2, 3, 4, 5 }, data);
            await modbus.WriteULongArrayAsync(MODBUS_SLAVE, 0, new ulong[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadULongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new ulong[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyULongArrayAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestBytes()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            byte[] data = await modbus.ReadBytesAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            await modbus.WriteBytesAsync(MODBUS_SLAVE, 0, new byte[] { 1, 2, 3, 4, 5 });
            data = await modbus.ReadBytesAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5 }, data);
            await modbus.WriteBytesAsync(MODBUS_SLAVE, 0, new byte[] { 0, 0, 0, 0, 0 });
            data = await modbus.ReadBytesAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
            Assert.Equal(new byte[] { 0, 0, 0, 0, 0 }, data);
            data = await modbus.ReadOnlyBytesAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, data.Length);
        }

        [Fact]
        public async Task TestString()
        {
            var factory = new ModbusFactory();
            IModbusMaster modbus = factory.CreateMaster(_client);
            await _client.ConnectAsync(MODBUS_IP, MODBUS_PORT);
            await modbus.WriteStringAsync(MODBUS_SLAVE, 0, "Hello");
            string text = await modbus.ReadStringAsync(MODBUS_SLAVE, 0, 5);
            Assert.Equal(5, text.Length);
            Assert.Equal("Hello", text);
            text = await modbus.ReadOnlyStringAsync(MODBUS_SLAVE, 0, 5);
        }
    }
}
