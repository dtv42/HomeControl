// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
[assembly: Microsoft.AspNetCore.Mvc.ApiConventionType(typeof(Microsoft.AspNetCore.Mvc.DefaultApiConventions))]
namespace KWLEC200Web
{
    #region Using Directives

    using System;
    using System.IO;
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;
    using Swashbuckle.AspNetCore.SwaggerUI;

    using BaseClassLib;
    using KWLEC200Lib;
    using KWLEC200Web.Models;
    using KWLEC200Web.Services;
    using KWLEC200Web.Hubs;

    #endregion

    /// <summary>
    /// The web application startup class.
    /// </summary>
    public class Startup : BaseStartupWeb<AppSettings>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
            : base(environment, configuration)
        { }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Adding additional services.
            services.AddSingleton<IHeliosClient, HeliosClient>(p => new HeliosClient(p)
            {
                TcpMaster = _settings.TcpMaster,
                TcpSlave = _settings.TcpSlave
            });
            services.AddSingleton<IKWLEC200, KWLEC200>();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, KWLEC200Monitor>();

            // Adding Healthchecks.
            services.AddHealthChecks()
                .AddCheck<HealthCheck>("health");

            // Adding SignalR support.
            services.AddSignalR();

            // Adds MVC support.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            // Register the Swagger generator, defining the Swagger document.
            services.AddSwaggerGen(options =>
            {
                // Set the comments path for the Swagger JSON and UI and the swagger options.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var swagger = _settings.Swagger;

                options.SwaggerDoc(swagger.Info.Version, swagger.Info);
                options.EnableAnnotations();
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Configuration["Syncfusion:LicenseKey"]);

            // Display health check status at the specified endpoint.
            app.UseHealthChecks("/health");

            // Enable static files.
            app.UseStaticFiles();

            // Use SignalR and setup the route to the hubs.
            app.UseSignalR(routes =>
            {
                routes.MapHub<KWLEC200Hub>("/hubs/monitor");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger-UI specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_settings.Swagger.Info.Version}");
                options.DefaultModelRendering(ModelRendering.Example);
                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(0);
                options.DisplayRequestDuration();
                options.EnableValidator();
            });

            app.UseMvc();
        }

        #endregion
    }
}
