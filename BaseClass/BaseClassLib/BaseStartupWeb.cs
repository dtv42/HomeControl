// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseStartupWeb.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BaseClassLib
{
    #region Using Directives

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    #endregion

    /// <summary>
    /// Base class for application startup providing logging and configuration.
    /// </summary>
    /// <typeparam name="T">The settings class.</typeparam>
    public class BaseStartupWeb<T> where T : class, new()
    {
        #region Protected Data Members

        protected readonly T _settings = new T();

        #endregion Protected Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// Using dependency injection, the application specific settings and configuration are initialized.
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public BaseStartupWeb(IHostingEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection("AppSettings")?.Bind(_settings);
        }

        #endregion Constructors

        #region Public Properties

        public IConfiguration Configuration { get; }

        #endregion Public Properties
    }
}
