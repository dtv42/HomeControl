﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LuminanceSensor.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Sensors
{
    #region Using Directives

    using System;

    #endregion

    public class LuminanceSensor
    {
        #region Public Properties

        /// <summary>
        /// The name of the plug.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The UUID of the plug.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The cummulative consumption value of the plug.
        /// </summary>
        public ValueInfo<double> Luminance { get; private set; } = new ValueInfo<double>();

        #endregion
    }
}