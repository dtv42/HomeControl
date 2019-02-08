// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    #region Using Directives

    using System;

    #endregion

    public class CommonDeviceData
    {
        public RawCommonDeviceData Inverter { get; set; } = new RawCommonDeviceData();

        public ResponseData Response { get; set; } = new ResponseData();

        public CommonDeviceData()
        {
        }

        public CommonDeviceData(FroniusCommonData data)
        {
            // Response data
            Response.Status = data.Head.Status;
            Response.Timestamp = new DateTimeOffset(data.Head.Timestamp);
            // Common data
            Inverter.Frequency = data.Body.Data.FAC;
            Inverter.CurrentDC = data.Body.Data.IDC;
            Inverter.CurrentAC = data.Body.Data.IAC;
            Inverter.VoltageDC = data.Body.Data.UDC;
            Inverter.VoltageAC = data.Body.Data.UAC;
            Inverter.PowerAC = data.Body.Data.PAC;
            Inverter.DailyEnergy = data.Body.Data.DAY_ENERGY;
            Inverter.YearlyEnergy = data.Body.Data.YEAR_ENERGY;
            Inverter.TotalEnergy = data.Body.Data.TOTAL_ENERGY;
            Inverter.DeviceStatus = data.Body.Data.DeviceStatus;
        }
    }
}
