// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartitionData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class PartitionData : IconEntity
    {
        public List<AttributeEntity> Attributes { get; set; } = new List<AttributeEntity> { };
        public List<ZoneData> Zones { get; set; } = new List<ZoneData> { };
    }
}
