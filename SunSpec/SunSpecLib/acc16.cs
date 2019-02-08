// --------------------------------------------------------------------------------------------------------------------
// <copyright file="acc16.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: acc16
    /// Range: 0...65535
    /// Not Accumulated: 0x0000
    /// </summary>
    [JsonConverter(typeof(Acc16JsonConverter))]
    [TypeConverter(typeof(Acc16TypeConverter))]
    public struct acc16
    {
        public const ushort NOT_ACCUMULATED = 0;
        public const ushort MAX_VALUE = 65535;

        private ushort _value;

        [JsonIgnore]
        public bool NotAccumulated { get => _value == 0; }

        public static implicit operator acc16(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new acc16 { _value = registers[0] };
        }

        public static implicit operator acc16(ushort value)
            => new acc16 { _value = value };

        public static implicit operator ushort(acc16 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
