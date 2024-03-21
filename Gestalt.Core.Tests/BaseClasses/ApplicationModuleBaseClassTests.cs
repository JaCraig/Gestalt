namespace Gestalt.Core.Tests.BaseClasses
{
    using Canister.Interfaces;
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

    public class ApplicationModuleBaseClass_1Tests : TestBaseClass
    {
        public ApplicationModuleBaseClass_1Tests()
        {
            _Name = "TestValue1960118988";
            _Category = "TestValue1860675215";
            _ContentPath = "TestValue192350694";
            _Tags = new[] { "TestValue443821728", "TestValue1316046464", "TestValue1292267405" };
            _TestClass = new TestApplicationModuleBaseClass(_Name, _Category, _ContentPath, _Tags);
        }

        private readonly string _Category;
        private readonly string _ContentPath;
        private readonly string _Name;
        private readonly string[] _Tags;
        private readonly TestApplicationModuleBaseClass _TestClass;
        protected override Type ObjectType { get; set; } = typeof(TestApplicationModuleBaseClass);

        [Fact]
        public void CanCallConfigureConfigurationSettings()
        {
            // Arrange
            var Configuration = Substitute.For<IConfigurationBuilder>();
            var Environment = Substitute.For<IHostEnvironment>();
            var Args = new[] { "TestValue2115566880", "TestValue240623593", "TestValue578315628" };

            // Act
            var Result = _TestClass.ConfigureConfigurationSettings(Configuration, Environment, Args);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Configuration, Result);
        }

        [Fact]
        public void CanCallConfigureConfigurationSettingsWithNullArgs()
        {
            _TestClass.ConfigureConfigurationSettings(Substitute.For<IConfigurationBuilder>(), Substitute.For<IHostEnvironment>(), default(string[]));
        }

        [Fact]
        public void CanCallConfigureHostSettings()
        {
            // Arrange
            var Host = Substitute.For<IHostBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureHostSettings(Host, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Host, Result);
        }

        [Fact]
        public void CanCallConfigureLoggingSettings()
        {
            // Arrange
            var Logging = Substitute.For<ILoggingBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureLoggingSettings(Logging, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Logging, Result);
        }

        [Fact]
        public void CanCallConfigureMetrics()
        {
            // Arrange
            var Metrics = Substitute.For<IMetricsBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureMetrics(Metrics, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Metrics, Result);
        }

        [Fact]
        public void CanCallConfigureMetricsWithNullMetrics()
        {
            var Result = _TestClass.ConfigureMetrics(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallConfigureServices()
        {
            // Arrange
            var Services = Substitute.For<IServiceCollection>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureServices(Services, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Services, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Class1 = new TestApplicationModuleBaseClass();
            var Class2 = new TestApplicationModuleBaseClass();

            // Act
            var Result = Class1 == Class2;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullClass1()
        {
            var Result = default(TestApplicationModuleBaseClass) == new TestApplicationModuleBaseClass();
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullClass2()
        {
            var Result = new TestApplicationModuleBaseClass() == default(TestApplicationModuleBaseClass);
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
            var Other = new TestApplicationModuleBaseClass2();

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Class1 = new TestApplicationModuleBaseClass();
            var Class2 = new TestApplicationModuleBaseClass();

            // Act
            var Result = Class1 != Class2;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullClass1()
        {
            var Result = default(TestApplicationModuleBaseClass) != new TestApplicationModuleBaseClass();
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullClass2()
        {
            var Result = new TestApplicationModuleBaseClass() != default(TestApplicationModuleBaseClass);
            Assert.True(Result);
        }

        [Fact]
        public void CanCallOnStarted()
        {
            // Act
            _TestClass.OnStarted();
        }

        [Fact]
        public void CanCallOnStopped()
        {
            // Act
            _TestClass.OnStopped();
        }

        [Fact]
        public void CanCallOnStopping()
        {
            // Act
            _TestClass.OnStopping();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestApplicationModuleBaseClass(_Name, _Category, _ContentPath, _Tags);

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new TestApplicationModuleBaseClass();

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidCategory(string value)
        {
            _ = new TestApplicationModuleBaseClass(_Name, value, _ContentPath, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContentPath(string value)
        {
            _ = new TestApplicationModuleBaseClass(_Name, _Category, value, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value)
        {
            _ = new TestApplicationModuleBaseClass(value, _Category, _ContentPath, _Tags);
        }

        [Fact]
        public void CanConstructWithNullTags()
        {
            _ = new TestApplicationModuleBaseClass(_Name, _Category, _ContentPath, default);
        }

        [Fact]
        public void CanGetCategory()
        {
            // Arrange
            const string TestValue = "TestValue1860675215";

            // Assert
            Assert.Equal(TestValue, _TestClass.Category);
        }

        [Fact]
        public void CanGetContentPath()
        {
            // Arrange
            const string TestValue = "TestValue192350694";

            // Assert
            Assert.Equal(TestValue, _TestClass.ContentPath);
        }

        [Fact]
        public void CanGetID()
        {
            // Arrange
            const string TestValue = "Gestalt.Core.Tests.BaseClasses.ApplicationModuleBaseClass_1Tests.TestApplicationModuleBaseClass";

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
            const string TestValue = "TestValue1960118988";

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
        public void CanGetTags()
        {
            // Arrange
            var TestValue = new[] { "TestValue443821728", "TestValue1316046464", "TestValue1292267405" };

            // Assert
            Assert.Equal(TestValue, _TestClass.Tags);
        }

        [Fact]
        public void CanGetVersion()
        {
            // Assert
            Assert.IsType<string>(_TestClass.Version);
        }

        [Fact]
        public void CategoryIsInitializedCorrectly()
        {
            Assert.Equal(_Category, _TestClass.Category);
        }

        [Fact]
        public void ContentPathIsInitializedCorrectly()
        {
            Assert.Equal(_ContentPath, _TestClass.ContentPath);
        }

        [Fact]
        public void ImplementsIEquatable_TModule()
        {
            // Arrange
            var Same = new TestApplicationModuleBaseClass();
            var Different = new TestApplicationModuleBaseClass2();

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
            Assert.False(_TestClass == (IApplicationModule)Different);
            Assert.False(_TestClass != Same);
            Assert.True(_TestClass != (IApplicationModule)Different);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            Assert.Equal(_Name, _TestClass.Name);
        }

        [Fact]
        public void TagsIsInitializedCorrectly()
        {
            Assert.Same(_Tags, _TestClass.Tags);
        }

        private class TestApplicationModuleBaseClass : ApplicationModuleBaseClass<TestApplicationModuleBaseClass>
        {
            public TestApplicationModuleBaseClass(string? name, string? category, string? contentPath, params string[] tags) : base(name, category, contentPath, tags)
            {
            }

            public TestApplicationModuleBaseClass()
            {
            }
        }

        private class TestApplicationModuleBaseClass2 : ApplicationModuleBaseClass<TestApplicationModuleBaseClass2>
        {
            public TestApplicationModuleBaseClass2(string? name, string? category, string? contentPath, params string[] tags) : base(name, category, contentPath, tags)
            {
            }

            public TestApplicationModuleBaseClass2()
            {
            }
        }
    }
}