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

        bool IsLocked { get; }
        bool Connect();

        DataStatus ReadAll();
        DataStatus ReadProperty(string property);
        DataStatus ReadProperties(List<string> properties);
        DataStatus ReadOverviewData();
        DataStatus WriteAll();
        DataStatus WriteProperty(string property, string data);
        DataStatus WriteProperty(string property);
        DataStatus WriteProperties(List<string> properties);

        object GetPropertyValue(string property);
    }
}
