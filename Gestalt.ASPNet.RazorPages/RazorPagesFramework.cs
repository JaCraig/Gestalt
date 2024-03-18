using Gestalt.ASPNet.RazorPages.Interfaces;
using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Gestalt.ASPNet.RazorPages
{
    /// <summary>
    /// Razor Pages Framework class
    /// </summary>
    public class RazorPagesFramework : ApplicationFrameworkBaseClass<RazorPagesFramework, IRazorPagesModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RazorPagesFramework"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        protected override void ConfigureModules(IRazorPagesModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (configuration is null || environment is null || services is null)
                return;

            modules ??= Array.Empty<IRazorPagesModule>();

            IMvcBuilder? MVCBuilder = services.AddRazorPages(options =>
            {
                for (var I = 0; I < modules.Length; I++)
                {
                    IRazorPagesModule Module = modules[I];
                    if (Module is null)
                        continue;
                    options = Module.Options(options, configuration, environment);
                }
            });

            for (var I = 0; I < modules.Length; I++)
            {
                IRazorPagesModule Module = modules[I];
                if (Module is null)
                    continue;
                MVCBuilder = Module.ConfigureRazorPages(MVCBuilder, configuration, environment);
                if (MVCBuilder?.PartManager.ApplicationParts.Any(x => x.Name == Module.GetType().Assembly.FullName) == false)
                    _ = MVCBuilder?.AddApplicationPart(Module.GetType().Assembly);
            }
        }
    }
}