namespace Gestalt.Console.Tests.Integration
{
    using Gestalt.Console.ExtensionMethods;
    using Gestalt.Core.BaseClasses;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Xunit;

    public class ConsoleIntegrationTests
    {
        [Fact]
        public async Task UseGestalt_BuildsHostAndRunsLifecycleCallbacks()
        {
            // Arrange
            TrackingConsoleModule.Reset();
            var Builder = new HostApplicationBuilder();
            var Args = new[] { "--integration-test=true" };

            // Act
            using var Host = Builder.UseGestalt(Args, typeof(TrackingConsoleModule).Assembly, typeof(InternalHostedService).Assembly);

            // Assert
            Assert.NotNull(Host);
            Assert.NotNull(Host.Services.GetService<TrackingConsoleModule.MarkerService>());
            Assert.True(TrackingConsoleModule.ConfigureConfigurationCalled);
            Assert.True(TrackingConsoleModule.ConfigureServicesCalled);

            await Host.StartAsync();
            Assert.True(TrackingConsoleModule.StartedCalled);

            await Host.StopAsync();
            Assert.True(TrackingConsoleModule.StoppedCalled);
        }

        public class TrackingConsoleModule : ApplicationModuleBaseClass<TrackingConsoleModule>
        {
            public static bool ConfigureConfigurationCalled { get; private set; }
            public static bool ConfigureServicesCalled { get; private set; }
            public static string?[] LastArgs { get; private set; } = [];
            public static bool StartedCalled { get; private set; }
            public static bool StoppedCalled { get; private set; }

            public static void Reset()
            {
                ConfigureConfigurationCalled = false;
                ConfigureServicesCalled = false;
                LastArgs = [];
                StartedCalled = false;
                StoppedCalled = false;
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

            public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureServicesCalled = true;
                return services?.AddSingleton<MarkerService>();
            }

            public override void OnStarted() => StartedCalled = true;

            public override void OnStopped() => StoppedCalled = true;

            public class MarkerService
            {
            }
        }
    }
}