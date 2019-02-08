// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SceneData.cs" company="DTV-Online">
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

    using ZipatoLib.Models.Entities;

    #endregion

    public class SceneData : UuidEntity
    {
        public List<string> Tags { get; set; } = new List<string> { };
    }
}
