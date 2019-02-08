// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RGBControl.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Devices
{
    #region Using Directives

    using System;
    using Newtonsoft.Json;

    #endregion

    public class RGBControl
    {
        #region Public Properties

        /// <summary>
        /// The UUID of the RGB controller.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name of the RGB controller.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The INTENSITY value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Intensity { get; private set; } = new ValueInfo<int>();

        /// <summary>
        /// The RED value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Red { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The GREEN value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Green { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The BLUE value of the RGB controller.
        /// </summary>
        public ValueInfo<int> Blue { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The ColdWhite value of the RGB controller.
        /// </summary>
        public ValueInfo<int> ColdWhite { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The WarmWhite value of the RGB controller.
        /// </summary>
        public ValueInfo<int> WarmWhite { get; set; } = new ValueInfo<int>();

        /// <summary>
        /// The RGB value of the RGB controller.
        /// </summary>
        [JsonIgnore]
        public ValueInfoString RGB { get; set; } = new ValueInfoString();

        /// <summary>
        /// The RGBW value of the RGB controller.
        /// </summary>
        [JsonIgnore]
        public ValueInfoString RGBW { get; set; } = new ValueInfoString();

        #endregion
    }
}
