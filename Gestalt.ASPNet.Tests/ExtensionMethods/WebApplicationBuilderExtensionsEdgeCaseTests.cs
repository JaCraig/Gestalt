using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Gestalt.ASPNet.Tests.ExtensionMethods
{
    using Gestalt.ASPNet.BaseClasses;
    using Gestalt.ASPNet.ExtensionMethods;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using Xunit;

    /// <summary>
    /// Extended tests for WebApplicationBuilder extensions covering edge cases and configuration scenarios.
    /// </summary>
    public class WebApplicationBuilderExtensionsEdgeCaseTests
    {
        [Fact]
        public void UseGestalt_BuildsApplicationSuccessfully()
        {
            // Arrange
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();

            // Act
            using var App = Builder.UseGestalt(null);

            // Assert
            Assert.NotNull(App);
            Assert.IsAssignableFrom<WebApplication>(App);
        }

        [Fact]
        public void UseGestalt_ConfiguresRoutingServices()
        {
            // Arrange
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();

            // Act
            using var App = Builder.UseGestalt(null);

            // Assert
            Assert.NotNull(App);
            Assert.NotNull(App.Services.GetService<IHost>());
        }

        [Fact]
        public async Task UseGestalt_ExecutesModuleConfigurationCallbacks()
        {
            // Arrange
            TrackingCallbackModule.Reset();
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();

            // Act
            await using var App = Builder.UseGestalt(null, typeof(TrackingCallbackModule).Assembly);
            await App.StartAsync();

            // Assert
            Assert.True(TrackingCallbackModule.ConfigureConfigurationCalled);
            Assert.True(TrackingCallbackModule.ConfigureServicesCalled);
            Assert.True(TrackingCallbackModule.ConfigureApplicationCalled);
            Assert.True(TrackingCallbackModule.ConfigureRoutesCalled);
            Assert.True(TrackingCallbackModule.StartedCalled);

            await App.StopAsync();
            Assert.True(TrackingCallbackModule.StoppedCalled);
        }

        [Fact]
        public async Task UseGestalt_ExecutesModuleRouteConfigurations()
        {
            // Arrange
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();

            // Act
            using var App = Builder.UseGestalt(null, typeof(RoutingModule).Assembly);
            await App.StartAsync();

            // Assert
            var Client = App.GetTestClient();
            var Response = await Client.GetStringAsync("/test");
            Assert.Equal("test", Response);

            await App.StopAsync();
        }

        [Fact]
        public void UseGestalt_WithAssembly_LoadsModulesFromAssembly()
        {
            // Arrange
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();
            var Assembly = typeof(RoutingModule).Assembly;

            // Act
            using var App = Builder.UseGestalt(null, Assembly);

            // Assert
            Assert.NotNull(App);
            Assert.NotNull(App.Services);
        }

        [Fact]
        public void UseGestalt_WithConfiguration_RegistersConfigurationValues()
        {
            // Arrange
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();
            Builder.Configuration["TestKey"] = "TestValue";

            // Act
            using var App = Builder.UseGestalt(null);

            // Assert
            var Config = App.Services.GetService<IConfiguration>();
            Assert.NotNull(Config);
            Assert.Equal("TestValue", Config?["TestKey"]);
        }

        [Fact]
        public void UseGestalt_WithEmptyArgs_BuildsSuccessfully()
        {
            // Arrange
            var Builder = WebApplication.CreateBuilder();
            Builder.WebHost.UseTestServer();
            var Args = Array.Empty<string>();

            // Act
            using var App = Builder.UseGestalt(Args);

            // Assert
            Assert.NotNull(App);
            Assert.NotNull(App.Services);
        }

        public class RoutingModule : AspNetModuleBaseClass<RoutingModule>
        {
            public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
            {
                endpoints?.MapGet("/test", () => "test");
                return endpoints;
            }
        }

        public class TrackingArgsModule : AspNetModuleBaseClass<TrackingArgsModule>
        {
            public static string?[] ReceivedArgs { get; private set; } = [];

            public static void Reset() => ReceivedArgs = [];

            public override IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string?[]? args)
            {
                ReceivedArgs = args ?? [];
                return configuration;
            }
        }

        public class TrackingCallbackModule : AspNetModuleBaseClass<TrackingCallbackModule>
        {
            public static bool ConfigureApplicationCalled { get; private set; }
            public static bool ConfigureConfigurationCalled { get; private set; }
            public static bool ConfigureRoutesCalled { get; private set; }
            public static bool ConfigureServicesCalled { get; private set; }
            public static bool StartedCalled { get; private set; }
            public static bool StoppedCalled { get; private set; }

            public static void Reset()
            {
                ConfigureApplicationCalled = false;
                ConfigureConfigurationCalled = false;
                ConfigureRoutesCalled = false;
                ConfigureServicesCalled = false;
                StartedCalled = false;
                StoppedCalled = false;
            }

            public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureApplicationCalled = true;
                return applicationBuilder;
            }

            public override IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string?[]? args)
            {
                ConfigureConfigurationCalled = true;
                return configuration;
            }

            public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureRoutesCalled = true;
                return endpoints;
            }

            public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureServicesCalled = true;
                return services;
            }

            public override void OnStarted() => StartedCalled = true;

            public override void OnStopped() => StoppedCalled = true;
        }
    }
}