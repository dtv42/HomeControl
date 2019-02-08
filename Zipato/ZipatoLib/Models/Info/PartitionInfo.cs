// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartitionInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class PartitionInfo : IconEntity
    {
        public List<AttributeInfo> Attributes { get; set; } = new List<AttributeInfo> { };
        public PartitionConfig Config { get; set; }
        public List<string> IconColors { get; set; } = new List<string> { };
        public AlarmState State { get; set; }
        public DateTime? StateTimestamp { get; set; }
        public List<ZoneInfo> Zones { get; set; } = new List<ZoneInfo> { };
    }
}
