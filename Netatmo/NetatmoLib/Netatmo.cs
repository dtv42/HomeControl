// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Netatmo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;
    using NetatmoLib.Models;

    #endregion

    /// <summary>
    /// Class holding data from the Netatmo weatherstation and thermostat.
    /// The data properties are based on the online specification found at
    /// Netatmo connect (https://dev.netatmo.com).
    /// </summary>
    public class Netatmo : BaseClass, INetatmo
    {
        #region Private Data Members

        /// <summary>
        /// The instantiated HTTP client.
        /// </summary>
        private readonly INetatmoClient _client;

        /// <summary>
        /// The Netatmo settings used internally.
        /// </summary>
        private readonly ISettingsData _settings;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Station property holds all Netatmo weather station data values.
        /// </summary>
        public NetatmoData Station { get; }

        /// <summary>
        /// Gets or sets the Netatmo token data.
        /// </summary>
        [JsonIgnore]
        public TokenData Token { get; private set; } = new TokenData();

        /// <summary>
        /// Gets the Netatmo token expiration time.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset Expiration { get; private set; } = new DateTimeOffset(DateTime.UtcNow);

        /// <summary>
        /// Flag indicating that the first update has been sucessful.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets or sets the Netatmo web service base uri.
        /// </summary>
        [JsonIgnore]
        public string BaseAddress
        {
            get => _client.BaseAddress;
            set => _client.BaseAddress = value;
        }

        /// <summary>
        /// Gets or sets the timeout of the Http requests in seconds.
        /// </summary>
        [JsonIgnore]
        public int Timeout
        {
            get => _client.Timeout;
            set => _client.Timeout = value;
        }

        /// <summary>
        /// Gets or sets the number of retries sending a request.
        /// </summary>
        [JsonIgnore]
        public int Retries
        {
            get => _client.Retries;
            set => _client.Retries = value;
        }

        /// <summary>
        /// Gets or sets the Netatmo User.
        /// </summary>
        [JsonIgnore]
        public string User
        {
            get => _settings.User;
            set => _settings.User = value;
        }

        /// <summary>
        /// Gets or sets the Netatmo user password.
        /// </summary>
        [JsonIgnore]
        public string Password
        {
            get => _settings.Password;
            set => _settings.Password = value;
        }

        /// <summary>
        /// Gets or sets the Netatmo client ID.
        /// </summary>
        [JsonIgnore]
        public string ClientID
        {
            get => _settings.ClientID;
            set => _settings.ClientID = value;
        }

        /// <summary>
        /// Gets or sets the Netatmo client secret.
        /// </summary>
        [JsonIgnore]
        public string ClientSecret
        {
            get => _settings.ClientSecret;
            set => _settings.ClientSecret = value;
        }

        /// <summary>
        /// Gets or sets the Netatmo API scope.
        /// </summary>
        [JsonIgnore]
        public string Scope
        {
            get => _settings.Scope;
            set => _settings.Scope = value;
        }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Netatmo"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The HTTP client.</param>
        /// <param name="settings">The Netatmo settings.</param>
        public Netatmo(ILogger<Netatmo> logger, INetatmoClient client, ISettingsData settings)
            : base(logger)
        {
            _client = client;
            _settings = settings;

            Station = new NetatmoData();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Updates all Netatmo station and thermostat properties reading the data from the Netatmo web service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync()
        {
            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");

                if (string.IsNullOrEmpty(Token.RefreshToken))
                {
                    await GetAccessTokenAsync();
                }
                else if (DateTime.UtcNow > Expiration)
                {
                    await RefreshAccessTokenAsync();
                }

                if (!string.IsNullOrEmpty(Token.AccessToken))
                {
                    string json = await _client.GetStringAsync($"api/getstationsdata?access_token={Token.AccessToken}&get_favorites=true");

                    if (!string.IsNullOrEmpty(json))
                    {
                        RawStationData data = JsonConvert.DeserializeObject<RawStationData>(json);
                        Station.Refresh(data);
                        if (IsInitialized == false) IsInitialized = true;

                        _logger?.LogDebug($"ReadAllAsync OK: {json}");
                    }
                    else
                    {
                        _logger?.LogDebug($"ReadAllAsync not OK.");
                        Station.Status = DataValue.BadUnknownResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadAllAsync().");
                Station.Status = DataValue.BadUnexpectedError;
            }
            finally
            {
                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            return Station.Status;
        }

        #endregion Public Methods

        #region Public Helpers

        /// <summary>
        /// Check if a valid Access Token can be retrieved.
        /// </summary>
        /// <returns>True if a valid Access Token is available.</returns>
        public async Task<bool> CheckAccess()
        {
            await GetAccessTokenAsync();
            return (!string.IsNullOrEmpty(Token.AccessToken) && (Token.ExpiresIn > 0));
        }

        /// <summary>
        /// Method to determine if the property is supported. Switches to the proper section.
        /// Note this routine supports nested properties and simple arrays and generic List (IList).
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>True if the property is found.</returns>
        public static bool IsProperty(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new[] { '.' }, 2);

                switch (parts[0])
                {
                    case "Netatmo":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Netatmo), parts[1]) != null : true;
                    case "Station":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(NetatmoData), parts[1]) != null : true;
                    case "MainModule":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(StationDeviceDashboard), parts[1]) != null : true;
                    case "RainGauge":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(RainGaugeDashboard), parts[1]) != null : true;
                    case "WindGauge":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(WindGaugeDashboard), parts[1]) != null : true;
                    case "OutdoorModule":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(OutdoorModuleDashboard), parts[1]) != null : true;
                    case "IndoorModule1":
                    case "IndoorModule2":
                    case "IndoorModule3":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(IndoorModuleDashboard), parts[1]) != null : true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method to get the property value by name. Switches to the proper section.
        /// Note this routine supports nested properties and simple arrays and generic List (IList).
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new[] { '.' }, 2);

                switch (parts[0])
                {
                    case "Netatmo":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Station":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station, parts[1]) : Station;
                    case "MainModule":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.DashboardData, parts[1]) : Station.Device.DashboardData;
                    case "RainGauge":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.RainGauge.DashboardData, parts[1]) : Station.Device.RainGauge.DashboardData;
                    case "WindGauge":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.WindGauge.DashboardData, parts[1]) : Station.Device.WindGauge.DashboardData;
                    case "OutdoorModule":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.OutdoorModule.DashboardData, parts[1]) : Station.Device.OutdoorModule.DashboardData;
                    case "IndoorModule1":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.IndoorModule1.DashboardData, parts[1]) : Station.Device.IndoorModule1.DashboardData;
                    case "IndoorModule2":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.IndoorModule2.DashboardData, parts[1]) : Station.Device.IndoorModule2.DashboardData;
                    case "IndoorModule3":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Station.Device.IndoorModule3.DashboardData, parts[1]) : Station.Device.IndoorModule3.DashboardData;
                }
            }

            return null;
        }

        #endregion Public Helpers

        #region Private Methods

        /// <summary>
        /// Async method to retrieve a Netatmo access token. The expiration date is updated accordingly.
        /// </summary>
        private async Task GetAccessTokenAsync()
        {
            try
            {
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", ClientID),
                    new KeyValuePair<string, string>("client_secret", ClientSecret),
                    new KeyValuePair<string, string>("username", User),
                    new KeyValuePair<string, string>("password", Password),
                    new KeyValuePair<string, string>("scope", Scope)
                };
                HttpContent content = new FormUrlEncodedContent(postData);
                string json = await _client.PostAsync("oauth2/token", content);
                Token = JsonConvert.DeserializeObject<TokenData>(json);

                if (Token.ExpiresIn > 0)
                {
                    Expiration = DateTime.UtcNow.AddSeconds(Token.ExpiresIn);
                }

                _logger?.LogDebug($"GetAccessTokenAsync OK: {json}");
            }
            catch (Exception ex)
            {
                _logger?.LogError("GetAccessTokenAsync exception: {0}.", ex.Message);
            }
        }

        /// <summary>
        /// Async method to refresh a Netatmo access token. The expiration date is updated accordingly.
        /// </summary>
        /// <returns>A result data instance</returns>
        private async Task RefreshAccessTokenAsync()
        {
            try
            {
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", Token.RefreshToken),
                    new KeyValuePair<string, string>("client_id", ClientID),
                    new KeyValuePair<string, string>("client_secret", ClientSecret)
                };
                HttpContent content = new FormUrlEncodedContent(postData);
                string json = await _client.PostAsync("oauth2/token", content);
                Token = JsonConvert.DeserializeObject<TokenData>(json);

                if (Token.ExpiresIn > 0)
                {
                    Expiration = DateTime.UtcNow.AddSeconds(Token.ExpiresIn);
                }

                _logger?.LogDebug($"RefreshAccessTokenAsync OK: {json}");
            }
            catch (Exception ex)
            {
                _logger?.LogError("RefreshAccessTokenAsync exception: {0}.", ex.Message);
            }
        }

        #endregion Private Methods
    }
}
