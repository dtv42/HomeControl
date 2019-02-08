// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Sim.Models
{
    #region Using Directives

    using KWLEC200Lib.Models;

    #endregion

    /// <summary>
    /// THe application settings. The class contains all application settings as properties and are configured
    /// using application configuration files (e.g. appsettings.json), pr environment variables.
    /// </summary>
    public class AppSettings
    {
        public string Address { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 502;
        public byte Slave { get; set; } = 180;
        public KWLEC200Data Data { get; set; } = new KWLEC200Data();
    }
}
