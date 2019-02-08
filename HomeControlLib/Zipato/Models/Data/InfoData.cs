// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using Newtonsoft.Json.Linq;

    #endregion

    public class InfoData
    {
        public JArray Configuration { get; set; }
    }
}
