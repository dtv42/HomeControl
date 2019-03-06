// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInitialStateClient.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlIoT.Models
{
    #region Using Directives

    using System.Threading.Tasks;
    using System.Net.Http;

    #endregion

    internal interface IInitialStateClient : IHttpClientSettings
    {
        Task<HttpResponseMessage> PostAsync(string request, StringContent content);
    }
}
