namespace Gestalt.ASPNet.Tests.ExtensionMethods
{
    using Gestalt.ASPNet.ExtensionMethods;
    using Gestalt.Tests.Helpers;
    using Microsoft.AspNetCore.Builder;
    using System;
    using System.Reflection;
    using Xunit;

    public class WebApplicationBuilderExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(WebApplicationBuilderExtensions);

        [Fact]
        public static void CanCallUseGestalt()
        {
            // Arrange
            var App = WebApplication.CreateBuilder();
            var Args = new[] { "TestValue931109757", "TestValue125126854", "TestValue330651101" };
            var Assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };

            // Act
            var Result = App.UseGestalt(Args, Assemblies);

            // Assert
            Assert.NotNull(Result);
            Assert.NotNull(Result.Services);
        }

        [Fact]
        public static void CanCallUseGestaltWithNullArgs()
        {
            WebApplication.CreateBuilder().UseGestalt(default, new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) });
            WebApplication.CreateBuilder().UseGestalt(default, default);
            WebApplication.CreateBuilder().UseGestalt(new[] { "TestValue-1", "TestValue-2", "TestValue-3" }, default);
        }

        [Fact]
        public static void UseGestaltPerformsMapping()
        {
            // Arrange
            var App = WebApplication.CreateBuilder();
            var Args = new[] { "TestValue282674785", "TestValue643503830", "TestValue1377056499" };
            var Assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };

            // Act
            var Result = App.UseGestalt(Args, Assemblies);

            // Assert
            Assert.NotNull(Result);
            Assert.NotNull(Result.Services);
            Assert.NotNull(Result.Configuration);
            Assert.NotNull(Result.Environment);
        }
    }
}