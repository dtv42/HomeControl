// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InverterDeviceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Net;

    using Extensions;

    #endregion

    public class InverterDeviceData
    {
        public RawInverterDeviceData Inverter { get; set; } = new RawInverterDeviceData();

        public ResponseData Response { get; set; } = new ResponseData();

        public InverterDeviceData()
        { }

        public InverterDeviceData(FroniusInverterInfo data)
        {
            // Response data
            Response.Status = data.Head.Status;
            Response.Timestamp = new DateTimeOffset(data.Head.Timestamp);
            // Inverter data
            if (data.Body.Data.Count > 0)
            {
                Inverter.Index = data.Body.Data.FirstOrDefault().Key;
                Inverter.DeviceType = data.Body.Data[Inverter.Index].DT;
                Inverter.PVPower = data.Body.Data[Inverter.Index].PVPower;
                Inverter.CustomName = WebUtility.HtmlDecode(data.Body.Data[Inverter.Index].CustomName);
                Inverter.Show = data.Body.Data[Inverter.Index].Show != 0;
                Inverter.UniqueID = data.Body.Data[Inverter.Index].UniqueID;
                Inverter.ErrorCode = data.Body.Data[Inverter.Index].ErrorCode;
                Inverter.StatusCode = data.Body.Data[Inverter.Index].StatusCode.ToStatusCode();
            }
        }
    }
}
