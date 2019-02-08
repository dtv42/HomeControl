// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SYMO823MMonitor.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MWeb.Services
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.SignalR;

    using SYMO823MLib;
    using SYMO823MWeb.Hubs;
    using SYMO823MWeb.Models;

    #endregion

    /// <summary>
    /// Monitor service updating selected SYMO823M instance data every minute.
    /// </summary>
    public class SYMO823MMonitor : TimedService
    {
        #region Private Data Members

        private readonly ISYMO823M _symo823m;
        private readonly IHubContext<SYMO823MHub> _hub;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="SYMO823MMonitor"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="hub">The test data SignalR hub.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        /// <param name="environment"></param>
        public SYMO823MMonitor(ISYMO823M symo823m,
                              IHubContext<SYMO823MHub> hub,
                              ILogger<SYMO823MMonitor> logger,
                              IOptions<AppSettings> options,
                              IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _symo823m = symo823m;
            _hub = hub;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Executes the update operation (every minute).
        /// </summary>
        protected override async Task DoWorkAsync()
        {
            try
            {
                _logger?.LogDebug("SYMO823MMonitor: DoWork...");
                await _hub.Clients.All.SendAsync("UpdateCommon", _symo823m.CommonModel);
                await _hub.Clients.All.SendAsync("UpdateInverter", _symo823m.InverterModel);
                await _hub.Clients.All.SendAsync("UpdateNameplate", _symo823m.NameplateModel);
                await _hub.Clients.All.SendAsync("UpdateSettings", _symo823m.SettingsModel);
                await _hub.Clients.All.SendAsync("UpdateExtended", _symo823m.ExtendedModel);
                await _hub.Clients.All.SendAsync("UpdateControl", _symo823m.ControlModel);
                await _hub.Clients.All.SendAsync("UpdateMultiple", _symo823m.MultipleModel);
                await _hub.Clients.All.SendAsync("UpdateFronius", _symo823m.FroniusRegister);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoWorkAsync: Exception");
            }
        }

        /// <summary>
        /// Executes the read and operation.
        /// </summary>
        protected override async Task DoUpdateAsync()
        {
            try
            {
                _logger?.LogDebug("SYMO823MMonitor: DoUpdateAsync...");
                var status = await _symo823m?.ReadBlockAsync();

                if (status.IsGood)
                {
                    await _hub.Clients.All.SendAsync("UpdateData", _symo823m.Data);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "DoUpdateAsync: Exception");
            }
        }

        #endregion
    }
}