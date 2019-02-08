// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ipaddr.cs" company="DTV-Online">
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
    /// Sunspec 32-bit integer values: ipaddr
    /// 32-bit IPv4 address
    /// Not Configured: 0x00000000
    /// </summary>
    [JsonConverter(typeof(IPAddrJsonConverter))]
    [TypeConverter(typeof(IPAddrTypeConverter))]
    public struct ipaddr
    {
        private IPAddress _value;

        public bool NotConfigured
        {
            get => (_value == null) || _value.GetAddressBytes().Equals(new byte[4]);
        }

        public static implicit operator ipaddr(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 2)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new ipaddr { _value = new IPAddress(ModbusConversions.ToBytes(registers)) };
        }

        public static implicit operator ipaddr(IPAddress value)
            => new ipaddr { _value = value };

        public static implicit operator IPAddress(ipaddr value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value.GetAddressBytes());

        public override string ToString()
            => _value.ToString();
    }
}
