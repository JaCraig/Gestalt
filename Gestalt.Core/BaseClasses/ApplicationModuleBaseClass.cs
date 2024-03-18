using BigBook;
using Gestalt.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gestalt.Core.BaseClasses
{
    /// <summary>
    /// Module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="IApplicationModule"/>
    /// <seealso cref="IEquatable{TModule}"/>
    /// <seealso cref="IEquatable{ApplicationModuleBaseClass}"/>
    /// <seealso cref="IApplicationModule"/>
    public abstract class ApplicationModuleBaseClass<TModule> : IEquatable<TModule>, IApplicationModule
        where TModule : ApplicationModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <param name="category">The category for the module.</param>
        /// <param name="contentPath">The content path for the module.</param>
        /// <param name="tags">The tags associated with the module.</param>
        protected ApplicationModuleBaseClass(string? name, string? category, string? contentPath, params string[] tags)
        {
            Name = name ?? typeof(TModule).GetName().Replace((typeof(TModule).Namespace ?? "") + ".", "");
            Category = category ?? typeof(TModule).Namespace?.Split(".", StringSplitOptions.RemoveEmptyEntries).Skip(1).FirstOrDefault() ?? "";
            Tags = tags ?? Array.Empty<string>();
            ContentPath = contentPath ?? $"wwwroot/Content/{ID}/";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected ApplicationModuleBaseClass()
            : this(null, null, null)
        {
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Category { get; protected set; }

        /// <summary>
        /// The content path
        /// </summary>
        /// <value>The content path.</value>
        public string ContentPath { get; protected set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; protected set; } = typeof(TModule)
            .GetName()
            .Trim()
            .Replace(" ", "-", StringComparison.OrdinalIgnoreCase)
            .Replace("--", "-", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime LastModified { get; } = new FileInfo(typeof(TModule).Assembly.Location).LastWriteTimeUtc;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public virtual int Order { get; protected set; } = ApplicationModuleDefaults.Order.Default;

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string[] Tags { get; protected set; } = Array.Empty<string>();

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; } = typeof(TModule).Assembly.GetName().Version?.ToString() ?? "";

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ApplicationModuleBaseClass<TModule>? class1, ApplicationModuleBaseClass<TModule>? class2) => !(class1 == class2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ApplicationModuleBaseClass<TModule>? class1, ApplicationModuleBaseClass<TModule>? class2) => EqualityComparer<ApplicationModuleBaseClass<TModule>>.Default.Equals(class1, class2);

        /// <summary>
        /// Configure the configuration settings.
        /// </summary>
        /// <param name="configuration">Configuration builder</param>
        /// <param name="environment">The host environment</param>
        /// <param name="args">The command line arguments</param>
        /// <returns>The configuration builder</returns>
        public virtual IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string[] args) => configuration;

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Host builder</returns>
        public virtual IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment) => host;

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Logging builder</returns>
        public virtual ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment) => logging;

        /// <summary>
        /// Configures the metrics.
        /// </summary>
        /// <param name="metrics">The metrics builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>The metrics builder</returns>
        public virtual IMetricsBuilder ConfigureMetrics(IMetricsBuilder metrics, IConfiguration? configuration, IHostEnvironment? environment) => metrics;

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public virtual IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment) => services;

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as TModule);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(TModule? other)
        {
            return other is not null
                   && ID == other.ID;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(ID);

        /// <summary>
        /// Called when the application is [started].
        /// </summary>
        public virtual void OnStarted()
        { }

        /// <summary>
        /// Called when the application is [stopped].
        /// </summary>
        public virtual void OnStopped()
        { }

        /// <summary>
        /// Called when the application is [stopping].
        /// </summary>
        public virtual void OnStopping()
        { }
    }
}