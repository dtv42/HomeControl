// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModbusConversions.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusTest
{
    #region Using Directives

    using System.Collections;
    using Xunit;

    using NModbus.Extensions;

    #endregion

    /// <summary>
    /// Test class for testing the Modbus conversion routines.
    /// </summary>
    public class TestModbusConversions
    {
        [Fact]
        public void TestBitArray()
        {
            ushort[] registers = new ushort[] { 0xFFFF };
            BitArray bitarray = registers.ToBitArray();
            ushort[] result = bitarray.ToRegisters();

            Assert.Equal(16, bitarray.Length);
            Assert.Equal(result, registers);

            foreach (bool bit in bitarray)
                Assert.True(bit);

            registers = new ushort[] { 0x0000 };
            bitarray = registers.ToBitArray();
            result = bitarray.ToRegisters();

            Assert.Equal(16, bitarray.Length);
            Assert.Equal(result, registers);

            foreach (bool bit in bitarray)
                Assert.False(bit);

            registers = new ushort[] { 0x8001 };
            bitarray = registers.ToBitArray();
            result = bitarray.ToRegisters();

            Assert.Equal(16, bitarray.Length);
            Assert.Equal(result, registers);
            Assert.True(bitarray[0]);
            Assert.False(bitarray[1]);
            Assert.False(bitarray[2]);
            Assert.False(bitarray[3]);
            Assert.False(bitarray[4]);
            Assert.False(bitarray[5]);
            Assert.False(bitarray[6]);
            Assert.False(bitarray[7]);
            Assert.False(bitarray[8]);
            Assert.False(bitarray[9]);
            Assert.False(bitarray[10]);
            Assert.False(bitarray[11]);
            Assert.False(bitarray[12]);
            Assert.False(bitarray[13]);
            Assert.False(bitarray[14]);
            Assert.True(bitarray[15]);

            bitarray = registers.ToBitArray(swapBytes: true);
            result = bitarray.ToRegisters(swapBytes: true);

            Assert.Equal(16, bitarray.Length);
            Assert.Equal(result, registers);
            Assert.False(bitarray[0]);
            Assert.False(bitarray[1]);
            Assert.False(bitarray[2]);
            Assert.False(bitarray[3]);
            Assert.False(bitarray[4]);
            Assert.False(bitarray[5]);
            Assert.False(bitarray[6]);
            Assert.True(bitarray[7]);
            Assert.True(bitarray[8]);
            Assert.False(bitarray[9]);
            Assert.False(bitarray[10]);
            Assert.False(bitarray[11]);
            Assert.False(bitarray[12]);
            Assert.False(bitarray[13]);
            Assert.False(bitarray[14]);
            Assert.False(bitarray[15]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(short.MinValue)]
        [InlineData(short.MaxValue)]
        public void TestShort(short value)
        {
            ushort[] registers = value.ToRegisters();
            short result = registers.ToShort();

            Assert.Single(registers);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToShort(swapBytes: true);

            Assert.Single(registers);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(ushort.MinValue)]
        [InlineData(ushort.MaxValue)]
        public void TestUShort(ushort value)
        {
            ushort[] registers = value.ToRegisters();
            ushort result = registers.ToUShort();

            Assert.Single(registers);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToUShort(swapBytes: true);

            Assert.Single(registers);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void TestInt32(int value)
        {
            ushort[] registers = value.ToRegisters();
            int result = registers.ToInt32();

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToInt32(swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToInt32(swapWords: true, swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToInt32(swapWords: true, swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(uint.MinValue)]
        [InlineData(uint.MaxValue)]
        public void TestUInt32(uint value)
        {
            ushort[] registers = value.ToRegisters();
            uint result = registers.ToUInt32();

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToUInt32(swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToUInt32(swapWords: true, swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToUInt32(swapWords: true, swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(float.MinValue)]
        [InlineData(float.MaxValue)]
        public void TestFloat(float value)
        {
            ushort[] registers = value.ToRegisters();
            float result = registers.ToFloat();

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToFloat(swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToFloat(swapWords: true, swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToFloat(swapWords: true, swapBytes: true);

            Assert.Equal(2, registers.Length);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        public void TestDouble(double value)
        {
            ushort[] registers = value.ToRegisters();
            double result = registers.ToDouble();

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToDouble(swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToDouble(swapWords: true, swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToDouble(swapWords: true, swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(long.MinValue)]
        [InlineData(long.MaxValue)]
        public void TestLong(long value)
        {
            ushort[] registers = value.ToRegisters();
            long result = registers.ToLong();

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToLong(swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToLong(swapWords: true, swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToLong(swapWords: true, swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void TestULong(ulong value)
        {
            ushort[] registers = value.ToRegisters();
            ulong result = registers.ToULong();

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapBytes: true);
            result = registers.ToULong(swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToULong(swapWords: true, swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);

            registers = value.ToRegisters(swapWords: true, swapBytes: true);
            result = registers.ToULong(swapWords: true, swapBytes: true);

            Assert.Equal(4, registers.Length);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData("?", 1)]
        [InlineData("???", 2)]
        [InlineData("Hello", 3)]
        [InlineData("Hello world!", 6)]
        public void TestASCII1(string value, ushort size)
        {
            ushort[] registers = value.ToRegisters();
            string result = registers.ToASCII();

            Assert.Equal(size, registers.Length);
            Assert.Equal(value, result);
        }

        [Fact]
        public void TestASCII2()
        {
            ushort[] registers = new ushort[] { 0x3031, 0x3233, 0x3435, 0x3637, 0x3839 };
            string result = registers.ToASCII();

            Assert.Equal(10, result.Length);
            Assert.Equal("0123456789", result);
        }

        [Theory]
        [InlineData(0x0000, "0000")]
        [InlineData(0x1234, "1234")]
        [InlineData(0xFFFF, "FFFF")]
        public void TestHex1(ushort value, string hex)
        {
            ushort[] registers = new ushort[] { value };
            string result = registers.ToHex(2);

            Assert.Equal(4, result.Length);
            Assert.Equal(hex, result);
        }

        [Fact]
        public void TestHex2()
        {
            ushort[] registers = new ushort[] { 0x3031, 0x3233, 0x3435, 0x3637, 0x3839 };
            string result = registers.ToHex(10);

            Assert.Equal(20, result.Length);
            Assert.Equal("30313233343536373839", result);
        }
    }
}
