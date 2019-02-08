// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseHub.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BaseClassLib
{
    #region Using Directives

    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.SignalR;

    #endregion Using Directives

    /// <summary>
    /// Base class for a SignalR hub providing logger and settings data members.
    /// </summary>
    /// <typeparam name="T">The settings class.</typeparam>
    public class BaseHub<T> : Hub where T : class, new()
    {
        #region Protected Data Members

        /// <summary>
        /// The logger instance.
        /// </summary>
        protected readonly ILogger<BaseHub<T>> _logger;

        /// <summary>
        /// The settings instance.
        /// </summary>
        protected readonly T _settings;

        #endregion Protected Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHub"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public BaseHub(ILogger<BaseHub<T>> logger,
                       IOptions<T> options)
        {
            _logger = logger;
            _settings = options.Value;
            _logger?.LogDebug($"BaseHub<{typeof(T)}>()");
        }

        #endregion Constructors
    }
}