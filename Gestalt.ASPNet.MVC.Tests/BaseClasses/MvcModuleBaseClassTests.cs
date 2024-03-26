namespace Gestalt.ASPNet.MVC.Tests.BaseClasses
{
    using Gestalt.ASPNet.MVC.BaseClasses;
    using Gestalt.Tests.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSubstitute;
    using System;
    using Xunit;

    public class MvcModuleBaseClass_1Tests : TestBaseClass
    {
        public MvcModuleBaseClass_1Tests()
        {
            _Name = "TestValue1335960113";
            _Category = "TestValue966733231";
            _ContentPath = "TestValue468335660";
            _Tags = new[] { "TestValue740309588", "TestValue1836641462", "TestValue2073426856" };
            _TestClass = new TestMvcModuleBaseClass(_Name, _Category, _ContentPath, _Tags);
        }

        private readonly string _Category;

        private readonly string _ContentPath;

        private readonly string _Name;

        private readonly string[] _Tags;

        private readonly TestMvcModuleBaseClass _TestClass;

        protected override Type? ObjectType { get; set; } = typeof(TestMvcModuleBaseClass);

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
            _ = _TestClass.ConfigureMVC(Substitute.For<IMvcBuilder>(), default, Substitute.For<IHostEnvironment>());
            _ = _TestClass.ConfigureMVC(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _ = _TestClass.ConfigureMVC(Substitute.For<IMvcBuilder>(), Substitute.For<IConfiguration>(), default);
            _ = _TestClass.ConfigureMVC(default, default, default);
        }

        [Fact]
        public void CanCallOptions()
        {
            // Arrange
            var Options = new MvcOptions
            {
                EnableEndpointRouting = true,
                AllowEmptyInputInBodyModelBinding = false,
                SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true,
                SuppressInputFormatterBuffering = true,
                SuppressOutputFormatterBuffering = false,
                EnableActionInvokers = true,
                MaxModelValidationErrors = 1815469643,
                RespectBrowserAcceptHeader = false,
                ReturnHttpNotAcceptable = false,
                SslPort = 1910700993,
                RequireHttpsPermanent = true,
                MaxValidationDepth = 1643361569,
                ValidateComplexTypesIfChildValidationFails = false,
                SuppressAsyncSuffixInActionNames = false,
                MaxModelBindingCollectionSize = 449387368,
                MaxModelBindingRecursionDepth = 1488505055,
                MaxIAsyncEnumerableBufferLimit = 821024735
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
            _TestClass.Options(new MvcOptions
            {
                EnableEndpointRouting = true,
                AllowEmptyInputInBodyModelBinding = false,
                SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true,
                SuppressInputFormatterBuffering = true,
                SuppressOutputFormatterBuffering = true,
                EnableActionInvokers = false,
                MaxModelValidationErrors = 1981897453,
                RespectBrowserAcceptHeader = true,
                ReturnHttpNotAcceptable = true,
                SslPort = 819216221,
                RequireHttpsPermanent = false,
                MaxValidationDepth = 1670997753,
                ValidateComplexTypesIfChildValidationFails = true,
                SuppressAsyncSuffixInActionNames = true,
                MaxModelBindingCollectionSize = 1117347560,
                MaxModelBindingRecursionDepth = 150734026,
                MaxIAsyncEnumerableBufferLimit = 1372681059
            }, default, Substitute.For<IHostEnvironment>());
            _TestClass.Options(new MvcOptions
            {
                EnableEndpointRouting = true,
                AllowEmptyInputInBodyModelBinding = false,
                SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true,
                SuppressInputFormatterBuffering = true,
                SuppressOutputFormatterBuffering = true,
                EnableActionInvokers = false,
                MaxModelValidationErrors = 1981897453,
                RespectBrowserAcceptHeader = true,
                ReturnHttpNotAcceptable = true,
                SslPort = 819216221,
                RequireHttpsPermanent = false,
                MaxValidationDepth = 1670997753,
                ValidateComplexTypesIfChildValidationFails = true,
                SuppressAsyncSuffixInActionNames = true,
                MaxModelBindingCollectionSize = 1117347560,
                MaxModelBindingRecursionDepth = 150734026,
                MaxIAsyncEnumerableBufferLimit = 1372681059
            }, Substitute.For<IConfiguration>(), default);
            _TestClass.Options(default, Substitute.For<IConfiguration>(), Substitute.For<IHostEnvironment>());
            _TestClass.Options(default, default, default);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TestMvcModuleBaseClass(_Name, _Category, _ContentPath, _Tags);

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal(_Name, Instance.Name);
            Assert.Equal(_Category, Instance.Category);
            Assert.Equal(_ContentPath, Instance.ContentPath);
            Assert.Equal(_Tags, Instance.Tags);

            // Act
            Instance = new TestMvcModuleBaseClass();

            // Assert
            Assert.NotNull(Instance);
            Assert.Equal("MvcModuleBaseClass_1Tests.TestMvcModuleBaseClass", Instance.Name);
            Assert.Equal("ASPNet", Instance.Category);
            Assert.Equal("wwwroot/Content/ASPNet/MvcModuleBaseClass_1Tests.TestMvcModuleBaseClass", Instance.ContentPath);
            Assert.NotNull(Instance.Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidCategory(string value)
        {
            _ = new TestMvcModuleBaseClass(_Name, value, _ContentPath, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContentPath(string value)
        {
            _ = new TestMvcModuleBaseClass(_Name, _Category, value, _Tags);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value)
        {
            _ = new TestMvcModuleBaseClass(value, _Category, _ContentPath, _Tags);
        }

        [Fact]
        public void CanConstructWithNullTags()
        {
            _ = new TestMvcModuleBaseClass(_Name, _Category, _ContentPath, default);
        }

        private class TestMvcModuleBaseClass : MvcModuleBaseClass<TestMvcModuleBaseClass>
        {
            public TestMvcModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags) : base(name, category, contentPath, tags)
            {
            }

            public TestMvcModuleBaseClass() : base()
            {
            }
        }
    }
}