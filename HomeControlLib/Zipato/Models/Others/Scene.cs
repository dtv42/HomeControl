// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scene.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Others
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// This class represents a Zipato scene.
    /// </summary>
    public class Scene
    {
        #region Public Properties

        /// <summary>
        /// The name of the scene.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The UUID of the scene.
        /// </summary>
        public Guid Uuid { get; set; }

        #endregion
    }
}
