// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    public class GroupInfo : Entity
    {
        public GroupConfig Config { get; set; }
        public List<EndpointData> Endpoints { get; set; } = new List<EndpointData> { };
        public List<string> IconColors { get; set; } = new List<string> { };
        public InfoData Info { get; set; }
    }
}
