// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="DTV-Online">
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

    using CommandLine.Core.Hosting.Abstractions;

    #endregion Using Directives

    /// <summary>
    /// Base class for application commands providing logger, settings, and environment data members.
    /// </summary>
    /// <typeparam name="T">The settings class.</typeparam>
    public class BaseCommand<T> : BaseClass<T> where T : class, new()
    {
        #region Protected Data Members

        /// <summary>
        /// The hosting environment instance.
        /// </summary>
        protected readonly IHostingEnvironment _environment;

        #endregion Protected Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public BaseCommand(ILogger<BaseCommand<T>> logger,
                           IOptions<T> options,
                           IHostingEnvironment environment) : base(logger, options)
        {
            _environment = environment;
            _logger?.LogDebug($"BaseCommand<{typeof(T)}>()");
        }

        #endregion Constructors
    }
}