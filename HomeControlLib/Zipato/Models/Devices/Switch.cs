// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Switch.cs" company="DTV-Online">
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

    public class Switch
    {
        #region Public Properties

        /// <summary>
        /// The UUID of the boolean device.
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// The name the boolean device.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The STATE value of the boolean device.
        /// </summary>
        public ValueInfo<bool> State { get; set; } = new ValueInfo<bool>();

        #endregion
    }
}
