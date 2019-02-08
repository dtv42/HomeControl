// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrandData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    public class BrandData
    {
        public string Description { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public string Role { get; set; }
    }
}
