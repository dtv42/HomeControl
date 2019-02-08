// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRFixture.cs" company="DTV-Online">
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

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using NModbusLib;
    using NModbusLib.Models;

    using ETAPU11Lib;
    using ETAPU11Test.Models;

    #endregion Using Directives

    public class ETAPU11Fixture : IDisposable
    {
        #region Public Properties

        public IETAPU11 ETAPU11 { get; private set; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion Public Properties

        #region Constructors

        public ETAPU11Fixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<ETAPU11>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            ETAPU11 = new ETAPU11(logger, new TcpClient()
            {
                TcpMaster = new TcpMasterData(),
                TcpSlave = new TcpSlaveData()
                {
                    Address = Settings.TcpSlave.Address,
                    Port = Settings.TcpSlave.Port,
                    ID = Settings.TcpSlave.ID
                }
            });

            ETAPU11.ReadAllAsync().Wait();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }

        #endregion Constructors
    }
}