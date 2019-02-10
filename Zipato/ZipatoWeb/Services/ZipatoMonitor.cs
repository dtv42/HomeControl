// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoMonitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using ZipatoLib;
    using ZipatoWeb.Hubs;
    using ZipatoWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected Zipato instance data every minute.
    /// </summary>
    public class ZipatoMonitor : TimedService
    {
        #region Private Data Members

        private readonly IZipato _zipato;
        private readonly IHubContext<ZipatoHub> _hub;
        private DateTime _currentdate = DateTime.Now;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ZipatoMonitor"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public ZipatoMonitor(IZipato zipato,
                              IHubContext<ZipatoHub> hub,
                              ILogger<ZipatoMonitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _zipato = zipato;
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
                _logger?.LogDebug("ZipatoMonitor: DoStart...");

                // Read just minimal data and update.
                await _zipato?.ReadAllDataAsync();
                await _hub.Clients.All.SendAsync("UpdateValues", _zipato.Data.Values);
                await _hub.Clients.All.SendAsync("UpdateDevices", _zipato.Devices);
                await _hub.Clients.All.SendAsync("UpdateSensors", _zipato.Sensors);
                await _hub.Clients.All.SendAsync("UpdateOthers", _zipato.Others);

                // Run ReadAllAsync only once.
                await _zipato?.ReadAllAsync();
                await _hub.Clients.All.SendAsync("UpdateData", _zipato.Data);
                await _hub.Clients.All.SendAsync("UpdateValues", _zipato.Data.Values);
                await _hub.Clients.All.SendAsync("UpdateDevices", _zipato.Devices);
                await _hub.Clients.All.SendAsync("UpdateSensors", _zipato.Sensors);
                await _hub.Clients.All.SendAsync("UpdateOthers", _zipato.Others);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoStartAsync: Exception");
            }
        }

        /// <summary>
        /// Executes the work operation every minute.
        /// </summary>
        protected override async Task DoWorkAsync()
        {
            try
            {
                _logger?.LogDebug("ZipatoMonitor: DoWork...");

                // Update just data values and update.
                await _zipato?.ReadAllValuesAsync();
                await _hub.Clients.All.SendAsync("UpdateValues", _zipato.Data.Values);
                await _hub.Clients.All.SendAsync("UpdateDevices", _zipato.Devices);
                await _hub.Clients.All.SendAsync("UpdateSensors", _zipato.Sensors);
                await _hub.Clients.All.SendAsync("UpdateOthers", _zipato.Others);

                // Check for new day and update other data.
                if (_currentdate.Date != DateTime.Now.Date)
                {
                    _currentdate = DateTime.Now;
                    await _zipato?.ReadAllAsync();
                    await _hub.Clients.All.SendAsync("UpdateData", _zipato.Data);
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
