// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using NetatmoLib;
    using NetatmoWeb.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for Netatmo data.
    /// </summary>
    public class NetatmoHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly INetatmo _netatmo;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="NetatmoHub"/> class.
        /// </summary>
        /// <param name="netatmo">The Netatmo instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public NetatmoHub(INetatmo netatmo,
                          ILogger<NetatmoHub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _netatmo = netatmo;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Establish connection and broadcast data.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _logger?.LogDebug("NetatmoHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _netatmo.Station);
            await Clients.All.SendAsync("UpdateMain", _netatmo.Station.Device.DashboardData);
            await Clients.All.SendAsync("UpdateOutdoor", _netatmo.Station.Device.OutdoorModule.DashboardData);
            await Clients.All.SendAsync("UpdateIndoor1", _netatmo.Station.Device.IndoorModule1.DashboardData);
            await Clients.All.SendAsync("UpdateIndoor2", _netatmo.Station.Device.IndoorModule2.DashboardData);
            await Clients.All.SendAsync("UpdateIndoor3", _netatmo.Station.Device.IndoorModule3.DashboardData);
            await Clients.All.SendAsync("UpdateRain", _netatmo.Station.Device.RainGauge.DashboardData);
            await Clients.All.SendAsync("UpdateWind", _netatmo.Station.Device.WindGauge.DashboardData);
        }

        #endregion
    }
}
