// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SceneInfo.cs" company="DTV-Online">
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
    using ZipatoLib.Models.Entities;

    #endregion

    public class SceneInfo : UuidEntity
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Order { get; set; }
        public List<SettingsData> Settings { get; set; } = new List<SettingsData> { };
        public List<string> Tags { get; set; } = new List<string> { };
    }
}
