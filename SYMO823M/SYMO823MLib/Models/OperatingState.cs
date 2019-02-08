// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatingState.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: OperatingState
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(OperatingStateJsonConverter))]
    [TypeConverter(typeof(OperatingStateTypeConverter))]
    public struct OperatingState
    {
        private OperatingStates _value;

        public bool NotImplemented { get => _value == OperatingStates.NotImplemented; }

        public static implicit operator OperatingState(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.IsDefined(typeof(OperatingStates), (int)(short)registers[0]))
            {
                throw new ArgumentException("Invalid OperatingState value in first register.", nameof(registers));
            }

            return new OperatingState { _value = (OperatingStates)registers[0] };
        }

        public static implicit operator OperatingState(ushort value)
        { OperatingState v = value.ToRegisters(); return v; }

        public static implicit operator OperatingState(short value)
        {
            if (!Enum.IsDefined(typeof(OperatingStates), (int)value))
            {
                throw new ArgumentException("Invalid OperatingState value in argument.", nameof(value));
            }

            return new OperatingState { _value = (OperatingStates)value };
        }

        public static implicit operator OperatingStates(OperatingState value)
           => value._value;

        public static implicit operator OperatingState(OperatingStates value)
            => new OperatingState { _value = value };

        public static implicit operator short(OperatingState value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
