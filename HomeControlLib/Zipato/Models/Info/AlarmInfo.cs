// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlarmInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    /// Class providing properties for Zipato alarm data.
    /// </summary>
    public class AlarmInfo : IconEntity
    {
        public AlarmConfig Config { get; set; }
        public List<PartitionInfo> Partitions { get; set; } = new List<PartitionInfo> { };
        public List<MonitorInfo> Monitors { get; set; } = new List<MonitorInfo> { };
        public List<string> IconColors { get; set; } = new List<string> { };
        public IdEntity Room { get; set; }
    }
}
