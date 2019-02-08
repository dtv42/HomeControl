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

    public class DERTypeJsonConverter : JsonConverter<DERType>
    {
        public override void WriteJson(JsonWriter writer, DERType value, JsonSerializer serializer)
        {
            writer.WriteValue((short)(DERType)value);
        }

        public override DERType ReadJson(JsonReader reader, Type objectType, DERType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            DERType value = Convert.ToInt16(reader.Value);
            return value;
        }
    }
    public class DERTypeTypeConverter : Int16TypeConverter
    {
    }
}
