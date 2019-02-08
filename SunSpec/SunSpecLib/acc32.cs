// --------------------------------------------------------------------------------------------------------------------
// <copyright file="acc32.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: acc32
    /// Range: 0...4294967295
    /// Not Accumulated: 0x00000000
    /// </summary>
    [JsonConverter(typeof(Acc32JsonConverter))]
    [TypeConverter(typeof(Acc32TypeConverter))]
    public struct acc32
    {
        public const uint NOT_ACCUMULATED = 0;
        public const uint MAX_VALUE = 4294967295;

        private uint _value;

        [JsonIgnore]
        public bool NotAccumulated { get => _value == 0; }

        public static implicit operator acc32(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new acc32 { _value = registers.ToUInt32() };
        }

        public static implicit operator acc32(uint value)
          => new acc32 { _value = value };

        public static implicit operator uint(acc32 value)
            => value._value;

        public ushort[] ToRegisters()
            => _value.ToRegisters();

        public override string ToString()
            => _value.ToString();
    }
}
