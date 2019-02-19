// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Camera.cs" company="DTV-Online">
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
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using HomeControlLib.Zipato.Models.Data;

    #endregion

    /// <summary>
    /// This class represents a Zipato camera.
    /// </summary>
    public class Camera
    {
        #region Public Properties

        /// <summary>
        /// The name of the scene.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The UUID of the camera.
        /// </summary>
        public Guid Uuid { get; set; }

        #endregion
    }
}
