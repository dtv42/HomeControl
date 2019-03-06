// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitialStateValue.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlIoT.Models
{
    #region Using Directives

    using System;
    using Newtonsoft.Json;

    #endregion

    internal class InitialStateValue
    {
        private double _value;

        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;

        [JsonProperty("value")]
        public double Value { get => _value; set { _value = value; Timestamp = DateTime.Now.ToString("o"); } }

        [JsonProperty("iso8601")]
        public string Timestamp { get; set; } = DateTime.Now.ToString("o");
    }
}
