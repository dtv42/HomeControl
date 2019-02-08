// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Wallbox.Models
{
    #region Using Directives

    using System.Collections.Generic;

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds a the mapped Wallbox data.
    /// </summary>
    public class WallboxData : DataValue
    {
        #region Public Properties

        /// <summary>
        /// The Report1 property holds all Wallbox UDP data from report 1.
        /// </summary>
        public Report1Udp Report1 { get; set; } = new Report1Udp();

        /// <summary>
        /// The Report2 property holds all Wallbox UDP data from report 2.
        /// </summary>
        public Report2Udp Report2 { get; set; } = new Report2Udp();

        /// <summary>
        /// The Report3 property holds all Wallbox UDP data from report 3.
        /// </summary>
        public Report3Udp Report3 { get; set; } = new Report3Udp();

        /// <summary>
        /// The Reports property holds all Wallbox UDP data from report 100.
        /// </summary>
        public ReportsUdp Report100 { get; set; } = new ReportsUdp();

        /// <summary>
        /// The Reports property holds all Wallbox UDP data from report 101 - 130.
        /// </summary>
        public List<ReportsUdp> Reports { get; set; } = new List<ReportsUdp> { };

        #endregion
    }
}
