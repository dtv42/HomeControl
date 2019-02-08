// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
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
        /// The Udp client settings.
        /// </summary>
        public string HostName { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 7090;
        public double Timeout { get; set; } = 10;

        #endregion

        #region Constructors

        public SettingsData()
        { }

        public SettingsData(IOptions<SettingsData> options)
        {
            HostName = options.Value.HostName;
            Port = options.Value.Port;
            Timeout = options.Value.Timeout;
        }

        #endregion
    }
}