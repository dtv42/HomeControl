// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FroniusFixture.cs" company="DTV-Online">
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
    using System.Net.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using FroniusLib;
    using FroniusTest.Models;

    #endregion

    public class FroniusFixture : IDisposable
    {
        #region Public Properties

        public IFronius Fronius { get; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion

        #region Constructors

        public FroniusFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<Fronius>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            var client = new FroniusClient(new HttpClient()
            {
                BaseAddress = new Uri(Settings.BaseAddress),
                Timeout = TimeSpan.FromSeconds(Settings.Timeout)
            }, loggerFactory.CreateLogger<FroniusClient>());

            Fronius = new Fronius(logger, client, Settings);

            Fronius.ReadAllAsync().Wait();
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
