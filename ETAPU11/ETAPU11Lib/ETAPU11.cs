// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Lib
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    using NModbus;

    using NModbusLib;
    using NModbusLib.Models;

    using BaseClassLib;
    using DataValueLib;
    using ETAPU11Lib.Models;
    using static DataValueLib.DataValue;
    using static ETAPU11Lib.Models.ETAPU11Data;

    #endregion

    /// <summary>
    /// Class holding data from the ETA PU 11 pellet boiler unit.
    /// The value properties are based on the specification ETAtouch Modbus/TCP interface
    /// Version 1.0 ETA Heiztechnik GmbH February 25, 2014
    /// </summary>
    public class ETAPU11 : BaseClass, IETAPU11
    {
        #region Private Fields

        /// <summary>
        /// The Modbus TCP/IP client instance.
        /// </summary>
        private readonly ITcpClient _client;

        /// <summary>
        /// Instantiate a Singleton of the Semaphore with a value of 1.
        /// This means that only 1 thread can be granted access at a time.
        /// </summary>
        static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// The Data property holds all ETAPU11 data properties.
        /// </summary>
        public ETAPU11Data Data { get; set; } = new ETAPU11Data();

        /// <summary>
        /// The BoilerData property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public BoilerData BoilerData { get; } = new BoilerData();

        /// <summary>
        /// The HotwaterData property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public HotwaterData HotwaterData { get; } = new HotwaterData();

        /// <summary>
        /// The HeatingData property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public HeatingData HeatingData { get; } = new HeatingData();

        /// <summary>
        /// The StorageData property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public StorageData StorageData { get; } = new StorageData();

        /// <summary>
        /// The SystemData property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public SystemData SystemData { get; } = new SystemData();

        /// <summary>
        /// Flag indicating that the first update has been sucessful.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets or sets the Modbus TCP/IP master options.
        /// </summary>
        public TcpMasterData TcpMaster
        {
            get => _client.TcpMaster;
            set => _client.TcpMaster = value;
        }

        /// <summary>
        /// Gets or sets the Modbus TCP/IP slave options.
        /// </summary>
        public TcpSlaveData TcpSlave
        {
            get => _client.TcpSlave;
            set => _client.TcpSlave = value;
        }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ETAPU11"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The TCP/IP client.</param>
        public ETAPU11(ILogger<ETAPU11> logger,
                       ITcpClient client)
            : base(logger)
        {
            _client = client;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Updates all properties reading the data from ETA PU 11 pellet boiler unit.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadAllAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadAllAsync() starting.");

                if (_client.Connect())
                {
                    ETAPU11Data data = new ETAPU11Data();

                    foreach (var property in ETAPU11Data.GetProperties())
                    {
                        if (ETAPU11Data.IsReadable(property))
                        {
                            status = await ReadPropertyAsync(data, property);

                            if (!status.IsGood)
                            {
                                _logger?.LogDebug($"ReadAllAsync('{property}') not OK.");
                            }
                        }
                    }

                    data.Status = status;
                    Data.Refresh(data);
                    BoilerData.Refresh(data);
                    HotwaterData.Refresh(data);
                    HeatingData.Refresh(data);
                    StorageData.Refresh(data);
                    SystemData.Refresh(data);

                    if (status.IsGood)
                    {
                        if (!IsInitialized)
                        {
                            IsInitialized = true;
                        }

                        _logger?.LogDebug($"ReadAllAsync OK.");
                    }
                    else
                    {
                        _logger?.LogDebug($"ReadAllAsync not OK: {status}.");
                    }
                }
                else
                {
                    _logger?.LogError("ReadAllAsync not connected.");
                    status = BadNotConnected;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadAllAsync exception.");
                status = BadInternalError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _client.Disconnect();
                _semaphore.Release();
                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates all properties reading the data in blocks from ETA PU 11 pellet boiler unit.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadBlockAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadBlockAsync() starting.");

                if (_client.Connect())
                {
                    ETAPU11Data data = new ETAPU11Data
                    {
                        DataBlock1 = await _client.ReadUInt32ArrayAsync(GetOffset("DataBlock1"), GetLength("DataBlock1")),
                        DataBlock2 = await _client.ReadUInt32ArrayAsync(GetOffset("DataBlock2"), GetLength("DataBlock2")),

                        Status = status
                    };
                    Data.Refresh(data);
                    BoilerData.Refresh(data);
                    HotwaterData.Refresh(data);
                    HeatingData.Refresh(data);
                    StorageData.Refresh(data);
                    SystemData.Refresh(data);

                    if (status.IsGood)
                    {
                        if (IsInitialized == false) IsInitialized = true;
                        _logger?.LogDebug("BlockReadAsync OK.");
                    }
                    else
                    {
                        _logger?.LogDebug($"BlockReadAsync not OK: {status}.");
                    }
                }
                else
                {
                    _logger?.LogError("BlockReadAsync not connected.");
                    status = BadNotConnected;
                }
            }
            catch (ArgumentNullException anx)
            {
                _logger?.LogError(anx, "ArgumentNullException in BlockReadAsync.");
                status = BadOutOfRange;
                status.Explanation = $"Exception: {anx.Message}";
            }
            catch (ArgumentOutOfRangeException aor)
            {
                _logger?.LogError(aor, "ArgumentOutOfRangeException in BlockReadAsync.");
                status = BadOutOfRange;
                status.Explanation = $"Exception: {aor.Message}";
            }
            catch (ArgumentException aex)
            {
                _logger?.LogError(aex, "ArgumentException in BlockReadAsync.");
                status = BadOutOfRange;
                status.Explanation = $"Exception: {aex.Message}";
            }
            catch (ObjectDisposedException odx)
            {
                _logger?.LogError(odx, "ObjectDisposedException in BlockReadAsync.");
                status = BadInternalError;
                status.Explanation = $"Exception: {odx.Message}";
            }
            catch (FormatException fex)
            {
                _logger?.LogError(fex, "FormatException in BlockReadAsync.");
                status = BadEncodingError;
                status.Explanation = $"Exception: {fex.Message}";
            }
            catch (IOException iox)
            {
                _logger?.LogError(iox, "IOException in BlockReadAsync.");
                status = BadCommunicationError;
                status.Explanation = $"Exception: {iox.Message}";
            }
            catch (InvalidModbusRequestException imr)
            {
                _logger?.LogError(imr, "InvalidModbusRequestException in BlockReadAsync.");
                status = BadCommunicationError;
                status.Explanation = $"Exception: {imr.Message}";
            }
            catch (InvalidOperationException iox)
            {
                _logger?.LogError(iox, "InvalidOperationException in BlockReadAsync.");
                status = BadInternalError;
                status.Explanation = $"Exception: {iox.Message}";
            }
            catch (SlaveException slx)
            {
                _logger?.LogError(slx, "SlaveException in BlockReadAsync.");
                status = BadDeviceFailure;
                status.Explanation = $"Exception: {slx.Message}";
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in BlockReadAsync.");
                status = BadInternalError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _client.Disconnect();
                _semaphore.Release();
                _logger?.LogDebug("ReadBlockAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a single property reading the data from ETA PU 11 pellet boiler unit.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadDataAsync(string property)
        {
            DataStatus status = Good;

            if (ETAPU11Data.IsProperty(property))
            {
                if (ETAPU11Data.IsReadable(property))
                {
                    try
                    {
                        if (_client.Connect())
                        {
                            status = await ReadPropertyAsync(Data, property);

                            if (status.IsGood)
                            {
                                _logger?.LogDebug($"ReadDataAsync('{property}') OK.");
                            }
                            else
                            {
                                _logger?.LogDebug($"ReadDataAsync('{property}') not OK.");
                            }
                        }
                        else
                        {
                            _logger?.LogError($"ReadDataAsync('{property}') not connected.");
                            status = BadNotConnected;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, $"ReadDataAsync('{property}') exception: {ex.Message}.");
                        status = BadInternalError;
                        status.Explanation = $"Exception: {ex.Message}";
                    }
                    finally
                    {
                        _client.Disconnect();
                    }
                }
                else
                {
                    _logger?.LogDebug($"ReadDataAsync('{property}') property not readable.");
                    status = BadNotReadable;
                    status.Explanation = $"Property '{property}' not readable.";
                }
            }
            else
            {
                _logger?.LogDebug($"ReadDataAsync('{property}') property not found.");
                status = BadNotFound;
                status.Explanation = $"Property '{property}' not found.";
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a list of properties reading the data from ETA PU 11 pellet boiler unit.
        /// </summary>
        /// <param name="properties">The list of the property names.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadDataAsync(List<string> properties)
        {
            DataStatus status = Good;

            try
            {
                if (_client.Connect())
                {
                    ETAPU11Data data = Data;
                    data.Status = Good;

                    foreach (var property in properties)
                    {
                        if (ETAPU11Data.IsProperty(property))
                        {
                            if (ETAPU11Data.IsReadable(property))
                            {
                                status = await ReadPropertyAsync(data, property);

                                if (status.IsGood)
                                {
                                    _logger?.LogDebug($"ReadDataAsync(List<property>) property '{property}' OK.");
                                }
                                else
                                {
                                    _logger?.LogDebug($"ReadDataAsync(List<property>) property '{property}' not OK.");
                                }
                            }
                            else
                            {
                                _logger?.LogDebug($"ReadDataAsync(List<property>) property '{property}' not readable.");
                                status = BadNotReadable;
                                status.Explanation = $"Property '{property}' not readable.";
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"ReadDataAsync(List<property>) property '{property}' not found.");
                            status = BadNotFound;
                            status.Explanation = $"Property '{property}' not found.";
                        }
                    }

                    Data = data;

                    if ((data.IsGood) && (status.IsGood))
                    {
                        _logger?.LogDebug("ReadDataAsync(List<property>) OK.");
                    }
                    else
                    {
                        _logger?.LogDebug("ReadDataAsync(List<property>) not OK.");
                    }
                }
                else
                {
                    _logger?.LogError("ReadDataAsync(List<property>) not connected.");
                    status = BadNotConnected;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadDataAsync(List<property>) exception: {ex.Message}.");
                status = BadInternalError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _client.Disconnect();
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the ETA PU 11 pellet boiler unit Boiler Data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadBoilerDataAsync()
        {
            var status = await ReadDataAsync(BoilerData.GetProperties());

            if (status.IsGood)
            {
                BoilerData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ETA PU 11 pellet boiler unit Hot Water Data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadHotwaterDataAsync()
        {
            var status = await ReadDataAsync(HotwaterData.GetProperties());

            if (status.IsGood)
            {
                HotwaterData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ETA PU 11 pellet boiler unit Heating Data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadHeatingDataAsync()
        {
            var status = await ReadDataAsync(HeatingData.GetProperties());

            if (status.IsGood)
            {
                HeatingData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ETA PU 11 pellet boiler unit Storage Data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadStorageDataAsync()
        {
            var status = await ReadDataAsync(StorageData.GetProperties());

            if (status.IsGood)
            {
                StorageData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ETA PU 11 pellet boiler unit System Data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadSystemDataAsync()
        {
            var status = await ReadDataAsync(SystemData.GetProperties());

            if (status.IsGood)
            {
                SystemData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ETA PU 11 pellet boiler unit writing all property values.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteAllAsync()
        {
            try
            {
                if (_client.Connect())
                {
                    foreach (var property in ETAPU11Data.GetProperties())
                    {
                        if (ETAPU11Data.IsWritable(property))
                        {
                            Data.Status = await WritePropertyAsync(Data, property);
                        }
                    }

                    _logger?.LogDebug("WriteAllAsync OK.");
                }
                else
                {
                    _logger?.LogError("WriteAllAsync not connected.");
                    Data.Status = BadNotConnected;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"WriteAllAsync exception: {ex.Message}.");
                Data.Status = BadInternalError;
                Data.Status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _client.Disconnect();
            }

            return Data.Status;
        }

        /// <summary>
        /// Updates a single data item at ETA PU 11 pellet boiler unit
        /// writing the data value. Note that the property is updated.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <param name="data">The data value of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteDataAsync(string property, string data)
        {
            DataStatus status = Good;

            if (ETAPU11Data.IsWritable(property))
            {
                try
                {
                    if (_client.Connect())
                    {
                        dynamic value = Data.GetPropertyValue(property);

                        switch (value)
                        {
                            case double d when double.TryParse(data, out double doubleData):
                                Data.SetPropertyValue(property, doubleData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case UInt32 u when UInt32.TryParse(data, out UInt32 uint32Data):
                                Data.SetPropertyValue(property, uint32Data);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case TimeSpan t when TimeSpan.TryParse(data, out TimeSpan timeData):
                                Data.SetPropertyValue(property, timeData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case DateTimeOffset o when DateTimeOffset.TryParse(data, out DateTimeOffset dateData):
                                Data.SetPropertyValue(property, dateData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case BoilerStates boilerstates when Enum.TryParse<BoilerStates>(data, true, out BoilerStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case FlowControlStates flowcontrolstates when Enum.TryParse<FlowControlStates>(data, true, out FlowControlStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case DiverterValveStates divertervalvestates when Enum.TryParse<DiverterValveStates>(data, true, out DiverterValveStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case DemandValues demandvalues when Enum.TryParse<DemandValues>(data, true, out DemandValues enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case DemandValuesEx demandvaluesxx when Enum.TryParse<DemandValuesEx>(data, true, out DemandValuesEx enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case FlowMixValveStates flowmixvalvestates when Enum.TryParse<FlowMixValveStates>(data, true, out FlowMixValveStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case ScrewStates screwstates when Enum.TryParse<ScrewStates>(data, true, out ScrewStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case AshRemovalStates ashremovalstates when Enum.TryParse<AshRemovalStates>(data, true, out AshRemovalStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case HopperStates hopperstates when Enum.TryParse<HopperStates>(data, true, out HopperStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case StartValues startvalues when Enum.TryParse<StartValues>(data, true, out StartValues enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case VacuumStates vacuumstates when Enum.TryParse<VacuumStates>(data, true, out VacuumStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case OnOffStates onoffstates when Enum.TryParse<OnOffStates>(data, true, out OnOffStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case HWTankStates hwtankstates when Enum.TryParse<HWTankStates>(data, true, out HWTankStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case HeatingCircuitStates heatingcircuitstates when Enum.TryParse<HeatingCircuitStates>(data, true, out HeatingCircuitStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case HWRunningStates hwrunningstates when Enum.TryParse<HWRunningStates>(data, true, out HWRunningStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case ConveyingSystemStates conveyingsystemstates when Enum.TryParse<ConveyingSystemStates>(data, true, out ConveyingSystemStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            case FirebedStates firebedstates when Enum.TryParse<FirebedStates>(data, true, out FirebedStates enumData):
                                Data.SetPropertyValue(property, enumData);
                                status = await WritePropertyAsync(Data, property);
                                break;
                            default:
                                _logger?.LogDebug($"WriteDataAsync {data} to '{property}' not OK.");
                                status = BadEncodingError;
                                break;
                        }

                        if (status.IsGood)
                        {
                            _logger?.LogDebug($"WriteDataAsync {data} to '{property}' OK.");
                        }
                        else
                        {
                            _logger?.LogDebug($"WriteDataAsync {data} to '{property}' not OK.");
                        }
                    }
                    else
                    {
                        _logger?.LogError("WriteDataAsync not connected.");
                        Data.Status = BadNotConnected;
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"WriteDataAsync exception: {ex.Message}.");
                    status = BadInternalError;
                    status.Explanation = $"Exception: {ex.Message}";
                }
                finally
                {
                    _client.Disconnect();
                }
            }
            else
            {
                _logger?.LogDebug($"WriteDataAsync invalid property '{property}'.");
                status = BadNotWritable;
                status.Explanation = $"Property '{property}' not writable.";
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a single property at ETA PU 11 pellet boiler unit writing the property value.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteDataAsync(string property)
        {
            DataStatus status = Good;

            if (ETAPU11Data.IsWritable(property))
            {
                try
                {
                    if (_client.Connect())
                    {
                        status = await WritePropertyAsync(Data, property);

                        if (status.IsGood)
                        {
                            _logger?.LogDebug($"WriteDataAsync '{property}' OK.");
                        }
                        else
                        {
                            _logger?.LogDebug($"WriteDataAsync '{property}' not OK.");
                        }
                    }
                    else
                    {
                        _logger?.LogError("WriteDataAsync not connected.");
                        status = BadNotConnected;
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"WriteDataAsync exception: {ex.Message}.");
                    status = BadInternalError;
                    status.Explanation = $"Exception: {ex.Message}";
                }
                finally
                {
                    _client.Disconnect();
                }
            }
            else
            {
                _logger?.LogDebug($"WriteDataAsync invalid property '{property}'.");
                status = BadNotWritable;
                status.Explanation = $"Property '{property}' not writable.";
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a list of properties from ETA PU 11 pellet boiler unit writing the property values.
        /// </summary>
        /// <param name="properties">The list of the property names.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteDataAsync(List<string> properties)
        {
            DataStatus status = Good;

            try
            {
                if (_client.Connect())
                {
                    ETAPU11Data data = Data;
                    data.Status = Good;

                    foreach (var property in properties)
                    {
                        if (ETAPU11Data.IsProperty(property))
                        {
                            if (ETAPU11Data.IsWritable(property))
                            {
                                status = await WritePropertyAsync(Data, property);

                                if (status.IsGood)
                                {
                                    _logger?.LogDebug($"WriteDataAsync(List<property>) to '{property}' OK.");
                                }
                                else
                                {
                                    _logger?.LogDebug($"WriteDataAsync(List<property>) to '{property}' not OK.");
                                }
                            }
                            else
                            {
                                _logger?.LogDebug($"WriteDataAsync(List<property>) property '{property}' not writeable.");
                                status = BadNotReadable;
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"WriteDataAsync(List<property>) property '{property}' not found.");
                            status = BadNotFound;
                        }
                    }

                    if ((data.IsGood) && (status.IsGood))
                    {
                        _logger?.LogDebug("WriteDataAsync(List<property>) OK.");
                    }
                    else
                    {
                        _logger?.LogDebug("WriteDataAsync(List<property>) not OK.");
                    }
                }
                else
                {
                    _logger?.LogError("WriteDataAsync(List<property>) not connected.");
                    status = BadNotConnected;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"WriteDataAsync(List<property>) exception: {ex.Message}.");
                status = BadInternalError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _client.Disconnect();
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Tries to connect to the Modbus TCP/IP slave.
        /// </summary>
        /// <returns>The boolean flag indicating success or failure.</returns>
        public bool Connect()
        {
            try
            {
                if (_client.Connect())
                {
                    _logger?.LogDebug($"ConnectAsync OK.");
                    return true;
                }
                else
                {
                    _logger?.LogDebug($"ConnectAsync not OK.");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "ConnectAsync exception.");
                Data.Status = DataValue.BadInternalError;
                Data.Status.Explanation = $"Exception: {ex.Message}";
            }

            return false;
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
                    case "ETAPU11":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(ETAPU11), parts[1]) != null : true;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(ETAPU11Data), parts[1]) != null : true;
                    case "BoilerData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(BoilerData), parts[1]) != null : true;
                    case "HotwaterData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(HotwaterData), parts[1]) != null : true;
                    case "HeatingData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(HeatingData), parts[1]) != null : true;
                    case "StorageData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(StorageData), parts[1]) != null : true;
                    case "SystemData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(SystemData), parts[1]) != null : true;
                    default:
                        return PropertyValue.GetPropertyInfo(typeof(ETAPU11Data), property) != null;
                }
            }

            return false;
        }

        /// <summary>
        /// Method to get the property value by name.
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
                    case "ETAPU11":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : Data;
                    case "BoilerData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(BoilerData, parts[1]) : BoilerData;
                    case "HotwaterData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(HotwaterData, parts[1]) : HotwaterData;
                    case "HeatingData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(HeatingData, parts[1]) : HeatingData;
                    case "StorageData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(StorageData, parts[1]) : StorageData;
                    case "SystemData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(SystemData, parts[1]) : SystemData;
                    default:
                        return PropertyValue.GetPropertyValue(Data, property);
                }
            }

            return null;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Helper method to read the property from the ETAPU11 boiler.
        /// </summary>
        /// <param name="data">The ETAPU11 data.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> ReadPropertyAsync(ETAPU11Data data, string property)
        {
            DataStatus status = Good;

            try
            {
                if (ETAPU11Data.IsReadable(property))
                {
                    object value = data.GetPropertyValue(property);
                    ushort offset = GetOffset(property);
                    ushort length = GetLength(property);
                    ushort scale = GetScale(property);

                    _logger?.LogDebug($"ReadAsync property '{property}' => Type: {value.GetType()}, Offset: {offset}, Length: {length}.");

                    if (value is double)
                    {
                        var intvalue = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, data.GetDoubleValue(property, intvalue));
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                    else if (value is UInt32)
                    {
                        var intvalue = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, intvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                    else if (value is TimeSpan)
                    {
                        var intvalue = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, data.GetTimeSpanValue(property, intvalue));
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                    else if (value is DateTimeOffset)
                    {
                        var intvalue = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, data.GetDateTimeOffsetValue(property, intvalue));
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                    else if (((dynamic)value).GetType().IsEnum)
                    {
                        var intvalue = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, Enum.ToObject(((dynamic)value).GetType(), intvalue));
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                }
                else
                {
                    _logger?.LogDebug($"ReadAsync property '{property}' not readable.");
                    status = BadNotReadable;
                }
            }
            catch (ArgumentNullException anx)
            {
                _logger?.LogError(anx, $"ArgumentNullException in ReadAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                _logger?.LogError(aor, $"ArgumentOutOfRangeException in ReadAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentException aex)
            {
                _logger?.LogError(aex, $"ArgumentException in ReadAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ObjectDisposedException odx)
            {
                _logger?.LogError(odx, $"ObjectDisposedException in ReadAsync property '{property}'.");
                status = BadInternalError;
            }
            catch (FormatException fex)
            {
                _logger?.LogError(fex, $"FormatException in ReadAsync property '{property}'.");
                status = BadEncodingError;
            }
            catch (IOException iox)
            {
                _logger?.LogError(iox, $"IOException in ReadAsync property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidModbusRequestException imr)
            {
                _logger?.LogError(imr, $"InvalidModbusRequestException in ReadAsync property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidOperationException iox)
            {
                _logger?.LogError(iox, $"InvalidOperationException in ReadAsync property '{property}'.");
                status = BadInternalError;
            }
            catch (SlaveException slx)
            {
                _logger?.LogError(slx, $"SlaveException in ReadAsync property '{property}'.");
                status = BadDeviceFailure;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in ReadAsync property '{property}'.");
                status = BadInternalError;
            }

            return status;
        }

        /// <summary>
        /// Helper method to write the property to the ETAPU11 boiler.
        /// </summary>
        /// <param name="data">The ETAPU11 data.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> WritePropertyAsync(ETAPU11Data data, string property)
        {
            DataStatus status = Good;

            try
            {
                if (ETAPU11Data.IsWritable(property))
                {
                    dynamic value = data.GetPropertyValue(property);
                    ushort offset = GetOffset(property);
                    ushort length = GetLength(property);
                    ushort scale = GetScale(property);

                    if (value is double)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        uint intvalue = data.GetUInt32Value(property, (double)value);
                        await _client.WriteUInt32Async(offset, intvalue);
                    }
                    else if (value is UInt32)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        await _client.WriteUInt32Async(offset, (UInt32)value);
                    }
                    else if (value is TimeSpan)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        uint intvalue = data.GetUInt32Value(property, (TimeSpan)value);
                        await _client.WriteUInt32Async(offset, intvalue);
                    }
                    else if (value is DateTimeOffset)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        uint intvalue = data.GetUInt32Value(property, (DateTimeOffset)value);
                        await _client.WriteUInt32Async(offset, intvalue);
                    }
                    else if (value.GetType().IsEnum)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        await _client.WriteUInt32Async(offset, (uint)value);
                    }
                }
                else
                {
                    _logger?.LogDebug($"WriteAsync property '{property}' not writable.");
                    status = BadNotWritable;
                }
            }
            catch (ArgumentNullException anx)
            {
                _logger?.LogError(anx, $"ArgumentNullException in WriteAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentOutOfRangeException aor)
            {
                _logger?.LogError(aor, $"ArgumentOutOfRangeException in WriteAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ArgumentException aex)
            {
                _logger?.LogError(aex, $"ArgumentException in WriteAsync property '{property}'.");
                status = BadOutOfRange;
            }
            catch (ObjectDisposedException odx)
            {
                _logger?.LogError(odx, $"ObjectDisposedException in WriteAsync property '{property}'.");
                status = BadInternalError;
            }
            catch (FormatException fex)
            {
                _logger?.LogError(fex, $"FormatException in WriteAsync property '{property}'.");
                status = BadEncodingError;
            }
            catch (IOException iox)
            {
                _logger?.LogError(iox, $"IOException in WriteAsync property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidModbusRequestException imr)
            {
                _logger?.LogError(imr, $"InvalidModbusRequestException in WriteAsync property '{property}'.");
                status = BadCommunicationError;
            }
            catch (InvalidOperationException iox)
            {
                _logger?.LogError(iox, $"InvalidOperationException in WriteAsync property '{property}'.");
                status = BadInternalError;
            }
            catch (SlaveException slx)
            {
                _logger?.LogError(slx, $"SlaveException in WriteAsync property '{property}'.");
                status = BadDeviceFailure;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception in WriteAsync property '{property}'.");
                status = BadInternalError;
            }

            return status;
        }

        #endregion Private Methods
    }
}
