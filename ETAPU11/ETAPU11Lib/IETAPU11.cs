// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IETAPU11.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Lib
{
    #region Using Directives

    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using DataValueLib;
    using NModbusLib.Models;
    using ETAPU11Lib.Models;

    #endregion

    /// <summary>
    /// The public interface implemented by the <see cref="ETAPU11"/> class.
    /// </summary>
    public interface IETAPU11 : ITcpClientSettings
    {
        ETAPU11Data Data { get; set; }
        [JsonIgnore]
        BoilerData BoilerData { get; }
        [JsonIgnore]
        HotwaterData HotwaterData { get; }
        [JsonIgnore]
        HeatingData HeatingData { get; }
        [JsonIgnore]
        StorageData StorageData { get; }
        [JsonIgnore]
        SystemData SystemData { get; }

        bool IsInitialized { get; }
        bool Connect();

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadBlockAsync();
        Task<DataStatus> ReadDataAsync(string property);
        Task<DataStatus> ReadBoilerDataAsync();
        Task<DataStatus> ReadHotwaterDataAsync();
        Task<DataStatus> ReadHeatingDataAsync();
        Task<DataStatus> ReadStorageDataAsync();
        Task<DataStatus> ReadSystemDataAsync();
        Task<DataStatus> WriteAllAsync();
        Task<DataStatus> WriteDataAsync(string property, string data);
        Task<DataStatus> WriteDataAsync(string property);
        Task<DataStatus> WriteDataAsync(List<string> properties);

        object GetPropertyValue(string property);
    }
}
