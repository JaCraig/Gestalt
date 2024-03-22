using BigBook;
using Gestalt.Core.ExtensionMethods;
using Gestalt.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Gestalt.Core.BaseClasses
{
    /// <summary>
    /// Application info holder.
    /// </summary>
    /// <seealso cref="IApplication"/>
    public abstract class ApplicationBaseClass : IApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBaseClass"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The host environment</param>
        /// <param name="assemblies">
        /// The assemblies to search for modules, if empty the base application directory is searched.
        /// </param>
        protected ApplicationBaseClass(IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies)
        {
            configuration ??= new ConfigurationBuilder().Build();
            IConfigurationSection? LoggingConfiguration = configuration.GetSection("Logging");
            InternalLogger = LoggerFactory.Create(builder =>
            {
                try
                {
                    if (LoggingConfiguration is not null)
                        builder = builder.AddConfiguration(LoggingConfiguration);
                }
                catch { }
                _ = builder.AddConsole();
            }).CreateLogger("Gestalt");
            var EntryAssembly = Assembly.GetEntryAssembly();
            if (assemblies is null || assemblies.Length == 0)
                assemblies = EntryAssembly.FindAssemblies();

            InternalLogger.LogInformation("Starting application {entryAssemblyName}", EntryAssembly?.GetName().Name);
            InternalLogger.LogInformation("Assembly Count: {assemblyCount}", assemblies.Length);
            if (assemblies.Length > 0)
                InternalLogger.LogInformation("Searching assemblies {assemblies}", assemblies.ToString(x => x?.GetName().Name ?? "", ", "));

            Configuration = configuration;
            Environment = env;
            Modules = assemblies.FindModules();

            InternalLogger.LogInformation("Module Count: {moduleCount}", Modules.Length);
            if (Modules.Length > 0)
                InternalLogger.LogInformation("Using modules {modules}", Modules.ToString(x => x?.Name ?? "", ", "));

            Frameworks = assemblies.FindFrameworks();

            InternalLogger.LogInformation("Framework Count: {frameworkCount}", Frameworks.Length);
            if (Frameworks.Length > 0)
                InternalLogger.LogInformation("Using frameworks {frameworks}", Frameworks.ToString(x => x?.Name ?? "", ", "));

            Name = EntryAssembly?.GetName().Name ?? "";
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        protected IConfiguration? Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>The environment.</value>
        protected IHostEnvironment? Environment { get; }

        /// <summary>
        /// Gets the frameworks.
        /// </summary>
        /// <value>The frameworks.</value>
        protected IApplicationFramework[] Frameworks { get; }

        /// <summary>
        /// Gets the internal logger.
        /// </summary>
        protected ILogger? InternalLogger { get; }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <value>The modules.</value>
        protected IApplicationModule[] Modules { get; }

        /// <summary>
        /// Configures the configuration settings.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="args">The command line arguments</param>
        /// <returns>The configuration builder.</returns>
        public IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, string?[]? args)
        {
            if (configuration is null)
                return configuration;

            args ??= Array.Empty<string?>();

            InternalLogger?.LogInformation("Configuring configuration settings");

            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                configuration = Module.ConfigureConfigurationSettings(configuration, Environment, args);
            }
            return configuration;
        }

        /// <summary>
        /// Configures the framework specific services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services.</returns>
        public IServiceCollection? ConfigureFrameworks(IServiceCollection? services)
        {
            if (services is null)
                return services;

            InternalLogger?.LogInformation("Configuring framework specific services");

            for (int I = 0, FrameworksLength = Frameworks.Length; I < FrameworksLength; I++)
            {
                IApplicationFramework Framework = Frameworks[I];
                services = Framework.Configure(Modules, services, Configuration, Environment);
            }
            return services;
        }

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <returns>Host builder</returns>
        public IHostBuilder? ConfigureHostSettings(IHostBuilder? host)
        {
            if (host is null)
                return host;

            InternalLogger?.LogInformation("Configuring host settings");

            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                host = Module.ConfigureHostSettings(host, Configuration, Environment);
            }
            return host;
        }

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <returns>Logging builder</returns>
        public ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging)
        {
            if (logging is null)
                return logging;

            InternalLogger?.LogInformation("Configuring logging settings");

            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                logging = Module.ConfigureLoggingSettings(logging, Configuration, Environment);
            }
            return logging;
        }

        /// <summary>
        /// Configures the services for MVC.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <returns>The service collection</returns>
        public IServiceCollection? ConfigureServices(IServiceCollection? services)
        {
            if (Configuration is null || Environment is null || services is null)
                return services;

            InternalLogger?.LogInformation("Configuring services");

            // Set up advanced configuration abilities.
            services = services?.AddCanisterModules();

            // Add application
            services = services?.AddSingleton<IApplication>(this);

            // Add options
            services = services?.AddOptions();

            //Add modules
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                services = services?.AddSingleton(Module);
            }
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                services = Module.ConfigureServices(services, Configuration, Environment);
            }
            return services;
        }

        /// <summary>
        /// Called when [started].
        /// </summary>
        public void OnStarted()
        {
            InternalLogger?.LogInformation("Application started");
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                Module.OnStarted();
            }
        }

        /// <summary>
        /// Called when [stopped].
        /// </summary>
        public void OnStopped()
        {
            InternalLogger?.LogInformation("Application stopped");
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                Module.OnStopped();
            }
        }

        /// <summary>
        /// Called when [shutdown].
        /// </summary>
        public void OnStopping()
        {
            InternalLogger?.LogInformation("Application stopping");
            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                IApplicationModule Module = Modules[I];
                Module.OnStopping();
            }
        }
    }
}