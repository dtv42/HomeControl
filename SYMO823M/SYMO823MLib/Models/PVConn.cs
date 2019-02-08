// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PVConn.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: PVConn
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(PVConnJsonConverter))]
    [TypeConverter(typeof(PVConnTypeConverter))]
    public struct PVConn
    {
        private PVConns _value;

        public bool NotImplemented { get => _value == PVConns.NotImplemented; }

        public static implicit operator PVConn(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<PVConns>(registers[0].ToString(), out PVConns v))
            {
                throw new ArgumentException("Invalid PVConn value in first register.", nameof(registers));
            }

            return new PVConn { _value = (PVConns)registers[0] };
        }

        public static implicit operator PVConn(ushort value)
        { PVConn v = value.ToRegisters(); return v; }

        public static implicit operator PVConn(short value)
        {
            if (!Enum.TryParse<PVConns>(value.ToString(), out PVConns v))
            {
                throw new ArgumentException("Invalid PVConn value in argument.", nameof(value));
            }

            return new PVConn { _value = (PVConns)value };
        }

        public static implicit operator PVConns(PVConn value)
            => value._value;

        public static implicit operator PVConn(PVConns value)
            => new PVConn { _value = value };

        public static implicit operator short(PVConn value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
