// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Brand.AvailData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info.Brand
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Dtos;

    #endregion

    public class AvailData
    {
        public int? Id { get; set; }
        public string ClassName { get; set; }
        public bool? HwRequired { get; set; }
        public bool? AllRealms { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
        public List<RealmDto> Realms { get; set; } = new List<RealmDto> { };
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
