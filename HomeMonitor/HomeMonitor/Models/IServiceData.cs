// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeMonitor.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Net.Http;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    interface IServiceData
    {
        string Name { get; set; }
        string Message { get; set; }
        List<RowData> Rows { get; set; }

        bool UpdateValues();
    }
}
