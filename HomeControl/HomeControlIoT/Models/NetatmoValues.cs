// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoValues.cs" company="DTV-Online">
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

    using HomeControlLib.Netatmo.Models;

    #endregion

    internal class NetatmoValues : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetatmoValues"/> class.
        /// </summary>
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public NetatmoValues(IInitialStateClient client,
                             ILogger<NetatmoValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Netatmo),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "Netatmo Temperature" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Pressure" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Humidity" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Min. Temperature" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Max. Temperature" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Rain" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Sum Rain 1h" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Sum Rain 24h" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Wind Strength" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Wind Angle" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Gust Strength" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Gust Angle" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Max. Wind Strength" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Max. Wind Angle" });

            Values.Add(new InitialStateValue() { Key = "Netatmo Temperature Main" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Humidity Main" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Noise" });
            Values.Add(new InitialStateValue() { Key = "Netatmo CO2 Main" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Temperature Module 1" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Humidity Module 1" });
            Values.Add(new InitialStateValue() { Key = "Netatmo CO2 Module 1" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Temperature Module 2" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Humidity Module 2" });
            Values.Add(new InitialStateValue() { Key = "Netatmo CO2 Module 2" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Temperature Module 3" });
            Values.Add(new InitialStateValue() { Key = "Netatmo Humidity Module 3" });
            Values.Add(new InitialStateValue() { Key = "Netatmo CO2 Module 3" });







        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the Netatmo web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json = await _client.GetStringAsync("api/netatmo/all");
                var result = JsonConvert.DeserializeObject<NetatmoData>(json);

                if (result.IsGood)
                {
                    Values[0].Value = result.Device.OutdoorModule.DashboardData.Temperature;    // Temperature
                    Values[1].Value = result.Device.DashboardData.Pressure;                     // Pressure
                    Values[2].Value = result.Device.OutdoorModule.DashboardData.Humidity;       // Humidity
                    Values[3].Value = result.Device.OutdoorModule.DashboardData.MinTemp;        // Min. Temperature
                    Values[4].Value = result.Device.OutdoorModule.DashboardData.MaxTemp;        // Max. Temperature
                    Values[5].Value = result.Device.RainGauge.DashboardData.Rain;               // Rain
                    Values[6].Value = result.Device.RainGauge.DashboardData.SumRain1;           // Sum Rain 1h
                    Values[7].Value = result.Device.RainGauge.DashboardData.SumRain24;          // Sum Rain 24h
                    Values[8].Value = result.Device.WindGauge.DashboardData.WindStrength;       // Wind Strength
                    Values[9].Value = result.Device.WindGauge.DashboardData.WindAngle;          // Wind Angle
                    Values[10].Value = result.Device.WindGauge.DashboardData.GustStrength;      // Gust Strength
                    Values[11].Value = result.Device.WindGauge.DashboardData.GustAngle;         // Gust Angle
                    Values[12].Value = result.Device.WindGauge.DashboardData.MaxWindStrength;   // Max. Wind Strength
                    Values[13].Value = result.Device.WindGauge.DashboardData.MaxWindAngle;      // Max. Wind Angle

                    Values[14].Value = result.Device.DashboardData.Temperature;                 // Temperature Main
                    Values[15].Value = result.Device.DashboardData.Humidity;                    // Humidity Main
                    Values[16].Value = result.Device.DashboardData.Noise;                       // Noise
                    Values[17].Value = result.Device.DashboardData.CO2;                         // CO2 Main
                    Values[18].Value = result.Device.IndoorModule1.DashboardData.Temperature;   // Temperature Module 1
                    Values[19].Value = result.Device.IndoorModule1.DashboardData.Humidity;      // Humidity Module 1
                    Values[20].Value = result.Device.IndoorModule1.DashboardData.CO2;           // CO2 Module 1
                    Values[21].Value = result.Device.IndoorModule2.DashboardData.Temperature;   // Temperature Module 2
                    Values[22].Value = result.Device.IndoorModule2.DashboardData.Humidity;      // Humidity Module 2
                    Values[23].Value = result.Device.IndoorModule2.DashboardData.CO2;           // CO2 Module 2
                    Values[24].Value = result.Device.IndoorModule3.DashboardData.Temperature;   // Temperature Module 3
                    Values[25].Value = result.Device.IndoorModule3.DashboardData.Humidity;      // Humidity Module 3
                    Values[26].Value = result.Device.IndoorModule3.DashboardData.CO2;           // CO2 Module 3

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
