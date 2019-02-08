// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UuidEntity.cs" company="DTV-Online">
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
    ///
    /// </summary>
    public class UuidEntity
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public Guid? Uuid { get; set; }
    }
}
