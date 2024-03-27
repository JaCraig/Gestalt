using Gestalt.ASPNet.ExtensionMethods;
using Gestalt.ASPNet.MVC.BaseClasses;
using Gestalt.ASPNet.MVC.Interfaces;
using Gestalt.ASPNet.RazorPages.BaseClasses;
using Gestalt.ASPNet.RazorPages.Interfaces;
using Gestalt.ASPNet.SignalR.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Diagnostics.Metrics;

namespace Gestalt.Example
{
    /// <summary>
    /// In this example, we are creating a basic web application using Gestalt. We are using the default configuration settings and showing examples of building your own modules.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application. This is where the application is configured and run.
        /// </summary>
        /// <param name="args">The command line arguments</param>
        public static void Main(string[] args)
        {
            // Create a new web application builder as normal.
            WebApplicationBuilder Builder = WebApplication.CreateBuilder(args);

            // Call the UseGestalt extension method and the Gestalt library will find your modules, use them to configure your application, and build your WebApplication object.
            // In this example, we have added the Gestalt.ASPNet.MVC, Gestalt.ASPNet.SignalR, and Gestalt.ASPNet.RazorPages libraries to the project.
            // As such, the UseGestalt extesion will automatically add routing, call AddControllersWithViews, AddRazorPages, and AddSignalR for us.
            // It will then allow the modules to configure the parts of the application that they are responsible for.
            WebApplication? App = Builder.UseGestalt(args);

            // If the application is null, then the Gestalt library was unable to build the application.
            // Normally an exception would be thrown but if the WebApplicationBuilder is null, then a null WebApplication object is returned.
            if (App is null)
                return;

            // Note that you can still configure the application as normal.
            // Gestalt is meant to help you build your application in a modular way but you can still configure it as you see fit.
            // This also applies above to the WebApplicationBuilder. You can always configure it as you see fit prior to calling UseGestalt.

            App.Map("/Custom", app =>
            {
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
                });
            });

            // Run the application as normal.
            App.Run();
        }
    }

    /// <summary>
    /// This is an example of a basic module that configures the application with the usual default settings when creating a new web application.
    /// It implements the MvcModuleBaseClass which simplifies the process of creating a module that needs to modify MVC settings.
    /// However, you can implement the IMvcModule interface directly if you prefer.
    /// </summary>
    public class BasicModule : MvcModuleBaseClass<BasicModule>
    {
        /// <summary>
        /// This is called to configure the IApplicationBuilder object. Since this is an MVC app, that would be the WebApplication object.
        /// </summary>
        /// <param name="applicationBuilder">The application builder object.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="environment">The host environment object.</param>
        /// <returns>The application builder object should be returned.</returns>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (applicationBuilder is null)
                return applicationBuilder;

            // Configure the HTTP request pipeline.
            if (environment?.IsDevelopment() == false)
            {
                // This is the default exception handler for the application when in production.
                _ = applicationBuilder.UseExceptionHandler("/Home/Error");

                // We will add a strict transport security header to the response.
                _ = applicationBuilder.UseHsts();
            }

            // This will redirect HTTP requests to HTTPS.
            _ = applicationBuilder.UseHttpsRedirection();
            // And let's serve static files.
            _ = applicationBuilder.UseStaticFiles();
            // We will also add authorization to the application.
            _ = applicationBuilder.UseAuthorization();
            // And lastly, we will return the application builder.
            return applicationBuilder;
        }

        /// <summary>
        /// This is called to configure the MVC options. We will just return the options object as is.
        /// </summary>
        /// <param name="mVCBuilder">The MVC builder object.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="environment">The host environment object.</param>
        /// <returns>The MVC builder object.</returns>
        public override IMvcBuilder? ConfigureMVC(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return mVCBuilder;
        }

        /// <summary>
        /// This is called to configure our endpoint routes. For our example, we will just use the default route.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="environment">The host environment object.</param>
        /// <returns>The endpoint route builder.</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (endpoints is null)
                return endpoints;

            // Map the default route.
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            return endpoints;
        }
    }

    /// <summary>
    /// <para>
    /// All modules implement the IApplicationModule interface. As such, they all have certain methods that they must implement and can be used to configure the application.
    /// Module interfaces then get more and more specific to the application type. For example, IAspNetModule adds application, route, and web app configuration methods.
    /// IMvcModule is used to configure MVC settings, like the BasicModule example.
    /// </para>
    /// <para>This example though implements the IRazorPagesModule interface. This is used to configure Razor Pages settings.</para>
    /// </summary>
    public class BasicRazorPagesModule : RazorPagesModuleBaseClass<BasicRazorPagesModule>
    {
        /// <summary>
        /// Sometimes modules need to be loaded in a specific order. By default, the order number is 100.
        /// Lower numbers are loaded first, as such this module will be loaded prior to the BasicModule.
        /// </summary>
        public override int Order => 20;

        /// <summary>
        /// This method is called to configure Razor Pages settings.
        /// </summary>
        /// <param name="mVCBuilder">The MVC builder object.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="environment">The host environment object.</param>
        /// <returns>The configured MVC builder object.</returns>
        public override IMvcBuilder? ConfigureRazorPages(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return mVCBuilder;
        }
    }

    /// <summary>
    /// The other examples use the *BaseClass classes to simplify the process of creating a module.
    /// However, you can implement the interfaces directly if you prefer or need to due to the module that you are trying to build.
    /// Note though, that I would recommend breaking down your modules into smaller parts if you find yourself needing to implement multiple interfaces.
    /// </summary>
    public class MoreAdvancedModule : IMvcModule, ISignalRModule, IRazorPagesModule
    {
        /// <summary>
        /// The category of the module. This is used to group modules together.
        /// When using the *BaseClass classes, this is the last part of the namespace that the module is in by default.
        /// </summary>
        public string Category { get; } = "Example Category";

        /// <summary>
        /// The content path of the module. Gestalt does not directly use this, but it is available for your use.
        /// For adding static web content, you would still configure the StaticWebAssetBasePath in the csproj file.
        /// When using the *BaseClass classes, this defaults to "wwwroot/Content/{Category}/{Name}".
        /// </summary>
        public string ContentPath { get; } = "MyContentPath";

        /// <summary>
        /// The ID of the module. This is used to identify the module and determine if there are any conflicts.
        /// This defaults to the module's type name.
        /// </summary>
        public string ID { get; } = "ExampleID";

        /// <summary>
        /// The last modified date of the module. This is used to determine if the module has been updated.
        /// When using the *BaseClass classes, this is the assembly last modified date.
        /// </summary>
        public DateTime LastModified { get; } = DateTime.Now;

        /// <summary>
        /// The display name of the module.
        /// By default this would be the name of the module class.
        /// </summary>
        public string Name { get; } = "Example";

        /// <summary>
        /// The order that the module is loaded in. Lower numbers are loaded first.
        /// By default, this is 100.
        /// </summary>
        public int Order { get; } = 0;

        /// <summary>
        /// The tags of the module. This is used to group modules together but is not used by Gestalt directly.
        /// </summary>
        public string?[] Tags { get; } = new string?[] { "Example" };

        /// <summary>
        /// The version of the module.
        /// By default, this is the assembly version number.
        /// </summary>
        public string Version { get; } = "1.0.0";

        /// <summary>
        /// This is called to configure the application.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The application builder.</returns>
        public IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment) => applicationBuilder;

        /// <summary>
        /// This is called to configure the configuration settings.
        /// </summary>
        /// <param name="configuration">The configuration builder.</param>
        /// <param name="environment">The host environment.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The configuration builder.</returns>
        public IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string?[]? args) => configuration;

        /// <summary>
        /// This is called to configure the host settings.
        /// </summary>
        /// <param name="host">The host builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The host builder.</returns>
        public IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment) => host;

        /// <summary>
        /// This is called to configure the logging settings.
        /// </summary>
        /// <param name="logging">The logging builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The logging builder.</returns>
        public ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment) => logging;

        /// <summary>
        /// This is called to configure the metrics settings. Note that this is only called if the metrics builder is available.
        /// In .Net 6, the metrics builder is not available by default and thus is not called. However, in .Net 8, it is available.
        /// The module can still be used in .Net 6 or 8, but the metrics builder will not be available in .Net 6.
        /// </summary>
        /// <param name="metrics">The metrics builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The metrics builder.</returns>
        public IMetricsBuilder? ConfigureMetrics(IMetricsBuilder? metrics, IConfiguration? configuration, IHostEnvironment? environment) => metrics;

        /// <summary>
        /// This is called to configure the MVC settings.
        /// </summary>
        /// <param name="mVCBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The MVC builder.</returns>
        public IMvcBuilder? ConfigureMVC(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment) => mVCBuilder;

        /// <summary>
        /// This is called to configure the Razor Pages settings.
        /// </summary>
        /// <param name="mVCBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The MVC builder.</returns>
        public IMvcBuilder? ConfigureRazorPages(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment) => mVCBuilder;

        /// <summary>
        /// This is called to configure endpoint routes.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The endpoint route builder.</returns>
        public IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder endpoints, IConfiguration? configuration, IHostEnvironment? environment) => endpoints;

        /// <summary>
        /// This is called to configure the services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The service collection.</returns>
        public IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment) => services;

        /// <summary>
        /// This is called to configure SignalR settings.
        /// </summary>
        /// <param name="builder">The SignalR server builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The SignalR server builder.</returns>
        public ISignalRServerBuilder? ConfigureSignalR(ISignalRServerBuilder? builder, IConfiguration? configuration, IHostEnvironment? environment) => builder;

        /// <summary>
        /// This is called to configure the web host settings.
        /// </summary>
        /// <param name="webHost">The web host builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The web host builder.</returns>
        public IWebHostBuilder? ConfigureWebHostSettings(IWebHostBuilder? webHost, IConfiguration? configuration, IHostEnvironment? environment) => webHost;

        /// <summary>
        /// This is called when the application is started. Any code that needs to be run when the application starts can be placed here.
        /// </summary>
        public void OnStarted()
        { }

        /// <summary>
        /// This is called when the application is stopped. Any code that needs to be run when the application stops can be placed here.
        /// </summary>
        public void OnStopped()
        { }

        /// <summary>
        /// This is called when the application is stopping. Any code that needs to be run when the application is stopping can be placed here.
        /// </summary>
        public void OnStopping()
        { }

        /// <summary>
        /// This is called to configure the options for razor pages.
        /// Note that you can always skip this method and instead configure the options in the ConfigureServices method.
        /// It's more of a convenience method than necessary.
        /// </summary>
        /// <param name="options">The razor pages options object.</param>
        /// <param name="configuration">The configuration</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The razor pages options object.</returns>
        public RazorPagesOptions? Options(RazorPagesOptions? options, IConfiguration? configuration, IHostEnvironment? environment) => options;

        /// <summary>
        /// This is called to configure the options for SignalR
        /// Note that you can always skip this method and instead configure the options in the ConfigureServices method.
        /// It's more of a convenience method than necessary.
        /// </summary>
        /// <param name="options">The SignalR hub options object.</param>
        /// <param name="configuration">The configuration</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The SignalR hub options object.</returns>
        public HubOptions? Options(HubOptions? options, IConfiguration? configuration, IHostEnvironment? environment) => options;

        /// <summary>
        /// This is called to configure the options for MVC.
        /// Note that you can always skip this method and instead configure the options in the ConfigureServices method.
        /// It's more of a convenience method than necessary.
        /// </summary>
        /// <param name="options">The MVC options object.</param>
        /// <param name="configuration">The configuration</param>
        /// <param name="environment">The host environment.</param>
        /// <returns>The MVC options object.</returns>
        public MvcOptions? Options(MvcOptions? options, IConfiguration? configuration, IHostEnvironment? environment) => options;
    }
}