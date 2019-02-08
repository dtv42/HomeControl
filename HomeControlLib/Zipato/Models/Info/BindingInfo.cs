// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;

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
