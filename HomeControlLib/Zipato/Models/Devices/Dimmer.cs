// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dimmer.cs" company="DTV-Online">
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

    #endregion

    public class Dimmer
    {
        #region Public Properties

        /// <summary>
        /// The UUID of the dimmer.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name of the dimmer.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The INTENSITY value of the dimmer.
        /// </summary>
        public ValueInfo<int> Intensity { get; private set; } = new ValueInfo<int>();

        #endregion
    }
}
