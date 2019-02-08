// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NModbusRTU.Models
{
    #region Using Directives

    using NModbusLib.Models;
    using NModbusRTU.Swagger;

    #endregion

    /// <summary>
    /// Helper class to hold application specific settings.
    /// </summary>
    public class AppSettings : IRtuClientSettings
    {
        #region Public Properties

        /// <summary>
        /// The MODBUS RTU master configuration.
        /// </summary>
        public RtuMasterData RtuMaster { get; set; } = new RtuMasterData();

        /// <summary>
        /// The MODBUS RTU slave configuration.
        /// </summary>
        public RtuSlaveData RtuSlave { get; set; } = new RtuSlaveData();

        /// <summary>
        /// The Swagger options.
        /// </summary>
        public SwaggerOptionSettings Swagger { get; set; } = new SwaggerOptionSettings();

        #endregion
    }
}
