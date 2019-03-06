// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataLib
{
    #region Using Directives

    using System.Threading.Tasks;

    using DataValueLib;
    using HomeDataLib.Models;

    #endregion

    public interface IHomeData : ISettingsData
    {
        HomeValues Data { get; }
        MeterData Meter1 { get; }
        MeterData Meter2 { get; }

        bool IsLocked { get; }
        bool CheckAccess();

        DataStatus ReadAll();

        Task<DataStatus> ReadAllAsync(bool update = false);

        object GetPropertyValue(string property);
    }
}
