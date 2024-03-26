namespace Gestalt.ASPNet.Tests
{
    using Gestalt.ASPNet;
    using Gestalt.Tests.Helpers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using System.Reflection;
    using Xunit;

    public class AspNetApplicationTests : TestBaseClass<AspNetApplication>
    {
        public AspNetApplicationTests()
        {
            _Configuration = Substitute.For<IConfiguration>();
            _Env = Substitute.For<IHostEnvironment>();
            _Assemblies = new[]
            {
                Assembly.GetAssembly(typeof(string)),
                Assembly.GetAssembly(typeof(string)),
                Assembly.GetAssembly(typeof(string))
            };
            _TestClass = new AspNetApplication(_Configuration, _Env, _Assemblies);
        }

        private readonly Assembly?[] _Assemblies;
        private readonly IConfiguration _Configuration;
        private readonly IHostEnvironment _Env;
        private readonly AspNetApplication _TestClass;

        [Fact]
        public void CanCallConfigureApplication()
        {
            // Arrange
            var Application = WebApplication.Create();

            // Act
            var Result = _TestClass.ConfigureApplication(Application);

            // Assert
            Assert.NotNull(Result);
            Assert.NotNull(Result.Services);
            Assert.NotNull(Result.Configuration);
            Assert.NotNull(Result.Environment);
        }

        [Fact]
        public void CanCallConfigureApplicationWithNullApplication()
        {
            _TestClass.ConfigureApplication(default);
        }

        [Fact]
        public void CanCallConfigureMetrics()
        {
            // Arrange
            var Metrics = Substitute.For<IMetricsBuilder>();

            // Act
            _TestClass.ConfigureMetrics(Metrics);
        }

        [Fact]
        public void CanCallConfigureMetricsWithNullMetrics()
        {
            _TestClass.ConfigureMetrics(default);
        }

        [Fact]
        public void CanCallConfigureWebHostSettings()
        {
            // Arrange
            var WebHost = Substitute.For<IWebHostBuilder>();

            // Act
            _TestClass.ConfigureWebHostSettings(WebHost);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new AspNetApplication(_Configuration, _Env, _Assemblies);

            // Assert
            Assert.NotNull(Instance);
        }
    }
}