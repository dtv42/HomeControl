// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthentificationData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib.Models
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion Using Directives

    public class AuthentificationData
    {
        [JsonProperty("http_statuscode")]
        public string HttpStatusCode { get; set; }

        [JsonProperty("ieq_serial")]
        public string IEQSerial { get; set; }

        [JsonProperty("serial")]
        public string SerialNumber { get; set; }

        [JsonProperty("app_version")]
        public string AppVersion { get; set; }

        [JsonProperty("ieqbox_label")]
        public string IEQBoxLabel { get; set; }

        [JsonProperty("auth_mode")]
        public string AuthenticationMode { get; set; }

        [JsonProperty("authentication")]
        public bool Authentication { get; set; }
    }
}