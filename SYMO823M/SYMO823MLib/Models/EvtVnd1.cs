// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvtVnd1.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: EvtVnd1
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(EvtVnd1JsonConverter))]
    [TypeConverter(typeof(EvtVnd1TypeConverter))]
    public struct EvtVnd1
    {
        private EventsVendor1 _value;

        public static implicit operator EvtVnd1(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<EventsVendor1>(registers.ToInt32().ToString(), out EventsVendor1 v))
            {
                throw new ArgumentException("Invalid event in registers.", nameof(registers));
            }

            return new EvtVnd1 { _value = (EventsVendor1)registers.ToInt32() };
        }

        public static implicit operator EvtVnd1(int value)
        {
            if (!Enum.TryParse<EventsVendor1>(value.ToString(), out EventsVendor1 v))
            {
                throw new ArgumentException("Invalid event in value.", nameof(value));
            }

            return new EvtVnd1 { _value = (EventsVendor1)value };
        }

        public static implicit operator EvtVnd1(EventsVendor1 value)
            => new EvtVnd1 { _value = value };

        public static implicit operator int(EvtVnd1 value)
            => (int)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((int)_value);

        public override string ToString()
            => _value.ToString();
    }
}
