// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ipv6addr.cs" company="DTV-Online">
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
    using System.Net;
    using Newtonsoft.Json;
    using NModbus.Extensions;
    using SunSpecLib.Converters;

    #endregion

    /// <summary>
    /// Sunspec 128-bit integer values: ipv6addr
    /// 128-bit IPv6 address
    /// Not Configured: 0
    /// </summary>
    [JsonConverter(typeof(IPV6AddrJsonConverter))]
    [TypeConverter(typeof(IPV6AddrTypeConverter))]
    public struct ipv6addr
    {
        private IPAddress _value;

        public bool NotConfigured
        {
            get => (_value == null) || _value.GetAddressBytes().Equals(new byte[16]);
        }

        public static implicit operator ipv6addr(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 4)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new ipv6addr { _value = new IPAddress(ModbusConversions.ToBytes(registers)) };
        }

        public static implicit operator ipv6addr(IPAddress value)
            => new ipv6addr { _value = value };

        public static implicit operator IPAddress(ipv6addr value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value.GetAddressBytes());

        public override string ToString()
            => _value.ToString();
    }
}
