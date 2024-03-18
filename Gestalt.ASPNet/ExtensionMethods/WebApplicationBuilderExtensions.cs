using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Gestalt.ASPNet.ExtensionMethods
{
    /// <summary>
    /// WebApplicationBuilder Extensions
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Sets up the Gestalt application modules.
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="args">The command line arguments</param>
        /// <param name="assemblies">
        /// The assemblies to search for modules, if empty the base application directory is searched.
        /// </param>
        /// <returns>The host application builder.</returns>
        public static WebApplication? UseGestalt(this WebApplicationBuilder? app, string[] args, params Assembly?[]? assemblies)
        {
            if (app is null)
                return null;

            // Create app framework.
            var App = new AspNetApplication(app.Configuration, app.Environment, assemblies);

            // Configures the configuration settings
            _ = App.ConfigureConfigurationSettings(app.Configuration, args);

            // Configures the host settings
            _ = App.ConfigureHostSettings(app.Host);

            // Configures the WebHost settings
            App.ConfigureWebHostSettings(app.WebHost);

            // Configures logging settings
            _ = App.ConfigureLoggingSettings(app.Logging);

#if NET8_0_OR_GREATER
            // Configure metrics settings
            App.ConfigureMetrics(app.Metrics);
#endif

            // Configure frameworks (e.g. MVC, SignalR, etc.)
            _ = App.ConfigureFrameworks(app.Services);

            // Configure services.
            _ = App.ConfigureServices(app.Services);

            // Build the application object.
            WebApplication Application = app.Build();

            // Configure the application.
            return App.ConfigureApplication(Application);
        }
    }
}