// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rule.PropertiesData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Info.Rule
{
    #region Using Directives

    using System;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// Class providing properties for Zipato rule info data.
    /// </summary>
    public class PropertiesData
    {
        public Guid? ScheduleUuid { get; set; }
        public JObject LeftSide { get; set; }
        public JObject RightSide { get; set; }
        public string Action { get; set; }
        public string EventType { get; set; }
        public string EndpointUuid { get; set; }
        public string Type { get; set; }
        public string ClusterType { get; set; }
        public string Attribute { get; set; }
        public bool? Value { get; set; }
        public string Op { get; set; }
    }
}
