// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBControl.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlLib
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using DataValueLib;
    using NModbusLib.Models;
    using BControlLib.Models;

    #endregion

    /// <summary>
    /// The public interface implemented by the <see cref="BControl"/> class.
    /// </summary>
    public interface IBControl : ITcpClientSettings
    {
        BControlData Data { get; set; }
        [JsonIgnore]
        InternalData InternalData { get; }
        [JsonIgnore]
        EnergyData EnergyData { get; }
        [JsonIgnore]
        PnPData PnPData { get; }
        [JsonIgnore]
        SunSpecData SunSpecData { get; }

        bool IsLocked { get; }
        bool Connect();

        DataStatus ReadAll();
        DataStatus ReadBlockAll();
        DataStatus ReadProperty(string property);
        DataStatus ReadProperties(List<string> properties);
        DataStatus ReadInternalData();
        DataStatus ReadEnergyData();
        DataStatus ReadPnPData();
        DataStatus ReadSunSpecData();

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadBlockAllAsync();
        Task<DataStatus> ReadPropertyAsync(string property);
        Task<DataStatus> ReadPropertiesAsync(List<string> properties);
        Task<DataStatus> ReadInternalDataAsync();
        Task<DataStatus> ReadEnergyDataAsync();
        Task<DataStatus> ReadPnPDataAsync();
        Task<DataStatus> ReadSunSpecDataAsync();

        object GetPropertyValue(string property);
    }
}
