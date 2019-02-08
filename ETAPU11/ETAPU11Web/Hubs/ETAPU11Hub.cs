// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ETAPU11Hub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Web.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using ETAPU11Lib;
    using ETAPU11Web.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for ETAPU11 data.
    /// </summary>
    public class ETAPU11Hub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IETAPU11 _etapu11;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ETAPU11Hub"/> class.
        /// </summary>
        /// <param name="etapu11">The ETAPU11 instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public ETAPU11Hub(IETAPU11 etapu11,
                          ILogger<ETAPU11Hub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _etapu11 = etapu11;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Establish connection and broadcast gauge data.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _logger?.LogDebug("ETAPU11Hub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _etapu11.Data);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateBoiler()
        {
            await Clients.All.SendAsync("UpdateBoiler", _etapu11.BoilerData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateHeating()
        {
            await Clients.All.SendAsync("UpdateHeating", _etapu11.HeatingData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateHotwater()
        {
            await Clients.All.SendAsync("UpdateHotwater", _etapu11.HotwaterData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateStorage()
        {
            await Clients.All.SendAsync("UpdateStorage", _etapu11.StorageData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateSystem()
        {
            await Clients.All.SendAsync("UpdateSystem", _etapu11.SystemData);
        }

        #endregion
    }
}
