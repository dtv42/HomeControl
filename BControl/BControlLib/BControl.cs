// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BControl.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlLib
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

    using BaseClassLib;
    using DataValueLib;
    using NModbusLib;
    using NModbusLib.Models;
    using SunSpecLib;
    using BControlLib.Models;
    using static DataValueLib.DataValue;
    using static BControlLib.Models.BControlData;

    #endregion

    /// <summary>
    /// Class holding data from the TQ Energy Manager unit.
    /// The value properties are based on the specification
    /// Technical note TQ Energy Manager Modbus Slave.0013 2018-01-17.
    /// </summary>
    public class BControl : BaseClass, IBControl
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

        public BControlData Data { get; set; } = new BControlData();
        [JsonIgnore]
        public InternalData InternalData { get; } = new InternalData();
        [JsonIgnore]
        public EnergyData EnergyData { get; } = new EnergyData();
        [JsonIgnore]
        public PnPData PnPData { get; } = new PnPData();
        [JsonIgnore]
        public SunSpecData SunSpecData { get; } = new SunSpecData();

        /// <summary>
        /// Returns true if no tasks can enter the semaphore.
        /// </summary>
        [JsonIgnore]
        public bool IsLocked { get => !(_semaphore.CurrentCount == 0); }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BControl"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The TCP/IP client.</param>
        public BControl(ILogger<BControl> logger,
                        ITcpClient client)
            : base(logger)
        {
            _client = client;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Synchronous methods.
        /// </summary>
        public DataStatus ReadAll() => ReadAllAsync().Result;
        public DataStatus ReadBlockAll() => ReadBlockAllAsync().Result;
        public DataStatus ReadProperty(string property) => ReadPropertyAsync(property).Result;
        public DataStatus ReadProperties(List<string> properties) => ReadPropertiesAsync(properties).Result;
        public DataStatus ReadInternalData() => ReadInternalDataAsync().Result;
        public DataStatus ReadEnergyData() => ReadEnergyDataAsync().Result;
        public DataStatus ReadPnPData() => ReadPnPDataAsync().Result;
        public DataStatus ReadSunSpecData() => ReadSunSpecDataAsync().Result;

        /// <summary>
        /// Updates all properties reading the data from TQ Energy Manager.
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
                    BControlData data = new BControlData();

                    foreach (var property in BControlData.GetProperties())
                    {
                        if (BControlData.IsReadable(property))
                        {
                            status = await ReadPropertyAsync(data, property);

                            if (status != Good)
                            {
                                _logger?.LogDebug($"ReadAllAsync('{property}') not OK.");
                            }
                        }
                    }

                    data.Status = status;
                    Data.Refresh(data);
                    InternalData.Refresh(data);
                    EnergyData.Refresh(data);
                    PnPData.Refresh(data);
                    SunSpecData.Refresh(data);

                    if (status.IsGood)
                    {
                        _logger?.LogDebug("ReadAllAsync OK.");
                    }
                    else
                    {
                        _logger?.LogDebug("ReadAllAsync not OK.");
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
        /// Updates all properties reading the data in blocks from TQ Energy Manager.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadBlockAllAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadBlockAsync() starting.");

                if (_client.Connect())
                {
                    BControlData data = new BControlData
                    {
                        InternalDataBlock1 = await _client.ReadOnlyUInt32ArrayAsync(GetOffset("InternalDataBlock1"), GetLength("InternalDataBlock1")),
                        InternalDataBlock2 = await _client.ReadOnlyUInt32ArrayAsync(GetOffset("InternalDataBlock2"), GetLength("InternalDataBlock2")),
                        EnergyDataBlock1 = await _client.ReadOnlyULongArrayAsync(GetOffset("EnergyDataBlock1"), GetLength("EnergyDataBlock1")),
                        EnergyDataBlock2 = await _client.ReadOnlyULongArrayAsync(GetOffset("EnergyDataBlock2"), GetLength("EnergyDataBlock2")),
                        EnergyDataBlock3 = await _client.ReadOnlyULongArrayAsync(GetOffset("EnergyDataBlock3"), GetLength("EnergyDataBlock3")),
                        PnPDataBlock1 = await _client.ReadOnlyUShortArrayAsync(GetOffset("PnPDataBlock1"), GetLength("PnPDataBlock1")),
                        SunSpecDataBlock1 = await _client.ReadOnlyUShortArrayAsync(GetOffset("SunSpecDataBlock1"), GetLength("SunSpecDataBlock1")),
                        SunSpecDataBlock2 = await _client.ReadOnlyUShortArrayAsync(GetOffset("SunSpecDataBlock2"), GetLength("SunSpecDataBlock2")),

                        Status = status
                    };
                    Data.Refresh(data);
                    InternalData.Refresh(data);
                    EnergyData.Refresh(data);
                    PnPData.Refresh(data);
                    SunSpecData.Refresh(data);

                    if (status.IsGood)
                    {
                        _logger?.LogDebug("ReadBlockAsync OK.");
                    }
                    else
                    {
                        _logger?.LogDebug("ReadBlockAsync not OK.");
                    }
                }
                else
                {
                    _logger?.LogError("ReadBlockAsync not connected.");
                    status = BadNotConnected;
                }
            }
            catch (ArgumentNullException anx)
            {
                _logger?.LogError(anx, "ArgumentNullException in ReadBlockAsync.");
                status = BadOutOfRange;
                status.Explanation = $"Exception: {anx.Message}";
            }
            catch (ArgumentOutOfRangeException aor)
            {
                _logger?.LogError(aor, "ArgumentOutOfRangeException in ReadBlockAsync.");
                status = BadOutOfRange;
                status.Explanation = $"Exception: {aor.Message}";
            }
            catch (ArgumentException aex)
            {
                _logger?.LogError(aex, "ArgumentException in ReadBlockAsync.");
                status = BadOutOfRange;
                status.Explanation = $"Exception: {aex.Message}";
            }
            catch (ObjectDisposedException odx)
            {
                _logger?.LogError(odx, "ObjectDisposedException in ReadBlockAsync.");
                status = BadInternalError;
                status.Explanation = $"Exception: {odx.Message}";
            }
            catch (FormatException fex)
            {
                _logger?.LogError(fex, "FormatException in ReadBlockAsync.");
                status = BadEncodingError;
                status.Explanation = $"Exception: {fex.Message}";
            }
            catch (IOException iox)
            {
                _logger?.LogError(iox, "IOException in ReadBlockAsync.");
                status = BadCommunicationError;
                status.Explanation = $"Exception: {iox.Message}";
            }
            catch (InvalidModbusRequestException imr)
            {
                _logger?.LogError(imr, "InvalidModbusRequestException in ReadBlockAsync.");
                status = BadCommunicationError;
                status.Explanation = $"Exception: {imr.Message}";
            }
            catch (InvalidOperationException iox)
            {
                _logger?.LogError(iox, "InvalidOperationException in ReadBlockAsync.");
                status = BadInternalError;
                status.Explanation = $"Exception: {iox.Message}";
            }
            catch (SlaveException slx)
            {
                _logger?.LogError(slx, "SlaveException in ReadBlockAsync.");
                status = BadDeviceFailure;
                status.Explanation = $"Exception: {slx.Message}";
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Exception in ReadBlockAsync.");
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
        /// Updates a single property reading the data from TQ Energy Manager.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadPropertyAsync(string property)
        {
            DataStatus status = DataValue.Good;
            _logger?.LogDebug($"ReadPropertyAsync('{property}') starting.");

            if (BControlData.IsProperty(property))
            {
                if (BControlData.IsReadable(property))
                {
                    await _semaphore.WaitAsync();

                    try
                    {
                        if (_client.Connect())
                        {
                            status = await ReadPropertyAsync(Data, property);

                            if (status.IsGood)
                            {
                                _logger?.LogDebug($"ReadPropertyAsync('{property}') OK.");
                            }
                            else
                            {
                                _logger?.LogDebug($"ReadPropertyAsync('{property}') not OK.");
                            }
                        }
                        else
                        {
                            _logger?.LogError($"ReadPropertyAsync('{property}') not connected.");
                            status = BadNotConnected;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, $"ReadPropertyAsync('{property}') exception: {ex.Message}.");
                        status = BadInternalError;
                        status.Explanation = $"Exception: {ex.Message}";
                    }
                    finally
                    {
                        _client.Disconnect();
                        _semaphore.Release();
                        _logger?.LogDebug($"ReadPropertyAsync('{property}') finished.");
                    }
                }
                else
                {
                    _logger?.LogDebug($"ReadPropertyAsync('{property}') property not readable.");
                    status = BadNotReadable;
                    status.Explanation = $"Property '{property}' not readable.";
                }
            }
            else
            {
                _logger?.LogDebug($"ReadPropertyAsync('{property}') property not found.");
                status = BadNotFound;
                status.Explanation = $"Property '{property}' not found.";
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a list of properties reading the data from TQ Energy Manager.
        /// </summary>
        /// <param name="properties">The list of the property names.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadPropertiesAsync(List<string> properties)
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                _logger?.LogDebug("ReadPropertiesAsync(List<property>) starting.");

                if (_client.Connect())
                {
                    BControlData data = Data;
                    data.Status = Good;

                    foreach (var property in properties)
                    {
                        if (BControlData.IsProperty(property))
                        {
                            if (BControlData.IsReadable(property))
                            {
                                status = await ReadPropertyAsync(data, property);

                                if (status.IsGood)
                                {
                                    _logger?.LogDebug($"ReadPropertiesAsync(List<property>) property '{property}' OK.");
                                }
                                else
                                {
                                    _logger?.LogDebug($"ReadPropertiesAsync(List<property>) property '{property}' not OK.");
                                }
                            }
                            else
                            {
                                _logger?.LogDebug($"ReadPropertiesAsync(List<property>) property '{property}' not readable.");
                                status = BadNotReadable;
                                status.Explanation = $"Property '{property}' not readable.";
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"ReadPropertiesAsync(List<property>) property '{property}' not found.");
                            status = BadNotFound;
                            status.Explanation = $"Property '{property}' not found.";
                        }
                    }

                    Data = data;

                    if ((data.IsGood) && (status.IsGood))
                    {
                        _logger?.LogDebug("ReadPropertiesAsync(List<property>) OK.");
                    }
                    else
                    {
                        _logger?.LogDebug("ReadPropertiesAsync(List<property>) not OK.");
                    }
                }
                else
                {
                    _logger?.LogError("ReadPropertiesAsync(List<property>) not connected.");
                    status = BadNotConnected;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ReadPropertiesAsync(List<property>).");
                status = BadInternalError;
                status.Explanation = $"Exception: {ex.Message}";
            }
            finally
            {
                _client.Disconnect();
                _semaphore.Release();
                _logger?.LogDebug("ReadPropertiesAsync(List<property>) finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates the Energy Manager Internal register data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadInternalDataAsync()
        {
            var status = await ReadPropertiesAsync(InternalData.GetProperties());

            if (status.IsGood)
            {
                InternalData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the TQ Energy Manager Energy register data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadEnergyDataAsync()
        {
            var status = await ReadPropertiesAsync(EnergyData.GetProperties());

            if (status.IsGood)
            {
                EnergyData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the TQ Energy Manager PnP register Data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadPnPDataAsync()
        {
            var status = await ReadPropertiesAsync(PnPData.GetProperties());

            if (status.IsGood)
            {
                PnPData.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the TQ Energy Manager SunSpec register data.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStatus> ReadSunSpecDataAsync()
        {
            var status = await ReadPropertiesAsync(SunSpecData.GetProperties());

            if (status.IsGood)
            {
                SunSpecData.Refresh(Data);
            }

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
                    case "BControl":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(BControl), parts[1]) != null : true;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(BControlData), parts[1]) != null : true;
                    case "InternalData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(InternalData), parts[1]) != null : true;
                    case "EnergyData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(EnergyData), parts[1]) != null : true;
                    case "PnPData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(PnPData), parts[1]) != null : true;
                    case "SunSpecData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(SunSpecData), parts[1]) != null : true;
                    default:
                        return PropertyValue.GetPropertyInfo(typeof(BControlData), property) != null;
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
                    case "BControl":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : Data;
                    case "InternalData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(InternalData, parts[1]) : InternalData;
                    case "EnergyData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(EnergyData, parts[1]) : EnergyData;
                    case "PnPData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(PnPData, parts[1]) : PnPData;
                    case "SunSpecData":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(SunSpecData, parts[1]) : SunSpecData;
                    default:
                        return PropertyValue.GetPropertyValue(Data, property);
                }
            }

            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper method to read the property from the TQ energy meter.
        /// </summary>
        /// <param name="data">The BControl data.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> ReadPropertyAsync(BControlData data, string property)
        {
            DataStatus status = Good;

            try
            {
                if (BControlData.IsReadable(property))
                {
                    object value = data.GetPropertyValue(property);
                    ushort offset = GetOffset(property);
                    ushort length = GetLength(property);

                    _logger?.LogDebug($"ReadAsync property '{property}' => Type: {value.GetType()}, Offset: {offset}, Length: {length}.");

                    if (value is UInt16)
                    {
                        value = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, value);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    if (value is Int16)
                    {
                        value = await _client.ReadShortAsync(offset);
                        data.SetPropertyValue(property, value);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is UInt32)
                    {
                        value = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, value);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is Int32)
                    {
                        value = await _client.ReadInt32Async(offset);
                        data.SetPropertyValue(property, value);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is string)
                    {
                        value = await _client.ReadStringAsync(offset, (ushort)(length * 2));
                        data.SetPropertyValue(property, value);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is uint16)
                    {
                        uint16 result = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, result);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is uint32)
                    {
                        uint32 result = await _client.ReadUInt32Async(offset);
                        data.SetPropertyValue(property, result);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is uint64)
                    {
                        uint64 result = await _client.ReadULongAsync(offset);
                        data.SetPropertyValue(property, result);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is int16)
                    {
                        int16 result = await _client.ReadShortAsync(offset);
                        data.SetPropertyValue(property, result);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is int32)
                    {
                        int32 result = await _client.ReadInt32Async(offset);
                        data.SetPropertyValue(property, result);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
                    }
                    else if (value is sunssf)
                    {
                        sunssf result = await _client.ReadShortAsync(offset);
                        data.SetPropertyValue(property, result);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {value}.");
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

        #endregion Private Methods
    }
}
