// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeliosClient.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Lib
{
    #region Using Directives

    using NModbusLib;
    using DataValueLib;
    using KWLEC200Lib.Models;

    #endregion

    public interface IHeliosClient : ITcpClient
    {
        DataStatus ReadProperty(KWLEC200Data data, string property);
        DataStatus WriteProperty(KWLEC200Data data, string property);
    }
}
