using Gestalt.ASPNet.SignalR.Interfaces;
using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.SignalR
{
    /// <summary>
    /// MVC Framework class
    /// </summary>
    /// <seealso cref="ApplicationFrameworkBaseClass{MvcFramework, IMvcModule}"/>
    public class SignalRFramework : ApplicationFrameworkBaseClass<SignalRFramework, ISignalRModule>
    {
        /// <inheritdoc/>
        protected override void ConfigureModules(ISignalRModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (configuration is null || environment is null || services is null)
                return;

            modules ??= [];

            // SignalR builder, setup the options.
            Microsoft.AspNetCore.SignalR.ISignalRServerBuilder? Builder = services.AddSignalR(options =>
            {
                for (int I = 0, ModulesLength = modules.Length; I < ModulesLength; I++)
                {
                    ISignalRModule Module = modules[I];
                    if (Module is null)
                        continue;
                    options = Module.Options(options, configuration, environment) ?? options;
                }
            });

            //Let modules configure MVC
            for (int I = 0, ModulesLength = modules.Length; I < ModulesLength; I++)
            {
                ISignalRModule Module = modules[I];
                if (Module is null)
                    continue;
                Builder = Module.ConfigureSignalR(Builder, configuration, environment);
            }
        }
    }
}