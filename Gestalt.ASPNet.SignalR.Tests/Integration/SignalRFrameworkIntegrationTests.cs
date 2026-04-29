namespace Gestalt.ASPNet.SignalR.Tests.Integration
{
    using Gestalt.ASPNet.SignalR.BaseClasses;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Integration tests for SignalR framework configuration with modules.
    /// </summary>
    public class SignalRFrameworkIntegrationTests
    {
        [Fact]
        public void SignalRModule_RegistersSignalRServices()
        {
            // Arrange
            var Services = new ServiceCollection();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();

            var Modules = new[] { new TestSignalRModule() };
            var Framework = new SignalRFrameworkModule();

            // Act
            _ = Framework.Configure(Modules, Services, Configuration, Environment);

            // Assert - SignalR services should be registered
            Assert.NotEmpty(Services);
            Assert.Contains(Services, sd => sd.ServiceType.Name.Contains("SignalRServerBuilder") || sd.ServiceType.Name.Contains("HubProtocolResolver"));
        }

        public class TestSignalRModule : SignalRModuleBaseClass<TestSignalRModule>
        {
        }

        public class SignalRFrameworkModule : SignalRFramework
        {
        }

        private class MockHostEnvironment : IHostEnvironment
        {
            public string EnvironmentName { get; set; } = "Test";
            public string ApplicationName { get; set; } = "TestApp";
            public string ContentRootPath { get; set; } = System.IO.Directory.GetCurrentDirectory();
            public Microsoft.Extensions.FileProviders.IFileProvider ContentRootFileProvider { get; set; } = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Directory.GetCurrentDirectory());
        }
    }
}