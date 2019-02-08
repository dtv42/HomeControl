// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeData.cs" company="DTV-Online">
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

    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class AttributeData : AttributeEntity
    {
        public string AttributeName { get; set; }
        public int? Room { get; set; }
    }
}
