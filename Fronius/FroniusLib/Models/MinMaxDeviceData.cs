// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinMaxDeviceData.cs" company="DTV-Online">
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

    public class MinMaxDeviceData
    {
        public RawMinMaxDeviceData Inverter { get; set; } = new RawMinMaxDeviceData();

        public ResponseData Response { get; set; } = new ResponseData();

        public MinMaxDeviceData() { }

        public MinMaxDeviceData(FroniusMinMaxData data)
        {
            // Response data
            Response.Status = data.Head.Status;
            Response.Timestamp = new DateTimeOffset(data.Head.Timestamp);
            // Common data
            Inverter.DailyMaxVoltageDC = data.Body.Data.DAY_UDCMAX;
            Inverter.DailyMaxVoltageAC = data.Body.Data.DAY_UACMAX;
            Inverter.DailyMinVoltageAC = data.Body.Data.DAY_UACMIN;
            Inverter.YearlyMaxVoltageDC = data.Body.Data.YEAR_UDCMAX;
            Inverter.YearlyMaxVoltageAC = data.Body.Data.YEAR_UACMAX;
            Inverter.YearlyMinVoltageAC = data.Body.Data.YEAR_UACMIN;
            Inverter.TotalMaxVoltageDC = data.Body.Data.TOTAL_UDCMAX;
            Inverter.TotalMaxVoltageAC = data.Body.Data.TOTAL_UACMAX;
            Inverter.TotalMinVoltageAC = data.Body.Data.TOTAL_UACMIN;
            Inverter.DailyMaxPower = data.Body.Data.DAY_PMAX;
            Inverter.YearlyMaxPower = data.Body.Data.YEAR_PMAX;
            Inverter.TotalMaxPower = data.Body.Data.TOTAL_PMAX;
        }
    }
}
