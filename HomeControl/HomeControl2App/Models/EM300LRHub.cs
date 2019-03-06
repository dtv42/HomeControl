// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRHub.cs" company="DTV-Online">
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

    using HomeControlLib.EM300LR.Models;

    #endregion

    /// <summary>
    /// Class implementing the hub connection and providing access to data.
    /// </summary>
    public class EM300LRHub : BaseHub, INotifyPropertyChanged
    {
        #region Private Data Members

        private TotalData _totaldata;
        private Phase1Data _phase1data;
        private Phase2Data _phase2data;
        private Phase3Data _phase3data;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public TotalData TotalData
        {
            get { return _totaldata; }
            set { _totaldata = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Phase1Data Phase1Data
        {
            get { return _phase1data; }
            set { _phase1data = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Phase2Data Phase2Data
        {
            get { return _phase2data; }
            set { _phase2data = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Phase3Data Phase3Data
        {
            get { return _phase3data; }
            set { _phase3data = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public EM300LRHub(ILogger<EM300LRHub> logger, IOptions<AppSettings> options)
            : base(logger, new HubSettings() { Uri = options.Value.EM300LR })
        {
            _logger.LogDebug("EM300LRHub()");

            _hub.On<TotalData>("UpdateTotal", async (data) =>
            {
                _logger.LogDebug("On<TotalData>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => TotalData = data);
            });

            _hub.On<Phase1Data>("UpdatePhase1", async (data) =>
            {
                _logger.LogDebug("On<Phase1Data>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Phase1Data = data);
            });

            _hub.On<Phase2Data>("UpdatePhase2", async (data) =>
            {
                _logger.LogDebug("On<Phase2Data>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Phase2Data = data);
            });

            _hub.On<Phase3Data>("UpdatePhase3", async (data) =>
            {
                _logger.LogDebug("On<Phase3Data>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Phase3Data = data);
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
                await _hub.InvokeAsync("UpdateTotal");
                await _hub.InvokeAsync("UpdatePhase1");
                await _hub.InvokeAsync("UpdatePhase2");
                await _hub.InvokeAsync("UpdatePhase3");
            }

            return ok;
        }

        #endregion
    }
}
