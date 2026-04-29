namespace Gestalt.Core.Tests.ExtensionMethods
{
    using Gestalt.Core.BaseClasses;
    using Gestalt.Core.ExtensionMethods;
    using Gestalt.Core.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Reflection;
    using Xunit;

    /// <summary>
    /// Integration tests for module and framework discovery via reflection.
    /// </summary>
    public class AssemblyExtensionsIntegrationTests
    {
        [Fact]
        public void FindModules_DiscoversAllImplementationsInAssembly()
        {
            // Arrange
            var Assemblies = new[] { typeof(TestModule1).Assembly };

            // Act
            var Modules = Assemblies.FindModules();

            // Assert
            Assert.NotEmpty(Modules);
            Assert.Contains(Modules, m => m is TestModule1);
            Assert.Contains(Modules, m => m is TestModule2);
        }

        [Fact]
        public void FindModules_DiscoversConcreteImplementations()
        {
            // Arrange
            var Assemblies = new[] { typeof(ConcreteTestModule).Assembly };

            // Act
            var Modules = Assemblies.FindModules();

            // Assert
            Assert.Contains(Modules, m => m is ConcreteTestModule);
        }

        [Fact]
        public void FindModules_OrdersByModuleOrder()
        {
            // Arrange
            var Assemblies = new[] { typeof(TestModule1).Assembly };

            // Act
            var Modules = Assemblies.FindModules();

            // Assert
            var Module1Index = Array.FindIndex(Modules, m => m is TestModule1);
            var Module2Index = Array.FindIndex(Modules, m => m is TestModule2);
            Assert.True(Module1Index < Module2Index, "Modules should be ordered by Order property");
        }

        [Fact]
        public void FindModules_HandlesNullAssemblyArray()
        {
            // Act
            var Result = ((Assembly?[])null).FindModules();

            // Assert
            Assert.Empty(Result);
        }

        [Fact]
        public void FindModules_HandlesEmptyAssemblyArray()
        {
            // Arrange
            var Assemblies = Array.Empty<Assembly>();

            // Act
            var Result = Assemblies.FindModules();

            // Assert
            Assert.Empty(Result);
        }

        [Fact]
        public void FindFrameworks_DiscoversAllImplementationsInAssembly()
        {
            // Arrange
            var Assemblies = new[] { typeof(TestFramework1).Assembly };

            // Act
            var Frameworks = Assemblies.FindFrameworks();

            // Assert
            Assert.NotEmpty(Frameworks);
            Assert.Contains(Frameworks, f => f is TestFramework1);
        }

        [Fact]
        public void FindFrameworks_OrdersByFrameworkOrder()
        {
            // Arrange
            var Assemblies = new[] { typeof(TestFramework1).Assembly };

            // Act
            var Frameworks = Assemblies.FindFrameworks();

            // Assert
            Assert.NotEmpty(Frameworks);
            var FirstFramework = Frameworks[0];
            Assert.IsType<TestFramework1>(FirstFramework);
        }

        [Fact]
        public void FindFrameworks_HandlesNullAssemblyArray()
        {
            // Act
            var Result = ((Assembly?[])null).FindFrameworks();

            // Assert
            Assert.Empty(Result);
        }

        public class TestModule1 : ApplicationModuleBaseClass<TestModule1>
        {
            public override int Order => 100;
        }

        public class TestModule2 : ApplicationModuleBaseClass<TestModule2>
        {
            public override int Order => 200;
        }

        public class ConcreteTestModule : ApplicationModuleBaseClass<ConcreteTestModule>
        {
        }

        public class TestFramework1 : ApplicationFrameworkBaseClass<TestFramework1, IApplicationModule>
        {
            protected override void ConfigureModules(IApplicationModule[] modules, IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
            {
            }
        }
    }
}