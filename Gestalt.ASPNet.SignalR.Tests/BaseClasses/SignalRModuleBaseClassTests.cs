namespace Gestalt.ASPNet.SignalR.Tests.BaseClasses
{
    using Gestalt.ASPNet.SignalR.BaseClasses;
    using Gestalt.Tests.Helpers;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class SignalRModuleBaseClass_1Tests : TestBaseClass
    {
        public SignalRModuleBaseClass_1Tests()
        {
            _Name = "TestValue1335960113";
            _Category = "TestValue966733231";
            _ContentPath = "TestValue468335660";
            _Tags = new[] { "TestValue740309588", "TestValue1836641462", "TestValue2073426856" };
            _TestClass = new TestSignalRModuleBaseClass(_Name, _Category, _ContentPath, _Tags);
        }

        private readonly string _Category;

        private readonly string _ContentPath;

        private readonly string _Name;

        private readonly string[] _Tags;

        private readonly TestSignalRModuleBaseClass _TestClass;

        protected override Type? ObjectType { get; set; } = typeof(TestSignalRModuleBaseClass);

        [Fact]
        public void CanCallConfigureSignalR()
        {
            // Arrange
            var SignalRBuilder = Substitute.For<ISignalRServerBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureSignalR(SignalRBuilder, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(SignalRBuilder, Result);
        }

        [Fact]
        public void CanCallConfigureSignalRWithNullValues()
        {
            _ = _TestClass.ConfigureSignalR(Substitute.For<ISignalRServerBuilder>(), default, Substitute.For<IHostEnvironment>());
            _ = _TestClass.ConfigureSignalR(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _ = _TestClass.ConfigureSignalR(Substitute.For<ISignalRServerBuilder>(), Substitute.For<IConfiguration>(), default);
            _ = _TestClass.ConfigureSignalR(default, default, default);
        }

        [Fact]
        public void CanCallOptions()
        {
            // Arrange
            var Options = Substitute.For<HubOptions>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.Options(Options, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Options, Result);
        }

        [Fact]
        public void CanCallOptionsWithNullValues()
        {
            _TestClass.Options(Substitute.For<HubOptions>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.Options(Substitute.For<HubOptions>(), Substitute.For<IConfiguration>(), default);
            _TestClass.Options(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Options(default, default, default);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestSignalRModuleBaseClass(_Name, _Category, _ContentPath, _Tags);

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal(_Name, Instance.Name);
            Assert.Equal(_Category, Instance.Category);
            Assert.Equal(_ContentPath, Instance.ContentPath);
            Assert.Equal(_Tags, Instance.Tags);

            // Act
            Instance = new TestSignalRModuleBaseClass();

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal("SignalRModuleBaseClass_1Tests.TestSignalRModuleBaseClass", Instance.Name);
            Assert.Equal("ASPNet", Instance.Category);
            Assert.Equal("wwwroot/Content/ASPNet/SignalRModuleBaseClass_1Tests.TestSignalRModuleBaseClass", Instance.ContentPath);
            Assert.NotNull(Instance.Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidCategory(string value)
        {
            _ = new TestSignalRModuleBaseClass(_Name, value, _ContentPath, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContentPath(string value)
        {
            _ = new TestSignalRModuleBaseClass(_Name, _Category, value, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value)
        {
            _ = new TestSignalRModuleBaseClass(value, _Category, _ContentPath, _Tags);
        }

        [Fact]
        public void CanConstructWithNullTags()
        {
            _ = new TestSignalRModuleBaseClass(_Name, _Category, _ContentPath, default);
        }

        private class TestSignalRModuleBaseClass : SignalRModuleBaseClass<TestSignalRModuleBaseClass>
        {
            public TestSignalRModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags) : base(name, category, contentPath, tags)
            {
            }

            public TestSignalRModuleBaseClass() : base()
            {
            }
        }
    }
}