// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Values.cs" company="DTV-Online">
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
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;
    using Serilog;

    using HomeControlLib.KWLEC200.Models;

    #endregion

    internal class KWLEC200Values : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KWLEC200Values"/> class.
        /// </summary>
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public KWLEC200Values(IInitialStateClient client,
                              ILogger<KWLEC200Values> logger,
                              IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.KWLEC200),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "KWLEC200 Temperature Outdoor" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 Temperature Exhaust" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 Temperature Extract" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 Temperature Supply" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 Ventilation Level" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 Supply Fan Speed" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 Exhaust Fan Speed" });
            Values.Add(new InitialStateValue() { Key = "KWLEC200 TemperatureChannel" });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the KWLEC200 web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json = await _client.GetStringAsync("api/kwlec200/all");
                var result = JsonConvert.DeserializeObject<OverviewData>(json);

                if (result.IsGood)
                {
                    Values[0].Value = result.TemperatureOutdoor;                    // Temperature Outdoor
                    Values[1].Value = result.TemperatureExhaust;                    // Temperature Exhaust
                    Values[2].Value = result.TemperatureExtract;                    // Temperature Extract
                    Values[3].Value = result.TemperatureSupply;                     // Temperature Supply
                    Values[4].Value = (double)result.VentilationLevel;              // Ventilation Level
                    Values[5].Value = result.SupplyFanSpeed;                        // Supply Fan Speed
                    Values[6].Value = result.ExhaustFanSpeed;                       // Exhaust Fan Speed
                    Values[7].Value = result.TemperatureChannel;                    // TemperatureChannel

                    return await SendValuesAsync();
                }

                Log.Warning($"UpdateValuesAsync(): Error {result.Status.Explanation}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Exception in UpdateValuesAsync().", ex.Message);
            }

            return false;
        }

        #endregion
    }
}
