// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnOff.cs" company="DTV-Online">
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

    public class OnOff
    {
        #region Public Properties

        /// <summary>
        /// The UUID of the OnOff switch.
        /// </summary>
        public Guid Uuid { get; }

        /// <summary>
        /// The name the OnOff switch.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The STATE value of the OnOff switch.
        /// </summary>
        public ValueInfo<bool> State { get; private set; } = new ValueInfo<bool>();

        #endregion
    }
}
