// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UInt64Converter.cs" company="DTV-Online">
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
    using System.Numerics;
    using Newtonsoft.Json;

    #endregion

    public class UInt64JsonConverter : JsonConverter<uint64>
    {
        public override void WriteJson(JsonWriter writer, uint64 value, JsonSerializer serializer)
        {
            writer.WriteValue((ulong)(uint64)value);
        }

        public override uint64 ReadJson(JsonReader reader, Type objectType, uint64 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            uint64 result;

            switch (reader.Value)
            {
                case Int64 value:
                    result = Convert.ToUInt64(value);
                    break;
                case UInt64 value:
                    result = Convert.ToUInt64(value);
                    break;
                case BigInteger value:
                    result = (ulong)value;
                    break;
                default:
                    result = uint64.NOT_IMPLEMENTED;
                    break;
            }

            return result;
        }
    }

    public class UInt64TypeConverter : TypeConverter
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
                uint64 result = ulong.Parse((string)value);
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return (ulong)value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
