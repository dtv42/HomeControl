// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusMonitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using EM300LRLib;
    using EM300LRWeb.Hubs;
    using EM300LRWeb.Models;

    #endregion Using Directives

    /// <summary>
    /// Monitor service updating selected EM300LR instance data every minute.
    /// </summary>
    public class EM300LRMonitor : TimedService
    {
        #region Private Data Members

        private readonly IEM300LR _em300lr;
        private readonly IHubContext<EM300LRHub> _hub;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="EM300LRMonitor"/> class.
        /// </summary>
        /// <param name="em300lr">The Fronius instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public EM300LRMonitor(IEM300LR em300lr,
                              IHubContext<EM300LRHub> hub,
                              ILogger<EM300LRMonitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _em300lr = em300lr;
            _hub = hub;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Executes the startup operation.
        /// </summary>
        protected override async Task DoStartAsync()
        {
            try
            {
                // Update the live data (total and phase data).
                _logger?.LogDebug("EM300LRMonitor: DoStart...");
                await _em300lr?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _em300lr.Data);
                await _hub.Clients.All.SendAsync("UpdateTotal", _em300lr.TotalData);
                await _hub.Clients.All.SendAsync("UpdatePhase1", _em300lr.Phase1Data);
                await _hub.Clients.All.SendAsync("UpdatePhase2", _em300lr.Phase2Data);
                await _hub.Clients.All.SendAsync("UpdatePhase3", _em300lr.Phase3Data);
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
                // Update the live data (total and phase data).
                _logger?.LogDebug("EM300LRMonitor: DoWork...");
                await _em300lr?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _em300lr.Data);
                await _hub.Clients.All.SendAsync("UpdateTotal", _em300lr.TotalData);
                await _hub.Clients.All.SendAsync("UpdatePhase1", _em300lr.Phase1Data);
                await _hub.Clients.All.SendAsync("UpdatePhase2", _em300lr.Phase2Data);
                await _hub.Clients.All.SendAsync("UpdatePhase3", _em300lr.Phase3Data);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion Public Methods
    }
}