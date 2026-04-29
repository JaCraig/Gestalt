namespace Gestalt.ASPNet.Controllers.Tests.Integration
{
    using Gestalt.ASPNet.Controllers.BaseClasses;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Xunit;

    /// <summary>
    /// Integration tests for Controllers framework configuration with modules.
    /// </summary>
    public class ControllerFrameworkIntegrationTests
    {
        [Fact]
        public void ControllerModule_RegistersMvcServices()
        {
            // Arrange
            var Services = new ServiceCollection();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();

            var Modules = new[] { new TestControllerModule() };
            var Framework = new ControllerFrameworkModule();

            // Act
            _ = Framework.Configure(Modules, Services, Configuration, Environment);

            // Assert - MVC services should be registered
            Assert.NotEmpty(Services);
            Assert.Contains(Services, sd => sd.ServiceType == typeof(IControllerFactory));
        }

        public class ControllerFrameworkModule : ControllerFramework
        {
        }

        public class TestControllerModule : ControllerModuleBaseClass<TestControllerModule>
        {
        }

        private class MockHostEnvironment : IHostEnvironment
        {
            public string ApplicationName { get; set; } = "TestApp";
            public Microsoft.Extensions.FileProviders.IFileProvider ContentRootFileProvider { get; set; } = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Directory.GetCurrentDirectory());
            public string ContentRootPath { get; set; } = System.IO.Directory.GetCurrentDirectory();
            public string EnvironmentName { get; set; } = "Test";
        }
    }
}