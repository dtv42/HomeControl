// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoSensors.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models
{
    #region Using Directives

    using System.Collections.Generic;

    using DataValueLib;
    using HomeControlLib.Zipato.Models.Sensors;

    #endregion

    /// <summary>
    /// Class holding attribute data values from the Zipato home controller.
    /// </summary>
    public class ZipatoSensors : DataValue
    {
        #region Public Properties

        public List<VirtualMeter> VirtualMeters { get; set; } = new List<VirtualMeter> { };
        public List<ConsumptionMeter> ConsumptionMeters { get; set; } = new List<ConsumptionMeter> { };
        public List<TemperatureSensor> TemperatureSensors { get; set; } = new List<TemperatureSensor> { };
        public List<HumiditySensor> HumiditySensors { get; set; } = new List<HumiditySensor> { };
        public List<LuminanceSensor> LuminanceSensors { get; set; } = new List<LuminanceSensor> { };

        #endregion
    }
}
