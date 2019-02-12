// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusMonitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using FroniusLib;
    using FroniusWeb.Hubs;
    using FroniusWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected Fronius instance data every minute.
    /// </summary>
    public class FroniusMonitor : TimedService
    {
        #region Private Data Members

        private readonly IFronius _fronius;
        private readonly IHubContext<FroniusHub> _hub;
        private DateTime _currentdate = DateTime.UtcNow;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="FroniusMonitor"/> class.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public FroniusMonitor(IFronius fronius,
                              IHubContext<FroniusHub> hub,
                              ILogger<FroniusMonitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _fronius = fronius;
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
                // Update all data.
                _logger?.LogDebug("FroniusMonitor: DoStart...");
                await _fronius?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateCommon", _fronius.CommonData);
                await _hub.Clients.All.SendAsync("UpdatePhase", _fronius.PhaseData);
                await _hub.Clients.All.SendAsync("UpdateInverter", _fronius.InverterInfo);
                await _hub.Clients.All.SendAsync("UpdateMinMax", _fronius.MinMaxData);
                await _hub.Clients.All.SendAsync("UpdateData", _fronius.Data);
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
                // Update the live data.
                _logger?.LogDebug("FroniusMonitor: DoWork...");
                await _fronius?.ReadCommonDataAsync();
                await _fronius?.ReadPhaseDataAsync();
                await _fronius?.ReadInverterInfoAsync();
                await _fronius?.ReadMinMaxDataAsync();
                await _hub.Clients.All.SendAsync("UpdateCommon", _fronius.CommonData);
                await _hub.Clients.All.SendAsync("UpdatePhase", _fronius.PhaseData);
                await _hub.Clients.All.SendAsync("UpdateInverter", _fronius.InverterInfo);
                await _hub.Clients.All.SendAsync("UpdateMinMax", _fronius.MinMaxData);

                // Check for new day and update logger data.
                if (_currentdate.Date != DateTime.Now.Date)
                {
                    _currentdate = DateTime.Now;
                    await _fronius?.ReadLoggerInfoAsync();
                    await _hub.Clients.All.SendAsync("UpdateLogger", _fronius.LoggerInfo);
                }

                await _hub.Clients.All.SendAsync("UpdateData", _fronius.Data);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion
    }
}
