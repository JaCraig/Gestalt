namespace Gestalt.ASPNet.Controllers.Tests
{
    using Gestalt.ASPNet.Controllers;
    using Gestalt.ASPNet.Controllers.Interfaces;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class ControllerFrameworkTests : TestBaseClass<ControllerFramework>
    {
        public ControllerFrameworkTests()
        {
            _TestClass = new ControllerFramework();
        }

        private readonly ControllerFramework _TestClass;

        [Fact]
        public void CanCallConfigureModules()
        {
            // Arrange
            var Modules = new[] { Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>() };
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
            _TestClass.Configure(default(IControllerModule[]), Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new[] { Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>() }, default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new[] { Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>() }, Substitute.For<IServiceCollection>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new[] { Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>(), Substitute.For<IControllerModule>() }, Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), default);
            _TestClass.Configure(default, default, default, default);
        }
    }
}