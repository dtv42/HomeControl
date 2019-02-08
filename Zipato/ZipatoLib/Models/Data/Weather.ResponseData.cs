// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.ResponseData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data.Weather
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class ResponseData
    {
        public string Version { get; set; }
        public Dictionary<string, int> Features { get; set; } = new Dictionary<string, int> { };
        //"error": null
    }
}
