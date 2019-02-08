// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeDataValues.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;
    using Serilog;

    using HomeDataLib.Models;

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
        /// <param name="client">The Zipato client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public HomeDataValues(IZipatoClient client,
                              ILogger<HomeDataValues> logger,
                              IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.HomeData),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Index = _settings.Meters.HomeData;
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
                    Values[0] = result.Load;                                          // Value1:  Load
                    Values[1] = result.Demand;                                        // Value2:  Demand
                    Values[2] = result.Generation;                                    // Value3:  Generation
                    Values[3] = result.Surplus;                                       // Value4:  Surplus
                    Values[4] = result.LoadL1;                                        // Value5:  Load L1
                    Values[5] = result.DemandL1;                                      // Value6:  Demand L1
                    Values[6] = result.GenerationL1;                                  // Value7:  Generation L1
                    Values[7] = result.SurplusL1;                                     // Value8:  Surplus L1
                    Values[8] = result.LoadL2;                                        // Value9:  Load L2
                    Values[9] = result.DemandL2;                                      // Value10: Demand L2
                    Values[10] = result.GenerationL2;                                 // Value11: Generation L2
                    Values[11] = result.SurplusL2;                                    // Value12: Surplus L2
                    Values[12] = result.LoadL3;                                       // Value13: Load L3
                    Values[13] = result.DemandL3;                                     // Value14: Demand L3
                    Values[14] = result.GenerationL3;                                 // Value15: Generation L3
                    Values[15] = result.SurplusL3;                                    // Value16: Surplus L3

                    return await SendValuesAsync();
                }

                Log.Warning($"GetValues(): Error {result.Status.Explanation}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Exception in GetValues().", ex.Message);
            }

            return false;
        }

        #endregion
    }
}
