// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DERType.cs" company="DTV-Online">
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
    using SunSpecLib.Converters;
    using SYMO823MLib.Converters;

    #endregion

    /// <summary>
    /// Sunspec 16-bit integer values: DERType
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(DERTypeJsonConverter))]
    [TypeConverter(typeof(DERTypeTypeConverter))]
    public struct DERType
    {
        private DERTypes _value;

        public bool NotImplemented { get => _value == DERTypes.NotImplemented; }

        public static implicit operator DERType(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.IsDefined(typeof(DERTypes), (int)(short)registers[0]))
            {
                throw new ArgumentException("Invalid DERType value in first register.", nameof(registers));
            }

            return new DERType { _value = (DERTypes)registers[0] };
        }

        public static implicit operator DERType(ushort value)
        { DERType v = value.ToRegisters(); return v; }

        public static implicit operator DERType(short value)
        {
            if (!Enum.IsDefined(typeof(DERTypes), (int)value))
            {
                throw new ArgumentException("Invalid DERType value in argument.", nameof(value));
            }

            return new DERType { _value = (DERTypes)value };
        }

        public static implicit operator DERTypes(DERType value)
           => value._value;

        public static implicit operator DERType(DERTypes value)
            => new DERType { _value = value };

        public static implicit operator short(DERType value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
