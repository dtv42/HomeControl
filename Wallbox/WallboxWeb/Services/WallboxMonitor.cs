// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxMonitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using WallboxLib;
    using WallboxWeb.Hubs;
    using WallboxWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected Wallbox instance data every minute.
    /// </summary>
    public class WallboxMonitor : TimedService
    {
        #region Private Data Members

        private readonly IWallbox _wallbox;
        private readonly IHubContext<WallboxHub> _hub;
        private DateTime _currentdate = DateTime.UtcNow;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="WallboxMonitor"/> class.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public WallboxMonitor(IWallbox wallbox,
                              IHubContext<WallboxHub> hub,
                              ILogger<WallboxMonitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _wallbox = wallbox;
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
                _logger?.LogDebug("WallboxMonitor: DoStart...");

                // Run ReadAllAsync only once.
                await _wallbox?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _wallbox.Data);
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
                // Update the report data.
                _logger?.LogDebug("WallboxMonitor: DoWork...");
                await _wallbox?.ReadReport1Async();
                await _wallbox?.ReadReport2Async();
                await _wallbox?.ReadReport3Async();
                await _wallbox?.ReadReport100Async();
                await _hub.Clients.All.SendAsync("UpdateReport1", _wallbox.Report1);
                await _hub.Clients.All.SendAsync("UpdateReport2", _wallbox.Report2);
                await _hub.Clients.All.SendAsync("UpdateReport3", _wallbox.Report3);
                await _hub.Clients.All.SendAsync("UpdateReport100", _wallbox.Report100);

                // Check for new day and update all report data.
                if (_currentdate.Date != DateTime.Now.Date)
                {
                    _currentdate = DateTime.Now;
                    await _wallbox?.ReadAllAsync();
                    await _hub.Clients.All.SendAsync("UpdateData", _wallbox.Data);
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
