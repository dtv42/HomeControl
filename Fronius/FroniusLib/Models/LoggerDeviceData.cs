// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerDeviceData.cs" company="DTV-Online">
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

    public class LoggerDeviceData
    {
        public RawLoggerDeviceData Logger { get; set; } = new RawLoggerDeviceData();

        public ResponseData Response { get; set; } = new ResponseData();

        public LoggerDeviceData()
        { }

        public LoggerDeviceData(FroniusLoggerInfo data)
        {
            // Response data
            Response.Status = data.Head.Status;
            Response.Timestamp = new DateTimeOffset(data.Head.Timestamp);
            // Logger data
            Logger.UniqueID = data.Body.LoggerInfo.UniqueID;
            Logger.ProductID = data.Body.LoggerInfo.ProductID;
            Logger.PlatformID = data.Body.LoggerInfo.PlatformID;
            Logger.HWVersion = data.Body.LoggerInfo.HWVersion;
            Logger.SWVersion = data.Body.LoggerInfo.SWVersion;
            Logger.TimezoneLocation = data.Body.LoggerInfo.TimezoneLocation;
            Logger.TimezoneName = data.Body.LoggerInfo.TimezoneName;
            Logger.UTCOffset = data.Body.LoggerInfo.UTCOffset;
            Logger.DefaultLanguage = data.Body.LoggerInfo.DefaultLanguage;
            Logger.CashFactor = data.Body.LoggerInfo.CashFactor;
            Logger.CashCurrency = data.Body.LoggerInfo.CashCurrency;
            Logger.CO2Factor = data.Body.LoggerInfo.CO2Factor;
            Logger.CO2Unit = data.Body.LoggerInfo.CO2Unit;
        }
    }
}
