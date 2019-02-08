// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeliosClient.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200Lib
{
    #region Using Directives

    using System;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NModbusLib;
    using DataValueLib;
    using KWLEC200Lib.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class HeliosClient : TcpClient, IHeliosClient
    {
        #region Private Data Members

        private Helios _helios;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public HeliosClient(IServiceProvider provider)
        {
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            _helios = new Helios(loggerFactory.CreateLogger<Helios>());
            TcpSlave.ID = 180;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public HeliosClient(ILogger<Helios> logger)
        {
            _helios = new Helios(logger);
            TcpSlave.ID = 180;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public DataStatus ReadProperty(KWLEC200Data data, string property)
            => _helios.ReadProperty(data, ModbusMaster, TcpSlave.ID, property);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public DataStatus WriteProperty(KWLEC200Data data, string property)
            => _helios.WriteProperty(data, ModbusMaster, TcpSlave.ID, property);

        #endregion
    }
}
