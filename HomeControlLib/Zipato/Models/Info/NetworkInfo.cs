// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Info
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using HomeControlLib.Zipato.Models.Config;
    using HomeControlLib.Zipato.Models.Entities;
    using HomeControlLib.Zipato.Extensions;

    #endregion

    public class NetworkInfo : IconEntity
    {
        List<string> Actions { get; set; } = new List<string> { };
        NetworkConfig Config { get; set; } = new NetworkConfig();
        public List<DeviceEntity> Devices { get; set; } = new List<DeviceEntity> { };
        public Dictionary<string, dynamic> State { get; set; } = new Dictionary<string, dynamic> { };
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime StateTimestamp { get; set; }
    }
}
