// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IZipatoClient.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System.Net.Http;
    using System.Threading.Tasks;
    using ZipatoLib.Models;

    #endregion

    public interface IZipatoClient : IHttpClientSettings
    {
        Task<string> GetStringAsync(string request);
        Task<HttpResponseMessage> GetAsync(string request);
        Task<HttpResponseMessage> PutAsync(string request, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string request, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string request);
    }
}
