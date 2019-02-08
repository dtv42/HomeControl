namespace FroniusLib
{
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
        bool IsInitialized { get; }

        Task<bool> CheckAccess();
        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadInverterInfoAsync();
        Task<DataStatus> ReadCommonDataAsync();
        Task<DataStatus> ReadPhaseDataAsync();
        Task<DataStatus> ReadMinMaxDataAsync();
        Task<DataStatus> ReadLoggerInfoAsync();
        Task<DataStatus> GetAPIVersionAsync();
        Task<DataStatus> ReadPropertyAsync(string property);

        object GetPropertyValue(string property);
    }
}
