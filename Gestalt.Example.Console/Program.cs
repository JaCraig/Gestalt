using Gestalt.Console.ExtensionMethods;
using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.Example.Console
{
    /// <summary>
    /// Example service interface.
    /// </summary>
    public interface IExampleService
    {
        /// <summary>
        /// Runs this instance of the service.
        /// </summary>
        void Run();
    }

    /// <summary>
    /// This is the host service that will be started when the application starts. It will start all
    /// of the services that implement the IExampleService interface.
    /// </summary>
    public class ExampleHostService : IHostedService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleHostService"/> class. This class
        /// takes in a collection of services that implement the IExampleService interface.
        /// </summary>
        /// <param name="services">The example services</param>
        public ExampleHostService(IEnumerable<IExampleService> services)
        {
            Services = services;
        }

        /// <summary>
        /// Gets the services that implement the IExampleService interface.
        /// </summary>
        public IEnumerable<IExampleService> Services { get; }

        /// <summary>
        /// Starts the services.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Starting services");
            foreach (IExampleService Service in Services)
            {
                Service.Run();
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the services.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Stopping services");
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Example module class. With modules you can either inherit from the
    /// ApplicationModuleBaseClass or implement the IApplicationModule interface. In the base class,
    /// there are default methods for configuration, services, lifecycle events, etc. that can be
    /// overridden. In this example, we are using the base class.
    /// </summary>
    public class ExampleModule : ApplicationModuleBaseClass<ExampleModule>
    {
        /// <summary>
        /// This method is called when setting up the configuration settings.
        /// </summary>
        /// <param name="configuration">
        /// The configuration builder that you can use to set up the configuration settings.
        /// </param>
        /// <param name="environment">The host environment.</param>
        /// <param name="args">
        /// The command line arguments that are passed in from the UseGestalt extension method.
        /// </param>
        /// <returns>The configuration builder.</returns>
        public override IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string[] args) => configuration?.AddCommandLine(args);

        /// <summary>
        /// This method is called when setting up the services needed for the module. This is where
        /// you would add your services to the service collection.
        /// </summary>
        /// <param name="services">The service collection that you can use to add your services.</param>
        /// <param name="configuration">The configuration information.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The service collection.</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            // Add all services that implement the IExampleService interface to the service
            // collection as transient services. We could also add them one by one using the
            // AddTransient method.
            return services?.AddAllTransient<IExampleService>()
                           ?.AddHostedService<ExampleHostService>();
        }

        /// <summary>
        /// This method is called when the application is started.
        /// </summary>
        public override void OnStarted() => System.Console.WriteLine("Application has started.");

        /// <summary>
        /// This method is called when the application is stopped.
        /// </summary>
        public override void OnStopped() => System.Console.WriteLine("Application has stopped.");

        /// <summary>
        /// This method is called when the application is stopping.
        /// </summary>
        public override void OnStopping() => System.Console.WriteLine("Application is stopping.");
    }

    /// <summary>
    /// This is an example service that we will add to the service collection.
    /// </summary>
    public class ExampleService1 : IExampleService
    {
        /// <summary>
        /// Runs this instance of the service.
        /// </summary>
        public void Run() => System.Console.WriteLine("Hello from ExampleService1!");
    }

    /// <summary>
    /// This is another example service that we will add to the service collection.
    /// </summary>
    public class ExampleService2 : IExampleService
    {
        /// <summary>
        /// Runs this instance of the service.
        /// </summary>
        public void Run() => System.Console.WriteLine("Hello from ExampleService2!");
    }

    /// <summary>
    /// The main program class. This example shows how to use the Gestalt framework in a console application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        private static void Main(string[] args)
        {
            IHost? HostApp = Host.CreateApplicationBuilder().UseGestalt(args);

            HostApp?.Run();
        }
    }
}