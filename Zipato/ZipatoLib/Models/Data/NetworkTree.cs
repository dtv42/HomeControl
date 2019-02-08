// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkTree.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Entities;
    using ZipatoLib.Models.Data.Network;

    #endregion

    public class NetworkTree : UuidEntity
    {
        public List<NetworkDeviceData> Devices { get; set; } = new List<NetworkDeviceData> { };
    }
}
