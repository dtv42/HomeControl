// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XunitLoggerProvider.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace Xunit.Logger
{
    #region Using Directives

    using Microsoft.Extensions.Logging;
    using System;
    using Xunit.Abstractions;

    #endregion Using Directives

    /// <summary>
    /// Helper class to support logging for xunit tests.
    /// </summary>
    public class XunitLoggerProvider : ILoggerProvider
    {
        #region Private Data Members

        private readonly ITestOutputHelper _testOutputHelper;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XunitLogger"/> class.
        /// </summary>
        /// <param name="testOutputHelper"></param>
        public XunitLoggerProvider(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
        }

        /// <summary>
        /// Creates a xunit logger.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) => new XunitLogger(_testOutputHelper, categoryName);

        #endregion Public Methods
    }
}