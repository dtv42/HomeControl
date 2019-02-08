// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrandInfo.cs" company="DTV-Online">
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

    using ZipatoLib.Models.Entities;
    using ZipatoLib.Models.Info.Brand;

    #endregion

    public class BrandInfo
    {
        public AvailData Avail { get; set; }
        public bool? Available { get; set; }
        public string Description { get; set; }
        public List<UuidEntity> Devices { get; set; } = new List<UuidEntity> { };
        public NameEntity Icon { get; set; }
        public int? Limit { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public List<UuidEntity> Networks { get; set; } = new List<UuidEntity> { };
        public int? Order { get; set; }
        public IdEntity Role { get; set; }
        public string RoleName { get; set; }
    }
}
