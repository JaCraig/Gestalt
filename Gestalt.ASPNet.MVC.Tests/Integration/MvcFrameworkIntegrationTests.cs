namespace Gestalt.ASPNet.MVC.Tests.Integration
{
    using Gestalt.ASPNet.MVC.BaseClasses;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Integration tests for MVC framework configuration with modules.
    /// </summary>
    public class MvcFrameworkIntegrationTests
    {
        [Fact]
        public void MvcModule_RegistersMvcServices()
        {
            // Arrange
            var Services = new ServiceCollection();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();

            var Modules = new[] { new TestMvcModule() };
            var MvcFramework = new MvcFrameworkModule();

            // Act
            _ = MvcFramework.Configure(Modules, Services, Configuration, Environment);

            // Assert - MVC services should be registered
            Assert.NotEmpty(Services);
            var ControllerFactory = Services.FirstOrDefault(sd => sd.ServiceType == typeof(IControllerFactory));
            Assert.NotNull(ControllerFactory);
        }

        public class MvcFrameworkModule : MvcFramework
        {
        }

        public class TestMvcModule : MvcModuleBaseClass<TestMvcModule>
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