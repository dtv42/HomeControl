// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using EM300LRLib;
    using EM300LRWeb.Models;

    #endregion Using Directives

    /// <summary>
    /// SignalR Hub class for Fronius data.
    /// </summary>
    public class EM300LRHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IEM300LR _em300lr;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="EM300LRHub"/> class.
        /// </summary>
        /// <param name="em300lr">The EM300LR instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public EM300LRHub(IEM300LR em300lr,
                          ILogger<EM300LRHub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _em300lr = em300lr;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Establish connection and broadcast data.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _logger?.LogDebug("EM300LRHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _em300lr.Data);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateTotal()
        {
            await Clients.All.SendAsync("UpdateTotal", _em300lr.TotalData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdatePhase1()
        {
            await Clients.All.SendAsync("UpdatePhase1", _em300lr.Phase1Data);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdatePhase2()
        {
            await Clients.All.SendAsync("UpdatePhase2", _em300lr.Phase2Data);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdatePhase3()
        {
            await Clients.All.SendAsync("UpdatePhase3", _em300lr.Phase3Data);
        }

        #endregion Public Methods
    }
}