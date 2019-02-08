// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace FroniusTest
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
    using FroniusLib;
    using FroniusLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Fronius Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _commonOK = false;
        private bool _inverterOK = false;
        private bool _loggerOK = false;
        private bool _minmaxOK = false;
        private bool _phaseOK = false;

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
            _logger = loggerFactory.CreateLogger<Fronius>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("http://localhost:8006/hubs/monitor")
                                    .Build();

            _hub.On<FroniusData>("UpdateData", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _dataOK = true;
            });

            _hub.On<CommonData>("UpdateCommon", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _commonOK = true;
            });

            _hub.On<InverterInfo>("UpdateInverter", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _inverterOK = true;
            });

            _hub.On<LoggerInfo>("UpdateLogger", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _loggerOK = true;
            });

            _hub.On<MinMaxData>("UpdateMinMax", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _minmaxOK = true;
            });

            _hub.On<PhaseData>("UpdatePhase", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _phaseOK = true;
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
        public async Task TestGetCommonData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateCommon");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _commonOK, 5000));
        }

        [Fact]
        public async Task TestGetInverterData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateInverter");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _inverterOK, 5000));
        }

        [Fact]
        public async Task TestGetLoggerData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateLogger");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _loggerOK, 5000));
        }

        [Fact]
        public async Task TestGetMinMaxData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateMinMax");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _minmaxOK, 5000));
        }

        [Fact]
        public async Task TestGetPhaseData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdatePhase");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _phaseOK, 5000));
        }

        #endregion
    }
}
