namespace FroniusLib
{
    using System.Collections.Generic;
    #region Using Directives

    using System.Threading.Tasks;

    using DataValueLib;
    using FroniusLib.Models;

    #endregion

    public interface IFronius : ISettingsData
    {
        FroniusData Data { get; }
        CommonData CommonData { get; }
        PhaseData PhaseData { get; }
        MinMaxData MinMaxData { get; }
        InverterInfo InverterInfo { get; }
        LoggerInfo LoggerInfo { get; }
        APIVersionData VersionInfo { get; }

        bool IsLocked { get; }
        bool CheckAccess();

        DataStatus ReadAll();
        DataStatus ReadInverterInfo();
        DataStatus ReadCommonData();
        DataStatus ReadPhaseData();
        DataStatus ReadMinMaxData();
        DataStatus ReadLoggerInfo();
        DataStatus ReadProperty(string property);
        DataStatus GetAPIVersion();

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadInverterInfoAsync();
        Task<DataStatus> ReadCommonDataAsync();
        Task<DataStatus> ReadPhaseDataAsync();
        Task<DataStatus> ReadMinMaxDataAsync();
        Task<DataStatus> ReadLoggerInfoAsync();
        Task<DataStatus> ReadPropertyAsync(string property);
        Task<DataStatus> GetAPIVersionAsync();

        object GetPropertyValue(string property);
    }
}
