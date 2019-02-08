// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddrConverter.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SunSpecLib.Converters
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;

    #endregion

    public class IPAddrJsonConverter : JsonConverter<ipaddr>
    {
        public override void WriteJson(JsonWriter writer, ipaddr value, JsonSerializer serializer)
        {
            var ipaddress = (IPAddress)value;
            writer.WriteValue(ipaddress.ToString());
        }

        public override ipaddr ReadJson(JsonReader reader, Type objectType, ipaddr existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string address = (string)reader.Value;
            ipaddr value = IPAddress.Parse(address);
            return value;
        }
    }

    public class IPAddrTypeConverter : TypeConverter
    {
        // Returns true for a sourceType of string to indicate that
        // conversions from string to integer are supported.
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        // Overrides the ConvertFrom method of TypeConverter.
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string address = (string)value;
                ipaddr result = IPAddress.Parse(address);
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var ipaddress = (IPAddress)value;
                return ipaddress.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
