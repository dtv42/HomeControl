// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11Monitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Web.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using ETAPU11Lib;
    using ETAPU11Web.Hubs;
    using ETAPU11Web.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected ETAPU11 instance data every minute.
    /// </summary>
    public class ETAPU11Monitor : TimedService
    {
        #region Private Data Members

        private readonly IETAPU11 _etapu11;
        private readonly IHubContext<ETAPU11Hub> _hub;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ETAPU11Monitor"/> class.
        /// </summary>
        /// <param name="etapu11">The ETAPU11 instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public ETAPU11Monitor(IETAPU11 etapu11,
                              IHubContext<ETAPU11Hub> hub,
                              ILogger<ETAPU11Monitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _etapu11 = etapu11;
            _hub = hub;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the update operation every minute.
        /// </summary>
        protected override async Task DoWorkAsync()
        {
            try
            {
                _logger?.LogDebug("ETAPU11Monitor: DoWork...");
                await _etapu11?.ReadBlockAsync();
                await _hub?.Clients.All.SendAsync("UpdateData", _etapu11.Data);
                await _hub?.Clients.All.SendAsync("UpdateBoiler", _etapu11.BoilerData);
                await _hub?.Clients.All.SendAsync("UpdateHeating", _etapu11.HeatingData);
                await _hub?.Clients.All.SendAsync("UpdateHotwater", _etapu11.HotwaterData);
                await _hub?.Clients.All.SendAsync("UpdateStorage", _etapu11.StorageData);
                await _hub?.Clients.All.SendAsync("UpdateSystem", _etapu11.SystemData);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        #endregion
    }
}