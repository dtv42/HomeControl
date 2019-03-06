// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeterValues.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlIoT.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using BaseClassLib;

    #endregion

    internal class MeterValues : BaseClass
    {
        #region Private Data Members

        /// <summary>
        /// The zipato HTTP client.
        /// </summary>
        private readonly IInitialStateClient _client;

        #endregion

        #region Protected Data Members

        /// <summary>
        /// The settings instance.
        /// </summary>
        protected readonly AppSettings _settings;

        #endregion Protected Data Members

        #region Public Properties

        /// <summary>
        /// Set of data values written to Initial State web service.
        /// </summary>
        public List<InitialStateValue> Values { get; } = new List<InitialStateValue> { };

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public MeterValues(IInitialStateClient client,
                           ILogger<MeterValues> logger,
                           IOptions<AppSettings> options) : base(logger)
        {
            _client = client;
            _settings = options.Value;
            _logger?.LogDebug("MeterValues()");
        }

        #endregion

        #region Public Methods

        public async Task<bool> SendValuesAsync()
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(Values), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"events", content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception SendValuesAsync(): {ex.Message}.");
            }

            return false;
        }

        #endregion
    }
}
