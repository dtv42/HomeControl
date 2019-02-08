// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace EM300LRApp
{
    #region Using Directives

    using System;
    using System.Net;
    using System.Net.Http;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using Polly;
    using Polly.Extensions.Http;
    using Serilog.AspNetCore;

    using CommandLine.Core.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using EM300LRLib;
    using EM300LRLib.Models;
    using EM300LRApp.Models;
    using EM300LRApp.Commands;

    #endregion Using Directives

    /// <summary>
    /// The Startup class provides application specific settings and configuration.
    /// </summary>
    public class Startup : BaseStartup<AppSettings>, IStartup
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// Using dependency injection, the application specific settings and configuration are initialized.
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostingEnvironment environment, IConfiguration configuration) : base(environment, configuration)
        { }

        #endregion

        #region Public Methods

        /// <summary>
        /// The application specific services are configured. The RootCommand and the command line utilities are added.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory>(logger => new SerilogLoggerFactory(null, true));
            services.Configure<SettingsData>(Configuration.GetSection("AppSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<ISettingsData, SettingsData>();

            services.AddHttpClient<IEM300LRClient, EM300LRClient>(client =>
            {
                client.BaseAddress = new Uri(_settings.BaseAddress);
                client.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
            })
            .ConfigureHttpMessageHandlerBuilder(config => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            })
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(_settings.Retries,
                                       attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));

            services.AddSingleton<IEM300LR, EM300LR>();
            services.AddSingleton<RootCommand>();
            services.AddCommandLineUtils();
        }

        /// <summary>
        /// The command line utils are configured using the RootCommand class.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseCommandLineUtils<RootCommand>();
        }

        #endregion Public Methods
    }
}