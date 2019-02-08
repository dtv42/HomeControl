// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceConfiguration.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Config
{
    #region Using Directives

    using System.Collections.Generic;
    using ZipatoLib.Models.Config.Device;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class DeviceConfiguration
    {
        public List<ConfigurationData> Configuration { get; set; } = new List<ConfigurationData> { };
    }
}
