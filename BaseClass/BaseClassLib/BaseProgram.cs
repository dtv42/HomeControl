// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseProgram.cs" company="DTV-Online">
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
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Serilog;
    using CommandLine.Core.Hosting;
    using CommandLine.Core.Hosting.Abstractions;
    using McMaster.Extensions.CommandLineUtils;

    #endregion Using Directives

    /// <summary>
    /// Standardized main routine setting up a logger instance.
    /// </summary>
    /// <typeparam name="TProgram">The application program class.</typeparam>
    /// <typeparam name="TStartup">The application startup class.</typeparam>
    public class BaseProgram<TProgram, TStartup> where TStartup : class, IStartup
    {
        #region Public Methods

        public async Task<int> RunAsync(string[] args)
        {
            // Read the application settings file containing the Serilog configuration.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<TStartup>(true)
                .AddEnvironmentVariables()
                .Build();

            // Setting up the static Serilog logger.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            try
            {
                // Create the application host.
                var app = CommandLineHost.CreateBuilder(args)
                    .ConfigureAppConfiguration((config) =>
                    {
                        config.SetBasePath(AppContext.BaseDirectory);
                        config.AddJsonFile("appsettings.json", false, false);
                        config.AddUserSecrets<TStartup>(true);
                        config.AddEnvironmentVariables();
                        config.Build();
                    })
                    .ConfigureLogging((logger) =>
                    {
                        logger.AddSerilog();
                    })
                    .UseStartup<TStartup>()
                    .Build();

                int code = 0;

                // Start the execution.
                Log.ForContext<TProgram>().Debug($"Executing '{Assembly.GetEntryAssembly().GetName().Name}{(args?.Length > 0 ? " " : "")}{string.Join(" ", args)}'.");
                Stopwatch stopWatch = new Stopwatch();

                // Start the timer.
                stopWatch.Start();
                code = await app.RunAsync();

                // Display the timing info.
                stopWatch.Stop();
                Log.ForContext<TProgram>().Debug($"Done.");
                Log.ForContext<TProgram>().Verbose($"Time elapsed {stopWatch.Elapsed}");
                Console.WriteLine($"Time elapsed {stopWatch.Elapsed}");

                return code;
            }
            catch (CommandParsingException cpx)
            {
                Console.WriteLine($"{cpx.Message}.");
                return -1;
            }
            catch (Exception ex)
            {
                Log.ForContext<TProgram>().Fatal(ex, "Application terminated unexpectedly");
                return -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        #endregion Public Methods
    }
}