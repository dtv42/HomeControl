// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Provides properties for the Netatmo access token.
    /// </summary>
    public class TokenData
    {
        #region Public Properties

        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        #endregion
    }
}