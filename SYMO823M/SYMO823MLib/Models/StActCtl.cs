// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StActCtl.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: StActCtl
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(StActCtlJsonConverter))]
    [TypeConverter(typeof(StActCtlTypeConverter))]
    public struct StActCtl
    {
        private StActCtls _value;

        public bool NotImplemented { get => _value == StActCtls.NotImplemented; }

        public static implicit operator StActCtl(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<StActCtls>(registers[0].ToString(), out StActCtls value))
            {
                throw new ArgumentException("Invalid StActCtl value in first register.", nameof(registers));
            }

            return new StActCtl { _value = (StActCtls)registers[0] };
        }

        public static implicit operator StActCtl(ushort value)
        { StActCtl v = value.ToRegisters(); return v; }

        public static implicit operator StActCtl(short value)
        {
            if (!Enum.TryParse<StActCtls>(value.ToString(), out StActCtls v))
            {
                throw new ArgumentException("Invalid StActCtl value in argument.", nameof(value));
            }

            return new StActCtl { _value = (StActCtls)value };
        }

        public static implicit operator StActCtls(StActCtl value)
            => value._value;

        public static implicit operator StActCtl(StActCtls value)
            => new StActCtl { _value = value };

        public static implicit operator short(StActCtl value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
