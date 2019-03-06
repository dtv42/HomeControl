// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxValues.cs" company="DTV-Online">
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

    using HomeControlLib.Wallbox.Models;

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
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public WallboxValues(IInitialStateClient client,
                             ILogger<WallboxValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Wallbox),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "Wallbox Charging State" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Power" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Current L1" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Current L2" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Current L3" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Voltage L1" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Voltage L2" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Voltage L3" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Power Factor" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Present Energy" });
            Values.Add(new InitialStateValue() { Key = "Wallbox Total Energy" });
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
                    Values[0].Value = (int)result1.State;                           // Charging State
                                                                                    // Power
                                                                                    // Current L1
                                                                                    // Current L2
                                                                                    // Current L3
                                                                                    // Voltage L1
                                                                                    // Voltage L2
                                                                                    // Voltage L3
                                                                                    // Power Factor
                                                                                    // Present Energy
                                                                                    // Total Energy

                    string json2 = await _client.GetStringAsync("api/wallbox/report3");
                    var result2 = JsonConvert.DeserializeObject<Report3Data>(json2);

                    if (result2.IsGood)
                    {
                                                                                    // Charging State
                        Values[1].Value = result2.Power;                            // Power
                        Values[2].Value = result2.CurrentL1;                        // Current L1
                        Values[3].Value = result2.CurrentL2;                        // Current L2
                        Values[4].Value = result2.CurrentL3;                        // Current L3
                        Values[5].Value = result2.VoltageL1N;                       // Voltage L1
                        Values[6].Value = result2.VoltageL2N;                       // Voltage L2
                        Values[7].Value = result2.VoltageL3N;                       // Voltage L3
                        Values[8].Value = result2.PowerFactor;                      // Power Factor
                        Values[9].Value = result2.EnergyCharging;                   // Present Energy
                        Values[10].Value = result2.EnergyTotal;                     // Total Energy

                        return await SendValuesAsync();
                    }

                    Log.Warning($"UpdateValuesAsync(): Error {result2.Status.Explanation}.");
                }

                Log.Warning($"UpdateValuesAsync(): Error {result1.Status.Explanation}.");
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
