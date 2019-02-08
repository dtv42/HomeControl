// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    public class ModuleData
    {
        #region Public Properties

        public string ID { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> DataType { get; set; } = new List<string> { };
        public string ModuleName { get; set; } = string.Empty;
        public DateTimeOffset LastMessage { get; set; }
        public DateTimeOffset LastSeen { get; set; }
        public DateTimeOffset LastSetup { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BatteryLevel BatteryVP { get; set; }
        public int BatteryPercent { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RFSignal RFStatus { get; set; }
        public int Firmware { get; set; }

        #endregion

        #region Public Methods

        public static RFSignal GetRFSignal(int rfstatus)
        {
            if (rfstatus >= 90) return RFSignal.Low;
            else if (rfstatus >= 80) return RFSignal.Medium;
            else if (rfstatus >= 70) return RFSignal.High;
            else if (rfstatus >= 60) return RFSignal.Full;
            else if (rfstatus < 60) return RFSignal.Full;
            else return RFSignal.Unknown;
        }

        #endregion
    }
}
