// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plug.cs" company="DTV-Online">
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

    public class Plug
    {
        #region Public Properties

        /// <summary>
        /// The name of the plug.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The UUID of the plug.
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// The boolean state of the plug.
        /// </summary>
        public ValueInfo<bool> State { get; set; } = new ValueInfo<bool>();

        /// <summary>
        /// The cummulative consumption value of the plug.
        /// </summary>
        public ValueInfo<double> CummulativeConsumption { get; set; } = new ValueInfo<double>();

        /// <summary>
        /// The current consumption value of the plug.
        /// </summary>
        public ValueInfo<double> CurrentConsumption { get; set; } = new ValueInfo<double>();

        #endregion
    }
}
