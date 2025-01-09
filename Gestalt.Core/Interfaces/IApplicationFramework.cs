using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Gestalt.Core.Interfaces
{
    /// <summary>
    /// The application framework interface. This is used to configure the framework specific modules.
    /// </summary>
    public interface IApplicationFramework
    {
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
        /// Application framework name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; }

        /// <summary>
        /// Calls the appropriate methods on the modules to configure the application.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The services.</returns>
        [return: NotNullIfNotNull(nameof(services))]
        public IServiceCollection? Configure(IApplicationModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment);
    }
}