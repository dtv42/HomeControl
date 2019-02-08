// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Network.NetworkDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data.Network
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Entities;

    #endregion

    public class NetworkDeviceData : Entity
    {
        public List<NetworkEndpointData> Endpoints { get; set; } = new List<NetworkEndpointData> { };
    }
}
