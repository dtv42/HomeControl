// --------------------------------------------------------------------------------------------------------------------
// <copyright file="sunssf.cs" company="DTV-Online">
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
    /// Sunspec 16-bit integer values: sunssf
    /// Range: ­‐10...10
    /// Not Implemented: 0x8000
    /// </summary>
    [JsonConverter(typeof(SunSSFJsonConverter))]
    [TypeConverter(typeof(SunSSFTypeConverter))]
    public struct sunssf
    {
        private ScaleFactors _value;

        public bool NotImplemented { get => _value == ScaleFactors.NotImplemented; }
        public ScaleFactors Scale { get => _value; }
        public int Factor { get => (int)(short)_value; }

        public static implicit operator sunssf(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            if (!Enum.IsDefined(typeof(ScaleFactors), registers[0]))
            {
                throw new ArgumentException("Invalid scale factor in first register.", nameof(registers));
            }

            return new sunssf { _value = (ScaleFactors)registers[0] };
        }

        public static implicit operator sunssf(ushort value)
         => (sunssf)value.ToRegisters();

        public static implicit operator sunssf(int value)
        {
            if (!Enum.IsDefined(typeof(ScaleFactors), (ushort)value))
            {
                throw new ArgumentException("Invalid scale factor in value.", nameof(value));
            }

            return new sunssf { _value = (ScaleFactors)value };
        }

        public static implicit operator sunssf(ScaleFactors value)
            => new sunssf { _value = value };

        public static implicit operator int(sunssf value)
            => (int)value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters((ushort)_value);

        public override string ToString()
            => _value.ToString();

        public static implicit operator sunssf(string value)
        {
            switch (value)
            {
                case "Minus10": return new sunssf { _value = ScaleFactors.Minus10 };
                case "Minus9": return new sunssf { _value = ScaleFactors.Minus9 };
                case "Minus8": return new sunssf { _value = ScaleFactors.Minus8 };
                case "Minus7": return new sunssf { _value = ScaleFactors.Minus7 };
                case "Minus6": return new sunssf { _value = ScaleFactors.Minus6 };
                case "Minus5": return new sunssf { _value = ScaleFactors.Minus5 };
                case "Minus4": return new sunssf { _value = ScaleFactors.Minus4 };
                case "Minus3": return new sunssf { _value = ScaleFactors.Minus3 };
                case "Minus2": return new sunssf { _value = ScaleFactors.Minus2 };
                case "Minus1": return new sunssf { _value = ScaleFactors.Minus1 };
                case "Zero": return new sunssf { _value = ScaleFactors.Zero };
                case "Plus1": return new sunssf { _value = ScaleFactors.Plus1 };
                case "Plus2": return new sunssf { _value = ScaleFactors.Plus2 };
                case "Plus3": return new sunssf { _value = ScaleFactors.Plus3 };
                case "Plus4": return new sunssf { _value = ScaleFactors.Plus4 };
                case "Plus5": return new sunssf { _value = ScaleFactors.Plus5 };
                case "Plus6": return new sunssf { _value = ScaleFactors.Plus6 };
                case "Plus7": return new sunssf { _value = ScaleFactors.Plus7 };
                case "Plus8": return new sunssf { _value = ScaleFactors.Plus8 };
                case "Plus9": return new sunssf { _value = ScaleFactors.Plus9 };
                case "Plus10": return new sunssf { _value = ScaleFactors.Plus10 };
                case "NotImplemented": return new sunssf { _value = ScaleFactors.NotImplemented };
                default: throw new ArgumentException("Invalid scale factor.", nameof(value));
            }
        }
    }
}
