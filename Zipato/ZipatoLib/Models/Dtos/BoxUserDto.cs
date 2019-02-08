// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoxUserDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Dtos
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class BoxUserDto
    {
        public int? UserId { get; set; }
        public string Uuid { get; set; }
        public UserDto User { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public Dictionary<string, JObject> DataMap { get; set; } = new Dictionary<string, JObject> { };
        public string UiData { get; set; }
        public bool? Deleted { get; set; }
        public int? Id { get; set; }
    }
}
