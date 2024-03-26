using Gestalt.ASPNet.MVC.Interfaces;
using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Gestalt.ASPNet.MVC
{
    /// <summary>
    /// MVC Framework class
    /// </summary>
    /// <seealso cref="ApplicationFrameworkBaseClass{MvcFramework, IMvcModule}"/>
    public class MvcFramework : ApplicationFrameworkBaseClass<MvcFramework, IMvcModule>
    {
        /// <inheritdoc/>
        protected override void ConfigureModules(IMvcModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (configuration is null || environment is null || services is null)
                return;

            modules ??= Array.Empty<IMvcModule>();

            // MVC Builder, setup the options.
            IMvcBuilder? MVCBuilder = services.AddControllersWithViews(options =>
            {
                for (int I = 0, ModulesLength = modules.Length; I < ModulesLength; I++)
                {
                    IMvcModule Module = modules[I];
                    if (Module is null)
                        continue;
                    options = Module.Options(options, configuration, environment);
                }
            });

            //Let modules configure MVC
            for (int I = 0, ModulesLength = modules.Length; I < ModulesLength; I++)
            {
                IMvcModule Module = modules[I];
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