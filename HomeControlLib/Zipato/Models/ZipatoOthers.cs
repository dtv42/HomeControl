// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoScenes.cs" company="DTV-Online">
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
    using HomeControlLib.Zipato.Models.Others;

    #endregion

    /// <summary>
    /// Class holding attribute data values from the Zipato home controller.
    /// </summary>
    public class ZipatoOthers : DataValue
    {
        #region Public Properties

        public List<Camera> Cameras { get; set; } = new List<Camera> { };
        public List<Scene> Scenes { get; set; } = new List<Scene> { };

        #endregion
    }
}
