// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWallboxClient.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib
{
    #region Using Directives

    using System.Threading.Tasks;

    using WallboxLib.Models;

    #endregion

    public interface IWallboxClient : IUdpClientSettings
    {
        Task<string> SendReceiveAsync(string message);
    }
}
