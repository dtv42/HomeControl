// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OutdoorValues.cs" company="DTV-Online">
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

    using NetatmoLib.Models;

    #endregion

    internal class NetatmoValues : Meter2Values, IMeter2Values
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
        /// <param name="client">The Zipato client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public NetatmoValues(IZipatoClient client,
                             ILogger<NetatmoValues> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.Netatmo),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Index1 = _settings.Meters.Outdoor;
            Index2 = _settings.Meters.Indoor;
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
                    Values1[0] = result.Device.OutdoorModule.DashboardData.Temperature;    // Value1:  Temperature
                    Values1[1] = result.Device.DashboardData.Pressure;                     // Value2:  Pressure
                    Values1[2] = result.Device.OutdoorModule.DashboardData.Humidity;       // Value3:  Humidity
                    Values1[3] = result.Device.OutdoorModule.DashboardData.MinTemp;        // Value4:  Min. Temperature
                    Values1[4] = result.Device.OutdoorModule.DashboardData.MaxTemp;        // Value5:  Max. Temperature
                    Values1[5] = result.Device.RainGauge.DashboardData.Rain;               // Value6:  Rain
                    Values1[6] = result.Device.RainGauge.DashboardData.SumRain1;           // Value7:  Sum Rain 1h
                    Values1[7] = result.Device.RainGauge.DashboardData.SumRain24;          // Value8:  Sum Rain 24h
                    Values1[8] = result.Device.WindGauge.DashboardData.WindStrength;       // Value9:  Wind Strength
                    Values1[9] = result.Device.WindGauge.DashboardData.WindAngle;          // Value10: Wind Angle
                    Values1[10] = result.Device.WindGauge.DashboardData.GustStrength;      // Value11: Gust Strength
                    Values1[11] = result.Device.WindGauge.DashboardData.GustAngle;         // Value12: Gust Angle
                    Values1[12] = result.Device.WindGauge.DashboardData.MaxWindStrength;   // Value13: Max. Wind Strength
                    Values1[13] = result.Device.WindGauge.DashboardData.MaxWindAngle;      // Value14: Max. Wind Angle
                                                                                           // Value15: value15 (not used)
                                                                                           // Value16: value16 (not used)

                    Values2[0] = result.Device.DashboardData.Temperature;                  // Value1:  Temperature Main
                    Values2[1] = result.Device.DashboardData.Humidity;                     // Value2:  Humidity Main
                    Values2[2] = result.Device.DashboardData.Noise;                        // Value3:  Noise
                    Values2[3] = result.Device.DashboardData.CO2;                          // Value4:  CO2 Main
                    Values2[4] = result.Device.IndoorModule1.DashboardData.Temperature;    // Value5:  Temperature Module 1
                    Values2[5] = result.Device.IndoorModule1.DashboardData.Humidity;       // Value6:  Humidity Module 1
                    Values2[6] = result.Device.IndoorModule1.DashboardData.CO2;            // Value7:  CO2 Module 1
                    Values2[7] = result.Device.IndoorModule2.DashboardData.Temperature;    // Value8:  Temperature Module 2
                    Values2[8] = result.Device.IndoorModule2.DashboardData.Humidity;       // Value9:  Humidity Module 2
                    Values2[9] = result.Device.IndoorModule2.DashboardData.CO2;            // Value10: CO2 Module 2
                    Values2[10] = result.Device.IndoorModule3.DashboardData.Temperature;   // Value11: Temperature Module 3
                    Values2[11] = result.Device.IndoorModule3.DashboardData.Humidity;      // Value12: Humidity Module 3
                    Values2[12] = result.Device.IndoorModule3.DashboardData.CO2;           // Value13: CO2 Module 3
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
