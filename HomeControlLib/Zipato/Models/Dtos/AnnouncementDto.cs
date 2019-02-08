// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnnouncementDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Dtos
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using HomeControlLib.Zipato.Models.Enums;

    #endregion

    /// <summary>
    /// Class providing properties for Zipato announcement data.
    /// </summary>
    public class AnnouncementDto
    {
        public string Name { get; set; }
        public UserDto Author { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AnnouncementLevel? Level { get; set; }
        public bool? Active { get; set; }
        public DateTime? Created { get; set; }
        public bool? ToAll { get; set; }
        public List<MessageDto> Messages { get; set; } = new List<MessageDto> { };
        public List<UserDto> Users { get; set; } = new List<UserDto> { };
        public List<ZipatoDto> Zipatos { get; set; } = new List<ZipatoDto> { };
        public List<RealmDto> Realms { get; set; } = new List<RealmDto> { };
        public int? Id { get; set; }
    }
}
