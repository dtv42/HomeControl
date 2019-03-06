// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHubSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App.Models
{
    public interface IHubSettings
    {
        string Uri { get; set; }
        int ServerTimeout { get; set; }
        int HandshakeTimeout { get; set; }
    }
}
