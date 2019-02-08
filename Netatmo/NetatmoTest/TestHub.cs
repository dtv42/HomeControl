// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoTest
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using DataValueLib;
    using NetatmoLib;
    using NetatmoLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Netatmo Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _mainOK = false;
        private bool _outdoorOK = false;
        private bool _indoor1OK = false;
        private bool _indoor2OK = false;
        private bool _indoor3OK = false;
        private bool _rainOK = false;
        private bool _windOK = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestHub"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestHub(ITestOutputHelper output)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<Netatmo>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("http://localhost:8002/hubs/monitor")
                                    .Build();

            _hub.On<NetatmoData>("UpdateData", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _dataOK = true;
            });

            _hub.On<StationDeviceData>("UpdateMain", data =>
            {
                Assert.NotNull(data);
                _mainOK = true;
            });

            _hub.On<OutdoorModuleData>("UpdateOutdoor", data =>
            {
                Assert.NotNull(data);
                _outdoorOK = true;
            });

            _hub.On<IndoorModuleData>("UpdateIndoor1", data =>
            {
                Assert.NotNull(data);
                _indoor1OK = true;
            });

            _hub.On<IndoorModuleData>("UpdateIndoor2", data =>
            {
                Assert.NotNull(data);
                _indoor2OK = true;
            });

            _hub.On<IndoorModuleData>("UpdateIndoor3", data =>
            {
                Assert.NotNull(data);
                _indoor3OK = true;
            });

            _hub.On<RainGaugeData>("UpdateRain", data =>
            {
                Assert.NotNull(data);
                _rainOK = true;
            });

            _hub.On<WindGaugeData>("UpdateWind", data =>
            {
                Assert.NotNull(data);
                _windOK = true;
            });
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Method to centralize all logic related to releasing resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hub?.DisposeAsync().Wait();
            }
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestGetAllData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _dataOK, 5000));
        }

        [Fact]
        public async Task TestGetMainData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _mainOK, 5000));
        }

        [Fact]
        public async Task TestGetOutdoorData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _outdoorOK, 5000));
        }

        [Fact]
        public async Task TestGetIndoor1Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _indoor1OK, 5000));
        }

        [Fact]
        public async Task TestGetIndoor2Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _indoor2OK, 5000));
        }

        [Fact]
        public async Task TestGetIndoor3Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _indoor3OK, 5000));
        }

        [Fact]
        public async Task TestGetRainData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _rainOK, 5000));
        }

        [Fact]
        public async Task TestGetWindData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("NetatmoUpdate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _windOK, 5000));
        }

        #endregion
    }
}
