// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Monitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Web.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using KWLEC200Lib;
    using KWLEC200Web.Hubs;
    using KWLEC200Web.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected KWLEC200 instance data every minute.
    /// </summary>
    public class KWLEC200Monitor : TimedService
    {
        #region Private Data Members

        private readonly IKWLEC200 _kwlec200;
        private readonly IHubContext<KWLEC200Hub> _hub;
        private DateTime _currentdate = DateTime.Now;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="KWLEC200Monitor"/> class.
        /// </summary>
        /// <param name="kwlec200">The KWLEC200 instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public KWLEC200Monitor(IKWLEC200 kwlec200,
                              IHubContext<KWLEC200Hub> hub,
                              ILogger<KWLEC200Monitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _kwlec200 = kwlec200;
            _hub = hub;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the start operation just once.
        /// </summary>
        protected override async Task DoStartAsync()
        {
            try
            {
                _logger?.LogDebug("KWLEC200Monitor: DoStart...");

                // Update minimal required data (overview data).
                _kwlec200?.ReadOverviewData();
                await _hub.Clients.All.SendAsync("UpdateOverview", _kwlec200.OverviewData);

                // Run ReadAllAsync only once.
                _kwlec200?.ReadAll();
                await _hub.Clients.All.SendAsync("UpdateData", _kwlec200.Data);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoStartAsync: Exception");
            }
        }

        /// <summary>
        /// Executes the update operation every minute.
        /// </summary>
        protected override async Task DoWorkAsync()
        {
            try
            {
                _logger?.LogDebug("KWLEC200Monitor: DoWork...");
                _kwlec200?.ReadOverviewData();
                await _hub.Clients.All.SendAsync("UpdateOverview", _kwlec200.OverviewData);

                // Check for new day and update other data.
                if (_currentdate.Date != DateTime.Now.Date)
                {
                    _currentdate = DateTime.Now;
                    _kwlec200?.ReadAll();
                    await _hub.Clients.All.SendAsync("UpdateData", _kwlec200.Data);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion
    }
}
