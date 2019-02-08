// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SYMO823MHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using SYMO823MLib;
    using SYMO823MWeb.Models;

    #endregion

    /// <summary>
    /// SignalR Hub class for SYMO823M data.
    /// </summary>
    public class SYMO823MHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly ISYMO823M _symo823m;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="SYMO823MHub"/> class.
        /// </summary>
        /// <param name="symo823m">The SYMO823M instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public SYMO823MHub(ISYMO823M symo823m,
                          ILogger<SYMO823MHub> logger,
                          IOptions<AppSettings> options)
            : base(logger, options)
        {
            _symo823m = symo823m;
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
            _logger?.LogDebug("SYMO823MHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _symo823m.Data);
            await Clients.All.SendAsync("UpdateCommon", _symo823m.CommonModel);
            await Clients.All.SendAsync("UpdateInverter", _symo823m.InverterModel);
            await Clients.All.SendAsync("UpdateNameplate", _symo823m.NameplateModel);
            await Clients.All.SendAsync("UpdateSettings", _symo823m.SettingsModel);
            await Clients.All.SendAsync("UpdateExtended", _symo823m.ExtendedModel);
            await Clients.All.SendAsync("UpdateControl", _symo823m.ControlModel);
            await Clients.All.SendAsync("UpdateMultiple", _symo823m.MultipleModel);
            await Clients.All.SendAsync("UpdateFronius", _symo823m.FroniusRegister);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCommon()
        {
            await Clients.All.SendAsync("UpdateCommon", _symo823m.CommonModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateInverter()
        {
            await Clients.All.SendAsync("UpdateInverter", _symo823m.InverterModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateNameplate()
        {
            await Clients.All.SendAsync("UpdateNameplate", _symo823m.NameplateModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateSettings()
        {
            await Clients.All.SendAsync("UpdateSettings", _symo823m.SettingsModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateExtended()
        {
            await Clients.All.SendAsync("UpdateExtended", _symo823m.ExtendedModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateControl()
        {
            await Clients.All.SendAsync("UpdateControl", _symo823m.ControlModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateMultiple()
        {
            await Clients.All.SendAsync("UpdateMultiple", _symo823m.MultipleModel);
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateFronius()
        {
            await Clients.All.SendAsync("UpdateFronius", _symo823m.FroniusRegister);
        }

        #endregion
    }
}