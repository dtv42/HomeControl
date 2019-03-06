// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxValues - Copy.cs" company="DTV-Online">
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

    using HomeControlLib.Zipato.Models;

    #endregion

    internal class ZipatoValues : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipatoValues"/> class.
        /// </summary>
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public ZipatoValues(IInitialStateClient client,
                            ILogger<ZipatoValues> logger,
                            IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Zipato),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "Zipato Plug 1" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 2" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 3" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 4" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 5" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 6" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 7" });
            Values.Add(new InitialStateValue() { Key = "Zipato Plug 8" });
            Values.Add(new InitialStateValue() { Key = "Zipato Heavy Duty Switch" });
            Values.Add(new InitialStateValue() { Key = "Zipato Thermostat 1" });
            Values.Add(new InitialStateValue() { Key = "Zipato Thermostat 2" });
            Values.Add(new InitialStateValue() { Key = "Zipato Thermostat 3" });
            Values.Add(new InitialStateValue() { Key = "Zipato Thermostat 4" });
            Values.Add(new InitialStateValue() { Key = "Zipato Smoke Sensor" });
            Values.Add(new InitialStateValue() { Key = "Zipato Flood Sensor" });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the Zipato web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json = await _client.GetStringAsync("api/sensors");
                var result = JsonConvert.DeserializeObject<ZipatoSensors>(json);

                if (result.IsGood)
                {
                    Values[0].Value = result.ConsumptionMeters[0].CurrentConsumption.Value;  // Plug 1
                    Values[1].Value = result.ConsumptionMeters[1].CurrentConsumption.Value;  // Plug 2
                    Values[2].Value = result.ConsumptionMeters[2].CurrentConsumption.Value;  // Plug 3
                    Values[3].Value = result.ConsumptionMeters[3].CurrentConsumption.Value;  // Plug 4
                    Values[4].Value = result.ConsumptionMeters[4].CurrentConsumption.Value;  // Plug 5
                    Values[5].Value = result.ConsumptionMeters[5].CurrentConsumption.Value;  // Plug 6
                    Values[6].Value = result.ConsumptionMeters[6].CurrentConsumption.Value;  // Plug 7
                    Values[7].Value = result.ConsumptionMeters[7].CurrentConsumption.Value;  // Plug 8
                    Values[8].Value = result.ConsumptionMeters[8].CurrentConsumption.Value;  // Heavy Duty Switch
                    Values[9].Value = result.TemperatureSensors[5].Temperature.Value;        // Thermostat 1
                    Values[10].Value = result.TemperatureSensors[6].Temperature.Value;       // Thermostat 2
                    Values[11].Value = result.TemperatureSensors[7].Temperature.Value;       // Thermostat 3
                    Values[12].Value = result.TemperatureSensors[8].Temperature.Value;       // Thermostat 4
                    Values[13].Value = result.TemperatureSensors[2].Temperature.Value;       // Smoke Sensor
                    Values[14].Value = result.TemperatureSensors[4].Temperature.Value;       // Flood Sensor

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
