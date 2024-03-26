namespace Gestalt.ASPNet.MVC.Tests
{
    using Gestalt.ASPNet.MVC;
    using Gestalt.ASPNet.MVC.BaseClasses;
    using Gestalt.ASPNet.MVC.Interfaces;
    using Gestalt.Core.Interfaces;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class MvcFrameworkTests : TestBaseClass<MvcFramework>
    {
        public MvcFrameworkTests()
        {
            _TestClass = new MvcFramework();
        }

        private readonly MvcFramework _TestClass;

        [Fact]
        public void CanCallConfigureModules()
        {
            // Arrange
            var Modules = new[] { Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>() };
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
            _TestClass.Configure(default(IMvcModule[]), Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>() }, default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>() }, Substitute.For<IServiceCollection>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>(), Substitute.For<IMvcModule>() }, Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), default);
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
            var Instance = new MvcFramework();

            // Assert
            Assert.NotNull(Instance);
        }

        public class TestModule : MvcModuleBaseClass<TestModule>
        {
        }
    }
}