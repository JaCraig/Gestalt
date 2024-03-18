using BigBook;
using Gestalt.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gestalt.Core.BaseClasses
{
    /// <summary>
    /// Application framework base class
    /// </summary>
    /// <seealso cref="IApplicationFramework"/>
    public abstract class ApplicationFrameworkBaseClass<TApplicationFramework, TModule> : IEquatable<TApplicationFramework>, IApplicationFramework
        where TApplicationFramework : ApplicationFrameworkBaseClass<TApplicationFramework, TModule>, new()
        where TModule : IApplicationModule
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ApplicationFrameworkBaseClass{TApplicationFramework, TModule}"/> class.
        /// </summary>
        /// <param name="name">The name of the application framework.</param>
        protected ApplicationFrameworkBaseClass(string? name)
        {
            Name = name ?? typeof(TModule).GetName().AddSpaces();
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ApplicationFrameworkBaseClass{TApplicationFramework, TModule}"/> class.
        /// </summary>
        protected ApplicationFrameworkBaseClass()
            : this(null)
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; protected set; } = typeof(TApplicationFramework)
            .GetName()
            .Trim()
            .Replace(" ", "-", StringComparison.OrdinalIgnoreCase)
            .Replace("--", "-", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime LastModified { get; } = new FileInfo(typeof(TApplicationFramework).Assembly.Location).LastWriteTimeUtc;

        /// <summary>
        /// Application framework name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public virtual int Order { get; protected set; } = ApplicationFrameworkDefaults.Order.Default;

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; } = typeof(TApplicationFramework).Assembly.GetName().Version?.ToString() ?? "";

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ApplicationFrameworkBaseClass<TApplicationFramework, TModule>? class1, ApplicationFrameworkBaseClass<TApplicationFramework, TModule>? class2) => !(class1 == class2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ApplicationFrameworkBaseClass<TApplicationFramework, TModule>? class1, ApplicationFrameworkBaseClass<TApplicationFramework, TModule>? class2) => EqualityComparer<ApplicationFrameworkBaseClass<TApplicationFramework, TModule>>.Default.Equals(class1, class2);

        /// <summary>
        /// Calls the appropriate methods on the modules to configure the application.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The modules.</returns>
        public IServiceCollection? Configure(IApplicationModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            modules ??= Array.Empty<IApplicationModule>();
            ConfigureModules(modules.OfType<TModule>().ToArray(), services, configuration, environment);
            return services;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as TApplicationFramework);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(TApplicationFramework? other)
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
        /// Calls the appropriate methods on the modules to configure the application.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="environment">The host environment.</param>
        protected abstract void ConfigureModules(TModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment);
    }
}