// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoxConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Config
{
    public class BoxConfig : ZipaboxConfig
    {
        public string ClusterBoxSerial { get; set; }
        public string Currency { get; set; }
        public string DateFormat { get; set; }
        public bool? EthernetRequired { get; set; }
        public bool? KeepOnline { get; set; }
        public int? LedBrightness { get; set; }
        public string Mtu { get; set; }
        public string Name { get; set; }
        public string Proxy { get; set; }
        public bool? SetupComplete { get; set; }
        public string SimAPN { get; set; }
        public string SimPassword { get; set; }
        public string SimPIN { get; set; }
        public string SimUsername { get; set; }
        public string StaticDns1 { get; set; }
        public string StaticDns2 { get; set; }
        public string StaticGateway { get; set; }
        public string StaticIp { get; set; }
        public string StaticNetmask { get; set; }
        public string TemperatureScale { get; set; }
        public string TimeFormat { get; set; }
        public int? TimeZone { get; set; }
        public int? TimeZoneId { get; set; }
        public bool? WifiDefault { get; set; }
        public bool? WifiRequired { get; set; }
    }
}
