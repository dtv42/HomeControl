// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enum16Converter.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib.Converters
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Newtonsoft.Json;
    using SunSpecLib;
    using SunSpecLib.Converters;
    using SYMO823MLib.Models;

    #endregion

    public class StorConnJsonConverter : JsonConverter<StorConn>
    {
        public override void WriteJson(JsonWriter writer, StorConn value, JsonSerializer serializer)
        {
            writer.WriteValue((short)(StorConn)value);
        }

        public override StorConn ReadJson(JsonReader reader, Type objectType, StorConn existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            StorConn value = Convert.ToInt16(reader.Value);
            return value;
        }
    }
    public class StorConnTypeConverter : Int16TypeConverter
    {
    }
}
