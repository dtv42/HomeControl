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

    public class ECPConnJsonConverter : JsonConverter<ECPConn>
    {
        public override void WriteJson(JsonWriter writer, ECPConn value, JsonSerializer serializer)
        {
            writer.WriteValue((short)(ECPConn)value);
        }

        public override ECPConn ReadJson(JsonReader reader, Type objectType, ECPConn existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ECPConn value = Convert.ToInt16(reader.Value);
            return value;
        }
    }
    public class ECPConnTypeConverter : Int16TypeConverter
    {
    }
}
