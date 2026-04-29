namespace Gestalt.ASPNet.Tests.Integration
{
    using Gestalt.ASPNet.BaseClasses;
    using Gestalt.ASPNet.ExtensionMethods;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Xunit;

    public class AspNetIntegrationTests
    {
        [Fact]
        public async Task UseGestalt_ConfiguresPipelineAndRunsLifecycleCallbacks()
        {
            // Arrange
            TrackingAspNetModule.Reset();
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();
            var Args = new[] { "--integration-test=true" };

            // Act
            await using var App = Builder.UseGestalt(Args, typeof(TrackingAspNetModule).Assembly);

            // Assert
            Assert.NotNull(App);
            Assert.NotNull(App.Services.GetService<TrackingAspNetModule.MarkerService>());
            Assert.True(TrackingAspNetModule.ConfigureConfigurationCalled);
            Assert.True(TrackingAspNetModule.ConfigureServicesCalled);
            Assert.True(TrackingAspNetModule.ConfigureApplicationCalled);
            Assert.True(TrackingAspNetModule.ConfigureRoutesCalled);
            Assert.Equal(Args, TrackingAspNetModule.LastArgs);

            await App.StartAsync();

            var Client = App.GetTestClient();
            var Response = await Client.GetStringAsync("/integration/ping");
            Assert.Equal("pong", Response);
            Assert.True(TrackingAspNetModule.StartedCalled);

            await App.StopAsync();
            Assert.True(TrackingAspNetModule.StoppingCalled);
            Assert.True(TrackingAspNetModule.StoppedCalled);
        }

        public class TrackingAspNetModule : AspNetModuleBaseClass<TrackingAspNetModule>
        {
            public static bool ConfigureApplicationCalled { get; private set; }
            public static bool ConfigureConfigurationCalled { get; private set; }
            public static bool ConfigureRoutesCalled { get; private set; }
            public static bool ConfigureServicesCalled { get; private set; }
            public static string?[] LastArgs { get; private set; } = [];
            public static bool StartedCalled { get; private set; }
            public static bool StoppedCalled { get; private set; }
            public static bool StoppingCalled { get; private set; }

            public static void Reset()
            {
                ConfigureApplicationCalled = false;
                ConfigureConfigurationCalled = false;
                ConfigureRoutesCalled = false;
                ConfigureServicesCalled = false;
                LastArgs = [];
                StartedCalled = false;
                StoppedCalled = false;
                StoppingCalled = false;
            }

            public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureApplicationCalled = true;
                return applicationBuilder;
            }

            public override IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string?[]? args)
            {
                ConfigureConfigurationCalled = true;
                LastArgs = args ?? [];
                return configuration;
            }

            public override IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment) => host;

            public override ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment) => logging;

            public override IMetricsBuilder? ConfigureMetrics(IMetricsBuilder? metrics, IConfiguration? configuration, IHostEnvironment? environment) => metrics;

            public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureRoutesCalled = true;
                endpoints?.MapGet("/integration/ping", () => "pong");
                return endpoints;
            }

            public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureServicesCalled = true;
                return services?.AddSingleton<MarkerService>();
            }

            public override IWebHostBuilder? ConfigureWebHostSettings(IWebHostBuilder? webHost, IConfiguration? configuration, IHostEnvironment? environment) => webHost;

            public override void OnStarted() => StartedCalled = true;

            public override void OnStopped() => StoppedCalled = true;

            public override void OnStopping() => StoppingCalled = true;

            public class MarkerService
            {
            }
        }
    }
}