namespace Gestalt.Console.Tests.Modules
{
    using Gestalt.Console.Modules;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class InternalModuleTests : TestBaseClass<InternalModule>
    {
        public InternalModuleTests()
        {
            _TestClass = new InternalModule();
        }

        private readonly InternalModule _TestClass;

        [Fact]
        public void CanCallConfigureServices()
        {
            // Arrange
            var Services = new ServiceCollection();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureServices(Services, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Single(Result);
            Assert.Same(Services, Result);
        }
    }
}