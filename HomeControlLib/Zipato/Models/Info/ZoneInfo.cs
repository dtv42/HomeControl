// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZoneInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Data;
    using HomeControlLib.Zipato.Models.Dtos;
    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class ZoneInfo : UuidEntity
    {
        public UuidEntity Attribute { get; set; }
        public ZipaboxEntity ClusterEndpoint { get; set; }
        public ZoneConfig Config { get; set; }
        public ZipaboxEntity Device { get; set; }
        public ZipaboxEntity Endpoint { get; set; }
        public ZipaboxEntity Network { get; set; }
        public ZoneState State { get; set; }
        public AttributeValueDto Value { get; set; }
    }
}
