// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OutdoorModuleDashboard.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    #region Using Directives

    using System;

    #endregion

    public class OutdoorModuleDashboard
    {
        #region Public Properties

        public DateTimeOffset TimeUTC { get; set; }
        public double Temperature { get; set; }
        public string TempTrend { get; set; } = string.Empty;
        public double Humidity { get; set; }
        public DateTimeOffset DateMaxTemp { get; set; }
        public DateTimeOffset DateMinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }

        #endregion
    }
}
