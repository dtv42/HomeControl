// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRClient.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib
{
    #region Using Directives

    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using BaseClassLib;

    #endregion Using Directives

    public class EM300LRClient : BaseClass, IEM300LRClient
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client used internally.
        /// </summary>
        private HttpClient _client;

        #endregion Private Data Members

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

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EM300LRClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        public EM300LRClient(HttpClient client,
                             ILogger<EM300LRClient> logger) : base(logger)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("User-Agent", "EM300LRClient");
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Helper method to perform a GET request and return the response as a string.
        /// </summary>
        /// <param name="request">The HTTP request</param>
        /// <returns>The string result.</returns>
        public async Task<string> GetStringAsync(string request)
        {
            var response = await _client.GetAsync(request);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
        }

        /// <summary>
        /// Helper method to perform a POST request and return the response as a string.
        /// </summary>
        /// <param name="request">The HTTP request</param>
        /// <param name="content">The POST content</param>
        /// <returns>The string result.</returns>
        public async Task<string> PostStringAsync(string request, string content)
        {
            var stringcontent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _client.PostAsync(request, stringcontent);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
        }

        #endregion Public Methods
    }
}