// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeConfig.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace HomeControlLib.Zipato.Models.Config
{
    /// <summary>
    ///
    /// </summary>
    public class AttributeConfig
    {
        public string Compression { get; set; }
        public Dictionary<string, string> EnumValues { get; set; } = new Dictionary<string, string> { };
        public int? Expire { get; set; }
        public bool? Hidden { get; set; }
        public bool? Master { get; set; }
        public string Name { get; set; }
        public double? Precision { get; set; }
        public bool? Reported { get; set; }
        public int? Room { get; set; }
        public double? Scale { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
    }
}
