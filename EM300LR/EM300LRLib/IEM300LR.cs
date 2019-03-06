// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEM300LR.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRLib
{
    #region Using Directives

    using System.Threading.Tasks;

    using DataValueLib;
    using EM300LRLib.Models;

    #endregion Using Directives

    public interface IEM300LR : IHttpClientSettings, ISettingsData
    {
        EM300LRData Data { get; }
        TotalData TotalData { get; }
        Phase1Data Phase1Data { get; }
        Phase2Data Phase2Data { get; }
        Phase3Data Phase3Data { get; }

        bool IsLocked { get; }

        Task<bool> CheckAccess();
        DataStatus ReadAll();

        Task<DataStatus> ReadAllAsync();

        object GetPropertyValue(string property);
    }
}
