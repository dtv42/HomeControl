// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWallbox.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxLib
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DataValueLib;
    using WallboxLib.Models;

    #endregion

    public interface IWallbox : IUdpClientSettings
    {
        WallboxData Data { get; }
        Report1Data Report1 { get; }
        Report2Data Report2 { get; }
        Report3Data Report3 { get; }
        ReportsData Report100 { get; }
        List<ReportsData> Reports { get; }
        InfoData Info { get; }

        bool IsLocked { get; }
        bool CheckAccess();

        DataStatus ReadAll();
        DataStatus ReadReport1();
        DataStatus ReadReport2();
        DataStatus ReadReport3();
        DataStatus ReadReport100();
        DataStatus ReadReports();
        DataStatus ReadReport(int id);
        DataStatus ReadProperty(string property);

        Task<DataStatus> ReadAllAsync();
        Task<DataStatus> ReadReport1Async();
        Task<DataStatus> ReadReport2Async();
        Task<DataStatus> ReadReport3Async();
        Task<DataStatus> ReadReport100Async();
        Task<DataStatus> ReadReportsAsync();
        Task<DataStatus> ReadReportAsync(int id);
        Task<DataStatus> ReadPropertyAsync(string property);

        object GetPropertyValue(string property);
    }
}
