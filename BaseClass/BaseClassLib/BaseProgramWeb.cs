// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseProgramWeb.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace BaseClassLib
{
    #region Using Directives

    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.Extensions.Configuration;

    using Serilog;

    #endregion

    /// <summary>
    /// Standardized ASP.NET main routine setting up a logger instance.
    /// </summary>
    /// <typeparam name="TProgram">The application program class.</typeparam>
    /// <typeparam name="TStartup">The application startup class.</typeparam>
    public class BaseProgramWeb<TProgram, TStartup> where TStartup : class
    {
        #region Static Methods

        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args"></param>
        public async Task RunAsync(string[] args)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            CurrentDirectoryHelpers.SetCurrentDirectory();

            try
            {
                var host = CreateWebHostBuilder(args)
                    .ConfigureAppConfiguration((config) =>
                    {
                        // Read the application settings file.
                        config.SetBasePath(AppContext.BaseDirectory);
                        config.AddJsonFile("appsettings.json", false, false);
                        config.AddUserSecrets<TStartup>(true);
                        config.AddEnvironmentVariables();
                        config.AddCommandLine(args);
                        var configuration = config.Build();

                        // Setting up the static Serilog logger.
                        Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(configuration)
                            .Enrich.FromLogContext()
                            .CreateLogger();
                    })
                    .Build();

                Log.ForContext<TProgram>().Information("Starting Web host.");

                var feature = (IServerAddressesFeature)host.ServerFeatures.Get<IServerAddressesFeature>();

                if (feature != null)
                {
                    foreach (var address in feature.Addresses)
                    {
                        Log.ForContext<TProgram>().Information($"Listening at {address}.");
                    }
                }

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.ForContext<TProgram>().Fatal(ex, "Exception in Web host.");
            }
            finally
            {
                Log.ForContext<TProgram>().Information("Web host terminated.");
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// The default web host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<TStartup>()
                   .UseSerilog()
                   .UseIISIntegration();

        #endregion
    }
}
