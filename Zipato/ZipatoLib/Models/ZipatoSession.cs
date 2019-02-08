// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoSession.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;

    using ZipatoLib.Models.Data;

    #endregion

    /// <summary>
    /// Class holding session related data.
    /// </summary>
    public sealed class ZipatoSession
    {
        #region Private Fields

        /// <summary>
        /// The singleton ZipatoSession instance.
        /// </summary>
        private static readonly Lazy<ZipatoSession> lazy = new Lazy<ZipatoSession>(() => new ZipatoSession());

        /// <summary>
        /// The logger instance.
        /// </summary>
        private static ILogger<BaseClass> _logger;

        /// <summary>
        /// The HTTP client used during the session.
        /// </summary>
        private static IZipatoClient _client;

        /// <summary>
        /// The JSON converter settings.
        /// </summary>
        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        /// <summary>
        /// Zipato session data.
        /// </summary>
        private static SessionData _session = new SessionData();

        /// <summary>
        /// Session timer.
        /// </summary>
        private static Timer _timer = new Timer(OnSessionTimer);

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Property returning the session success state.
        /// </summary>
        public bool IsActive { get => _session.Success; }

        /// <summary>
        /// Property indicating that a session is active (logged in).
        /// </summary>
        public static bool IsSessionActive { get => _session.Success; }

        #endregion Public Properties

        #region Constructors

        private ZipatoSession()
        {
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Returns a (singleton) ZipatoSession and tries to establish a session with the Zipato web service (logging in).
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="client">The HTTP client.</param>
        /// <param name="settings">The Zipato settings.</param>
        /// <returns>The (singleton) ZipatoSession instance.</returns>
        public static ZipatoSession StartSession(ILogger<BaseClass> logger, IZipatoClient client, ISettingsData settings)
        {
            _client = client;
            _logger = logger;
            var session = lazy.Value;

            if (_session.Success)
            {
                _timer.Change(settings.SessionTimeout, Timeout.Infinite);
                return session;
            }

            try
            {
                LoginData login = session.InitUserAsync().Result;

                if (login.Success)
                {
                    Cookie cookie = new Cookie("JSESSIONID", login.JSessionId);
                    _client.Cookies.Add(new Uri(client.BaseAddress), cookie);
                    string token = session.SHA1(login.Nonce + session.SHA1(settings.Password));

                    _session = session.LoginUserAsync(token, settings.User).Result;

                    if ((_session != null) && _session.Success)
                    {
                        _logger?.LogDebug("Session established.");
                        _timer.Change(settings.SessionTimeout, Timeout.Infinite);
                        return session;
                    }
                    else
                    {
                        _logger?.LogDebug($"Session not established (login): {_session.Error}.");
                    }
                }
                else
                {
                    _logger?.LogDebug($"Session not established (init): {_session.Error}.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in Session().");
            }

            _session = new SessionData();
            return session;
        }

        /// <summary>
        /// Ends the established session (logging out).
        /// </summary>
        public static void EndSession()
        {
            var session = lazy.Value;

            if ((_client != null) && _session.Success)
            {
                if (session.LogoutUserAsync().Result)
                {
                    _logger?.LogDebug("Session ended.");
                }
                else
                {
                    _logger?.LogWarning("Session: could not end session.");
                }
            }

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _session = new SessionData();
        }

        /// <summary>
        ///         // This method is called by the timer delegate.
        /// </summary>
        /// <param name="stateInfo"></param>
        public static void OnSessionTimer(Object stateInfo)
        {
            _logger?.LogDebug("Session timeout.");
            EndSession();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<(string, DataStatus)> GetDataAsync(string query)
        {
            var (data, status) = await GetStringAsync(query);

            if (status.IsGood)
            {
                return (data, DataValue.Good);
            }
            else
            {
                _logger?.LogError($"No result in GetDataAsync('{query}').");
                return (string.Empty, status);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<(T, DataStatus)> GetDataAsync<T>(string query) where T : new()
        {
            var (json, status) = await GetStringAsync(query);

            try
            {
                if (status.IsGood)
                {
                    if (string.IsNullOrEmpty(json))
                    {
                        _logger?.LogDebug($"Empty result in GetDataAsync<{typeof(T)}>('{query}').");
                        return (new T(), status);
                    }

                    return (JsonConvert.DeserializeObject<T>(json, _settings), DataValue.Good);
                }

                _logger?.LogDebug($"No valid result in GetDataAsync<{typeof(T)}>('{query}').");
                return (new T(), status);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in GetDataAsync<{typeof(T)}>('{query}').");
                return (new T(), DataValue.BadUnknownResponse);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<(List<T>, DataStatus)> GetListDataAsync<T>(string query) where T : new()
        {
            var (json, status) = await GetStringAsync(query);

            try
            {
                if (status.IsGood)
                {
                    if (string.IsNullOrEmpty(json))
                    {
                        _logger?.LogDebug($"Empty result in GetDataAsync<{typeof(T)}>('{query}').");
                        return (new List<T> { }, status);
                    }

                    return (JsonConvert.DeserializeObject<List<T>>(json, _settings), DataValue.Good);
                }

                _logger?.LogError($"No valid result in GetListDataAsync<{typeof(T)}>('{query}').");
                return (new List<T> { }, status);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in GetListDataAsync<{typeof(T)}>('{query}').");
                return (new List<T> { }, DataValue.BadUnknownResponse);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<DataStatus> PutDataAsync<T>(string query, T data) where T : new()
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                return await PutStringAsync(query, json);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in PutDataAsync<{typeof(T)}>('{query}').");
                return DataValue.BadInternalError;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<DataStatus> PutDataAsync(string query, string data)
        {
            try
            {
                return await PutTextAsync(query, data);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in PutDataAsync('{query}', '{data}').");
                return DataValue.BadInternalError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<DataStatus> PostDataAsync<T>(string query, T data) where T : new()
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                return await PostStringAsync(query, json);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in PostDataAsync<{typeof(T)}>('{query}').");
                return DataValue.BadInternalError;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<DataStatus> PostDataAsync(string query, string data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                return await PostTextAsync(query, json);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in PostDataAsync('{query}').");
                return DataValue.BadInternalError;
            }
        }

        /// <summary>
        /// Deletes data from the Zipatile Web service.
        /// </summary>
        /// <param name="request">The Web service DELETE command string.</param>
        /// <returns>True if successful.</returns>
        public async Task<DataStatus> DeleteAsync(string request)
        {
            if (IsActive)
            {
                try
                {
                    _logger?.LogDebug($"DELETE Request: {request}");
                    HttpResponseMessage response = await _client.DeleteAsync(request);
                    DataStatus status = DataValue.Uncertain;

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.NoContent:
                            status = DataValue.Good;
                            break;
                        case HttpStatusCode.NotFound:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotFound;
                            break;
                        case HttpStatusCode.BadRequest:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotSupported;
                            break;
                        case HttpStatusCode.Forbidden:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadResourceUnavailable;
                            break;
                        default:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.Bad;
                            break;
                    }

                    return status;
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"Exception in DeleteAsync({request}).");
                    return DataValue.BadInternalError;
                }
            }

            return DataValue.BadNotConnected;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Async method to initiate a user session with the Zipato web service.
        /// </summary>
        /// <returns>The login data</returns>
        private async Task<LoginData> InitUserAsync()
        {
            try
            {
                string request = $"user/init";
                _logger?.LogDebug($"Init user request: {_client.BaseAddress + request}");
                HttpResponseMessage response = await _client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                _logger?.LogDebug($"Init user OK.");
                return JsonConvert.DeserializeObject<LoginData>(json, _settings);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in InitUserAsync().");
            }

            return new LoginData();
        }

        /// <summary>
        /// Async method to login into the Zipato web service.
        /// </summary>
        /// <param name="token">The access token</param>
        /// <param name="user">The Zipato user name.</param>
        /// <returns>The session data</returns>
        private async Task<SessionData> LoginUserAsync(string token, string user)
        {
            try
            {
                string request = $"user/login?token={token}&username={user}";
                _logger?.LogDebug($"Login user request: {request}");
                HttpResponseMessage response = await _client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                _logger?.LogDebug($"Login user OK.");
                return JsonConvert.DeserializeObject<SessionData>(json, _settings);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in LoginUserAsync().");
            }

            return new SessionData();
        }

        /// <summary>
        /// Async method to logout from the Zipato web service.
        /// </summary>
        /// <returns>True if logout was successful</returns>
        private async Task<bool> LogoutUserAsync()
        {
            try
            {
                string request = $"user/logout";
                _logger?.LogDebug($"Logout user request: {request}");
                HttpResponseMessage response = await _client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                _logger?.LogDebug($"Logout user OK.");
                LoginData login = JsonConvert.DeserializeObject<LoginData>(json, _settings);

                return ((login != null) && login.Success);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in LogoutUserAsync().");
            }

            return false;
        }

        /// <summary>
        /// Helper function to calculate the SHA1 hash value (Secure Hash Algorithm 1).
        /// </summary>
        /// <param name="message">The message to be hashed</param>
        /// <returns>The hash value (message digest)</returns>
        private string SHA1(string message)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            StringBuilder hex = new StringBuilder((int)(bytes.Length * 2));

            for (int i = 0; i < bytes.Length; ++i)
            {
                hex.AppendFormat("{0:x2}", bytes[i]);
            }

            return hex.ToString();
        }

        /// <summary>
        /// Gets data from the Zipatile Web service as a string (typically containing JSON data).
        /// </summary>
        /// <param name="request">The Web service request string.</param>
        /// <returns>The response string retrieved.</returns>
        private async Task<(string Data, DataStatus Status)> GetStringAsync(string request)
        {
            if (IsActive)
            {
                try
                {
                    _logger?.LogDebug($"GET Request: {request}");
                    HttpResponseMessage response = await _client.GetAsync(request);
                    _logger?.LogDebug($"Status code: {response.StatusCode.ToString()}");
                    string result = await response.Content.ReadAsStringAsync();
                    DataStatus status = DataValue.Uncertain;
                    string data = string.Empty;

                    if (result.Equals("\"\""))
                    {
                        result = string.Empty;
                    }

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                        case HttpStatusCode.Accepted:
                        case HttpStatusCode.NoContent:
                            data = result;
                            status = DataValue.Good;
                            break;
                        case HttpStatusCode.NotFound:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotFound;
                            break;
                        case HttpStatusCode.BadRequest:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotSupported;
                            break;
                        case HttpStatusCode.Forbidden:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadResourceUnavailable;
                            break;
                        default:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.Bad;
                            break;
                    }

                    return (data, CheckForError(result, status));
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Exception in GetStringAsync().");
                    return (string.Empty, DataValue.BadInternalError);
                }
            }

            return (string.Empty, DataValue.BadNotConnected);
        }

        /// <summary>
        /// Sets data at the Zipatile Web service using a string content (typically containing JSON data).
        /// </summary>
        /// <param name="request">The Web service request string.</param>
        /// <param name="data">The string data.</param>
        /// <returns>Good if successful.</returns>
        private async Task<DataStatus> PutStringAsync(string request, string data)
        {
            if (IsActive)
            {
                try
                {
                    _logger?.LogDebug($"PUT Request: {request}");
                    StringContent content = new StringContent(data);
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    HttpResponseMessage response = await _client.PutAsync(request, content);
                    _logger?.LogDebug($"Status code: {response.StatusCode.ToString()}");
                    string result = await response.Content.ReadAsStringAsync();
                    DataStatus status = DataValue.Uncertain;

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                        case HttpStatusCode.Accepted:
                        case HttpStatusCode.NoContent:
                            status = DataValue.Good;
                            break;
                        case HttpStatusCode.NotFound:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotFound;
                            break;
                        case HttpStatusCode.BadRequest:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotSupported;
                            break;
                        case HttpStatusCode.Forbidden:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadResourceUnavailable;
                            break;
                        default:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.Bad;
                            break;
                    }

                    return CheckForError(result, status);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Exception in PutStringAsync().");
                    return DataValue.BadInternalError;
                }
            }

            return DataValue.BadNotConnected;
        }

        /// <summary>
        /// Sets data at the Zipatile Web service using a string content.
        /// </summary>
        /// <param name="request">The Web service request string.</param>
        /// <param name="data">The string data.</param>
        /// <returns>Good if successful.</returns>
        private async Task<DataStatus> PutTextAsync(string request, string data)
        {
            if (IsActive)
            {
                try
                {
                    _logger?.LogDebug($"PUT Request: {request}");
                    StringContent content = new StringContent(data);
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("text/plain");
                    HttpResponseMessage response = await _client.PutAsync(request, content);
                    _logger?.LogDebug($"Status code: {response.StatusCode.ToString()}");
                    string result = await response.Content.ReadAsStringAsync();
                    DataStatus status = DataValue.Uncertain;

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                        case HttpStatusCode.Accepted:
                        case HttpStatusCode.NoContent:
                            status = DataValue.Good;
                            break;
                        case HttpStatusCode.NotFound:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotFound;
                            break;
                        case HttpStatusCode.BadRequest:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotSupported;
                            break;
                        case HttpStatusCode.Forbidden:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadResourceUnavailable;
                            break;
                        default:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.Bad;
                            break;
                    }

                    return CheckForError(result, status);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Exception in PutTextAsync().");
                    return DataValue.BadInternalError;
                }
            }

            return DataValue.BadNotConnected;
        }

        /// <summary>
        /// Creates data at the Zipatile Web service using a string content (typically containing JSON data).
        /// </summary>
        /// <param name="request">The Web service request string.</param>
        /// <param name="data">The string data.</param>
        /// <returns>Good if successful.</returns>
        private async Task<DataStatus> PostStringAsync(string request, string data)
        {
            if (IsActive)
            {
                try
                {
                    _logger?.LogDebug($"POST Request: {request}");
                    StringContent content = new StringContent(data);
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    HttpResponseMessage response = await _client.PostAsync(request, content);
                    _logger?.LogDebug($"Status code: {response.StatusCode.ToString()}");
                    string result = await response.Content.ReadAsStringAsync();
                    DataStatus status = DataValue.Uncertain;

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                        case HttpStatusCode.Accepted:
                        case HttpStatusCode.NoContent:
                            status = DataValue.Good;
                            break;
                        case HttpStatusCode.NotFound:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotFound;
                            break;
                        case HttpStatusCode.BadRequest:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotSupported;
                            break;
                        case HttpStatusCode.Forbidden:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadResourceUnavailable;
                            break;
                        default:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.Bad;
                            break;
                    }

                    return CheckForError(result, status);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Exception in PostStringAsync().");
                    return DataValue.BadInternalError;
                }
            }

            return DataValue.BadNotConnected;
        }

        /// <summary>
        /// Creates data at the Zipatile Web service using a string content.
        /// </summary>
        /// <param name="request">The Web service request string.</param>
        /// <param name="data">The string data.</param>
        /// <returns>Good if successful.</returns>
        private async Task<DataStatus> PostTextAsync(string request, string data)
        {
            if (IsActive)
            {
                try
                {
                    _logger?.LogDebug($"POST Request: {request}");
                    StringContent content = new StringContent(data);
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("text/plain");
                    HttpResponseMessage response = await _client.PostAsync(request, content);
                    _logger?.LogDebug($"Status code: {response.StatusCode.ToString()}");
                    string result = await response.Content.ReadAsStringAsync();
                    DataStatus status = DataValue.Uncertain;

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                        case HttpStatusCode.Accepted:
                        case HttpStatusCode.NoContent:
                            status = DataValue.Good;
                            break;
                        case HttpStatusCode.NotFound:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotFound;
                            break;
                        case HttpStatusCode.BadRequest:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadNotSupported;
                            break;
                        case HttpStatusCode.Forbidden:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.BadResourceUnavailable;
                            break;
                        default:
                            _logger?.LogError($"Error reason: {response.ReasonPhrase}");
                            status = DataValue.Bad;
                            break;
                    }

                    return CheckForError(result, status);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Exception in PostTextAsync().");
                    return DataValue.BadInternalError;
                }
            }

            return DataValue.BadNotConnected;
        }

        /// <summary>
        /// Checks if the json string can be parsed as an error response.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private DataStatus CheckForError(string json, DataStatus status)
        {
            if (!string.IsNullOrEmpty(json))
            {
                json = json.Trim();

                if (json.StartsWith("{") && json.EndsWith("}"))
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<ErrorData>(json);

                        if (!result.Success)
                        {
                            _logger?.LogDebug($"Error: {result.Error}");
                            status = new DataStatus(DataStatus.BadNotSupported, "BadNotSupported", result.Error);
                        }
                    }
                    catch (JsonReaderException jex)
                    {
                        _logger?.LogError(jex, "Exception in CheckForError().");
                        status = new DataStatus(DataStatus.Bad, "Bad", $"Json Exception {jex.Message}.");
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, "Exception in CheckForError().");
                        status = new DataStatus(DataStatus.Bad, "Bad", $"Exception {ex.Message}.");
                    }
                }
            }

            return status;
        }

        #endregion Private Methods
    }
}
