// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISYMO823M.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MLib
{
    #region Using Directives

    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using DataValueLib;
    using NModbusLib.Models;
    using SYMO823MLib.Models;

    #endregion

    /// <summary>
    /// The interface implemented by the <see cref="SYMO823M"/> class.
    /// </summary>
    public interface  ISYMO823M
    {
        SYMO823MData Data { get; set; }
        [JsonIgnore]
        CommonModelData CommonModel { get; }
        [JsonIgnore]
        InverterModelData InverterModel { get; }
        [JsonIgnore]
        NameplateModelData NameplateModel { get; }
        [JsonIgnore]
        SettingsModelData SettingsModel { get; }
        [JsonIgnore]
        ExtendedModelData ExtendedModel { get; }
        [JsonIgnore]
        ControlModelData ControlModel { get; }
        [JsonIgnore]
        MultipleModelData MultipleModel { get; }
        [JsonIgnore]
        FroniusRegisterData FroniusRegister { get; }

        TcpMasterData Master { get; set; }
        TcpSlaveData Slave { get; set; }
        bool IsInitialized { get; }
        bool Connect();

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadBlockAsync();
        Task<DataStatus> ReadDataAsync(string property);
        Task<DataStatus> ReadCommonModelAsync();
        Task<DataStatus> ReadInverterModelAsync();
        Task<DataStatus> ReadNameplateModelAsync();
        Task<DataStatus> ReadSettingsModelAsync();
        Task<DataStatus> ReadExtendedModelAsync();
        Task<DataStatus> ReadControlModelAsync();
        Task<DataStatus> ReadMultipleModelAsync();
        Task<DataStatus> ReadFroniusRegisterAsync();
        Task<DataStatus> WriteAllAsync();
        Task<DataStatus> WriteDataAsync(string property, string data);
        Task<DataStatus> WriteDataAsync(string property);
        Task<DataStatus> WriteDataAsync(List<string> properties);

        object GetPropertyValue(string property);
    }
}
