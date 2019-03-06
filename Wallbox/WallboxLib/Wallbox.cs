// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wallbox.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using BaseClassLib;
    using DataValueLib;
    using WallboxLib.Models;

    #endregion

    /// <summary>
    /// Class holding data from the BMW wallbox.
    /// The data properties are based on the KEBA specification "KeContact P30 Charging station UDP Programmers Guide V 2.01".
    /// Document: V 2.01 / Document No.: 92651, from 10.09.2018
    /// </summary>
    public class Wallbox : BaseClass, IWallbox
    {
        #region Constants

        public static readonly int MAX_REPORTS = 30;
        public static readonly int REPORTS_ID = 100;

        #endregion

        #region Private Data Members

        /// <summary>
        /// The instantiated UDP client.
        /// </summary>
        private readonly IWallboxClient _client;

        /// <summary>
        /// The Wallbox settings used internally.
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
        /// The Data property holds all Wallbox data values.
        /// </summary>
        public WallboxData Data { get; }

        /// <summary>
        /// The Report1 property holds all Wallbox report 1 data.
        /// </summary>
        [JsonIgnore]
        public Report1Data Report1 { get; } = new Report1Data();

        /// <summary>
        /// The Report2 property holds all Wallbox report 2 data.
        /// </summary>
        [JsonIgnore]
        public Report2Data Report2 { get; } = new Report2Data();

        /// <summary>
        /// The Report3 property holds all Wallbox report 3 data.
        /// </summary>
        [JsonIgnore]
        public Report3Data Report3 { get; } = new Report3Data();

        /// <summary>
        /// The Report100 property holds all Wallbox report 100 data.
        /// </summary>
        [JsonIgnore]
        public ReportsData Report100 { get; } = new ReportsData();

        /// <summary>
        /// The Reports property holds all Wallbox charging report data.
        /// </summary>
        [JsonIgnore]
        public List<ReportsData> Reports { get; } = new List<ReportsData> { };

        /// <summary>
        /// Gets or sets the Wallbox info data.
        /// </summary>
        [JsonIgnore]
        public InfoData Info { get; private set; } = new InfoData();

        /// <summary>
        /// Returns true if no tasks can enter the semaphore.
        /// </summary>
        [JsonIgnore]
        public bool IsLocked { get => !(_semaphore.CurrentCount == 0); }

        /// <summary>
        /// Gets or sets the Wallbox UDP service hostname.
        /// </summary>
        [JsonIgnore]
        public string HostName
        {
            get => _client.HostName;
            set => _client.HostName = value;
        }

        /// <summary>
        /// Gets or sets the Wallbox UDP service port.
        /// </summary>
        [JsonIgnore]
        public int Port
        {
            get => _client.Port;
            set => _client.Port = value;
        }

        /// <summary>
        /// Gets or sets the Wallbox UDP receive timeout.
        /// </summary>
        [JsonIgnore]
        public double Timeout
        {
            get => _client.Timeout;
            set => _client.Timeout = value;
        }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Wallbox"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The UDP client.</param>
        /// <param name="settings">The Wallbox settings.</param>
        public Wallbox(ILogger<Wallbox> logger, IWallboxClient client, ISettingsData settings)
            : base(logger)
        {
            _client = client;
            _settings = settings;

            Data = new WallboxData();

            for (int i = 0; i < MAX_REPORTS; ++i)
            {
                Reports.Add(new ReportsData());
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Synchronous methods.
        /// </summary>
        public DataStatus ReadAll() => ReadAllAsync().Result;
        public DataStatus ReadReport1() => ReadReport1Async().Result;
        public DataStatus ReadReport2() => ReadReport2Async().Result;
        public DataStatus ReadReport3() => ReadReport3Async().Result;
        public DataStatus ReadReport100() => ReadReport100Async().Result;
        public DataStatus ReadReports() => ReadReportsAsync().Result;
        public DataStatus ReadReport(int id) => ReadReportAsync(id).Result;
        public DataStatus ReadProperty(string property) => ReadPropertyAsync(property).Result;

        /// <summary>
        /// Check if a valid Access Token can be retrieved.
        /// </summary>
        /// <returns>True if a valid Access Token is available.</returns>
        public bool CheckAccess()
        {
            string json = "{ " + _client.SendReceiveAsync("i").Result + " }";
            Info = JsonConvert.DeserializeObject<InfoData>(json);
            return (Info.Firmware.Length > 0);
        }

        /// <summary>
        /// Updates all Wallbox properties reading the data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync()
        {
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");

                await ReadReport1Async();
                await ReadReport2Async();
                await ReadReport3Async();
                await ReadReport100Async();
                await ReadReportsAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadAllAsync().");
                status = DataValue.BadUnexpectedError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the Wallbox properties reading the report 1 data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadReport1Async()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadReport1Async() starting.");
                string json = await _client.SendReceiveAsync("report 1");

                if (!string.IsNullOrEmpty(json))
                {
                    Data.Report1 = JsonConvert.DeserializeObject<Report1Udp>(json);
                    Report1.Refresh(Data);
                    _logger?.LogDebug("ReadReport1Async OK.");
                }
                else
                {
                    _logger?.LogDebug($"ReadReport1Async not OK.");
                    status = DataValue.BadUnknownResponse;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadReport1Async exception: {ex.Message}.");
                status = DataValue.BadInternalError;
            }
            finally
            {
                _semaphore.Release();
                _logger?.LogDebug("ReadReport1Async() finished.");
            }

            Report1.Status = status;
            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the Wallbox properties reading the report 2 data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadReport2Async()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadReport2Async() starting.");
                string json = await _client.SendReceiveAsync("report 2");

                if (!string.IsNullOrEmpty(json))
                {
                    Data.Report2 = JsonConvert.DeserializeObject<Report2Udp>(json);
                    Report2.Refresh(Data);
                    _logger?.LogDebug("ReadReport2Async OK.");
                }
                else
                {
                    _logger?.LogDebug($"ReadReport2Async not OK.");
                    status = DataValue.BadUnknownResponse;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadReport2Async exception: {ex.Message}.");
                status = DataValue.BadInternalError;
            }
            finally
            {
                _semaphore.Release();
                _logger?.LogDebug("ReadReport2Async() finished.");
            }

            Report2.Status = status;
            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the Wallbox properties reading the report 3 data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadReport3Async()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadReport3Async() starting.");
                string json = await _client.SendReceiveAsync("report 3");

                if (!string.IsNullOrEmpty(json))
                {
                    Data.Report3 = JsonConvert.DeserializeObject<Report3Udp>(json);
                    Report3.Refresh(Data);
                    _logger?.LogDebug("ReadReport3Async OK.");
                }
                else
                {
                    _logger?.LogDebug($"ReadReport3Async not OK.");
                    status = DataValue.BadUnknownResponse;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadReport3Async exception: {ex.Message}.");
                status = DataValue.BadInternalError;
            }
            finally
            {
                _semaphore.Release();
                _logger?.LogDebug("ReadReport3Async() finished.");
            }

            Report3.Status = status;
            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the Wallbox properties reading the report 100 data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadReport100Async()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadReport100Async() starting.");
                string json = await _client.SendReceiveAsync("report 100");

                if (!string.IsNullOrEmpty(json))
                {
                    Data.Report100 = JsonConvert.DeserializeObject<ReportsUdp>(json);
                    Report100.Refresh(Data);
                    _logger?.LogDebug("ReadReport100Async OK.");
                }
                else
                {
                    _logger?.LogDebug($"ReadReport100Async not OK.");
                    status = DataValue.BadUnknownResponse;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadReport100Async exception: {ex.Message}.");
                status = DataValue.BadInternalError;
            }
            finally
            {
                _semaphore.Release();
                _logger?.LogDebug("ReadReport100Async() finished.");
            }

            Report100.Status = status;
            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the Wallbox properties reading charging report data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <param name="id">The reports ID (101 - 130).</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadReportAsync(int id)
        {
            if ((id > REPORTS_ID) && (id <= (REPORTS_ID + MAX_REPORTS)))
            {
                int index = id - REPORTS_ID - 1;

                try
                {
                    string json = await _client.SendReceiveAsync($"report {id}");

                    if (!string.IsNullOrEmpty(json))
                    {
                        Data.Reports[index] = JsonConvert.DeserializeObject<ReportsUdp>(json);
                        Data.Status = DataValue.Good;
                        Reports[index].Refresh(Data, id);
                        _logger?.LogDebug($"ReadReportAsync({id}) OK.");
                    }
                    else
                    {
                        _logger?.LogDebug($"ReadReportAsync({id}) not OK.");
                        Reports[index].Status = DataValue.BadUnknownResponse;
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"ReadReportAsync({id}) exception: {ex.Message}.");
                    Data.Status = DataValue.BadInternalError;
                    Reports[index].Status = DataValue.BadInternalError;
                }
            }
            else
            {
                _logger?.LogDebug($"ReadReportAsync({id}) report ID not supported.");
                Data.Status = DataValue.BadNotSupported;
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates the Wallbox properties reading all the charging report data from the Wallbox UDP service.
        /// If successful the data value will be updated (timestamp).
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadReportsAsync()
        {
            try
            {
                for (int id = REPORTS_ID + 1; id <= (REPORTS_ID + MAX_REPORTS); ++id)
                {
                    var status = await ReadReportAsync(id);

                    if (!status.IsGood)
                    {
                        _logger?.LogDebug("ReadReportsAsync not OK.");
                        return status;
                    }
                }

                Data.Status = DataValue.Good;
                _logger?.LogDebug("ReadReportsAsync OK.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadReportsAsync exception: {ex.Message}.");
                Data.Status = DataValue.BadInternalError;
            }

            return Data.Status;
        }

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
                    case "Wallbox":
                    case "Data":
                        if (parts.Length > 1)
                        {
                            return await ReadPropertyAsync(parts[1]);
                        }
                        else
                        {
                            return await ReadAllAsync();
                        }
                    case "Report1":
                        return await ReadReport1Async();
                    case "Report2":
                        return await ReadReport2Async();
                    case "Report3":
                        return await ReadReport3Async();
                    case "Report100":
                        return await ReadReport100Async();
                    case "Reports[0]":
                        return await ReadReportAsync(101);
                    case "Reports[1]":
                        return await ReadReportAsync(102);
                    case "Reports[2]":
                        return await ReadReportAsync(103);
                    case "Reports[3]":
                        return await ReadReportAsync(104);
                    case "Reports[4]":
                        return await ReadReportAsync(105);
                    case "Reports[5]":
                        return await ReadReportAsync(106);
                    case "Reports[6]":
                        return await ReadReportAsync(107);
                    case "Reports[7]":
                        return await ReadReportAsync(108);
                    case "Reports[8]":
                        return await ReadReportAsync(109);
                    case "Reports[9]":
                        return await ReadReportAsync(110);
                    case "Reports[10]":
                        return await ReadReportAsync(111);
                    case "Reports[11]":
                        return await ReadReportAsync(112);
                    case "Reports[12]":
                        return await ReadReportAsync(113);
                    case "Reports[13]":
                        return await ReadReportAsync(114);
                    case "Reports[14]":
                        return await ReadReportAsync(115);
                    case "Reports[15]":
                        return await ReadReportAsync(116);
                    case "Reports[16]":
                        return await ReadReportAsync(117);
                    case "Reports[17]":
                        return await ReadReportAsync(118);
                    case "Reports[18]":
                        return await ReadReportAsync(119);
                    case "Reports[19]":
                        return await ReadReportAsync(120);
                    case "Reports[20]":
                        return await ReadReportAsync(121);
                    case "Reports[21]":
                        return await ReadReportAsync(122);
                    case "Reports[22]":
                        return await ReadReportAsync(123);
                    case "Reports[23]":
                        return await ReadReportAsync(124);
                    case "Reports[24]":
                        return await ReadReportAsync(125);
                    case "Reports[25]":
                        return await ReadReportAsync(126);
                    case "Reports[26]":
                        return await ReadReportAsync(127);
                    case "Reports[27]":
                        return await ReadReportAsync(128);
                    case "Reports[28]":
                        return await ReadReportAsync(129);
                    case "Reports[29]":
                        return await ReadReportAsync(130);
                    case "Reports":
                        return await ReadReportsAsync();
                }
            }

            return status;
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
                    case "Wallbox":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Wallbox), parts[1]) != null : true;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(WallboxData), parts[1]) != null : true;
                    case "Report1":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Report1Data), parts[1]) != null : true;
                    case "Report2":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Report2Data), parts[1]) != null : true;
                    case "Report3":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(Report3Data), parts[1]) != null : true;
                    case "Report100":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(ReportsData), parts[1]) != null : true;
                    case "Reports":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(ReportsData), parts[1]) != null : true;
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
                    case "Wallbox":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : Data;
                    case "Report1":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Report1, parts[1]) : Report1;
                    case "Report2":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Report2, parts[1]) : Report2;
                    case "Report3":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Report3, parts[1]) : Report3;
                    case "Report100":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Report100, parts[1]) : Report100;
                    case "Reports":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Reports, parts[1]) : Reports;
                }
            }

            return null;
        }

        #endregion
    }
}
