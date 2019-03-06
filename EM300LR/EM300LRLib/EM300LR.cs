// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LR.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib
{
    #region Using Directives

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;
    using EM300LRLib.Models;

    #endregion Using Directives

    /// <summary>
    /// Class holding data from the EM300LR Symo 8.2-3-M inverter.
    /// The data properties are based on the specification EM300LR
    /// Solar API V1 (42,0410,2012,EN 008-15092016).
    /// </summary>
    public class EM300LR : BaseClass, IEM300LR
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client instantiated using the HTTP client factory.
        /// </summary>
        private readonly IEM300LRClient _client;

        /// <summary>
        /// The EM300LR specific settings used internally.
        /// </summary>
        private readonly ISettingsData _settings;

        /// <summary>
        /// Instantiate a Singleton of the Semaphore with a value of 1.
        /// This means that only 1 thread can be granted access at a time.
        /// </summary>
        static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        #endregion Private Data Members

        #region Public Properties

        /// <summary>
        /// The Data property holds all EM300LR data properties.
        /// </summary>
        public EM300LRData Data { get; }

        /// <summary>
        /// The InfoData property holds a subset of the EM300LR data values.
        /// </summary>
        [JsonIgnore]
        public TotalData TotalData { get; } = new TotalData();

        /// <summary>
        /// The InfoData property holds a subset of the EM300LR data values.
        /// </summary>
        [JsonIgnore]
        public Phase1Data Phase1Data { get; } = new Phase1Data();

        /// <summary>
        /// The InfoData property holds a subset of the EM300LR data values.
        /// </summary>
        [JsonIgnore]
        public Phase2Data Phase2Data { get; } = new Phase2Data();

        /// <summary>
        /// The InfoData property holds a subset of the EM300LR data values.
        /// </summary>
        [JsonIgnore]
        public Phase3Data Phase3Data { get; } = new Phase3Data();

        /// <summary>
        /// Returns true if no tasks can enter the semaphore.
        /// </summary>
        [JsonIgnore]
        public bool IsLocked { get => !(_semaphore.CurrentCount == 0); }

        /// <summary>
        /// Gets or sets the EM300LR web service base uri.
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
        /// Gets or sets the EM300LR web service password.
        /// </summary>
        [JsonIgnore]
        public string Password
        {
            get => _settings.Password;
            set => _settings.Password = value;
        }

        /// <summary>
        /// Gets or sets the EM300LR serial number.
        /// </summary>
        [JsonIgnore]
        public string SerialNumber
        {
            get => _settings.SerialNumber;
            set => _settings.SerialNumber = value;
        }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EM300LR"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The HTTP client.</param>
        /// <param name="settings">The EM300LR settings.</param>
        public EM300LR(ILogger<EM300LR> logger, IEM300LRClient client, ISettingsData settings)
            : base(logger)
        {
            _client = client;
            _settings = settings;

            Data = new EM300LRData();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Helper method to check for the EM300LR Web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckAccess()
            => (await GetStartAsync() == DataValue.Good);

        /// <summary>
        /// Updates all EM300LR properties reading the data from the EM300LR web service.
        /// If successful the data values will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public DataStatus ReadAll() => ReadAllAsync().Result;

        /// <summary>
        /// Updates all EM300LR properties reading the data from the EM300LR web service.
        /// If successful the data values will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");

                status = await GetStartAsync();

                if (status.IsGood)
                {
                    status = await PostStartAsync();

                    if (status.IsGood)
                    {
                        string json = await _client.GetStringAsync($"mum-webservice/data.php");

                        if (!string.IsNullOrEmpty(json))
                        {
                            var data = JsonConvert.DeserializeObject<EM300LRData>(json);

                            if (data.StatusCode == 0)
                            {
                                data.Status = DataValue.Good;
                                Data.Refresh(data);
                                TotalData.Refresh(data);
                                Phase1Data.Refresh(data);
                                Phase2Data.Refresh(data);
                                Phase3Data.Refresh(data);

                                _logger?.LogDebug($"ReadAllAsync OK.");
                            }
                            else
                            {
                                status = DataValue.BadDeviceFailure;
                                status.Explanation = $"EM300LR status code: {data.StatusCode}.";
                                _logger?.LogError($"EM300LR status code: {data.StatusCode}.");
                                _logger?.LogDebug($"ReadAllAsync not OK.");
                            }
                        }
                        else
                        {
                            _logger?.LogError("ReadAllAsync no data returned.");
                            status = DataValue.BadUnknownResponse;
                            status.Explanation = "EM300LR no data returned.";
                        }
                    }
                    else
                    {
                        _logger?.LogError("ReadAllAsync not authenticated.");
                        status = DataValue.BadNotConnected;
                        status.Explanation = "EM300LR not authenticated.";
                    }
                }
                else
                {
                    _logger?.LogError("ReadAllAsync invalid response.");
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
                _semaphore.Release();
                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        #endregion Public Methods

        #region Public Helpers

        /// <summary>
        /// Method to determine if the property is supported.
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
                    case "EM300LR":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(EM300LR), parts[1]) != null : true;

                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(EM300LRData), parts[1]) != null : true;

                    case "TotalData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(TotalData), parts[1]) != null : true;

                    case "Phase1Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Phase1Data), parts[1]) != null : true;

                    case "Phase2Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Phase2Data), parts[1]) != null : true;

                    case "Phase3Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Phase3Data), parts[1]) != null : true;

                    default:
                        return PropertyValue.GetPropertyInfo(typeof(EM300LRData), property) != null;
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
                    case "EM300LR":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;

                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : Data;

                    case "TotalData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(TotalData, parts[1]) : TotalData;

                    case "Phase1Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Phase1Data, parts[1]) : Phase1Data;

                    case "Phase2Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Phase2Data, parts[1]) : Phase2Data;

                    case "Phase3Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Phase3Data, parts[1]) : Phase3Data;

                    default:
                        return PropertyValue.GetPropertyValue(Data, property);
                }
            }

            return null;
        }

        #endregion Public Helpers

        #region Private Methods

        /// <summary>
        /// Async method to retrieve authentication data from the b-Control EM300LR web service.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> GetStartAsync()
        {
            DataStatus status = DataValue.Good;

            try
            {
                string json = await _client.GetStringAsync($"start.php");

                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonConvert.DeserializeObject<AuthentificationData>(json);

                    if (_settings.SerialNumber == data.SerialNumber)
                    {
                        _logger?.LogDebug("GetStartAsync OK.");
                        return status;
                    }
                    else
                    {
                        _logger?.LogDebug($"GetStartAsync: SerialNumber '{_settings.SerialNumber}' expected, but '{data.SerialNumber}' received.");
                        status = DataValue.BadNotFound;
                        status.Explanation = $"SerialNumber '{_settings.SerialNumber}' expected, but '{data.SerialNumber}' received.";
                        return status;
                    }
                }
                else
                {
                    _logger?.LogDebug("GetStartAsync: invalid response.");
                    status = DataValue.BadUnknownResponse;
                    status.Explanation = "Invalid response.";
                    return status;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"GetStartAsync exception: {ex.Message}.");
                status = DataValue.BadInternalError;
                status.Explanation = ex.Message;
                return status;
            }
        }

        /// <summary>
        /// Async method to retrieve authentication data from the b-Control EM300LR web service.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> PostStartAsync()
        {
            DataStatus status = DataValue.Good;

            try
            {
                string json = await _client.PostStringAsync($"start.php", $"login={_settings.SerialNumber}&password={_settings.Password}");

                if (!string.IsNullOrEmpty(json))
                {
                    var data = JsonConvert.DeserializeObject<AuthentificationData>(json);

                    if (data.Authentication)
                    {
                        _logger?.LogDebug("PostStartAsync OK.");
                        return status;
                    }
                    else
                    {
                        _logger?.LogDebug("GetStartAsync: not authenticated.");
                        status = DataValue.Bad;
                        status.Explanation = "Not authenticated.";
                        return status;
                    }
                }
                else
                {
                    _logger?.LogDebug("GetStartAsync: invalid response.");
                    status = DataValue.BadUnknownResponse;
                    status.Explanation = "Invalid response.";
                    return status;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"PostStartAsync exception: {ex.Message}.");
                status = DataValue.BadInternalError;
                status.Explanation = ex.Message;
                return status;
            }
        }

        #endregion Private Methods
    }
}
