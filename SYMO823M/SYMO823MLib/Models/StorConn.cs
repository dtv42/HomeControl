// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorConn.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib.Models
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using NModbus.Extensions;
    using SYMO823MLib.Converters;

    #endregion

    /// <summary>
    /// Sunspec 16-bit integer values: StorConn
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(StorConnJsonConverter))]
    [TypeConverter(typeof(StorConnTypeConverter))]
    public struct StorConn
    {
        private StorConns _value;

        public bool NotImplemented { get => _value == StorConns.NotImplemented; }

        public static implicit operator StorConn(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<StorConns>(registers[0].ToString(), out StorConns value))
            {
                throw new ArgumentException("Invalid StorConn value in first register.", nameof(registers));
            }

            return new StorConn { _value = (StorConns)registers[0] };
        }

        public static implicit operator StorConn(ushort value)
        { StorConn v = value.ToRegisters(); return v; }

        public static implicit operator StorConn(short value)
        {
            if (!Enum.TryParse<StorConns>(value.ToString(), out StorConns v))
            {
                throw new ArgumentException("Invalid StorConn value in first register.", nameof(value));
            }

            return new StorConn { _value = (StorConns)value };
        }

        public static implicit operator StorConns(StorConn value)
            => value._value;

        public static implicit operator StorConn(StorConns value)
            => new StorConn { _value = value };

        public static implicit operator short(StorConn value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
