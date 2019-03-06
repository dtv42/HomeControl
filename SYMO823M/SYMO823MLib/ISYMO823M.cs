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

        bool IsLocked { get; }
        bool Connect();

        DataStatus ReadAll();
        DataStatus ReadBlockAll();
        DataStatus ReadProperty(string property);
        DataStatus ReadProperties(List<string> properties);
        DataStatus ReadCommonModel();
        DataStatus ReadInverterModel();
        DataStatus ReadNameplateModel();
        DataStatus ReadSettingsModel();
        DataStatus ReadExtendedModel();
        DataStatus ReadControlModel();
        DataStatus ReadMultipleModel();
        DataStatus ReadFroniusRegister();
        DataStatus WriteAll();
        DataStatus WriteProperty(string property, string data);
        DataStatus WriteProperty(string property);
        DataStatus WriteProperties(List<string> properties);

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadBlockAllAsync();
        Task<DataStatus> ReadPropertyAsync(string property);
        Task<DataStatus> ReadPropertiesAsync(List<string> properties);
        Task<DataStatus> ReadCommonModelAsync();
        Task<DataStatus> ReadInverterModelAsync();
        Task<DataStatus> ReadNameplateModelAsync();
        Task<DataStatus> ReadSettingsModelAsync();
        Task<DataStatus> ReadExtendedModelAsync();
        Task<DataStatus> ReadControlModelAsync();
        Task<DataStatus> ReadMultipleModelAsync();
        Task<DataStatus> ReadFroniusRegisterAsync();
        Task<DataStatus> WriteAllAsync();
        Task<DataStatus> WritePropertyAsync(string property, string data);
        Task<DataStatus> WritePropertyAsync(string property);
        Task<DataStatus> WritePropertiesAsync(List<string> properties);

        object GetPropertyValue(string property);
    }
}
