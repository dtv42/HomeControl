// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using FroniusLib;
    using FroniusWeb.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for Fronius data.
    /// </summary>
    public class FroniusHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IFronius _fronius;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="FroniusHub"/> class.
        /// </summary>
        /// <param name="fronius">The Fronius instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public FroniusHub(IFronius fronius,
                          ILogger<FroniusHub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _fronius = fronius;
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
            _logger?.LogDebug("FroniusHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _fronius.Data);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCommon()
        {
            await Clients.All.SendAsync("UpdateCommon", _fronius.CommonData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateInverter()
        {
            await Clients.All.SendAsync("UpdateInverter", _fronius.InverterInfo);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateLogger()
        {
            await Clients.All.SendAsync("UpdateLogger", _fronius.LoggerInfo);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateMinMax()
        {
            await Clients.All.SendAsync("UpdateMinMax", _fronius.MinMaxData);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdatePhase()
        {
            await Clients.All.SendAsync("UpdatePhase", _fronius.PhaseData);
        }

        #endregion
    }
}
