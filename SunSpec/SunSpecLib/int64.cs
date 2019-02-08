// --------------------------------------------------------------------------------------------------------------------
// <copyright file="int64.cs" company="DTV-Online">
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
    /// Sunspec 64-bit integer values: int64
    /// Range: ­‐9223372036854775807...9223372036854775807
    /// Not Implemented: 0x8000000000000000
    /// </summary>
    [JsonConverter(typeof(Int64JsonConverter))]
    [TypeConverter(typeof(Int64TypeConverter))]
    public struct int64
    {
        private const long _NOT_IMPLEMENTED = -32768;
        private const ushort NOT_IMPLEMENTED_R1 = 0x0000;
        private const ushort NOT_IMPLEMENTED_R2 = 0x0000;
        private const ushort NOT_IMPLEMENTED_R3 = 0x0000;
        private const ushort NOT_IMPLEMENTED_R4 = 0x8000;
        public const ulong NOT_IMPLEMENTED = 0x8000000000000000;
        public const long MIN_VALUE = -9223372036854775807;
        public const long MAX_VALUE = 9223372036854775807;

        private long _value;

        [JsonIgnore]
        public bool NotImplemented { get => _value == _NOT_IMPLEMENTED; }

        public static implicit operator int64(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 4)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            bool notimplemented = (registers[0] == NOT_IMPLEMENTED_R1) &&
                                  (registers[1] == NOT_IMPLEMENTED_R2) &&
                                  (registers[2] == NOT_IMPLEMENTED_R3) &&
                                  (registers[3] == NOT_IMPLEMENTED_R4);
            return new int64 { _value = notimplemented ? _NOT_IMPLEMENTED : registers.ToLong() };
        }

        public static implicit operator int64(ulong value)
            => new int64 { _value = value.ToRegisters().ToLong() };

        public static implicit operator int64(long value)
            => new int64 { _value = value };

        public static implicit operator long(int64 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
