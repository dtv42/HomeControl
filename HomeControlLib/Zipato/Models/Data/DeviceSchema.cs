// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceSchema.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    #region Using Directives

    using Newtonsoft.Json.Schema;

    using HomeControlLib.Zipato.Models.Config;

    #endregion

    public class DeviceSchema
    {
        public JSchema Schema { get; set; }
        public UiConfig UiConfig { get; set; }
        public DeviceConfig Config { get; set; }
    }
}
