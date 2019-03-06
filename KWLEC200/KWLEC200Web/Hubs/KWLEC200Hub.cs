// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Hub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Web.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using KWLEC200Lib;
    using KWLEC200Web.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for KWLEC200 data.
    /// </summary>
    public class KWLEC200Hub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IKWLEC200 _kwlec200;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="KWLEC200Hub"/> class.
        /// </summary>
        /// <param name="kwlec200">The KWLEC200 instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public KWLEC200Hub(IKWLEC200 kwlec200,
                           ILogger<KWLEC200Hub> logger,
                           IOptions<AppSettings> options)
            : base(logger, options)
        {
            _kwlec200 = kwlec200;
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
            _logger?.LogDebug("KWLEC200Hub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _kwlec200.Data);
        }

        /// <summary>
        /// Callback to provide overview data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateOverview()
        {
            await Clients.All.SendAsync("UpdateOverview", _kwlec200.OverviewData);
        }

        #endregion
    }
}
