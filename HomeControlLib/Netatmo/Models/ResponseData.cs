// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    #region Using Directives

    using System;

    #endregion

    public class ResponseData
    {
        #region Public Properties

        public string Status { get; set; } = string.Empty;
        public double TimeExec { get; set; }
        public DateTimeOffset TimeServer { get; set; }

        #endregion
    }
}
