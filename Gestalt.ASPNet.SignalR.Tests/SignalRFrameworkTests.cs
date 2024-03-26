namespace Gestalt.ASPNet.SignalR.Tests
{
    using Gestalt.ASPNet.SignalR;
    using Gestalt.ASPNet.SignalR.Interfaces;
    using Gestalt.Core.Interfaces;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class SignalRFrameworkTests : TestBaseClass<SignalRFramework>
    {
        public SignalRFrameworkTests()
        {
            _TestClass = new SignalRFramework();
        }

        private readonly SignalRFramework _TestClass;

        [Fact]
        public void CanCallConfigureModules()
        {
            // Arrange
            var Modules = new[] { Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>() };
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
            _TestClass.Configure(default(ISignalRModule[]), Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>() }, default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>() }, Substitute.For<IServiceCollection>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.Configure(new IApplicationModule[] { Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>(), Substitute.For<ISignalRModule>() }, Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), default);
            _TestClass.Configure(default, default, default, default);
        }
    }
}