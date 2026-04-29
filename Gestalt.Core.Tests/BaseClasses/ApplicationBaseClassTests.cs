using Gestalt.Core.BaseClasses;
using Gestalt.Core.Interfaces;
using Gestalt.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Reflection;
using Xunit;

namespace Gestalt.Core.Tests.BaseClasses
{
    public class ApplicationBaseClassTests : TestBaseClass
    {
        public ApplicationBaseClassTests()
        {
            _Configuration = new ConfigurationBuilder().Build();
            _Env = Substitute.For<IHostEnvironment>();
            _Assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };
            _TestClass = new TestApplicationBaseClass(_Configuration, _Env, _Assemblies);
        }

        protected override Type? ObjectType { get; set; } = typeof(TestApplicationBaseClass);
        private readonly Assembly?[] _Assemblies;

        private readonly IConfiguration _Configuration;
        private readonly IHostEnvironment _Env;

        private readonly TestApplicationBaseClass _TestClass;

        [Fact]
        public void CanCallConfigureConfigurationSettings()
        {
            // Arrange
            IConfigurationBuilder Configuration = Substitute.For<IConfigurationBuilder>();
            var Args = new[] { "TestValue1452827348", "TestValue1719450934", "TestValue1098954403" };

            // Act
            IConfigurationBuilder? Result = _TestClass.ConfigureConfigurationSettings(Configuration, Args);

            // Assert
            Assert.Same(Configuration, Result);
        }

        [Fact]
        public void CanCallConfigureConfigurationSettingsWithNullArgs() => _TestClass.ConfigureConfigurationSettings(Substitute.For<IConfigurationBuilder>(), default);

        [Fact]
        public void CanCallConfigureFrameworks()
        {
            // Arrange
            IServiceCollection Services = Substitute.For<IServiceCollection>();

            // Act
            IServiceCollection? Result = _TestClass.ConfigureFrameworks(Services);

            // Assert
            Assert.Same(Services, Result);
        }

        [Fact]
        public void CanCallConfigureFrameworksWithNullServices() => _TestClass.ConfigureFrameworks(default);

        [Fact]
        public void CanCallConfigureHostSettings()
        {
            // Arrange
            IHostBuilder Host = Substitute.For<IHostBuilder>();

            // Act
            IHostBuilder? Result = _TestClass.ConfigureHostSettings(Host);

            // Assert
            Assert.Same(Host, Result);
        }

        [Fact]
        public void CanCallConfigureHostSettingsWithNullHost() => _TestClass.ConfigureHostSettings(default);

        [Fact]
        public void CanCallConfigureLoggingSettings()
        {
            // Arrange
            ILoggingBuilder Logging = Substitute.For<ILoggingBuilder>();

            // Act
            ILoggingBuilder? Result = _TestClass.ConfigureLoggingSettings(Logging);

            // Assert
            Assert.Same(Logging, Result);
        }

        [Fact]
        public void CanCallConfigureLoggingSettingsWithNullLogging() => _TestClass.ConfigureLoggingSettings(default);

        [Fact]
        public void CanCallConfigureServices()
        {
            // Arrange
            IServiceCollection Services = Substitute.For<IServiceCollection>();

            // Act
            IServiceCollection? Result = _TestClass.ConfigureServices(Services);

            // Assert
            Assert.Same(Services, Result);
        }

        [Fact]
        public void ConfigureServicesRegistersApplicationAndModuleServices()
        {
            // Arrange
            TrackingApplicationModule.Reset();
            var Environment = Substitute.For<IHostEnvironment>();
            var Configuration = new ConfigurationBuilder().Build();
            var Application = new TestApplicationBaseClass(Configuration, Environment, typeof(TrackingApplicationModule).Assembly);
            var Services = new ServiceCollection();

            // Act
            _ = Application.ConfigureServices(Services);

            // Assert
            using var ServiceProvider = Services.BuildServiceProvider();
            Assert.Same(Application, ServiceProvider.GetService<IApplication>());
            Assert.NotNull(ServiceProvider.GetService<TrackingApplicationModule>());
            Assert.NotNull(ServiceProvider.GetService<TrackingApplicationModule.MarkerService>());
            Assert.True(TrackingApplicationModule.ConfigureServicesCalled);
        }

        [Fact]
        public void CanCallConfigureServicesWithNullServices() => _TestClass.ConfigureServices(default);

        [Fact]
        public void CanCallOnStarted() =>
            // Act
            _TestClass.OnStarted();

        [Fact]
        public void CanCallOnStopped() =>
            // Act
            _TestClass.OnStopped();

        [Fact]
        public void CanCallOnStopping() =>
            // Act
            _TestClass.OnStopping();

        [Fact]
        public void CanGetName()
        {
            // Assert
            _ = Assert.IsType<string>(_TestClass.Name);
            Assert.Equal("testhost", _TestClass.Name);
        }

        private class TestApplicationBaseClass : ApplicationBaseClass
        {
            public TestApplicationBaseClass(IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies) : base(configuration, env, assemblies)
            {
            }

            public IConfiguration? PublicConfiguration => base.Configuration;

            public IHostEnvironment? PublicEnvironment => base.Environment;

            public IApplicationFramework[] PublicFrameworks => base.Frameworks;

            public ILogger? PublicInternalLogger => base.InternalLogger;

            public IApplicationModule[] PublicModules => base.Modules;
        }

        public class TrackingApplicationModule : ApplicationModuleBaseClass<TrackingApplicationModule>
        {
            public static bool ConfigureServicesCalled { get; private set; }

            public static void Reset() => ConfigureServicesCalled = false;

            public override IConfigurationBuilder? ConfigureConfigurationSettings(IConfigurationBuilder? configuration, IHostEnvironment? environment, string?[]? args) => configuration;

            public override IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment) => host;

            public override ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment) => logging;

            public override IMetricsBuilder? ConfigureMetrics(IMetricsBuilder? metrics, IConfiguration? configuration, IHostEnvironment? environment) => metrics;

            public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
            {
                ConfigureServicesCalled = true;
                return services?.AddSingleton<MarkerService>();
            }

            public class MarkerService
            {
            }
        }
    }
}