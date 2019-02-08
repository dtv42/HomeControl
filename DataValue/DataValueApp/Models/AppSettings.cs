// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueApp.Models
{
    /// <summary>
    /// The application settings. The class contains all application settings as properties and is configured
    /// using application configuration files (appsettings.json), environment variables, and command line options.
    /// </summary>
    public sealed class AppSettings
    {
        public AppData TestData { get; set; } = new AppData();
    }
}