// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThermostatInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Data;
    using ZipatoLib.Models.Config;
    using ZipatoLib.Models.Entities;

    #endregion

    public class ThermostatInfo : IconEntity
    {
        public ThermostatConfig Config { get; set; } = new ThermostatConfig();
        public UuidEntity Network { get; set; } = new UuidEntity();
        public List<OperationData> Operations { get; set; } = new List<OperationData> { };
    }
}
