// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    #region Using Directives

    using Microsoft.Extensions.Options;

    #endregion Using Directives

    /// <summary>
    /// Class holding all Netatmo settings.
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
        /// The Netatmo specific settings.
        /// </summary>
        public string User { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; } = "read_station";

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
            ClientID = options.Value.ClientID;
            ClientSecret = options.Value.ClientSecret;
        }

        #endregion
    }
}