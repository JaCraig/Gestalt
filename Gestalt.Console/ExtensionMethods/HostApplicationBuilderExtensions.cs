using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Gestalt.Console.ExtensionMethods
{
    /// <summary>
    /// Host application builder extensions
    /// </summary>
    public static class HostApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the Gestalt framework.
        /// </summary>
        /// <param name="app">The app builder</param>
        /// <param name="args">The command line arguments</param>
        /// <param name="assemblies">The assemblies to look in for modules</param>
        /// <returns>The resulting host</returns>
        public static IHost? UseGestalt(this HostApplicationBuilder? app, string?[]? args, params Assembly?[]? assemblies)
        {
            if (app?.Services.IsReadOnly != false)
                return null;

            var App = new ConsoleApplication(app.Configuration, app.Environment, assemblies);

            // Configures the configuration settings
            _ = App.ConfigureConfigurationSettings(app.Configuration, args);

            // Configures logging settings
            _ = App.ConfigureLoggingSettings(app.Logging);

            // Configures metrics
            App.ConfigureMetrics(app.Metrics);

            // Configure services.
            _ = App.ConfigureServices(app.Services);

            return app.Build();
        }
    }
}