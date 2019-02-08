// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    public class SettingsData
    {
        public string Attribute { get; set; }
        public string AttributeUuid { get; set; }
        public string ClusterEndpoint { get; set; }
        public string ClusterClass { get; set; }
        public string Endpoint { get; set; }
        public string Value { get; set; }
    }
}
