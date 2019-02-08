// --------------------------------------------------------------------------------------------------------------------
// <copyright file="acc64.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SunSpecLib
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using NModbus.Extensions;
    using SunSpecLib.Converters;

    #endregion

    /// <summary>
    /// Sunspec 64-bit integer values: acc64
    /// Range: Range: 0...9223372036854775807
    /// Not Accumulated: 0x0000
    /// </summary>
    [JsonConverter(typeof(Acc64JsonConverter))]
    [TypeConverter(typeof(Acc64TypeConverter))]
    public struct acc64
    {
        public const ulong NOT_ACCUMULATED = 0;
        public const ulong MAX_VALUE = 9223372036854775807;

        private ulong _value;

        [JsonIgnore]
        public bool NotAccumulated { get => _value == 0; }

        public static implicit operator acc64(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 4)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new acc64 { _value = registers.ToULong() };
        }

        public static implicit operator acc64(ulong value)
            => new acc64 { _value = value };

        public static implicit operator ulong(acc64 value)
            => value._value;

        public ushort[] ToRegisters()
            => _value.ToRegisters();

        public override string ToString()
            => _value.ToString();
    }
}
