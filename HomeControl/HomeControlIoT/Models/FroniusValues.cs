// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusValues.cs" company="DTV-Online">
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

    using HomeControlLib.Fronius.Models;

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
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public FroniusValues(IInitialStateClient client,
                             ILogger<FroniusValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Fronius),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "Fronius Voltage DC" });
            Values.Add(new InitialStateValue() { Key = "Fronius Current DC" });
            Values.Add(new InitialStateValue() { Key = "Fronius Voltage AC" });
            Values.Add(new InitialStateValue() { Key = "Fronius Current AC" });
            Values.Add(new InitialStateValue() { Key = "Fronius Power AC" });
            Values.Add(new InitialStateValue() { Key = "Fronius Daily Energy" });
            Values.Add(new InitialStateValue() { Key = "Fronius Yearly Energy" });
            Values.Add(new InitialStateValue() { Key = "Fronius Current L1" });
            Values.Add(new InitialStateValue() { Key = "Fronius Current L2" });
            Values.Add(new InitialStateValue() { Key = "Fronius Current L3" });
            Values.Add(new InitialStateValue() { Key = "Fronius Voltage L1N" });
            Values.Add(new InitialStateValue() { Key = "Fronius Voltage L2N" });
            Values.Add(new InitialStateValue() { Key = "Fronius Voltage L3N" });
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
                    Values[0].Value = result1.VoltageDC;                            // Voltage DC
                    Values[1].Value = result1.CurrentDC;                            // Current DC
                    Values[2].Value = result1.VoltageAC;                            // Voltage AC
                    Values[3].Value = result1.CurrentAC;                            // Current AC
                    Values[4].Value = result1.PowerAC;                              // Power AC
                    Values[5].Value = result1.DailyEnergy;                          // Daily Energy
                    Values[6].Value = result1.YearlyEnergy;                         // Yearly Energy
                                                                                    // Current L1
                                                                                    // Current L2
                                                                                    // Current L3
                                                                                    // Voltage L1N
                                                                                    // Voltage L2N
                                                                                    // Voltage L3N

                    var json2 = await _client.GetStringAsync("api/fronius/phase");
                    var result2 = JsonConvert.DeserializeObject<PhaseData>(json2);

                    if (result2.IsGood)
                    {
                                                                                    // Voltage DC
                                                                                    // Current DC
                                                                                    // Voltage AC
                                                                                    // Current AC
                                                                                    // Power AC
                                                                                    // Daily Energy
                                                                                    // Yearly Energy
                        Values[7].Value = result2.CurrentL1;                        // Current L1
                        Values[8].Value = result2.CurrentL2;                        // Current L2
                        Values[9].Value = result2.CurrentL3;                        // Current L3
                        Values[10].Value = result2.VoltageL1N;                      // Voltage L1N
                        Values[11].Value = result2.VoltageL2N;                      // Voltage L2N
                        Values[12].Value = result2.VoltageL3N;                      // Voltage L3N

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
