// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BControlMonitor.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using BControlLib;
    using BControlWeb.Hubs;
    using BControlWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected BControl instance data every minute.
    /// </summary>
    public class BControlMonitor : TimedService
    {
        #region Private Data Members

        private readonly IBControl _bcontrol;
        private readonly IHubContext<BControlHub> _hub;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="BControlMonitor"/> class.
        /// </summary>
        /// <param name="bcontrol">The BControl instance.</param>
        /// <param name="hub">The BControl data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public BControlMonitor(IBControl bcontrol,
                               IHubContext<BControlHub> hub,
                               ILogger<BControlMonitor> logger,
                               IOptions<AppSettings> options,
                               IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _bcontrol = bcontrol;
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
                _logger?.LogDebug("BControlMonitor: DoStart...");

                await _bcontrol?.ReadBlockAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _bcontrol.Data);
                await _hub.Clients.All.SendAsync("UpdateInternal", _bcontrol.InternalData);
                await _hub.Clients.All.SendAsync("UpdateEnergy", _bcontrol.EnergyData);
                await _hub.Clients.All.SendAsync("UpdatePnP", _bcontrol.PnPData);
                await _hub.Clients.All.SendAsync("UpdateSunSpec", _bcontrol.SunSpecData);
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
                _logger?.LogDebug("BControlMonitor: DoWork...");
                await _bcontrol?.ReadBlockAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _bcontrol.Data);
                await _hub.Clients.All.SendAsync("UpdateInternal", _bcontrol.InternalData);
                await _hub.Clients.All.SendAsync("UpdateEnergy", _bcontrol.EnergyData);
                await _hub.Clients.All.SendAsync("UpdatePnP", _bcontrol.PnPData);
                await _hub.Clients.All.SendAsync("UpdateSunSpec", _bcontrol.SunSpecData);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion
    }
}
