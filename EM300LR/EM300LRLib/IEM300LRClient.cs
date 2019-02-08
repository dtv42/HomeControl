// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEM300LRClient.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib
{
    #region Using Directives

    using System.Threading.Tasks;

    using EM300LRLib.Models;

    #endregion Using Directives

    public interface IEM300LRClient : IHttpClientSettings
    {
        Task<string> GetStringAsync(string request);
        Task<string> PostStringAsync(string request, string content);
    }
}