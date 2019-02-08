// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStatus.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
using Newtonsoft.Json;

namespace DataValueLib
{
    /// <summary>
    /// The enumeration of all supported status codes.
    /// </summary>
    public class DataStatus
    {
        #region Public Constants

        public const uint Good = 0x00;
        public const uint Uncertain = 0x40000000;
        public const uint Bad = 0x80000000;
        public const uint BadUnexpectedError = 0x80010000;
        public const uint BadInternalError = 0x80020000;
        public const uint BadOutOfMemory = 0x80030000;
        public const uint BadResourceUnavailable = 0x80040000;
        public const uint BadCommunicationError = 0x80050000;
        public const uint BadEncodingError = 0x80060000;
        public const uint BadDecodingError = 0x80070000;
        public const uint BadEncodingLimitsExceeded = 0x80080000;
        public const uint BadRequestTooLarge = 0x80B80000;
        public const uint BadResponseTooLarge = 0x80B90000;
        public const uint BadUnknownResponse = 0x80090000;
        public const uint BadTimeout = 0x800A0000;
        public const uint BadNoCommunication = 0x80310000;
        public const uint BadWaitingForInitialData = 0x80320000;
        public const uint BadNotReadable = 0x803A0000;
        public const uint BadNotWritable = 0x803B0000;
        public const uint BadOutOfRange = 0x803C0000;
        public const uint BadNotSupported = 0x803D0000;
        public const uint BadNotFound = 0x803E0000;
        public const uint BadTcpServerTooBusy = 0x807D0000;
        public const uint BadTcpMessageTooLarge = 0x80800000;
        public const uint BadTcpNotEnoughResources = 0x80810000;
        public const uint BadTcpInternalError = 0x80820000;
        public const uint BadTcpEndpointUrlInvalid = 0x80830000;
        public const uint BadRequestInterrupted = 0x80840000;
        public const uint BadRequestTimeout = 0x80850000;
        public const uint BadNotConnected = 0x808A0000;
        public const uint BadDeviceFailure = 0x808B0000;

        #endregion Public Constants

        #region Public Properties

        /// <summary>
        /// The numeric value of the StatusCode.
        /// </summary>
        public uint Code { get; set; }

        /// <summary>
        /// The symbolic name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short message explaining the StatusCode.
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Return true if Code == Good.
        /// </summary>
        [JsonIgnore]
        public bool IsGood { get => Code == DataStatus.Good; }

        /// <summary>
        /// Return true if Code == Bad.
        /// </summary>
        [JsonIgnore]
        public bool IsBad { get => (Code & DataStatus.Bad) == DataStatus.Bad; }

        /// <summary>
        /// Return true if Code == Uncertain.
        /// </summary>
        [JsonIgnore]
        public bool IsUncertain { get => Code == DataStatus.Uncertain; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStatus"/> class.
        /// Note that the default values are set to represent Uncertain.
        /// </summary>
        public DataStatus()
        {
            Code = DataValue.Uncertain.Code;
            Name = DataValue.Uncertain.Name;
            Explanation = DataValue.Uncertain.Explanation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStatus"/> class.
        /// </summary>
        /// <param name="code">The status code.</param>
        /// <param name="name">The status name.</param>
        /// <param name="explanation">The explaation of the status.</param>
        public DataStatus(uint code, string name, string explanation)
        {
            Code = code;
            Name = name;
            Explanation = explanation;
        }

        #endregion Constructors
    }
}