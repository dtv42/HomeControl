// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SYMO823M.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib
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
    using SunSpecLib;
    using SYMO823MLib.Models;
    using static DataValueLib.DataValue;
    using static SYMO823MLib.Models.SYMO823MData;

    #endregion

    /// <summary>
    /// Class holding data from the Fronius Symo 8.2-3-M inverter.
    /// The value properties are based on the specification Fronius Datamanager
    /// Modbus TCP & RTU (42,0410,2049) 012-23102017
    /// </summary>
    public class SYMO823M : BaseClass, ISYMO823M
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
        /// The Data property holds all SYMO823M data properties.
        /// </summary>
        public SYMO823MData Data { get; set; } = new SYMO823MData();

        /// <summary>
        /// The CommonModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public CommonModelData CommonModel { get; } = new CommonModelData();

        /// <summary>
        /// The InverterModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public InverterModelData InverterModel { get; } = new InverterModelData();

        /// <summary>
        /// The NameplateModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public NameplateModelData NameplateModel { get; } = new NameplateModelData();

        /// <summary>
        /// The SettingsModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public SettingsModelData SettingsModel { get; } = new SettingsModelData();

        /// <summary>
        /// The ExtendedModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public ExtendedModelData ExtendedModel { get; } = new ExtendedModelData();

        /// <summary>
        /// The ControlModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public ControlModelData ControlModel { get; } = new ControlModelData();

        /// <summary>
        /// The MultipleModel property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public MultipleModelData MultipleModel { get; } = new MultipleModelData();

        /// <summary>
        /// The FroniusRegister property holds a subset of the Modbus data values.
        /// </summary>
        [JsonIgnore]
        public FroniusRegisterData FroniusRegister { get; } = new FroniusRegisterData();

        /// <summary>
        /// Flag indicating that the first update has been sucessful.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets or sets the Modbus TCP/IP master options.
        /// </summary>
        public TcpMasterData Master
        {
            get => _client.TcpMaster;
            set => _client.TcpMaster = value;
        }

        /// <summary>
        /// Gets or sets the Modbus TCP/IP slave options.
        /// </summary>
        public TcpSlaveData Slave
        {
            get => _client.TcpSlave;
            set => _client.TcpSlave = value;
        }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SYMO823M"/> class.
        /// </summary>
        /// <param name="logger">The application logger instance.</param>
        /// <param name="client">The TCP/IP client.</param>
        public SYMO823M(ILogger<SYMO823M> logger,
                       ITcpClient client)
            : base(logger)
        {
            _client = client;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Updates all properties reading the data from Fronius Symo 8.2-3-M inverter.
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
                    SYMO823MData data = new SYMO823MData();

                    foreach (var property in SYMO823MData.GetProperties())
                    {
                        if (SYMO823MData.IsReadable(property))
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
                    CommonModel.Refresh(data);
                    InverterModel.Refresh(data);
                    NameplateModel.Refresh(data);
                    SettingsModel.Refresh(data);
                    ExtendedModel.Refresh(data);
                    ControlModel.Refresh(data);
                    MultipleModel.Refresh(data);
                    FroniusRegister.Refresh(data);

                    if (status.IsGood)
                    {
                        if (IsInitialized == false) IsInitialized = true;
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
        /// Updates all properties reading the data in blocks from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadBlockAsync()
        {
            await _semaphore.WaitAsync();
            DataStatus status = DataValue.Good;

            try
            {
                if (_client.Connect())
                {
                    SYMO823MData data = new SYMO823MData
                    {
                        C001Block = await _client.ReadUShortArrayAsync(GetOffset("C001Block"), GetLength("C001Block")),
                        I113Block = await _client.ReadUShortArrayAsync(GetOffset("I113Block"), GetLength("I113Block")),
                        IC120Block = await _client.ReadUShortArrayAsync(GetOffset("IC120Block"), GetLength("IC120Block")),
                        IC121Block = await _client.ReadUShortArrayAsync(GetOffset("IC121Block"), GetLength("IC121Block")),
                        IC122Block = await _client.ReadUShortArrayAsync(GetOffset("IC122Block"), GetLength("IC122Block")),
                        IC123Block = await _client.ReadUShortArrayAsync(GetOffset("IC123Block"), GetLength("IC123Block")),
                        I160Block = await _client.ReadUShortArrayAsync(GetOffset("I160Block"), GetLength("I160Block")),
                        Register1 = await _client.ReadUShortArrayAsync(GetOffset("Register1"), GetLength("Register1")),
                        Register2 = await _client.ReadUShortArrayAsync(GetOffset("Register2"), GetLength("Register2")),

                        Status = status
                    };
                    Data.Refresh(data);
                    CommonModel.Refresh(data);
                    InverterModel.Refresh(data);
                    NameplateModel.Refresh(data);
                    SettingsModel.Refresh(data);
                    ExtendedModel.Refresh(data);
                    ControlModel.Refresh(data);
                    MultipleModel.Refresh(data);
                    FroniusRegister.Refresh(data);

                    if (status.IsGood)
                    {
                        if (IsInitialized == false) IsInitialized = true;
                        _logger?.LogDebug("BlockReadAsync OK.");
                    }
                    else
                    {
                        _logger?.LogDebug("BlockReadAsync not OK.");
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
                _logger?.LogDebug("ReadAllAsync() finished.");
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a single property reading the data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadDataAsync(string property)
        {
            DataStatus status = Good;

            if (SYMO823MData.IsProperty(property))
            {
                if (SYMO823MData.IsReadable(property))
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
                            _logger?.LogError("ReadDataAsync('{property}') not connected.");
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
                }
            }
            else
            {
                _logger?.LogDebug($"ReadDataAsync('{property}') property not found.");
                status = BadNotFound;
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a list of properties reading the data from Fronius Symo 8.2-3-M inverter.
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
                    SYMO823MData data = Data;
                    data.Status = Good;

                    foreach (var property in properties)
                    {
                        if (SYMO823MData.IsProperty(property))
                        {
                            if (SYMO823MData.IsReadable(property))
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
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"ReadDataAsync(List<property>) property '{property}' not found.");
                            status = BadNotFound;
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
                _logger?.LogError(ex, $"ReadDataAsync(List<property>).");
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
        /// Updates the CommonModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadCommonModelAsync()
        {
            var status = await ReadDataAsync(CommonModelData.GetProperties());

            if (status.IsGood)
            {
                CommonModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the InverterModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadInverterModelAsync()
        {
            var status = await ReadDataAsync(InverterModelData.GetProperties());

            if (status.IsGood)
            {
                InverterModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the NameplateModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadNameplateModelAsync()
        {
            var status = await ReadDataAsync(NameplateModelData.GetProperties());

            if (status.IsGood)
            {
                NameplateModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the SettingsModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadSettingsModelAsync()
        {
            var status = await ReadDataAsync(SettingsModelData.GetProperties());

            if (status.IsGood)
            {
                SettingsModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ExtendedModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadExtendedModelAsync()
        {
            var status = await ReadDataAsync(ExtendedModelData.GetProperties());

            if (status.IsGood)
            {
                ExtendedModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the ControlModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadControlModelAsync()
        {
            var status = await ReadDataAsync(ControlModelData.GetProperties());

            if (status.IsGood)
            {
                ControlModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the MultipleModel data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadMultipleModelAsync()
        {
            var status = await ReadDataAsync(MultipleModelData.GetProperties());

            if (status.IsGood)
            {
                MultipleModel.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the FroniusRegister data from Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> ReadFroniusRegisterAsync()
        {
            var status = await ReadDataAsync(FroniusRegisterData.GetProperties());

            if (status.IsGood)
            {
                FroniusRegister.Refresh(Data);
            }

            return status;
        }

        /// <summary>
        /// Updates the Fronius Symo 8.2-3-M inverter writing all property values.
        /// </summary>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteAllAsync()
        {
            try
            {
                if (_client.Connect())
                {
                    foreach (var property in SYMO823MData.GetProperties())
                    {
                        if (SYMO823MData.IsWritable(property))
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
        /// Updates a single data item at Fronius Symo 8.2-3-M inverter
        /// writing the data value. Note that the property is updated.
        /// </summary>
        /// <remarks>
        /// Only the following data types are supported:
        ///     ushort
        ///     uint16
        ///     int16
        /// </remarks>
        /// <param name="property">The name of the property.</param>
        /// <param name="data">The data value of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteDataAsync(string property, string data)
        {
            DataStatus status = Good;

            if (SYMO823MData.IsWritable(property))
            {
                try
                {
                    if (_client.Connect())
                    {
                        dynamic value = Data.GetPropertyValue(property);

                        switch (value)
                        {
                            case ushort u when ushort.TryParse(data, out ushort ushortData):
                                Data.SetPropertyValue(property, ushortData);
                                status = await WriteDataAsync(property);
                                break;
                            case uint16 u when UInt16.TryParse(data, out UInt16 uint16Data):
                                uint16 u16 = new uint16();
                                u16 = uint16Data;
                                Data.SetPropertyValue(property, u16);
                                status = await WriteDataAsync(property);
                                break;
                            case int16 i when Int16.TryParse(data, out Int16 int16Data):
                                int16 i16 = new int16();
                                i16 = int16Data;
                                Data.SetPropertyValue(property, i16);
                                status = await WriteDataAsync(property);
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
                }
            }
            else
            {
                _logger?.LogDebug($"WriteDataAsync invalid property '{property}'.");
                status = BadNotWritable;
            }

            Data.Status = status;
            return status;
        }

        /// <summary>
        /// Updates a single property at Fronius Symo 8.2-3-M inverter writing the property value.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <returns>The status indicating success or failure.</returns>
        public async Task<DataStatus> WriteDataAsync(string property)
        {
            DataStatus status = Good;

            if (SYMO823MData.IsWritable(property))
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
        /// Updates a list of properties from Fronius Symo 8.2-3-M inverter writing the property values.
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
                    SYMO823MData data = Data;
                    data.Status = Good;

                    foreach (var property in properties)
                    {
                        if (SYMO823MData.IsProperty(property))
                        {
                            if (SYMO823MData.IsWritable(property))
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
                    case "SYMO823M":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(SYMO823M), parts[1]) != null : true;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyInfo(typeof(SYMO823MData), parts[1]) != null : true;
                    default:
                        return PropertyValue.GetPropertyInfo(typeof(SYMO823MData), property) != null;
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
                    case "SYMO823M":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(this, parts[1]) : this;
                    case "Data":
                        return parts.Length > 1 ? PropertyValue.GetPropertyValue(Data, parts[1]) : Data;
                    default:
                        return PropertyValue.GetPropertyValue(Data, property);
                }
            }

            return null;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Helper method to read the property from the Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <remarks>
        /// Note that the additional SunSpec and SYMO data types are supported.</remarks>
        /// <param name="data">The SYMO823M data.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> ReadPropertyAsync(SYMO823MData data, string property)
        {
            DataStatus status = Good;

            try
            {
                if (SYMO823MData.IsReadable(property))
                {
                    object value = data.GetPropertyValue(property);
                    ushort offset = GetOffset(property);
                    ushort length = GetLength(property);

                    _logger?.LogDebug($"ReadAsync property '{property}' => Type: {value.GetType()}, Offset: {offset}, Length: {length}.");

                    if (value is float)
                    {
                        var floatvalue = await _client.ReadFloatAsync(offset);
                        data.SetPropertyValue(property, floatvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {floatvalue}.");
                    }
                    else if (value is UInt16)
                    {
                        var uintvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, uintvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {uintvalue}.");
                    }
                    else if (value is Int16)
                    {
                        var intvalue = await _client.ReadShortAsync(offset);
                        data.SetPropertyValue(property, intvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                    else if (value is string)
                    {
                        var stringvalue = await _client.ReadStringAsync(offset, (ushort)(2 * length));
                        data.SetPropertyValue(property, stringvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {stringvalue}.");
                    }
                    else if (value is uint16)
                    {
                        uint16 uintvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, uintvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {uintvalue}.");
                    }
                    else if (value is int16)
                    {
                        int16 intvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, intvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {intvalue}.");
                    }
                    else if (value is sunssf)
                    {
                        sunssf ssfvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, ssfvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {ssfvalue}.");
                    }
                    else if (value is DERType)
                    {
                        DERType enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is ECPConn)
                    {
                        ECPConn enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is Evt1)
                    {
                        Evt1 enumvalue = await _client.ReadHoldingRegistersAsync(offset, 2);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is Evt2)
                    {
                        Evt2 enumvalue = await _client.ReadHoldingRegistersAsync(offset, 2);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is EvtVnd1)
                    {
                        EvtVnd1 enumvalue = await _client.ReadHoldingRegistersAsync(offset, 2);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is EvtVnd2)
                    {
                        EvtVnd2 enumvalue = await _client.ReadHoldingRegistersAsync(offset, 2);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is EvtVnd3)
                    {
                        EvtVnd3 enumvalue = await _client.ReadHoldingRegistersAsync(offset, 2);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is EvtVnd4)
                    {
                        EvtVnd4 enumvalue = await _client.ReadHoldingRegistersAsync(offset, 2);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is OperatingState)
                    {
                        OperatingState enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is PVConn)
                    {
                        PVConn enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is StActCtl)
                    {
                        StActCtl enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is StorConn)
                    {
                        StorConn enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
                    }
                    else if (value is VendorState)
                    {
                        VendorState enumvalue = await _client.ReadUShortAsync(offset);
                        data.SetPropertyValue(property, enumvalue);
                        _logger?.LogDebug($"ReadAsync property '{property}' => Value: {enumvalue}.");
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
        /// Helper method to write the property to the Fronius Symo 8.2-3-M inverter.
        /// </summary>
        /// <param name="data">The SYMO823M data.</param>
        /// <param name="property">The property name.</param>
        /// <returns>The status indicating success or failure.</returns>
        private async Task<DataStatus> WritePropertyAsync(SYMO823MData data, string property)
        {
            DataStatus status = Good;

            try
            {
                if (SYMO823MData.IsWritable(property))
                {
                    dynamic value = data.GetPropertyValue(property);
                    ushort offset = GetOffset(property);
                    ushort length = GetLength(property);

                    if (value is UInt16)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        await _client.WriteUShortAsync(offset, (UInt16)value);
                    }
                    else if (value is Int16)
                    {
                        _logger?.LogDebug($"WriteAsync property '{property}' => Type: {value.GetType()}, Value: {value}, Offset: {offset}, Length: {length}.");
                        await _client.WriteShortAsync(offset, (Int16)value);
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
