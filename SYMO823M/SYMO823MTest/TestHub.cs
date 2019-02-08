// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SYMO823MTest
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
    using SYMO823MLib;
    using SYMO823MLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("SYMO823M Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _commonOK = false;
        private bool _inverterOK = false;
        private bool _nameplateOK = false;
        private bool _settingsOK = false;
        private bool _extendedOK = false;
        private bool _controlOK = false;
        private bool _multipleOK = false;
        private bool _froniusOK = false;

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
            _logger = loggerFactory.CreateLogger<SYMO823M>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("http://localhost:8010/hubs/monitor")
                                    .Build();

            _hub.On<SYMO823MData>("UpdateData", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _dataOK = true;
            });

            _hub.On<CommonModelData>("UpdateCommon", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _commonOK = true;
            });

            _hub.On<InverterModelData>("UpdateInverter", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _inverterOK = true;
            });

            _hub.On<NameplateModelData>("UpdateNameplate", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _nameplateOK = true;
            });

            _hub.On<SettingsModelData>("UpdateSettings", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _settingsOK = true;
            });

            _hub.On<ExtendedModelData>("UpdateExtended", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _extendedOK = true;
            });

            _hub.On<ControlModelData>("UpdateControl", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _controlOK = true;
            });

            _hub.On<MultipleModelData>("UpdateMultiple", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _multipleOK = true;
            });

            _hub.On<FroniusRegisterData>("UpdateFronius", data =>
            {
                Assert.NotNull(data);
                Assert.Equal(DataStatus.Good, data.Status.Code);
                _froniusOK = true;
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
        public async Task TestGetCommonModel()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateCommon");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _commonOK, 5000));
        }

        [Fact]
        public async Task TestGetInverterModel()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateInverter");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _inverterOK, 5000));
        }

        [Fact]
        public async Task TestGetNameplateModel()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateNameplate");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _nameplateOK, 5000));
        }

        [Fact]
        public async Task TestGetSettingsModelData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateSettings");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _settingsOK, 5000));
        }

        [Fact]
        public async Task TestGetExtendedModel()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateExtended");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _extendedOK, 5000));
        }

        [Fact]
        public async Task TestGetControlModel()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateControl");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _controlOK, 5000));
        }

        [Fact]
        public async Task TestGetMultipleModel()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateMultiple");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _multipleOK, 5000));
        }

        [Fact]
        public async Task TestGetFroniusRegister()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateSystem");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _froniusOK, 5000));
        }

        #endregion
    }
}
