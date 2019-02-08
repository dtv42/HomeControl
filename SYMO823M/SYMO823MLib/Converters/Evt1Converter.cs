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

    public class Evt1JsonConverter : JsonConverter<Evt1>
    {
        public override void WriteJson(JsonWriter writer, Evt1 value, JsonSerializer serializer)
        {
            writer.WriteValue((int)(Evt1)value);
        }

        public override Evt1 ReadJson(JsonReader reader, Type objectType, Evt1 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Evt1 value = Convert.ToInt32(reader.Value);
            return value;
        }
    }
    public class Evt1TypeConverter : Int32TypeConverter
    {
    }
}
