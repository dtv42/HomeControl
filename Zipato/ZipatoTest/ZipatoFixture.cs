// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoFixture.cs" company="DTV-Online">
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
    using System.Globalization;
    using System.Net.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using ZipatoLib;
    using ZipatoTest.Models;

    #endregion

    public class ZipatoFixture : IDisposable
    {
        #region Public Properties

        public Zipato Zipato { get; private set; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion

        #region Constructors

        public ZipatoFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<Zipato>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            var client = new ZipatoClient(new HttpClient()
            {
                BaseAddress = new Uri(Settings.BaseAddress),
                Timeout = TimeSpan.FromSeconds(Settings.Timeout)
            }, Settings, loggerFactory.CreateLogger<ZipatoClient>());

            Zipato = new Zipato(logger, client, Settings);
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
