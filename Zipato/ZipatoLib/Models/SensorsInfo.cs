// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SensorsInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Helper class holding endpoint UUIDs.
    /// </summary>
    public class SensorsInfo
    {
        public List<Guid> VirtualMeters { get; set; } = new List<Guid> { };
        public List<Guid> ConsumptionMeters { get; set; } = new List<Guid> { };
        public List<Guid> TemperatureSensors { get; set; } = new List<Guid> { };
        public List<Guid> HumiditySensors { get; set; } = new List<Guid> { };
        public List<Guid> LuminanceSensors { get; set; } = new List<Guid> { };
    }
}
