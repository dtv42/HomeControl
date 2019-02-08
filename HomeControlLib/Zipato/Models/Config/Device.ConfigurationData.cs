// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Device.ConfigurationData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Config.Device
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///
    /// </summary>
    public class ConfigurationData
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public int? FactoryDefault { get; set; }
        public bool? MapEntry { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public string Unit { get; set; }
        public string FieldType { get; set; }
        public bool? Hidden { get; set; }
        public string Description { get; set; }
        public string ByteSize { get; set; }
        public bool? MultiSelect { get; set; }
        public List<ItemData> Items { get; set; } = new List<ItemData> { };
    }
}
