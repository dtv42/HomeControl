// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App.Models
{
    public class HubSettings : IHubSettings
    {
        public string Uri { get; set; } = string.Empty;
        public int ServerTimeout { get; set; } = 30;
        public int HandshakeTimeout { get; set; } = 15;
    }
}
