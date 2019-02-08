// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseClass.cs" company="DTV-Online">
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

    #endregion Using Directives

    /// <summary>
    /// Base class providing a logger data member.
    /// </summary>
    public class BaseClass
    {
        #region Protected Data Members

        /// <summary>
        /// The logger instance.
        /// </summary>
        protected readonly ILogger<BaseClass> _logger;

        #endregion Protected Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClass"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public BaseClass(ILogger<BaseClass> logger)
        {
            _logger = logger;
            _logger?.LogDebug("BaseClass()");
        }

        #endregion Constructors
    }

    /// <summary>
    /// Base class providing settings and logger data members.
    /// </summary>
    /// <typeparam name="T">The settings class.</typeparam>
    public class BaseClass<T> : BaseClass where T : class, new()
    {
        #region Protected Data Members

        /// <summary>
        /// The settings instance.
        /// </summary>
        protected readonly T _settings;

        #endregion Protected Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClass"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        public BaseClass(ILogger<BaseClass<T>> logger,
                         IOptions<T> options) : base(logger)
        {
            _settings = options.Value;
            _logger?.LogDebug($"BaseClass<{typeof(T)}>()");
        }

        #endregion Constructors
    }
}