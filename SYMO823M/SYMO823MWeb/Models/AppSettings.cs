// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MWeb.Models
{
    #region Using Directives

    using NModbusLib.Models;

    #endregion

    /// <summary>
    /// Helper class to hold application specific settings.
    /// </summary>
    public class AppSettings
    {
        #region Public Properties

        /// <summary>
        /// The MODBUS TCP master configuration.
        /// </summary>
        public TcpMasterData Master { get; set; } = new TcpMasterData();

        /// <summary>
        /// The MODBUS TCP slave configuration.
        /// </summary>
        public TcpSlaveData Slave { get; set; } = new TcpSlaveData();

        /// <summary>
        /// Update timer period in msec.
        /// </summary>
        public int UpdatePeriod { get; set; } = 10000;

        /// <summary>
        /// The Swagger options.
        /// </summary>
        public SwaggerOptions Swagger { get; set; } = new SwaggerOptions();

        #endregion
    }
}
