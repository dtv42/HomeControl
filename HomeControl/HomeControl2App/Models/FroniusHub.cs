// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusHub.cs" company="DTV-Online">
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

    using HomeControlLib.Fronius.Models;

    #endregion

    /// <summary>
    /// Class implementing the hub connection and providing access to data.
    /// </summary>
    public class FroniusHub : BaseHub, INotifyPropertyChanged
    {
        #region Private Data Members

        private CommonData _commondata;
        private PhaseData _phasedata;
        private InverterInfo _inverterinfo;
        private MinMaxData _minmaxdata;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public CommonData CommonData
        {
            get { return _commondata; }
            set { _commondata = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public PhaseData PhaseData
        {
            get { return _phasedata; }
            set { _phasedata = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public InverterInfo InverterInfo
        {
            get { return _inverterinfo; }
            set { _inverterinfo = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public MinMaxData MinMaxData
        {
            get { return _minmaxdata; }
            set { _minmaxdata = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public FroniusHub(ILogger<FroniusHub> logger, IOptions<AppSettings> options)
            : base(logger, new HubSettings() { Uri = options.Value.Fronius })
        {
            _logger.LogDebug("FroniusHub()");

            _hub.On<CommonData>("UpdateCommon", async (data) =>
            {
                _logger.LogDebug("On<CommonData>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => CommonData = data);
            });

            _hub.On<PhaseData>("UpdatePhase", async (data) =>
            {
                _logger.LogDebug("On<PhaseData>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => PhaseData = data);
            });

            _hub.On<InverterInfo>("UpdateInverter", async (data) =>
            {
                _logger.LogDebug("On<InverterInfo>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => InverterInfo = data);
            });

            _hub.On<MinMaxData>("UpdateMinMax", async (data) =>
            {
                _logger.LogDebug("On<MinMaxData>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => MinMaxData = data);
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
                await _hub.InvokeAsync("UpdateCommon");
                await _hub.InvokeAsync("UpdatePhase");
                await _hub.InvokeAsync("UpdateInverter");
                await _hub.InvokeAsync("UpdateMinMax");
            }

            return ok;
        }

        #endregion
    }
}
