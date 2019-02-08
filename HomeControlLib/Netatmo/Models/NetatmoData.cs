// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Netatmo.Models
{
    #region Using Directives

    using DataValueLib;

    #endregion

    /// <summary>
    /// This class holds a the mapped Netatmo Station data.
    /// </summary>
    public class NetatmoData : DataValue
    {
        #region Public Properties

        public StationDeviceData Device { get; set; } = new StationDeviceData();

        public UserData User { get; set; } = new UserData();

        public ResponseData Response { get; set; } = new ResponseData();

        #endregion
    }
}
