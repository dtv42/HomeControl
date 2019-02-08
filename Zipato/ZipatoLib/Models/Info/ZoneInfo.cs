// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZoneInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Dtos;
    using ZipatoLib.Models.Entities;

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
