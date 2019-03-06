// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRValues.cs" company="DTV-Online">
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

    using HomeControlLib.EM300LR.Models;

    #endregion

    internal class EM300LRValues : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EM300LRValues"/> class.
        /// </summary>
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public EM300LRValues(IInitialStateClient client,
                             ILogger<EM300LRValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.EM300LR),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "EM300LR Frequency" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power+" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power+ L1" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power+ L2" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power+ L3" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power-" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power- L1" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power- L2" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power- L3" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Current L1" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Current L2" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Current L3" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Voltage L1" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Voltage L2" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Voltage L3" });
            Values.Add(new InitialStateValue() { Key = "EM300LR Power Factor" });
        }
                  
        #endregion
                              
        #region Public Methods

        /// <summary>
        /// Getting data values from the EM300LR web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json1 = await _client.GetStringAsync("api/em300lr/total");
                var result1 = JsonConvert.DeserializeObject<TotalData>(json1);

                if (result1.IsGood)
                {
                    Values[0].Value = result1.SupplyFrequency;                         // Frequency
                    Values[1].Value = result1.ActivePowerPlus / 1000.0;                // Power+
                                                                                       // Power+ L1
                                                                                       // Power+ L2
                                                                                       // Power+ L3
                    Values[5].Value = result1.ActivePowerMinus / 1000.0;               // Power-
                                                                                       // Power- L1
                                                                                       // Power- L2
                                                                                       // Power- L3
                                                                                       // Current L1
                                                                                       // Current L2
                                                                                       // Current L3
                                                                                       // Voltage L1
                                                                                       // Voltage L2
                                                                                       // Voltage L3
                    Values[15].Value = result1.PowerFactor;                            // Power Factor

                    string json2 = await _client.GetStringAsync("api/em300lr/phase1");
                    var result2 = JsonConvert.DeserializeObject<Phase1Data>(json2);

                    if (result2.IsGood)
                    {
                                                                                       // Frequency
                                                                                       // Power+
                        Values[2].Value = result2.ActivePowerPlus / 1000.0;            // Power+ L1
                                                                                       // Power+ L2
                                                                                       // Power+ L3
                                                                                       // Power-
                        Values[6].Value = result2.ActivePowerMinus / 1000.0;           // Power- L1
                                                                                       // Power- L2
                                                                                       // Power- L3
                        Values[9].Value = result2.Current;                             // Current L1
                                                                                       // Current L2
                                                                                       // Current L3
                        Values[12].Value = result2.Voltage;                            // Voltage L1
                                                                                       // Voltage L2
                                                                                       // Voltage L3
                                                                                       // Power Factor

                        string json3 = await _client.GetStringAsync("api/em300lr/phase2");
                        var result3 = JsonConvert.DeserializeObject<Phase2Data>(json3);

                        if (result1.IsGood)
                        {
                                                                                       // Value1:   Frequency
                                                                                       // Value2:   Power+
                                                                                       // Value3:   Power+ L1
                            Values[3].Value = result3.ActivePowerPlus / 1000.0;        // Value4:   Power+ L2
                                                                                       // Value5:   Power+ L3
                                                                                       // Value6:   Power-
                                                                                       // Value7:   Power- L1
                            Values[7].Value = result3.ActivePowerMinus / 1000.0;       // Value8:   Power- L2
                                                                                       // Value9:   Power- L3
                                                                                       // Value10:  Current L1
                            Values[10].Value = result3.Current;                        // Value11:  Current L2
                                                                                       // Value12:  Current L3
                                                                                       // Value13:  Voltage L1
                            Values[13].Value = result3.Voltage;                        // Value14:  Voltage L2
                                                                                       // Value15:  Voltage L3
                                                                                       // Value16:  Power Factor

                            string json4 = await _client.GetStringAsync("api/em300lr/phase3");
                            var result4 = JsonConvert.DeserializeObject<Phase3Data>(json4);

                            if (result4.IsGood)
                            {
                                                                                       // Value1:   Frequency
                                                                                       // Value2:   Power+
                                                                                       // Value3:   Power+ L1
                                                                                       // Value4:   Power+ L2
                                Values[4].Value = result4.ActivePowerPlus / 1000.0;    // Value5:   Power+ L3
                                                                                       // Value6:   Power-
                                                                                       // Value7:   Power- L1
                                                                                       // Value8:   Power- L2
                                Values[8].Value = result4.ActivePowerMinus / 1000.0;   // Value9:   Power- L3
                                                                                       // Value10:  Current L1
                                                                                       // Value11:  Current L2
                                Values[11].Value = result4.Current;                    // Value12:  Current L3
                                                                                       // Value13:  Voltage L1
                                                                                       // Value14:  Voltage L2
                                Values[14].Value = result4.Voltage;                    // Value15:  Voltage L3
                                                                                       // Value16:  Power Factor

                                return await SendValuesAsync();
                            }

                            Log.Warning($"UpdateValuesAsync(): Error {result4.Status.Explanation}.");
                        }

                        Log.Warning($"UpdateValuesAsync(): Error {result3.Status.Explanation}.");
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
