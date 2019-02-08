// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11App
{
    #region Using Directives

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using Serilog.AspNetCore;

    using CommandLine.Core.CommandLineUtils;
    using CommandLine.Core.Hosting.Abstractions;

    using NModbusLib;
    using BaseClassLib;
    using ETAPU11Lib;
    using ETAPU11App.Models;
    using ETAPU11App.Commands;

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
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<ITcpClient, TcpClient>(p => new TcpClient()
            {
                TcpMaster = _settings.TcpMaster,
                TcpSlave = _settings.TcpSlave
            });
            services.AddSingleton<IETAPU11, ETAPU11>();
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
