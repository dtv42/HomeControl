// --------------------------------------------------------------------------------------------------------------------
// <copyright file="int16.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: int16
    /// Range: ­‐32767...32767
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(Int16JsonConverter))]
    [TypeConverter(typeof(Int16TypeConverter))]
    public struct int16
    {
        private const short _NOT_IMPLEMENTED = -32768;
        public const ushort NOT_IMPLEMENTED = 0x8000;
        public const short MIN_VALUE = -32767;
        public const short MAX_VALUE = 32767;

        private short _value;

        [JsonIgnore]
        public bool NotImplemented { get => _value == _NOT_IMPLEMENTED; }

        public static implicit operator int16(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new int16 { _value = (registers[0] == NOT_IMPLEMENTED) ? _NOT_IMPLEMENTED : (short)registers[0] };
        }

        public static implicit operator int16(ushort value)
            => new int16 { _value = value.ToRegisters().ToShort() };

        public static implicit operator int16(short value)
            => new int16 { _value = value };

        public static implicit operator short(int16 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
