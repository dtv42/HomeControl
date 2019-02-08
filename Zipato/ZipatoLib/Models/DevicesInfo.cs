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
    /// Helper class holding endpoint UUIDs.
    /// </summary>
    public class DevicesInfo
    {
        public List<Guid> Switches { get; set; } = new List<Guid> { };
        public List<Guid> OnOffSwitches { get; set; } = new List<Guid> { };
        public List<Guid> Dimmers { get; set; } = new List<Guid> { };
        public List<Guid> Wallplugs { get; set; } = new List<Guid> { };
        public List<Guid> RGBControls { get; set; } = new List<Guid> { };
    }
}
