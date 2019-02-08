// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataValue.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueLib
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    #endregion Using Directives

    /// <summary>
    /// This class provides the basic status and value properties for a remote data value.
    /// The supported status codes are preset for a predetermined set of status information.
    /// </summary>
    public class DataValue : IDataValue, INotifyPropertyChanged
    {
        #region Private Data Members

        /// <summary>
        /// Internal fields.
        /// </summary>
        private DataStatus _status = Uncertain;

        private DateTimeOffset _timestamp = new DateTimeOffset(DateTime.UtcNow);

        #endregion Private Data Members

        #region Static Properties

        /// <summary>
        /// The supported various status informations (code, name, and explanation).
        /// </summary>
        public static DataStatus Good { get; } = new DataStatus(DataStatus.Good, "Good", "The operation completed successfully.");

        public static DataStatus Uncertain { get; } = new DataStatus(DataStatus.Uncertain, "Uncertain", "The status is uncertain and the value is not necessarily set.");
        public static DataStatus Bad { get; } = new DataStatus(DataStatus.Bad, "Bad", "The operation failed.");
        public static DataStatus BadUnexpectedError { get; } = new DataStatus(DataStatus.BadUnexpectedError, "BadUnexpectedError", "An unexpected error occurred.");
        public static DataStatus BadInternalError { get; } = new DataStatus(DataStatus.BadInternalError, "BadInternalError", "An internal error occurred as a result of a programming or configuration error.");
        public static DataStatus BadOutOfMemory { get; } = new DataStatus(DataStatus.BadOutOfMemory, "BadOutOfMemory", "Not enough memory to complete the operation.");
        public static DataStatus BadResourceUnavailable { get; } = new DataStatus(DataStatus.BadResourceUnavailable, "BadResourceUnavailable", "An operating system resource is not available.");
        public static DataStatus BadCommunicationError { get; } = new DataStatus(DataStatus.BadCommunicationError, "BadCommunicationError", "A low level communication error occurred.");
        public static DataStatus BadEncodingError { get; } = new DataStatus(DataStatus.BadEncodingError, "BadEncodingError", "Encoding halted because of invalid data in the objects being serialized.");
        public static DataStatus BadDecodingError { get; } = new DataStatus(DataStatus.BadDecodingError, "BadDecodingError", "Decoding halted because of invalid data in the stream.");
        public static DataStatus BadEncodingLimitsExceeded { get; } = new DataStatus(DataStatus.BadEncodingLimitsExceeded, "BadEncodingLimitsExceeded", "The message encoding/decoding limits imposed by the stack have been exceeded.");
        public static DataStatus BadRequestTooLarge { get; } = new DataStatus(DataStatus.BadRequestTooLarge, "BadRequestTooLarge", "The request message size exceeds limits set by the server.");
        public static DataStatus BadResponseTooLarge { get; } = new DataStatus(DataStatus.BadResponseTooLarge, "BadResponseTooLarge", "The response message size exceeds limits set by the client.");
        public static DataStatus BadUnknownResponse { get; } = new DataStatus(DataStatus.BadUnknownResponse, "BadUnknownResponse", "An unrecognized response was received from the server.");
        public static DataStatus BadTimeout { get; } = new DataStatus(DataStatus.BadTimeout, "BadTimeout", "The operation timed out.");
        public static DataStatus BadNoCommunication { get; } = new DataStatus(DataStatus.BadNoCommunication, "BadNoCommunication", "Communication with the data source is defined, but not established, and there is no last known value available.");
        public static DataStatus BadWaitingForInitialData { get; } = new DataStatus(DataStatus.BadWaitingForInitialData, "BadWaitingForInitialData", "Waiting for the server to obtain values from the underlying data source.");
        public static DataStatus BadNotReadable { get; } = new DataStatus(DataStatus.BadNotReadable, "BadNotReadable", "The access level does not allow reading or subscribing to the Node.");
        public static DataStatus BadNotWritable { get; } = new DataStatus(DataStatus.BadNotWritable, "BadNotWritable", "The access level does not allow writing to the Node.");
        public static DataStatus BadOutOfRange { get; } = new DataStatus(DataStatus.BadOutOfRange, "BadOutOfRange", "The value was out of range.");
        public static DataStatus BadNotSupported { get; } = new DataStatus(DataStatus.BadNotSupported, "BadNotSupported", "The requested operation is not supported.");
        public static DataStatus BadNotFound { get; } = new DataStatus(DataStatus.BadNotFound, "BadNotFound", "A requested item was not found or a search operation ended without success.");
        public static DataStatus BadTcpServerTooBusy { get; } = new DataStatus(DataStatus.BadTcpServerTooBusy, "BadTcpServerTooBusy", "The server cannot process the request because it is too busy.");
        public static DataStatus BadTcpMessageTooLarge { get; } = new DataStatus(DataStatus.BadTcpMessageTooLarge, "BadTcpMessageTooLarge", "The size of the message specified in the header is too large.");
        public static DataStatus BadTcpNotEnoughResources { get; } = new DataStatus(DataStatus.BadTcpNotEnoughResources, "BadTcpNotEnoughResources", "There are not enough resources to process the request.");
        public static DataStatus BadTcpInternalError { get; } = new DataStatus(DataStatus.BadTcpInternalError, "BadTcpInternalError", "An internal error occurred.");
        public static DataStatus BadTcpEndpointUrlInvalid { get; } = new DataStatus(DataStatus.BadTcpEndpointUrlInvalid, "BadTcpEndpointUrlInvalid", "The Server does not recognize the QueryString specified.");
        public static DataStatus BadRequestInterrupted { get; } = new DataStatus(DataStatus.BadRequestInterrupted, "BadRequestInterrupted", "The request could not be sent because of a network interruption.");
        public static DataStatus BadRequestTimeout { get; } = new DataStatus(DataStatus.BadRequestTimeout, "BadRequestTimeout", "Timeout occurred while processing the request.");
        public static DataStatus BadNotConnected { get; } = new DataStatus(DataStatus.BadNotConnected, "BadNotConnected", "The variable should receive its value from another variable, but has never been configured to do so.");
        public static DataStatus BadDeviceFailure { get; } = new DataStatus(DataStatus.BadDeviceFailure, "BadDeviceFailure", "There has been a failure in the device/data source that generates the value that has affected the value.");

        #endregion Static Properties

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Sets and gets the status information. Note that the timestamp is updated when setting a new value.
        /// </summary>
        public DataStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                _timestamp = new DateTimeOffset(DateTime.UtcNow);
                OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Returns the current timestamp of the data value.
        /// </summary>
        public DateTimeOffset Timestamp
        {
            get { return _timestamp; }
        }

        /// <summary>
        /// Return true if Code == Good.
        /// </summary>
        [JsonIgnore]
        public bool IsGood { get => Status.IsGood; }

        /// <summary>
        /// Return true if Code == Bad.
        /// </summary>
        [JsonIgnore]
        public bool IsBad { get => Status.IsBad; }

        /// <summary>
        /// Return true if Code == Uncertain.
        /// </summary>
        [JsonIgnore]
        public bool IsUncertain { get => Status.IsUncertain; }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// Propagates the change of the property.
        /// </summary>
        /// <param name="caller">The name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        /// <summary>
        /// Sets the field value and calls OnPropertyChanged().
        /// </summary>
        /// <typeparam name="T">The field type.</typeparam>
        /// <param name="field">The field reference.</param>
        /// <param name="value">The field value.</param>
        /// <param name="propertyName">The property name.</param>
        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion Protected Methods

        #region Static Methods

        /// <summary>
        /// Returns the DataStatus for the specified status code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>The data status.</returns>
        public static DataStatus GetDataStatus(uint code)
        {
            switch (code)
            {
                case DataStatus.Good: return Good;
                case DataStatus.Uncertain: return Uncertain;
                case DataStatus.Bad: return Bad;
                case DataStatus.BadUnexpectedError: return BadUnexpectedError;
                case DataStatus.BadInternalError: return BadInternalError;
                case DataStatus.BadOutOfMemory: return BadOutOfMemory;
                case DataStatus.BadResourceUnavailable: return BadResourceUnavailable;
                case DataStatus.BadCommunicationError: return BadCommunicationError;
                case DataStatus.BadEncodingError: return BadEncodingError;
                case DataStatus.BadDecodingError: return BadDecodingError;
                case DataStatus.BadEncodingLimitsExceeded: return BadEncodingLimitsExceeded;
                case DataStatus.BadRequestTooLarge: return BadRequestTooLarge;
                case DataStatus.BadResponseTooLarge: return BadResponseTooLarge;
                case DataStatus.BadUnknownResponse: return BadUnknownResponse;
                case DataStatus.BadTimeout: return BadTimeout;
                case DataStatus.BadNoCommunication: return BadNoCommunication;
                case DataStatus.BadWaitingForInitialData: return BadWaitingForInitialData;
                case DataStatus.BadNotReadable: return BadNotReadable;
                case DataStatus.BadNotWritable: return BadNotWritable;
                case DataStatus.BadOutOfRange: return BadOutOfRange;
                case DataStatus.BadNotSupported: return BadNotSupported;
                case DataStatus.BadNotFound: return BadNotFound;
                case DataStatus.BadTcpServerTooBusy: return BadTcpServerTooBusy;
                case DataStatus.BadTcpMessageTooLarge: return BadTcpMessageTooLarge;
                case DataStatus.BadTcpNotEnoughResources: return BadTcpNotEnoughResources;
                case DataStatus.BadTcpInternalError: return BadTcpInternalError;
                case DataStatus.BadTcpEndpointUrlInvalid: return BadTcpEndpointUrlInvalid;
                case DataStatus.BadRequestInterrupted: return BadRequestInterrupted;
                case DataStatus.BadRequestTimeout: return BadRequestTimeout;
                case DataStatus.BadNotConnected: return BadNotConnected;
                case DataStatus.BadDeviceFailure: return BadDeviceFailure;
                default: return Uncertain;
            }
        }

        #endregion Static Methods
    }
}