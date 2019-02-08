// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Evt1.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: Evt1
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(Evt1JsonConverter))]
    [TypeConverter(typeof(Evt1TypeConverter))]
    public struct Evt1
    {
        private Events1 _value;

        public static implicit operator Evt1(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<Events1>(registers.ToInt32().ToString(), out Events1 v))
            {
                throw new ArgumentException("Invalid event in registers.", nameof(registers));
            }

            return new Evt1 { _value = (Events1)registers.ToInt32() };
        }

        public static implicit operator Evt1(int value)
        {
            if (!Enum.TryParse<Events1>(value.ToString(), out Events1 v))
            {
                throw new ArgumentException("Invalid event in value.", nameof(value));
            }

            return new Evt1 { _value = (Events1)value };
        }

        public static implicit operator Evt1(Events1 value)
            => new Evt1 { _value = value };

        public static implicit operator int(Evt1 value)
            => (int)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((int)_value);

        public override string ToString()
            => _value.ToString();
    }
}
