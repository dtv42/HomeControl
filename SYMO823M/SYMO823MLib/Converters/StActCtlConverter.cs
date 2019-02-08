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

    public class StActCtlJsonConverter : JsonConverter<StActCtl>
    {
        public override void WriteJson(JsonWriter writer, StActCtl value, JsonSerializer serializer)
        {
            writer.WriteValue((short)(StActCtl)value);
        }

        public override StActCtl ReadJson(JsonReader reader, Type objectType, StActCtl existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            StActCtl value = Convert.ToInt16(reader.Value);
            return value;
        }
    }
    public class StActCtlTypeConverter : Int16TypeConverter
    {
    }
}
