// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InverterInfo.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    /// <summary>
    /// Class holding selected data from the Fronius Symo 8.2-3-M inverter.
    /// </summary>
    public class InverterInfo : DataValue
    {
        #region Public Properties

        public string Index { get; set; } = string.Empty;
        public int DeviceType { get; set; }
        public int PVPower { get; set; }
        public string CustomName { get; set; } = string.Empty;
        public bool Show { get; set; }
        public string UniqueID { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
        public StatusCodes StatusCode { get; set; } = new StatusCodes();

        #endregion
    }
}
