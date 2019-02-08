// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRFixture.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BControlTest
{
    #region Using Directives

    using System;
    using System.Globalization;

    using Microsoft.Extensions.Logging;

    using NModbusLib;
    using NModbusLib.Models;

    using BControlLib;

    #endregion Using Directives

    public class BControlFixture : IDisposable
    {
        #region Public Properties

        public IBControl BControl { get; private set; }

        #endregion Public Properties

        #region Constructors

        public BControlFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<BControl>();
            BControl = new BControl(logger, new TcpClient()
            {
                TcpMaster = new TcpMasterData(),
                TcpSlave = new TcpSlaveData()
                {
                    Address = "10.0.1.5"
                }
            });
            BControl.ReadAllAsync().Wait();
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