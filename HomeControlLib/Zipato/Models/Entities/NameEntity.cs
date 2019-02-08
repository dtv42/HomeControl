// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameEntity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Entities
{
    /// <summary>
    /// Class providing properties for Zipato named entity information.
    /// </summary>
    public class NameEntity
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public string EndpointType { get; set; }
        public string RelativeUrl { get; set; }
    }
}
