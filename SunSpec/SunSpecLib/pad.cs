// --------------------------------------------------------------------------------------------------------------------
// <copyright file="pad.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: pad
    /// Range: 0x8000
    /// </summary>
    [JsonConverter(typeof(PadJsonConverter))]
    [TypeConverter(typeof(PadTypeConverter))]
    public struct pad
    {
        public const ushort RESERVED_VALUE = 0x8000;

        private ushort _value;

        public static implicit operator pad(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new pad { _value = RESERVED_VALUE };
        }

        public static implicit operator pad(ushort value)
            => new pad { _value = RESERVED_VALUE };

        public static implicit operator ushort(pad value)
            => RESERVED_VALUE;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
