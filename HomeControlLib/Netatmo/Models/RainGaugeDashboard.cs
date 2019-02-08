// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RainGaugeDashboard.cs" company="DTV-Online">
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

    public class RainGaugeDashboard
    {
        #region Public Properties

        public DateTimeOffset TimeUTC { get; set; }
        public double Rain { get; set; }
        public double SumRain1 { get; set; }
        public double SumRain24 { get; set; }

        #endregion
    }
}
