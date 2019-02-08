// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusLib.Models
{
    #region Using Directives

    using System;

    #endregion

    public class ResponseData
    {
        public StatusData Status { get; set; } = new StatusData();
        public DateTimeOffset Timestamp { get; set; } = new DateTimeOffset();
    }
}
