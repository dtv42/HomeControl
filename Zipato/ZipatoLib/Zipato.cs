// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zipato.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;
    using ZipatoLib.Models;

    #endregion

    /// <summary>
    /// Class holding data from the Zipato Zipatile home control.
    /// The data properties are based on the online specification found at (https://my.zipato.com/zipato-web/api/).
    /// </summary>
    public partial class Zipato : BaseClass, IZipato
    {
        #region Private Data Members

        /// <summary>
        /// The instantiated HTTP client.
        /// </summary>
        private readonly IZipatoClient _client;

        /// <summary>
        /// The Zipato specific settings used internally.
        /// </summary>
        private readonly ISettingsData _settings;

        /// <summary>
        /// Instantiate a Singleton of the Semaphore with a value of 1.
        /// This means that only 1 thread can be granted access at a time.
        /// </summary>
        static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        #endregion

        #region Public Properties

        /// <summary>
        /// The Data property holds all Zipato data properties.
        /// </summary>
        public ZipatoData Data { get; private set; } = new ZipatoData();

        /// <summary>
        /// The Values property holds selected Zipato other devices.
        /// </summary>
        [JsonIgnore]
        public ZipatoOthers Others { get; private set; } = new ZipatoOthers();

        /// <summary>
        /// The Values property holds selected Zipato devices.
        /// </summary>
        [JsonIgnore]
        public ZipatoDevices Devices { get; private set; } = new ZipatoDevices();

        /// <summary>
        /// The Values property holds selected Zipato sensors (and meters).
        /// </summary>
        [JsonIgnore]
        public ZipatoSensors Sensors { get; private set; } = new ZipatoSensors();

        /// <summary>
        /// Flag indicating that the first update has been sucessful.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Flag indicating that a session is active (logged in).
        /// </summary>
        public bool IsSessionActive { get => ZipatoSession.IsSessionActive; }

        /// <summary>
        /// Gets or sets the Zipato web service base uri.
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
        /// Gets or sets the cookie container used in sending a request.
        /// </summary>
        [JsonIgnore]
        public CookieContainer Cookies
        {
            get => _client.Cookies;
            set => _client.Cookies = value;
        }

        /// <summary>
        /// Gets or sets the Zipato User.
        /// </summary>
        [JsonIgnore]
        public string User
        {
            get => _settings.User;
            set => _settings.User = value;
        }

        /// <summary>
        /// Gets or sets the Zipato user password.
        /// </summary>
        [JsonIgnore]
        public string Password
        {
            get => _settings.Password;
            set => _settings.Password = value;
        }

        /// <summary>
        /// Flag indicating that the Zipato box is accessed in the local network.
        /// </summary>
        [JsonIgnore]
        public bool IsLocal
        {
            get => _settings.IsLocal;
            set => _settings.IsLocal = value;
        }

        /// <summary>
        /// Gets or sets the session timeout in milliseconds.
        /// </summary>
        [JsonIgnore]
        public int SessionTimeout
        {
            get => _settings.SessionTimeout;
            set => _settings.SessionTimeout = value;
        }

        /// <summary>
        /// Settings for the scenes.
        /// </summary>
        [JsonIgnore]
        public OthersInfo OthersInfo
        {
            get => _settings.OthersInfo;
            set => _settings.OthersInfo = value;
        }

        /// <summary>
        /// Settings for the devices.
        /// </summary>
        [JsonIgnore]
        public DevicesInfo DevicesInfo
        {
            get => _settings.DevicesInfo;
            set => _settings.DevicesInfo = value;
        }

        /// <summary>
        /// Settings for the sensors (and meters).
        /// </summary>
        [JsonIgnore]
        public SensorsInfo SensorsInfo
        {
            get => _settings.SensorsInfo;
            set => _settings.SensorsInfo = value;
        }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Zipato"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The HTTP client.</param>
        /// <param name="settings">The Zipato settings.</param>
        public Zipato(ILogger<Zipato> logger, IZipatoClient client, ISettingsData settings)
            : base(logger)
        {
            _client = client;
            _settings = settings;

            Data = new ZipatoData();
            Others = new ZipatoOthers(this);
            Devices = new ZipatoDevices(this);
            Sensors = new ZipatoSensors(this);
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<(string, DataStatus)> ReadDataAsync(string query)
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    return await session.GetDataAsync(query);
                }
                else
                {
                    _logger?.LogDebug($"ReadDataAsync('{query}'): Session not established.");
                    return (string.Empty, DataValue.BadCommunicationError);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in ReadDataAsync('{query}').");
                return (string.Empty, DataValue.BadUnexpectedError);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<(T, DataStatus)> ReadDataAsync<T>(string query) where T : new()
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    return await session.GetDataAsync<T>(query);
                }
                else
                {
                    _logger?.LogDebug($"ReadDataAsync<T>('{query}'): Session not established.");
                    return (new T(), DataValue.BadCommunicationError);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in ReadDataAsync('{query}').");
                return (new T(), DataValue.BadUnexpectedError);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<(List<T>, DataStatus)> ReadListDataAsync<T>(string query) where T : new()
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    return await session.GetListDataAsync<T>(query);
                }
                else
                {
                    _logger?.LogDebug($"ReadListDataAsync<T>('{query}'): Session not established.");
                    return (new List<T> { }, DataValue.BadCommunicationError);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in ReadListDataAsync<T>('{query}').");
                return (new List<T> { }, DataValue.BadUnexpectedError);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<DataStatus> CreateDataAsync<T>(string query, T data) where T : new()
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    var status = await session.PostDataAsync<T>(query, data);

                    if (status != DataValue.Good)
                    {
                        _logger?.LogDebug($"CreateDataAsync<T>('{query}'): Not successful.", data);
                    }

                    return status;
                }
                else
                {
                    _logger?.LogDebug($"CreateDataAsync<T>('{query}'): Session not established.");
                    return DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in CreateDataAsync<T>('{query}').");
                return DataValue.BadUnexpectedError;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<DataStatus> UpdateDataAsync<T>(string query, T data) where T : new()
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    var status = await session.PutDataAsync<T>(query, data);

                    if (status != DataValue.Good)
                    {
                        _logger?.LogDebug($"UpdateDataAsync<T>('{query}'): Not successful.", data);
                    }

                    return status;
                }
                else
                {
                    _logger?.LogDebug($"UpdateDataAsync<T>('{query}'): Session not established.");
                    return DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in UpdateDataAsync<T>('{query}').");
                return DataValue.BadUnexpectedError;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="data"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<DataStatus> UpdateDataAsync(string query, string data)
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    var status = await session.PutDataAsync(query, data);

                    if (status != DataValue.Good)
                    {
                        _logger?.LogDebug($"UpdateDataAsync('{query}', '{data}'): Not successful.");
                    }

                    return status;
                }
                else
                {
                    _logger?.LogDebug($"UpdateDataAsync('{query}', '{data}'): Session not established.");
                    return DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in UpdateDataAsync('{query}', '{data}').");
                return DataValue.BadUnexpectedError;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <param name="currentsession"></param>
        /// <returns></returns>
        private async Task<DataStatus> DeleteDataAsync(string query)
        {
            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    var status = await session.DeleteAsync(query);

                    if (status != DataValue.Good)
                    {
                        _logger?.LogDebug($"DeleteDataAsync('{query}'): Not successful.");
                    }

                    return status;
                }
                else
                {
                    _logger?.LogDebug($"DeleteDataAsync('{query}'): Session not established.");
                    return DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in DeleteDataAsync('{query}').");
                return DataValue.BadUnexpectedError;
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Starts a Zipato sessions (logging in).
        /// </summary>
        public ZipatoSession StartSession() => ZipatoSession.StartSession(_logger, _client, _settings);

        /// <summary>
        /// Ends the current session (logging out).
        /// </summary>
        public void EndSession() => ZipatoSession.EndSession();

        /// <summary>
        /// Reads all data from the Zipato web service.
        /// </summary>
        /// <remarks>Note this may take several minutes to complete.</remarks>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");
                var session = StartSession();

                if (session.IsActive)
                {
                    await DataReadAlarmAsync();
                    await DataReadAttributesFullAsync();
                    await DataReadBrandsFullAsync();
                    await DataReadCamerasFullAsync();
                    await DataReadClusterEndpointsFullAsync();
                    await DataReadContactsAsync();
                    await DataReadDevicesFullAsync();
                    await DataReadEndpointsFullAsync();
                    await DataReadGroupsFullAsync();
                    await DataReadNetworksFullAsync();
                    await DataReadNetworkTreesAsync();
                    await DataReadRoomsAsync();
                    await DataReadScenesFullAsync();
                    await DataReadSchedulesFullAsync();
                    await DataReadSavedFilesAsync();
                    await DataReadThermostatsFullAsync();
                    await DataReadBoxAsync();
                    await DataReadValuesAsync();

                    if (!IsLocal)
                    {
                        await DataReadAnnouncementsAsync();
                        await DataReadBindingsFullAsync();
                        await DataReadClustersAsync();
                        await DataReadRulesAsync();
                        await DataReadVirtualEndpointsAsync();
                    }

                    if (Data.Status.IsGood)
                    {
                        if (!IsInitialized)
                        {
                            IsInitialized = true;
                        }

                        Others.Update();
                        Devices.Update();
                        Sensors.Update();
                    }
                }
                else
                {
                    _logger?.LogDebug($"ReadAllAsync(): Session not established.");
                    status = DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadAllAsync().");
                status = DataValue.BadUnexpectedError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                EndSession();
                _semaphore.Release();
                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Reads just the minimal data values from the Zipato web service.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadAllDataAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadAllDataAsync() starting.");

                var session = StartSession();

                if (session.IsActive)
                {
                    await DataReadAttributesEndpointsAsync();
                    await DataReadEndpointsAsync();
                    await DataReadCamerasAsync();
                    await DataReadSavedFilesAsync();
                    await DataReadScenesAsync();

                    if (Data.Status.IsGood)
                    {
                        Others.Update();
                        Devices.Update();
                        Sensors.Update();
                    }
                }
                else
                {
                    _logger?.LogDebug($"ReadAllDataAsync: Session not established.");
                    status = DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadAllDataAsync().");
                status = DataValue.BadUnexpectedError;
            }
            finally
            {
                EndSession();
                _semaphore.Release();
                _logger?.LogDebug("ReadAllDataAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Reads all attribute data values from the Zipato web service.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllValuesAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                var session = StartSession();

                if (session.IsActive)
                {
                    _logger?.LogDebug("ReadAllValuesAsync() starting.");

                    await DataReadAttributeValuesAsync();

                    Others.Refresh();
                    Devices.Refresh();
                    Sensors.Refresh();
                }
                else
                {
                    _logger?.LogDebug($"ReadAllValuesAsync(): Session not established.");
                    Data.Status = DataValue.BadCommunicationError;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadAllValuesAsync().");
                Data.Status = DataValue.BadUnexpectedError;
            }
            finally
            {
                EndSession();
                _semaphore.Release();
                _logger?.LogDebug("ReadAllValuesAsync() finished.");
            }

            return Data.Status;
        }

        #endregion Public Methods
    }
}
