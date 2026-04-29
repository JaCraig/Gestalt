namespace Gestalt.Core.Tests
{
    using Gestalt.Core.BaseClasses;
    using Gestalt.Core.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Reflection;
    using Xunit;

    /// <summary>
    /// Tests for module ordering and lifecycle behavior.
    /// </summary>
    public class ModuleLifecycleTests
    {
        [Fact]
        public void ApplicationCallsAllModulesOnStarted()
        {
            // Arrange
            var Calls = new System.Collections.Generic.List<string>();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();
            var App = new ApplicationWithModuleTracking(Calls, Configuration, Environment,
                typeof(LifecycleModule1).Assembly, typeof(LifecycleModule2).Assembly);

            // Act
            App.OnStarted();

            // Assert
            Assert.Contains("Module1Started", Calls);
            Assert.Contains("Module2Started", Calls);
        }

        [Fact]
        public void ApplicationCallsAllModulesOnStopped()
        {
            // Arrange
            var Calls = new System.Collections.Generic.List<string>();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();
            var App = new ApplicationWithModuleTracking(Calls, Configuration, Environment,
                typeof(LifecycleModule1).Assembly);

            // Act
            App.OnStopped();

            // Assert
            Assert.Contains("Module1Stopped", Calls);
        }

        [Fact]
        public void ApplicationCallsModuleLifecycleMethodsInCorrectOrder()
        {
            // Arrange
            var CallOrder = new System.Collections.Generic.List<string>();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();
            var App = new TrackedApplication(CallOrder, Configuration, Environment, typeof(LifecycleModule).Assembly);

            // Act
            App.TrackStarted();
            App.TrackStopping();
            App.TrackStopped();

            // Assert
            Assert.Contains("ApplicationStarted", CallOrder);
            Assert.Contains("ApplicationStopping", CallOrder);
            Assert.Contains("ApplicationStopped", CallOrder);
            Assert.True(CallOrder.IndexOf("ApplicationStarted") < CallOrder.IndexOf("ApplicationStopping"));
            Assert.True(CallOrder.IndexOf("ApplicationStopping") < CallOrder.IndexOf("ApplicationStopped"));
        }

        [Fact]
        public void ConfigureServicesCallsAllModules()
        {
            // Arrange
            var Services = new ServiceCollection();
            var Configuration = new ConfigurationBuilder().Build();
            var Environment = new MockHostEnvironment();
            var App = new ApplicationWithServiceTracking(Configuration, Environment, typeof(ServiceModule).Assembly);

            // Act
            App.ConfigureServices(Services);

            // Assert
            Assert.Contains(Services, sd => sd.ServiceType == typeof(ServiceModule.TestService));
        }

        public class ApplicationWithModuleTracking : ApplicationBaseClass
        {
            public ApplicationWithModuleTracking(System.Collections.Generic.List<string> calls, IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies)
                : base(configuration, env, assemblies)
            {
                foreach (var Module in Modules)
                {
                    if (Module is LifecycleModule1 mod1)
                        mod1.Calls = calls;
                    else if (Module is LifecycleModule2 mod2)
                        mod2.Calls = calls;
                }
            }
        }

        public class ApplicationWithServiceTracking : ApplicationBaseClass
        {
            public ApplicationWithServiceTracking(IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies)
                : base(configuration, env, assemblies)
            {
            }
        }

        public class LifecycleModule : ApplicationModuleBaseClass<LifecycleModule>
        {
            public override void OnStarted() => System.Diagnostics.Debug.WriteLine("ModuleStarted");

            public override void OnStopped() => System.Diagnostics.Debug.WriteLine("ModuleStopped");

            public override void OnStopping() => System.Diagnostics.Debug.WriteLine("ModuleStopping");
        }

        public class LifecycleModule1 : ApplicationModuleBaseClass<LifecycleModule1>
        {
            public System.Collections.Generic.List<string>? Calls { get; set; }

            public override void OnStarted() => Calls?.Add("Module1Started");

            public override void OnStopped() => Calls?.Add("Module1Stopped");
        }

        public class LifecycleModule2 : ApplicationModuleBaseClass<LifecycleModule2>
        {
            public System.Collections.Generic.List<string>? Calls { get; set; }

            public override void OnStarted() => Calls?.Add("Module2Started");

            public override void OnStopped() => Calls?.Add("Module2Stopped");
        }

        public class MockHostEnvironment : IHostEnvironment
        {
            public string ApplicationName { get; set; } = "TestApp";

            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();

            public string ContentRootPath { get; set; } = System.IO.Directory.GetCurrentDirectory();

            public string EnvironmentName { get; set; } = "Test";

            private class NullChangeToken : IChangeToken
            {
                public bool ActiveChangeCallbacks => false;
                public bool HasChanged => false;

                public IDisposable RegisterChangeCallback(Action<object?> callback, object? state) => new NullDisposable();
            }

            private class NullDisposable : IDisposable
            {
                public void Dispose()
                { }
            }

            private class NullFileProvider : IFileProvider
            {
                public IDirectoryContents GetDirectoryContents(string subpath) => throw new NotImplementedException();

                public IFileInfo GetFileInfo(string subpath) => throw new NotImplementedException();

                public IChangeToken Watch(string filter) => new NullChangeToken();
            }
        }

        public class ServiceModule : ApplicationModuleBaseClass<ServiceModule>
        {
            public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
            {
                return services?.AddSingleton<TestService>();
            }

            public class TestService
            {
            }
        }

        public class TrackedApplication : ApplicationBaseClass
        {
            public TrackedApplication(System.Collections.Generic.List<string> callOrder, IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies)
                : base(configuration, env, assemblies)
            {
                _CallOrder = callOrder;
            }

            private readonly System.Collections.Generic.List<string> _CallOrder;

            public void TrackStarted()
            {
                _CallOrder.Add("ApplicationStarted");
                OnStarted();
            }

            public void TrackStopped()
            {
                _CallOrder.Add("ApplicationStopped");
                OnStopped();
            }

            public void TrackStopping()
            {
                _CallOrder.Add("ApplicationStopping");
                OnStopping();
            }
        }
    }
}