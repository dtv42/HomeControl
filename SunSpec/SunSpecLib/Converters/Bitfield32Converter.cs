// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bitfield32Converter.cs" company="DTV-Online">
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

    public class Bitfield32JsonConverter : JsonConverter<bitfield32>
    {
        public override void WriteJson(JsonWriter writer, bitfield32 value, JsonSerializer serializer)
        {
            writer.WriteValue((uint)(bitfield32)value);
        }

        public override bitfield32 ReadJson(JsonReader reader, Type objectType, bitfield32 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            bitfield32 value = Convert.ToUInt32(reader.Value);
            return value;
        }
    }
    public class Bitfield32TypeConverter : TypeConverter
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
                bitfield32 result = uint.Parse((string)value);
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return (uint)value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
