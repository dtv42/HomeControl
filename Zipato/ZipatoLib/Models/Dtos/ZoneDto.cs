// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZoneDto.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class ZoneDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? PositionX { get; set; }
        public double? PositionY { get; set; }
        public LocationDto Location { get; set; }
        public int? Id { get; set; }
   }
}
