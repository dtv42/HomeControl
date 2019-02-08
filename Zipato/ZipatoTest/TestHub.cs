// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHub.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoTest
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using ZipatoLib;
    using ZipatoLib.Models;
    using ZipatoLib.Models.Data;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Zipato Test Collection")]
    public class TestHub : IDisposable
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HubConnection _hub;
        private bool _dataOK = false;
        private bool _valuesOK = false;

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
            _logger = loggerFactory.CreateLogger<Zipato>();
            _hub = new HubConnectionBuilder()
                                    .WithUrl("http://localhost:8007/hubs/monitor")
                                    .Build();

            _hub.On<ZipatoData>("UpdateData", data =>
            {
                Assert.NotNull(data);
                _dataOK = true;
            });

            _hub.On<List<ValueData>>("UpdateValues", data =>
            {
                Assert.NotNull(data);
                Assert.NotEmpty(data);
                _valuesOK = true;
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
        public async Task TestUpdateData()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateData");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _dataOK, 200000));
        }

        [Fact]
        public async Task TestUpdateValues()
        {
            // Act
            await _hub.StartAsync();
            await _hub.SendAsync("UpdateValues");

            // Assert
            Assert.True(SpinWait.SpinUntil(() => _valuesOK, 5000));
        }

        #endregion
    }
}
