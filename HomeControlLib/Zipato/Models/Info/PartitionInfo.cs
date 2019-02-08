// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartitionInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;

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
