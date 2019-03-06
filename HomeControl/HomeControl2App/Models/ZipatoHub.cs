// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoHub.cs" company="DTV-Online">
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

    using HomeControlLib.Zipato.Models;

    #endregion

    /// <summary>
    /// Class implementing the hub connection and providing access to data.
    /// </summary>
    public class ZipatoHub : BaseHub, INotifyPropertyChanged
    {
        #region Private Data Members

        private ZipatoDevices _devices;
        private ZipatoSensors _sensors;
        private ZipatoOthers _others;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public ZipatoDevices Devices
        {
            get { return _devices; }
            set { _devices = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ZipatoSensors Sensors
        {
            get { return _sensors; }
            set { _sensors = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ZipatoOthers Others
        {
            get { return _others; }
            set { _others = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public ZipatoHub(ILogger<ZipatoHub> logger, IOptions<AppSettings> options)
            : base(logger, new HubSettings() { Uri = options.Value.Zipato })
        {
            _logger.LogDebug("ZipatoHub()");

            _hub.On<ZipatoDevices>("UpdateDevices", async (data) =>
            {
                _logger.LogDebug("On<ZipatoDevices>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Devices = data);
            });

            _hub.On<ZipatoSensors>("UpdateSensors", async (data) =>
            {
                _logger.LogDebug("On<ZipatoSensors>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Sensors = data);
            });

            _hub.On<ZipatoOthers>("UpdateOthers", async (data) =>
            {
                _logger.LogDebug("On<ZipatoOthers>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Others = data);
            });

            _hub.On<(bool ok, int index)>("RunScene", (data) =>
            {
                _logger.LogDebug($"Running Scene[{data.index}] {(data.ok ? "OK" : "Not OK")}");
            });

            _hub.On<(bool ok, int index, bool state)>("ToggleOnOff", async (data) =>
            {
                _logger.LogDebug($"Toggle OnOff Switch[{data.index}] {(data.ok ? "OK" : "Not OK")}");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Devices.OnOffSwitches[data.index].State.Value = data.state);
            });

            _hub.On<(bool ok, int index, bool state)>("TogglePlug", async (data) =>
            {
                _logger.LogDebug($"Toggle Wallplug[{data.index}] {(data.ok ? "OK" : "Not OK")}");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Devices.Wallplugs[data.index].State.Value = data.state);
            });

            _hub.On<(bool ok, int index, int intensity)>("SetDimmer", async (data) =>
            {
                _logger.LogDebug($"Toggle Wallplug[{data.index}] {(data.ok ? "OK" : "Not OK")}");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Devices.Dimmers[data.index].Intensity.Value = data.intensity);
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
                await _hub.InvokeAsync("UpdateDevices");
                await _hub.InvokeAsync("UpdateSensors");
                await _hub.InvokeAsync("UpdateOthers");
            }

            return ok;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task RunScene(int index)
        {
            if (IsConnected)
            {
                try
                {
                    await _hub.InvokeAsync("RunScene", index);
                }
                catch (Exception ex)
                {
                    Message = $"Exception running scene: {ex.Message}.";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task ToggleOnOff(int index)
        {
            if (IsConnected)
            {
                try
                {
                    await _hub.InvokeAsync("ToggleOnOff", index);
                }
                catch (Exception ex)
                {
                    Message = $"Exception toggle onof switch: {ex.Message}.";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task TogglePlug(int index)
        {
            if (IsConnected)
            {
                try
                {
                    await _hub.InvokeAsync("TogglePlug", index);
                }
                catch (Exception ex)
                {
                    Message = $"Exception toggle wallplug: {ex.Message}.";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task SetDimmer(int index, int intensity)
        {
            if (IsConnected)
            {
                try
                {
                    await _hub.InvokeAsync("SetDimmer", index, intensity);
                }
                catch (Exception ex)
                {
                    Message = $"Exception setting dimmer: {ex.Message}.";
                }
            }
        }

        #endregion
    }
}
