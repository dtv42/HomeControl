// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECPConn.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: ECPConn
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(ECPConnJsonConverter))]
    [TypeConverter(typeof(ECPConnTypeConverter))]
    public struct ECPConn
    {
        private ECPConns _value;

        public bool NotImplemented { get => _value == ECPConns.NotImplemented; }

        public static implicit operator ECPConn(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<ECPConns>(registers[0].ToString(), out ECPConns v))
            {
                throw new ArgumentException("Invalid ECPConn value in first register.", nameof(registers));
            }

            return new ECPConn { _value = (ECPConns)registers[0] };
        }

        public static implicit operator ECPConn(ushort value)
        { ECPConn v = value.ToRegisters(); return v; }

        public static implicit operator ECPConn(short value)
        {
            if (!Enum.TryParse<ECPConns>(value.ToString(), out ECPConns v))
            {
                throw new ArgumentException("Invalid ECPConn value in argument.", nameof(value));
            }

            return new ECPConn { _value = (ECPConns)value };
        }

        public static implicit operator ECPConns(ECPConn value)
           => value._value;

        public static implicit operator ECPConn(ECPConns value)
            => new ECPConn { _value = value };

        public static implicit operator short(ECPConn value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
