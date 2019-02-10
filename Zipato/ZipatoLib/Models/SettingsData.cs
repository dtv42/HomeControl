// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Net;
    using Microsoft.Extensions.Options;
    using ZipatoLib.Models.Devices;

    #endregion Using Directives

    /// <summary>
    /// Class holding all Zipato settings.
    /// </summary>
    public class SettingsData : ISettingsData
    {
        #region Public Properties

        /// <summary>
        /// The Http client settings.
        /// </summary>
        public string BaseAddress { get; set; } = "http://localhost";
        public int Timeout { get; set; } = 10;
        public int Retries { get; set; } = 3;
        public CookieContainer Cookies { get; set; } = new CookieContainer();

        /// <summary>
        /// The Zipato specific settings.
        /// </summary>
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsLocal { get; set; } = true;
        public int SessionTimeout { get; set; } = 120000;

        /// <summary>
        /// The selected scenes, devices, and sensors.
        /// </summary>
        public OthersInfo OthersInfo { get; set; } = new OthersInfo();
        public DevicesInfo DevicesInfo { get; set; } = new DevicesInfo();
        public SensorsInfo SensorsInfo { get; set; } = new SensorsInfo();

        #endregion

        #region Constructors

        public SettingsData()
        { }

        public SettingsData(IOptions<SettingsData> options)
        {
            BaseAddress = options.Value.BaseAddress;
            Timeout = options.Value.Timeout;
            Retries = options.Value.Retries;
            User = options.Value.User;
            Password = options.Value.Password;
            IsLocal = options.Value.IsLocal;

            OthersInfo = options.Value.OthersInfo;
            DevicesInfo = options.Value.DevicesInfo;
            SensorsInfo = options.Value.SensorsInfo;
        }

        #endregion
    }
}
