// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Values.cs" company="DTV-Online">
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

    using KWLEC200Lib.Models;

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
        /// <param name="client">The Zipato client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public KWLEC200Values(IZipatoClient client,
                              ILogger<KWLEC200Values> logger,
                              IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.KWLEC200),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Index = _settings.Meters.KWLEC200;
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
                    Values[0] = result.TemperatureOutdoor;                          // Value1:  Temperature Outdoor
                    Values[1] = result.TemperatureExhaust;                          // Value2:  Temperature Exhaust
                    Values[2] = result.TemperatureExtract;                          // Value3:  Temperature Extract
                    Values[3] = result.TemperatureSupply;                           // Value4:  Temperature Supply
                    Values[4] = (double)result.VentilationLevel;                    // Value5:  Ventilation Level
                    Values[5] = result.SupplyFanSpeed;                              // Value6:  Supply Fan Speed
                    Values[6] = result.ExhaustFanSpeed;                             // Value7:  Exhaust Fan Speed
                    Values[7] = result.TemperatureChannel;                          // Value8:  TemperatureChannel
                                                                                    // Value9:  value9  (not used)
                                                                                    // Value10: value10 (not used)
                                                                                    // Value11: value11 (not used)
                                                                                    // Value12: value12 (not used)
                                                                                    // Value13: value13 (not used)
                                                                                    // Value14: value14 (not used)
                                                                                    // Value15: value15 (not used)
                                                                                    // Value16: value16 (not used)

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
