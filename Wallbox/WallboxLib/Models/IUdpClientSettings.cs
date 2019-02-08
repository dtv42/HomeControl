// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUdpClientSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
{
    public interface IUdpClientSettings
    {
        string HostName { get; set; }
        int Port { get; set; }
        double Timeout { get; set; }
    }
}