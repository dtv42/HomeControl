// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Test
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
    using ETAPU11Lib;
    using ETAPU11Lib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("ETAPU11 Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _boilerOK = false;
        private bool _hotwaterOK = false;
        private bool _heatingOK = false;
        private bool _storageOK = false;
        private bool _systemOK = false;

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
            _logger = loggerFactory.CreateLogger<ETAPU11>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("http://localhost:8004/hubs/monitor")
                                    .Build();

            _hub.On<ETAPU11Data>("UpdateData", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _dataOK = true;
            });

            _hub.On<BoilerData>("UpdateBoiler", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _boilerOK = true;
            });

            _hub.On<HotwaterData>("UpdateHotwater", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _hotwaterOK = true;
            });

            _hub.On<HeatingData>("UpdateHeating", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _heatingOK = true;
            });

            _hub.On<StorageData>("UpdateStorage", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _storageOK = true;
            });

            _hub.On<SystemData>("UpdateSystem", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _systemOK = true;
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
            await _hub.SendAsync("UpdateData");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _dataOK, 5000));
        }

        [Fact]
        public async Task TestGetBoilerData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateBoiler");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _boilerOK, 5000));
        }

        [Fact]
        public async Task TestGetHotwaterData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateHotwater");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _hotwaterOK, 5000));
        }

        [Fact]
        public async Task TestGetHeatingData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateHeating");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _heatingOK, 5000));
        }

        [Fact]
        public async Task TestGetStorageData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateStorage");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _storageOK, 5000));
        }

        [Fact]
        public async Task TestGetSystemData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateSystem");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _systemOK, 5000));
        }

        #endregion
    }
}
