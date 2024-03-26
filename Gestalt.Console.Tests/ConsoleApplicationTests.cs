namespace Gestalt.Console.Tests
{
    using Gestalt.Console;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using System.Reflection;
    using Xunit;

    public class ConsoleApplicationTests : TestBaseClass<ConsoleApplication>
    {
        public ConsoleApplicationTests()
        {
            _Configuration = Substitute.For<IConfiguration>();
            _Env = Substitute.For<IHostEnvironment>();
            _Assemblies = new[]
            {
                Assembly.GetAssembly(typeof(string)),
                Assembly.GetAssembly(typeof(string)),
                Assembly.GetAssembly(typeof(string))
            };
            _TestClass = new ConsoleApplication(_Configuration, _Env, _Assemblies);
        }

        private readonly Assembly?[] _Assemblies;
        private readonly IConfiguration _Configuration;
        private readonly IHostEnvironment _Env;
        private readonly ConsoleApplication _TestClass;

        [Fact]
        public void CanCallConfigureMetrics()
        {
            // Arrange
            var MetricsBuilder = Substitute.For<IMetricsBuilder>();

            // Act
            _TestClass.ConfigureMetrics(MetricsBuilder);
        }

        [Fact]
        public void CanCallConfigureMetricsWithNullMetricsBuilder()
        {
            _TestClass.ConfigureMetrics(default);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new ConsoleApplication(_Configuration, _Env, _Assemblies);

            // Assert
            Assert.NotNull(Instance);
        }
    }
}