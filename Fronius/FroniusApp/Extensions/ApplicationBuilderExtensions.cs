// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace CommandLine.Core.CommandLineUtils
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using McMaster.Extensions.CommandLineUtils;

    using CommandLine.Core.Hosting.Abstractions;
    using global::CommandLineUtils.Extensions.Conventions;

    #endregion

    public static class ApplicationBuilderExtensions
    {
        #region Public Methods

        /// <summary>
        /// Configures a <c>McMaster.Extensions.CommandLineUtils</c> application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configureApp">An optional callback that can be used to configure the root application command.</param>
        public static IApplicationBuilder UseCommandLineUtils<TModel>(this IApplicationBuilder app, Action<CommandLineApplication> configureApp = null)
            where TModel : class
        {
            app.Use(args =>
            {
                var rootApp = app.ApplicationServices.GetService<RootCommandLineApplication>();
                var modelApp = new CommandLineApplication<TModel>(rootApp.HelpTextGenerator, rootApp.GetService<IConsole>(), rootApp.WorkingDirectory, rootApp.ThrowOnUnexpectedArgument);
                modelApp.Conventions.UseDefaultConventionsWithServices(app.ApplicationServices);
                modelApp.ModelFactory = () => app.ApplicationServices.GetService<TModel>();
                configureApp?.Invoke(modelApp);
                return Task.FromResult(modelApp.Execute(args));
            });

            return app;
        }

        #endregion
    }
}
