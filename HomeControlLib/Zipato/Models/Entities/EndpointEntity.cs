// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointEntity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Entities
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class EndpointEntity : Entity
    {
        public List<string> Tags { get; set; } = new List<string> { };
    }
}
