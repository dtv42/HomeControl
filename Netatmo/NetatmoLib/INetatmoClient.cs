// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INetatmoClient.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib
{
    #region Using Directives

    using System.Net.Http;
    using System.Threading.Tasks;

    using NetatmoLib.Models;

    #endregion

    public interface INetatmoClient : IHttpClientSettings
    {
        Task<string> GetStringAsync(string request);
        Task<string> PostAsync(string request, HttpContent content);
    }
}
