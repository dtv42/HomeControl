namespace SunSpecTest
{
    #region Using Directives

    using System;
    using System.Net;

    using Xunit;

    using SunSpecLib;
    using Newtonsoft.Json;

    #endregion

    public class UnitTest
    {
        [Fact]
        public void TestPad()
        {
            pad value;

            value = (ushort)0;
            Assert.Equal(pad.RESERVED_VALUE, (ushort)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            value = registers;
            Assert.Equal(pad.RESERVED_VALUE, (ushort)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<pad>(json);
            Assert.Equal((ushort)convert, (ushort)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt16(text);
            pad data = convert;
            Assert.Equal((ushort)data, (ushort)value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-12345)]
        [InlineData(12345)]
        [InlineData(int16.MIN_VALUE)]
        [InlineData(int16.MAX_VALUE)]
        public void TestInt16(short data)
        {
            int16 value;

            value = data;
            Assert.Equal(data, (short)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            value = registers;
            Assert.Equal(data, (short)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<int16>(json);
            Assert.Equal((short)convert, (short)value);
            string text = Convert.ToString(value);
            convert = Convert.ToInt16(text);
            Assert.Equal(data, (long)convert);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(0xCFC7, -12345)]
        [InlineData(0x3039, 12345)]
        [InlineData(0x8001, int16.MIN_VALUE)]
        [InlineData(0x7FFF, int16.MAX_VALUE)]
        [InlineData(int16.NOT_IMPLEMENTED, -32768)]
        public void TestInt16FromRegisters(ushort register1, short expected)
        {
            int16 value;

            value = new ushort[] { register1 };
            Assert.Equal(expected, (short)value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1234567890)]
        [InlineData(1234567890)]
        [InlineData(int32.MIN_VALUE)]
        [InlineData(int32.MAX_VALUE)]
        public void TestInt32(int data)
        {
            int32 value;

            value = data;
            Assert.Equal(data, (int)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(2, registers.Length);
            value = registers;
            Assert.Equal(data, (int)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<int32>(json);
            Assert.Equal((int)convert, (int)value);
            string text = Convert.ToString(value);
            convert = Convert.ToInt32(text);
            Assert.Equal(data, (long)convert);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(0xB669, 0xFD2E, -1234567890)]
        [InlineData(0x4996, 0x02D2, 1234567890)]
        [InlineData(0x8000, 0x0001, int32.MIN_VALUE)]
        [InlineData(0x7FFF, 0xFFFF, int32.MAX_VALUE)]
        [InlineData(0x8000, 0x0000, -2147483648)]
        public void TestInt32FromRegisters(ushort register1, ushort register2, int expected)
        {
            int32 value;

            value = new ushort[] { register1, register2 };
            Assert.Equal(expected, (int)value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1234567890123456789)]
        [InlineData(1234567890123456789)]
        [InlineData(int64.MIN_VALUE)]
        [InlineData(int64.MAX_VALUE)]
        public void TestInt64(long data)
        {
            int64 value;

            value = data;
            Assert.Equal(data, (long)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(4, registers.Length);
            value = registers;
            Assert.Equal(data, (long)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<int64>(json);
            Assert.Equal((long)convert, (long)value);
            string text = Convert.ToString(value);
            convert = Convert.ToInt64(text);
            Assert.Equal(data, (long)convert);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 1, 1)]
        [InlineData(0xEEDD, 0xEF0B, 0x8216, 0x7EEB, -1234567890123456789)]
        [InlineData(0x1122, 0x10F4, 0x7DE9, 0x8115, 1234567890123456789)]
        [InlineData(0x8000, 0x0000, 0x0000, 0x0001, int64.MIN_VALUE)]
        [InlineData(0x7FFF, 0xFFFF, 0xFFFF, 0xFFFF, int64.MAX_VALUE)]
        [InlineData(0x8000, 0x0000, 0x0000, 0x0000, -9223372036854775808)]
        public void TestInt64FromRegisters(ushort register1, ushort register2,
                                           ushort register3, ushort register4, long expected)
        {
            int64 value;

            value = new ushort[] { register1, register2, register3, register4 };
            Assert.Equal(expected, (long)value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(uint16.NOT_IMPLEMENTED)]
        [InlineData(12345)]
        [InlineData(uint16.MIN_VALUE)]
        [InlineData(uint16.MAX_VALUE)]
        public void TestUInt16(ushort data)
        {
            uint16 value;

            value = data;
            Assert.Equal(data, (ushort)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            value = registers;
            Assert.Equal(data, (ushort)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<uint16>(json);
            Assert.Equal((ushort)convert, (ushort)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt16(text);
            Assert.Equal(data, (ushort)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(uint32.NOT_IMPLEMENTED)]
        [InlineData(1234567890)]
        [InlineData(uint32.MIN_VALUE)]
        [InlineData(uint32.MAX_VALUE)]
        public void TestUInt32(uint data)
        {
            uint32 value;

            value = data;
            Assert.Equal(data, (uint)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(2, registers.Length);
            value = registers;
            Assert.Equal(data, (uint)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<uint32>(json);
            Assert.Equal((uint)convert, (uint)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt32(text);
            Assert.Equal(data, (uint)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(uint64.NOT_IMPLEMENTED)]
        [InlineData(1234567890123456789)]
        [InlineData(uint64.MIN_VALUE)]
        [InlineData(uint64.MAX_VALUE)]
        public void TestUInt64(ulong data)
        {
            uint64 value;

            value = data;
            Assert.Equal(data, (ulong)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(4, registers.Length);
            value = registers;
            Assert.Equal(data, (ulong)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<uint64>(json);
            Assert.Equal((ulong)convert, (ulong)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt64(text);
            Assert.Equal(data, (ulong)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(enum16.NOT_IMPLEMENTED)]
        [InlineData(12345)]
        [InlineData(enum16.MIN_VALUE)]
        [InlineData(enum16.MAX_VALUE)]
        public void TestEnum16(ushort data)
        {
            enum16 value;

            value = data;
            Assert.Equal(data, (ushort)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            value = registers;
            Assert.Equal(data, (ushort)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<enum16>(json);
            Assert.Equal((ushort)convert, (ushort)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt16(text);
            Assert.Equal(data, (ushort)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(enum32.NOT_IMPLEMENTED)]
        [InlineData(1234567890)]
        [InlineData(enum32.MIN_VALUE)]
        [InlineData(enum32.MAX_VALUE)]
        public void TestEnum32(uint data)
        {
            enum32 value;

            value = data;
            Assert.Equal(data, (uint)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(2, registers.Length);
            value = registers;
            Assert.Equal(data, (uint)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<enum32>(json);
            Assert.Equal((uint)convert, (uint)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt32(text);
            Assert.Equal(data, (uint)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(12345)]
        [InlineData(acc16.MAX_VALUE)]
        [InlineData(acc16.NOT_ACCUMULATED)]
        public void TestAcc16(ushort data)
        {
            acc16 value;

            value = data;
            Assert.Equal(data, (ushort)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            value = registers;
            Assert.Equal(data, (ushort)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<acc16>(json);
            Assert.Equal((ushort)convert, (ushort)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt16(text);
            Assert.Equal(data, (ushort)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1234567890)]
        [InlineData(acc32.MAX_VALUE)]
        [InlineData(acc32.NOT_ACCUMULATED)]
        public void TestAcc32(uint data)
        {
            acc32 value;

            value = data;
            Assert.Equal(data, (uint)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(2, registers.Length);
            value = registers;
            Assert.Equal(data, (uint)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<acc32>(json);
            Assert.Equal((uint)convert, (uint)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt32(text);
            Assert.Equal(data, (uint)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1234567890123456789)]
        [InlineData(acc64.MAX_VALUE)]
        [InlineData(acc64.NOT_ACCUMULATED)]
        public void TestAcc64(ulong data)
        {
            acc64 value;

            value = data;
            Assert.Equal(data, (ulong)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(4, registers.Length);
            value = registers;
            Assert.Equal(data, (ulong)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<acc64>(json);
            Assert.Equal((ulong)convert, (ulong)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt64(text);
            Assert.Equal(data, (ulong)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(bitfield16.NOT_IMPLEMENTED)]
        [InlineData(12345)]
        [InlineData(bitfield16.MIN_VALUE)]
        [InlineData(bitfield16.MAX_VALUE)]
        public void TestBitfield16(ushort data)
        {
            bitfield16 value;

            value = data;
            Assert.Equal(data, (ushort)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            value = registers;
            Assert.Equal(data, (ushort)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<bitfield16>(json);
            Assert.Equal((ushort)convert, (ushort)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt16(text);
            Assert.Equal(data, (ushort)convert);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(bitfield32.NOT_IMPLEMENTED)]
        [InlineData(1234567890)]
        [InlineData(bitfield32.MIN_VALUE)]
        [InlineData(bitfield32.MAX_VALUE)]
        public void TestBitfield32(uint data)
        {
            bitfield32 value;

            value = data;
            Assert.Equal(data, (uint)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(2, registers.Length);
            value = registers;
            Assert.Equal(data, (uint)value);
            Assert.Equal(data, (uint)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<bitfield32>(json);
            Assert.Equal((ushort)convert, (ushort)value);
            string text = Convert.ToString(value);
            convert = Convert.ToUInt32(text);
            Assert.Equal(data, (uint)convert);
        }

        [Fact]
        public void TestIPAddressV4()
        {
            ipaddr value = new ipaddr();

            Assert.True(value.NotConfigured);
            value = new IPAddress(new byte[] { 127, 0, 0, 1 });
            Assert.False(value.NotConfigured);
            Assert.Equal(new IPAddress(new byte[] { 127, 0, 0, 1 }), (IPAddress)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(2, registers.Length);
            value = registers;
            Assert.False(value.NotConfigured);
            Assert.Equal(new IPAddress(new byte[] { 127, 0, 0, 1 }), (IPAddress)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<ipaddr>(json);
            Assert.Equal((IPAddress)convert, (IPAddress)value);
            string text = ((IPAddress)value).ToString();
            convert = IPAddress.Parse(text);
            Assert.Equal(convert, value);
        }

        [Fact]
        public void TestIPAddressV6()
        {
            ipv6addr value = new ipv6addr();

            Assert.True(value.NotConfigured);
            value = new IPAddress(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            Assert.False(value.NotConfigured);
            Assert.Equal(new IPAddress(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }), (IPAddress)value);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Equal(8, registers.Length);
            value = registers;
            Assert.False(value.NotConfigured);
            Assert.Equal(new IPAddress(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }), (IPAddress)value);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<ipv6addr>(json);
            Assert.Equal(convert, value);
            string text = ((IPAddress)value).ToString();
            convert = IPAddress.Parse(text);
            Assert.Equal(convert, value);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-9)]
        [InlineData(-8)]
        [InlineData(-7)]
        [InlineData(-6)]
        [InlineData(-5)]
        [InlineData(-4)]
        [InlineData(-3)]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void TestScaleFactor(int factor)
        {
            sunssf value = factor;
            Assert.Equal(factor, value.Factor);
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            Assert.Equal((int)(short)registers[0], value.Factor);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<sunssf>(json);
            Assert.Equal(convert, value);
        }

        [Theory]
        [InlineData(ScaleFactors.Minus10)]
        [InlineData(ScaleFactors.Minus9)]
        [InlineData(ScaleFactors.Minus8)]
        [InlineData(ScaleFactors.Minus7)]
        [InlineData(ScaleFactors.Minus6)]
        [InlineData(ScaleFactors.Minus5)]
        [InlineData(ScaleFactors.Minus4)]
        [InlineData(ScaleFactors.Minus3)]
        [InlineData(ScaleFactors.Minus2)]
        [InlineData(ScaleFactors.Minus1)]
        [InlineData(ScaleFactors.Zero)]
        [InlineData(ScaleFactors.Plus1)]
        [InlineData(ScaleFactors.Plus2)]
        [InlineData(ScaleFactors.Plus3)]
        [InlineData(ScaleFactors.Plus4)]
        [InlineData(ScaleFactors.Plus5)]
        [InlineData(ScaleFactors.Plus6)]
        [InlineData(ScaleFactors.Plus7)]
        [InlineData(ScaleFactors.Plus8)]
        [InlineData(ScaleFactors.Plus9)]
        [InlineData(ScaleFactors.Plus10)]
        [InlineData(ScaleFactors.NotImplemented)]
        public void TestScaleFactors(ScaleFactors factor)
        {
            sunssf value = factor;
            Assert.True(Enum.IsDefined(typeof(ScaleFactors), value.Scale));
            var registers = value.ToRegisters();
            Assert.NotEmpty(registers);
            Assert.Single(registers);
            Assert.Equal((int)(short)registers[0], value.Factor);
            string json = JsonConvert.SerializeObject(value);
            var convert = JsonConvert.DeserializeObject<sunssf>(json);
            Assert.Equal(convert, value);
        }
    }
}
