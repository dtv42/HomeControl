// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhaseDeviceData.cs" company="DTV-Online">
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

    public class PhaseDeviceData
    {
        public RawPhaseDeviceData Inverter { get; set; } = new RawPhaseDeviceData();

        public ResponseData Response { get; set; } = new ResponseData();

        public PhaseDeviceData() { }

        public PhaseDeviceData(FroniusPhaseData data)
        {
            // Response data
            Response.Status = data.Head.Status;
            Response.Timestamp = new DateTimeOffset(data.Head.Timestamp);
            // Common data
            Inverter.CurrentL1 = data.Body.Data.IAC_L1;
            Inverter.CurrentL2 = data.Body.Data.IAC_L2;
            Inverter.CurrentL3 = data.Body.Data.IAC_L3;
            Inverter.VoltageL1N = data.Body.Data.UAC_L1;
            Inverter.VoltageL2N = data.Body.Data.UAC_L2;
            Inverter.VoltageL3N = data.Body.Data.UAC_L3;
        }
    }
}
