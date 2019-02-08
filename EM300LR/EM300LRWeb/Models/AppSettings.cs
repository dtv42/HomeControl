// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRWeb.Models
{
    #region Using Directives

    using EM300LRLib;
    using EM300LRLib.Models;

    #endregion Using Directives

    /// <summary>
    /// Helper class to hold application specific settings.
    /// </summary>
    public class AppSettings : SettingsData
    {
        #region Public Properties

        /// <summary>
        /// The Swagger options.
        /// </summary>
        public SwaggerOptions Swagger { get; set; } = new SwaggerOptions();

        #endregion Public Properties
    }
}