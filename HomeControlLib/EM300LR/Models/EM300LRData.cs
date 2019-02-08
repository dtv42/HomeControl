// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.EM300LR.Models
{
    #region Using Directives

    using Newtonsoft.Json;
    using DataValueLib;

    #endregion Using Directives

    /// <summary>
    /// Class holding all data from the b-Control EM300LR energy manager.
    /// </summary>
    public class EM300LRData : DataValue
    {
        #region Public Properties

        [JsonProperty("serial")]
        public string Serial { get; set; }

        [JsonProperty("1-0:1.4.0*255")] public double ActivePowerPlus { get; set; }
        [JsonProperty("1-0:1.8.0*255")] public double ActiveEnergyPlus { get; set; }
        [JsonProperty("1-0:2.4.0*255")] public double ActivePowerMinus { get; set; }
        [JsonProperty("1-0:2.8.0*255")] public double ActiveEnergyMinus { get; set; }
        [JsonProperty("1-0:3.4.0*255")] public double ReactivePowerPlus { get; set; }
        [JsonProperty("1-0:3.8.0*255")] public double ReactiveEnergyPlus { get; set; }
        [JsonProperty("1-0:4.4.0*255")] public double ReactivePowerMinus { get; set; }
        [JsonProperty("1-0:4.8.0*255")] public double ReactiveEnergyMinus { get; set; }
        [JsonProperty("1-0:9.4.0*255")] public double ApparentPowerPlus { get; set; }
        [JsonProperty("1-0:9.8.0*255")] public double ApparentEnergyPlus { get; set; }
        [JsonProperty("1-0:10.4.0*255")] public double ApparentPowerMinus { get; set; }
        [JsonProperty("1-0:10.8.0*255")] public double ApparentEnergyMinus { get; set; }
        [JsonProperty("1-0:13.4.0*255")] public double PowerFactor { get; set; }
        [JsonProperty("1-0:14.4.0*255")] public double SupplyFrequency { get; set; }
        [JsonProperty("1-0:21.4.0*255")] public double ActivePowerPlusL1 { get; set; }
        [JsonProperty("1-0:21.8.0*255")] public double ActiveEnergyPlusL1 { get; set; }
        [JsonProperty("1-0:22.4.0*255")] public double ActivePowerMinusL1 { get; set; }
        [JsonProperty("1-0:22.8.0*255")] public double ActiveEnergyMinusL1 { get; set; }
        [JsonProperty("1-0:23.4.0*255")] public double ReactivePowerPlusL1 { get; set; }
        [JsonProperty("1-0:23.8.0*255")] public double ReactiveEnergyPlusL1 { get; set; }
        [JsonProperty("1-0:24.4.0*255")] public double ReactivePowerMinusL1 { get; set; }
        [JsonProperty("1-0:24.8.0*255")] public double ReactiveEnergyMinusL1 { get; set; }
        [JsonProperty("1-0:29.4.0*255")] public double ApparentPowerPlusL1 { get; set; }
        [JsonProperty("1-0:29.8.0*255")] public double ApparentEnergyPlusL1 { get; set; }
        [JsonProperty("1-0:30.4.0*255")] public double ApparentPowerMinusL1 { get; set; }
        [JsonProperty("1-0:30.8.0*255")] public double ApparentEnergyMinusL1 { get; set; }
        [JsonProperty("1-0:31.4.0*255")] public double CurrentL1 { get; set; }
        [JsonProperty("1-0:32.4.0*255")] public double VoltageL1 { get; set; }
        [JsonProperty("1-0:33.4.0*255")] public double PowerFactorL1 { get; set; }
        [JsonProperty("1-0:41.4.0*255")] public double ActivePowerPlusL2 { get; set; }
        [JsonProperty("1-0:41.8.0*255")] public double ActiveEnergyPlusL2 { get; set; }
        [JsonProperty("1-0:42.4.0*255")] public double ActivePowerMinusL2 { get; set; }
        [JsonProperty("1-0:42.8.0*255")] public double ActiveEnergyMinusL2 { get; set; }
        [JsonProperty("1-0:43.4.0*255")] public double ReactivePowerPlusL2 { get; set; }
        [JsonProperty("1-0:43.8.0*255")] public double ReactiveEnergyPlusL2 { get; set; }
        [JsonProperty("1-0:44.4.0*255")] public double ReactivePowerMinusL2 { get; set; }
        [JsonProperty("1-0:44.8.0*255")] public double ReactiveEnergyMinusL2 { get; set; }
        [JsonProperty("1-0:49.4.0*255")] public double ApparentPowerPlusL2 { get; set; }
        [JsonProperty("1-0:49.8.0*255")] public double ApparentEnergyPlusL2 { get; set; }
        [JsonProperty("1-0:50.4.0*255")] public double ApparentPowerMinusL2 { get; set; }
        [JsonProperty("1-0:50.8.0*255")] public double ApparentEnergyMinusL2 { get; set; }
        [JsonProperty("1-0:51.4.0*255")] public double CurrentL2 { get; set; }
        [JsonProperty("1-0:52.4.0*255")] public double VoltageL2 { get; set; }
        [JsonProperty("1-0:53.4.0*255")] public double PowerFactorL2 { get; set; }
        [JsonProperty("1-0:61.4.0*255")] public double ActivePowerPlusL3 { get; set; }
        [JsonProperty("1-0:61.8.0*255")] public double ActiveEnergyPlusL3 { get; set; }
        [JsonProperty("1-0:62.4.0*255")] public double ActivePowerMinusL3 { get; set; }
        [JsonProperty("1-0:62.8.0*255")] public double ActiveEnergyMinusL3 { get; set; }
        [JsonProperty("1-0:63.4.0*255")] public double ReactivePowerPlusL3 { get; set; }
        [JsonProperty("1-0:63.8.0*255")] public double ReactiveEnergyPlusL3 { get; set; }
        [JsonProperty("1-0:64.4.0*255")] public double ReactivePowerMinusL3 { get; set; }
        [JsonProperty("1-0:64.8.0*255")] public double ReactiveEnergyMinusL3 { get; set; }
        [JsonProperty("1-0:69.4.0*255")] public double ApparentPowerPlusL3 { get; set; }
        [JsonProperty("1-0:69.8.0*255")] public double ApparentEnergyPlusL3 { get; set; }
        [JsonProperty("1-0:70.4.0*255")] public double ApparentPowerMinusL3 { get; set; }
        [JsonProperty("1-0:70.8.0*255")] public double ApparentEnergyMinusL3 { get; set; }
        [JsonProperty("1-0:71.4.0*255")] public double CurrentL3 { get; set; }
        [JsonProperty("1-0:72.4.0*255")] public double VoltageL3 { get; set; }
        [JsonProperty("1-0:73.4.0*255")] public double PowerFactorL3 { get; set; }

        [JsonProperty("status")]
        public int StatusCode { get; set; }

        #endregion Public Properties
    }
}
