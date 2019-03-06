// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11Values.cs" company="DTV-Online">
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

    using HomeControlLib.ETAPU11.Models;

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
        /// <param name="client">The Initial State client instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public ETAPU11Values(IInitialStateClient client,
                             ILogger<ETAPU11Values> logger,
                             IOptions<AppSettings> options) : base(client, logger, options)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_settings.Servers.ETAPU11),
                Timeout = TimeSpan.FromSeconds(_settings.Timeout)
            };

            Values.Add(new InitialStateValue() { Key = "ETAPU11 Boiler Pressur" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Boiler Temperature" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Hotwater Temperature" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Heating Temperature" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Room Temperature" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Boiler Target" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Boiler Bottom" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Fluegas Temperature" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Draught Fan Speed" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Residual O2" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Room Target" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Flow Temperature" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Bin Content" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Stock Content" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Hotwater Target" });
            Values.Add(new InitialStateValue() { Key = "ETAPU11 Outside Temperature" });
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
                    Values[0].Value = result.BoilerPressure;                                // Boiler Pressure
                    Values[1].Value = result.BoilerTemperature;                             // Boiler Temperature
                    Values[2].Value = result.HotwaterTemperature;                           // Hotwater Temperature
                    Values[3].Value = result.HeatingTemperature;                            // Heating Temperature
                    Values[4].Value = result.RoomTemperature;                               // Room Temperature
                    Values[5].Value = result.BoilerTarget;                                  // Boiler Target
                    Values[6].Value = result.BoilerBottom;                                  // Boiler Bottom
                    Values[7].Value = result.FlueGasTemperature;                            // Fluegas Temperature
                    Values[8].Value = result.DraughtFanSpeed;                               // Draught Fan Speed
                    Values[9].Value = result.ResidualO2;                                    // Residual O2
                    Values[10].Value = result.RoomTarget;                                   // Room Target
                    Values[11].Value = result.Flow;                                         // Flow Temperature
                    Values[12].Value = result.HopperPelletBinContents / 10;                 // Bin Content
                    Values[13].Value = result.Stock;                                        // Stock Content
                    Values[14].Value = result.HotwaterTarget;                               // Hotwater Target
                    Values[15].Value = result.OutsideTemperature;                           // Outside Temperature

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
