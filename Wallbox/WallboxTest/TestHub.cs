// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxTest
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
    using WallboxLib;
    using WallboxLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Wallbox Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _report1OK = false;
        private bool _report2OK = false;
        private bool _report3OK = false;
        private bool _report100OK = false;

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
            _logger = loggerFactory.CreateLogger<Wallbox>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("http://localhost:8006/hubs/monitor")
                                    .Build();

            _hub.On<WallboxData>("UpdateData", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _dataOK = true;
            });

            _hub.On<Report1Data>("UpdateReport1", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _report1OK = true;
            });

            _hub.On<Report2Data>("UpdateReport2", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _report2OK = true;
            });

            _hub.On<Report3Data>("UpdateReport3", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _report3OK = true;
            });

            _hub.On<ReportsData>("UpdateReport100", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _report100OK = true;
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
            Assert.True(SpinWait.SpinUntil(() => _dataOK, 30000));
        }

        [Fact]
        public async Task TestGeReport1Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateReport1");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _report1OK, 5000));
        }

        [Fact]
        public async Task TestGeReport2Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateReport2");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _report2OK, 5000));
        }

        [Fact]
        public async Task TestGeReport3Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateReport3");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _report3OK, 5000));
        }

        [Fact]
        public async Task TestGeReport100Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateReport100");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _report100OK, 5000));
        }

        #endregion
    }
}
