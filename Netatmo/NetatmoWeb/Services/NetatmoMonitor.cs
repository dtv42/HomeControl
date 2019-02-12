// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoMonitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using NetatmoLib;
    using NetatmoWeb.Hubs;
    using NetatmoWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected Netatmo instance data every 5 minutes.
    /// </summary>
    public class NetatmoMonitor : TimedService
    {
        #region Private Data Members

        private readonly INetatmo _netatmo;
        private readonly IHubContext<NetatmoHub> _hub;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="NetatmoMonitor"/> class.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public NetatmoMonitor(INetatmo netatmo,
                              IHubContext<NetatmoHub> hub,
                              ILogger<NetatmoMonitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _netatmo = netatmo;
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
                _logger?.LogDebug("NetatmoMonitor: DoStart...");

                // Update data.
                await _netatmo?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _netatmo.Station);
                await _hub.Clients.All.SendAsync("UpdateMain", _netatmo.Station.Device.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateOutdoor", _netatmo.Station.Device.OutdoorModule.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateIndoor1", _netatmo.Station.Device.IndoorModule1.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateIndoor2", _netatmo.Station.Device.IndoorModule2.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateIndoor3", _netatmo.Station.Device.IndoorModule3.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateRain", _netatmo.Station.Device.RainGauge.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateWind", _netatmo.Station.Device.WindGauge.DashboardData);
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
                _logger?.LogDebug("NetatmoMonitor: DoWork...");

                if ((DateTime.Now.Minute % 5) == 0)
                {
                    // Read and update all data values every 5 minutes.
                    await _netatmo?.ReadAllAsync();
                }

                await _hub.Clients.All.SendAsync("UpdateData", _netatmo.Station);
                await _hub.Clients.All.SendAsync("UpdateMain", _netatmo.Station.Device.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateOutdoor", _netatmo.Station.Device.OutdoorModule.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateIndoor1", _netatmo.Station.Device.IndoorModule1.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateIndoor2", _netatmo.Station.Device.IndoorModule2.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateIndoor3", _netatmo.Station.Device.IndoorModule3.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateRain", _netatmo.Station.Device.RainGauge.DashboardData);
                await _hub.Clients.All.SendAsync("UpdateWind", _netatmo.Station.Device.WindGauge.DashboardData);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion
    }
}
