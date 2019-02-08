// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimestampConverter.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Extensions
{
    #region Using Directives

    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    /// <summary>
    /// Custom converter for DateTime values (Zipato timestamps).
    /// </summary>
    public class TimestampConverter : DateTimeConverterBase
    {
        #region Public Methods

        /// <summary>
        /// Read the DateTime value (UNIX timestamp in msec, or DateTime string).
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns>A DateTime object.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Check for UNIX timestamp.
            if (reader.TokenType == JsonToken.Integer)
            {
                var ticks = (long)reader.Value;

                var date = new DateTime(1970, 1, 1);
                date = date.AddSeconds(ticks / 1000);

                return date;
            }
            else if (reader.TokenType == JsonToken.Date)
            {
                return (DateTime)reader.Value;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                return DateTime.Parse((string)reader.Value);
            }
            else if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else
            {
                throw new Exception($"Unexpected token parsing date: '{reader.TokenType}'.");
            }
        }

        /// <summary>
        /// Write the DateTime value (DateTime string).
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        #endregion
    }
}
