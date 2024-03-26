namespace Gestalt.ASPNet.Controllers.Tests.BaseClasses
{
    using Gestalt.ASPNet.Controllers.BaseClasses;
    using Gestalt.Tests.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class ControllerModuleBaseClass_1Tests : TestBaseClass
    {
        public ControllerModuleBaseClass_1Tests()
        {
            _Name = "TestValue320179691";
            _Category = "TestValue70116587";
            _ContentPath = "TestValue1307734067";
            _Tags = new[] { "TestValue308109472", "TestValue1224095980", "TestValue1680671033" };
            _TestClass = new TestControllerModuleBaseClass(_Name, _Category, _ContentPath, _Tags);
        }

        private readonly string _Category;

        private readonly string _ContentPath;

        private readonly string _Name;

        private readonly string[] _Tags;

        private readonly TestControllerModuleBaseClass _TestClass;

        protected override Type? ObjectType { get; set; } = typeof(TestControllerModuleBaseClass);

        [Fact]
        public void CanCallConfigureMVC()
        {
            // Arrange
            var MVCBuilder = Substitute.For<IMvcBuilder>();
            var Configuration = Substitute.For<IConfiguration>();
            var Environment = Substitute.For<IHostEnvironment>();

            // Act
            var Result = _TestClass.ConfigureMVC(MVCBuilder, Configuration, Environment);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(MVCBuilder, Result);
        }

        [Fact]
        public void CanCallConfigureMVCWithNullValues()
        {
            _TestClass.ConfigureMVC(Substitute.For<IMvcBuilder>(), default, Substitute.For<IHostEnvironment>());
            _TestClass.ConfigureMVC(Substitute.For<IMvcBuilder>(), Substitute.For<IConfiguration>(), default);
            _TestClass.ConfigureMVC(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.ConfigureMVC(default, default, default);
        }

        [Fact]
        public void CanCallOptions()
        {
            // Arrange
            var Options = new MvcOptions
            {
                EnableEndpointRouting = true,
                AllowEmptyInputInBodyModelBinding = true,
                SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false,
                SuppressInputFormatterBuffering = false,
                SuppressOutputFormatterBuffering = false,
                EnableActionInvokers = false,
                MaxModelValidationErrors = 827983212,
                RespectBrowserAcceptHeader = false,
                ReturnHttpNotAcceptable = true,
                SslPort = 2000306516,
                RequireHttpsPermanent = false,
                MaxValidationDepth = 164701958,
                ValidateComplexTypesIfChildValidationFails = false,
                SuppressAsyncSuffixInActionNames = false,
                MaxModelBindingCollectionSize = 1353876861,
                MaxModelBindingRecursionDepth = 72925627,
                MaxIAsyncEnumerableBufferLimit = 359943086
            };
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
            _ = _TestClass.Options(new MvcOptions
            {
                EnableEndpointRouting = false,
                AllowEmptyInputInBodyModelBinding = true,
                SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false,
                SuppressInputFormatterBuffering = true,
                SuppressOutputFormatterBuffering = false,
                EnableActionInvokers = false,
                MaxModelValidationErrors = 95407607,
                RespectBrowserAcceptHeader = false,
                ReturnHttpNotAcceptable = false,
                SslPort = 1604069249,
                RequireHttpsPermanent = true,
                MaxValidationDepth = 10471409,
                ValidateComplexTypesIfChildValidationFails = false,
                SuppressAsyncSuffixInActionNames = false,
                MaxModelBindingCollectionSize = 1068947027,
                MaxModelBindingRecursionDepth = 1007865240,
                MaxIAsyncEnumerableBufferLimit = 2137765139
            }, default, Substitute.For<IHostEnvironment>());
            _TestClass.Options(new MvcOptions
            {
                EnableEndpointRouting = false,
                AllowEmptyInputInBodyModelBinding = true,
                SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false,
                SuppressInputFormatterBuffering = true,
                SuppressOutputFormatterBuffering = false,
                EnableActionInvokers = false,
                MaxModelValidationErrors = 95407607,
                RespectBrowserAcceptHeader = false,
                ReturnHttpNotAcceptable = false,
                SslPort = 1604069249,
                RequireHttpsPermanent = true,
                MaxValidationDepth = 10471409,
                ValidateComplexTypesIfChildValidationFails = false,
                SuppressAsyncSuffixInActionNames = false,
                MaxModelBindingCollectionSize = 1068947027,
                MaxModelBindingRecursionDepth = 1007865240,
                MaxIAsyncEnumerableBufferLimit = 2137765139
            }, Substitute.For<IConfiguration>(), default);
            _TestClass.Options(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Options(default, default, default);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestControllerModuleBaseClass(_Name, _Category, _ContentPath, _Tags);

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal(_Name, Instance.Name);
            Assert.Equal(_Category, Instance.Category);
            Assert.Equal(_ContentPath, Instance.ContentPath);
            Assert.Equal(_Tags, Instance.Tags);

            // Act
            Instance = new TestControllerModuleBaseClass();

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal("ControllerModuleBaseClass_1Tests.TestControllerModuleBaseClass", Instance.Name);
            Assert.Equal("ASPNet", Instance.Category);
            Assert.Equal("wwwroot/Content/ASPNet/ControllerModuleBaseClass_1Tests.TestControllerModuleBaseClass", Instance.ContentPath);
            Assert.NotNull(Instance.Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidCategory(string value)
        {
            _ = new TestControllerModuleBaseClass(_Name, value, _ContentPath, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContentPath(string value)
        {
            _ = new TestControllerModuleBaseClass(_Name, _Category, value, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value)
        {
            _ = new TestControllerModuleBaseClass(value, _Category, _ContentPath, _Tags);
        }

        [Fact]
        public void CanConstructWithNullTags()
        {
            _ = new TestControllerModuleBaseClass(_Name, _Category, _ContentPath, default);
        }

        private class TestControllerModuleBaseClass : ControllerModuleBaseClass<TestControllerModuleBaseClass>
        {
            public TestControllerModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags) : base(name, category, contentPath, tags)
            {
            }

            public TestControllerModuleBaseClass() : base()
            {
            }
        }
    }
}