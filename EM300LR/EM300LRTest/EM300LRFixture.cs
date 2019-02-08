// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EM300LRFixture.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRTest
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Net.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using EM300LRLib;
    using EM300LRTest.Models;

    #endregion Using Directives

    public class EM300LRFixture : IDisposable
    {
        #region Public Properties

        public IEM300LR EM300LR { get; private set; }
        public AppSettings Settings { get; } = new AppSettings();

        #endregion Public Properties

        #region Constructors

        public EM300LRFixture()
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<EM300LR>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Startup>(true)
                .Build();

            configuration.GetSection("AppSettings").Bind(Settings);

            var client = new EM300LRClient(new HttpClient()
            {
                BaseAddress = new Uri(Settings.BaseAddress),
                Timeout = TimeSpan.FromSeconds(Settings.Timeout)
            }, loggerFactory.CreateLogger<EM300LRClient>());

            EM300LR = new EM300LR(logger, client, Settings);

            EM300LR.ReadAllAsync().Wait();
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