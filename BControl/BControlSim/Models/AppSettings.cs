// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlSim.Models
{
    #region Using Directives

    using NModbusLib.Models;
    using BControlLib.Models;

    #endregion

    /// <summary>
    /// THe application settings. The class contains all application settings as properties and are configured
    /// using application configuration files (e.g. appsettings.json), or environment variables.
    /// </summary>
    public class AppSettings
    {
        public TcpSlaveData Slave { get; set; } = new TcpSlaveData();
        public BControlData Data { get; set; } = new BControlData();
    }
}
