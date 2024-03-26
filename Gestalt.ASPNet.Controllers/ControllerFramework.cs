using Gestalt.ASPNet.Controllers.Interfaces;
using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Gestalt.ASPNet.Controllers
{
    /// <summary>
    /// MVC Framework controller only class
    /// </summary>
    /// <seealso cref="ApplicationFrameworkBaseClass{MvcFramework, IMvcModule}"/>
    public class ControllerFramework : ApplicationFrameworkBaseClass<ControllerFramework, IControllerModule>
    {
        /// <inheritdoc/>
        protected override void ConfigureModules(IControllerModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (configuration is null || environment is null || services is null)
                return;

            modules ??= Array.Empty<IControllerModule>();

            // MVC Builder, setup the options.
            IMvcBuilder? MVCBuilder = services.AddControllers(options =>
            {
                for (int I = 0, ModulesLength = modules.Length; I < ModulesLength; I++)
                {
                    IControllerModule Module = modules[I];
                    if (Module is null)
                        continue;
                    options = Module.Options(options, configuration, environment) ?? options;
                }
            });

            //Let modules configure MVC
            for (int I = 0, ModulesLength = modules.Length; I < ModulesLength; I++)
            {
                IControllerModule Module = modules[I];
                if (Module is null)
                    continue;
                var ModuleAssembly = Module.GetType().Assembly;
                var ModuleName = ModuleAssembly.FullName;
                MVCBuilder = Module.ConfigureMVC(MVCBuilder, configuration, environment);
                if (MVCBuilder?.PartManager?.ApplicationParts.Any(x => x.Name == ModuleName) == false)
                    _ = MVCBuilder?.AddApplicationPart(ModuleAssembly);
            }
        }
    }
}