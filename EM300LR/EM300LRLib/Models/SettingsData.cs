// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib.Models
{
    #region Using Directives

    using Microsoft.Extensions.Options;

    #endregion Using Directives

    /// <summary>
    /// Class holding all EM300LR settings (password, and serial number).
    /// </summary>
    public class SettingsData : ISettingsData
    {
        /// <summary>
        /// The Http client settings.
        /// </summary>
        public string BaseAddress { get; set; } = "http://localhost";
        public int Timeout { get; set; } = 10;
        public int Retries { get; set; } = 3;

        /// <summary>
        /// The EM300LR settings;
        /// </summary>
        public string Password { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        public SettingsData()
        { }

        public SettingsData(IOptions<SettingsData> options)
        {
            BaseAddress = options.Value.BaseAddress;
            Timeout = options.Value.Timeout;
            Retries = options.Value.Retries;
            Password = options.Value.Password;
            SerialNumber = options.Value.SerialNumber;
        }
    }
}