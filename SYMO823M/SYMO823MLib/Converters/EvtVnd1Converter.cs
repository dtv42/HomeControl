// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enum32Converter.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib.Converters
{
    #region Using Directives

    using Newtonsoft.Json;
    using SunSpecLib;
    using SunSpecLib.Converters;
    using SYMO823MLib.Models;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    #endregion

    public class EvtVnd1JsonConverter : JsonConverter<EvtVnd1>
    {
        public override void WriteJson(JsonWriter writer, EvtVnd1 value, JsonSerializer serializer)
        {
            writer.WriteValue((int)(EvtVnd1)value);
        }

        public override EvtVnd1 ReadJson(JsonReader reader, Type objectType, EvtVnd1 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            EvtVnd1 value = Convert.ToInt32(reader.Value);
            return value;
        }
    }
    public class EvtVnd1TypeConverter : Int32TypeConverter
    {
    }
}
