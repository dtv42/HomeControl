// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SunSSFConverter.cs" company="DTV-Online">
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
    using Newtonsoft.Json;
    using NModbus.Extensions;

    #endregion

    public class SunSSFJsonConverter : JsonConverter<sunssf>
    {
        public override void WriteJson(JsonWriter writer, sunssf value, JsonSerializer serializer)
        {
            writer.WriteValue((ushort)(sunssf)value);
        }

        public override sunssf ReadJson(JsonReader reader, Type objectType, sunssf existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            sunssf value;

            switch (Type.GetTypeCode(reader.ValueType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    value = Convert.ToUInt16(reader.Value);
                    break;
                case TypeCode.String:
                    value = (string)reader.Value;
                    break;
                default:
                    value = new sunssf();
                    break;
            }

            return value;
        }
    }

    public class SunSSFTypeConverter : TypeConverter
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
                sunssf result = ushort.Parse((string)value);
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return (short)value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
