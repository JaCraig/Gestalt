using Gestalt.ASPNet.RazorPages.BaseClasses;
using Gestalt.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using System;
using Xunit;

namespace Gestalt.ASPNet.RazorPages.Tests.BaseClasses
{
    public class RazorPagesModuleBaseClass_1Tests : TestBaseClass
    {
        public RazorPagesModuleBaseClass_1Tests()
        {
            _Name = "TestValue1335960113";
            _Category = "TestValue966733231";
            _ContentPath = "TestValue468335660";
            _Tags = ["TestValue740309588", "TestValue1836641462", "TestValue2073426856"];
            _TestClass = new TestRazorPagesModuleBaseClass(_Name, _Category, _ContentPath, _Tags);
        }

        protected override Type? ObjectType { get; set; } = typeof(TestRazorPagesModuleBaseClass);
        private readonly string _Category;

        private readonly string _ContentPath;

        private readonly string _Name;

        private readonly string[] _Tags;

        private readonly TestRazorPagesModuleBaseClass _TestClass;

        [Fact]
        public void CanCallConfigureRazorPages()
        {
            // Arrange
            IMvcBuilder RazorPagesBuilder = Substitute.For<IMvcBuilder>();
            IConfiguration Configuration = Substitute.For<IConfiguration>();
            IHostEnvironment Environment = Substitute.For<IHostEnvironment>();

            // Act
            IMvcBuilder? Result = _TestClass.ConfigureRazorPages(RazorPagesBuilder, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(RazorPagesBuilder, Result);
        }

        [Fact]
        public void CanCallConfigureRazorPagesWithNullValues()
        {
            _ = _TestClass.ConfigureRazorPages(Substitute.For<IMvcBuilder>(), default, Substitute.For<IHostEnvironment>());
            _ = _TestClass.ConfigureRazorPages(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _ = _TestClass.ConfigureRazorPages(Substitute.For<IMvcBuilder>(), Substitute.For<IConfiguration>(), default);
            _ = _TestClass.ConfigureRazorPages(default, default, default);
        }

        [Fact]
        public void CanCallOptions()
        {
            // Arrange
            RazorPagesOptions Options = Substitute.For<RazorPagesOptions>();
            IConfiguration Configuration = Substitute.For<IConfiguration>();
            IHostEnvironment Environment = Substitute.For<IHostEnvironment>();

            // Act
            RazorPagesOptions? Result = _TestClass.Options(Options, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(Options, Result);
        }

        [Fact]
        public void CanCallOptionsWithNullValues()
        {
            _ = _TestClass.Options(Substitute.For<RazorPagesOptions>(), default, Substitute.For<IHostEnvironment>());
            _ = _TestClass.Options(Substitute.For<RazorPagesOptions>(), Substitute.For<IConfiguration>(), default);
            _ = _TestClass.Options(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _ = _TestClass.Options(default, default, default);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestRazorPagesModuleBaseClass(_Name, _Category, _ContentPath, _Tags);

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal(_Name, Instance.Name);
            Assert.Equal(_Category, Instance.Category);
            Assert.Equal(_ContentPath, Instance.ContentPath);
            Assert.Equal(_Tags, Instance.Tags);

            // Act
            Instance = new TestRazorPagesModuleBaseClass();

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal("RazorPagesModuleBaseClass_1Tests.TestRazorPagesModuleBaseClass", Instance.Name);
            Assert.Equal("ASPNet", Instance.Category);
            Assert.Equal("wwwroot/Content/ASPNet/RazorPagesModuleBaseClass_1Tests.TestRazorPagesModuleBaseClass", Instance.ContentPath);
            Assert.NotNull(Instance.Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidCategory(string value) => _ = new TestRazorPagesModuleBaseClass(_Name, value, _ContentPath, _Tags);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContentPath(string value) => _ = new TestRazorPagesModuleBaseClass(_Name, _Category, value, _Tags);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value) => _ = new TestRazorPagesModuleBaseClass(value, _Category, _ContentPath, _Tags);

        [Fact]
        public void CanConstructWithNullTags() => _ = new TestRazorPagesModuleBaseClass(_Name, _Category, _ContentPath, default);

        private class TestRazorPagesModuleBaseClass : RazorPagesModuleBaseClass<TestRazorPagesModuleBaseClass>
        {
            public TestRazorPagesModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags) : base(name, category, contentPath, tags)
            {
            }

            public TestRazorPagesModuleBaseClass() : base()
            {
            }
        }
    }
}