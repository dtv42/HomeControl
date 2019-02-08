// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoDevices.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models
{
    #region Using Directives

    using System.Collections.Generic;

    using DataValueLib;
    using HomeControlLib.Zipato.Models.Devices;

    #endregion

    /// <summary>
    /// Class holding attribute data values from the Zipato home controller.
    /// </summary>
    public class ZipatoDevices : DataValue
    {
        #region Public Properties

        public List<Switch> Switches { get; set; } = new List<Switch> { };
        public List<OnOff> OnOffSwitches { get; set; } = new List<OnOff> { };
        public List<Plug> Wallplugs { get; set; } = new List<Plug> { };
        public List<Dimmer> Dimmers { get; set; } = new List<Dimmer> { };
        public List<RGBControl> RGBControls { get; set; } = new List<RGBControl> { };

        #endregion
    }
}
