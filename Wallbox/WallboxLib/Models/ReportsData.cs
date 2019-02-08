// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using DataValueLib;

    #endregion

    public class ReportsData : DataValue, IPropertyHelper
    {
        #region Public Properties

        /// <summary>
        /// ID of the report
        /// </summary>
        public ushort ID { get; set; }

        /// <summary>
        /// ID of the current charging session.
        /// </summary>
        public uint SessionID { get; set; }

        /// <summary>
        /// Maximum current value in mA that can be supported by the hardware of the device.
        /// </summary>
        public double CurrentHW { get; set; }

        /// <summary>
        /// Total energy consumption (persistent, device related) without the current charging session.
        public double EnergyConsumption { get; set; }

        /// <summary>
        /// Energy transferred in the current charging session.
        /// </summary>
        public double EnergyTransferred { get; set; }

        /// <summary>
        /// System clock in seconds from the last startup of the device at the start of the charging session.
        /// </summary>
        public uint StartedSeconds { get; set; }

        /// <summary>
        /// System clock in seconds from the last startup of the device at the end of the charging session.
        /// </summary>
        public uint EndedSeconds { get; set; }

        /// <summary>
        /// Date stamp representing the current time in UTC at the start of the charging session.
        /// </summary>
        public DateTime Started { get; set; } = new DateTime();

        /// <summary>
        /// Date stamp representing the current time in UTC at the end of the charging session.
        /// </summary>
        public DateTime Ended { get; set; } = new DateTime();

        /// <summary>
        /// Enum indicating the reason for ending the charging session.
        /// </summary>
        public Reasons Reason { get; set; }

        /// <summary>
        /// Synced time.
        /// </summary>
        public ushort TimeQ { get; set; }

        /// <summary>
        /// RFID Token ID if session started with RFID.
        /// </summary>
        public string RFID { get; set; } = string.Empty;

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
        /// Updates the Properties used in ReportsData.
        /// </summary>
        /// <param name="data">The Wallbox data.</param>
        /// <param name="id">The reports index (100 - 130).</param>
        public void Refresh(WallboxData data, int id = 100)
        {
            if (data != null)
            {
                CultureInfo provider = CultureInfo.InvariantCulture;

                if (id == Wallbox.REPORTS_ID)
                {
                    ID = data.Report100.ID;
                    SessionID = data.Report100.SessionID;
                    CurrentHW = data.Report100.CurrHW / 1000.0;
                    EnergyConsumption = data.Report100.Estart / 10000.0;
                    EnergyTransferred = data.Report100.Epres / 10000.0;
                    StartedSeconds = data.Report100.StartedSec;
                    EndedSeconds = data.Report100.EndedSec;
                    Started = DateTime.TryParse(data.Report100.Started, out DateTime started) ? started : new DateTime();
                    Ended = DateTime.TryParse(data.Report100.Ended, out DateTime ended) ? ended : new DateTime();
                    Reason = data.Report100.Reason;
                    TimeQ = data.Report100.TimeQ;
                    RFID = data.Report100.RFIDclass;
                    Serial = data.Report100.Serial;
                    Seconds = data.Report100.Sec;
                }
                else if ((id > Wallbox.REPORTS_ID) && (id <= (Wallbox.MAX_REPORTS + Wallbox.REPORTS_ID)))
                {
                    int index = id - (Wallbox.REPORTS_ID + 1);
                    ID = data.Reports[index].ID;
                    SessionID = data.Reports[index].SessionID;
                    CurrentHW = data.Reports[index].CurrHW / 1000.0;
                    EnergyConsumption = data.Reports[index].Estart / 10000.0;
                    EnergyTransferred = data.Reports[index].Epres / 10000.0;
                    StartedSeconds = data.Reports[index].StartedSec;
                    EndedSeconds = data.Reports[index].EndedSec;
                    Started = DateTime.TryParse(data.Reports[index].Started, out DateTime started) ? started : new DateTime();
                    Ended = DateTime.TryParse(data.Reports[index].Ended, out DateTime ended) ? ended : new DateTime();
                    Reason = data.Reports[index].Reason;
                    TimeQ = data.Reports[index].TimeQ;
                    RFID = data.Reports[index].RFIDclass;
                    Serial = data.Reports[index].Serial;
                    Seconds = data.Reports[index].Sec;
                }
            }

            Status = data?.Status ?? Uncertain;
        }

        /// <summary>
        /// Gets the property list for the Report3Data class.
        /// </summary>
        /// <returns>The property list.</returns>
        public static List<string> GetProperties()
            => typeof(Report3Data).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(p => p.Name).ToList();

        /// <summary>
        /// Returns true if property with the specified name is found in the Report3Data class.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns true if property is found.</returns>
        public static bool IsProperty(string property)
            => (PropertyValue.GetPropertyInfo(typeof(Report3Data), property) != null) ? true : false;

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
