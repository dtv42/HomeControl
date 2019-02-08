// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxApp
{
    #region Using Directives

    using System;
    using System.Net;
    using System.Net.Sockets;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using Serilog.AspNetCore;

    using CommandLine.Core.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using WallboxLib;
    using WallboxLib.Models;
    using WallboxApp.Models;
    using WallboxApp.Commands;

    #endregion

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
            services.AddSingleton<IWallboxClient, WallboxClient>();
            services.AddSingleton<IWallbox, Wallbox>();
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

        #endregion
    }
}
