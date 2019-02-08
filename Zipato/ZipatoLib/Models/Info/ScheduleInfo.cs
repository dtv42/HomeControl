// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    public class ScheduleInfo : IconEntity
    {
        public ScheduleConfig Config { get; set; }
    }
}
