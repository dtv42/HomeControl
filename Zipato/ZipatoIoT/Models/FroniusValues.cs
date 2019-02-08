// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusValues.cs" company="DTV-Online">
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

    using FroniusLib.Models;

    #endregion

    internal class FroniusValues : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FroniusValues"/> class.
        /// </summary>
        /// <param name="client">The Zipato client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public FroniusValues(IZipatoClient client,
                             ILogger<FroniusValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Fronius),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Index = _settings.Meters.Fronius;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the Fronius web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json1 = await _client.GetStringAsync("api/fronius/common");
                var result1 = JsonConvert.DeserializeObject<CommonData>(json1);

                if (result1.IsGood)
                {
                    Values[0] = result1.VoltageDC;                                  // Value1:  Voltage DC
                    Values[1] = result1.CurrentDC;                                  // Value2:  Current DC
                    Values[2] = result1.VoltageAC;                                  // Value3:  Voltage AC
                    Values[3] = result1.CurrentAC;                                  // Value4:  Current AC
                    Values[4] = result1.PowerAC;                                    // Value5:  Power AC
                    Values[5] = result1.DailyEnergy;                                // Value6:  Daily Energy
                    Values[6] = result1.YearlyEnergy;                               // Value7:  Yearly Energy
                                                                                    // Value8:  Current L1
                                                                                    // Value9:  Current L2
                                                                                    // Value10: Current L3
                                                                                    // Value11: Voltage L1N
                                                                                    // Value12: Voltage L2N
                                                                                    // Value13: Voltage L3N
                                                                                    // Value14: value14
                                                                                    // Value15: value15
                                                                                    // Value16: value16

                    var json2 = await _client.GetStringAsync("api/fronius/phase");
                    var result2 = JsonConvert.DeserializeObject<PhaseData>(json2);

                    if (result2.IsGood)
                    {
                                                                                    // Value1:  Voltage DC
                                                                                    // Value2:  Current DC
                                                                                    // Value3:  Voltage AC
                                                                                    // Value4:  Current AC
                                                                                    // Value5:  Power AC
                                                                                    // Value6:  Daily Energy
                                                                                    // Value7:  Yearly Energy
                        Values[7] = result2.CurrentL1;                              // Value8:  Current L1
                        Values[8] = result2.CurrentL2;                              // Value9:  Current L2
                        Values[9] = result2.CurrentL3;                              // Value10: Current L3
                        Values[10] = result2.VoltageL1N;                            // Value11: Voltage L1N
                        Values[11] = result2.VoltageL2N;                            // Value12: Voltage L2N
                        Values[12] = result2.VoltageL3N;                            // Value13: Voltage L3N
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
