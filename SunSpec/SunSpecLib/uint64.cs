// --------------------------------------------------------------------------------------------------------------------
// <copyright file="uint64.cs" company="DTV-Online">
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
    /// Sunspec 64-bit integer values: uint64
    /// Range: ­0...18446744073709553214
    /// Not Implemented: 0xFFFFFFFFFFFFFFFF
    /// </summary>
    [JsonConverter(typeof(UInt64JsonConverter))]
    [TypeConverter(typeof(UInt64TypeConverter))]
    public struct uint64
    {
        public const ulong NOT_IMPLEMENTED = 0xFFFFFFFFFFFFFFFF;
        public const ulong MIN_VALUE = 0x0000000000000000;
        public const ulong MAX_VALUE = 0xFFFFFFFFFFFFFFFE;

        private ulong _value;

        public bool NotImplemented { get => _value == NOT_IMPLEMENTED; }

        public static implicit operator uint64(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 4)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new uint64 { _value = registers.ToULong() };
        }

        public static implicit operator uint64(ulong value)
            => new uint64 { _value = value };

        public static implicit operator ulong(uint64 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
