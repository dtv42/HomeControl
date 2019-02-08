// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KWLEC200Fixture.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Test
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Net.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using NModbusLib.Models;
    using KWLEC200Lib;
    using KWLEC200Lib.Models;
    using KWLEC200Test.Models;

    #endregion

    public class KWLEC200Fixture : IDisposable
    {
        #region Public Properties

        public IKWLEC200 KWLEC200 { get; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion

        #region Constructors

        public KWLEC200Fixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<KWLEC200>();
            var helioslogger = loggerFactory.CreateLogger<Helios>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            KWLEC200 = new KWLEC200(logger, new HeliosClient(helioslogger)
            {
                TcpMaster = new TcpMasterData(),
                TcpSlave = new TcpSlaveData()
                {
                    Address = Settings.Slave.Address,
                    Port = Settings.Slave.Port,
                    ID = Settings.Slave.ID
                }
            });

            KWLEC200.ReadAll();
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

        #endregion
    }
}
