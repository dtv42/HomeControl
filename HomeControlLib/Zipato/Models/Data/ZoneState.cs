// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZoneState.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    public class ZoneState
    {
        public bool? Ready { get; set; }
        public bool? Armed { get; set; }
        public bool? Tripped { get; set; }
        public bool? Bypassed { get; set; }
        public bool? SensorState { get; set; }
        public bool? SensorOffline { get; set; }
    }
}
