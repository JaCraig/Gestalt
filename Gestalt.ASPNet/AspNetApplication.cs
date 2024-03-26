using Gestalt.ASPNet.Interfaces;
using Gestalt.Core.BaseClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Gestalt.ASPNet
{
    /// <summary>
    /// ASP.NET Application.
    /// </summary>
    /// <seealso cref="ApplicationBaseClass"/>
    public class AspNetApplication : ApplicationBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetApplication"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="assemblies">The assemblies.</param>
        public AspNetApplication(IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies)
            : base(configuration, env, assemblies)
        {
        }

        /// <summary>
        /// Configures the application, setting up the modules and endpoints.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The web application.</returns>
        public WebApplication? ConfigureApplication(WebApplication? application)
        {
            if (application is null || application.Lifetime is null)
                return application;
            IApplicationBuilder? ApplicationBuilder = application;

            _ = (ApplicationBuilder?.UseRouting());

            //Configure modules
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                if (Modules[I] is not IAspNetModule Module)
                    continue;
                ApplicationBuilder = Module.ConfigureApplication(ApplicationBuilder, Configuration, Environment);
            }

            // Set up endpoints
            ApplicationBuilder = ApplicationBuilder?.UseEndpoints(endpoints =>
            {
                //Module specific routes added
                for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
                {
                    if (Modules[I] is not IAspNetModule Module)
                        continue;
                    endpoints = Module.ConfigureRoutes(endpoints, Configuration, Environment) ?? endpoints;
                }
            });

            // Set up application lifetime events
            _ = application.Lifetime.ApplicationStarted.Register(OnStarted);
            _ = application.Lifetime.ApplicationStopped.Register(OnStopped);
            _ = application.Lifetime.ApplicationStopping.Register(OnStopping);

            return application;
        }

        /// <summary>
        /// Configures the metrics settings.
        /// </summary>
        /// <param name="metrics">The metrics builder.</param>
        public void ConfigureMetrics(IMetricsBuilder? metrics)
        {
            if (metrics is null)
                return;
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                if (Modules[I] is not IAspNetModule Module)
                    continue;
                // Configure metrics settings
                metrics = Module.ConfigureMetrics(metrics, Configuration, Environment);
            }
        }

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="webHost">The web host builder</param>
        public void ConfigureWebHostSettings(IWebHostBuilder? webHost)
        {
            if (webHost is null)
                return;
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                if (Modules[I] is not IAspNetModule Module)
                    continue;
                // Configure web host settings
                webHost = Module.ConfigureWebHostSettings(webHost, Configuration, Environment);
            }
        }
    }
}