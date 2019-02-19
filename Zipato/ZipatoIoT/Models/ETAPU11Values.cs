// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11Values.cs" company="DTV-Online">
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

    using ETAPU11Lib.Models;

    #endregion

    internal class ETAPU11Values : MeterValues
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client of the web services providing the data values.
        /// </summary>
        private HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ETAPU11Values"/> class.
        /// </summary>
        /// <param name="client">The Zipato client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public ETAPU11Values(IZipatoClient client,
                             ILogger<ETAPU11Values> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.ETAPU11),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Index = _settings.Meters.ETAPU11;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Getting data values from the ETAPU11 web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateValuesAsync()
        {
            try
            {
                string json = await _client.GetStringAsync("api/etapu11/all");
                var result = JsonConvert.DeserializeObject<ETAPU11Data>(json);

                if (result.IsGood)
                {
                    Values[0] = result.BoilerPressure;                                // Value1:  Boiler Pressure
                    Values[1] = result.BoilerTemperature;                             // Value2:  Boiler Temperature
                    Values[2] = result.HotwaterTemperature;                           // Value3:  Hotwater Temperature
                    Values[3] = result.HeatingTemperature;                            // Value4:  Heating Temperature
                    Values[4] = result.RoomTemperature;                               // Value5:  Room Temperature
                    Values[5] = result.BoilerTarget;                                  // Value6:  Boiler Target
                    Values[6] = result.BoilerBottom;                                  // Value7:  Boiler Bottom
                    Values[7] = result.FlueGasTemperature;                            // Value8:  Fluegas Temperature
                    Values[8] = result.DraughtFanSpeed;                               // Value9:  Draught Fan Speed
                    Values[9] = result.ResidualO2;                                    // Value10: Residual O2
                    Values[10] = result.RoomTarget;                                   // Value11: Room Target
                    Values[11] = result.Flow;                                         // Value12: Flow Temperature
                    Values[12] = result.HopperPelletBinContents / 10;                 // Value13: Bin Content
                    Values[13] = result.Stock;                                        // Value14: Stock Content
                    Values[14] = result.HotwaterTarget;                               // Value15: Hotwater Target
                    Values[15] = result.OutsideTemperature;                           // Value16: Outside Temperature

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
