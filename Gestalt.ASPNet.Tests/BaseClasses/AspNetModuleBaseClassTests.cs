namespace Gestalt.ASPNet.Tests.BaseClasses
{
    using Gestalt.ASPNet.BaseClasses;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class AspNetModuleBaseClass_1Tests
    {
        public AspNetModuleBaseClass_1Tests()
        {
            _Name = "TestValue703122731";
            _Category = "TestValue2106696766";
            _ContentPath = "TestValue1285730747";
            _Tags = new[] { "TestValue1636223399", "TestValue1241604349", "TestValue1882672904" };
            _TestClass = new TestAspNetModuleBaseClass(_Name, _Category, _ContentPath, _Tags);
        }

        private readonly string _Category;

        private readonly string _ContentPath;

        private readonly string _Name;

        private readonly string[] _Tags;

        private readonly TestAspNetModuleBaseClass _TestClass;

        [Fact]
        public void CanCallConfigureApplication()
        {
            // Arrange
            var ApplicationBuilder = Substitute.For<IApplicationBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureApplication(ApplicationBuilder, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallConfigureRoutes()
        {
            // Arrange
            var Endpoints = Substitute.For<IEndpointRouteBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureRoutes(Endpoints, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Endpoints, Result);
        }

        [Fact]
        public void CanCallConfigureRoutesWithNullEndpoints()
        {
            _TestClass.ConfigureRoutes(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.ConfigureRoutes(Substitute.For<IEndpointRouteBuilder>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.ConfigureRoutes(Substitute.For<IEndpointRouteBuilder>(), Substitute.For<IConfiguration>(), default);
            _TestClass.ConfigureRoutes(default, default, default);
        }

        [Fact]
        public void CanCallConfigureWebHostSettings()
        {
            // Arrange
            var WebHost = Substitute.For<IWebHostBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureWebHostSettings(WebHost, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(WebHost, Result);
        }

        [Fact]
        public void CanCallConfigureWebHostSettingsWithNullWebHost()
        {
            _TestClass.ConfigureWebHostSettings(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.ConfigureWebHostSettings(Substitute.For<IWebHostBuilder>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.ConfigureWebHostSettings(Substitute.For<IWebHostBuilder>(), Substitute.For<IConfiguration>(), default);
            _TestClass.ConfigureWebHostSettings(default, default, default);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestAspNetModuleBaseClass(_Name, _Category, _ContentPath, _Tags);

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal(_Name, Instance.Name);
            Assert.Equal(_Category, Instance.Category);
            Assert.Equal(_ContentPath, Instance.ContentPath);
            Assert.Equal(_Tags, Instance.Tags);

            // Act
            Instance = new TestAspNetModuleBaseClass();

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal("AspNetModuleBaseClass_1Tests.TestAspNetModuleBaseClass", Instance.Name);
            Assert.Equal("ASPNet", Instance.Category);
            Assert.Equal("wwwroot/Content/ASPNet/AspNetModuleBaseClass_1Tests.TestAspNetModuleBaseClass", Instance.ContentPath);
            Assert.Empty(Instance.Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidCategory(string value)
        {
            _ = new TestAspNetModuleBaseClass(_Name, value, _ContentPath, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContentPath(string value)
        {
            _ = new TestAspNetModuleBaseClass(_Name, _Category, value, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value)
        {
            _ = new TestAspNetModuleBaseClass(value, _Category, _ContentPath, _Tags);
        }

        [Fact]
        public void CanConstructWithNullTags()
        {
            _ = new TestAspNetModuleBaseClass(_Name, _Category, _ContentPath, default);
        }

        private class TestAspNetModuleBaseClass : AspNetModuleBaseClass<TestAspNetModuleBaseClass>
        {
            public TestAspNetModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags) : base(name, category, contentPath, tags)
            {
            }

            public TestAspNetModuleBaseClass() : base()
            {
            }
        }
    }
}