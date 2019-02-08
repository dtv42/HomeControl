// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InverterData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class RawInverterData
    {
        public int DT { get; set; }
        public int PVPower { get; set; }
        public int Show { get; set; }
        public string UniqueID { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string CustomName { get; set; } = string.Empty;
    }
}
