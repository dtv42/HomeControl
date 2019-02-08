// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Config
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class NetworkConfig : ZipaboxConfig
    {
        public string Name { get; set; }
        public string Order { get; set; }
        public List<string> Tags { get; set; } = new List<string> { };
        public Guid? Uuid { get; set; }
    }
}
