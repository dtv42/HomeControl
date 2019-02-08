// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeDataFixture.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeDataTest
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Net.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using HomeDataLib;
    using HomeDataTest.Models;

    #endregion

    public class HomeDataFixture : IDisposable
    {
        #region Public Properties

        public IHomeData HomeData { get; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion

        #region Constructors

        public HomeDataFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<HomeData>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            var client1 = new HomeDataClient1(new HttpClient()
            {
                BaseAddress = new Uri(Settings.Meter1Address),
                Timeout = TimeSpan.FromSeconds(Settings.Timeout)
            }, loggerFactory.CreateLogger<HomeDataClient1>());

            var client2 = new HomeDataClient2(new HttpClient()
            {
                BaseAddress = new Uri(Settings.Meter1Address),
                Timeout = TimeSpan.FromSeconds(Settings.Timeout)
            }, loggerFactory.CreateLogger<HomeDataClient2>());

            HomeData = new HomeData(logger, client1, client2, Settings);

            HomeData.ReadAllAsync().Wait();
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
