﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PadConverter.cs" company="DTV-Online">
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

    #endregion

    public class PadJsonConverter : JsonConverter<pad>
    {
        public override void WriteJson(JsonWriter writer, pad value, JsonSerializer serializer)
        {
            writer.WriteValue((ushort)(pad)value);
        }

        public override pad ReadJson(JsonReader reader, Type objectType, pad existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            pad value = Convert.ToUInt16(reader.Value);
            return value;
        }
    }
    public class PadTypeConverter : TypeConverter
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
                pad result = ushort.Parse((string)value);
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
