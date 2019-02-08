// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeUrlEntity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Entities
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// Class providing properties for Zipato attribute information.
    /// </summary>
    public class AttributeUrlEntity
    {
        public string Attribute { get; set; }
        public int? AttributeId { get; set; }
        public Guid? Uuid { get; set; }
        public string Url { get; set; }
    }
}
