// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationData.cs" company="DTV-Online">
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

    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    public class OperationData : IconEntity
    {
        public List<AttributeData> Attributes { get; set; } = new List<AttributeData> { };
        public List<EndpointEntity> ClusterEndpoints { get; set; } = new List<EndpointEntity> { };
        public OperationConfig Config { get; set; } = new OperationConfig();
        public string Operation { get; set; }
        public bool? Supported { get; set; }
    }
}
