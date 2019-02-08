// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModbusAttribute.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusTest
{
    #region Using Directives

    using Xunit;

    using DataValueLib;
    using NModbus.Extensions;

    #endregion

    /// <summary>
    /// Test class for testing the Modbus attribute.
    /// </summary>
    public class TestModbusAttribute : PropertyHelper<TestModbusAttribute>
    {
        /// <summary>
        /// Internal class holding properties with various Modbus attributes.
        /// </summary>
        public class TestData
        {
            [Modbus]
            public ushort TestRegister { get; set; } = 1;

            [Modbus(offset: 100, access: ModbusAttribute.AccessModes.ReadOnly, length: 2)]
            public int TestInteger { get; set; } = -1;

            [Modbus(200, 2, ModbusAttribute.AccessModes.ReadOnly)]
            public float TestFloat { get; set; } = 1.23F;

            [Modbus(300, 4, ModbusAttribute.AccessModes.WriteOnly)]
            public double TestDouble { get; set; } = 1.23456789;
        }

        /// <summary>
        /// Property to test the various Modbus attributes.
        /// </summary>
        public TestData Test { get; } = new TestData();

        ModbusAttribute GetModbusAttribute(string property) =>
            ModbusAttribute.GetModbusAttribute(GetPropertyInfo(property));

        ModbusAttribute.AccessModes GetAccess(string property) =>
            GetModbusAttribute(property).Access;

        ushort GetOffset(string property) =>
            GetModbusAttribute(property).Offset;

        ushort GetLength(string property) =>
            GetModbusAttribute(property).Length;

        [Theory]
        [InlineData("TestRegister", 0, ModbusAttribute.AccessModes.ReadWrite, 1)]
        [InlineData("TestInteger", 100, ModbusAttribute.AccessModes.ReadOnly, 2)]
        [InlineData("TestFloat", 200, ModbusAttribute.AccessModes.ReadOnly, 2)]
        [InlineData("TestDouble", 300, ModbusAttribute.AccessModes.WriteOnly, 4)]
        public void TestAttribute(string property, ushort offset, ModbusAttribute.AccessModes access, ushort length)
        {
            var info = typeof(TestData).GetProperty(property);
            var attribute = ModbusAttribute.GetModbusAttribute(info);
            Assert.NotNull(info);
            Assert.NotNull(attribute);
            Assert.Equal(offset, attribute?.Offset);
            Assert.Equal(access, attribute?.Access);
            Assert.Equal(length, attribute?.Length);
        }

        [Theory]
        [InlineData("Test.TestRegister", 0, ModbusAttribute.AccessModes.ReadWrite, 1, (ushort)1)]
        [InlineData("Test.TestInteger", 100, ModbusAttribute.AccessModes.ReadOnly, 2, (int)-1)]
        [InlineData("Test.TestFloat", 200, ModbusAttribute.AccessModes.ReadOnly, 2, (float)1.23F)]
        [InlineData("Test.TestDouble", 300, ModbusAttribute.AccessModes.WriteOnly, 4, (double)1.23456789)]
        public void TestHelpers(string property, ushort offset, ModbusAttribute.AccessModes access, ushort length, dynamic value)
        {
            var flag = IsProperty(property);
            var info = GetPropertyInfo(property);
            var attribute = GetModbusAttribute(property);
            Assert.True(flag);
            Assert.NotNull(info);
            Assert.NotNull(attribute);
            Assert.Equal(value, GetPropertyValue(property));
            Assert.Equal(access, GetAccess(property));
            Assert.Equal(offset, GetOffset(property));
            Assert.Equal(length, GetLength(property));
        }
    }
}
