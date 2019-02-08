// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeterValues.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoIoT.Models
{
    #region Using Directives

    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using BaseClassLib;

    #endregion

    internal class Meter2Values : BaseClass
    {
        #region Private Data Members

        /// <summary>
        /// The zipato HTTP client.
        /// </summary>
        private readonly IZipatoClient _client;

        #endregion

        #region Protected Data Members

        /// <summary>
        /// The settings instance.
        /// </summary>
        protected readonly AppSettings _settings;

        #endregion Protected Data Members

        #region Public Properties

        /// <summary>
        /// The meter index (Zipato).
        /// </summary>
        public int Index1 { get; set; }

        /// <summary>
        /// The meter index (Zipato).
        /// </summary>
        public int Index2 { get; set; }

        /// <summary>
        /// Set of data values written to Zipato virtual meters.
        /// </summary>
        public double?[] Values1 { get; set; } = new double?[16] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
        public double?[] Values2 { get; set; } = new double?[16] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public Meter2Values(IZipatoClient client,
                            ILogger<Meter2Values> logger,
                            IOptions<AppSettings> options) : base(logger)
        {
            _client = client;
            _settings = options.Value;
            _logger?.LogDebug("Meter2Values()");
        }

        #endregion

        #region Public Methods

        public async Task<bool> SendValuesAsync()
        {
            try
            {
                var content1 = new StringContent(JsonConvert.SerializeObject(Values1), Encoding.UTF8, "application/json");
                var response1 = await _client.PutAsync($"api/sensors/virtual/{Index1}", content1);
                response1.EnsureSuccessStatusCode();

                var content2 = new StringContent(JsonConvert.SerializeObject(Values2), Encoding.UTF8, "application/json");
                var response2 = await _client.PutAsync($"api/sensors/virtual/{Index2}", content2);
                response2.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception SetValues(): {ex.Message}.");
            }

            return false;
        }

        #endregion
    }
}
