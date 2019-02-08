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

    public class EvtVnd2JsonConverter : JsonConverter<EvtVnd2>
    {
        public override void WriteJson(JsonWriter writer, EvtVnd2 value, JsonSerializer serializer)
        {
            writer.WriteValue((int)(EvtVnd2)value);
        }

        public override EvtVnd2 ReadJson(JsonReader reader, Type objectType, EvtVnd2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            EvtVnd2 value = Convert.ToInt32(reader.Value);
            return value;
        }
    }
    public class EvtVnd2TypeConverter : Int32TypeConverter
    {
    }
}
