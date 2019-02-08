// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrandsUsedData.cs" company="DTV-Online">
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

    using ZipatoLib.Models.Info;

    #endregion

    public class BrandsUsedData
    {
        public int? Total { get; set; }
        public int? Limit { get; set; }
        public List<BrandData> BrandList { get; set; } = new List<BrandData> { };
        public List<BrandInfo> DeviceBrands { get; set; } = new List<BrandInfo> { };
        public List<BrandInfo> NetworkBrands { get; set; } = new List<BrandInfo> { };
    }
}
