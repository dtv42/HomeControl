// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindGaugeDashboard.cs" company="DTV-Online">
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
    using System.Collections.Generic;

    #endregion

    public class WindGaugeDashboard
    {
        #region Public Methods

        public DateTimeOffset TimeUTC { get; set; }
        public double WindAngle { get; set; }
        public double WindStrength { get; set; }
        public double GustAngle { get; set; }
        public double GustStrength { get; set; }
        public double MaxWindAngle { get; set; }
        public double MaxWindStrength { get; set; }
        public DateTimeOffset DateMaxWindStrength { get; set; }

        #endregion
    }
}
