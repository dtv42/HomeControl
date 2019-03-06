// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INetatmo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib
{
    #region Using Directives

    using System.Threading.Tasks;

    using DataValueLib;
    using NetatmoLib.Models;

    #endregion

    public interface INetatmo : IHttpClientSettings, ISettingsData
    {
        NetatmoData Station { get; }

        bool IsLocked { get; }
        bool CheckAccess();

        DataStatus ReadAll();
        Task<DataStatus> ReadAllAsync();

        object GetPropertyValue(string property);
    }
}
