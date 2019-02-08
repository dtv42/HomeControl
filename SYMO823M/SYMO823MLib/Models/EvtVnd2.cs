// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvtVnd2.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: EvtVnd2
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(EvtVnd2JsonConverter))]
    [TypeConverter(typeof(EvtVnd2TypeConverter))]
    public struct EvtVnd2
    {
        private EventsVendor2 _value;

        public static implicit operator EvtVnd2(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<EventsVendor2>(registers.ToInt32().ToString(), out EventsVendor2 v))
            {
                throw new ArgumentException("Invalid event in registers.", nameof(registers));
            }

            return new EvtVnd2 { _value = (EventsVendor2)registers.ToInt32() };
        }

        public static implicit operator EvtVnd2(int value)
        {
            if (!Enum.TryParse<EventsVendor2>(value.ToString(), out EventsVendor2 v))
            {
                throw new ArgumentException("Invalid event in value.", nameof(value));
            }

            return new EvtVnd2 { _value = (EventsVendor2)value };
        }

        public static implicit operator EvtVnd2(EventsVendor2 value)
            => new EvtVnd2 { _value = value };

        public static implicit operator int(EvtVnd2 value)
            => (int)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((int)_value);

        public override string ToString()
            => _value.ToString();
    }
}
