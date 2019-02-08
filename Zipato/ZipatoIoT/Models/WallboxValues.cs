// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxValues.cs" company="DTV-Online">
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

    using WallboxLib.Models;

    #endregion

    internal class WallboxValues : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WallboxValues"/> class.
        /// </summary>
        /// <param name="client">The Zipato client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public WallboxValues(IZipatoClient client,
                             ILogger<WallboxValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Wallbox),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Index = _settings.Meters.Wallbox;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the Wallbox web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json1 = await _client.GetStringAsync("api/wallbox/report2");
                var result1 = JsonConvert.DeserializeObject<Report2Data>(json1);

                if (result1.IsGood)
                {
                    Values[0] = (int)result1.State;                                 // Value1:  Charging State
                                                                                    // Value2:  Power
                                                                                    // Value3:  Current L1
                                                                                    // Value4:  Current L2
                                                                                    // Value5:  Current L3
                                                                                    // Value6:  Voltage L1
                                                                                    // Value7:  Voltage L2
                                                                                    // Value8:  Voltage L3
                                                                                    // Value9:  Power Factor
                                                                                    // Value10: Present Energy
                                                                                    // Value11: Total Energy
                                                                                    // Value12: value12
                                                                                    // Value13: value13
                                                                                    // Value14: value14
                                                                                    // Value15: value15
                                                                                    // Value16: value16

                    string json2 = await _client.GetStringAsync("api/wallbox/report3");
                    var result2 = JsonConvert.DeserializeObject<Report3Data>(json2);

                    if (result2.IsGood)
                    {
                        // Value1:  Charging State
                        Values[1] = result2.Power;                                  // Value2:  Power
                        Values[2] = result2.CurrentL1;                              // Value3:  Current L1
                        Values[3] = result2.CurrentL2;                              // Value4:  Current L2
                        Values[4] = result2.CurrentL3;                              // Value5:  Current L3
                        Values[5] = result2.VoltageL1N;                             // Value6:  Voltage L1
                        Values[6] = result2.VoltageL2N;                             // Value7:  Voltage L2
                        Values[7] = result2.VoltageL3N;                             // Value8:  Voltage L3
                        Values[8] = result2.PowerFactor;                            // Value9:  Power Factor
                        Values[9] = result2.EnergyCharging;                         // Value10: Present Energy
                        Values[10] = result2.EnergyTotal;                           // Value11: Total Energy
                                                                                    // Value12: value12
                                                                                    // Value13: value13
                                                                                    // Value14: value14
                                                                                    // Value15: value15
                                                                                    // Value16: value16

                        return await SendValuesAsync();
                    }

                    Log.Warning($"GetValues(): Error {result2.Status.Explanation}.");
                }

                Log.Warning($"GetValues(): Error {result1.Status.Explanation}.");
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
