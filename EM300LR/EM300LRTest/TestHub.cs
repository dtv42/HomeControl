// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRTest
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
    using EM300LRLib;
    using EM300LRLib.Models;

    #endregion Using Directives

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("EM300LR Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _totalOK = false;
        private bool _phase1OK = false;
        private bool _phase2OK = false;
        private bool _phase3OK = false;

        #endregion Private Data Members

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
            _logger = loggerFactory.CreateLogger<EM300LR>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("https://localhost:8012/hubs/monitor")
                                    .Build();

            _hub.On<EM300LRData>("UpdateData", data =>
            {
                Assert.NotNull(data);
                Assert.True(data.IsGood);
                _dataOK = true;
            });

            _hub.On<TotalData>("UpdateTotal", data =>
            {
                Assert.NotNull(data);
                Assert.True(data.IsGood);
                _totalOK = true;
            });

            _hub.On<Phase1Data>("UpdatePhase1", data =>
            {
                Assert.NotNull(data);
                Assert.True(data.IsGood);
                _phase1OK = true;
            });

            _hub.On<Phase2Data>("UpdatePhase2", data =>
            {
                Assert.NotNull(data);
                Assert.True(data.IsGood);
                _phase2OK = true;
            });

            _hub.On<Phase3Data>("UpdatePhase3", data =>
            {
                Assert.NotNull(data);
                Assert.True(data.IsGood);
                _phase3OK = true;
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

        #endregion Constructors

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
        public async Task TestGetTotalData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateTotal");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _totalOK, 5000));
        }

        [Fact]
        public async Task TestGetPhase1Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdatePhase1");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _phase1OK, 5000));
        }

        [Fact]
        public async Task TestGetPhase2Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdatePhase2");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _phase2OK, 5000));
        }

        [Fact]
        public async Task TestGetPhase3Data()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdatePhase3");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _phase3OK, 5000));
        }

        #endregion Test Methods
    }
}