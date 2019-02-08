// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report2Udp.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
{
    #region Using Directives

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// {
    ///   "ID": 2,
    ///   "State": 1,
    ///   "Error1": 0,
    ///   "Error2": 0,
    ///   "Plug": 3,
    ///   "AuthON": 1,
    ///   "Authreq": 1,
    ///   "Enable sys": 0,
    ///   "Enable user": 1,
    ///   "Max curr": 0,
    ///   "Max curr %": 1000,
    ///   "Curr HW": 20000,
    ///   "Curr user": 63000,
    ///   "Curr FS": 0,
    ///   "Tmo FS": 0,
    ///   "Curr timer": 0,
    ///   "Tmo CT": 0,
    ///   "Setenergy": 0,
    ///   "Output": 20,
    ///   "Input": 0,
    ///   "Serial": "18711747",
    ///   "Sec": 934473
    /// }
    /// </summary>
    public class Report2Udp
    {
        #region Public Properties

        /// <summary>
        /// 2 - ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// Enum indicating the charging state
        /// </summary>
        public ChargingStates State { get; set; }

        /// <summary>
        /// uint16 - Decimal number defining the error
        /// </summary>
        public ushort Error1 { get; set; }

        /// <summary>
        /// uint16 - Decimal number defining the error
        /// </summary>
        public ushort Error2 { get; set; }

        /// <summary>
        /// Enum indicating the plug state
        /// </summary>
        public PlugStates Plug { get; set; }

        /// <summary>
        /// Enum indicating the authorization function activation state
        /// </summary>
        public AuthorizationFunction AuthON { get; set; }

        /// <summary>
        /// Enum indicating the authorization requirement via RFID card
        /// </summary>
        public AuthorizationRequired AuthReq { get; set; }

        /// <summary>
        /// Enum indicating if the charging can be enabled
        /// </summary>
        [JsonProperty("Enable sys")]
        public ChargingEnabled EnableSys { get; set; }

        /// <summary>
        /// Enum indicating if the device is enabled
        /// </summary>
        [JsonProperty("Enable user")]
        public UserEnabled EnableUser { get; set; }

        /// <summary>
        /// uint16 - Current value in mA offered to the vehicle via control pilot signalization.
        ///          (Signal type: PWM) Possible values: 0; 6000 - 32000
        /// </summary>
        [JsonProperty("Max curr")]
        public ushort MaxCurr { get; set; }

        /// <summary>
        /// uint16 - Duty cycle of the control pilot signal in 0.1%.
        ///          Possible values: 100 - 533; 1000
        /// </summary>
        [JsonProperty("Max curr %")]
        public ushort MaxCurrPercent { get; set; }

        /// <summary>
        /// uint16 - Maximum current value in mA that can be supported by the hardware of the device.
        ///          This value represents the minimum of the DIP switch settings, cable coding and
        ///          temperature monitoring function.
        ///          Possible values: 0; 6000 - 32000
        /// </summary>
        [JsonProperty("Curr HW")]
        public ushort CurrHW { get; set; }

        /// <summary>
        /// uint16 - Current setting in mA defined via UDP current commands. (Default: 63000 mA)
        ///          Possible values: 0; 6000 - 63000
        /// </summary>
        [JsonProperty("Curr user")]
        public ushort CurrUser { get; set; }

        /// <summary>
        /// uint16 - Current setting in mA defined via failsafe function.
        ///          Possible values: 0; 6000 - 63000
        /// </summary>
        [JsonProperty("Curr FS")]
        public ushort CurrFS { get; set; }

        /// <summary>
        /// uint16 - Communication timeout in seconds before triggering the Failsafe function.
        ///          Possible values: 0; 10 - 600
        /// </summary>
        [JsonProperty("Tmo FS")]
        public ushort TmoFS { get; set; }

        /// <summary>
        /// uint16 - Current value in mA that will replace the setting in the “Curr user”
        ///          field as soon as “Tmo CT” expires.
        ///          Possible values: 0; 6000 - 63000
        /// </summary>
        [JsonProperty("Curr timer")]
        public ushort CurrTimer { get; set; }

        /// <summary>
        /// uint32 - Timeout in seconds before the current setting defined by the last
        ///          currtime command will be applied.
        ///          Possible values: 0; 1 - 860400
        /// </summary>
        [JsonProperty("Tmo CT")]
        public uint TmoCT { get; set; }

        /// <summary>
        /// uint32 - Energy value in 0.1 Wh defined by the last setenergy command.
        ///          (setenergy 100000 specifies 10 kWh).
        ///          Possible values: 0; 1 - 999999999
        /// </summary>
        public uint Setenergy { get; set; }

        /// <summary>
        /// uint32 - Show the setting of the UDP command output.
        ///          Possible values: 0; 1; 10 - 150
        /// </summary>
        public uint Output { get; set; }

        /// <summary>
        /// Enum indicating the state of the input X1.
        /// </summary>
        public InputStates Input { get; set; }

        /// <summary>
        /// String - (8 chars) Serial number of the device.
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// uint32 - Current state of the system clock in seconds
        ///          from the last startup of the device.
        /// </summary>
        public uint Sec { get; set; }

        #endregion
    }
}
