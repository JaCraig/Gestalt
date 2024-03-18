using Gestalt.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.Interfaces
{
    /// <summary>
    /// ASP.NET Module
    /// </summary>
    public interface IAspNetModule : IApplicationModule
    {
        /// <summary>
        /// Called to configure the application.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The application builder.</returns>
        IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">Endpoint route builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The endpoint route builder.</returns>
        IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder endpoints, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="webHost">The web host builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>The web host builder</returns>
        IWebHostBuilder ConfigureWebHostSettings(IWebHostBuilder webHost, IConfiguration? configuration, IHostEnvironment? environment);
    }
}