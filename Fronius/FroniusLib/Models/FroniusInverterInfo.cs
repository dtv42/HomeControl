// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusInverterInfo.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class FroniusInverterInfo
    {
        public HeaderData Head { get; set; } = new HeaderData();
        public BodyInverterInfo Body { get; set; } = new BodyInverterInfo();
    }
}
