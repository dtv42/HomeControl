// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvtVnd4.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: EvtVnd4
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(EvtVnd4JsonConverter))]
    [TypeConverter(typeof(EvtVnd4TypeConverter))]
    public struct EvtVnd4
    {
        private EventsVendor4 _value;

        public static implicit operator EvtVnd4(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.TryParse<EventsVendor4>(registers.ToInt32().ToString(), out EventsVendor4 v))
            {
                throw new ArgumentException("Invalid event in registers.", nameof(registers));
            }

            return new EvtVnd4 { _value = (EventsVendor4)registers.ToInt32() };
        }

        public static implicit operator EvtVnd4(int value)
        {
            if (!Enum.TryParse<EventsVendor4>(value.ToString(), out EventsVendor4 v))
            {
                throw new ArgumentException("Invalid event in value.", nameof(value));
            }

            return new EvtVnd4 { _value = (EventsVendor4)value };
        }

        public static implicit operator EvtVnd4(EventsVendor4 value)
            => new EvtVnd4 { _value = value };

        public static implicit operator int(EvtVnd4 value)
            => (int)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((int)_value);

        public override string ToString()
            => _value.ToString();
    }
}
