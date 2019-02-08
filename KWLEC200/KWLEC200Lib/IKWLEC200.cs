namespace KWLEC200Lib
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json;

    using DataValueLib;
    using NModbusLib.Models;
    using KWLEC200Lib.Models;

    #endregion

    /// <summary>
    /// The public interface implemented by the <see cref="KWLEC200"/> class.
    /// </summary>
    public interface IKWLEC200
    {
        KWLEC200Data Data { get; set; }
        [JsonIgnore]
        OverviewData OverviewData { get; }
        [JsonIgnore]

        TcpMasterData Master { get; set; }
        TcpSlaveData Slave { get; set; }
        bool IsInitialized { get; }
        bool Connect();

        DataStatus ReadAll();
        DataStatus ReadData(string property);
        DataStatus ReadOverviewData();
        DataStatus WriteAll();
        DataStatus WriteData(string property, string data);
        DataStatus WriteData(string property);
        DataStatus WriteData(List<string> properties);

        object GetPropertyValue(string property);
    }
}
