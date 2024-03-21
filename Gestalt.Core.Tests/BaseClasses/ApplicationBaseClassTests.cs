namespace Gestalt.Core.Tests.BaseClasses
{
    using Gestalt.Core.BaseClasses;
    using Gestalt.Core.Interfaces;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using System;
    using System.Reflection;
    using Xunit;

    public class ApplicationBaseClassTests : TestBaseClass
    {
        public ApplicationBaseClassTests()
        {
            _configuration = new ConfigurationBuilder().Build();
            _env = Substitute.For<IHostEnvironment>();
            _assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };
            _testClass = new TestApplicationBaseClass(_configuration, _env, _assemblies);
        }

        private Assembly[] _assemblies;

        private IConfiguration _configuration;
        private IHostEnvironment _env;

        private TestApplicationBaseClass _testClass;

        protected override Type ObjectType { get; set; } = typeof(TestApplicationBaseClass);

        [Fact]
        public void CanCallConfigureConfigurationSettings()
        {
            // Arrange
            var Configuration = Substitute.For<IConfigurationBuilder>();
            var Args = new[] { "TestValue1452827348", "TestValue1719450934", "TestValue1098954403" };

            // Act
            var Result = _testClass.ConfigureConfigurationSettings(Configuration, Args);

            // Assert
            Assert.Same(Configuration, Result);
        }

        [Fact]
        public void CanCallConfigureConfigurationSettingsWithNullArgs()
        {
            _testClass.ConfigureConfigurationSettings(Substitute.For<IConfigurationBuilder>(), default(string[]));
        }

        [Fact]
        public void CanCallConfigureFrameworks()
        {
            // Arrange
            var Services = Substitute.For<IServiceCollection>();

            // Act
            var Result = _testClass.ConfigureFrameworks(Services);

            // Assert
            Assert.Same(Services, Result);
        }

        [Fact]
        public void CanCallConfigureFrameworksWithNullServices()
        {
            _testClass.ConfigureFrameworks(default);
        }

        [Fact]
        public void CanCallConfigureHostSettings()
        {
            // Arrange
            var Host = Substitute.For<IHostBuilder>();

            // Act
            var Result = _testClass.ConfigureHostSettings(Host);

            // Assert
            Assert.Same(Host, Result);
        }

        [Fact]
        public void CanCallConfigureHostSettingsWithNullHost()
        {
            _testClass.ConfigureHostSettings(default);
        }

        [Fact]
        public void CanCallConfigureLoggingSettings()
        {
            // Arrange
            var Logging = Substitute.For<ILoggingBuilder>();

            // Act
            var Result = _testClass.ConfigureLoggingSettings(Logging);

            // Assert
            Assert.Same(Logging, Result);
        }

        [Fact]
        public void CanCallConfigureLoggingSettingsWithNullLogging()
        {
            _testClass.ConfigureLoggingSettings(default);
        }

        [Fact]
        public void CanCallConfigureServices()
        {
            // Arrange
            var Services = Substitute.For<IServiceCollection>();

            // Act
            var Result = _testClass.ConfigureServices(Services);

            // Assert
            Assert.Same(Services, Result);
        }

        [Fact]
        public void CanCallConfigureServicesWithNullServices()
        {
            _testClass.ConfigureServices(default);
        }

        [Fact]
        public void CanCallOnStarted()
        {
            // Act
            _testClass.OnStarted();
        }

        [Fact]
        public void CanCallOnStopped()
        {
            // Act
            _testClass.OnStopped();
        }

        [Fact]
        public void CanCallOnStopping()
        {
            // Act
            _testClass.OnStopping();
        }

        [Fact]
        public void CanGetName()
        {
            // Assert
            Assert.IsType<string>(_testClass.Name);
            Assert.Equal("testhost", _testClass.Name);
        }

        private class TestApplicationBaseClass : ApplicationBaseClass
        {
            public TestApplicationBaseClass(IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies) : base(configuration, env, assemblies)
            {
            }

            public IConfiguration PublicConfiguration => base.Configuration;

            public IHostEnvironment PublicEnvironment => base.Environment;

            public IApplicationFramework[] PublicFrameworks => base.Frameworks;

            public ILogger PublicInternalLogger => base.InternalLogger;

            public IApplicationModule[] PublicModules => base.Modules;
        }
    }
}