// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventEntity.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Entities
{
    /// <summary>
    /// Class providing properties for Zipato id entity information.
    /// </summary>
    public class EventEntity : IdEntity
    {
        public string ClassName { get; set; }
    }
}
