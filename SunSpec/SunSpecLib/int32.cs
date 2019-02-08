// --------------------------------------------------------------------------------------------------------------------
// <copyright file="int32.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SunSpecLib
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using NModbus.Extensions;
    using SunSpecLib.Converters;

    #endregion

    /// <summary>
    /// Sunspec 32-bit integer values: int32
    /// Range: ­‐2147483647...2147483647
    /// Not Implemented: 0x80000000
    /// </summary>
    [JsonConverter(typeof(Int32JsonConverter))]
    [TypeConverter(typeof(Int32TypeConverter))]
    public struct int32
    {
        private const int _NOT_IMPLEMENTED = -2147483648;
        private const ushort NOT_IMPLEMENTED_R1 = 0x0000;
        private const ushort NOT_IMPLEMENTED_R2 = 0x8000;
        public const uint NOT_IMPLEMENTED = 0x80000000;
        public const int MIN_VALUE = -2147483647;
        public const int MAX_VALUE = 2147483647;

        private int _value;

        [JsonIgnore]
        public bool NotImplemented { get => _value == _NOT_IMPLEMENTED; }

        public static implicit operator int32(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            bool notimplemented = (registers[0] == NOT_IMPLEMENTED_R1) &&
                                  (registers[1] == NOT_IMPLEMENTED_R2);
            return new int32 { _value = notimplemented ? _NOT_IMPLEMENTED : registers.ToInt32() };
        }

        public static implicit operator int32(uint value)
            => new int32 { _value = value.ToRegisters().ToInt32() };

        public static implicit operator int32(int value)
            => new int32 { _value = value };

        public static implicit operator int(int32 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
