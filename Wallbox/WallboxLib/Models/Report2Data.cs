// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report2Data.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    public class Report2Data : DataValue, IPropertyHelper
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

        #region Public Methods

        /// <summary>
        /// Updates the Properties used in Report1Data.
        /// </summary>
        /// <param name="data">The Wallbox data.</param>
        public void Refresh(WallboxData data)
        {
            if (data != null)
            {
                ID = data.Report2.ID;
                State = data.Report2.State;
                Error1 = data.Report2.Error1;
                Error2 = data.Report2.Error2;
                Plug = data.Report2.Plug;
                AuthON = data.Report2.AuthON;
                AuthRequired = data.Report2.AuthReq;
                EnableSystem = data.Report2.EnableSys;
                EnableUser = data.Report2.EnableUser;
                MaxCurrent = data.Report2.MaxCurr / 1000.0;
                DutyCycle = data.Report2.MaxCurrPercent / 10.0;
                CurrentHW = data.Report2.CurrHW / 1000.0;
                CurrentUser = data.Report2.CurrUser / 1000.0;
                CurrentFS = data.Report2.CurrFS / 1000.0;
                TimeoutFS = data.Report2.TmoFS;
                CurrentTimer = data.Report2.CurrTimer / 1000.0;
                TimeoutCT = data.Report2.TmoCT;
                SetEnergy = data.Report2.Setenergy / 10000.0;
                Output = data.Report2.Output;
                Input = data.Report2.Input;
                Serial = data.Report2.Serial;
                Seconds = data.Report2.Sec;
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the Report2Data class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(Report2Data).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the Report2Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(Report2Data), property) != null) ? true : false;

        /// <summary>
        /// Returns the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>The property value.</returns>
        public object GetPropertyValue(string property) => PropertyValue.GetPropertyValue(this, property);

        /// <summary>
        /// Sets the value for the property with the specified name.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        public void SetPropertyValue(string property, object value) => PropertyValue.SetPropertyValue(this, property, value);

        #endregion
    }
}
