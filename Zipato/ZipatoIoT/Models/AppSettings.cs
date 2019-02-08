// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoIoT.Models
{
    #region Using Directives

    using ZipatoLib.Models;

    #endregion

    /// <summary>
    /// Helper class holding all application settings.
    /// </summary>
    internal class AppSettings : SettingsData
    {
        public class WebServerAddresses
        {
            public string Fronius { get; set; } = string.Empty;
            public string EM300LR { get; set; } = string.Empty;
            public string ETAPU11 { get; set; } = string.Empty;
            public string KWLEC200 { get; set; } = string.Empty;
            public string Netatmo { get; set; } = string.Empty;
            public string HomeData { get; set; } = string.Empty;
            public string Wallbox { get; set; } = string.Empty;
            public string Zipato { get; set; } = string.Empty;
        }

        public class VirtualMeterIndexes
        {
            public int Outdoor { get; set; }
            public int Indoor { get; set; }
            public int ETAPU11 { get; set; }
            public int EM300LR { get; set; }
            public int Fronius { get; set; }
            public int KWLEC200 { get; set; }
            public int HomeData { get; set; }
            public int Wallbox { get; set; }
        }

        public WebServerAddresses Servers { get; set; } = new WebServerAddresses();
        public VirtualMeterIndexes Meters { get; set; } = new VirtualMeterIndexes();
    }
}
