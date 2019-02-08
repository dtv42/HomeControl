// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipaboxEntity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Entities
{
    #region Using Directives

    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// Class providing properties for Zipato entity information.
    /// </summary>
    public class ZipaboxEntity : EndpointEntity
    {
        public string Order { get; set; }
        public string Description { get; set; }
        public Dictionary<string, JObject> Extra { get; set; } = new Dictionary<string, JObject> { };
    }

}
