// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeDataHub.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App.Models
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;

    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using HomeControlLib.HomeData.Models;

    #endregion

    /// <summary>
    /// Class implementing the hub connection and providing access to data.
    /// </summary>
    public class HomeDataHub : BaseHub, INotifyPropertyChanged
    {
        #region Private Data Members

        private HomeValues _data;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public HomeValues Data
        {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public HomeDataHub(ILogger<HomeDataHub> logger, IOptions<AppSettings> options)
            : base(logger, new HubSettings() { Uri = options.Value.HomeData })
        {
            _logger.LogDebug("HomeDataHub()");

            _hub.On<HomeValues>("UpdateData", async (data) =>
            {
                _logger.LogDebug("On<HomeValues>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Data = data);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Connect()
        {
            var ok = await base.Connect();

            if (ok)
            {
                await _hub.InvokeAsync("UpdateData");
            }

            return ok;
        }

        #endregion
    }
}
