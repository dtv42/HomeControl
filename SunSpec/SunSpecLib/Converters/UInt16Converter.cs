// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UInt16Converter.cs" company="DTV-Online">
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
    using Newtonsoft.Json.Linq;

    #endregion

    public class UInt16JsonConverter : JsonConverter<uint16>
    {
        public override void WriteJson(JsonWriter writer, uint16 value, JsonSerializer serializer)
        {
            writer.WriteValue((ushort)(uint16)value);
        }

        public override uint16 ReadJson(JsonReader reader, Type objectType, uint16 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            uint16 value = Convert.ToUInt16(reader.Value);
            return value;
        }
    }
    public class UInt16TypeConverter : TypeConverter
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
                uint16 result = ushort.Parse((string)value);
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return (ushort)value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
