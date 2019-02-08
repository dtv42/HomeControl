// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusMonitor.cs" company="DTV-Online">
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
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using HomeDataLib;
    using HomeDataWeb.Hubs;
    using HomeDataWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected Fronius instance data every minute.
    /// </summary>
    public class HomeDataMonitor : TimedService
    {
        #region Private Data Members

        private readonly IHomeData _homedata;
        private readonly IHubContext<HomeDataHub> _hub;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="HomeDataMonitor"/> class.
        /// </summary>
        /// <param name="homedata">The HomeData instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public HomeDataMonitor(IHomeData homedata,
                               IHubContext<HomeDataHub> hub,
                               ILogger<HomeDataMonitor> logger,
                               IOptions<AppSettings> options,
                               IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _homedata = homedata;
            _hub = hub;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the startup operation.
        /// </summary>
        protected override async Task DoStartAsync()
        {
            try
            {
                // Update the live data (total and phase data).
                _logger?.LogDebug("HomeDataMonitor: DoStart...");
                await _homedata?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _homedata.Data);
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
                _logger?.LogDebug("HomeDataMonitor: DoWorkAsync...");
                await _homedata?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _homedata.Data);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion
    }
}
