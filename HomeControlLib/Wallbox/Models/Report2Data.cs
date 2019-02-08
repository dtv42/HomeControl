// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report2Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Wallbox.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    public class Report2Data : DataValue
    {
        #region Public Properties

        /// <summary>
        /// ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// Enum indicating the charging state
        /// </summary>
        public ChargingStates State { get; set; }

        /// <summary>
        /// Decimal number defining the error
        /// </summary>
        public ushort Error1 { get; set; }

        /// <summary>
        /// Decimal number defining the error
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
        public AuthorizationRequired AuthRequired { get; set; }

        /// <summary>
        /// Enum indicating if the charging can be enabled
        /// </summary>
        public ChargingEnabled EnableSystem { get; set; }

        /// <summary>
        /// Enum indicating if the device is enabled
        /// </summary>
        public UserEnabled EnableUser { get; set; }

        /// <summary>
        /// Current value in A offered to the vehicle via control pilot signalization.
        /// </summary>
        public double MaxCurrent { get; set; }

        /// <summary>
        /// uint16 - Duty cycle of the control pilot signal in %.
        /// </summary>
        public double DutyCycle { get; set; }

        /// <summary>
        /// Maximum current value in A that can be supported by the hardware of the device.
        /// </summary>
        public double CurrentHW { get; set; }

        /// <summary>
        /// Current setting in A defined via UDP current commands.
        /// </summary>
        public double CurrentUser { get; set; }

        /// <summary>
        /// Current setting in A defined via failsafe function.
        /// </summary>
        public double CurrentFS { get; set; }

        /// <summary>
        /// Communication timeout in seconds before triggering the Failsafe function.
        /// </summary>
        public ushort TimeoutFS { get; set; }

        /// <summary>
        /// Current value in A that will replace the setting in the “Curr user” field as soon as “Tmo CT” expires.
        /// </summary>
        public double CurrentTimer { get; set; }

        /// <summary>
        /// Timeout in seconds before the current setting defined by the last currtime command will be applied.
        /// </summary>
        public uint TimeoutCT { get; set; }

        /// <summary>
        /// Energy value in kWh defined by the last setenergy command.
        /// </summary>
        public double SetEnergy { get; set; }

        /// <summary>
        ///UDP command output.
        /// </summary>
        public uint Output { get; set; }

        /// <summary>
        /// Enum indicating the state of the input X1.
        /// </summary>
        public InputStates Input { get; set; }

        /// <summary>
        /// Serial number of the device.
        /// </summary>
        public string Serial { get; set; } = string.Empty;

        /// <summary>
        /// Current state of the system clock in seconds from the last startup of the device.
        /// </summary>
        public uint Seconds { get; set; }

        #endregion
    }
}
