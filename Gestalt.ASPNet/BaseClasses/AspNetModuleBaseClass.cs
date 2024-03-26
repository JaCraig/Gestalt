using Gestalt.ASPNet.Interfaces;
using Gestalt.Core.BaseClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.BaseClasses
{
    /// <summary>
    /// Base class for ASP.NET modules.
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="ApplicationModuleBaseClass{TModule}"/>
    /// <seealso cref="IAspNetModule"/>
    public abstract class AspNetModuleBaseClass<TModule> : ApplicationModuleBaseClass<TModule>, IAspNetModule
        where TModule : ApplicationModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="contentPath">The content path.</param>
        /// <param name="tags">The tags.</param>
        protected AspNetModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags)
            : base(name, category, contentPath, tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected AspNetModuleBaseClass()
        {
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The application builder.</returns>
        public virtual IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment) => applicationBuilder;

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">Endpoint route builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The endpoint route builder.</returns>
        public virtual IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment) => endpoints;

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="webHost">The web host builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>The web host builder</returns>
        public virtual IWebHostBuilder? ConfigureWebHostSettings(IWebHostBuilder? webHost, IConfiguration? configuration, IHostEnvironment? environment) => webHost;
    }
}