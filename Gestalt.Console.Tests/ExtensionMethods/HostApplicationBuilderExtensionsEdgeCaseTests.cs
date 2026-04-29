using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.Console.Tests.ExtensionMethods
{
    using Gestalt.Console.ExtensionMethods;
    using Gestalt.Core.BaseClasses;
    using Gestalt.Core.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using Xunit;

    /// <summary>
    /// Extended tests for HostApplicationBuilder extensions covering edge cases and configuration scenarios.
    /// </summary>
    public class HostApplicationBuilderExtensionsEdgeCaseTests
    {
        [Fact]
        public void UseGestalt_BuildsHostSuccessfully()
        {
            // Arrange
            var Builder = new HostApplicationBuilder();

            // Act
            using var Host = Builder.UseGestalt(null);

            // Assert
            Assert.NotNull(Host);
            Assert.IsAssignableFrom<IHost>(Host);
        }

        [Fact]
        public void UseGestalt_RegistersApplicationInstance()
        {
            // Arrange
            var Builder = new HostApplicationBuilder();

            // Act
            using var Host = Builder.UseGestalt(null);

            // Assert
            var App = Host.Services.GetService<IApplication>();
            Assert.NotNull(App);
        }

        [Fact]
        public void UseGestalt_WithAssembly_LoadsModulesFromAssembly()
        {
            // Arrange
            var Builder = new HostApplicationBuilder();
            var Assembly = typeof(TestModule).Assembly;

            // Act
            using var Host = Builder.UseGestalt(null, Assembly);

            // Assert
            Assert.NotNull(Host);
            var App = Host.Services.GetService<IApplication>();
            Assert.NotNull(App);
        }

        [Fact]
        public void UseGestalt_WithConfiguration_RegistersConfigurationValues()
        {
            // Arrange
            var Builder = new HostApplicationBuilder();
            Builder.Configuration["TestKey"] = "TestValue";

            // Act
            using var Host = Builder.UseGestalt(null);

            // Assert
            var Config = Host.Services.GetService<IConfiguration>();
            Assert.NotNull(Config);
            Assert.Equal("TestValue", Config?["TestKey"]);
        }

        [Fact]
        public void UseGestalt_WithEmptyArgs_BuildsSuccessfully()
        {
            // Arrange
            var Builder = new HostApplicationBuilder();
            var Args = Array.Empty<string>();

            // Act
            using var Host = Builder.UseGestalt(Args);

            // Assert
            Assert.NotNull(Host);
            Assert.NotNull(Host.Services);
        }

        public class TestModule : ApplicationModuleBaseClass<TestModule>
        {
        }
    }

    public class TrackingCallbackModule : ApplicationModuleBaseClass<TrackingCallbackModule>
    {
        public static bool ConfigureConfigurationCalled { get; private set; }
        public static bool ConfigureServicesCalled { get; private set; }
        public static bool StartedCalled { get; private set; }
        public static bool StoppedCalled { get; private set; }

        public static void Reset()
        {
            ConfigureConfigurationCalled = false;
            ConfigureServicesCalled = false;
            StartedCalled = false;
            StoppedCalled = false;
        }

        public override IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string?[]? args)
        {
            ConfigureConfigurationCalled = true;
            return configuration;
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