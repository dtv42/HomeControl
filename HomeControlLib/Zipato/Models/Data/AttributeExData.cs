// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeExData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using HomeControlLib.Zipato.Models.Entities;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class AttributeExData : AttributeEntity
    {
        public string Name { get; set; }
        public EndpointEntity ClusterEndpoint { get; set; }
        public DeviceEntity Device { get; set; }
        public EndpointEntity Endpoint { get; set; }
    }
}
