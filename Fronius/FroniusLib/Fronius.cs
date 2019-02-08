// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fronius.cs" company="DTV-Online">
//   Copyright(c) 2017 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;
    using FroniusLib.Models;

    #endregion

    /// <summary>
    /// Class holding data from the Fronius Symo 8.2-3-M inverter.
    /// The data properties are based on the specification Fronius
    /// Solar API V1 (42,0410,2012,EN 008-15092016).
    /// </summary>
    public class Fronius : BaseClass, IFronius
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP client instantiated using the HTTP client factory.
        /// </summary>
        private readonly IFroniusClient _client;

        /// <summary>
        /// The Fronius settings used internally.
        /// </summary>
        private readonly ISettingsData _settings;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Data property holds all Fronius data properties.
        /// </summary>
        public FroniusData Data { get; }

        /// <summary>
        /// The InverterInfo holds all Fronius inverter data.
        /// </summary>
        [JsonIgnore]
        public InverterInfo InverterInfo { get; }

        /// <summary>
        /// The CommonData property holds all Fronius common data.
        /// </summary>
        [JsonIgnore]
        public CommonData CommonData { get; }

        /// <summary>
        /// The PhaseData property holds all Fronius phase data.
        /// </summary>
        [JsonIgnore]
        public PhaseData PhaseData { get; }

        /// <summary>
        /// The MinMaxData property holds all Fronius minmax data.
        /// </summary>
        [JsonIgnore]
        public MinMaxData MinMaxData { get; }

        /// <summary>
        /// The LoggerInfo property holds all Fronius logger info data.
        /// </summary>
        [JsonIgnore]
        public LoggerInfo LoggerInfo { get; }

        /// <summary>
        /// Gets or sets the Fronius API version.
        /// </summary>
        [JsonIgnore]
        public APIVersionData VersionInfo { get; private set; } = new APIVersionData();

        /// <summary>
        /// Flag indicating that the first update has been sucessful.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets or sets the Fronius web service base uri.
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
        /// Gets or sets the Fronius device id (used in retrieving the device data using the Fronius REST API).
        /// </summary>
        [JsonIgnore]
        public string DeviceID
        {
            get => _settings.DeviceID;
            set => _settings.DeviceID = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Fronius"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The HTTP client.</param>
        /// <param name="settings">The Fronius settings.</param>
        public Fronius(ILogger<Fronius> logger, IFroniusClient client, ISettingsData settings)
            : base(logger)
        {
            _client = client;
            _settings = settings;

            Data = new FroniusData();
            InverterInfo = new InverterInfo();
            CommonData = new CommonData();
            PhaseData = new PhaseData();
            MinMaxData = new MinMaxData();
            LoggerInfo = new LoggerInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper method to check for the Fronius Web service.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckAccess()
            => (await GetAPIVersionAsync() == DataValue.Good);

        /// <summary>
        /// Updates all Fronius properties reading the data from the Fronius web service.
        /// If successful the data values will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync()
        {
            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");

                await ReadCommonDataAsync();
                await ReadInverterInfoAsync();
                await ReadLoggerInfoAsync();
                await ReadMinMaxDataAsync();
                await ReadPhaseDataAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadAllAsync().");
                Data.Status = DataValue.BadUnexpectedError;
            }
            finally
            {
                if (CommonData.IsGood && InverterInfo.IsGood &&
                    LoggerInfo.IsGood && MinMaxData.IsGood && PhaseData.IsGood)
                {
                    Data.Status = DataValue.Good;
                    if (IsInitialized == false) IsInitialized = true;
                }

                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates all Fronius inverter info properties reading the data from the Fronius web service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadInverterInfoAsync()
        {
            try
            {
                string json = await _client.GetStringAsync($"solar_api/v1/GetInverterInfo.cgi");

                if (!string.IsNullOrEmpty(json))
                {
                    FroniusInverterInfo raw = JsonConvert.DeserializeObject<FroniusInverterInfo>(json);
                    Data.InverterInfo = new InverterDeviceData(raw);

                    if (Data.InverterInfo.Response.Status.Code == 0)
                    {
                        Data.Status = DataValue.Good;
                        InverterInfo.Refresh(Data);
                        _logger?.LogDebug("ReadInverterInfoAsync OK.");
                    }
                    else
                    {
                        Data.Status = DataValue.BadUnexpectedError;
                        Data.Status.Explanation = Data.InverterInfo.Response.Status.UserMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadInverterInfoDataAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
                InverterInfo.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates all Fronius common data properties reading the data from the Fronius web service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadCommonDataAsync()
        {
            try
            {
                string json = await _client.GetStringAsync($"solar_api/v1/GetInverterRealtimeData.cgi?Scope=Device&DeviceId={DeviceID}&DataCollection=CommonInverterData");

                if (!string.IsNullOrEmpty(json))
                {
                    FroniusCommonData raw = JsonConvert.DeserializeObject<FroniusCommonData>(json);
                    Data.CommonData = new CommonDeviceData(raw);

                    if (Data.CommonData.Response.Status.Code == 0)
                    {
                        Data.Status = DataValue.Good;
                        CommonData.Refresh(Data);
                        _logger?.LogDebug("ReadCommonDataAsync OK.");
                    }
                    else
                    {
                        Data.Status = DataValue.BadUnexpectedError;
                        Data.Status.Explanation = Data.CommonData.Response.Status.UserMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadCommonDataAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
                CommonData.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates all Fronius phase data properties reading the data from the Fronius web service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadPhaseDataAsync()
        {
            try
            {
                string json = await _client.GetStringAsync($"solar_api/v1/GetInverterRealtimeData.cgi?Scope=Device&DeviceId={DeviceID}&DataCollection=3PInverterData");

                if (!string.IsNullOrEmpty(json))
                {
                    FroniusPhaseData raw = JsonConvert.DeserializeObject<FroniusPhaseData>(json);
                    Data.PhaseData = new PhaseDeviceData(raw);

                    if (Data.PhaseData.Response.Status.Code == 0)
                    {
                        Data.Status = DataValue.Good;
                        PhaseData.Refresh(Data);
                        _logger?.LogDebug("ReadPhaseDataAsync OK.");
                    }
                    else
                    {
                        Data.Status = DataValue.BadUnexpectedError;
                        Data.Status.Explanation = Data.PhaseData.Response.Status.UserMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadPhaseDataAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
                PhaseData.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates all Fronius minmax data properties reading the data from the Fronius web service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadMinMaxDataAsync()
        {
            try
            {
                string json = await _client.GetStringAsync($"solar_api/v1/GetInverterRealtimeData.cgi?Scope=Device&DeviceId={DeviceID}&DataCollection=MinMaxInverterData");

                if (!string.IsNullOrEmpty(json))
                {
                    FroniusMinMaxData raw = JsonConvert.DeserializeObject<FroniusMinMaxData>(json);
                    Data.MinMaxData = new MinMaxDeviceData(raw);

                    if (Data.MinMaxData.Response.Status.Code == 0)
                    {
                        Data.Status = DataValue.Good;
                        MinMaxData.Refresh(Data);
                        _logger?.LogDebug("ReadMinMaxDataAsync OK.");
                    }
                    else
                    {
                        Data.Status = DataValue.BadUnexpectedError;
                        Data.Status.Explanation = Data.MinMaxData.Response.Status.UserMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMinMaxDataAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
                MinMaxData.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates all Fronius inverter info properties reading the data from the Fronius web service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadLoggerInfoAsync()
        {
            try
            {
                string json = await _client.GetStringAsync($"solar_api/v1/GetLoggerInfo.cgi");

                if (!string.IsNullOrEmpty(json))
                {
                    FroniusLoggerInfo raw = JsonConvert.DeserializeObject<FroniusLoggerInfo>(json);
                    Data.LoggerInfo = new LoggerDeviceData(raw);

                    if (Data.LoggerInfo.Response.Status.Code == 0)
                    {
                        Data.Status = DataValue.Good;
                        LoggerInfo.Refresh(Data);
                        _logger?.LogDebug("ReadLoggerInfoAsync OK.");
                    }
                    else
                    {
                        Data.Status = DataValue.BadUnexpectedError;
                        Data.Status.Explanation = Data.LoggerInfo.Response.Status.UserMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadLoggerInfoAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
                LoggerInfo.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

        /// <summary>
        /// Async method to retrieve the Fronius web API version.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> GetAPIVersionAsync()
        {
            try
            {
                string json = await _client.GetStringAsync($"solar_api/GetAPIVersion.cgi");

                if (!string.IsNullOrEmpty(json))
                {
                    VersionInfo = JsonConvert.DeserializeObject<APIVersionData>(json);
                    Data.Status = DataValue.Good;
                    _logger?.LogDebug("GetAPIVersionAsync OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"GetAPIVersionAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

        #endregion Public Methods

        #region Public Helpers

        /// <summary>
        /// Async method to retrieve just the necessary data depending on the property.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadPropertyAsync(string property)
        {
            DataStatus status = DataValue.BadNotFound;

            if (!string.IsNullOrEmpty(property))
            {
                string[] parts = property.Split(new[] { '.' }, 2);

                switch (parts[0])
                {
                    case "Fronius":
                        if (parts.Length > 1)
                        {
                            return await ReadPropertyAsync(parts[1]);
                        }
                        else
                        {
                            return await ReadAllAsync();
                        }
                    case "Inverter":
                    case "InverterInfo":
                        return await ReadInverterInfoAsync();
                    case "Logger":
                    case "LoggerInfo":
                        return await ReadLoggerInfoAsync();
                    case "Phase":
                    case "PhaseData":
                        return await ReadPhaseDataAsync();
                    case "MinMax":
                    case "MinMaxData":
                        return await ReadMinMaxDataAsync();
                    case "Common":
                    case "CommonData":
                        return await ReadCommonDataAsync();
                }
            }

            return status;
        }

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
                    case "Fronius":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Fronius), parts[1]) != null : true;
                    case "Inverter":
                    case "InverterInfo":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(InverterInfo), parts[1]) != null : true;
                    case "Logger":
                    case "LoggerInfo":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(LoggerInfo), parts[1]) != null : true;
                    case "Phase":
                    case "PhaseData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(PhaseData), parts[1]) != null : true;
                    case "MinMax":
                    case "MinMaxData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(MinMaxData), parts[1]) != null : true;
                    case "Common":
                    case "CommonData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(CommonData), parts[1]) != null : true;
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
                    case "Fronius":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Inverter":
                    case "InverterInfo":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(InverterInfo, parts[1]) : InverterInfo;
                    case "Logger":
                    case "LoggerInfo":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(LoggerInfo, parts[1]) : LoggerInfo;
                    case "Phase":
                    case "PhaseData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(PhaseData, parts[1]) : PhaseData;
                    case "MinMax":
                    case "MinMaxData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(MinMaxData, parts[1]) : MinMaxData;
                    case "Common":
                    case "CommonData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(CommonData, parts[1]) : CommonData;
                }
            }

            return null;
        }

        #endregion Public Helpers
    }
}
