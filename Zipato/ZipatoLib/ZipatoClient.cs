// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoClient.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using BaseClassLib;
    using ZipatoLib.Models;

    #endregion

    public class ZipatoClient : BaseClass, IZipatoClient
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client used in HTTP requests.
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// Cookie container used in HTTP requests.
        /// </summary>
        private readonly CookieContainer _cookies = new CookieContainer();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the base address of the Internet resource used when sending requests.
        /// </summary>
        public string BaseAddress
        {
            get => _client.BaseAddress.OriginalString;
            set => _client.BaseAddress = new Uri(value);
        }

        /// <summary>
        /// Gets or sets the timespan to wait before the request times out.
        /// </summary>
        public int Timeout
        {
            get => (int)_client.Timeout.TotalSeconds;
            set => _client.Timeout = TimeSpan.FromSeconds(value);
        }

        /// <summary>
        /// Gets or sets the number of retries sending a request.
        /// </summary>
        public int Retries { get; set; }

        /// <summary>
        /// Gets or sets the cookie container used in sending a request.
        /// </summary>
        public CookieContainer Cookies { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        public ZipatoClient(HttpClient client,
                            ISettingsData settings,
                            ILogger<ZipatoClient> logger) : base(logger)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("User-Agent", "ZipatoClient");
            Cookies = settings.Cookies;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper method to perform a GET request and return the response as a string.
        /// </summary>
        /// <param name="request">The HTTP request</param>
        /// <returns>The string result.</returns>
        public async Task<string> GetStringAsync(string request)
            => await _client.GetStringAsync(request);

        /// <summary>
        /// Helper method to perform a GET request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string request)
            => await _client.GetAsync(request);

        /// <summary>
        /// Helper method to perform a PUT request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync(string request, HttpContent content)
            => await _client.PutAsync(request, content);

        /// <summary>
        /// Helper method to perform a POST request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string request, HttpContent content)
            => await _client.PostAsync(request, content);

        /// <summary>
        /// Helper method to perform a DELETE request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string request)
            => await _client.DeleteAsync(request);

        #endregion
    }
}
