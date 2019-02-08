// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmLogDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Dtos
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    using ZipatoLib.Models.Enums;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class AlarmLogDto
    {
        public int? Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevels? Levels { get; set; }
        public int? AlarmId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Message { get; set; }
        public int? Code { get; set; }
        public string Subtype { get; set; }
        public int? SubtypeId { get; set; }
        public string SubtypeUuid { get; set; }
        public bool? NeedAck { get; set; }
        public List<JObject> Acks { get; set; } = new List<JObject> { };
        public Dictionary<string, JObject> Data { get; set; } = new Dictionary<string, JObject> { };
    }
}
