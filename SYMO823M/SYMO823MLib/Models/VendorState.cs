// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendorState.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: VendorState
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(VendorStateJsonConverter))]
    [TypeConverter(typeof(VendorStateTypeConverter))]
    public struct VendorState
    {
        private VendorStates _value;

        public bool NotImplemented { get => _value == VendorStates.NotImplemented; }

        public static implicit operator VendorState(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.IsDefined(typeof(VendorStates), (int)(short)registers[0]))
            {
                throw new ArgumentException("Invalid VendorState value in first register.", nameof(registers));
            }

            return new VendorState { _value = (VendorStates)registers[0] };
        }

        public static implicit operator VendorState(ushort value)
        { VendorState v = value.ToRegisters(); return v; }

        public static implicit operator VendorState(short value)
        {
            if (!Enum.IsDefined(typeof(VendorStates), (int)value))
            {
                throw new ArgumentException("Invalid VendorState value in argument.", nameof(value));
            }

            return new VendorState { _value = (VendorStates)value };
        }

        public static implicit operator VendorStates(VendorState value)
           => value._value;

        public static implicit operator VendorState(VendorStates value)
            => new VendorState { _value = value };

        public static implicit operator short(VendorState value)
            => (short)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((short)_value);

        public override string ToString()
            => _value.ToString();
    }
}
