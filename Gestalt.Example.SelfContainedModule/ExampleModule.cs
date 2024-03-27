using Gestalt.ASPNet.MVC.BaseClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.Example.SelfContainedModule
{
    /// <summary>
    /// This module is self-contained and can be used in any project.
    /// As long as the project is using Gestalt, this module can be created and reused as needed.
    /// In order to test it out, it is already added to the Gestalt.Example project. Just run the project and navigate to /healthcheck or /health.
    /// </summary>
    public class ExampleModule : MvcModuleBaseClass<ExampleModule>
    {
        /// <summary>
        /// Let's set the order to the lowest possible value so it's loaded first.
        /// </summary>
        public override int Order => int.MinValue;

        /// <summary>
        /// Let's set up health checks for the application.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder</param>
        /// <param name="configuration">The configuration</param>
        /// <param name="environment">The host environment</param>
        /// <returns>The endpoint route builder</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            endpoints?.MapHealthChecks("/healthcheck");
            return endpoints;
        }

        /// <summary>
        /// Here we add the health checks to the services and add a custom health check class.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration</param>
        /// <param name="environment">The host environment</param>
        /// <returns>The service collection</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            services?.AddHealthChecks()
                .AddCheck<SystemStatusHealthCheck>("System", null, ["System"], new TimeSpan(0, 0, 3));
            return services;
        }
    }
}