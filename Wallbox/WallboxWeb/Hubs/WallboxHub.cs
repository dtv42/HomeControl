// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using WallboxLib;
    using WallboxWeb.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for Wallbox data.
    /// </summary>
    public class WallboxHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IWallbox _wallbox;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="WallboxHub"/> class.
        /// </summary>
        /// <param name="wallbox">The Wallbox instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public WallboxHub(IWallbox wallbox,
                          ILogger<WallboxHub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _wallbox = wallbox;
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
            _logger?.LogDebug("WallboxHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _wallbox.Data);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateReport1()
        {
            await Clients.All.SendAsync("UpdateReport1", _wallbox.Report1);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateReport2()
        {
            await Clients.All.SendAsync("UpdateReport2", _wallbox.Report2);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateReport3()
        {
            await Clients.All.SendAsync("UpdateReport3", _wallbox.Report3);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateReport100()
        {
            await Clients.All.SendAsync("UpdateReport100", _wallbox.Report100);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateReports()
        {
            await Clients.All.SendAsync("UpdateReports", _wallbox.Reports);
        }

        #endregion
    }
}
