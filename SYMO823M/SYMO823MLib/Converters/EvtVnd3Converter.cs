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

    public class EvtVnd3JsonConverter : JsonConverter<EvtVnd3>
    {
        public override void WriteJson(JsonWriter writer, EvtVnd3 value, JsonSerializer serializer)
        {
            writer.WriteValue((int)(EvtVnd3)value);
        }

        public override EvtVnd3 ReadJson(JsonReader reader, Type objectType, EvtVnd3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            EvtVnd3 value = Convert.ToInt32(reader.Value);
            return value;
        }
    }
    public class EvtVnd3TypeConverter : Int32TypeConverter
    {
    }
}
