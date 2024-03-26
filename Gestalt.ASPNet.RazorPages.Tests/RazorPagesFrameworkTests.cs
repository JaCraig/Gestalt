namespace Gestalt.ASPNet.RazorPages.Tests
{
    using Gestalt.ASPNet.RazorPages;
    using Gestalt.ASPNet.RazorPages.BaseClasses;
    using Gestalt.ASPNet.RazorPages.Interfaces;
    using Gestalt.Core.Interfaces;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class RazorPagesFrameworkTests : TestBaseClass<RazorPagesFramework>
    {
        public RazorPagesFrameworkTests()
        {
            _TestClass = new RazorPagesFramework();
        }

        private readonly RazorPagesFramework _TestClass;

        [Fact]
        public void CanCallConfigureModules()
        {
            // Arrange
            var Modules = new[] { Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>() };
            var Services = Substitute.For<IServiceCollection>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.Configure(Modules, Services, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Services, Result);
        }

        [Fact]
        public void CanCallConfigureModulesWithNullValues()
        {
            _TestClass.Configure(default(IRazorPagesModule[]), Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>() }, default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>() }, Substitute.For<IServiceCollection>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>(), Substitute.For<IRazorPagesModule>() }, Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), default);
            _TestClass.Configure(default, default, default, default);
        }

        [Fact]
        public void CanCallWithModule()
        {
            // Arrange
            var Services = new ServiceCollection();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.Configure(new[] { new TestModule() }, Services, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Services, Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new RazorPagesFramework();

            // Assert
            Assert.NotNull(Instance);
        }

        public class TestModule : RazorPagesModuleBaseClass<TestModule>
        {
        }
    }
}