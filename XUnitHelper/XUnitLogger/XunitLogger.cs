// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XunitLogger.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace Xunit.Logger
{
    #region Using Directives

    using System;
    using Microsoft.Extensions.Logging;
    using Xunit.Abstractions;

    #endregion Using Directives

    /// <summary>
    /// Helper class to support logging for xunit tests.
    /// </summary>
    public class XunitLogger : ILogger
    {
        #region Private Data Members

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _categoryName;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XunitLogger"/> class.
        /// </summary>
        /// <param name="testOutputHelper"></param>
        /// <param name="categoryName"></param>
        public XunitLogger(ITestOutputHelper testOutputHelper, string categoryName)
        {
            _testOutputHelper = testOutputHelper;
            _categoryName = categoryName;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Helper method.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => NoopDisposable.Instance;

        /// <summary>
        /// Allows logging.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// Simplified logging.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _testOutputHelper.WriteLine($"{_categoryName} [{eventId}] {formatter(state, exception)}");

            if (exception != null)
            {
                _testOutputHelper.WriteLine(exception.ToString());
            }
        }

        #endregion Public Methods

        #region Private Types

        /// <summary>
        /// Helper class.
        /// </summary>
        private class NoopDisposable : IDisposable
        {
            public static NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            { }
        }

        #endregion Private Types
    }
}