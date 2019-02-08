// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PAC3200Hub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using BControlLib;
    using BControlWeb.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for BControl data.
    /// </summary>
    public class BControlHub : BaseHub<AppSettings>
    {
        #region Private Data Members

        private readonly IBControl _bcontrol;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="BControlHub"/> class.
        /// </summary>
        /// <param name="bcontrol">The BControl instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public BControlHub(IBControl bcontrol,
                           ILogger<BControlHub> logger,
                           IOptions<AppSettings> options)
            : base(logger, options)
        {
            _bcontrol = bcontrol;
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
            _logger?.LogDebug("BControlHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _bcontrol.Data);
        }

        /// <summary>
        /// Callback to provide internal data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateInternal()
        {
            await Clients.All.SendAsync("UpdateInternal", _bcontrol.InternalData);
        }

        /// <summary>
        /// Callback to provide energy data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateEnergy()
        {
            await Clients.All.SendAsync("UpdateEnergy", _bcontrol.EnergyData);
        }

        /// <summary>
        /// Callback to provide pnp data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdatePnP()
        {
            await Clients.All.SendAsync("UpdatePnP", _bcontrol.PnPData);
        }

        /// <summary>
        /// Callback to provide SunSpec data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateSunSpec()
        {
            await Clients.All.SendAsync("UpdateSunSpec", _bcontrol.SunSpecData);
        }

        #endregion
    }
}
