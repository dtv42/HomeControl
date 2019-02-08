// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorInfo.cs" company="DTV-Online">
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
    ///
    /// </summary>
    public class MonitorInfo : IconEntity
    {
        public ZipaboxEntity Alarm { get; set; }
        public MonitorConfig Config { get; set; }
        public List<string> IconColors { get; set; } = new List<string> { };
    }
}
