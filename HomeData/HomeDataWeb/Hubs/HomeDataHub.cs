// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using HomeDataLib;
    using HomeDataWeb.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for HomeData data values.
    /// </summary>
    public class HomeDataHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IHomeData _homedata;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="HomeDataHub"/> class.
        /// </summary>
        /// <param name="homedata">The HomeData instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public HomeDataHub(IHomeData homedata,
                          ILogger<HomeDataHub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _homedata = homedata;
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
            _logger?.LogDebug("HomeDataHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _homedata.Data);
        }

        #endregion
    }
}