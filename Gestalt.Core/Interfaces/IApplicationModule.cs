using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Gestalt.Core.Interfaces
{
    /// <summary>
    /// Application Module Interface
    /// </summary>
    public interface IApplicationModule
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        string Category { get; }

        /// <summary>
        /// The content path
        /// </summary>
        /// <value>The content path.</value>
        string ContentPath { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string ID { get; }

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        DateTime LastModified { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        string[] Tags { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; }

        /// <summary>
        /// Used to configure configuration settings.
        /// </summary>
        /// <param name="configuration">Configuration builder</param>
        /// <param name="environment">The host environment</param>
        /// <param name="args">The command line arguments</param>
        /// <returns>The configuration builder.</returns>
        IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string[] args);

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Host builder</returns>
        IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Logging builder</returns>
        ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the metrics.
        /// </summary>
        /// <param name="metrics">The metrics builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>The metrics builder</returns>
        IMetricsBuilder ConfigureMetrics(IMetricsBuilder metrics, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Called when the application is [started].
        /// </summary>
        void OnStarted();

        /// <summary>
        /// Called when the application is [stopped].
        /// </summary>
        void OnStopped();

        /// <summary>
        /// Called when the application is [stopping].
        /// </summary>
        void OnStopping();
    }
}