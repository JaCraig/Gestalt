namespace Gestalt.Core.Tests.BaseClasses
{
    using Gestalt.Core.BaseClasses;
    using Gestalt.Core.Interfaces;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.Metrics;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using System;
    using Xunit;

    public class ApplicationFrameworkBaseClass_2Tests : TestBaseClass
    {
        public ApplicationFrameworkBaseClass_2Tests()
        {
            _Name = "TestValue556958240";
            _TestClass = new TestApplicationFrameworkBaseClass(_Name);
        }

        private readonly string _Name;

        private readonly TestApplicationFrameworkBaseClass _TestClass;

        protected override Type ObjectType { get; set; } = typeof(TestApplicationFrameworkBaseClass);

        [Fact]
        public void CanCallConfigure()
        {
            // Arrange
            var Modules = new[] { Substitute.For<IApplicationModule>(), Substitute.For<IApplicationModule>(), Substitute.For<IApplicationModule>() };
            var Services = Substitute.For<IServiceCollection>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.Configure(Modules, Services, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Services, Result);
        }

        [Fact]
        public void CanCallConfigureWithNullModules()
        {
            _TestClass.Configure(default(IApplicationModule[]), Substitute.For<IServiceCollection>(), Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Configure(null, null, null, null);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Class1 = new TestApplicationFrameworkBaseClass();
            var Class2 = new TestApplicationFrameworkBaseClass();

            // Act
            var Result = Class1 == Class2;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullClass1()
        {
            var Result = new TestApplicationFrameworkBaseClass() == default;
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullClass2()
        {
            var Result = null == new TestApplicationFrameworkBaseClass();
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithObj()
        {
            // Arrange
            var Obj = new object();

            // Act
            var Result = _TestClass.Equals(Obj);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithOther()
        {
            // Arrange
            var Other = new TestApplicationFrameworkBaseClass();

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            Assert.IsType<int>(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Class1 = new TestApplicationFrameworkBaseClass();
            var Class2 = new TestApplicationFrameworkBaseClass();

            // Act
            var Result = Class1 != Class2;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullClass1()
        {
            var Result = default != new TestApplicationFrameworkBaseClass();
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullClass2()
        {
            var Result = new TestApplicationFrameworkBaseClass() != default;
            Assert.True(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestApplicationFrameworkBaseClass(_Name);

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new TestApplicationFrameworkBaseClass();

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithCustomName(string value)
        {
            var Object = new TestApplicationFrameworkBaseClass(value);

            Assert.Equal(value, Object.Name);
        }

        [Fact]
        public void CanGetID()
        {
            // Arrange
            const string TestValue = "Gestalt.Core.Tests.BaseClasses.ApplicationFrameworkBaseClass_2Tests.TestApplicationFrameworkBaseClass";

            // Assert
            Assert.Equal(TestValue, _TestClass.ID);
        }

        [Fact]
        public void CanGetLastModified()
        {
            // Assert
            Assert.IsType<DateTime>(_TestClass.LastModified);

            Assert.NotEqual(default, _TestClass.LastModified);
        }

        [Fact]
        public void CanGetName()
        {
            // Arrange
            const string TestValue = "TestValue556958240";

            // Assert
            Assert.Equal(TestValue, _TestClass.Name);
        }

        [Fact]
        public void CanGetOrder()
        {
            // Arrange
            const int TestValue = 100;

            // Assert
            Assert.Equal(TestValue, _TestClass.Order);
        }

        [Fact]
        public void CanGetVersion()
        {
            // Assert
            Assert.IsType<string>(_TestClass.Version);
        }

        [Fact]
        public void ImplementsIEquatable_TApplicationFramework()
        {
            // Arrange
            var Same = new TestApplicationFrameworkBaseClass();
            var Different = new TestApplicationFrameworkBaseClass2("ABC");

            // Assert
            Assert.False(_TestClass.Equals(default(object)));
            Assert.False(_TestClass.Equals(new object()));
            Assert.True(_TestClass.Equals((object)Same));
            Assert.False(_TestClass.Equals((object)Different));
            Assert.True(_TestClass.Equals(Same));
            Assert.False(_TestClass.Equals(Different));
            Assert.Equal(Same.GetHashCode(), _TestClass.GetHashCode());
            Assert.NotEqual(Different.GetHashCode(), _TestClass.GetHashCode());
            Assert.True(_TestClass == Same);
            Assert.False(_TestClass == (IApplicationFramework)Different);
            Assert.False(_TestClass != Same);
            Assert.True(_TestClass != (IApplicationFramework)Different);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            Assert.Equal(_Name, _TestClass.Name);
        }

        private class TestApplicationFrameworkBaseClass : ApplicationFrameworkBaseClass<TestApplicationFrameworkBaseClass, TestModule>
        {
            public TestApplicationFrameworkBaseClass(string? name) : base(name)
            {
            }

            public TestApplicationFrameworkBaseClass() : base()
            {
            }

            protected override void ConfigureModules(TestModule[] modules, IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
            {
            }
        }

        private class TestApplicationFrameworkBaseClass2 : ApplicationFrameworkBaseClass<TestApplicationFrameworkBaseClass2, TestModule>
        {
            public TestApplicationFrameworkBaseClass2(string? name) : base(name)
            {
            }

            public TestApplicationFrameworkBaseClass2() : base()
            {
            }

            protected override void ConfigureModules(TestModule[] modules, IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
            {
            }
        }

        private class TestModule : IApplicationModule
        {
            public string Category { get; }
            public string ContentPath { get; }
            public string ID { get; set; }
            public DateTime LastModified { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
            public string[] Tags { get; }
            public string Version { get; set; }

            public IConfigurationBuilder ConfigureConfigurationSettings(IConfigurationBuilder configuration, IHostEnvironment environment, string[] args) => configuration;

            public IHostBuilder ConfigureHostSettings(IHostBuilder host, IConfiguration configuration, IHostEnvironment environment) => host;

            public ILoggingBuilder ConfigureLoggingSettings(ILoggingBuilder logging, IConfiguration configuration, IHostEnvironment environment) => logging;

            public IMetricsBuilder ConfigureMetrics(IMetricsBuilder metrics, IConfiguration configuration, IHostEnvironment environment) => metrics;

            public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment) => services;

            public void OnStarted()
            { }

            public void OnStopped()
            { }

            public void OnStopping()
            {
            }
        }
    }
}