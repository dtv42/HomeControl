// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataLib
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
    using HomeDataLib.Models;

    #endregion

    public class HomeData : BaseClass, IHomeData
    {
        #region Private Data Members

        /// <summary>
        /// The HTTP clients instantiated using the HTTP client factory.
        /// </summary>
        private readonly IHomeDataClient1 _client1;
        private readonly IHomeDataClient2 _client2;
        private int _timeout;
        private int _retries;

        /// <summary>
        /// The Fronius settings used internally.
        /// </summary>
        private readonly Models.ISettingsData _settings;

        /// <summary>
        /// Instantiate a Singleton of the Semaphore with a value of 1.
        /// This means that only 1 thread can be granted access at a time.
        /// </summary>
        static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        #endregion

        #region Public Properties

        /// <summary>
        /// The Data property holds all home data properties.
        /// </summary>
        public HomeValues Data { get; }

        /// <summary>
        /// The Meter1 properties holds all EM300LR-1 data.
        /// </summary>
        public MeterData Meter1 { get; }

        /// <summary>
        /// The Meter2 properties holds all EM300LR-2 data.
        /// </summary>
        public MeterData Meter2 { get; }

        /// <summary>
        /// Returns true if no tasks can enter the semaphore.
        /// </summary>
        [JsonIgnore]
        public bool IsLocked { get => !(_semaphore.CurrentCount == 0); }

        /// <summary>
        /// Gets or sets the meter 1 web service base uri.
        /// </summary>
        [JsonIgnore]
        public string Meter1Address
        {
            get => _client1.BaseAddress;
            set => _client1.BaseAddress = value;
        }

        /// <summary>
        /// Gets or sets the timeout of the Http requests in seconds.
        /// </summary>
        [JsonIgnore]
        public int Timeout
        {
            get => _timeout;
            set { _timeout = value; _client1.Timeout = value; _client2.Timeout = value; }
        }

        /// <summary>
        /// Gets or sets the number of retries sending a request.
        /// </summary>
        [JsonIgnore]
        public int Retries
        {
            get => _retries;
            set { _retries = value; _client1.Retries = value; _client2.Retries = value; }
        }

        /// <summary>
        /// Gets or sets the meter 2 web service base uri.
        /// </summary>
        [JsonIgnore]
        public string Meter2Address
        {
            get => _client2.BaseAddress;
            set => _client2.BaseAddress = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeData"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="settings">The HomeData settings.</param>
        public HomeData(ILogger<HomeData> logger,
                        IHomeDataClient1 client1,
                        IHomeDataClient2 client2,
                        Models.ISettingsData settings)
            : base(logger)
        {
            _client1 = client1;
            _client2 = client2;
            _settings = settings;

            Data = new HomeValues();
            Meter1 = new MeterData();
            Meter2 = new MeterData();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Helper method to check for the EM300LR Web services.
        /// </summary>
        /// <returns></returns>
        public bool CheckAccess()
            => !string.IsNullOrEmpty(_client1.GetStringAsync("health").Result) &&
               !string.IsNullOrEmpty(_client2.GetStringAsync("health").Result);

        /// <summary>
        /// Updates all HomeData properties reading the data from the EM300LR web services.
        /// If successful the data values will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public DataStatus ReadAll() => ReadAllAsync().Result;

        /// <summary>
        /// Updates all HomeData properties reading the data from the EM300LR web services.
        /// If successful the data values will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync(bool update = false)
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");

                await ReadMeter1TotalAsync(update);
                await ReadMeter1Phase1Async();
                await ReadMeter1Phase2Async();
                await ReadMeter1Phase3Async();

                await ReadMeter2TotalAsync(update);
                await ReadMeter2Phase1Async();
                await ReadMeter2Phase2Async();
                await ReadMeter2Phase3Async();
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

                if (Meter1.Total.IsGood && Meter1.Phase1.IsGood && Meter1.Phase2.IsGood && Meter1.Phase3.IsGood &&
                    Meter2.Total.IsGood && Meter2.Phase1.IsGood && Meter2.Phase2.IsGood && Meter2.Phase3.IsGood)
                {
                    Data.Status = DataValue.Good;
                    Data.Refresh(Meter1, Meter2);
                }

                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        #endregion

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
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(HomeValues), parts[1]) != null : true;
                    case "Meter1":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(MeterData), parts[1]) != null : true;
                    case "Meter2":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(MeterData), parts[1]) != null : true;
                    default:
                        return IsProperty("Data." + property);
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
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : this;
                    case "Meter1":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Meter1, parts[1]) : this;
                    case "Meter2":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Meter2, parts[1]) : this;
                    default:
                        return GetPropertyValue("Data." + property);
                }
            }

            return null;
        }

        #endregion Public Helpers

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter1TotalAsync(bool update = false)
        {
            try
            {
                string json = await _client1.GetStringAsync($"api/em300lr/total?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter1.Total = JsonConvert.DeserializeObject<TotalData>(json);
                    _logger?.LogDebug($"ReadMeter1TotalAsync() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter1TotalAsync exception: {ex.Message}.");
                Meter1.Total.Status = DataValue.BadInternalError;
            }

            return Meter1.Total.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter1Phase1Async(bool update = false)
        {
            try
            {
                string json = await _client1.GetStringAsync($"api/em300lr/phase1?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter1.Phase1 = JsonConvert.DeserializeObject<Phase1Data>(json);
                    _logger?.LogDebug($"ReadMeter1Phase1Async() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter1Phase1Async exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter1.Phase1.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter1Phase2Async(bool update = false)
        {
            try
            {
                string json = await _client1.GetStringAsync($"api/em300lr/phase2?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter1.Phase2 = JsonConvert.DeserializeObject<Phase2Data>(json);
                    _logger?.LogDebug($"ReadMeter1Phase2Async() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter1Phase2Async exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter1.Phase2.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter1Phase3Async(bool update = false)
        {
            try
            {
                string json = await _client1.GetStringAsync($"api/em300lr/phase3?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter1.Phase3 = JsonConvert.DeserializeObject<Phase3Data>(json);
                    _logger?.LogDebug($"ReadMeter1Phase3Async() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter1Phase3Async exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter1.Phase3.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter2TotalAsync(bool update = false)
        {
            try
            {
                string json = await _client2.GetStringAsync($"api/em300lr/total?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter2.Total = JsonConvert.DeserializeObject<TotalData>(json);
                    _logger?.LogDebug($"ReadMeter2TotalAsync() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter2TotalAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter2.Total.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter2Phase1Async(bool update = false)
        {
            try
            {
                string json = await _client2.GetStringAsync($"api/em300lr/phase1?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter2.Phase1 = JsonConvert.DeserializeObject<Phase1Data>(json);
                    _logger?.LogDebug($"ReadMeter2Phase1Async() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter2Phase1Async exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter2.Phase1.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter2Phase2Async(bool update = false)
        {
            try
            {
                string json = await _client2.GetStringAsync($"api/em300lr/phase2?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter2.Phase2 = JsonConvert.DeserializeObject<Phase2Data>(json);
                    _logger?.LogDebug($"ReadMeter2Phase2Async() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter2Phase2Async exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter2.Phase2.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<DataStatus> ReadMeter2Phase3Async(bool update = false)
        {
            try
            {
                string json = await _client2.GetStringAsync($"api/em300lr/phase3?update={update.ToString().ToLower()}");

                if (!string.IsNullOrEmpty(json))
                {
                    Meter2.Phase3 = JsonConvert.DeserializeObject<Phase3Data>(json);
                    _logger?.LogDebug($"ReadMeter2Phase3Async() OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadMeter2Phase3Async exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Meter2.Phase3.Status;
        }

        #endregion
    }
}
