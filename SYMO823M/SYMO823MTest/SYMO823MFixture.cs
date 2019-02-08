// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SYMO823MFixture.cs" company="DTV-Online">
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

    using Microsoft.Extensions.Logging;

    using NModbusLib;
    using NModbusLib.Models;

    using SYMO823MLib;

    #endregion

    public class SYMO823MFixture : IDisposable
    {
        #region Public Properties

        public ISYMO823M SYMO823M { get; private set; }

        #endregion

        #region Constructors

        public SYMO823MFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<SYMO823M>();
            SYMO823M = new SYMO823M(logger, new TcpClient()
            {
                TcpMaster = new TcpMasterData(),
                TcpSlave = new TcpSlaveData()
                {
                    Address = "127.0.0.1"
                }
            });
            SYMO823M.ReadAllAsync().Wait();
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
