// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    #region Using Directives

    using Microsoft.Extensions.Options;

    #endregion Using Directives

    /// <summary>
    /// Class holding all Fronius settings.
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

        /// <summary>
        /// The Fronius specific settings.
        /// </summary>
        public string DeviceID { get; set; } = "1";

        #endregion

        #region Constructors

        public SettingsData()
        { }

        public SettingsData(IOptions<SettingsData> options)
        {
            BaseAddress = options.Value.BaseAddress;
            Timeout = options.Value.Timeout;
            Retries = options.Value.Retries;
            DeviceID = options.Value.DeviceID;
        }

        #endregion
    }
}