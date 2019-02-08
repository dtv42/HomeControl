// --------------------------------------------------------------------------------------------------------------------
// <copyright file="bitfield32.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: bitfield32
    /// Range: ­0...0x7FFFFFFF
    /// Not Implemented: 0xFFFFFFFF
    /// </summary>
    [JsonConverter(typeof(Bitfield32JsonConverter))]
    [TypeConverter(typeof(Bitfield32TypeConverter))]
    public struct bitfield32
    {
        public const uint NOT_IMPLEMENTED = 0xFFFFFFFF;
        public const uint MIN_VALUE = 0x00000000;
        public const uint MAX_VALUE = 0x7FFFFFFF;

        private uint _value;

        [JsonIgnore]
        public bool NotImplemented { get => _value == NOT_IMPLEMENTED; }

        public static implicit operator bitfield32(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new bitfield32 { _value = registers.ToUInt32() };
        }

        public static implicit operator bitfield32(uint value)
            => new bitfield32 { _value = ((value >= MIN_VALUE) && (value <= MAX_VALUE)) ? value : NOT_IMPLEMENTED };

        public static implicit operator uint(bitfield32 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
