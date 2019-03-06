// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeDataValues.cs" company="DTV-Online">
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

    using HomeControlLib.HomeData.Models;

    #endregion

    internal class HomeDataValues : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeDataValues"/> class.
        /// </summary>
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public HomeDataValues(IInitialStateClient client,
                              ILogger<HomeDataValues> logger,
                              IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.HomeData),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "HomeData Load" });
            Values.Add(new InitialStateValue() { Key = "HomeData Demand" });
            Values.Add(new InitialStateValue() { Key = "HomeData Generation" });
            Values.Add(new InitialStateValue() { Key = "HomeData Surplus" });
            Values.Add(new InitialStateValue() { Key = "HomeData Load L1" });
            Values.Add(new InitialStateValue() { Key = "HomeData Demand L1" });
            Values.Add(new InitialStateValue() { Key = "HomeData Generation L1" });
            Values.Add(new InitialStateValue() { Key = "HomeData Surplus L1" });
            Values.Add(new InitialStateValue() { Key = "HomeData Load L2" });
            Values.Add(new InitialStateValue() { Key = "HomeData Demand L2" });
            Values.Add(new InitialStateValue() { Key = "HomeData Generation L2" });
            Values.Add(new InitialStateValue() { Key = "HomeData Surplus L2" });
            Values.Add(new InitialStateValue() { Key = "HomeData Load L3" });
            Values.Add(new InitialStateValue() { Key = "HomeData Demand L3" });
            Values.Add(new InitialStateValue() { Key = "HomeData Generation L3" });
            Values.Add(new InitialStateValue() { Key = "HomeData Surplus L3" });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the HomeData web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json = await _client.GetStringAsync("api/homedata/all");
                var result = JsonConvert.DeserializeObject<HomeValues>(json);

                if (result.IsGood)
                {
                    Values[0].Value = result.Load;                                    // Load
                    Values[1].Value = result.Demand;                                  // Demand
                    Values[2].Value = result.Generation;                              // Generation
                    Values[3].Value = result.Surplus;                                 // Surplus
                    Values[4].Value = result.LoadL1;                                  // Load L1
                    Values[5].Value = result.DemandL1;                                // Demand L1
                    Values[6].Value = result.GenerationL1;                            // Generation L1
                    Values[7].Value = result.SurplusL1;                               // Surplus L1
                    Values[8].Value = result.LoadL2;                                  // Load L2
                    Values[9].Value = result.DemandL2;                                // Demand L2
                    Values[10].Value = result.GenerationL2;                           // Generation L2
                    Values[11].Value = result.SurplusL2;                              // Surplus L2
                    Values[12].Value = result.LoadL3;                                 // Load L3
                    Values[13].Value = result.DemandL3;                               // Demand L3
                    Values[14].Value = result.GenerationL3;                           // Generation L3
                    Values[15].Value = result.SurplusL3;                              // Surplus L3

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
