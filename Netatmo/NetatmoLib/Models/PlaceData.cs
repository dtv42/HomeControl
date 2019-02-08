// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaceData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoLib.Models
{
    public class PlaceData
    {
        #region Public Properties

        public double Altitude { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool ImproveLocProposed { get; set; }
        public LocationData Location { get; set; } = new LocationData();
        public string Timezone { get; set; } = string.Empty;

        #endregion
    }

    public class StationPlaceData : PlaceData
    {
        #region Public Properties

        public string GeoIpCity { get; set; } = string.Empty;

        #endregion
    }
}
