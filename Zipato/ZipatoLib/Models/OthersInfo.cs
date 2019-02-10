// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevicesInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Helper class holding scene UUIDs.
    /// </summary>
    public class OthersInfo
    {
        public List<Guid> Cameras { get; set; } = new List<Guid> { };
        public List<Guid> Scenes { get; set; } = new List<Guid> { };
    }
}
