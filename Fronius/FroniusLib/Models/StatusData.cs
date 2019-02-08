// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    public class StatusData
    {
        public int Code { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string UserMessage { get; set; } = string.Empty;
    }
}
