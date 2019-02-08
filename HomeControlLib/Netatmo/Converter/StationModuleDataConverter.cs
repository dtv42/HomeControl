// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationModuleDataConverter.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Converter
{
    #region Using Directives

    using System;

    using Newtonsoft.Json.Linq;

    using HomeControlLib.Netatmo.Models;

    #endregion

    /// <summary>
    /// List<ModuleData> modules = JsonConvert.DeserializeObject<List<ModuleData>>(json, new ModuleDataConverter());
    /// </summary>
    public class StationModuleDataConverter : JsonCreationConverter<RawStationData.BodyData.DeviceData.ModuleData>
    {
        #region Protected Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="jObject"></param>
        /// <returns></returns>
        protected override RawStationData.BodyData.DeviceData.ModuleData Create(Type objectType, JObject jObject)
        {
            if (jObject["type"].Value<string>() == "NAModule1")
            {
                return new RawStationData.BodyData.DeviceData.Module1Data();
            }
            else if (jObject["type"].Value<string>() == "NAModule2")
            {
                return new RawStationData.BodyData.DeviceData.Module2Data();
            }
            else if (jObject["type"].Value<string>() == "NAModule3")
            {
                return new RawStationData.BodyData.DeviceData.Module3Data();
            }
            else if (jObject["type"].Value<string>() == "NAModule4")
            {
                return new RawStationData.BodyData.DeviceData.Module4Data();
            }
            else
            {
                return new RawStationData.BodyData.DeviceData.ModuleData();
            }
        }

        #endregion
    }
}
