// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimedService.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using HomeDataWeb.Models;

    #endregion Using Directives

    /// <summary>
    /// Timed background task based on code from https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.1
    /// </summary>
    public abstract class TimedService : BaseService<AppSettings>, IHostedService, IDisposable
    {
        #region Private Data Members

        private Timer _timer;
        private bool _initialized;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedService"/> class using dependency injection.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        /// <param name="environment"></param>
        protected TimedService(ILogger<TimedService> logger,
                               IOptions<AppSettings> options,
                               IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("TimedService()");
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Start the async operation.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> object that represents an asynchronous operation.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger?.LogDebug($"TimedService starting.");
            _timer = new Timer(OnTimerElapsed, null, 0, -1);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop the async operation.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> object that represents an asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation($"TimedService stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Implementing basic dispose pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The timer callback method.
        /// </summary>
        /// <param name="state">The timer callback state object.</param>
        public async void OnTimerElapsed(object state)
        {
            _logger?.LogDebug($"TimedService timer elapsed.");

            // Run start method if not yet initialized.
            if (!_initialized)
            {
                await DoStartAsync();
                _initialized = true;
            }
            else
            {
                await DoWorkAsync();
            }

            _timer.Change(GetDueTime(), Timeout.Infinite);
        }

        #endregion Public Methods

        #region Virtual Methods

        /// <summary>
        /// Actual resource cleanup when disposing.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }
        }

        /// <summary>
        /// Derived classes should override this.
        /// </summary>
        protected virtual async Task DoStartAsync()
            => await Task.Delay(1);

        /// <summary>
        /// Derived classes should override this.
        /// </summary>
        protected virtual async Task DoWorkAsync()
            => await Task.Delay(1);

        #endregion Virtual Methods

        #region Private Methods

        /// <summary>
        /// Returns the milliseconds to the next minute.
        /// </summary>
        /// <returns>The milliseconds to the next minute.</returns>
        private int GetDueTime()
        {
            DateTime now = DateTime.Now;
            return (60 - now.Second) * 1000 - now.Millisecond;
        }

        #endregion Private Methods
    }
}
