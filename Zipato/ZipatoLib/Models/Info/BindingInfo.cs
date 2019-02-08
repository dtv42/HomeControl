// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    public class BindingInfo : UuidEntity
    {
        public BindingConfig Config { get; set; }
        public DeviceEntity Device { get; set; }
        public EndpointEntity Endpoint { get; set; }
        public List<UuidEntity> Endpoints { get; set; } = new List<UuidEntity> { };
        public List<UuidEntity> Groups { get; set; } = new List<UuidEntity> { };
        public UuidEntity Network { get; set; }
    }
}
