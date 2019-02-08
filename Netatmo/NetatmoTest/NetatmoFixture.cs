// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetatmoFixture.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace NetatmoTest
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Net.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using NetatmoLib;
    using NetatmoTest.Models;

    #endregion

    public class NetatmoFixture : IDisposable
    {
        #region Public Properties

        public Netatmo Netatmo { get; private set; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion

        #region Constructors

        public NetatmoFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<Netatmo>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            var client = new NetatmoClient(new HttpClient()
            {
                BaseAddress = new Uri(Settings.BaseAddress),
                Timeout = TimeSpan.FromSeconds(Settings.Timeout)
            }, loggerFactory.CreateLogger<NetatmoClient>());

            Netatmo = new Netatmo(logger, client, Settings);

            Netatmo.ReadAllAsync().Wait();
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
