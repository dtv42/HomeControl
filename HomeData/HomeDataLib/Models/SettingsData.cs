// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataLib.Models
{
    #region Using Directives

    using Microsoft.Extensions.Options;

    #endregion Using Directives

    /// <summary>
    /// Class holding all EM300LR settings (HttpClient, password, and serial number).
    /// </summary>
    public class SettingsData : ISettingsData
    {
        #region Public Properties

        /// <summary>
        /// The Http client settings.
        /// </summary>
        public int Timeout { get; set; } = 10;
        public int Retries { get; set; } = 3;

        /// <summary>
        /// The HomeData specific settings.
        /// </summary>
        public string Meter1Address { get; set; } = string.Empty;
        public string Meter2Address { get; set; } = string.Empty;

        #endregion

        #region Constructors

        public SettingsData()
        { }

        public SettingsData(IOptions<SettingsData> options)
        {
            Timeout = options.Value.Timeout;
            Retries = options.Value.Retries;
            Meter1Address = options.Value.Meter1Address;
            Meter2Address = options.Value.Meter2Address;
        }

        #endregion
    }
}
