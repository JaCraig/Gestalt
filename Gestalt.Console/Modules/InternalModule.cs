using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.Console.Modules
{
    /// <summary>
    /// Internal module
    /// </summary>
    /// <seealso cref="ApplicationModuleBaseClass{InternalModule}"/>
    public class InternalModule : ApplicationModuleBaseClass<InternalModule>
    {
        /// <summary>
        /// Used to add the internal hosted service to the application.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment</param>
        /// <returns>The service collection</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment) => services?.AddHostedService<InternalHostedService>();
    }
}