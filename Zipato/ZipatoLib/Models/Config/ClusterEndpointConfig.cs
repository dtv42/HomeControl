// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClusterEndpointConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Config
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class ClusterEndpointConfig
    {
        public bool? Hidden { get; set; }
        public bool? HiddenRules { get; set; }
        public bool? IsSecurity { get; set; }
        public string Name { get; set; }
        public string Order { get; set; }
        public List<string> QueryAttributes { get; set; } = new List<string> { };
        public int? QueryInterval { get; set; }
        public int? Room { get; set; }
        public bool? ShouldBeQueried { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public Guid? Uuid { get; set; }
    }
}
