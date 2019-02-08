// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class MonitorData : UuidEntity
    {
        public ZipaboxEntity Alarm { get; set; }
    }
}
