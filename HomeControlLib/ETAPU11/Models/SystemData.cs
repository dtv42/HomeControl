// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.ETAPU11.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    public class SystemData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The ETAPU11 property subset.
        /// </summary>
        public double OutsideTemperature { get; set; }

        #endregion
    }
}
