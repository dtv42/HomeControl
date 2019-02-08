// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Int64Converter.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SunSpecLib.Converters
{
    #region Using Directives

    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    #endregion

    public class Int64JsonConverter : JsonConverter<int64>
    {
        public override void WriteJson(JsonWriter writer, int64 value, JsonSerializer serializer)
        {
            writer.WriteValue((long)(int64)value);
        }

        public override int64 ReadJson(JsonReader reader, Type objectType, int64 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            int64 value = Convert.ToInt64(reader.Value);
            return value;
        }
    }
    public class Int64TypeConverter : TypeConverter
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
                int64 result = long.Parse((string)value);
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return (long)value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
