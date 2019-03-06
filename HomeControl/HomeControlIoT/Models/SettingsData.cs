// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlIoT.Models
{
    #region Using Directives

    using Microsoft.Extensions.Options;

    #endregion Using Directives

    /// <summary>
    /// Class holding all Initial State settings.
    /// </summary>
    internal class SettingsData : ISettingsData
    {
        #region Public Properties

        /// <summary>
        /// The Http client settings.
        /// </summary>
        public string BaseAddress { get; set; } = string.Empty;
        public int Timeout { get; set; } = 10;
        public int Retries { get; set; } = 3;

        /// <summary>
        /// The Initial State settings.
        /// </summary>
        public string BucketKey { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;

        #endregion

        #region Constructors

        public SettingsData()
        { }

        public SettingsData(IOptions<SettingsData> options)
        {
            Timeout = options.Value.Timeout;
            Retries = options.Value.Retries;
        }

        #endregion
    }
}
